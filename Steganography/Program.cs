/******************************************************************************/
/*                                                                            */
/*                         File: Program.cs                                   */
/*                   Created By: Dmitry Diordichuk                            */
/*                        Email: cort@mail.ru                                 */
/*                                                                            */
/*                 File Created: 18th April 2020 7:14:38 pm                   */
/*                Last Modified: 20th April 2020 8:17:06 am                   */
/*                                                                            */
/******************************************************************************/

using System;
using System.IO;

namespace Steganography
{
    class Program
    {
        static void Main(string[] args)
        {
            FileModel   fileModel;
            BMPModel    bmpModel;
            string      message;

            fileModel = null;
            bmpModel = null;
            if (args.Length == 0)
                fileModel = new FileModel(string.Empty);
            else if (args.Length == 1)
                fileModel = new FileModel(args[0]);
            else
                Console.WriteLine("Неверное число аргументов!");
            if (fileModel != null)
            {
                fileModel.ReadFile();
                bmpModel = new BMPModel(fileModel.FileContent);
            }
            Console.WriteLine("Введите сообщение для шифрования.");
            Console.WriteLine($"Максимальная длина {(bmpModel.Height * bmpModel.Width - 1)/8}");
            message = Console.ReadLine();
            fileModel.FileContent = bmpModel.ConcealMessage(message, fileModel.FileContent);
            fileModel.WriteFile();
        }
    }
}
