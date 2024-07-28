using Cinepolis.Core.Entities;

namespace Cinepolis.Core.Interface
{
    public interface IHorarioRepository
    {
        Task<IEnumerable<Horarios>> Gets();
        Task<Horarios> Get(int horarioId);
        Task Insert(Horarios horario);
        Task<bool> Edit(Horarios horario);
        Task<IEnumerable<SP_AsientosOcupados>> GetAsientosOcupados(int horarioId);
    }
}
