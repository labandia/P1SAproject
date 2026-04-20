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

        public Task<List<TraceableSubAssyModel>> GetSubAssyDatalist(string finalShopOrder)
        {
             return SqlDataAccess.GetData<TraceableSubAssyModel>($@"
                SELECT * FROM FanTraceabilitySub 
                WHERE FinalShopOrder = @FinalShopOrder", 
                new { FinalShopOrder = finalShopOrder });
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
