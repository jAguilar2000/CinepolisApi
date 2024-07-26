using Cinepolis.Core.Entities;
using Cinepolis.Core.Exceptions;
using Cinepolis.Core.Interface;
using Cinepolis.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cinepolis.Infrastructure.Repositories
{
    public class TipoProyeccionRepository : ITipoProyeccionRepository
    {
        private readonly DatabaseContext _context;
        public TipoProyeccionRepository(DatabaseContext context) { _context = context; }

        public async Task<IEnumerable<TipoProyeccion>> Gets()
        {
            try
            {
                var result = await _context.TipoProyeccion.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }

        public async Task<TipoProyeccion> Get(int tipoProyeccionId)
        {
            try
            {
                var result = await _context.TipoProyeccion.FirstOrDefaultAsync(x => x.tipoProyeccionId == tipoProyeccionId);

                if (result == null)
                    throw new BusinessException("Advertencia, Tipo Proyeccion no existe.");

                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }

        public async Task Insert(TipoProyeccion tipoProyeccion)
        {
            try
            {
                if (tipoProyeccion == null)
                    throw new BusinessException("Advertencia, favor llenar datos de tipo proyeccion.");

                if (String.IsNullOrEmpty(tipoProyeccion.descripcion))
                    throw new BusinessException("Favor llenar campo Descripción.");

                var tipoProyeccionDB = await _context.TipoProyeccion.FirstOrDefaultAsync(x => x.descripcion == tipoProyeccion.descripcion);

                if (tipoProyeccionDB != null)
                {
                    if (tipoProyeccionDB.activo)
                        throw new BusinessException("Tipo proyeccion ya existe, activo.");
                    else
                        throw new BusinessException("Tipo proyeccion ya existe, inactivo.");
                }

                tipoProyeccion.activo = true;
                _context.TipoProyeccion.Add(tipoProyeccion);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }

        public async Task<bool> Edit(TipoProyeccion tipoProyeccion)
        {
            try
            {
                var currientTipoProyect = await Get(tipoProyeccion.tipoProyeccionId);

                if (currientTipoProyect == null)
                    throw new BusinessException("Advertencia, Tipo proyeccion no existe.");

                if (String.IsNullOrEmpty(tipoProyeccion.descripcion))
                    throw new BusinessException("Favor llenar campo Descripción.");

                currientTipoProyect.activo = tipoProyeccion.activo;
                currientTipoProyect.descripcion = tipoProyeccion.descripcion;
                int row = await _context.SaveChangesAsync();
                return row > 0;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}");
            }
        }
    }
}
