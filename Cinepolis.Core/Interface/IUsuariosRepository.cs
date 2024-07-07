using Cinepolis.Core.Entities;
using Cinepolis.Core.ViewModels;

namespace Cinepolis.Core.Interface
{
    public interface IUsuariosRepository
    {
        Task<IEnumerable<Usuario>> Gets();
        Task<Usuario> Get(int usuarioId);
        Task<Usuario> Insert(UsuariosViewModel usuario);
        Task<bool> Edit(Usuario usuario);
        Task<bool> UploadImage(ImagenFileViewModel img);
        Task<string> UploadFotoBase64(ImagenViewModel foto);
    }
}
