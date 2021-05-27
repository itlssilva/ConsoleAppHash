using System;

namespace ConsoleAppHash
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite o código do que você deseja fazer");
            Console.WriteLine("");
            Console.WriteLine("1 - Hash/Criptografia");
            Console.WriteLine("2 - Hash");
            Console.WriteLine("3 - Criptografia");
            Console.WriteLine("4 - Decriptografia");
            Console.WriteLine("");
            var option = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("========================================");
            Console.WriteLine("");
            int number;
            bool success = int.TryParse(option, out number);
            if (success)
            {
                string key = "";

                if (number != 2)
                {
                    Console.WriteLine("Digite a chave de criptografia");
                    Console.WriteLine("");
                    key = Console.ReadLine();
                    Console.WriteLine("");
                    Console.WriteLine("========================================");                    
                }

                Console.WriteLine("");
                Console.WriteLine("Digita a palavra/frase chave");
                Console.WriteLine("");
                var text = Console.ReadLine();
                Console.WriteLine("");
                Console.WriteLine("========================================");
                switch (number)
                {
                    case 1:
                        new Service().HashCrypto(text, key);
                        break;
                    case 2:
                        new Service().Hash(text);
                        break;
                    case 3:
                        new Service().Crypto(text, key);
                        break;
                    case 4:
                        new Service().Decrypto(text, key);
                        break;
                    default:
                        Console.WriteLine("Opção selecionada não valida");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Attempted conversion of '{0}' failed.",
                                   option ?? "<null>");
            }            
        }
    }
}
