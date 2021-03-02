using checkpoint.CheckPrices.Models;
using System.Collections.Generic;

namespace checkpoint.CheckPrices.Services
{
    public interface IProductsCheckServices
    {

        PLUProductCheck GetProductByPLU(string PLU);

        List<PLUProductCheck> GetProductByProductName(string name);

    }
}
