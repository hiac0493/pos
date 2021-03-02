using Microsoft.EntityFrameworkCore;
using Security.Business.Model;

namespace Security.EntityFramework.Edbm
{
    public class SecurityDbContext : DbContext
    {
        private string _connString;

        public SecurityDbContext(string connString)
        {
            _connString = connString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connString);
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ------------- Pantallas -------------------------------

            // Fields
            modelBuilder.Entity<Pantallas>().ToTable("Pantallas");
            modelBuilder.Entity<Pantallas>().HasKey(c => new { c.idPantalla });
            modelBuilder.Entity<Pantallas>().Property(t => t.idPantalla).HasColumnName("idPantalla");
            modelBuilder.Entity<Pantallas>().Property(t => t.NombrePantalla).HasColumnName("NombrePantalla");
            modelBuilder.Entity<Pantallas>().Property(t => t.TextoPanel).HasColumnName("TextoPanel");
            modelBuilder.Entity<Pantallas>().Property(t => t.Url).HasColumnName("Url");
            modelBuilder.Entity<Pantallas>().Property(t => t.Icono).HasColumnName("Icono");
            modelBuilder.Entity<Pantallas>().Property(t => t.SubPantalla).HasColumnName("SubPantalla");
            modelBuilder.Entity<Pantallas>().Property(t => t.Activo).HasColumnName("Activo");

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
        }
    }
}
