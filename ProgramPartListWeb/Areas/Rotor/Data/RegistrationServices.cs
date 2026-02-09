using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.EMMA;
using ProgramPartListWeb.Areas.Rotor.Interface;
using ProgramPartListWeb.Areas.Rotor.Model;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Rotor.Data
{
    public class RegistrationServices : IRotorRegistration
    {
        public Task<bool> AddRegistration(RotorRegistrationModel masterlist)
        {
            string strquery = $@"IF NOT EXISTS (
                                    SELECT 1
                                    FROM Registration
                                    WHERE RegistrationNo = @RegistrationNo
                                      AND IsDeleted = 0
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
                            RegistrationNo = @NewRegistrationNo,
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

            string strquery = @"SELECT RegistrationID
                                  ,DateCreated
                                  ,RegistrationNo
                                  ,Desciprtion
                                  ,Remarks
                                  ,CategoryID
                                  ,DepartmentID
                              FROM Registration WHERE IsDeleted = 0 ";

            if (monthfilter != 0 && intyear != 0)
            {
                strquery += "AND MONTH(DateCreated) = @Month AND YEAR(DateCreated) = @strYear;";
                parameters.Add("@Month", monthfilter);
                parameters.Add("@strYear", intyear);
            }

            // Filter By Area 
            if (catID != 0)
            {
                strquery += "AND CategoryID = @CategoryID";
                parameters.Add("@CategoryID", catID);
            }

            // Filter By Model Type 
            if (Department != 0)
            {
                strquery += "AND DepartmentID = @DepartmentID";
                parameters.Add("@DepartmentID", Department);
            }



            // Search Partnumber
            if (!string.IsNullOrEmpty(search))
            {
                strquery += $@" AND (
                                @Search IS NULL
                                OR RegistrationNo LIKE '%' + @Search + '%'
                              )";
                parameters.Add("@Search", search);
            }

            // If the Get Data has a Pagination function
            if (pageSize != 0)
            {
                strquery += $@" ORDER BY RegistrationID DESC
                            OFFSET @Offset ROWS
                            FETCH NEXT @PageSize ROWS ONLY";
                parameters.Add("@Offset", offset);
                parameters.Add("@PageSize", pageSize);
            }

            if (pageSize == 0 && pageNumber == 0)
            {
                strquery += $@" ORDER BY RegistrationID DESC";
            }


            var items = await SqlDataAccess.GetData<RotorRegistrationModel>(strquery, parameters);

            int TotalRecords = items.Count;

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