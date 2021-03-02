using checkpoint.Views.Catalogs.Products.Models;
using checkpoint.Views.Catalogs.Products.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.Products.Presenters
{
    public class ProductsPresenter : IProductsServices
    {
        private readonly IProductsServices _productsServices;
        public ProductsPresenter(IProductsServices productsServices)
        {
            _productsServices = productsServices;
        }
        public List<Departamentos> GetAllDepartamentos()
        {
            return _productsServices.GetAllDepartamentos();
        }

        public List<Unidades> GetAllUnidades()
        {
            return _productsServices.GetAllUnidades();
        }        
        public List<Marca> GetAllMarcas()
        {
            return _productsServices.GetAllMarcas();
        }        
        
        public List<CatalogoSat> GetAllCatalogoSat()
        {
            return _productsServices.GetAllCatalogoSat();
        }        
        public List<UnidadSat> GetAllUnidadesSat()
        {
            return _productsServices.GetAllUnidadesSat();
        }        
        public List<Impuestos> GetAllImpuestos()
        {
            return _productsServices.GetAllImpuestos();
        }        
        public List<UnidadSat> GetUnidadesSatByid(int unitSatId)
        {
            return _productsServices.GetUnidadesSatByid(unitSatId);
        }

        public Task<Productos> SaveProduct(Productos productos)
        {
            return _productsServices.SaveProduct(productos);
        }

        public Productos GetProductObjectByPLU(string plu)
        {
            return _productsServices.GetProductObjectByPLU(plu);
        }

        public List<Proveedores> GetAllSuppliers()
        {
            return _productsServices.GetAllSuppliers();
        }

        public void DeleteProductoProveedorById(int idProductoProveedor)
        {
            _productsServices.DeleteProductoProveedorById(idProductoProveedor);
        }

        public void DeleteImpuestoProductoById(int idImpuestoProducto)
        {
            _productsServices.DeleteImpuestoProductoById(idImpuestoProducto);
        }

        public void DeletePluProductoById(int idPlu)
        {
            _productsServices.DeletePluProductoById(idPlu);
        }

        public List<Productos> GetAllProductos()
        {
            return _productsServices.GetAllProductos();
        }
    }
}
