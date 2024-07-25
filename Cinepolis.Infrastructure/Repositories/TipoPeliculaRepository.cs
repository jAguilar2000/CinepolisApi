using Cinepolis.Core.Entities;
using Cinepolis.Core.Exceptions;
using Cinepolis.Core.Interface;
using Cinepolis.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinepolis.Infrastructure.Repositories
{
    public class TipoPeliculaRepository: ITipoPeliculaRepository
    {
        private readonly DatabaseContext _context;
        public TipoPeliculaRepository(DatabaseContext context) { _context = context; }

        public async Task<IEnumerable<TipoPelicula>> Gets()
        {
            try
            {
                var result = await _context.TipoPelicula.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }
    }
}
