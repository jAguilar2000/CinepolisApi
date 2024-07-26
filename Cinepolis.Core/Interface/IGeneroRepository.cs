using Cinepolis.Core.Entities;

namespace Cinepolis.Core.Interface
{
    public interface IGeneroRepository
    {
        Task<IEnumerable<Genero>> Gets();
        Task<Genero> Get(int generoId);
        Task Insert(Genero genero);
        Task<bool> Edit(Genero genero);
    }
}
