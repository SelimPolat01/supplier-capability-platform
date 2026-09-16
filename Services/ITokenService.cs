namespace TedarikciKabiliyetYonetimSistemi.Services
{
    public interface ITokenService
    {
        public string GenerateToken(string userId, string email, string role, bool rememberMe);
    }
}
