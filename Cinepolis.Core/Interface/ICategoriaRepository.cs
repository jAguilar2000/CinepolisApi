using Cinepolis.Core.Entities;

namespace Cinepolis.Core.Interface
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> Gets();
        Task<Categoria> Get(int categoriaId);
        Task Insert(Categoria categoria);
        Task<bool> Edit(Categoria categoria);
    }
}
