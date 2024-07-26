using Cinepolis.Core.Entities;
using Cinepolis.Core.Exceptions;
using Cinepolis.Core.Interface;
using Cinepolis.Core.ViewModels;
using Cinepolis.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cinepolis.Infrastructure.Repositories
{
    public class GenerosRepository : IGeneroRepository
    {
        private readonly DatabaseContext _context;
        public GenerosRepository(DatabaseContext context) { _context = context; }

        public async Task<IEnumerable<Genero>> Gets()
        {
            try
            {
                var result = await _context.Genero.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }
        public async Task<Genero> Get(int generoId)
        {
            try
            {
                var result = await _context.Genero.FirstOrDefaultAsync(x => x.generoId == generoId);

                if (result == null)
                    throw new BusinessException("Advertencia, Genero no existe.");

                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }

        public async Task Insert(Genero genero)
        {
            try
            {
                if (genero == null)
                    throw new BusinessException("Advertencia, favor llenar datos de la genero.");

                if (String.IsNullOrEmpty(genero.descripcion))
                    throw new BusinessException("Favor llenar campo Descripción.");

                var generoDB = await _context.Genero.FirstOrDefaultAsync(x => x.descripcion == genero.descripcion);

                if (generoDB != null)
                {
                    if (generoDB.activo)
                        throw new BusinessException("Genero ya existe, activo.");
                    else
                        throw new BusinessException("Genero ya existe, inactivo.");
                }

                genero.activo = true;
                _context.Genero.Add(genero);
                
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }

        public async Task<bool> Edit(Genero genero)
        {
            try
            {
                var currientGenero = await Get(genero.generoId);

                if (currientGenero == null)
                    throw new BusinessException("Advertencia, Genero no existe.");

                if (String.IsNullOrEmpty(genero.descripcion))
                    throw new BusinessException("Favor llenar campo Descripción.");

                currientGenero.activo = genero.activo;
                currientGenero.descripcion = genero.descripcion;
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
