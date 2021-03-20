using checkpoint.Views.Promotions.Models;
using System.Collections.Generic;

namespace checkpoint.Views.Promotions.Services
{
    public interface IPromotionsServices
    {
        List<Promociones> GetAllPromociones();
        Promociones SavePromotion(Promociones promotion);
        ProductosPromocion GetProductoByPLU(string plu);
        List<Departamentos> GetAllDepartamentos();
        List<Marca> GetAllMarca();
        Promociones GetPromocionById(long idPromocion);
        void DeleteProductoPromocion(long idProductoPromocion);
    }
}
