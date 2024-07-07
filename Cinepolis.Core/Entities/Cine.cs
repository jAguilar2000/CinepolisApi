using System.ComponentModel.DataAnnotations;

namespace Cinepolis.Core.Entities
{
    public class Cine
    {
        [Key]
        public int cineId { get; set; }
        public string descripcion { get; set; } = string.Empty;
        public string foto { get; set; } = string.Empty;
        public bool activo { get; set; }
    }
}
