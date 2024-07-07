using Cinepolis.Core.ViewModels;

namespace Cinepolis.Core.Interface
{
    public interface IAutenticacionRepository
    {
        Task<AutenticacionResponse> Login(Credenciales credenciales);
        Task<ClaveTemporalResponse> EnviarClaveTemporal(string usuario);
        Task<bool> UsuarioVerificado(int usuarioId);
        Task<bool> ReestablecerPassword(string password, int userId);
    }
}
