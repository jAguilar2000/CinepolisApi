using Cinepolis.Core.Entities;
using Cinepolis.Core.Exceptions;
using Cinepolis.Core.Interface;
using Cinepolis.Core.ViewModels;
using Cinepolis.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Cinepolis.Infrastructure.Repositories
{
    public class HorarioRepository : IHorarioRepository
    {
        private readonly DatabaseContext _context;
        public HorarioRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Horarios>> Gets(int? peliculaId)
        {
            try
            {
                var result = await _context.Horarios.Where(x => peliculaId != null ? x.peliculaId == peliculaId : true).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }

        public async Task<Horarios> Get(int horarioId)
        {
            try
            {
                var result = await _context.Horarios.FirstOrDefaultAsync(x => x.horarioId == horarioId);
                if (result == null)
                    throw new BusinessException("Advertencia, Horario no existe.");

                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }
        public async Task Insert(Horarios horario)
        {
            try
            {
                if (horario == null)
                    throw new BusinessException("Advertencia, favor llenar datos horarios.");

                if (horario.peliculaId <= 0)
                    throw new BusinessException("Favor seleccione la pelicula.");

                if (horario.salaId <= 0)
                    throw new BusinessException("Favor seleccione la sala.");

                if (horario.tipoProyeccioId <= 0)
                    throw new BusinessException("Favor seleccione tipo proyección.");

                bool traslapeHorario = await _context.Horarios.AnyAsync(x => x.salaId == horario.salaId && x.activo && ((horario.horaInicio >= x.horaInicio && horario.horaInicio < x.horaFinal) || (horario.horaFinal > x.horaInicio && horario.horaFinal <= x.horaFinal) || (horario.horaInicio <= x.horaInicio && horario.horaFinal >= x.horaFinal)));

                if (traslapeHorario)
                    throw new BusinessException("Hay un traslape de horario, con otro existente en la misma sala");

                horario.activo = true;
                Horarios newHorario = new Horarios();
                newHorario = horario;
                _context.Horarios.Add(newHorario);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }

        public async Task<bool> Edit(Horarios horario)
        {
            try
            {
                var current = await Get(horario.horarioId);
                if (horario == null)
                    throw new BusinessException("Advertencia, favor llenar datos horarios.");

                if (horario.peliculaId <= 0)
                    throw new BusinessException("Favor seleccione la pelicula.");

                if (horario.salaId <= 0)
                    throw new BusinessException("Favor seleccione la sala.");

                if (horario.tipoProyeccioId <= 0)
                    throw new BusinessException("Favor seleccione tipo proyección.");

                bool traslapeHorario = await _context.Horarios.AnyAsync(x => x.horarioId != horario.horarioId && x.salaId == horario.salaId && x.activo && ((horario.horaInicio >= x.horaInicio && horario.horaInicio < x.horaFinal) || (horario.horaFinal > x.horaInicio && horario.horaFinal <= x.horaFinal) || (horario.horaInicio <= x.horaInicio && horario.horaFinal >= x.horaFinal)));

                if (traslapeHorario)
                    throw new BusinessException("Hay un traslape de horario, con otro existente en la misma sala");

                current.horaInicio = horario.horaInicio;
                current.horaFinal = horario.horaFinal;
                current.peliculaId = horario.peliculaId;
                current.salaId = horario.salaId;
                current.tipoProyeccioId = horario.tipoProyeccioId;
                current.activo = horario.activo;

                int row = await _context.SaveChangesAsync();
                return row > 0;

            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }

        public async Task<IEnumerable<SP_AsientosOcupados>> GetAsientosOcupados(int horarioId)
        {
            try
            {
                var sql = "EXEC SP_AsientosOcupados @Horario";
                var paramValue = new SqlParameter("@Horario", horarioId);

                return await _context.SP_AsientosOcupados
                    .FromSqlRaw(sql, paramValue)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }
    }
}
