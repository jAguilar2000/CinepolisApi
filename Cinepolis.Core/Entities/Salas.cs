using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinepolis.Core.Entities
{
    public class Salas
    {
        [Key]
        public int salaId { get; set; }
        public string descripcion { get; set; } = string.Empty;
        public int cineId { get; set; }
        public int? capacidad { get; set; }
        public bool activo { get; set; }
        [ForeignKey("cineId")]
        public virtual Cines? Cine { get; set; }
    }
}
