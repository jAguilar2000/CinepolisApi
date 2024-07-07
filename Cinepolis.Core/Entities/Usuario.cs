using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinepolis.Core.Entities
{
    public class Usuario
    {
        [Key]
        public int usuarioId { get; set; }
        public string nombres { get; set; } = string.Empty;
        public string apellidos { get; set; } = string.Empty;
        public string usuario { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string correo { get; set; } = string.Empty;
        public string telefono { get; set; }=string.Empty;
        public string? foto {  get; set; }
        public bool verificado { get; set; }
        public int rolId { get; set; }
        public bool activo { get; set; }
        [NotMapped]
        public string imgBase64 { get; set; } = string.Empty;
        [ForeignKey("rolId")]
        public virtual Rol? Rol { get; set; }
    }
}
