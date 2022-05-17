using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace scyllareworked
{
	// Token: 0x02000015 RID: 21
	public static class encryption
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x0000B698 File Offset: 0x00009898
		public static string byte_arr_to_str(byte[] ba)
		{
			StringBuilder stringBuilder = new StringBuilder(ba.Length * 2);
			foreach (byte b in ba)
			{
				stringBuilder.AppendFormat("{0:x2}", b);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000B6DC File Offset: 0x000098DC
		public static byte[] str_to_byte_arr(string hex)
		{
			byte[] result;
			try
			{
				int length = hex.Length;
				byte[] array = new byte[length / 2];
				for (int i = 0; i < length; i += 2)
				{
					array[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
				}
				result = array;
			}
			catch
			{
				Console.WriteLine("\n\n  The session has ended, open program again.");
				Thread.Sleep(3500);
				Environment.Exit(0);
				result = null;
			}
			return result;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000B750 File Offset: 0x00009950
		public static string encrypt_string(string plain_text, byte[] key, byte[] iv)
		{
			Aes aes = Aes.Create();
			aes.Mode = CipherMode.CBC;
			aes.Key = key;
			aes.IV = iv;
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (ICryptoTransform cryptoTransform = aes.CreateEncryptor())
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
					{
						byte[] bytes = Encoding.Default.GetBytes(plain_text);
						cryptoStream.Write(bytes, 0, bytes.Length);
						cryptoStream.FlushFinalBlock();
						result = encryption.byte_arr_to_str(memoryStream.ToArray());
					}
				}
			}
			return result;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000B808 File Offset: 0x00009A08
		public static string decrypt_string(string cipher_text, byte[] key, byte[] iv)
		{
			Aes aes = Aes.Create();
			aes.Mode = CipherMode.CBC;
			aes.Key = key;
			aes.IV = iv;
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (ICryptoTransform cryptoTransform = aes.CreateDecryptor())
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
					{
						byte[] array = encryption.str_to_byte_arr(cipher_text);
						cryptoStream.Write(array, 0, array.Length);
						cryptoStream.FlushFinalBlock();
						byte[] array2 = memoryStream.ToArray();
						@string = Encoding.Default.GetString(array2, 0, array2.Length);
					}
				}
			}
			return @string;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000B8C8 File Offset: 0x00009AC8
		public static string iv_key()
		{
			return Guid.NewGuid().ToString().Substring(0, Guid.NewGuid().ToString().IndexOf("-", StringComparison.Ordinal));
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000029B5 File Offset: 0x00000BB5
		public static string sha256(string r)
		{
			return encryption.byte_arr_to_str(new SHA256Managed().ComputeHash(Encoding.Default.GetBytes(r)));
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000B90C File Offset: 0x00009B0C
		public static string encrypt(string message, string enc_key, string iv)
		{
			byte[] bytes = Encoding.Default.GetBytes(encryption.sha256(enc_key).Substring(0, 32));
			byte[] bytes2 = Encoding.Default.GetBytes(encryption.sha256(iv).Substring(0, 16));
			return encryption.encrypt_string(message, bytes, bytes2);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000B954 File Offset: 0x00009B54
		public static string decrypt(string message, string enc_key, string iv)
		{
			byte[] bytes = Encoding.Default.GetBytes(encryption.sha256(enc_key).Substring(0, 32));
			byte[] bytes2 = Encoding.Default.GetBytes(encryption.sha256(iv).Substring(0, 16));
			return encryption.decrypt_string(message, bytes, bytes2);
		}
	}
}
