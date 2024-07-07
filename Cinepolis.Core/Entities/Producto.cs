using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinepolis.Core.Entities
{
    public class Producto
    {
        [Key]
        public int productoId { get; set; }
        public string descripcion { get; set; } = string.Empty;
        public decimal precio { get; set; }
        public decimal? disponible { get; set; }
        public string foto { get; set; } = string.Empty;
        public bool activo { get; set; }
        [ForeignKey("GeneroId")]
        public virtual Genero? Genero { get; set; }
    }
}
