using Cinepolis.Core.Entities;

namespace Cinepolis.Core.Interface
{
    public interface ITipoProyeccionRepository
    {
        Task<IEnumerable<TipoProyeccion>> Gets();
        Task<TipoProyeccion> Get(int tipoProyeccionId);
        Task Insert(TipoProyeccion tipoProyeccion);
        Task<bool> Edit(TipoProyeccion tipoProyeccion);
    }
}
