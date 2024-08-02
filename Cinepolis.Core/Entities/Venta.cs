using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinepolis.Core.Entities
{
    public class Venta
    {
        [Key]
        public int ventaId { get; set; }
        public int usuarioId { get; set; }
        public int? horarioId { get; set; }
        public DateTime fecha { get; set; }
        public decimal? total { get; set; }
        [ForeignKey("usuarioId")]
        public virtual Usuario? Usuario { get; set; }
        [ForeignKey("horarioId")]
        public virtual Horarios? Horario { get; set; }
        public virtual ICollection<VentaEntradasDetalle> ventaEntradasDetalles { get; set; }
        public virtual ICollection<VentaProductoDetalle> ventaProductoDetalles { get; set; }
    }
}
