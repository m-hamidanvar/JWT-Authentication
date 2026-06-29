using System.Security.Cryptography;
using System.Text;
namespace Project.Api.Services
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            var sha256Alg = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256Alg.ComputeHash(bytes);
            return Convert.ToBase64String(hash);

        }

        public static bool VerifyPassword(string password, string HashPass) 
        { 
            return HashPassword(password)== HashPass;
        }
    }
}
