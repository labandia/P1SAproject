using Parts_locator.Data;
using System.Data;

namespace Parts_locator.Models
{
    public interface IProductRepository
    {
        DataTable GetAllProducts();
        DataTable SearchProductLocation(string partnum);
    
        
    }
}
