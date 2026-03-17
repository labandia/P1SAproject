using MSDMonitoring.Data;
using NCR_system.Interface;
using NCR_system.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace NCR_system.Repository
{
    internal class CustomerRepository : ICustomerComplaint
    {
        public Task<bool> DeleteCustomers(int record)
        {
            return SqlDataAccess.ExecuteAsync($@"UPDATE PC_CustomerConplaint 
                    SET IsDelete = 1 WHERE RecordID =@RecordID ", new { RecordID = record });
        }

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
		                        Details,Status,SectionID,RegNo,
                                CustomerName,CCtype, UploadImage
                            FROM PC_CustomerConplaint
                            WHERE IsDelete = 0 ";

            strquery += " AND Status = @Status";
            parameters.Add("@Status", Stats);

            // Filter By SMT Line
            if (departmentID != 0)
            {
                strquery += " AND SectionID = @SectionID";
                parameters.Add("@SectionID", departmentID);
            }

          

            // Search Text
            if (!string.IsNullOrEmpty(search))
            {
                strquery += $@" AND ModelNo LIKE '%' + @Search + '%'";
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


            return await SqlDataAccess.GetDataAsync<CustomerModel>(strquery, parameters);
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
            return SqlDataAccess.GetSingleAsync<CustomerModel>(strsql, new { RecordID = recordID });
        }

        public async Task<List<CustomerTotalModel>> GetCustomersOpenItem(int type = 0, int  sec = 0, int stats = 1)
        {
            string strquery = $@"SELECT 
                                s.DepartmentName,
                                COUNT(c.Status) AS totalOpen
                            FROM PC_Section s
                            LEFT JOIN PC_CustomerConplaint c
                                ON c.SectionID = s.SectionID 
                                AND c.CCtype = @CCtype 
                                AND c.Status = @Status 
                                AND c.IsDelete = 0";

            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("@Status", stats);

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

            return await SqlDataAccess.GetDataAsync<CustomerTotalModel>(strquery, parameters);
        }

       

        public async Task<bool> InsertCustomerData(CustomerModel customer, int type)
        {
            // 1 is form SDC 
            string strsql = (type == 0) ? 
                $@"INSERT INTO PC_CustomerConplaint
                    (ModelNo, LotNo, NGQty, Details, SectionID, CCtype, UploadImage, RegNo, CustomerName)
                    VALUES
                    (@ModelNo, @LotNo, @NGQty, @Details, @SectionID,  @CCtype, @UploadImage, @RegNo, @CustomerName)"
                : @"
                INSERT INTO PC_CustomerConplaint
                (ModelNo, LotNo, NGQty, Details, SectionID, CCtype, UploadImage)
                VALUES
                (@ModelNo, @LotNo, @NGQty, @Details, @SectionID,  @CCtype, @UploadImage)";

           

            return await SqlDataAccess.ExecuteAsync(strsql, customer);
        }

        public async Task<bool> UpdateCustomerData(CustomerModel customer, ComplaintUpdateType type)
        {
            string strsql = @"
                UPDATE PC_CustomerConplaint SET
                    ModelNo = @ModelNo,
                    LotNo = @LotNo,
                    NGQty = @NGQty,
                    Details = @Details,
                    Status = @Status,
                    SectionID = @SectionID";

            if (type == ComplaintUpdateType.WithCustomerInfo)
            {
                strsql += @",
                    RegNo = @RegNo,
                    CustomerName = @CustomerName";
            }

            strsql += @" WHERE RecordID = @RecordID";

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
                customer.CustomerName
            };

            return await SqlDataAccess.ExecuteAsync(strsql, parameter);
        }

        public Task<bool> UpdateCustomers(CustomerModel cus, int type)
        {
            string strsql = type != 1 
                ? $@"UPDATE PC_CustomerConplaint SET RegNo =@Regno, CustomerName =@CustomerName, LotNo =@LotNo, ModelNo =@ModelNo, 
                    NGQty =@NGQty, SectionID =@SectionID, Details =@Details, UploadImage =@UploadImage WHERE RecordID =@RecordID "
                : $@"UPDATE PC_CustomerConplaint SET ModelNo =@ModelNo, LotNo=@LotNo, NGQty =@NGQty, 
                    Details =@Details, Status =@Status, SectionID =@SectionID, UploadImage =@UploadImage
                    WHERE RecordID =@RecordID";

            return SqlDataAccess.ExecuteAsync(strsql, cus);
        }
    }
}
