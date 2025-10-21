using System.Security.Cryptography;
using System.Text;

namespace PropEase.Helper
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            using(var md5=MD5.Create()) { 
            var inputBytes=Encoding.ASCII.GetBytes(password);
                var hashBytes=md5.ComputeHash(inputBytes);
                var sb =new StringBuilder();
                foreach(var b in hashBytes)
                {
                    sb.Append(b.ToString("X2"));
                }
                return sb.ToString();

          
            }
        }
    }
}
