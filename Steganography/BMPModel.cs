/******************************************************************************/
/*                                                                            */
/*                         File: BMPModel.cs                                  */
/*                   Created By: Dmitry Diordichuk                            */
/*                        Email: cort@mail.ru                                 */
/*                                                                            */
/*                 File Created: 20th April 2020 2:59:22 pm                   */
/*                Last Modified: 20th April 2020 3:00:08 pm                   */
/*                                                                            */
/******************************************************************************/

using System;
using System.Collections.Generic;

namespace	Steganography
{
	public enum Endian { Little, Big }
	public class	BMPModel
	{
		public Endian	Type { get; set; }
		public int		Offset { get; set; }
		public int		Width { get; set; }
		public int		Height { get; set; }

		public	BMPModel(byte[] bytes)
		{
			List<byte> offsetAr = new List<byte>();
			List<byte> width = new List<byte>();
			List<byte> height = new List<byte>();

			if (bytes == null)
			{
				Console.WriteLine("Файл пустой");
				Environment.Exit(0);
			}
			else
				for (int i = 0; i < bytes.Length; i++)
				{
					if ((i == 0 || i == 1)
					&& bytes[i] != 77 && bytes[i] != 66)
					{
						Console.WriteLine("Содержание файла не соответствует формату!");
						Environment.Exit(0);
					}
					if (i == 0 && bytes[i] == 66)
						Type = Endian.Little;
					else
						Type = Endian.Big;
					if (i > 9 && i < 14)
						offsetAr.Add(bytes[i]);
					if (i > 17 && i < 22)
						width.Add(bytes[i]);
					if (i > 21 && i < 26)
						height.Add(bytes[i]);
				}
			Offset = ConvertByteToInt(offsetAr, Type);
			Width = ConvertByteToInt(width, Type);
			Height = ConvertByteToInt(height, Type);
		}
		private int	ConvertByteToInt(List<byte> byteList, Endian type)
		{
			if (type == Endian.Big)
				return (BitConverter.ToInt32(byteList.ToArray(), 0));
			return (BitConverter.ToInt32(byteList.ToArray(), 3));
		}
	}
}
