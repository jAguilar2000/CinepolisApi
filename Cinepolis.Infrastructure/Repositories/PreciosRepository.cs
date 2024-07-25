using Cinepolis.Core.Entities;
using Cinepolis.Core.Exceptions;
using Cinepolis.Core.Interface;
using Cinepolis.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cinepolis.Infrastructure.Repositories
{
    public class PreciosRepository : IPreciosRepository
    {
        private readonly DatabaseContext _context;
        public PreciosRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Precios>> Gets()
        {
            try
            {
                var result = await _context.Precios.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }
        public async Task<Precios> Get(int precioId)
        {
            try
            {
                var result = await _context.Precios.FirstOrDefaultAsync(x => x.precioId == precioId);
                if (result == null)
                    throw new BusinessException("Advertencia, Precio no existe.");

                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }
        public async Task Insert(Precios precios)
        {
            try
            {
                if (precios == null)
                    throw new BusinessException("Advertencia, favor llenar datos Precios.");

                if (precios.categoriaId <= 0)
                    throw new BusinessException("Favor seleccione la categoria.");

                if (precios.tipoProyeccionId <= 0)
                    throw new BusinessException("Favor seleccione tipo proyección.");

                if (precios.monto <= 0)
                    throw new BusinessException("Monto invalido.");



                bool existePrecoi = await _context.Precios.AnyAsync(x => x.categoriaId == precios.categoriaId && x.activo == true && x.tipoProyeccionId == precios.tipoProyeccionId);

                if (existePrecoi)
                    throw new BusinessException("Ya existe el precio para esta categoria.");

                precios.activo = true;
                Precios newPrecio = new Precios();
                newPrecio = precios;
                _context.Precios.Add(newPrecio);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }
        public async Task<bool> Edit(Precios precios)
        {
            try
            {
                var current = await Get(precios.precioId);
                if (current == null)
                    throw new BusinessException("No existe registro del precio");

                if (precios.categoriaId <= 0)
                    throw new BusinessException("Favor seleccione la categoria.");

                if (precios.tipoProyeccionId <= 0)
                    throw new BusinessException("Favor seleccione tipo proyección.");

                if (precios.monto <= 0)
                    throw new BusinessException("Monto invalido.");



                bool existePrecio = await _context.Precios.AnyAsync(x => x.precioId != precios.precioId && x.categoriaId == precios.categoriaId && x.activo == true);

                if(existePrecio)
                    throw new BusinessException("Ya esta establecido el precio para la categoria.");

                current.categoriaId = precios.categoriaId;
                current.tipoProyeccionId = precios.tipoProyeccionId;
                current.monto = precios.monto;
                current.activo = precios.activo;

                int row = await _context.SaveChangesAsync();
                return row > 0;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }
       
    }
}
