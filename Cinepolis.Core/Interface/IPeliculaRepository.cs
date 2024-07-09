using Cinepolis.Core.Entities;
using Cinepolis.Core.ViewModels;

namespace Cinepolis.Core.Interface
{
    public interface IPeliculaRepository
    {
        Task<IEnumerable<Pelicula>> Gets();
        Task<Pelicula> Get(int peliculaId);
        Task Insert(Pelicula pelicula);
        Task<bool> Edit(Pelicula pelicula);
        Task<bool> UploadImage(ImagenFileViewModel img);
        Task<string> UploadFotoBase64(ImagenViewModel foto);
    }
}
