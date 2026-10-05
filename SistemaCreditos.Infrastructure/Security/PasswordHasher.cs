using BCrypt.Net;
using SistemaCreditos.Application.Interfaces.Security;

namespace SistemaCreditos.Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password); ;
        }
        public bool VerifyPassword(string inputPassword, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, passwordHash);
        }
    }
}
