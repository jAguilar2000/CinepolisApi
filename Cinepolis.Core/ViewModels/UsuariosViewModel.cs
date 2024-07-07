using System.ComponentModel.DataAnnotations.Schema;

namespace Cinepolis.Core.ViewModels
{
    public class UsuariosViewModel
    {
        public string usuario { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string nombres { get; set; } = string.Empty;
        public string apellidos { get; set; } = string.Empty;
        public string telefono { get; set; } = string.Empty;
        public string? direccion { get; set; } = string.Empty;
        public string correo { get; set; } = string.Empty;
        public bool esAdministrador { get; set; }
        [NotMapped]
        public string imgBase64 { get; set; } = string.Empty;
    }
}
