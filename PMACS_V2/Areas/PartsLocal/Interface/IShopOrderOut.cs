using PMACS_V2.Areas.PartsLocal.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMACS_V2.Areas.PartsLocal.Interface
{
    internal interface IShopOrderOut
    {
        Task<IEnumerable<ShopOrderOutModel>> GetShopOderOutlist();
        Task<bool> AddTransactionOut(ShopOrderOutModel shop);
        Task<bool> EditTransactionOut(ShopOrderOutModel shop);
        Task<bool> DeleteTransactionOut(ShopOrderOutModel shop);
    }
}
