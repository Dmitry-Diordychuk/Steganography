using System;
using System.IO;

namespace Steganography
{
    class Program
    {
        static void Main(string[] args)
        {
            FileModel fileModel;

            if (args.Length == 0)
                fileModel = new FileModel(string.Empty);
            else if (args.Length == 1)
                fileModel = new FileModel(args[0]);
            else
                Console.WriteLine("Неверное число аргументов!");
        }
    }
}
