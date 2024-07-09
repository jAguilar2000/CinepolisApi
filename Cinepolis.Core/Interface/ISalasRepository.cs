using Cinepolis.Core.Entities;

namespace Cinepolis.Core.Interface
{
    public interface ISalasRepository
    {
        Task<IEnumerable<Salas>> Gets();
        Task<Salas> Get(int salaId);
        Task Insert(Salas sala);
        Task<bool> Edit(Salas sala);
    }
}
