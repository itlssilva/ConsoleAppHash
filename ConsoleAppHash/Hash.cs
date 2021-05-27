using System.Security.Cryptography;
using System.Text;

namespace ConsoleAppHash
{
    public class Hash
    {
        public string CriarHash(string text)
        {
            //var hmac256 = HMACSHA256.Create();
            var sha256 = SHA256.Create();
            byte[] bytes = Encoding.ASCII.GetBytes(text);
            byte[] hash = sha256.ComputeHash(bytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("X2"));

            return sb.ToString();
        }
    }
}
