using Cinepolis.Core.Entities;
using Cinepolis.Core.Exceptions;
using Cinepolis.Core.Interface;
using Cinepolis.Core.ViewModels;
using Cinepolis.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Cinepolis.Infrastructure.Repositories
{
    public class AutenticacionRepository : IAutenticacionRepository
    {
        private string _caracteresPermitidos = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static readonly Random _random = new Random();
        private readonly DatabaseContext _context;

        NotificacionesRepository _notificacionesRepository = new NotificacionesRepository();
        public AutenticacionRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<AutenticacionResponse> Login(Credenciales credenciales)
        {
            try
            {
                if (String.IsNullOrEmpty(credenciales.UserName) || String.IsNullOrEmpty(credenciales.Password))
                    return new AutenticacionResponse { Status = 401, Message = "El usuario y contraseña son requeridos." };

                Usuario? userDB = await _context.Usuario.FirstOrDefaultAsync(x => x.usuario == credenciales.UserName);

                if (userDB == null)
                    return new AutenticacionResponse { Status = 401, Message = "Usuario no encontrado." };

                if (!userDB.activo)
                    return new AutenticacionResponse { Status = 401, Message = "Usuario inactivo." };

                bool isPassword = BCrypt.Net.BCrypt.Verify(credenciales.Password, userDB.password);

                if (!isPassword)
                    return new AutenticacionResponse { Status = 401, Message = "Contraseña incorrecta." };

                Rol? roles = await _context.Rol.FirstOrDefaultAsync(x => x.rolId == userDB.rolId);

                if (roles == null)
                    throw new BusinessException("Usuario no tiene Rol");

                if (!userDB.verificado)
                {
                    var codVerif = GetCodVerif_PwdTemp();
                    string subject = "Código Verificación";
                    EmailTemplete? templete = await _context.EmailTemplete.FirstOrDefaultAsync(x => x.nombre == "NOTIF_CODIGO_VERIFICACION");
                    string NombreUsuario = $"{userDB.nombres} {userDB.apellidos}";

                    if (templete == null)
                        throw new BusinessException("Temple de correo no esta diseñado.");

                    string body = templete.body;
                    body = body.Replace("@paramUsuario", NombreUsuario);
                    body = body.Replace("@paramCodigo", codVerif);
                    _ = _notificacionesRepository.EnviarCorreo(subject, body, true, userDB.correo);

                    return new AutenticacionResponse { Status = 200, Message = "Ok", UserId = userDB.usuarioId, Username = credenciales.UserName, CodVerificacion = codVerif, Rol = roles.descripcion };
                }

                return new AutenticacionResponse { Status = 200, Message = "Ok", UserId = userDB.usuarioId, Username = credenciales.UserName, Rol = roles.descripcion };
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error de autenticación: {ex.Message}");
            }
        }

        public async Task<ClaveTemporalResponse> EnviarClaveTemporal(string usuario)
        {
            try
            {
                Usuario? userDB = await _context.Usuario.FirstOrDefaultAsync(x => x.usuario == usuario);

                if (userDB == null)
                    throw new BusinessException($"Usuario {usuario} no existe.");

                if (!userDB.activo)
                    throw new BusinessException($"Usuario {usuario} se encuentra inactivo, favor contactarse con el administrador.");

                if (String.IsNullOrEmpty(userDB.correo))
                    throw new BusinessException($"Usuario {usuario} no tiene correo asociado.");

                string pwdTemporal = GetCodVerif_PwdTemp();

                EmailTemplete? templete = await _context.EmailTemplete.FirstOrDefaultAsync(x => x.nombre == "NOTIF_PASSWORD_TEMPORAL");
                string NombreUsuario = $"{userDB.nombres} {userDB.apellidos}";

                if (templete == null)
                    throw new BusinessException("Temple de correo no esta diseñado.");

                string body = templete.body;
                body = body.Replace("@paramUsuario", NombreUsuario);
                body = body.Replace("@paramUserName", usuario);
                body = body.Replace("@paramPwdTemporal", pwdTemporal);
                string subject = "Reestablecer Contraseña";
                _ = _notificacionesRepository.EnviarCorreo(subject, body, true, userDB.correo);

                return new ClaveTemporalResponse { UserId = userDB.usuarioId, UserName = usuario, ClaveTemporal = pwdTemporal, Message = $"Se envió la clave temporal al correo {userDB.correo}" };

            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error de reestablecimiento: {ex.Message}");
            }
        }

        public async Task<bool> UsuarioVerificado(int usuarioId)
        {
            try
            {
                Usuario? currentUser = await _context.Usuario.FirstOrDefaultAsync(x => x.usuarioId == usuarioId);

                if (currentUser == null)
                    throw new BusinessException($"Usuario no existe.");

                currentUser.verificado = true;

                int row = await _context.SaveChangesAsync();
                return row > 0;

            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error de verificación: {ex.Message}");
            }
        }

        public string GetCodVerif_PwdTemp()
        {
            int size = 6;
            char[] codigo = new char[size];
            for (int i = 0; i < size; i++)
            {
                codigo[i] = _caracteresPermitidos[_random.Next(_caracteresPermitidos.Length)];
            }
            return new string(codigo);
        }

        public async Task<bool> ReestablecerPassword(string password, int userId)
        {
            try
            {
                if (String.IsNullOrEmpty(password))
                    throw new BusinessException("Favor ingrese la contraseña.");

                Usuario? currienUser = await _context.Usuario.FirstOrDefaultAsync(x => x.usuarioId == userId);

                if (currienUser == null) throw new BusinessException("No se encontro usuario");

                currienUser.password = BCrypt.Net.BCrypt.HashPassword(password);
                int row = await _context.SaveChangesAsync();
                return row > 0;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error cambio de contraseña: {ex.Message}");
            }
        }
    }
}
