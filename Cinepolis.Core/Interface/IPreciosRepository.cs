using Cinepolis.Core.Entities;

namespace Cinepolis.Core.Interface
{
    public interface IPreciosRepository
    {
        Task<IEnumerable<Precios>> Gets();
        Task<Precios> Get(int precioId);
        Task Insert(Precios precios);
        Task<bool> Edit(Precios precios);
    }
}
