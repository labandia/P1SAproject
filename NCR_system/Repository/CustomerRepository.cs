using MSDMonitoring.Data;
using NCR_system.Interface;
using NCR_system.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NCR_system.Repository
{
    internal class CustomerRepository : ICustomerComplaint
    {
        public async Task<List<CustomerModel>> GetCustomerData(
            string search,
            int departmentID,
            int type,
            int Stats,
            int pageNumber,
            int pageSize)
        {
            int offset = (pageNumber - 1) * pageSize;

            Dictionary<string, object> parameters = new Dictionary<string, object>();

            string strquery = $@"SELECT RecordID,
		                        FORMAT(DateCreated, 'MM/dd/yy') as DateCreated,
		                        ModelNo,LotNo,NGQty,
		                        Details,Status,SectionID
                                ,RegNo,CustomerName,CCtype
                            FROM PC_CustomerConplaint
                            WHERE IsDelete = 0 ";

            // Filter By SMT Line
            if (departmentID != 0)
            {
                strquery += " AND SectionID = @SectionID";
                parameters.Add("@SectionID", departmentID);
            }

            // Filter By SMT Line
            if (Stats != 0)
            {
                strquery += " AND Status = @Status";
                parameters.Add("@Status", Stats);
            }


            // Search Text
            if (!string.IsNullOrEmpty(search))
            {
                strquery += $@" AND (
                                @Search IS NULL
                                OR ModelNo LIKE '%' + @Search + '%'
                              )";
                parameters.Add("@Search", search);
            }

            strquery += $@" AND CCtype = @CCtype";
            parameters.Add("@CCtype", type);

            strquery += $@" ORDER BY RecordID DESC";

            // If the Get Data has a Pagination function
            if (pageSize != 0)
            {
                strquery += $@" OFFSET @Offset ROWS
                            FETCH NEXT @PageSize ROWS ONLY";
                parameters.Add("@Offset", offset);
                parameters.Add("@PageSize", pageSize);
            }


            return await SqlDataAccess.GetData<CustomerModel>(strquery, parameters);
        }

        public Task<CustomerModel> GetCustomerDataByID(int recordID)
        {
            string strsql = $@"SELECT TOP 1 RecordID,
		                        FORMAT(DateCreated, 'MM/dd/yy') as DateCreated,
		                        ModelNo,LotNo,NGQty,
		                        Details,Status,SectionID
                                ,RegNo,CustomerName,CCtype
                            FROM PC_CustomerConplaint
                            WHERE RecordID = @RecordID
                            ORDER BY RecordID DESC";
            return SqlDataAccess.GetDataByID<CustomerModel>(strsql, new { RecordID = recordID });
        }

        public Task<List<CustomerTotalModel>> GetCustomersOpenItem(int type = 0, int  sec = 0)
        {
            string strquery = $@"SELECT 
                                s.DepartmentName,
                                COUNT(c.Status) AS totalOpen
                            FROM PC_Section s
                            LEFT JOIN PC_CustomerConplaint c
                                ON c.SectionID = s.SectionID 
                                AND c.CCtype = @CCtype 
                                AND c.Status = 1 
                                AND c.IsDelete = 0";

            Dictionary<string, object> parameters = new Dictionary<string, object>();


            // Filter By Process 
            if (sec != 0)
            {
                strquery += @" WHERE c.SectionID = @SectionID";
                parameters.Add("@SectionID", sec);
            }

            strquery += @" GROUP BY 
                                s.SectionID,
                                s.DepartmentName
                            ORDER BY
                                s.SectionID ASC;";

            parameters.Add("@CCtype", type);

            return SqlDataAccess.GetData<CustomerTotalModel>(strquery, parameters);
        }

       

        public async Task<bool> InsertCustomerData(CustomerModel customer, int type)
        {
            bool result = false;

            if(type == 0)
            {
                string strsql = $@"INSERT INTO PC_CustomerConplaint(ModelNo,LotNo,NGQty,Details,SectionID,RegNo,CustomerName,CCtype, UploadImage)
                               VALUES(@ModelNo, @LotNo, @NGQty, @Details, @SectionID, @RegNo, @CustomerName, @CCtype, @UploadImage)";
                var parameter = new
                {
                    customer.ModelNo,
                    customer.LotNo,
                    customer.NGQty,
                    customer.Details,
                    customer.SectionID,
                    customer.RegNo,
                    customer.CustomerName,
                    customer.CCtype, 
                    customer.UploadImage
                };
                result = await SqlDataAccess.UpdateInsertQuery(strsql, parameter);
            }
            else
            {
                string strsql = $@"INSERT INTO PC_CustomerConplaint(ModelNo,LotNo,NGQty,Details,SectionID,CCtype, UploadImage)
                               VALUES(@ModelNo, @LotNo, @NGQty, @Details, @SectionID, @CCtype, @UploadImage)";
                var parameter = new
                {
                    customer.ModelNo,
                    customer.LotNo,
                    customer.NGQty,
                    customer.Details,
                    customer.SectionID,
                    customer.CCtype, 
                    customer.UploadImage
                };
                result = await SqlDataAccess.UpdateInsertQuery(strsql, parameter);
            }

            return result;
        }

        public async Task<bool> UpdateCustomerData(CustomerModel customer, int type)
        {
            bool result = false;

            if (type == 0)
            {
                string strsql = $@"UPDATE PC_CustomerConplaint SET ModelNo =@ModelNo, LotNo =@LotNo ,NGQty =@NGQty,
                             Details =@Details, Status =@Status, SectionID =@SectionID, RegNo =@RegNo,
                             CustomerName =@CustomerName
                             WHERE RecordID = @RecordID";

                var parameter = new
                {
                    customer.RecordID,
                    customer.ModelNo,
                    customer.LotNo,
                    customer.NGQty,
                    customer.Details,
                    customer.Status,
                    customer.SectionID,
                    customer.RegNo,
                    customer.CustomerName,
                    customer.CCtype
                };

                result = await SqlDataAccess.UpdateInsertQuery(strsql, parameter);
            }
            else
            {
                string strsql = $@"UPDATE PC_CustomerConplaint SET ModelNo =@ModelNo, 
                             LotNo =@LotNo ,NGQty =@NGQty, Status =@Status,
                             Details =@Details,SectionID =@SectionID
                             WHERE RecordID = @RecordID";

                var parameter = new
                {
                    customer.RecordID,
                    customer.ModelNo,
                    customer.LotNo,
                    customer.NGQty,
                    customer.Status,    
                    customer.Details,
                    customer.SectionID,
                    customer.CCtype
                };
                result = await SqlDataAccess.UpdateInsertQuery(strsql, parameter);
            }

            return result;
        }
    }
}
