using checkpoint.CheckPrices.Models;
using checkpoint.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace checkpoint.CheckPrices.Services
{
    public class ProductsCheckServices : IProductsCheckServices
    {
        public PLUProductCheck GetProductByPLU(string PLU)
        {
            string webApiUrl = WebApiMethods.GetProductByPLU + PLU;
            PLUProductCheck product = new PLUProductCheck();
            var ResponseOK = App.HttpTools.HttpGetSingle<PLUProductCheck>(webApiUrl, ref product, "No se encontró el producto");

            if(ResponseOK == HttpStatusCode.OK)
            {
                return product;
            }
            else
            {
                return null;
            }
        }

        public List<PLUProductCheck> GetProductByProductName(string name)
        {
            string webApiUrl = WebApiMethods.GetProdcutByName + name;
            IList<PLUProductCheck> productList = new List<PLUProductCheck>();
            var ResponseOK = App.HttpTools.HttpGetList<PLUProductCheck>(webApiUrl, ref productList, "No se encontró el producto");
            
            if (ResponseOK == HttpStatusCode.OK)
            {
                return productList.ToList();
            }
            else
            {
                return null;
            }
        }
    }
}
