using Pos.Business.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.BAL.Interface.Domain
{
    public interface IProveedoresRepository : IGenericRepository<Proveedores>
    {
        IEnumerable<Proveedores> GetSuppliersByName(string name);
        IEnumerable<object> GetSuppliersWithProducts();
        object GetSupplierWithProductsById(int supplierId);
    }
}
