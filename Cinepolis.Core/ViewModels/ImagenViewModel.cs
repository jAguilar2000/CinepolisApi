using Microsoft.AspNetCore.Http;

namespace Cinepolis.Core.ViewModels
{
    public class ImagenFileViewModel
    {
        public int Id { get; set; }
        public IFormFile fileImagen { get; set; }
    }

    public class ImagenViewModel
    {
        public int Id { get; set; }
        public string ImgBase64 { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
    }
}
