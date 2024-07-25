using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinepolis.Core.Entities
{
    public class TipoPelicula
    {
        public int tipoPeliculaId { get; set; }
        public string tipoPeliculas { get; set; } = string.Empty;
        public bool activo { get; set; }
    }
}
