using ProgramPartListWeb.Areas.Rotor.Interface;
using ProgramPartListWeb.Areas.Rotor.Model;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Rotor.Data
{
    public class RegistrationServices : IRotorRegistration
    {
        public Task<bool> AddRegistration(RotorRegistrationModel masterlist)
        {
            string strquery = @"
                IF EXISTS (
                    SELECT 1
                    FROM Registration
                    WHERE RegistrationNo = @RegistrationNo
                      AND IsDeleted = 1
                )
                BEGIN
                    UPDATE Registration
                    SET
                        IsDeleted = 0,
                        Desciprtion = @Desciprtion,
                        Remarks = @Remarks,
                        CategoryID = @CategoryID,
                        DepartmentID = @DepartmentID,
                        DateCreated = GETDATE()
                    WHERE RegistrationNo = @RegistrationNo;
                END
                ELSE IF NOT EXISTS (
                    SELECT 1
                    FROM Registration
                    WHERE RegistrationNo = @RegistrationNo
                )
                BEGIN
                    INSERT INTO Registration
                    (
                        RegistrationNo,
                        Desciprtion,
                        Remarks,
                        CategoryID,
                        DepartmentID
                    )
                    VALUES
                    (
                        @RegistrationNo,
                        @Desciprtion,
                        @Remarks,
                        @CategoryID,
                        @DepartmentID
                    );
                END";

            return SqlDataAccess.UpdateInsertQuery(strquery, masterlist);

        }

        public Task<bool> DeleteRegistration(int registID)
        {
            return SqlDataAccess.UpdateInsertQuery($@"UPDATE Registration
                                    SET IsDeleted = 1
                                    WHERE RegistrationID = @RegistrationID;", 
                                    new { RegistrationID = registID });
        }

        public Task<bool> EditRegistration(RotorRegistrationModel masterlist)
        {
            return SqlDataAccess.UpdateInsertQuery(@"
                        UPDATE Registration
                        SET
                            RegistrationNo = @RegistrationNo,
                            Desciprtion    = @Desciprtion,
                            Remarks        = @Remarks,
                            CategoryID     = @CategoryID,
                            DepartmentID   = @DepartmentID
                        WHERE RegistrationID = @RegistrationID
                          AND IsDeleted = 0;",
                        masterlist);
        }

        public async Task<PagedResult<RotorRegistrationModel>> GetRegistrationsList(
            string search,
            int monthfilter,
            int intyear,
            int catID,
            int Department,
            int pageNumber,
            int pageSize)
        {
            int offset = (pageNumber - 1) * pageSize;
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            string countQuery = "SELECT COUNT(*) FROM Registration WHERE IsDeleted = 0 ";



            string strquery = @"SELECT r.RegistrationID
                                        ,r.DateCreated
                                        ,r.RegistrationNo
                                        ,r.Desciprtion
                                        ,r.Remarks
                                        ,r.CategoryID
                                        ,c.CategoryName
                                        ,r.DepartmentID
                                    FROM Registration r 
                                    INNER JOIN Register_Category c 
                                    ON c.CategoryID = r.CategoryID
                                    WHERE r.IsDeleted = 0 ";

            if (monthfilter != 0 && intyear != 0)
            {
                strquery += "AND MONTH(r.DateCreated) = @Month AND YEAR(r.DateCreated) = @strYear";
                countQuery += "AND MONTH(DateCreated) = @Month AND YEAR(DateCreated) = @strYear ";
                parameters.Add("@Month", monthfilter);
                parameters.Add("@strYear", intyear);
            }

            // Filter By Area 
            if (catID != 0)
            {
                strquery += "AND r.CategoryID = @CategoryID";
                countQuery += "AND CategoryID = @CategoryID ";

                parameters.Add("@CategoryID", catID);
            }

            // Filter By Model Type 
            if (Department != 0)
            {
                strquery += " AND r.DepartmentID = @DepartmentID";
                countQuery += " AND DepartmentID = @DepartmentID ";
                parameters.Add("@DepartmentID", Department);
            }



            // Search Partnumber
            if (!string.IsNullOrEmpty(search))
            {
                strquery += $@" AND (
                                @Search IS NULL
                                OR r.RegistrationNo LIKE '%' + @Search + '%'
                              )";
                countQuery += $@" AND (
                             @Search IS NULL
                             OR RegistrationNo LIKE '%' + @Search + '%'
                           )";
                parameters.Add("@Search", search);
            }

            strquery += $@" ORDER BY r.RegistrationID DESC";

            // If the Get Data has a Pagination function
            if (pageSize != 0)
            {
                strquery += $@" OFFSET @Offset ROWS
                            FETCH NEXT @PageSize ROWS ONLY";
                parameters.Add("@Offset", offset);
                parameters.Add("@PageSize", pageSize);
            }


            var items = await SqlDataAccess.GetData<RotorRegistrationModel>(strquery, parameters);

            // Now get the total count
            int TotalRecords = await SqlDataAccess.GetCountDataSync(countQuery, parameters);

            return new PagedResult<RotorRegistrationModel>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = TotalRecords
            };
        }

        public Task<List<string>> GetRegistrationYear()
        {
            return SqlDataAccess.StringList($@"SELECT 
                            YEAR(DateCreated) AS GetYear
                        FROM Registration
                        GROUP BY YEAR(DateCreated)
                        ORDER BY [GetYear];");
        }
    }
}