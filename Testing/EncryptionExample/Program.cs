using System;

namespace EncryptionExample
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("What kind of Encryption would you like to demo? Pig Latin (p) or Encryption (e)?");
            var encryptionType = Console.ReadLine();

            if (encryptionType == "e")
            {
                //var encryptionType = Console.ReadLine();
                Console.WriteLine("Please enter the string you wish to encrypt:");
                var sentence = Console.ReadLine();
                var plainText = "This is a test";
                var pwd = "testing";


                var encryptedText = CryptoProvider.EncryptStringAES(sentence, pwd);

                Console.WriteLine("Encrypted Text: ");
                Console.WriteLine(encryptedText);


                var decryptedText = CryptoProvider.DecryptStringAES(encryptedText, pwd);

                Console.WriteLine("Decrypted Text:");
                Console.WriteLine(decryptedText);
                Console.ReadLine();

            }
            if (encryptionType == "p")
            {
                var pigLatin = new PigLatinEncryption();
                pigLatin.pigTalk("PigLatin");
                //encryptionType = "q";
            }

        }
    }
}