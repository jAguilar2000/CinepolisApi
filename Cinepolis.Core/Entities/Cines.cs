using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinepolis.Core.Entities
{
    public class Cines
    {
        [Key]
        public int cineId { get; set; }
        public string descripcion { get; set; } = string.Empty;
        public string foto { get; set; } = string.Empty;
        public bool activo { get; set; }
        [NotMapped]
        public string imgBase64 { get; set; } = string.Empty;
    }
}
