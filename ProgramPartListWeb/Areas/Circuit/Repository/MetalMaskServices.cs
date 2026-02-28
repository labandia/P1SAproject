using ProgramPartListWeb.Areas.Circuit.Interface;
using ProgramPartListWeb.Areas.Circuit.Models;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Circuit.Repository
{
    public class MetalMaskServices : IMaskMasterlist
    {
        public async Task<PagedResult<MetalMaskModel>> GetMetalMaskMasterlist(
            string search, 
            int Area, // -- 
            int ModelType,  
            int pageNumber, 
            int pageSize)
        {
            int offset = (pageNumber - 1) * pageSize;

            Dictionary<string, object> parameters = new Dictionary<string, object>();

            string countstring = @"SELECT COUNT(*) FROM MetalMask_Masterlist 
                                   WHERE IsDelete = 0 ";


            string strquery = @"SELECT RecordID
                                  ,Partnumber
                                  ,AREA
                                  ,Alternate
                                  ,Side
                                  ,Thickness
                                  ,Blocks
                                  ,Condition
                                  ,Remarks
                                  ,DateReceived
                                  ,ModelType
                                  ,DateManufacture
                              FROM MetalMask_Masterlist
                              WHERE IsDelete = 0 ";

            

            // Filter By Area 
            if (Area != 0)
            {
                strquery += "AND AREA = @AREA";
                countstring += "AND AREA = @AREA";
                parameters.Add("@AREA", Area);
            }

            // Filter By Model Type 
            if (ModelType != 0)
            {
                strquery += "AND ModelType = @ModelType";
                countstring += "AND ModelType = @ModelType";
                parameters.Add("@ModelType", ModelType);
            }

            // Search Partnumber
            if (!string.IsNullOrEmpty(search))
            {
                strquery += $@" AND (Partnumber LIKE '%' + @Search + '%')";
                countstring += $@" AND  Partnumber LIKE '%' + @Search + '%')";
                parameters.Add("@Search", search);
            }

            // If the Get Data has a Pagination function
            if (pageSize != 0)
            {
                strquery += $@" ORDER BY RecordID ASC
                            OFFSET @Offset ROWS
                            FETCH NEXT @PageSize ROWS ONLY";
                parameters.Add("@Offset", offset);
                parameters.Add("@PageSize", pageSize);
            }

            //Debug.WriteLine(strquery);
           
            var items = await SqlDataAccess.GetData<MetalMaskModel>(strquery, parameters);

            int TotalRecords = await SqlDataAccess.GetCountData(countstring, parameters);

            return new PagedResult<MetalMaskModel>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = TotalRecords
            };
        }

        public Task<MetalMaskModel> GetMetalMaskByID(int ID)
        {
            string strquery = @"SELECT RecordID
                                  ,Partnumber
                                  ,AREA
                                  ,Alternate
                                  ,Side
                                  ,Thickness
                                  ,Blocks
                                  ,Condition
                                  ,Remarks
                                  ,DateReceived
                                  ,ModelType
                              FROM MetalMask_Masterlist
                              WHERE IsDelete = 0 ";
            var parameter = new { RecordID = ID };

            return SqlDataAccess.GetObjectOnly<MetalMaskModel>(strquery, parameter);
        }

        public async Task<List<MetalMaskModel>> SearchMetalMaskData(string partnum, int model)
        {
            string strquery = @"SELECT RecordID
                                    ,Partnumber
                                    ,AREA
                                    ,Alternate
                                    ,Side
                                    ,Thickness
                                    ,Blocks
                                    ,Condition
                                    ,Remarks
                                    ,DateReceived
                                    ,ModelType, 
	                                CASE 
		                                WHEN ModelType = 1 
			                                THEN NumPart + '-01'
		                                ELSE 
			                                NumPart
	                                END AS PWB_Blocks
                                FROM MetalMask_Masterlist
                                CROSS APPLY (
                                    SELECT 
                                        SUBSTRING(
                                            Partnumber,
                                            PATINDEX('%[0-9]%', Partnumber),
                                            PATINDEX('%[^0-9]%', SUBSTRING(Partnumber, PATINDEX('%[0-9]%', Partnumber), 50) + 'X') - 1
                                        ) AS NumPart
                                ) p WHERE Partnumber =@Partnumber AND ModelType = @model";
            var parameter = new { Partnumber = partnum, model = model };

            return await SqlDataAccess.GetData<MetalMaskModel>(strquery, parameter);
        }

        public Task<bool> AddMasterlist(MetalMaskModel masterlist)
        {
            string strquery = $@"UPDATE MetalMask_Masterlist
                                 SET
                                    Alternate    = @Alternate,
                                    Side         = @Side,
                                    Thickness    = @Thickness,
                                    Blocks       = @Blocks,
                                    [Condition]  = @Condition,
                                    Remarks      = @Remarks
                                WHERE Partnumber = @Partnumber
                                  AND AREA = @AREA
                                  AND IsDelete = 0;

                                IF @@ROWCOUNT = 0
                                BEGIN
                                    INSERT INTO MetalMask_Masterlist
                                    (
                                        Partnumber,
                                        AREA,
                                        Alternate,
                                        Side,
                                        Thickness,
                                        Blocks,
                                        [Condition],
                                        Remarks,
                                        DateReceived,
                                        ModelType
                                    )
                                    VALUES
                                    (
                                        @Partnumber,
                                        @AREA,
                                        @Alternate,
                                        @Side,
                                        @Thickness,
                                        @Blocks,
                                        @Condition,
                                        @Remarks,
                                        @DateReceived,
                                        @ModelType
                                    )
                                END";

            return SqlDataAccess.UpdateInsertQuery(strquery, masterlist);
        }

        public Task<bool> EditMasterlist(MetalMaskModel masterlist)
        {
            string strquery = @"IF EXISTS (
                            SELECT 1
                            FROM MetalMask_Masterlist
                            WHERE Partnumber = @Partnumber
                              AND AREA = @AREA
                              AND RecordID <> @RecordID
                              AND IsDelete = 0
                        )
                        BEGIN
                            RAISERROR('Duplicate AREA for the same Partnumber.', 16, 1);
                            RETURN;
                        END

                        UPDATE MetalMask_Masterlist
                        SET
                             Partnumber   = @Partnumber,
                             AREA         = @AREA,
                            Alternate    = @Alternate,
                            Side         = @Side,
                            Thickness    = @Thickness,
                            Blocks       = @Blocks,
                            [Condition]  = @Condition,
                            Remarks      = @Remarks
                        WHERE RecordID = @RecordID";

            return SqlDataAccess.UpdateInsertQuery(strquery, masterlist);
        }

      

       
       
    }
}