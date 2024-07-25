using Cinepolis.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cinepolis.Infrastructure.Data
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            Categoria = Set<Categoria>();
            Cines = Set<Cines>();
            Configuraciones = Set<Configuraciones>();
            EmailTemplete = Set<EmailTemplete>();
            Genero = Set<Genero>();
            Horarios = Set<Horarios>();
            Pelicula = Set<Pelicula>();
            Precios = Set<Precios>();
            Producto = Set<Producto>();
            Rol = Set<Rol>();
            Salas = Set<Salas>();
            TipoProyeccion = Set<TipoProyeccion>();
            Usuario = Set<Usuario>();
            Venta = Set<Venta>();
            VentaEntradasDetalle = Set<VentaEntradasDetalle>();
            VentaProductoDetalle = Set<VentaProductoDetalle>();
            TipoPelicula = Set<TipoPelicula>();
        }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Categoria = Set<Categoria>();
            Cines = Set<Cines>();
            Configuraciones = Set<Configuraciones>();
            EmailTemplete = Set<EmailTemplete>();
            Genero = Set<Genero>();
            Horarios = Set<Horarios>();
            Pelicula = Set<Pelicula>();
            Precios = Set<Precios>();
            Producto = Set<Producto>();
            Rol = Set<Rol>();
            Salas = Set<Salas>();
            TipoProyeccion = Set<TipoProyeccion>();
            Usuario = Set<Usuario>();
            Venta = Set<Venta>();
            VentaEntradasDetalle = Set<VentaEntradasDetalle>();
            VentaProductoDetalle = Set<VentaProductoDetalle>();
            TipoPelicula = Set<TipoPelicula>();
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Cines> Cines { get; set; }
        public virtual DbSet<Configuraciones> Configuraciones { get; set; }
        public virtual DbSet<EmailTemplete> EmailTemplete { get; set; }
        public virtual DbSet<Genero> Genero { get; set; }
        public virtual DbSet<Horarios> Horarios { get; set; }
        public virtual DbSet<Pelicula> Pelicula { get; set; }
        public virtual DbSet<Precios> Precios { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Salas> Salas { get; set; }
        public virtual DbSet<TipoProyeccion> TipoProyeccion { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Venta> Venta { get; set; }
        public virtual DbSet<VentaEntradasDetalle> VentaEntradasDetalle { get; set; }
        public virtual DbSet<VentaProductoDetalle> VentaProductoDetalle { get; set; }
        public virtual DbSet<TipoPelicula> TipoPelicula { get; set; }
    }
}
