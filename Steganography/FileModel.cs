/******************************************************************************/
/*                                                                            */
/*                         File: FileModel.cs                                 */
/*                   Created By: Dmitry Diordichuk                            */
/*                        Email: cort@mail.ru                                 */
/*                                                                            */
/*                 File Created: 18th April 2020 7:36:48 pm                   */
/*                Last Modified: 20th April 2020 8:16:55 am                   */
/*                                                                            */
/******************************************************************************/

using System;
using System.IO;

namespace					Steganography
{
	public class			FileModel
	{
		private bool		InitFlag { get; set; } = false;
		public string		FullPath { get; set; }
		public byte[]		FileContent { get; set; }
		public				FileModel(string _fullPath)
		{
			if (_fullPath == string.Empty)
				FullPath = TryInitPath();
			else if (ValidateFilePath(_fullPath))
				FullPath = _fullPath;
			else
				FullPath = TryInitPath();
			InitFlag = true;
		}
		private string		TryInitPath()
		{
			string path;

			path = null;
			while (!ValidateFilePath(path))
			{
				Console.WriteLine("Введите полный путь к файлу:");
				path = Console.ReadLine();
			}
			return (path);
		}
		private bool		ValidateFilePath(string _fullPath)
		{
			FileInfo	fileInfo;

			if (_fullPath == null)
			{
				return (false);
			}
			fileInfo = new FileInfo(_fullPath);
			if (fileInfo.Exists)
			{
				if (fileInfo.Extension == ".bmp")
				{
					return (true);
				}
				else
				{
					Console.WriteLine($"Не верное расширение файла!");
					return (false);
				}
			}
			else
			{
				Console.WriteLine($"Файл {_fullPath} не существует!");
				return (false);
			}
		}
		public void			ReadFile()
		{
			if (InitFlag)
			{
				try
				{
					FileContent = File.ReadAllBytes(this.FullPath);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Исключение: {ex.Message}");
					Environment.Exit(0);
				}
			}
			else
				FileContent = null;
		}
	}
}
