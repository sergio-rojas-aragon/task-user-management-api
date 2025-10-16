using System.Security.Cryptography;
using System.Text;

namespace GTU.Api.Services
{
    public class HashService
    {

        public HashService() { }

        // Métodos auxiliares para hash de contraseña
        internal void CrearPasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        internal bool VerificarPasswordHash(string password, byte[] hash, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(hash);
        }
    }
}
