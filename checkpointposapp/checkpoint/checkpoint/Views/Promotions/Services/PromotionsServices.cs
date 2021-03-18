using checkpoint.Resources;
using checkpoint.Views.Promotions.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace checkpoint.Views.Promotions.Services
{
    public class PromotionsServices : IPromotionsServices
    {
        public List<Departamentos> GetAllDepartamentos()
        {
            string webApiUrl = WebApiMethods.GetAllDepartamentos;
            IList<Departamentos> departmentsList = new List<Departamentos>();
            var result = App.HttpTools.HttpGetList<Departamentos>(webApiUrl, ref departmentsList, "Error al leer los departamentos");
            if (result == HttpStatusCode.OK)
                return departmentsList.ToList();
            else
                return null;
        }

        public List<Marca> GetAllMarca()
        {
            string webApiUrl = WebApiMethods.GetAllMarcas;
            IList<Marca> brandsList = new List<Marca>();
            var result = App.HttpTools.HttpGetList<Marca>(webApiUrl, ref brandsList, "Error en la lectura de las marcas");
            if (result == HttpStatusCode.OK)
                return brandsList.ToList();
            else
                return null;
        }

        public List<Promociones> GetAllPromociones()
        {
            string webApiUrl = WebApiMethods.GetAllPromociones;
            IList<Promociones> promocionesResult = new List<Promociones>();
            var result = App.HttpTools.HttpGetList<Promociones>(webApiUrl, ref promocionesResult, "Error en la lectura de las promociones");
            if (result == HttpStatusCode.OK)
                return promocionesResult.ToList();
            else
                return null;
        }

        public ProductosPromocion GetProductoByPLU(string plu)
        {
            string webApiUrl = WebApiMethods.GetProductByPLU + plu;
            ProductosPromocion productoResult = new ProductosPromocion();
            var result = App.HttpTools.HttpGetSingle<ProductosPromocion>(webApiUrl, ref productoResult, "Error al leer producto");
            if (result == HttpStatusCode.OK)
                return productoResult;
            else
                return null;
        }

        public Promociones SavePromotion(Promociones promotion)
        {
            string webApiUrl = WebApiMethods.SavePromocion;
            Promociones promocionResult = App.HttpTools.HttpPostObjectWithResponseDataAsync<Promociones, Promociones>(webApiUrl, promotion, "Error al guardar la promocion").Result;
            return promocionResult;
        }
    }
}
