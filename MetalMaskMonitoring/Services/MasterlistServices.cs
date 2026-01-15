using MetalMaskMonitoring.Interface;
using MetalMaskMonitoring.Model;
using ProgramPartListWeb.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MetalMaskMonitoring.Services
{
    internal class MasterlistServices : IMaskMasterlist
    {
        public Task<bool> AddMasterlist(MetalMaskModel masterlist)
        {
            string strquery = $@"UPDATE MetalMask_Masterlist
                                 SET
                                    Alternate    = @Alternate,
                                    Side         = @Side,
                                    Thickness    = @Thickness,
                                    Blocks       = @Blocks,
                                    [Condition]  = @Condition,
                                    Remarks      = @Remarks,
                                    DateReceived = @DateReceived,
                                    ModelType    = @ModelType
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
                            Remarks      = @Remarks,
                            DateReceived = @DateReceived,
                            ModelType    = @ModelType,
                            AREA         = @AREA
                        WHERE RecordID = @RecordID";

            return SqlDataAccess.UpdateInsertQuery(strquery, masterlist);
        }

        public Task<List<MetalMaskModel>> GetMasterlist(string partnum, int Area, int Model, string search)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

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

            if (!string.IsNullOrEmpty(partnum))
            {
                strquery += "AND Partnumber = @Partnumber";
                parameters.Add("@Partnumber", partnum);
            }

            if (Area != 0)
            {
                strquery += "AND AREA = @AREA";
                parameters.Add("@AREA", Area);
            }

            if (Model != 0)
            {
                strquery += "AND ModelType = @ModelType";
                parameters.Add("@ModelType", Model);
            }

            if (!string.IsNullOrEmpty(search))
            {
                strquery += $@" AND (
                                @Search IS NULL
                                OR Partnumber LIKE '%' + @Search + '%'
                              )";
                parameters.Add("@Search", search);
            }


            strquery += " ORDER BY RecordID ASC";

            return SqlDataAccess.GetData<MetalMaskModel>(strquery, parameters);
        }
    }
}
