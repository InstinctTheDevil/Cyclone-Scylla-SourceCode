using System;
using System.Linq;
using System.Security.Principal;

namespace scyllareworked.Features
{
	// Token: 0x0200001D RID: 29
	internal class Constants
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00002ACA File Offset: 0x00000CCA
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00002AD1 File Offset: 0x00000CD1
		public static string Token { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00002AD9 File Offset: 0x00000CD9
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00002AE0 File Offset: 0x00000CE0
		public static string Date { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00002AE8 File Offset: 0x00000CE8
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00002AEF File Offset: 0x00000CEF
		public static string APIENCRYPTKEY { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00002AF7 File Offset: 0x00000CF7
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00002AFE File Offset: 0x00000CFE
		public static string APIENCRYPTSALT { get; set; }

		// Token: 0x06000108 RID: 264 RVA: 0x00002B06 File Offset: 0x00000D06
		public static string RandomString(int length)
		{
			return new string((from s in Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", length)
			select s[Constants.random.Next(s.Length)]).ToArray<char>());
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00002B41 File Offset: 0x00000D41
		public static string HWID()
		{
			return WindowsIdentity.GetCurrent().User.Value;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00002683 File Offset: 0x00000883
		public Constants()
		{
			Class2.m8ctx7Mzu9Lmx();
			base..ctor();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00002B52 File Offset: 0x00000D52
		static Constants()
		{
			Class2.m8ctx7Mzu9Lmx();
			Constants.Breached = false;
			Constants.Started = false;
			Constants.IV = null;
			Constants.Key = null;
			Constants.ApiUrl = "https://api.auth.gg/csharp/";
			Constants.Initialized = false;
			Constants.random = new Random();
		}

		// Token: 0x040000AF RID: 175
		public static bool Breached;

		// Token: 0x040000B0 RID: 176
		public static bool Started;

		// Token: 0x040000B1 RID: 177
		public static string IV;

		// Token: 0x040000B2 RID: 178
		public static string Key;

		// Token: 0x040000B3 RID: 179
		public static string ApiUrl;

		// Token: 0x040000B4 RID: 180
		public static bool Initialized;

		// Token: 0x040000B5 RID: 181
		public static Random random;
	}
}
