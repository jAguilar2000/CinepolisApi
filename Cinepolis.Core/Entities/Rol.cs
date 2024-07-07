using System.ComponentModel.DataAnnotations;

namespace Cinepolis.Core.Entities
{
    public class Rol
    {
        [Key]
        public int rolId { get; set; }
        public string descripcion { get; set; } = string.Empty;
        public bool activo { get; set; }
    }
}
