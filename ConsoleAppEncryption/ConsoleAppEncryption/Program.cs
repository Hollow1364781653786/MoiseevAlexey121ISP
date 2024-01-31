using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

namespace EncryptStringSample
{
    public static class StringCipher
    {
        
        public static void instructions()
        {
            
        }
        private const int Keysize = 256;

        private const int DerivationIterations = 1000;

        public static string Encrypt(string plainText, string passPhrase)
        {
            Console.WriteLine("");
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            using (var streamReader = new StreamReader(cryptoStream, Encoding.UTF8))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
            
        }
    }
}
namespace EncryptStringSample
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true) { 
            Console.WriteLine("Чтобы выйти, нажмите 0");
            Console.WriteLine("Чтобы зашифровать текст, нажмите 1");
            Console.WriteLine("Чтобы зашифровать и расшифровать текст, нажмите 2");

            string Input = Console.ReadLine();
            string InputPwrd;
                
                switch (Input)
                {
                    case "0":
                        Environment.Exit(0);
                    break;

                    
                    case "1":
                        Console.WriteLine("");
                        Console.WriteLine("Введите сообщение, которое хотели бы зашифровать");
                        Input = Console.ReadLine();
                        Console.WriteLine("Введите пароль, которым вы хотели бы зашифровать сообщение:");
                        InputPwrd = Console.ReadLine();
                        Console.WriteLine("Введите сообщение, которое хотели бы зашифровать");
                        Console.WriteLine("текущее зашифрованное сообщение - " + StringCipher.Encrypt(Input, InputPwrd));
                        Console.WriteLine("");
                    break;

                    case "2":
                        Console.WriteLine("");
                        Console.WriteLine("Введите сообщение, которое хотели бы зашифровать");
                        Input = Console.ReadLine();
                        Console.WriteLine("Введите пароль, которым вы хотели бы зашифровать сообщение:");
                        InputPwrd = Console.ReadLine();
                        Console.WriteLine("Введите сообщение, которое хотели бы зашифровать");
                        Console.WriteLine("Идет расшифровка текущего зашифрованого сообщения...");
                        Console.WriteLine("текущее зашифрованное сообщение - " + StringCipher.Encrypt(Input, InputPwrd));
                        Console.WriteLine("текущее расшифрованное сообщение - " + StringCipher.Decrypt(StringCipher.Encrypt(Input, InputPwrd), InputPwrd));
                        Console.WriteLine("");
                    break;
                }
            }
        }
    }
}