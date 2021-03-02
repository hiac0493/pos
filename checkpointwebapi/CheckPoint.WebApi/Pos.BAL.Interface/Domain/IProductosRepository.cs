using Pos.Business.Model;
using System.Collections.Generic;

namespace Pos.BAL.Interface.Domain
{
    public interface IProductosRepository : IGenericRepository<Productos>
    {
        IEnumerable<object> GetProductosByCriteria(string searchCriteria);
        object GetProductosWithSuppliers(int idProduct);
        IEnumerable<object> GetProductsByList(List<ProductosOrden> productos);
        IEnumerable<object> GetAllProductsByDepartment();
        IEnumerable<object> GetAllProductsByDepartment(int idDepartamento);
        IEnumerable<object> GetSuggestionProductsToBuyBySupplier(int supplier);
        IEnumerable<object> GetProductsSuggestToBuy(int supplier);
        IEnumerable<object> GetProductsSuggestToBuy();
        Productos GetProductObjectByPLU(string plu);
        IEnumerable<Productos> GetAllProducts();
    }
}
