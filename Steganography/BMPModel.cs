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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace	Steganography
{
	public enum Endian { Little, Big }
	public class	BMPModel
	{
		public Endian	Type { get; set; }
		public int		Offset { get; set; }
		public int		Width { get; set; }
		public int		Height { get; set; }

		public				BMPModel(byte[] bytes)
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
		private int			ConvertByteToInt(List<byte> byteList, Endian type)
		{
			if (type == Endian.Big)
				return (BitConverter.ToInt32(byteList.ToArray(), 0));
			return (BitConverter.ToInt32(byteList.ToArray(), 3));
		}
		private BitArray	ConvertMessageToBitArray(string message)
		{
			List<byte> byteList = new List<byte>();

			foreach(char letter in message)
			{
				byteList.Add((byte)letter);
			}
			byteList.Add((byte)'#');
			return (new BitArray(byteList.ToArray()));
		}
		public byte[]		ConcealMessage(string message, byte[] bytes)
		{
			int			w;
			int			p;
			int			n;
			int			padding;
			BitArray	bitMessage;
			byte		mask;

			mask = (byte)1;
			bitMessage = ConvertMessageToBitArray(message);
			w = 0;
			p = 0;
			n = 0;
			padding = 0;
			for (int i = 0; i < bytes.Length - Offset; i++)
			{
				p = (i - 2 * padding) % 3;
				if (p == 1)
				{
					if (n >= bitMessage.Length)
						break;
					if (bitMessage[n] == true)
						bytes[Offset + i] |= mask;
					else if (bitMessage[n] == false)
						bytes[Offset + i] &= (byte)~mask;
					n++;
				}
				if (p == 2)
					w++;
				if (w == Width)
				{
					i = i + 2;
					padding++;
					w = 0;
				}
			}
			return (bytes);
		}
		private byte		BitArrayToByte(BitArray bits)
		{
			if (bits.Count != 8)
			{
				throw new ArgumentException("Количество бит не соответствует 8!");
			}
			byte[] bytes = new byte[1];
			bits.CopyTo(bytes, 0);
			return (bytes[0]);
		}
		private string		BitArrayToStr(List<bool> bitList)
		{
			List<bool> oneByte = new List<bool>();
			List<char> str = new List<char>();

			for (int i = 0; i < bitList.Count; i++)
			{
				oneByte.Add(bitList[i]);
				if (oneByte.Count == 8)
				{
					str.Add((char)BitArrayToByte(new BitArray(oneByte.ToArray())));
					oneByte.Clear();
					if (str.Last() == '#')
					{
						str.Remove('#');
						break;
					}
				}
			}
			string result = new string(str.ToArray());
			return (result);
		}
		public string		RevealMessage(byte[] bytes)
		{
			int			w;
			int			p;
			int			n;
			int			padding;
			List<bool>	bitMessage = new List<bool>();
			byte		mask;

			mask = (byte)1;
			w = 0;
			p = 0;
			n = 0;
			padding = 0;
			for (int i = 0; i < bytes.Length - Offset; i++)
			{
				p = (i - 2 * padding) % 3;
				if (p == 1)
				{
					if ((bytes[Offset + i] & mask) == 1)
						bitMessage.Add(true);
					else if ((bytes[Offset + i] & mask) == 0)
						bitMessage.Add(false);
					n++;
				}
				if (p == 2)
					w++;
				if (w == Width)
				{
					i = i + 2;
					padding++;
					w = 0;
				}
			}
			return (BitArrayToStr(bitMessage));
		}
	}
}
