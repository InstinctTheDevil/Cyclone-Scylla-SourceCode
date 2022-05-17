using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

namespace scyllareworked.Features
{
	// Token: 0x02000023 RID: 35
	internal class Security
	{
		// Token: 0x06000143 RID: 323 RVA: 0x0000D45C File Offset: 0x0000B65C
		public static string Signature(string value)
		{
			string result;
			using (MD5 md = MD5.Create())
			{
				byte[] bytes = Encoding.UTF8.GetBytes(value);
				result = BitConverter.ToString(md.ComputeHash(bytes)).Replace("-", "");
			}
			return result;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000D4B4 File Offset: 0x0000B6B4
		private static string Session(int length)
		{
			Random random = new Random();
			return new string((from s in Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz", length)
			select s[random.Next(s.Length)]).ToArray<char>());
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000D4F8 File Offset: 0x0000B6F8
		public static string Obfuscate(int length)
		{
			Random random = new Random();
			return new string((from s in Enumerable.Repeat<string>("gd8JQ57nxXzLLMPrLylVhxoGnWGCFjO4knKTfRE6mVvdjug2NF/4aptAsZcdIGbAPmcx0O+ftU/KvMIjcfUnH3j+IMdhAW5OpoX3MrjQdf5AAP97tTB5g1wdDSAqKpq9gw06t3VaqMWZHKtPSuAXy0kkZRsc+DicpcY8E9+vWMHXa3jMdbPx4YES0p66GzhqLd/heA2zMvX8iWv4wK7S3QKIW/a9dD4ALZJpmcr9OOE=", length)
			select s[random.Next(s.Length)]).ToArray<char>());
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000D53C File Offset: 0x0000B73C
		public static void Start()
		{
			string pathRoot = Path.GetPathRoot(Environment.SystemDirectory);
			if (Constants.Started)
			{
				MessageBox.Show("A session has already been started, please end the previous one!");
				Process.GetCurrentProcess().Kill();
				return;
			}
			using (StreamReader streamReader = new StreamReader(pathRoot + "Windows\\System32\\drivers\\etc\\hosts"))
			{
				if (streamReader.ReadToEnd().Contains("api.auth.gg"))
				{
					Constants.Breached = true;
					MessageBox.Show("DNS redirecting has been detected!");
					Process.GetCurrentProcess().Kill();
				}
			}
			new InfoManager().StartListener();
			Constants.Token = Guid.NewGuid().ToString();
			ServicePointManager.SecurityProtocol = (SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12);
			Constants.APIENCRYPTKEY = Convert.ToBase64String(Encoding.Default.GetBytes(Security.Session(32)));
			Constants.APIENCRYPTSALT = Convert.ToBase64String(Encoding.Default.GetBytes(Security.Session(16)));
			Constants.IV = Convert.ToBase64String(Encoding.Default.GetBytes(Constants.RandomString(16)));
			Constants.Key = Convert.ToBase64String(Encoding.Default.GetBytes(Constants.RandomString(32)));
			Constants.Started = true;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000D66C File Offset: 0x0000B86C
		public static void End()
		{
			if (!Constants.Started)
			{
				MessageBox.Show("No session has been started, closing for security reasons!");
				Process.GetCurrentProcess().Kill();
				return;
			}
			Constants.Token = null;
			ServicePointManager.ServerCertificateValidationCallback = ((object <p0>, X509Certificate <p1>, X509Chain <p2>, SslPolicyErrors <p3>) => true);
			Constants.APIENCRYPTKEY = null;
			Constants.APIENCRYPTSALT = null;
			Constants.IV = null;
			Constants.Key = null;
			Constants.Started = false;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00002CF7 File Offset: 0x00000EF7
		private static bool PinPublicKey(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return certificate != null && certificate.GetPublicKeyString() == "04E32E295F50051CD5A5AF5B9B19DFAAB514806DDDEEAEBB38AFCC8AB7D9F1BE5C8E7A782E377DC198E62A1D091A2ADD63F4AC0A320BC4341AD980E34B47C08DB6";
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000B464 File Offset: 0x00009664
		public static string Integrity(string filename)
		{
			string result;
			using (MD5 md = MD5.Create())
			{
				using (FileStream fileStream = File.OpenRead(filename))
				{
					result = BitConverter.ToString(md.ComputeHash(fileStream)).Replace("-", "").ToLowerInvariant();
				}
			}
			return result;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000D6E0 File Offset: 0x0000B8E0
		public static bool MaliciousCheck(string date)
		{
			DateTime d = DateTime.Parse(date);
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = d - now;
			if (Convert.ToInt32(timeSpan.Seconds.ToString().Replace("-", "")) < 5 && Convert.ToInt32(timeSpan.Minutes.ToString().Replace("-", "")) < 1)
			{
				return false;
			}
			Constants.Breached = true;
			return true;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00002683 File Offset: 0x00000883
		public Security()
		{
			Class2.m8ctx7Mzu9Lmx();
			base..ctor();
		}
	}
}
