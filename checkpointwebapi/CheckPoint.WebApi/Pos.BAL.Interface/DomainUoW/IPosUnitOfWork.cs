using Pos.BAL.Interface.Domain;
using System;

namespace Pos.BAL.Interface.DomainUoW
{
    public interface IPosUnitOfWork : IDisposable
    {
        IAlmacenesRepository AlmacenesRepository { get; }
        ICatalogoSatRepository CatalogoSatRepository { get; }
        IComprasRepository ComprasRepository { get; }
        ICortesRepository CortesRepository { get; }
        IDepartamentosRepository DepartamentosRepository { get; }
        IImpuestoProductosRepository ImpuestoProductosRepository { get; }
        IImpuestosRepository ImpuestosRepository { get; }
        IMarcaRepository MarcaRepository { get; }
        IOrdenesRepository OrdenesRepository { get; }
        IPLUProductoRepository PLUProductoRepository { get; }
        IPantallasRepository PantallasRepository { get; }
        IPantallasUsuarioRepository PantallasUsuarioRepository { get; }
        IProductoAlmacenRepository ProductoAlmacenRepository { get; }
        IProductosCompraRepository ProductosCompraRepository { get; }
        IProductosOrdenRepository ProductosOrdenRepository { get; }
        IProductosPromocionRepository ProductosPromocionRepository { get; }
        IProductosProveedorRepository ProductosProveedorRepository { get; }
        IProductosRepository ProductosRepository { get; }
        IProductosVentaRepository ProductosVentaRepository { get; }
        IPromocionesRepository PromocionesRepository { get; }
        IProveedoresRepository ProveedoresRepository { get; }
        IRetirosRepository RetirosRepository { get; }
        ITipoPagoRepository TipoPagoRepository { get; }
        ITipoUsuarioRepository TipoUsuarioRepository { get; }
        ITurnosRepository TurnosRepository { get; }
        IUnidadesRepository UnidadesRepository { get; }
        IUnidadSatRepository UnidadSatRepository  { get; }
        IUsuariosRepository UsuariosRepository { get; }
        IVentaImpuestosRepository VentaImpuestosRepository { get; } 
        IVentaPagosRepository VentaPagosRepository { get; }
        IVentasRepository VentasRepository { get; }
        int Save();
    }
}
