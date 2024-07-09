using Cinepolis.Core.Entities;
using Cinepolis.Core.ViewModels;

namespace Cinepolis.Core.Interface
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> Gets();
        Task<Producto> Get(int productoId);
        Task Insert(Producto producto);
        Task<bool> Edit(Producto producto);
        Task<bool> UploadImage(ImagenFileViewModel img);
        Task<string> UploadFotoBase64(ImagenViewModel foto);
    }
}
