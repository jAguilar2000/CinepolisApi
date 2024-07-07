using Cinepolis.Core.Entities;
using Cinepolis.Core.Exceptions;
using Cinepolis.Core.Interface;
using Cinepolis.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cinepolis.Infrastructure.Repositories
{
    public class RolesRepository : IRolesRepository
    {
        private readonly DatabaseContext _context;
        public RolesRepository(DatabaseContext context) { _context = context; }

        public async Task<IEnumerable<Rol>> Gets()
        {
            try
            {
                var roles = await _context.Rol.ToListAsync();
                return roles;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }
        public async Task<Rol> Get(int rolId)
        {
            try
            {
                var rol = await _context.Rol.FirstOrDefaultAsync(x => x.rolId == rolId);
                if (rol == null)
                    throw new BusinessException("Advertencia, Rol no existe.");

                return rol;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }

        public async Task Insert(Rol rol)
        {
            try
            {
                if (rol == null)
                    throw new BusinessException("Advertencia, favor llenar datos del rol.");

                if (String.IsNullOrEmpty(rol.descripcion))
                    throw new BusinessException("Favor llenar campo Descripción.");

                var rolBD = await _context.Rol.FirstOrDefaultAsync(x => x.descripcion == rol.descripcion);

                if (rolBD != null)
                {
                    if (rolBD.activo)
                        throw new BusinessException("Rol ya existe, activo.");
                    else
                        throw new BusinessException("Rol ya existe, inactivo.");
                }

                rol.activo = true;
                _context.Rol.Add(rol);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}.");
            }
        }

        public async Task<bool> Edit(Rol rol)
        {
            try
            {
                var currientRol = await Get(rol.rolId);

                if (currientRol == null)
                    throw new BusinessException("Advertencia, categoría no existe.");

                if (String.IsNullOrEmpty(rol.descripcion))
                    throw new BusinessException("Favor llenar campo Descripción.");

                if (currientRol.descripcion != rol.descripcion)
                {
                    Rol? rolaExist = await _context.Rol.FirstOrDefaultAsync(x => x.descripcion == rol.descripcion);

                    if (rolaExist != null)
                    {
                        if (rolaExist.descripcion == rol.descripcion)
                        {
                            if (rol.activo)
                                throw new BusinessException("Rol ya existe, activo.");
                            else
                                throw new BusinessException("Rol ya existe, inactivo.");
                        }
                        currientRol.descripcion = rol.descripcion;
                    }
                    else
                    {
                        currientRol.descripcion = rol.descripcion;
                    }
                }

                currientRol.activo = rol.activo;
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
