using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace scyllareworked.Features
{
	// Token: 0x02000027 RID: 39
	internal class Encryption
	{
		// Token: 0x06000153 RID: 339 RVA: 0x0000D758 File Offset: 0x0000B958
		public static string smethod_0(string value)
		{
			string @string = Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTKEY));
			byte[] key = SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(@string));
			byte[] bytes = Encoding.ASCII.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTSALT)));
			return Encryption.EncryptString(value, key, bytes);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000D7B8 File Offset: 0x0000B9B8
		public static string EncryptService(string value)
		{
			string @string = Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTKEY));
			byte[] key = SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(@string));
			byte[] bytes = Encoding.ASCII.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTSALT)));
			string str = Encryption.EncryptString(value, key, bytes);
			int length = int.Parse(OnProgramStart.AID.Substring(0, 2));
			return str + Security.Obfuscate(length);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000D834 File Offset: 0x0000BA34
		public static string DecryptService(string value)
		{
			string @string = Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTKEY));
			byte[] key = SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(@string));
			byte[] bytes = Encoding.ASCII.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(Constants.APIENCRYPTSALT)));
			return Encryption.DecryptString(value, key, bytes);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000D894 File Offset: 0x0000BA94
		public static string EncryptString(string plainText, byte[] key, byte[] iv)
		{
			Aes aes = Aes.Create();
			aes.Mode = CipherMode.CBC;
			aes.Key = key;
			aes.IV = iv;
			MemoryStream memoryStream = new MemoryStream();
			ICryptoTransform transform = aes.CreateEncryptor();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
			byte[] bytes = Encoding.ASCII.GetBytes(plainText);
			cryptoStream.Write(bytes, 0, bytes.Length);
			cryptoStream.FlushFinalBlock();
			byte[] array = memoryStream.ToArray();
			memoryStream.Close();
			cryptoStream.Close();
			return Convert.ToBase64String(array, 0, array.Length);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000D910 File Offset: 0x0000BB10
		public static string DecryptString(string cipherText, byte[] key, byte[] iv)
		{
			Aes aes = Aes.Create();
			aes.Mode = CipherMode.CBC;
			aes.Key = key;
			aes.IV = iv;
			MemoryStream memoryStream = new MemoryStream();
			ICryptoTransform transform = aes.CreateDecryptor();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
			string result = string.Empty;
			try
			{
				byte[] array = Convert.FromBase64String(cipherText);
				cryptoStream.Write(array, 0, array.Length);
				cryptoStream.FlushFinalBlock();
				byte[] array2 = memoryStream.ToArray();
				result = Encoding.ASCII.GetString(array2, 0, array2.Length);
			}
			finally
			{
				memoryStream.Close();
				cryptoStream.Close();
			}
			return result;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000D9AC File Offset: 0x0000BBAC
		public static string Decode(string text)
		{
			text = text.Replace('_', '/').Replace('-', '+');
			int num = text.Length % 4;
			if (num != 2)
			{
				if (num == 3)
				{
					text += "=";
				}
			}
			else
			{
				text += "==";
			}
			return Encoding.UTF8.GetString(Convert.FromBase64String(text));
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00002683 File Offset: 0x00000883
		public Encryption()
		{
			Class2.m8ctx7Mzu9Lmx();
			base..ctor();
		}
	}
}
