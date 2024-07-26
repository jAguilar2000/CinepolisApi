using Cinepolis.Core.Entities;
using Cinepolis.Core.Exceptions;
using Cinepolis.Core.Interface;
using Cinepolis.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cinepolis.Infrastructure.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly DatabaseContext _context;
        public CategoriaRepository(DatabaseContext context) { _context = context; }

        public async Task<IEnumerable<Categoria>> Gets()
        {
            try
            {
                var result = await _context.Categoria.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }

        public async Task<Categoria> Get(int categoriaId)
        {
            try
            {
                var result = await _context.Categoria.FirstOrDefaultAsync(x => x.categoriaId == categoriaId);

                if (result == null)
                    throw new BusinessException("Advertencia, Categoria no existe.");

                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }

        public async Task Insert(Categoria categoria)
        {
            try
            {
                if (categoria == null)
                    throw new BusinessException("Advertencia, favor llenar datos de la categoria.");

                if (String.IsNullOrEmpty(categoria.descripcion))
                    throw new BusinessException("Favor llenar campo Descripción.");

                var categoriaDB = await _context.Categoria.FirstOrDefaultAsync(x => x.descripcion == categoria.descripcion);

                if (categoriaDB != null)
                {
                    if (categoriaDB.activo)
                        throw new BusinessException("Categoria ya existe, activo.");
                    else
                        throw new BusinessException("Categoria ya existe, inactivo.");
                }

                categoria.activo = true;
                _context.Categoria.Add(categoria);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }

        public async Task<bool> Edit(Categoria categoria)
        {
            try
            {
                var currientCategoria = await Get(categoria.categoriaId);

                if (currientCategoria == null)
                    throw new BusinessException("Advertencia, Categoria no existe.");

                if (String.IsNullOrEmpty(categoria.descripcion))
                    throw new BusinessException("Favor llenar campo Descripción.");

                currientCategoria.activo = categoria.activo;
                currientCategoria.descripcion = categoria.descripcion;
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
