using checkpoint.Views.Promotions.Models;
using checkpoint.Views.Promotions.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace checkpoint.Views.Promotions.Presenters
{
    public class PromotionsPresenter
    {
        private readonly IPromotionsServices _promotionsServices;

        public PromotionsPresenter(IPromotionsServices promotionsServices)
        {
            _promotionsServices = promotionsServices;
        }

        public void DeleteProductoPromocion(long idProductoPromocion)
        {
            _promotionsServices.DeleteProductoPromocion(idProductoPromocion);
        }

        public List<Departamentos> GetAllDepartamentos()
        {
            return _promotionsServices.GetAllDepartamentos();
        }

        public List<Marca> GetAllMarca()
        {
            return _promotionsServices.GetAllMarca();
        }

        public List<Promociones> GetAllPromociones()
        {
            return _promotionsServices.GetAllPromociones();
        }

        public ProductosPromocion GetProductoByPLU(string plu)
        {
            return _promotionsServices.GetProductoByPLU(plu);
        }

        public Promociones GetPromocionById(long idPromocion)
        {
            return _promotionsServices.GetPromocionById(idPromocion);
        }

        public Promociones SavePromotion(Promociones promotion)
        {
            return _promotionsServices.SavePromotion(promotion);
        }
    }
}
