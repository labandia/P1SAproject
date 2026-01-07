using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PMACS_V2.Areas.PartsLocal.Model;
using PMACS_V2.Models;

namespace PMACS_V2.Areas.PartsLocal.Interface
{
    public interface IShopOrderOut
    {
        Task<PagedResult<ShopOrderOutModel>> GetShopOderOutlist(
              DateTime startDate,
              DateTime endDate, 
              string search,
              int pageNumber,
              int pageSize);
        Task<bool> AddTransactionOut(ShopOrderOutModel shop);
        Task<bool> EditTransactionOut(ShopOrderOutModel shop);
        Task<bool> DeleteTransactionOut(ShopOrderOutModel shop);
    }
}
