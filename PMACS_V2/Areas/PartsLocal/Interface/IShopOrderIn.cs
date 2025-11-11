using PMACS_V2.Areas.PartsLocal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMACS_V2.Areas.PartsLocal.Interface
{
    public interface IShopOrderIn
    {
        Task<IEnumerable<ShopOrderInModel>> GetShopOderInlist();
        Task<bool> AddTransactionIN(ShopOrderInModel shop);
        Task<bool> EditTransaction(ShopOrderInModel shop);  
        Task<bool> DeleteTransaction(ShopOrderInModel shop);
    }
}
