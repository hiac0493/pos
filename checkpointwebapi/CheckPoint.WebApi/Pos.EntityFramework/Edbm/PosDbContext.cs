using Microsoft.EntityFrameworkCore;
using Pos.Business.Model;

namespace Pos.EntityFramework.Edbm
{
    public class PosDbContext : DbContext
    {
        private string _connString;
        public PosDbContext(string connString)
        {
            _connString = connString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connString);
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Almacenes> Almacenes { get; set; }
        public DbSet<CatalogoSat> CatalogoSat { get; set; }
        public DbSet<Departamentos> Departamentos { get; set; }
        public DbSet<Compras> Compras { get; set; }
        public DbSet<ImpuestoProductos> ImpuestoProductos { get; set; }
        public DbSet<Impuestos> Impuestos { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Ordenes> Ordenes { get; set; }
        public DbSet<Pantallas> Pantallas { get; set; }
        public DbSet<PantallasUsuario> PantallasUsuario { get; set; }
        public DbSet<PLUProductos> PLUProductos { get; set; }
        public DbSet<ProductoAlmacen> ProductoAlmacen { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<ProductosCompra> ProductosCompra { get; set; }
        public DbSet<ProductosProveedor> ProductosProveedor { get; set; }
        public DbSet<ProductosVenta> ProductosVenta { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<Retiro> Retiro { get; set; }
        public DbSet<TipoPago> TipoPago { get; set; }
        public DbSet<Unidades> Unidades { get; set; }
        public DbSet<UnidadSat> UnidadSat { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<VentaImpuestos> VentaImpuestos { get; set; }
        public DbSet<VentaLote> VentaLote { get; set; }
        public DbSet<VentaPagos> VentaPagos { get; set; }
        public DbSet<Ventas> Ventas { get; set; }
        public DbSet<Cajas> Cajas { get; set; }
        public DbSet<Turnos> Turnos { get; set; }
        public DbSet<TipoUsuario> TipoUsuarios { get; set; }
        public DbSet<CortePagos> CortePagos { get; set; }
        public DbSet<Cortes> Cortes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ------------- Almacenes ------------------------------

            //Fields
            modelBuilder.Entity<Almacenes>().ToTable("Almacenes");
            modelBuilder.Entity<Almacenes>().HasKey(c => new { c.idAlmacen });
            modelBuilder.Entity<Almacenes>().Property(t => t.idAlmacen).HasColumnName("idAlmacen");
            modelBuilder.Entity<Almacenes>().Property(t => t.Nombre).HasColumnName("Nombre");
            modelBuilder.Entity<Almacenes>().Property(t => t.Observaciones).HasColumnName("Observaciones");
            modelBuilder.Entity<Almacenes>().Property(t => t.Principal).HasColumnName("Principal");
            modelBuilder.Entity<Almacenes>().Property(t => t.Activo).HasColumnName("Activo");
            

            // Navigation
            modelBuilder.Entity<Almacenes>().HasMany(t => t.Productos).WithOne(p => p.Almacen);
            modelBuilder.Entity<Almacenes>().HasMany(t => t.Compras).WithOne(a => a.Almacen);
            

            // ------------- Cajas ------------------------------

            // Fields
            modelBuilder.Entity<Cajas>().ToTable("Cajas");
            modelBuilder.Entity<Cajas>().HasKey(c => new { c.IdCaja });
            modelBuilder.Entity<Cajas>().Property(t => t.IdCaja).HasColumnName("IdCaja");
            modelBuilder.Entity<Cajas>().Property(t => t.Nombre).HasColumnName("Nombre");
            modelBuilder.Entity<Cajas>().Property(t => t.Estatus).HasColumnName("Estatus");

            // Navigation
            modelBuilder.Entity<Cajas>().HasMany(t => t.Cortes).WithOne(t => t.Caja);

            // -------------CatalogoSat ------------------------------
            //Fields
            modelBuilder.Entity<CatalogoSat>().ToTable("CatalogoSat");
            modelBuilder.Entity<CatalogoSat>().HasKey(c => new { c.idCatalogoSat });
            modelBuilder.Entity<CatalogoSat>().Property(t => t.idCatalogoSat).HasColumnName("idCatalogoSat");
            modelBuilder.Entity<CatalogoSat>().Property(t => t.Clave).HasColumnName("Clave");
            modelBuilder.Entity<CatalogoSat>().Property(t => t.Descripcion).HasColumnName("Descripcion");
            modelBuilder.Entity<CatalogoSat>().Property(t => t.Activo).HasColumnName("Activo");

            // Navigation
            modelBuilder.Entity<CatalogoSat>().HasMany(t => t.Productos).WithOne(t => t.CatalogoSat);

            // ------------- Compras ---------------------------------

            // Fields
            modelBuilder.Entity<Compras>().ToTable("Compras");
            modelBuilder.Entity<Compras>().HasKey(c => new { c.FolioCompra });
            modelBuilder.Entity<Compras>().Property(t => t.FolioCompra).HasColumnName("FolioCompra");
            modelBuilder.Entity<Compras>().Property(t => t.Subtotal).HasColumnName("Subtotal");
            modelBuilder.Entity<Compras>().Property(t => t.Impuestos).HasColumnName("Impuestos");
            modelBuilder.Entity<Compras>().Property(t => t.Total).HasColumnName("Total");
            modelBuilder.Entity<Compras>().Property(t => t.idUsuario).HasColumnName("idUsuario");
            modelBuilder.Entity<Compras>().Property(t => t.Estatus).HasColumnName("Estatus");
            modelBuilder.Entity<Compras>().Property(t => t.idUsuarioCancela).HasColumnName("idUsuarioCancela");
            modelBuilder.Entity<Compras>().Property(t => t.Fecha).HasColumnName("Fecha");
            modelBuilder.Entity<Compras>().Property(t => t.idAlmacen).HasColumnName("idAlmacen");   

            // Navigation
            modelBuilder.Entity<Compras>().HasOne(t => t.Usuario).WithMany(cu => cu.ComprasUsuario).HasForeignKey(t => t.idUsuario);
            modelBuilder.Entity<Compras>().HasOne(t => t.UsuarioCancela).WithMany(cc => cc.ComprasCanceladas).HasForeignKey(t => t.idUsuarioCancela);
            modelBuilder.Entity<Compras>().HasMany(t => t.ProductosCompra).WithOne(t => t.Compra);
            modelBuilder.Entity<Compras>().HasOne(t => t.Almacen).WithMany(al => al.Compras).HasForeignKey(t => t.idAlmacen);
            

            // ------------- CortePagos ------------------------------

            // Fields
            modelBuilder.Entity<CortePagos>().ToTable("CortePagos");
            modelBuilder.Entity<CortePagos>().HasKey(c => new { c.IdCortePago });
            modelBuilder.Entity<CortePagos>().Property(t => t.IdCortePago).HasColumnName("IdCortePago");
            modelBuilder.Entity<CortePagos>().Property(t => t.IdCorte).HasColumnName("IdCorte");
            modelBuilder.Entity<CortePagos>().Property(t => t.IdTipoPago).HasColumnName("IdTipoPago");
            modelBuilder.Entity<CortePagos>().Property(t => t.Cantidad).HasColumnName("Cantidad");

            // Navigation
            modelBuilder.Entity<CortePagos>().HasOne(t => t.Corte).WithMany(p => p.Pagos).HasForeignKey(t => t.IdCorte);
            modelBuilder.Entity<CortePagos>().HasOne(t => t.TipoPago).WithMany(v => v.Cortes).HasForeignKey(t => t.IdTipoPago);

            // ------------- Cortes ----------------------------------

            // Fields
            modelBuilder.Entity<Cortes>().ToTable("Cortes");
            modelBuilder.Entity<Cortes>().HasKey(c => new { c.IdCorte });
            modelBuilder.Entity<Cortes>().Property(t => t.IdCorte).HasColumnName("IdCorte");
            modelBuilder.Entity<Cortes>().Property(t => t.IdTurno).HasColumnName("IdTurno");
            modelBuilder.Entity<Cortes>().Property(t => t.IdCaja).HasColumnName("IdCaja");
            modelBuilder.Entity<Cortes>().Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            modelBuilder.Entity<Cortes>().Property(t => t.FolioVentaInicio).HasColumnName("FolioVentaInicio");
            modelBuilder.Entity<Cortes>().Property(t => t.FolioVentaFin).HasColumnName("FolioVentaFin");
            modelBuilder.Entity<Cortes>().Property(t => t.FondoCaja).HasColumnName("FondoCaja");
            modelBuilder.Entity<Cortes>().Property(t => t.FechaInicio).HasColumnName("FechaInicio");
            modelBuilder.Entity<Cortes>().Property(t => t.FechaFinal).HasColumnName("FechaFinal");
            modelBuilder.Entity<Cortes>().Property(t => t.TotalVenta).HasColumnName("TotalVenta");
            modelBuilder.Entity<Cortes>().Property(t => t.TotalUtilidad).HasColumnName("TotalUtilidad");


            // Navigation
            modelBuilder.Entity<Cortes>().HasOne(t => t.Turno).WithMany(tu => tu.Cortes).HasForeignKey(t => t.IdTurno);
            modelBuilder.Entity<Cortes>().HasOne(t => t.Caja).WithMany(ca => ca.Cortes).HasForeignKey(t => t.IdCaja);
            modelBuilder.Entity<Cortes>().HasOne(t => t.Usuario).WithMany(cu => cu.CortesUsuario).HasForeignKey(t => t.IdUsuario);
            modelBuilder.Entity<Cortes>().HasMany(t => t.Pagos).WithOne(t => t.Corte);
            modelBuilder.Entity<Cortes>().HasMany(t => t.Retiros).WithOne(t => t.Corte);

            // ------------- Departamentos ----------------------------

            // Fields
            modelBuilder.Entity<Departamentos>().ToTable("Departamentos");
            modelBuilder.Entity<Departamentos>().HasKey(c => new { c.idDepartamento });
            modelBuilder.Entity<Departamentos>().Property(t => t.idDepartamento).HasColumnName("idDepartamento");
            modelBuilder.Entity<Departamentos>().Property(t => t.Descripcion).HasColumnName("Descripcion");
            modelBuilder.Entity<Departamentos>().Property(t => t.Estatus).HasColumnName("Estatus");

            // Navigation
            modelBuilder.Entity<Departamentos>().HasMany(t => t.Productos).WithOne(pd => pd.Departamento).HasForeignKey(t => t.idDepartamento);
            modelBuilder.Entity<Departamentos>().HasMany(t => t.SubDepartamentos).WithOne(pd => pd.Departamento).HasForeignKey(t => t.IdSubDepartamento);
            modelBuilder.Entity<Departamentos>().HasMany(t => t.Promociones).WithOne(pr => pr.Departamento).HasForeignKey(t => t.idDepartamento);




            // ------------- ImpuestoProductos -----------------------

            // Fields
            modelBuilder.Entity<ImpuestoProductos>().ToTable("ImpuestoProductos");
            modelBuilder.Entity<ImpuestoProductos>().HasKey(c => new { c.idImpuestoProducto });
            modelBuilder.Entity<ImpuestoProductos>().Property(t => t.idImpuestoProducto).HasColumnName("idImpuestoProducto");
            modelBuilder.Entity<ImpuestoProductos>().Property(t => t.idImpuesto).HasColumnName("idImpuesto");
            modelBuilder.Entity<ImpuestoProductos>().Property(t => t.idProducto).HasColumnName("idProducto");
            modelBuilder.Entity<ImpuestoProductos>().Property(t => t.Desglosado).HasColumnName("Desglosado");

            // Navigation
            modelBuilder.Entity<ImpuestoProductos>().HasOne(t => t.Impuesto).WithMany(p => p.Productos).HasForeignKey(t => t.idImpuesto);
            modelBuilder.Entity<ImpuestoProductos>().HasOne(t => t.Producto).WithMany(i => i.Impuestos).HasForeignKey(t => t.idProducto);

            // ------------- Impuestos -------------------------------

            // Fields
            modelBuilder.Entity<Impuestos>().ToTable("Impuestos");
            modelBuilder.Entity<Impuestos>().HasKey(c => new { c.idImpuesto });
            modelBuilder.Entity<Impuestos>().Property(t => t.idImpuesto).HasColumnName("idImpuesto");
            modelBuilder.Entity<Impuestos>().Property(t => t.Descripcion).HasColumnName("Descripcion");
            modelBuilder.Entity<Impuestos>().Property(t => t.Porcentaje).HasColumnName("Porcentaje");
            modelBuilder.Entity<Impuestos>().Property(t => t.Estatus).HasColumnName("Estatus");

            // Navigation
            modelBuilder.Entity<Impuestos>().HasMany(t => t.Productos).WithOne(i => i.Impuesto).HasForeignKey(t => t.idImpuesto);
            modelBuilder.Entity<Impuestos>().HasMany(t => t.Venta).WithOne(i => i.Impuesto).HasForeignKey(t => t.IdImpuesto);


            // ------------- Marca -----------------------------------

            // Fields 
            modelBuilder.Entity<Marca>().ToTable("Marca");
            modelBuilder.Entity<Marca>().HasKey(c => new { c.idMarca });
            modelBuilder.Entity<Marca>().Property(t => t.idMarca).HasColumnName("idMarca");
            modelBuilder.Entity<Marca>().Property(t => t.Descripcion).HasColumnName("Descripcion");
            modelBuilder.Entity<Marca>().Property(t => t.Activo).HasColumnName("Activo");

            // Navigation
            modelBuilder.Entity<Marca>().HasMany(t => t.Productos).WithOne(p => p.Marca).HasForeignKey(t => t.idMarca);
            modelBuilder.Entity<Marca>().HasMany(t => t.Promociones).WithOne(pr => pr.Marca).HasForeignKey(pr => pr.idMarca);

            // ------------- Ordenes ---------------------------------

            // Fields
            modelBuilder.Entity<Ordenes>().ToTable("Ordenes");
            modelBuilder.Entity<Ordenes>().HasKey(c => new { c.idOrden });
            modelBuilder.Entity<Ordenes>().Property(t => t.idOrden).HasColumnName("idOrden");
            modelBuilder.Entity<Ordenes>().Property(t => t.Estatus).HasColumnName("Estatus");
            modelBuilder.Entity<Ordenes>().Property(t => t.idProveedor).HasColumnName("idProveedor");
            modelBuilder.Entity<Ordenes>().Property(t => t.NumeroArticulos).HasColumnName("NumeroArticulos");
            modelBuilder.Entity<Ordenes>().Property(t => t.Total).HasColumnName("Total");
            modelBuilder.Entity<Ordenes>().Property(t => t.idCompra).HasColumnName("idCompra");
            modelBuilder.Entity<Ordenes>().Property(t => t.FechaCreacion).HasColumnName("FechaCreacion");
            modelBuilder.Entity<Ordenes>().Property(t => t.UltimaModificacion).HasColumnName("UltimaModificacion");
            modelBuilder.Entity<Ordenes>().Property(t => t.UsuarioAutoriza).HasColumnName("UsuarioAutoriza");
            modelBuilder.Entity<Ordenes>().Property(t => t.FechaAutorizacion).HasColumnName("FechaAutorizacion");

            // Navigation
            modelBuilder.Entity<Ordenes>().HasOne(t => t.Usuario).WithMany(oa => oa.OrdenesAutorizadas).HasForeignKey(t => t.UsuarioAutoriza);
            modelBuilder.Entity<Ordenes>().HasOne(t => t.Proveedor).WithMany(op => op.Ordenes).HasForeignKey(t => t.idProveedor);
            modelBuilder.Entity<Ordenes>().HasMany(t => t.ProductosOrden).WithOne(t => t.Orden);
            modelBuilder.Entity<Ordenes>().HasOne(t => t.Compra).WithMany(oc => oc.OrdenesCompra).HasForeignKey(t => t.idCompra);
            
            // ------------- Pantallas ---------------------------------

            // Fields
            modelBuilder.Entity<Pantallas>().ToTable("Pantallas");
            modelBuilder.Entity<Pantallas>().HasKey(c => new { c.idPantalla });
            modelBuilder.Entity<Pantallas>().Property(t => t.idPantalla).HasColumnName("idPantalla");
            modelBuilder.Entity<Pantallas>().Property(t => t.NombrePantalla).HasColumnName("NombrePantalla");
            modelBuilder.Entity<Pantallas>().Property(t => t.TextoPanel).HasColumnName("TextoPanel");
            modelBuilder.Entity<Pantallas>().Property(t => t.Url).HasColumnName("Url");
            modelBuilder.Entity<Pantallas>().Property(t => t.Icono).HasColumnName("Icono");
            modelBuilder.Entity<Pantallas>().Property(t => t.Nivel).HasColumnName("Nivel"); 
            modelBuilder.Entity<Pantallas>().Property(t => t.Activo).HasColumnName("Activo");
            modelBuilder.Entity<Pantallas>().Property(t => t.SubPantalla).HasColumnName("SubPantalla");

            //Navigation
            modelBuilder.Entity<Pantallas>().HasMany(t => t.PantallasUsuarios).WithOne(t => t.Pantalla);

            // ------------- PantallasUsuario ---------------------------------
            // Fields
            modelBuilder.Entity<PantallasUsuario>().ToTable("PantallasUsuario");
            modelBuilder.Entity<PantallasUsuario>().HasKey(c => new { c.idPantallasUsuario });
            modelBuilder.Entity<PantallasUsuario>().Property(t => t.idPantallasUsuario).HasColumnName("idPantallasUsuario");
            modelBuilder.Entity<PantallasUsuario>().Property(t => t.idUsuario).HasColumnName("idUsuario");
            modelBuilder.Entity<PantallasUsuario>().Property(t => t.idPantalla).HasColumnName("idPantalla");

            // Navigation
            modelBuilder.Entity<PantallasUsuario>().HasOne(t => t.Usuario).WithMany(p => p.PantallasUsuario).HasForeignKey(t => t.idUsuario);
            modelBuilder.Entity<PantallasUsuario>().HasOne(t => t.Pantalla).WithMany(t => t.PantallasUsuarios).HasForeignKey(t => t.idPantalla);
            // ------------- PLUProductos ----------------------------

            // Fields
            modelBuilder.Entity<PLUProductos>().ToTable("PLUProductos");
            modelBuilder.Entity<PLUProductos>().HasKey(c => new { c.idPLU });
            modelBuilder.Entity<PLUProductos>().Property(t => t.idPLU).HasColumnName("idPLU");
            modelBuilder.Entity<PLUProductos>().Property(t => t.idProducto).HasColumnName("idProducto");
            modelBuilder.Entity<PLUProductos>().Property(t => t.PLU).HasColumnName("PLU");

            // Navigation
            modelBuilder.Entity<PLUProductos>().HasOne(t => t.Producto).WithMany(p => p.PLUs).HasForeignKey(t => t.idProducto);

            // ------------- ProductoAlmacen -------------------------

            // Fields
            modelBuilder.Entity<ProductoAlmacen>().ToTable("ProductoAlmacen");
            modelBuilder.Entity<ProductoAlmacen>().HasKey(c => new { c.idProductoAlmacen });
            modelBuilder.Entity<ProductoAlmacen>().Property(t => t.idProductoAlmacen).HasColumnName("idProductoAlmacen");
            modelBuilder.Entity<ProductoAlmacen>().Property(t => t.idAlmacen).HasColumnName("idAlmacen");
            modelBuilder.Entity<ProductoAlmacen>().Property(t => t.idProducto).HasColumnName("idProducto");
            modelBuilder.Entity<ProductoAlmacen>().Property(t => t.Existencia).HasColumnName("Existencia");

            // Navigation
            modelBuilder.Entity<ProductoAlmacen>().HasOne(t => t.Producto).WithMany(p => p.Almacenes).HasForeignKey(t => t.idProducto);
            modelBuilder.Entity<ProductoAlmacen>().HasOne(t => t.Almacen).WithMany(p => p.Productos).HasForeignKey(t => t.idAlmacen);

            // ------------- Productos -------------------------------

            // Fields
            modelBuilder.Entity<Productos>().ToTable("Productos");
            modelBuilder.Entity<Productos>().HasKey(c => new { c.idProducto });
            modelBuilder.Entity<Productos>().Property(t => t.idProducto).HasColumnName("idProducto");
            modelBuilder.Entity<Productos>().Property(t => t.NombreProducto).HasColumnName("NombreProducto");
            modelBuilder.Entity<Productos>().Property(t => t.PrecioCosto).HasColumnName("PrecioCosto");
            modelBuilder.Entity<Productos>().Property(t => t.PrecioVenta).HasColumnName("PrecioVenta");
            modelBuilder.Entity<Productos>().Property(t => t.PrecioVentaSinImpuestos).HasColumnName("PrecioVentaSinImpuestos");
            modelBuilder.Entity<Productos>().Property(t => t.idMarca).HasColumnName("idMarca");
            modelBuilder.Entity<Productos>().Property(t => t.idDepartamento).HasColumnName("idDepartamento");
            modelBuilder.Entity<Productos>().Property(t => t.Ganancia).HasColumnName("Ganancia");
            modelBuilder.Entity<Productos>().Property(t => t.idUnidad).HasColumnName("idUnidad");
            modelBuilder.Entity<Productos>().Property(t => t.Minimo).HasColumnName("Minimo");
            modelBuilder.Entity<Productos>().Property(t => t.Maximo).HasColumnName("Maximo");
            modelBuilder.Entity<Productos>().Property(t => t.MinimoCompra).HasColumnName("MinimoCompra");
            modelBuilder.Entity<Productos>().Property(t => t.idCatalogoSat).HasColumnName("idCatalogoSat");
            modelBuilder.Entity<Productos>().Property(t => t.idUnidadSat).HasColumnName("idUnidadSat");
            modelBuilder.Entity<Productos>().Property(t => t.ImagenId).HasColumnName("ImagenId");
            modelBuilder.Entity<Productos>().Property(t => t.idSubDepartamento).HasColumnName("idSubDepartamento");

            // Navigation
            modelBuilder.Entity<Productos>().HasOne(t => t.Marca).WithMany(p => p.Productos).HasForeignKey(t => t.idMarca);
            modelBuilder.Entity<Productos>().HasOne(t => t.Departamento).WithMany(p => p.Productos).HasForeignKey(t => t.idDepartamento);
            modelBuilder.Entity<Productos>().HasOne(t => t.SubDepartamento).WithMany(p => p.Productos).HasForeignKey(t => t.idSubDepartamento);
            modelBuilder.Entity<Productos>().HasMany(t => t.Impuestos).WithOne(t => t.Producto);
            modelBuilder.Entity<Productos>().HasMany(t => t.PLUs).WithOne(t => t.Producto);
            modelBuilder.Entity<Productos>().HasMany(t => t.Proveedores).WithOne(t => t.Producto);
            modelBuilder.Entity<Productos>().HasMany(t => t.Ventas).WithOne(t => t.Productos);
            modelBuilder.Entity<Productos>().HasMany(t => t.Compras).WithOne(t => t.Producto);
            modelBuilder.Entity<Productos>().HasOne(t => t.CatalogoSat).WithMany(cs => cs.Productos).HasForeignKey(t => t.idCatalogoSat);
            modelBuilder.Entity<Productos>().HasOne(t => t.UnidadSat).WithMany(cs => cs.Productos).HasForeignKey(t => t.idUnidadSat);
            modelBuilder.Entity<Productos>().HasMany(t => t.Lotes).WithOne(t => t.producto);
            modelBuilder.Entity<Productos>().HasMany(t => t.Almacenes).WithOne(p => p.Producto);
            modelBuilder.Entity<Productos>().HasMany(t => t.Promociones).WithOne(p => p.Producto);
            modelBuilder.Entity<Productos>().HasOne(t => t.Unidad).WithMany(t => t.Productos).HasForeignKey(t => t.idUnidad);

            // ------------- ProductosCompra -------------------------

            // Fields
            modelBuilder.Entity<ProductosCompra>().ToTable("ProductosCompra");
            modelBuilder.Entity<ProductosCompra>().HasKey(c => new { c.idCompraProducto });
            modelBuilder.Entity<ProductosCompra>().Property(t => t.idCompraProducto).HasColumnName("idCompraProducto");
            modelBuilder.Entity<ProductosCompra>().Property(t => t.idCompra).HasColumnName("idCompra");
            modelBuilder.Entity<ProductosCompra>().Property(t => t.idProducto).HasColumnName("idProducto");
            modelBuilder.Entity<ProductosCompra>().Property(t => t.Cantidad).HasColumnName("Cantidad");
            modelBuilder.Entity<ProductosCompra>().Property(t => t.Monto).HasColumnName("Monto");
            modelBuilder.Entity<ProductosCompra>().Property(t => t.Restante).HasColumnName("Restante");
            modelBuilder.Entity<ProductosCompra>().Property(t => t.Costo).HasColumnName("Costo");

            // Navigation
            modelBuilder.Entity<ProductosCompra>().HasOne(t => t.Compra).WithMany(p => p.ProductosCompra).HasForeignKey(t => t.idCompra);
            modelBuilder.Entity<ProductosCompra>().HasOne(t => t.Producto).WithMany(c => c.Compras).HasForeignKey(t => t.idProducto);
            modelBuilder.Entity<ProductosCompra>().HasMany(t => t.Lotes).WithOne(t => t.compra);

            // ------------- ProductosOrden --------------------------

            // Fields
            modelBuilder.Entity<ProductosOrden>().ToTable("ProductosOrden");
            modelBuilder.Entity<ProductosOrden>().HasKey(c => new { c.idProductoOrden });
            modelBuilder.Entity<ProductosOrden>().Property(t => t.idProductoOrden).HasColumnName("idProductoOrden");
            modelBuilder.Entity<ProductosOrden>().Property(t => t.idOrden).HasColumnName("idOrden");
            modelBuilder.Entity<ProductosOrden>().Property(t => t.idProducto).HasColumnName("idProducto");
            modelBuilder.Entity<ProductosOrden>().Property(t => t.Cantidad).HasColumnName("Cantidad");
            modelBuilder.Entity<ProductosOrden>().Property(t => t.Monto).HasColumnName("Monto");

            // Navigation
            modelBuilder.Entity<ProductosOrden>().HasOne(t => t.Orden).WithMany(p => p.ProductosOrden).HasForeignKey(t => t.idOrden);
            modelBuilder.Entity<ProductosOrden>().HasOne(t => t.Producto).WithMany(po => po.Ordenes).HasForeignKey(t => t.idProducto);

            // ------------- ProductosPromocion ----------------------

            // Fields
            modelBuilder.Entity<ProductosPromocion>().ToTable("ProductosPromocion");
            modelBuilder.Entity<ProductosPromocion>().HasKey(c => new { c.idProductoPromocion });
            modelBuilder.Entity<ProductosPromocion>().Property(t => t.idProductoPromocion).HasColumnName("idProductoPromocion");
            modelBuilder.Entity<ProductosPromocion>().Property(t => t.idPromocion).HasColumnName("idPromocion");
            modelBuilder.Entity<ProductosPromocion>().Property(t => t.idProducto).HasColumnName("idProducto");
            modelBuilder.Entity<ProductosPromocion>().Property(t => t.Cantidad).HasColumnName("Cantidad");

            // Navigation
            modelBuilder.Entity<ProductosPromocion>().HasOne(t => t.Promocion).WithMany(pr => pr.Productos).HasForeignKey(t => t.idPromocion);
            modelBuilder.Entity<ProductosPromocion>().HasOne(t => t.Producto).WithMany(pr => pr.Promociones).HasForeignKey(t => t.idProducto);

            // ------------- ProductosProveedor ----------------------

            // Fields 
            modelBuilder.Entity<ProductosProveedor>().ToTable("ProductosProveedor");
            modelBuilder.Entity<ProductosProveedor>().HasKey(c => new { c.idProductoProveedor });
            modelBuilder.Entity<ProductosProveedor>().Property(t => t.idProductoProveedor).HasColumnName("idProductoProveedor");
            modelBuilder.Entity<ProductosProveedor>().Property(t => t.idProveedor).HasColumnName("idProveedor");
            modelBuilder.Entity<ProductosProveedor>().Property(t => t.idProducto).HasColumnName("idProducto");
            modelBuilder.Entity<ProductosProveedor>().Property(t => t.ultimoCosto).HasColumnName("ultimoCosto");

            // Navigation
            modelBuilder.Entity<ProductosProveedor>().HasOne(t => t.Proveedor).WithMany(p => p.Productos).HasForeignKey(t => t.idProveedor);
            modelBuilder.Entity<ProductosProveedor>().HasOne(t => t.Producto).WithMany(p => p.Proveedores).HasForeignKey(t => t.idProducto);

            // ------------- ProductosVenta --------------------------

            // Fields 
            modelBuilder.Entity<ProductosVenta>().ToTable("ProductosVenta");
            modelBuilder.Entity<ProductosVenta>().HasKey(c => new { c.idVentaProducto });
            modelBuilder.Entity<ProductosVenta>().Property(t => t.idVentaProducto).HasColumnName("idVentaProducto");
            modelBuilder.Entity<ProductosVenta>().Property(t => t.idVenta).HasColumnName("idVenta");
            modelBuilder.Entity<ProductosVenta>().Property(t => t.idProducto).HasColumnName("idProducto");
            modelBuilder.Entity<ProductosVenta>().Property(t => t.Cantidad).HasColumnName("Cantidad");
            modelBuilder.Entity<ProductosVenta>().Property(t => t.Monto).HasColumnName("Monto");
            modelBuilder.Entity<ProductosVenta>().Property(t => t.Estatus).HasColumnName("Estatus");

            // Navigation
            modelBuilder.Entity<ProductosVenta>().HasOne(t => t.Venta).WithMany(p => p.Productos).HasForeignKey(t => t.idVenta);
            modelBuilder.Entity<ProductosVenta>().HasOne(t => t.Productos).WithMany(v => v.Ventas).HasForeignKey(t => t.idProducto);

            // ------------- Promociones -----------------------------

            // Fields
            modelBuilder.Entity<Promociones>().ToTable("Promociones");
            modelBuilder.Entity<Promociones>().HasKey(c => new { c.idPromocion });
            modelBuilder.Entity<Promociones>().Property(t => t.idPromocion).HasColumnName("idPromocion");
            modelBuilder.Entity<Promociones>().Property(t => t.NombrePromocion).HasColumnName("NombrePromocion");
            modelBuilder.Entity<Promociones>().Property(t => t.Inicio).HasColumnName("Inicio");
            modelBuilder.Entity<Promociones>().Property(t => t.Fin).HasColumnName("Fin");
            modelBuilder.Entity<Promociones>().Property(t => t.Monto).HasColumnName("Monto");
            modelBuilder.Entity<Promociones>().Property(t => t.Porcentaje).HasColumnName("Porcentaje");
            modelBuilder.Entity<Promociones>().Property(t => t.idDepartamento).HasColumnName("idDepartamento");
            modelBuilder.Entity<Promociones>().Property(t => t.idMarca).HasColumnName("idMarca");
            modelBuilder.Entity<Promociones>().Property(t => t.DiasPromocion).HasColumnName("DiasPromocion");
            modelBuilder.Entity<Promociones>().Property(t => t.Estatus).HasColumnName("Estatus");

            // Navigation
            modelBuilder.Entity<Promociones>().HasOne(t => t.Marca).WithMany(m => m.Promociones).HasForeignKey(t => t.idMarca);
            modelBuilder.Entity<Promociones>().HasOne(t => t.Departamento).WithMany(d => d.Promociones).HasForeignKey(t => t.idDepartamento);
            modelBuilder.Entity<Promociones>().HasMany(t => t.Productos).WithOne(t => t.Promocion);

            // ------------- Proveedores -----------------------------

            // Fields
            modelBuilder.Entity<Proveedores>().ToTable("Proveedores");
            modelBuilder.Entity<Proveedores>().HasKey(c => new { c.idProveedor });
            modelBuilder.Entity<Proveedores>().Property(t => t.idProveedor).HasColumnName("idProveedor");
            modelBuilder.Entity<Proveedores>().Property(t => t.Nombre).HasColumnName("Nombre");
            modelBuilder.Entity<Proveedores>().Property(t => t.Representante).HasColumnName("Representante");
            modelBuilder.Entity<Proveedores>().Property(t => t.Telefonos).HasColumnName("Telefonos");
            modelBuilder.Entity<Proveedores>().Property(t => t.Correos).HasColumnName("Correos");
            modelBuilder.Entity<Proveedores>().Property(t => t.PaginaWeb).HasColumnName("PaginaWeb");
            modelBuilder.Entity<Proveedores>().Property(t => t.Comentarios).HasColumnName("Comentarios");
            modelBuilder.Entity<Proveedores>().Property(t => t.Estatus).HasColumnName("Estatus");

            // Navigation
            modelBuilder.Entity<Proveedores>().HasMany(t => t.Productos).WithOne(t => t.Proveedor);
            modelBuilder.Entity<Proveedores>().HasMany(t => t.Ordenes).WithOne(t => t.Proveedor);


            // ------------- Retiros -----------------------------

            // Fields

            modelBuilder.Entity<Retiro>().ToTable("Retiros");
            modelBuilder.Entity<Retiro>().HasKey(c => new { c.IdRetiro });
            modelBuilder.Entity<Retiro>().Property(t => t.IdRetiro).HasColumnName("IdRetiro");
            modelBuilder.Entity<Retiro>().Property(t => t.IdCorte).HasColumnName("IdCorte");
            modelBuilder.Entity<Retiro>().Property(t => t.Comentarios).HasColumnName("Comentarios");
            modelBuilder.Entity<Retiro>().Property(t => t.Hora).HasColumnName("Hora");
            modelBuilder.Entity<Retiro>().Property(t => t.Cantidad).HasColumnName("Cantidad");
            modelBuilder.Entity<Retiro>().Property(t => t.Tipo).HasColumnName("Tipo");
            modelBuilder.Entity<Retiro>().Property(t => t.Estatus).HasColumnName("Estatus");
            
            // Navigation
            modelBuilder.Entity<Retiro>().HasOne(t => t.Corte).WithMany(r => r.Retiros).HasForeignKey(t => t.IdCorte);

            // ------------- TipoPago --------------------------------

            // Fields 
            modelBuilder.Entity<TipoPago>().ToTable("TipoPago");
            modelBuilder.Entity<TipoPago>().HasKey(c => new { c.idTipoPago });
            modelBuilder.Entity<TipoPago>().Property(t => t.idTipoPago).HasColumnName("idTipoPago");
            modelBuilder.Entity<TipoPago>().Property(t => t.Descripcion).HasColumnName("Descripcion");
            modelBuilder.Entity<TipoPago>().Property(t => t.Estatus).HasColumnName("Estatus");


            // Navigation
            modelBuilder.Entity<TipoPago>().HasMany(t => t.Ventas).WithOne(t => t.TipoPago);
            modelBuilder.Entity<TipoPago>().HasMany(t => t.Cortes).WithOne(t => t.TipoPago);

            // ------------- TipoUsuario --------------------------------

            // Fields 
            modelBuilder.Entity<TipoUsuario>().ToTable("TipoUsuario");
            modelBuilder.Entity<TipoUsuario>().HasKey(c => new { c.idTipoUsuario });
            modelBuilder.Entity<TipoUsuario>().Property(t => t.idTipoUsuario).HasColumnName("idTipoUsuario");
            modelBuilder.Entity<TipoUsuario>().Property(t => t.Descripcion).HasColumnName("Descripcion");
            modelBuilder.Entity<TipoUsuario>().Property(t => t.NivelUsuario).HasColumnName("NivelUsuario");

            // Navigation
            modelBuilder.Entity<TipoUsuario>().HasMany(t => t.Usuarios).WithOne(t => t.TipoUsuario);

            // ------------- Turnos ------------------------------

            // Fields
            modelBuilder.Entity<Turnos>().ToTable("Turnos");
            modelBuilder.Entity<Turnos>().HasKey(c => new { c.IdTurno });
            modelBuilder.Entity<Turnos>().Property(t => t.IdTurno).HasColumnName("IdTurno");
            modelBuilder.Entity<Turnos>().Property(t => t.Nombre).HasColumnName("Nombre");
            modelBuilder.Entity<Turnos>().Property(t => t.Estatus).HasColumnName("Estatus");
            modelBuilder.Entity<Turnos>().Property(t => t.HoraInicio).HasColumnName("HoraInicio");
            modelBuilder.Entity<Turnos>().Property(t => t.HoraFin).HasColumnName("HoraFin");

            // Navigation
            modelBuilder.Entity<Turnos>().HasMany(t => t.Cortes).WithOne(cor => cor.Turno).HasForeignKey(cor => cor.IdTurno);


            // ------------- Unidades --------------------------------
            //Fields
            modelBuilder.Entity<Unidades>().ToTable("Unidades");
            modelBuilder.Entity<Unidades>().HasKey(c => new { c.idUnidad });
            modelBuilder.Entity<Unidades>().Property(t => t.idUnidad).HasColumnName("idUnidad");
            modelBuilder.Entity<Unidades>().Property(t => t.Descripcion).HasColumnName("Descripcion");
            modelBuilder.Entity<Unidades>().Property(t => t.Activo).HasColumnName("Activo");

            //Navigation
            modelBuilder.Entity<Unidades>().HasMany(t => t.Productos).WithOne(x => x.Unidad).HasForeignKey(t => t.idUnidad);

            // ------------- Unidad SAT --------------------------------
            //Fields
            modelBuilder.Entity<UnidadSat>().ToTable("UnidadSat");
            modelBuilder.Entity<UnidadSat>().HasKey(c => new { c.idUnidadSat });
            modelBuilder.Entity<UnidadSat>().Property(t => t.ClaveUnidad).HasColumnName("idUnidadSat");
            modelBuilder.Entity<UnidadSat>().Property(t => t.ClaveUnidad).HasColumnName("ClaveUnidad");
            modelBuilder.Entity<UnidadSat>().Property(t => t.Descripcion).HasColumnName("Descripcion");
            modelBuilder.Entity<UnidadSat>().Property(t => t.Activo).HasColumnName("Activo");

            //Navigation
            modelBuilder.Entity<UnidadSat>().HasMany(t => t.Productos).WithOne(t => t.UnidadSat);

            // ------------- Usuarios --------------------------------

            // Fields
            modelBuilder.Entity<Usuarios>().ToTable("Usuarios");
            modelBuilder.Entity<Usuarios>().HasKey(c => new { c.idUsuario });
            modelBuilder.Entity<Usuarios>().Property(t => t.idUsuario).HasColumnName("idUsuario");
            modelBuilder.Entity<Usuarios>().Property(t => t.NombreUsuario).HasColumnName("NombreUsuario");
            modelBuilder.Entity<Usuarios>().Property(t => t.ApellidoMaterno).HasColumnName("ApellidoMaterno");
            modelBuilder.Entity<Usuarios>().Property(t => t.ApellidoPaterno).HasColumnName("ApellidoPaterno");
            modelBuilder.Entity<Usuarios>().Property(t => t.Nombres).HasColumnName("Nombres");
            modelBuilder.Entity<Usuarios>().Property(t => t.Contraseña).HasColumnName("Contraseña");
            modelBuilder.Entity<Usuarios>().Property(t => t.Activo).HasColumnName("Activo");
            modelBuilder.Entity<Usuarios>().Property(t => t.idTipoUsuario).HasColumnName("idTipoUsuario");

            // Navigation
            modelBuilder.Entity<Usuarios>().HasMany(t => t.VentasUsuario).WithOne(t => t.Usuario);
            modelBuilder.Entity<Usuarios>().HasMany(t => t.VentasCanceladas).WithOne(t => t.UsuarioCancela);
            modelBuilder.Entity<Usuarios>().HasMany(t => t.ComprasUsuario).WithOne(t => t.Usuario);
            modelBuilder.Entity<Usuarios>().HasMany(t => t.ComprasCanceladas).WithOne(t => t.UsuarioCancela);
            modelBuilder.Entity<Usuarios>().HasMany(t => t.OrdenesAutorizadas).WithOne(t => t.Usuario);
            modelBuilder.Entity<Usuarios>().HasMany(t => t.CortesUsuario).WithOne(t => t.Usuario);
            modelBuilder.Entity<Usuarios>().HasMany(t => t.PantallasUsuario).WithOne(t => t.Usuario);
            modelBuilder.Entity<Usuarios>().HasOne(t => t.TipoUsuario).WithMany(tu => tu.Usuarios).HasForeignKey(t => t.idUsuario);

            // ------------- SubDepartamentos ----------------------------

            // Fields
            modelBuilder.Entity<SubDepartamento>().ToTable("SubDepartamentos");
            modelBuilder.Entity<SubDepartamento>().HasKey(c => new { c.IdSubDepartamento });
            modelBuilder.Entity<SubDepartamento>().Property(t => t.IdSubDepartamento).HasColumnName("IdSubDepartamentos");
            modelBuilder.Entity<SubDepartamento>().Property(t => t.IdDepartamento).HasColumnName("IdDepartamento");
            modelBuilder.Entity<SubDepartamento>().Property(t => t.Nombre).HasColumnName("Nombre");

            // Navigation
            modelBuilder.Entity<SubDepartamento>().HasMany(t => t.Productos).WithOne(pd => pd.SubDepartamento).HasForeignKey(t => t.idSubDepartamento);
            modelBuilder.Entity<SubDepartamento>().HasOne(t => t.Departamento).WithMany(pd => pd.SubDepartamentos).HasForeignKey(t => t.IdDepartamento);

            // ------------- VentaImpuestos ------------------------------

            // Fields
            modelBuilder.Entity<VentaImpuestos>().ToTable("VentaImpuesto");
            modelBuilder.Entity<VentaImpuestos>().HasKey(c => new { c.IdVentaImpuesto });
            modelBuilder.Entity<VentaImpuestos>().Property(t => t.IdVentaImpuesto).HasColumnName("IdVentaImpuesto");
            modelBuilder.Entity<VentaImpuestos>().Property(t => t.IdVenta).HasColumnName("IdVenta");
            modelBuilder.Entity<VentaImpuestos>().Property(t => t.IdImpuesto).HasColumnName("IdImpuesto");
            modelBuilder.Entity<VentaImpuestos>().Property(t => t.Cantidad).HasColumnName("Cantidad");

            // Navigation
            modelBuilder.Entity<VentaImpuestos>().HasOne(t => t.Venta).WithMany(p => p.Impuesto).HasForeignKey(t => t.IdVenta);
            modelBuilder.Entity<VentaImpuestos>().HasOne(t => t.Impuesto).WithMany(p => p.Venta).HasForeignKey(t => t.IdImpuesto);

            // ------------- VentaLote -------------------------------

            // Fields
            modelBuilder.Entity<VentaLote>().ToTable("VentaLote");
            modelBuilder.Entity<VentaLote>().HasKey(c => new { c.idVentaLote });
            modelBuilder.Entity<VentaLote>().Property(t => t.idVentaLote).HasColumnName("idVentaLote");
            modelBuilder.Entity<VentaLote>().Property(t => t.idVenta).HasColumnName("idVenta");
            modelBuilder.Entity<VentaLote>().Property(t => t.idProductoCompra).HasColumnName("idProductoCompra");
            modelBuilder.Entity<VentaLote>().Property(t => t.idProducto).HasColumnName("idProducto");
            modelBuilder.Entity<VentaLote>().Property(t => t.cantidad).HasColumnName("cantidad");
            modelBuilder.Entity<VentaLote>().Property(t => t.estatus).HasColumnName("estatus");

            // Navigation
            modelBuilder.Entity<VentaLote>().HasOne(t => t.venta).WithMany(t => t.Lotes).HasForeignKey(t => t.idVenta);
            modelBuilder.Entity<VentaLote>().HasOne(t => t.compra).WithMany(t => t.Lotes).HasForeignKey(t => t.idProductoCompra);
            modelBuilder.Entity<VentaLote>().HasOne(t => t.producto).WithMany(t => t.Lotes).HasForeignKey(t => t.idProducto);

            // ------------- VentaPagos ------------------------------

            // Fields
            modelBuilder.Entity<VentaPagos>().ToTable("VentaPagos");
            modelBuilder.Entity<VentaPagos>().HasKey(c => new { c.idVentaPago });
            modelBuilder.Entity<VentaPagos>().Property(t => t.idVentaPago).HasColumnName("idVentaPago");
            modelBuilder.Entity<VentaPagos>().Property(t => t.idVenta).HasColumnName("idVenta");
            modelBuilder.Entity<VentaPagos>().Property(t => t.idTipoPago).HasColumnName("idTipoPago");
            modelBuilder.Entity<VentaPagos>().Property(t => t.Cantidad).HasColumnName("Cantidad");
            modelBuilder.Entity<VentaPagos>().Property(t => t.Referencia).HasColumnName("Referencia");

            // Navigation
            modelBuilder.Entity<VentaPagos>().HasOne(t => t.Venta).WithMany(p => p.Pagos).HasForeignKey(t => t.idVenta);
            modelBuilder.Entity<VentaPagos>().HasOne(t => t.TipoPago).WithMany(v => v.Ventas).HasForeignKey(t => t.idTipoPago);

            // ------------- Ventas ----------------------------------

            // Fields
            modelBuilder.Entity<Ventas>().ToTable("Ventas");
            modelBuilder.Entity<Ventas>().HasKey(c => new { c.FolioVenta });
            modelBuilder.Entity<Ventas>().Property(t => t.FolioVenta).HasColumnName("FolioVenta");
            modelBuilder.Entity<Ventas>().Property(t => t.Subtotal).HasColumnName("Subtotal");
            modelBuilder.Entity<Ventas>().Property(t => t.Impuestos).HasColumnName("Impuestos");
            modelBuilder.Entity<Ventas>().Property(t => t.Total).HasColumnName("Total");
            modelBuilder.Entity<Ventas>().Property(t => t.Fecha).HasColumnName("Fecha");
            modelBuilder.Entity<Ventas>().Property(t => t.Pagado).HasColumnName("Pagado");
            modelBuilder.Entity<Ventas>().Property(t => t.Cambio).HasColumnName("Cambio");
            modelBuilder.Entity<Ventas>().Property(t => t.Utilidad).HasColumnName("Utilidad");
            modelBuilder.Entity<Ventas>().Property(t => t.idUsuario).HasColumnName("idUsuario");
            modelBuilder.Entity<Ventas>().Property(t => t.Estatus).HasColumnName("Estatus");
            modelBuilder.Entity<Ventas>().Property(t => t.idUsuarioCancela).HasColumnName("idUsuarioCancela");

            // Navigation
            modelBuilder.Entity<Ventas>().HasOne(t => t.Usuario).WithMany(vu => vu.VentasUsuario).HasForeignKey(t => t.idUsuario);
            modelBuilder.Entity<Ventas>().HasOne(t => t.UsuarioCancela).WithMany(vc => vc.VentasCanceladas).HasForeignKey(t => t.idUsuarioCancela);
            modelBuilder.Entity<Ventas>().HasMany(t => t.Productos).WithOne(t => t.Venta);
            modelBuilder.Entity<Ventas>().HasMany(t => t.Pagos).WithOne(t => t.Venta);
            modelBuilder.Entity<Ventas>().HasMany(t => t.Lotes).WithOne(t => t.venta);
            modelBuilder.Entity<Ventas>().HasMany(t => t.Impuesto).WithOne(t => t.Venta); 
        }
    }
}


