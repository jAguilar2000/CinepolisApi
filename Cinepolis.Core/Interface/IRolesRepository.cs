using Cinepolis.Core.Entities;

namespace Cinepolis.Core.Interface
{
    public interface IRolesRepository
    {
        Task<IEnumerable<Rol>> Gets();
        Task<Rol> Get(int rolId);
        Task Insert(Rol rol);
        Task<bool> Edit(Rol rol);
    }
}
