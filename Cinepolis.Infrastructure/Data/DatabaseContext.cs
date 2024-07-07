﻿using Cinepolis.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cinepolis.Infrastructure.Data
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            Categoria = Set<Categoria>();
            Cine = Set<Cine>();
            Configuraciones = Set<Configuraciones>();
            EmailTemplete = Set<EmailTemplete>();
            Genero = Set<Genero>();
            Horario = Set<Horario>();
            Pelicula = Set<Pelicula>();
            Precio = Set<Precio>();
            Producto = Set<Producto>();
            Rol = Set<Rol>();
            Sala = Set<Sala>();
            TipoProyeccion = Set<TipoProyeccion>();
            Usuario = Set<Usuario>();
            Venta = Set<Venta>();
            VentaEntradasDetalle = Set<VentaEntradasDetalle>();
            VentaProductoDetalle = Set<VentaProductoDetalle>();
        }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Categoria = Set<Categoria>();
            Cine = Set<Cine>();
            Configuraciones = Set<Configuraciones>();
            EmailTemplete = Set<EmailTemplete>();
            Genero = Set<Genero>();
            Horario = Set<Horario>();
            Pelicula = Set<Pelicula>();
            Precio = Set<Precio>();
            Producto = Set<Producto>();
            Rol = Set<Rol>();
            Sala = Set<Sala>();
            TipoProyeccion = Set<TipoProyeccion>();
            Usuario = Set<Usuario>();
            Venta = Set<Venta>();
            VentaEntradasDetalle = Set<VentaEntradasDetalle>();
            VentaProductoDetalle = Set<VentaProductoDetalle>();
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Cine> Cine { get; set; }
        public virtual DbSet<Configuraciones> Configuraciones { get; set; }
        public virtual DbSet<EmailTemplete> EmailTemplete { get; set; }
        public virtual DbSet<Genero> Genero { get; set; }
        public virtual DbSet<Horario> Horario { get; set; }
        public virtual DbSet<Pelicula> Pelicula { get; set; }
        public virtual DbSet<Precio> Precio { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Sala> Sala { get; set; }
        public virtual DbSet<TipoProyeccion> TipoProyeccion { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Venta> Venta { get; set; }
        public virtual DbSet<VentaEntradasDetalle> VentaEntradasDetalle { get; set; }
        public virtual DbSet<VentaProductoDetalle> VentaProductoDetalle { get; set; }
    }
}