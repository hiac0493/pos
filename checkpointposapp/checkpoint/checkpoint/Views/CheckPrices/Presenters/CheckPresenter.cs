using checkpoint.CheckPrices.Models;
using checkpoint.CheckPrices.Services;
using System.Collections.Generic;

namespace checkpoint.CheckPrices.Presenters
{
    public class CheckPresenter
    {
        private readonly IProductsCheckServices _productsCheckServices;

        public CheckPresenter(IProductsCheckServices productsCheckServices)
        {
            _productsCheckServices = productsCheckServices;
        }

        public PLUProductCheck GetProductByPLU(string PLU)
        {
            return _productsCheckServices.GetProductByPLU(PLU);
        }
        public List<PLUProductCheck> GetProductsByName(string name)
        {
            return _productsCheckServices.GetProductByProductName(name);
        }
    }
}
