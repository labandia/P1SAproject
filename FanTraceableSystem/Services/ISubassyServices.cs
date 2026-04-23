using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FanTraceableSystem.Data;
using FanTraceableSystem.Interface;
using MSDMonitoring.Data;

namespace FanTraceableSystem.Services
{
    internal class ISubassyServices : ISubassy
    {
        public Task<bool> AddSubAssy(TraceableSubAssyModel subassy)
        {
            return SqlDataAccess.UpdateInsertQuery($@"INSERT INTO FanTraceabilitySub
                (FinalShopOrder, ShopOrder, PreparedQuantity, LotNo, Line, SubAssyIssued) 
                VALUES(@FinalShopOrder, @ShopOrder, @PreparedQuantity, @LotNo, @Line, @SubAssyIssued)", new
            { subassy});
        }

        public Task<bool> CheckShopOrder(string ShopOrder)
        {
            return SqlDataAccess.Checkdata($@"SELECT COUNT(1) FROM FanTraceabilitySub 
                WHERE ShopOrder = @ShopOrder", new { ShopOrder = ShopOrder });
        }

        public Task<List<TraceableSubAssyModel>> GetSubAssyDatalist(int finalid)
        {
             return SqlDataAccess.GetData<TraceableSubAssyModel>($@"
                SELECT SubAssyID, FinalShopOrder, ShopOrder, PreparedQuantity, LotNo, Line, SubAssyIssued, Rev
                FROM FanTraceabilitySub 
                WHERE FinalId = @FinalId", 
                new { FinalId = finalid });
        }

        public Task<bool> RemoveSubAssy(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateSubAssy(TraceableSubAssyModel subassy)
        {
            return SqlDataAccess.UpdateInsertQuery($@"
                UPDATE FanTraceabilitySub SET FinalShopOrder =@FinalShopOrder, 
                ShopOrder =@ShopOrder, PreparedQuantity =@PreparedQuantity, LotNo =@LotNo, 
                Line =@Line, SubAssyIssued =@SubAssyIssued WHERE SubAssyID =@SubAssyID", new
            { subassy });
        }
    }
}
