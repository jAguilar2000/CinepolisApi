using Cinepolis.Core.Entities;
using Cinepolis.Core.Exceptions;
using Cinepolis.Core.Interface;
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
    }
}
