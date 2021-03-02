using checkpoint.Resources;
using checkpoint.Views.Catalogs.Products.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.Products.Services
{
    public class ProductsServices : IProductsServices
    {
        public List<Departamentos> GetAllDepartamentos()
        {
            string webApiUrl = WebApiMethods.GetAllDepartamentos;
            IList<Departamentos> departamentosList = new List<Departamentos>();
            var ResponseOK = App.HttpTools.HttpGetList<Departamentos>(webApiUrl, ref departamentosList, "No se encontró el departamento");
            if(ResponseOK == HttpStatusCode.OK)
            {
                return departamentosList.ToList();
            }
            else
            {
                return null;
            }
        }

        public List<Marca> GetAllMarcas()
        {
            string webApiUrl = WebApiMethods.GetAllMarcas;
            IList<Marca> marcasList = new List<Marca>();
            var ResponseOK = App.HttpTools.HttpGetList<Marca>(webApiUrl, ref marcasList, "No se encontró la marca");
            if (ResponseOK == HttpStatusCode.OK)
            {
                return marcasList.ToList();
            }
            else
            {
                return null;
            }
        }

        public List<Unidades> GetAllUnidades()
        {
            string webApiUrl = WebApiMethods.GetAllUnidades;
            IList<Unidades> unidadesList = new List<Unidades>();
            var ResponseOK = App.HttpTools.HttpGetList<Unidades>(webApiUrl, ref unidadesList, "No se encontró la unidad");
            if(ResponseOK == HttpStatusCode.OK)
            {
                return unidadesList.ToList();
            }
            else
            {
                return null;
            }
        }
        public List<CatalogoSat> GetAllCatalogoSat()
        {
            string webApiUrl = WebApiMethods.GetAllCatalogoSat;
            IList<CatalogoSat> catalogoSatList = new List<CatalogoSat>();
            var ResponseOK = App.HttpTools.HttpGetList<CatalogoSat>(webApiUrl, ref catalogoSatList, "No se encontró el catalogo del SAT");
            if(ResponseOK == HttpStatusCode.OK)
            {
                return catalogoSatList.ToList();
            }
            else
            {
                return null;
            }
        }
        public List<UnidadSat> GetAllUnidadesSat()
        {
            string webApiUrl = WebApiMethods.GetAllUnidadesSat;
            IList<UnidadSat> unidadSatList = new List<UnidadSat>();
            var ResponseOK = App.HttpTools.HttpGetList<UnidadSat>(webApiUrl, ref unidadSatList, "No se encontró la unidad del SAT");
            if(ResponseOK == HttpStatusCode.OK)
            {
                return unidadSatList.ToList();
            }
            else
            {
                return null;
            }
        }
        public List<Impuestos> GetAllImpuestos()
        {
            string webApiUrl = WebApiMethods.GetAllImpuestos;
            IList<Impuestos> impuestosList = new List<Impuestos>();
            var ResponseOK = App.HttpTools.HttpGetList<Impuestos>(webApiUrl, ref impuestosList, "No se encontraron los impuestos");
            if(ResponseOK == HttpStatusCode.OK)
            {
                return impuestosList.ToList();
            }
            else
            {
                return null;
            }
        }
        public List<UnidadSat> GetUnidadesSatByid(int unitSatId)
        {
            string webApiUrl = WebApiMethods.GetUnidadesSatByid + unitSatId;
            IList<UnidadSat> unidadSatList = new List<UnidadSat>();
            var ResponseOK = App.HttpTools.HttpGetList<UnidadSat>(webApiUrl, ref unidadSatList, "No se encontró la unidad del SAT");
            if(ResponseOK == HttpStatusCode.OK)
            {
                return unidadSatList.ToList();
            }
            else
            {
                return null;
            }
        }

        public async Task<Productos> SaveProduct(Productos productos)
        {
            string webApiUrl = WebApiMethods.SaveProduct;   
            Productos productResult = await App.HttpTools.HttpPostObjectWithResponseDataAsync<Productos, Productos>(webApiUrl, productos, "Hubo un error en el guardado del producto").ConfigureAwait(false);
            return productResult;
        }

        public Productos GetProductObjectByPLU(string plu)
        {
            string webApiUrl = WebApiMethods.GetProductObjectByPLU + plu;
            Productos productResult = new Productos();
            var result = App.HttpTools.HttpGetSingle<Productos>(webApiUrl, ref productResult, "Error en la obtención del producto");
            if (result == HttpStatusCode.OK)
                return productResult;
            else
                return null;
        }

        public List<Proveedores> GetAllSuppliers()
        {
            string webApiUrl = WebApiMethods.GetAllProveedores;
            IList<Proveedores> suppliersList = new List<Proveedores>();
            var result = App.HttpTools.HttpGetList<Proveedores>(webApiUrl, ref suppliersList, "Error en la lectura de los proveedores");
            return suppliersList.ToList();
        }

        public void DeleteProductoProveedorById(int idProductoProveedor)
        {
            string webApiUrl = WebApiMethods.DeleteProductoProveedorById;
            var result = App.HttpTools.HttpDeleteAsync(webApiUrl, idProductoProveedor, "Error al intentar eliminar el proveedor");
        }

        public void DeleteImpuestoProductoById(int idImpuestoProducto)
        {
            string webApiUrl = WebApiMethods.DeleteImpuestoProductoById;
            var result = App.HttpTools.HttpDeleteAsync(webApiUrl, idImpuestoProducto, "Error al intentar eliminar el impuesto del producto");
        }

        public void DeletePluProductoById(int idPlu)
        {
            string webApiUrl = WebApiMethods.DeletePluProductoById;
            var result = App.HttpTools.HttpDeleteAsync(webApiUrl, idPlu, "Error al intentar eliminar el PLU de producto");
        }

        public List<Productos> GetAllProductos()
        {
            string webApiUrl = WebApiMethods.GetAllProducts;
            IList<Productos> productsListResult = new List<Productos>();
            var result = App.HttpTools.HttpGetList<Productos>(webApiUrl, ref productsListResult, "Error al leer el listado de productos");
            return productsListResult.ToList();
        }
    }
}
