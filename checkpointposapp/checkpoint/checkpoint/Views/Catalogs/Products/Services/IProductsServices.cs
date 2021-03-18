using checkpoint.Views.Catalogs.Products.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.Products.Services
{
    public interface IProductsServices
    {
        List<Departamentos> GetAllDepartamentos();
        List<Unidades> GetAllUnidades();        
        List<Marca> GetAllMarcas();
        List<CatalogoSat> GetAllCatalogoSat();
        List<UnidadSat> GetAllUnidadesSat();
        List<Impuestos> GetAllImpuestos();
        List<UnidadSat> GetUnidadesSatByid(int unitSatId);
        Task<Productos> SaveProduct(Productos productos);
        Productos GetProductObjectByPLU(string plu);
        List<Proveedores> GetAllSuppliers();
        Task<List<Productos>> SaveProducts(List<Productos> productlist);
        void DeleteProductoProveedorById(int idProductoProveedor);
        void DeleteImpuestoProductoById(int idImpuestoProducto);
        void DeletePluProductoById(int idPlu);

        List<Productos> GetAllProductos();
    }
}
