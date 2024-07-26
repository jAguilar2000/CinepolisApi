using Cinepolis.Core.Entities;
using Cinepolis.Core.Exceptions;
using Cinepolis.Core.Interface;
using Cinepolis.Core.ViewModels;
using Cinepolis.Infrastructure.Data;
using CloudinaryDotNet.Core;
using Microsoft.EntityFrameworkCore;

namespace Cinepolis.Infrastructure.Repositories
{
    public class VentasRepository : IVentasRepository
    {
        private readonly DatabaseContext _context;
        public VentasRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task InsertVenta(VentaViewModels venta)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    decimal totalEntrada = venta.VentaEntrada.Sum(x => x.totalEntrada);
                    decimal totalProducto = venta.VentaProducto.Sum(x => x.totalProducto);
                    Venta newVenta = new Venta
                    {
                        usuarioId = venta.usuarioId,
                        horarioId = venta.horarioId,
                        fecha = DateTime.Now,
                        total = totalEntrada + totalProducto,
                    };

                    _context.Venta.Add(newVenta);
                    await _context.SaveChangesAsync();

                    foreach (var ent in venta.VentaEntrada)
                    {
                        VentaEntradasDetalle newVentaEntrada = new VentaEntradasDetalle
                        {
                            ventaId = newVenta.ventaId,
                            numeroBoleto = ent.numeroBoleto,
                            precio = ent.precio,
                            cantidad = ent.cantidad
                        };

                        _context.VentaEntradasDetalle.Add(newVentaEntrada);
                        await _context.SaveChangesAsync();
                    }

                    foreach (var prod in venta.VentaProducto)
                    {
                        Producto? producto = await _context.Producto.FirstOrDefaultAsync(x => x.productoId == prod.productoId);

                        if(producto != null)
                        {
                            if (producto.disponible >= prod.cantidad)
                            {
                                VentaProductoDetalle newVentaProducto = new VentaProductoDetalle
                                {
                                    ventaId = newVenta.ventaId,
                                    productoId = prod.productoId,
                                    precio = prod.precio,
                                    cantidad = prod.cantidad
                                };


                                _context.VentaProductoDetalle.Add(newVentaProducto);
                                await _context.SaveChangesAsync();

                                producto.disponible -= prod.cantidad;

                                await _context.SaveChangesAsync();
                            }
                            else
                            {
                                throw new BusinessException($"No se completo su compra debido que sobrepasa lo disponible del producto <<{producto.descripcion}>>");
                            }

                        }
                    }

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new BusinessException($"Error: {ex.Message}.");
                }
            }
        }
    }
}
