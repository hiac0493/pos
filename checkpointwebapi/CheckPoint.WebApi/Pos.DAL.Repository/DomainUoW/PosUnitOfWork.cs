using Pos.BAL.Interface.Domain;
using Pos.BAL.Interface.DomainUoW;
using Pos.DAL.Repository.Domain;
using Pos.EntityFramework.Edbm;

namespace Pos.DAL.Repository.DomainUoW
{
    public class PosUnitOfWork : IPosUnitOfWork
    {
        private readonly PosDbContext _context;

        public PosUnitOfWork(PosDbContext context)
        {
            _context = context;
            AlmacenesRepository = new AlmacenesRepository(_context);
            CatalogoSatRepository = new CatalogoSatRepository(_context);
            ComprasRepository = new ComprasRepository(_context);
            CortesRepository = new CortesRepository(_context);
            DepartamentosRepository = new DepartamentosRepository(_context);
            ImpuestoProductosRepository = new ImpuestoProductosRepository(_context);
            ImpuestosRepository = new ImpuestosRepository(_context);
            MarcaRepository = new MarcaRepository(_context);
            OrdenesRepository = new OrdenesRepository(_context);
            PantallasRepository = new PantallasRepository(_context);
            PantallasUsuarioRepository = new PantallasUsuarioRepository(_context);
            PLUProductoRepository = new PLUProductosRepository(_context);
            ProductoAlmacenRepository = new ProductoAlmacenRepository(_context);
            ProductosCompraRepository = new ProductosCompraRepository(_context);
            ProductosOrdenRepository = new ProductosOrdenRepository(_context);
            ProductosPromocionRepository = new ProductosPromocionRepository(_context);
            ProductosProveedorRepository = new ProductosProveedorRepository(_context);
            ProductosRepository = new ProductosRepository(_context);
            ProductosVentaRepository = new ProductosVentaRepository(_context);
            PromocionesRepository = new PromocionesRepository(_context);
            ProveedoresRepository = new ProveedoresRepository(_context);
            RetirosRepository = new RetirosRepository(_context);
            TipoPagoRepository = new TipoPagoRepository(_context);
            TurnosRepository = new TurnosRepository(_context);
            UnidadesRepository = new UnidadesRepository(_context);
            UnidadSatRepository = new UnidadSatRepository(_context);
            UsuariosRepository = new UsuariosRepository(_context);
            VentaImpuestosRepository = new VentaImpuestosRepository(_context);
            VentaPagosRepository = new VentaPagosRepository(_context);
            VentasRepository = new VentasRepository(_context);
        }
        public IAlmacenesRepository AlmacenesRepository { get; set; }
        public ICatalogoSatRepository CatalogoSatRepository { get; private set; }
        public IComprasRepository ComprasRepository { get; set; }
        public ICortesRepository CortesRepository { get; set; }
        public IDepartamentosRepository DepartamentosRepository { get; private set; }
        public IImpuestoProductosRepository ImpuestoProductosRepository { get; private set; }
        public IImpuestosRepository ImpuestosRepository { get; private set; }
        public IMarcaRepository MarcaRepository { get; private set; }
        public IOrdenesRepository OrdenesRepository { get; private set; }
        public IPantallasRepository PantallasRepository { get; private set; }
        public IPantallasUsuarioRepository PantallasUsuarioRepository { get; private set; }
        public IPLUProductoRepository PLUProductoRepository { get; private set; }
        public IProductoAlmacenRepository ProductoAlmacenRepository { get; private set; }
        public IProductosCompraRepository ProductosCompraRepository { get; private set; }
        public IProductosOrdenRepository ProductosOrdenRepository { get; private set; }
        public IProductosPromocionRepository ProductosPromocionRepository { get; private set; }
        public IProductosProveedorRepository ProductosProveedorRepository { get; private set; }
        public IProductosRepository ProductosRepository { get; private set; }
        public IProductosVentaRepository ProductosVentaRepository { get; private set; }
        public IPromocionesRepository PromocionesRepository { get; private set; }
        public IProveedoresRepository ProveedoresRepository { get; private set; }
        public IRetirosRepository RetirosRepository { get; private set; }
        public ITipoPagoRepository TipoPagoRepository { get; private set; }
        public ITurnosRepository TurnosRepository { get; private set; }
        public IUnidadesRepository UnidadesRepository { get; private set; }
        public IUnidadSatRepository UnidadSatRepository { get; private set; }
        public IUsuariosRepository UsuariosRepository { get; private set; }
        public IVentaImpuestosRepository VentaImpuestosRepository { get; private set; }
        public IVentaPagosRepository VentaPagosRepository { get; private set; }
        public IVentasRepository VentasRepository { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
