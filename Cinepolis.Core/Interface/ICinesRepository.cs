using Cinepolis.Core.Entities;
using Cinepolis.Core.ViewModels;

namespace Cinepolis.Core.Interface
{
    public interface ICinesRepository
    {
        Task<IEnumerable<Cines>> Gets();
        Task<Cines> Get(int cineId);
        Task Insert(Cines cine);
        Task<bool> Edit(Cines cine);
        Task<bool> UploadImage(ImagenFileViewModel img);
        Task<string> UploadFotoBase64(ImagenViewModel foto);
    }
}
