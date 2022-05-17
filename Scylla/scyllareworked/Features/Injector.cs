using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace scyllareworked.Features
{
	// Token: 0x0200002A RID: 42
	internal class Injector
	{
		// Token: 0x06000169 RID: 361 RVA: 0x00002DE6 File Offset: 0x00000FE6
		public static string Base64Encode(string plainText)
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public static string Base64Decode(string base64EncodedData)
		{
			return Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedData));
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000DC10 File Offset: 0x0000BE10
		public static string Dump_FullProfile()
		{
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://" + Main.server + ".bhvrdbd.com/api/v1/players/me/states/FullProfile/binary");
			httpWebRequest.Method = "GET";
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Accept = "*/*";
			httpWebRequest.Headers["Accept-Encoding"] = "deflate, gzip";
			httpWebRequest.UserAgent = "DeadByDaylight/++DeadByDaylight+Live-CL-267915 Windows/10.0.18363.1.256.64bit";
			httpWebRequest.ContentType = "application/json";
			if (Main.epicservers)
			{
				httpWebRequest.Headers["x-kraken-client-platform"] = "egs";
				httpWebRequest.Headers["x-kraken-client-provider"] = "egs";
			}
			else
			{
				httpWebRequest.Headers["x-kraken-client-platform"] = "steam";
				httpWebRequest.Headers["x-kraken-client-provider"] = "steam";
			}
			Cookie cookie = new Cookie();
			cookie.Name = "bhvrSession";
			cookie.Value = Main.bhvrSession;
			cookie.Domain = Main.server + ".bhvrdbd.com";
			httpWebRequest.CookieContainer = new CookieContainer();
			httpWebRequest.CookieContainer.Add(cookie);
			Main.krakenStateVersion = string.Empty;
			string result = null;
			try
			{
				using (WebResponse response = httpWebRequest.GetResponse())
				{
					using (HttpWebResponse httpWebResponse = (HttpWebResponse)response)
					{
						Main.krakenStateVersion = httpWebResponse.Headers["Kraken-State-Version"].Split(new char[]
						{
							';'
						})[0];
						using (Stream responseStream = response.GetResponseStream())
						{
							using (StreamReader streamReader = new StreamReader(responseStream))
							{
								result = streamReader.ReadLine();
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return result;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000DE20 File Offset: 0x0000C020
		public static bool Inject_FullProfile(string fullProfile)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://" + Main.server + ".bhvrdbd.com/api/v1/players/me/states/binary?stateName=FullProfile&version={0}&schemaVersion=0", Main.krakenStateVersion));
			httpWebRequest.Method = "POST";
			httpWebRequest.ProtocolVersion = HttpVersion.Version11;
			httpWebRequest.Host = Main.server + ".bhvrdbd.com";
			httpWebRequest.Accept = "*/*";
			httpWebRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "deflate, gzip");
			httpWebRequest.Headers.Add("Cookie", "bhvrSession=" + Main.bhvrSession);
			byte[] bytes = Encoding.UTF8.GetBytes(fullProfile);
			httpWebRequest.ContentType = "application/octet-stream";
			if (Main.epicservers)
			{
				httpWebRequest.Headers["x-kraken-client-platform"] = "egs";
				httpWebRequest.Headers["x-kraken-client-provider"] = "egs";
			}
			else
			{
				httpWebRequest.Headers["x-kraken-client-platform"] = "steam";
				httpWebRequest.Headers["x-kraken-client-provider"] = "steam";
			}
			httpWebRequest.Headers["x-kraken-client-version"] = "4.0.2";
			httpWebRequest.UserAgent = "DeadByDaylight/++DeadByDaylight+Live-CL-316492 Windows/10.0.18363.1.256.64bit";
			httpWebRequest.ContentLength = (long)bytes.Length;
			bool result;
			try
			{
				using (Stream requestStream = httpWebRequest.GetRequestStream())
				{
					requestStream.Write(bytes, 0, bytes.Length);
				}
				httpWebRequest.GetResponse();
				result = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00002367 File Offset: 0x00000567
		private void metroTextBox1_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00002683 File Offset: 0x00000883
		public Injector()
		{
			Class2.m8ctx7Mzu9Lmx();
			base..ctor();
		}
	}
}
