
using Parts_locator.Models;
using System.Data;


namespace Parts_locator.View.Rotor
{
    internal interface ProductVIew
    {
        DataTable productslistData { get; }

        Products selectedProduct { get; }

    }
}
