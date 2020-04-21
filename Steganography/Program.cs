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
        private static void Hide(string message, FileModel fileModel, BMPModel bmpModel)
        {
            Console.WriteLine("Введите сообщение для сокрытия.");
            Console.WriteLine($"Максимальная длина {(bmpModel.Height * bmpModel.Width - 1)/8}");
            message = Console.ReadLine();
            fileModel.FileContent = bmpModel.ConcealMessage(message, fileModel.FileContent);
            fileModel.WriteFile();
        }
        private static void Reveal(FileModel fileModel, BMPModel bmpModel)
        {
            string message;

            message = bmpModel.RevealMessage(fileModel.FileContent);
            Console.WriteLine(message);
        }
        static void         Main(string[] args)
        {
            FileModel       fileModel;
            BMPModel        bmpModel;
            string          message;
            ConsoleKey      key;

            fileModel = null;
            bmpModel = null;
            message = null;
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
            Console.WriteLine("Скрыть сообщение(F1), раскрыть сообщение(F2), выйти(q)");
            key = Console.ReadKey().Key;
            switch (key)
            {
                case ConsoleKey.F1:
                    Hide(message, fileModel, bmpModel);
                    break;
                case ConsoleKey.F2:
                    Reveal(fileModel, bmpModel);
                    break;
                case ConsoleKey.Q:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Ничего не выбрано!");
                    break;
            }
            Console.WriteLine("Нажмите любую кнопку!");
            Console.ReadKey();
        }
    }
}
