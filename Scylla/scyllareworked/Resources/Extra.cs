using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using Newtonsoft.Json;
using Org.BouncyCastle.Utilities.Zlib;

namespace scyllareworked.Resources
{
	// Token: 0x0200001B RID: 27
	public static class Extra
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x0000BA5C File Offset: 0x00009C5C
		public static byte[] ReadToEnd(Stream stream)
		{
			long position = 0L;
			if (stream.CanSeek)
			{
				position = stream.Position;
				stream.Position = 0L;
			}
			byte[] result;
			try
			{
				byte[] array = new byte[4096];
				int num = 0;
				int num2;
				while ((num2 = stream.Read(array, num, array.Length - num)) > 0)
				{
					num += num2;
					if (num == array.Length)
					{
						int num3 = stream.ReadByte();
						if (num3 != -1)
						{
							byte[] array2 = new byte[array.Length * 2];
							Buffer.BlockCopy(array, 0, array2, 0, array.Length);
							Buffer.SetByte(array2, num, (byte)num3);
							array = array2;
							num++;
						}
					}
				}
				byte[] array3 = array;
				if (array.Length != num)
				{
					array3 = new byte[num];
					Buffer.BlockCopy(array, 0, array3, 0, num);
				}
				result = array3;
			}
			finally
			{
				if (stream.CanSeek)
				{
					stream.Position = position;
				}
			}
			return result;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000BB3C File Offset: 0x00009D3C
		public static string RawDecrypt(string text, string key)
		{
			byte[] array = Convert.FromBase64String(text);
			byte[] bytes = Encoding.ASCII.GetBytes(key);
			ICryptoTransform transform = new RijndaelManaged
			{
				Mode = CipherMode.ECB,
				Padding = PaddingMode.Zeros
			}.CreateDecryptor(bytes, null);
			MemoryStream memoryStream = new MemoryStream(array);
			CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read);
			byte[] array2 = new byte[array.Length];
			int count = cryptoStream.Read(array2, 0, array2.Length);
			memoryStream.Close();
			cryptoStream.Close();
			return Encoding.UTF8.GetString(array2, 0, count);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000BBBC File Offset: 0x00009DBC
		public static string DecryptCdn(string content, string key)
		{
			content = content.Substring(8).Trim();
			string text = Extra.RawDecrypt(content, key);
			string text2 = "";
			foreach (char c in text)
			{
				text2 += (c + '\u0001').ToString();
			}
			string text4 = text2.Replace("\u0001", "");
			if (!text4.StartsWith("DbdDAQEB"))
			{
				return text4;
			}
			byte[] array = Convert.FromBase64String(text4.Substring(8));
			byte[] buffer = array.Subset(4, array.Length - 4);
			string text3;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (InflaterInputStream inflaterInputStream = new InflaterInputStream(new MemoryStream(buffer)))
				{
					inflaterInputStream.CopyTo(memoryStream);
				}
				memoryStream.Position = 0L;
				text3 = Encoding.Unicode.GetString(Extra.ReadToEnd(memoryStream));
			}
			return text3;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		public static byte[] PaddingWithNumber(byte[] buffer, int num)
		{
			byte[] bytes = BitConverter.GetBytes(num);
			byte[] array = new byte[bytes.Length + buffer.Length];
			Buffer.BlockCopy(bytes, 0, array, 0, bytes.Length);
			Buffer.BlockCopy(buffer, 0, array, bytes.Length, buffer.Length);
			return array;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000BD10 File Offset: 0x00009F10
		public static string RawEncrypt(string text, string key)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(text);
			byte[] bytes2 = Encoding.ASCII.GetBytes(key);
			ICryptoTransform transform = new RijndaelManaged
			{
				Mode = CipherMode.ECB,
				Padding = PaddingMode.Zeros
			}.CreateEncryptor(bytes2, null);
			MemoryStream memoryStream = new MemoryStream(bytes);
			CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read);
			byte[] array = new byte[bytes.Length];
			int length = cryptoStream.Read(array, 0, array.Length);
			memoryStream.Close();
			cryptoStream.Close();
			return Convert.ToBase64String(array, 0, length);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000BD90 File Offset: 0x00009F90
		public static string EncryptCdn(string content, string key)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(content);
			string text5;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (ZOutputStream zoutputStream = new ZOutputStream(memoryStream, -1))
				{
					zoutputStream.Write(bytes, 0, bytes.Length);
					zoutputStream.Flush();
					zoutputStream.Finish();
					memoryStream.Position = 0L;
					string text = Convert.ToBase64String(Extra.PaddingWithNumber(Extra.ReadToEnd(memoryStream), bytes.Length));
					string text2 = "DbdDAQEB";
					string str = "DbdDAgAC";
					int num = 16 - (text2.Length + text.Length) % 16;
					string text3 = text2 + text.PadRight(text.Length + num, '\u0001');
					string text4 = "";
					foreach (char c in text3)
					{
						text4 += (c - '\u0001').ToString();
					}
					text5 = str + Extra.RawEncrypt(text4, key);
				}
			}
			return text5;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000BEB4 File Offset: 0x0000A0B4
		public static string Send(string message)
		{
			WebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://discord.com/api/webhooks/808388078640955402/VGXQFAXMEx-9pw7OYzxEV1-duh46EnwQBgovP8dnl2C2LujILL16astszdHFWhqoJa0M");
			webRequest.ContentType = "application/json";
			webRequest.Method = "POST";
			using (StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream()))
			{
				string value = JsonConvert.SerializeObject(new
				{
					username = "Cyclone | Scyllaa",
					embeds = new <>f__AnonymousType1<string, string, string>[]
					{
						new
						{
							description = message,
							title = "Action Encountered",
							color = "445522"
						}
					}
				});
				streamWriter.Write(value);
			}
			HttpWebResponse httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
			return message;
		}
	}
}
