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
            BMPModel    bMPModel;

            fileModel = null;
            if (args.Length == 0)
                fileModel = new FileModel(string.Empty);
            else if (args.Length == 1)
                fileModel = new FileModel(args[0]);
            else
                Console.WriteLine("Неверное число аргументов!");
            if (fileModel != null)
            {
                fileModel.ReadFile();
                bMPModel = new BMPModel(fileModel.FileContent);
            }
        }
    }
}
