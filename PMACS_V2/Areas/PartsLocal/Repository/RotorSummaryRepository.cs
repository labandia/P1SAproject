
using PMACS_V2.Areas.PartsLocal.Interface;
using PMACS_V2.Areas.PartsLocal.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMACS_V2.Areas.PartsLocal.Repository
{
    public class RotorSummaryRepository : IShopOrderIn, IShopOrderOut
    {
       
        public Task<bool> AddTransactionIN(ShopOrderInModel shop)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> AddTransactionOut(ShopOrderOutModel shop)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteTransaction(ShopOrderInModel shop)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteTransactionOut(ShopOrderOutModel shop)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> EditTransaction(ShopOrderInModel shop)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> EditTransactionOut(ShopOrderOutModel shop)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ShopOrderInModel>> GetShopOderInlist()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ShopOrderOutModel>> GetShopOderOutlist()
        {
            throw new System.NotImplementedException();
        }
    }
}