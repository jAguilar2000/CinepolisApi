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
    }
}
