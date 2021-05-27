using System;
using System.Text;

namespace ConsoleAppHash
{
    public class Service
    {
        public void HashCrypto(string text, string key)
        {
            Console.WriteLine("Criar Hash/Criptografia!");
            Console.WriteLine("");
            Console.WriteLine("Hash: ");
            var hash = new Hash().CriarHash(text);
            Console.WriteLine("");
            Console.WriteLine(hash);
            Console.WriteLine("");
            Console.WriteLine("===================================================");
            Console.WriteLine("Hash Criptografado");
            var hashCriptografado = CryptoService.EncryptStringAES(hash, key);
            Console.WriteLine("");
            Console.WriteLine(hashCriptografado);
            Console.WriteLine("");
            Console.WriteLine("===================================================");
            Console.WriteLine("Hash Descriptografado");
            var hashDescriptografado = CryptoService.DecryptStringAES(Encoding.Default.GetBytes(hashCriptografado), key);
            Console.WriteLine("");
            Console.WriteLine(hashDescriptografado);
            Console.WriteLine("");
            Console.WriteLine("===================================================");
        }

        public void Hash(string text)
        {
            Console.WriteLine("Criar Hash!");
            Console.WriteLine("");
            Console.WriteLine("Hash: ");
            var hash = new Hash().CriarHash(text);
            Console.WriteLine("");
            Console.WriteLine(hash);
            Console.WriteLine("");
            Console.WriteLine("===================================================");
        }

        public void Crypto(string text, string key)
        {
            Console.WriteLine("Criptografia");
            var textCrypto = CryptoService.EncryptStringAES(text, key);
            Console.WriteLine("");
            Console.WriteLine(textCrypto);
            Console.WriteLine("");
            Console.WriteLine("===================================================");
        }

        public void Decrypto(string text, string key)
        {
            Console.WriteLine("Descriptografado");
            var descrypto = CryptoService.DecryptStringAES(Encoding.Default.GetBytes(text), key);
            Console.WriteLine("");
            Console.WriteLine(descrypto);
            Console.WriteLine("");
            Console.WriteLine("===================================================");
        }
    }
}
