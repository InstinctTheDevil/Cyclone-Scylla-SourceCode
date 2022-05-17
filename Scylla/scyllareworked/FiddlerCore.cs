using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using BCCertMaker;
using Fiddler;

namespace scyllareworked
{
	// Token: 0x0200000A RID: 10
	public static class FiddlerCore
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000068 RID: 104 RVA: 0x0000A0C8 File Offset: 0x000082C8
		// (remove) Token: 0x06000069 RID: 105 RVA: 0x0000A0FC File Offset: 0x000082FC
		public static event FiddlerCore.CookieHandler OnTokenReceived;

		// Token: 0x0600006A RID: 106 RVA: 0x00002690 File Offset: 0x00000890
		static FiddlerCore()
		{
			Class2.m8ctx7Mzu9Lmx();
			FiddlerCore.AssemblyDirectory = Path.GetTempPath();
			FiddlerCore.responded = false;
			FiddlerApplication.BeforeRequest += new SessionStateHandler(FiddlerCore.FiddlerApplication_BeforeRequest);
			FiddlerCore.EnsureRootCertificate();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000026BD File Offset: 0x000008BD
		private static void FiddlerApplication_BeforeRequest(Session oSession)
		{
			if (oSession.uriContains("/api/v1/inventories"))
			{
				oSession.bBufferResponse = true;
				oSession.utilSetRequestBody("");
				FiddlerCore.hui(oSession.oRequest["Cookie"]);
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000A130 File Offset: 0x00008330
		private static void hui(string pizda)
		{
			FiddlerCore.CookieHandler onTokenReceived = FiddlerCore.OnTokenReceived;
			if (onTokenReceived != null)
			{
				onTokenReceived(pizda);
			}
			FiddlerApplication.Shutdown();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000A154 File Offset: 0x00008354
		private static void EnsureRootCertificate()
		{
			BCCertMaker bccertMaker = new BCCertMaker();
			CertMaker.oCertProvider = bccertMaker;
			string text = Path.Combine(FiddlerCore.AssemblyDirectory, "defaultCertificate.p12");
			string text2 = "$0M3$H1T";
			if (!File.Exists(text))
			{
				CertMaker.removeFiddlerGeneratedCerts(true);
				bccertMaker.CreateRootCertificate();
				bccertMaker.WriteRootCertificateAndPrivateKeyToPkcs12File(text, text2, null);
			}
			else
			{
				bccertMaker.ReadRootCertificateAndPrivateKeyFromPkcs12File(text, text2, null);
			}
			if (CertMaker.rootCertIsTrusted())
			{
				return;
			}
			CertMaker.trustRootCert();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000A1BC File Offset: 0x000083BC
		public static void Start()
		{
			if (Main.epicgames.Checked)
			{
				Main.epicservers = true;
				Main.server = "brill.live";
				MessageBox.Show("Manually start DBD from Epic Games please.");
			}
			else
			{
				Process.Start("steam://rungameid/381210");
			}
			CONFIG.IgnoreServerCertErrors = true;
			FiddlerApplication.Startup(new FiddlerCoreStartupSettingsBuilder().ListenOnPort(0).RegisterAsSystemProxy().ChainToUpstreamGateway().DecryptSSL().OptimizeThreadPool().Build());
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000026F3 File Offset: 0x000008F3
		public static void Stop()
		{
			FiddlerApplication.Shutdown();
		}

		// Token: 0x0400006F RID: 111
		private static readonly string AssemblyDirectory;

		// Token: 0x04000070 RID: 112
		public static bool responded;

		// Token: 0x0200000B RID: 11
		// (Invoke) Token: 0x06000071 RID: 113
		public delegate void CookieHandler(string cookie);
	}
}
