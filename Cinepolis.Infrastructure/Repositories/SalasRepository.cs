using Cinepolis.Core.Entities;
using Cinepolis.Core.Exceptions;
using Cinepolis.Core.Interface;
using Cinepolis.Core.ViewModels;
using Cinepolis.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinepolis.Infrastructure.Repositories
{
    public class SalasRepository : ISalasRepository
    {
        private readonly DatabaseContext _context;
        UploadFileRepository upload = new UploadFileRepository();
        public SalasRepository(DatabaseContext context) { _context = context; }

        public async Task<IEnumerable<Salas>> Gets()
        {
            try
            {
                var result = await _context.Salas.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }
        public async Task<Salas> Get(int salaId)
        {
            try
            {
                var result = await _context.Salas.FirstOrDefaultAsync(x => x.salaId == salaId);
                if (result == null)
                    throw new BusinessException("Advertencia, Sala no existe.");

                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }

        public async Task Insert(Salas sala)
        {
            try
            {
                if (sala == null)
                    throw new BusinessException("Advertencia, favor llenar datos de la Sala.");

                if (String.IsNullOrEmpty(sala.descripcion))
                    throw new BusinessException("Favor llenar campo Descripción.");

                var salaDB = await _context.Salas.FirstOrDefaultAsync(x => x.descripcion == sala.descripcion);

                if (salaDB != null)
                {

                    if (salaDB.activo)
                        throw new BusinessException("Sala ya existe, activo.");
                    else
                        throw new BusinessException("Sala ya existe, inactivo.");
                }

                sala.activo = true;
                Salas newSala = new Salas();
                newSala = sala;
                _context.Salas.Add(newSala);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }

        public async Task<bool> Edit(Salas sala)
        {
            try
            {
                var currientSala = await Get(sala.salaId);

                if (currientSala == null)
                    throw new BusinessException("Advertencia, Pelicula no existe.");
                if (String.IsNullOrEmpty(sala.descripcion))
                    throw new BusinessException("Favor llenar campo Descripción.");

                currientSala.activo = sala.activo;
                int row = await _context.SaveChangesAsync();
                return row > 0;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}");
            }
        }
    }
}
