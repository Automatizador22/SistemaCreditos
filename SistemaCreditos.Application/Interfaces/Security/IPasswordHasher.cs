namespace SistemaCreditos.Application.Interfaces.Security
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string passwordHash, string inputPassword);
    }
}
