using System;
using System.IO;

namespace					Steganography
{
	public class			FileModel
	{
		public string		FullPath { get; set; }
		public				FileModel(string _fullPath)
		{
			if (_fullPath == string.Empty)
				FullPath = TryInitPath();
			else if (ValidateFilePath(_fullPath))
				FullPath = _fullPath;
			else
				FullPath = TryInitPath();
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
	}
}
