using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinepolis.Core.Entities
{
    public class VentaProductoDetalle
    {
        [Key]
        public int ventaDetalleId { get; set; }
        public int ventaId { get; set; }
        public int? productoId { get; set; }
        public decimal precio { get; set; }
        public decimal cantidad { get; set; }
        [ForeignKey("ventaId")]
        public virtual Venta? Venta { get; set; }
    }
}
