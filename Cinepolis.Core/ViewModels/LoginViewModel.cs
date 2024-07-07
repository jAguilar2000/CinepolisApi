namespace Cinepolis.Core.ViewModels
{
    public class LoginViewModel { }

    public class Credenciales
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class AutenticacionResponse
    {
        public int UserId { get; set; }
        public string Message { get; set; } = string.Empty;
        public int Status { get; set; }
        public string CodVerificacion { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }

    public class ClaveTemporalResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string ClaveTemporal { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
