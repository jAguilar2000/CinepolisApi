using System.ComponentModel.DataAnnotations;

namespace Cinepolis.Core.Entities
{
    public class Configuraciones
    {
        [Key]
        public int configuracionId { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public string valor { get; set; } = string.Empty;
    }
}
