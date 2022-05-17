using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using scyllareworked.Features;
using scyllareworked.Properties;
using scyllareworked.Resources;

namespace scyllareworked
{
	// Token: 0x02000006 RID: 6
	public partial class Main : Form
	{
		// Token: 0x0600001C RID: 28
		[DllImport("Gdi32.dll")]
		private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002388 File Offset: 0x00000588
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002390 File Offset: 0x00000590
		public string Token { get; private set; }

		// Token: 0x0600001F RID: 31
		[DllImport("User32.dll")]
		public static extern bool ReleaseCapture();

		// Token: 0x06000020 RID: 32
		[DllImport("User32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

		// Token: 0x06000021 RID: 33 RVA: 0x00004020 File Offset: 0x00002220
		public Main()
		{
			Class2.m8ctx7Mzu9Lmx();
			base..ctor();
			this.InitializeComponent();
			base.FormBorderStyle = FormBorderStyle.None;
			FiddlerCore.OnTokenReceived += this.FiddlerCoreOnOnTokenReceived;
			base.Region = Region.FromHrgn(Main.CreateRoundRectRgn(0, 0, 704, 344, 20, 20));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00004078 File Offset: 0x00002278
		private void FiddlerCoreOnOnTokenReceived(string token)
		{
			this.Token = token;
			Main.bhvrSession = token.Replace("bhvrSession=", string.Empty);
			FiddlerCore.Stop();
			if (Main.epicgames.Checked)
			{
				Main.server = "brill.live";
				Main.epicservers = true;
			}
			Main.bhvrSession = this.Token.Replace("bhvrSession=", string.Empty);
			FiddlerCore.Stop();
			this.KillDbd();
			new Thread(delegate(object t)
			{
				this.SendGetRequest("/wallet/currencies");
			})
			{
				IsBackground = true
			}.Start();
			this.allowbuttons = true;
			this.CheckRankAdvanced();
			this.StopItSoon();
			this.stopwhile = true;
			this.cookiepanel.Visible = false;
			this.label25.Visible = false;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00004138 File Offset: 0x00002338
		private void KillDbd()
		{
			foreach (Process process in Process.GetProcesses())
			{
				if (process.ProcessName.ToUpper().Contains("DAYLIGHT"))
				{
					process.Kill();
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002399 File Offset: 0x00000599
		private void Main_Load(object sender, EventArgs e)
		{
			base.ClientSize = new Size(704, 344);
			base.MouseDown += this.Form_MouseDown;
			this.cookiepanel.Location = new Point(0, 29);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000023D5 File Offset: 0x000005D5
		private void Form_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				Main.ReleaseCapture();
				Main.SendMessage(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000023FD File Offset: 0x000005FD
		private void panel2_Paint(object sender, PaintEventArgs e)
		{
			this.panel2.Region = Region.FromHrgn(Main.CreateRoundRectRgn(0, 0, 167, 131, 20, 20));
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002424 File Offset: 0x00000624
		private void panel3_Paint(object sender, PaintEventArgs e)
		{
			this.panel3.Region = Region.FromHrgn(Main.CreateRoundRectRgn(0, 0, 167, 68, 20, 20));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002448 File Offset: 0x00000648
		private void panel4_Paint(object sender, PaintEventArgs e)
		{
			this.panel4.Region = Region.FromHrgn(Main.CreateRoundRectRgn(0, 0, 167, 68, 20, 20));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002367 File Offset: 0x00000567
		private void label4_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000246C File Offset: 0x0000066C
		private void panel5_Paint(object sender, PaintEventArgs e)
		{
			this.panel5.Region = Region.FromHrgn(Main.CreateRoundRectRgn(0, 0, 497, 323, 20, 20));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000417C File Offset: 0x0000237C
		private void panel5_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				base.Location = new Point(Cursor.Position.X + e.X, Cursor.Position.Y + e.Y);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002367 File Offset: 0x00000567
		private void profile_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000041CC File Offset: 0x000023CC
		private void CheckRankKiller(string json, string path)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://" + Main.server + ".bhvrdbd.com/api/v1" + path);
			httpWebRequest.ServicePoint.Expect100Continue = false;
			httpWebRequest.Method = "PUT";
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Accept = "*/*";
			httpWebRequest.Headers["Accept-Encoding"] = "deflate, gzip";
			httpWebRequest.UserAgent = "DeadByDaylight/++DeadByDaylight+Live-CL-281719 Windows/10.0.18363.1.256.64bit";
			httpWebRequest.ContentType = "application/json";
			Cookie cookie = new Cookie();
			cookie.Name = "bhvrSession";
			cookie.Value = Main.bhvrSession;
			cookie.Domain = Main.server + ".bhvrdbd.com";
			httpWebRequest.CookieContainer = new CookieContainer();
			httpWebRequest.CookieContainer.Add(cookie);
			using (Stream requestStream = httpWebRequest.GetRequestStream())
			{
				using (StreamWriter streamWriter = new StreamWriter(requestStream))
				{
					streamWriter.Write(json);
				}
			}
			try
			{
				Stream stream = httpWebRequest.GetRequestStream();
				WebResponse response = httpWebRequest.GetResponse();
				stream = response.GetResponseStream();
				string text = null;
				using (StreamReader streamReader = new StreamReader(stream))
				{
					text = streamReader.ReadLine();
				}
				stream.Close();
				response.Close();
				int num = text.IndexOf("{\"killerPips\":") + "{\"killerPips\":".Length;
				int num2 = text.IndexOf(",\"", num);
				string pepicek = text.Substring(num, num2 - num);
				if (base.InvokeRequired)
				{
					base.BeginInvoke(new Action(delegate()
					{
						if (int.Parse(pepicek) == 0)
						{
							this.label9.Text = "Killer Rank: 20";
						}
						if (int.Parse(pepicek) >= 0 && int.Parse(pepicek) <= 2)
						{
							this.label9.Text = "Killer Rank: 20";
							return;
						}
						if (int.Parse(pepicek) >= 3 && int.Parse(pepicek) <= 6)
						{
							this.label9.Text = "Killer Rank: 19";
							return;
						}
						if (int.Parse(pepicek) >= 7 && int.Parse(pepicek) <= 10)
						{
							this.label9.Text = "Killer Rank: 18";
							return;
						}
						if (int.Parse(pepicek) >= 11 && int.Parse(pepicek) <= 14)
						{
							this.label9.Text = "Killer Rank: 17";
							return;
						}
						if (int.Parse(pepicek) >= 15 && int.Parse(pepicek) <= 18)
						{
							this.label9.Text = "Killer Rank: 16";
							return;
						}
						if (int.Parse(pepicek) >= 19 && int.Parse(pepicek) <= 22)
						{
							this.label9.Text = "Killer Rank: 15";
							return;
						}
						if (int.Parse(pepicek) >= 23 && int.Parse(pepicek) <= 26)
						{
							this.label9.Text = "Killer Rank: 14";
							return;
						}
						if (int.Parse(pepicek) >= 27 && int.Parse(pepicek) <= 30)
						{
							this.label9.Text = "Killer Rank: 13";
							return;
						}
						if (int.Parse(pepicek) >= 31 && int.Parse(pepicek) <= 35)
						{
							this.label9.Text = "Killer Rank: 12";
							return;
						}
						if (int.Parse(pepicek) >= 36 && int.Parse(pepicek) <= 39)
						{
							this.label9.Text = "Killer Rank: 11";
							return;
						}
						if (int.Parse(pepicek) >= 40 && int.Parse(pepicek) <= 44)
						{
							this.label9.Text = "Killer Rank: 10";
							return;
						}
						if (int.Parse(pepicek) >= 45 && int.Parse(pepicek) <= 49)
						{
							this.label9.Text = "Killer Rank: 9";
							return;
						}
						if (int.Parse(pepicek) >= 50 && int.Parse(pepicek) <= 54)
						{
							this.label9.Text = "Killer Rank: 8";
							return;
						}
						if (int.Parse(pepicek) >= 55 && int.Parse(pepicek) <= 59)
						{
							this.label9.Text = "Killer Rank: 7";
							return;
						}
						if (int.Parse(pepicek) >= 60 && int.Parse(pepicek) <= 64)
						{
							this.label9.Text = "Killer Rank: 6";
							return;
						}
						if (int.Parse(pepicek) >= 65 && int.Parse(pepicek) <= 69)
						{
							this.label9.Text = "Killer Rank: 5";
							return;
						}
						if (int.Parse(pepicek) >= 70 && int.Parse(pepicek) <= 74)
						{
							this.label9.Text = "Killer Rank: 4";
							return;
						}
						if (int.Parse(pepicek) >= 75 && int.Parse(pepicek) <= 79)
						{
							this.label9.Text = "Killer Rank: 3";
							return;
						}
						if (int.Parse(pepicek) >= 80 && int.Parse(pepicek) <= 84)
						{
							this.label9.Text = "Killer Rank: 2";
							return;
						}
						if (int.Parse(pepicek) >= 85)
						{
							this.label9.Text = "Killer Rank: 1";
						}
					}));
				}
				else if (base.InvokeRequired)
				{
					base.BeginInvoke(new Action(delegate()
					{
						if (int.Parse(pepicek) == 0)
						{
							this.label9.Text = "Killer Rank: 20";
						}
						if (int.Parse(pepicek) >= 0 && int.Parse(pepicek) <= 2)
						{
							this.label9.Text = "Killer Rank: 20";
							return;
						}
						if (int.Parse(pepicek) >= 3 && int.Parse(pepicek) <= 6)
						{
							this.label9.Text = "Killer Rank: 19";
							return;
						}
						if (int.Parse(pepicek) >= 7 && int.Parse(pepicek) <= 10)
						{
							this.label9.Text = "Killer Rank: 18";
							return;
						}
						if (int.Parse(pepicek) >= 11 && int.Parse(pepicek) <= 14)
						{
							this.label9.Text = "Killer Rank: 17";
							return;
						}
						if (int.Parse(pepicek) >= 15 && int.Parse(pepicek) <= 18)
						{
							this.label9.Text = "Killer Rank: 16";
							return;
						}
						if (int.Parse(pepicek) >= 19 && int.Parse(pepicek) <= 22)
						{
							this.label9.Text = "Killer Rank: 15";
							return;
						}
						if (int.Parse(pepicek) >= 23 && int.Parse(pepicek) <= 26)
						{
							this.label9.Text = "Killer Rank: 14";
							return;
						}
						if (int.Parse(pepicek) >= 27 && int.Parse(pepicek) <= 30)
						{
							this.label9.Text = "Killer Rank: 13";
							return;
						}
						if (int.Parse(pepicek) >= 31 && int.Parse(pepicek) <= 35)
						{
							this.label9.Text = "Killer Rank: 12";
							return;
						}
						if (int.Parse(pepicek) >= 36 && int.Parse(pepicek) <= 39)
						{
							this.label9.Text = "Killer Rank: 11";
							return;
						}
						if (int.Parse(pepicek) >= 40 && int.Parse(pepicek) <= 44)
						{
							this.label9.Text = "Killer Rank: 10";
							return;
						}
						if (int.Parse(pepicek) >= 45 && int.Parse(pepicek) <= 49)
						{
							this.label9.Text = "Killer Rank: 9";
							return;
						}
						if (int.Parse(pepicek) >= 50 && int.Parse(pepicek) <= 54)
						{
							this.label9.Text = "Killer Rank: 8";
							return;
						}
						if (int.Parse(pepicek) >= 55 && int.Parse(pepicek) <= 59)
						{
							this.label9.Text = "Killer Rank: 7";
							return;
						}
						if (int.Parse(pepicek) >= 60 && int.Parse(pepicek) <= 64)
						{
							this.label9.Text = "Killer Rank: 6";
							return;
						}
						if (int.Parse(pepicek) >= 65 && int.Parse(pepicek) <= 69)
						{
							this.label9.Text = "Killer Rank: 5";
							return;
						}
						if (int.Parse(pepicek) >= 70 && int.Parse(pepicek) <= 74)
						{
							this.label9.Text = "Killer Rank: 4";
							return;
						}
						if (int.Parse(pepicek) >= 75 && int.Parse(pepicek) <= 79)
						{
							this.label9.Text = "Killer Rank: 3";
							return;
						}
						if (int.Parse(pepicek) >= 80 && int.Parse(pepicek) <= 84)
						{
							this.label9.Text = "Killer Rank: 2";
							return;
						}
						if (int.Parse(pepicek) >= 85)
						{
							this.label9.Text = "Killer Rank: 1";
						}
					}));
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000043D0 File Offset: 0x000025D0
		private void CheckRankSurvivor(string json, string path)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://" + Main.server + ".bhvrdbd.com/api/v1" + path);
			httpWebRequest.ServicePoint.Expect100Continue = false;
			httpWebRequest.Method = "PUT";
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Accept = "*/*";
			httpWebRequest.Headers["Accept-Encoding"] = "deflate, gzip";
			httpWebRequest.UserAgent = "DeadByDaylight/++DeadByDaylight+Live-CL-281719 Windows/10.0.18363.1.256.64bit";
			httpWebRequest.ContentType = "application/json";
			Cookie cookie = new Cookie();
			cookie.Name = "bhvrSession";
			cookie.Value = Main.bhvrSession;
			cookie.Domain = Main.server + ".bhvrdbd.com";
			httpWebRequest.CookieContainer = new CookieContainer();
			httpWebRequest.CookieContainer.Add(cookie);
			using (Stream requestStream = httpWebRequest.GetRequestStream())
			{
				using (StreamWriter streamWriter = new StreamWriter(requestStream))
				{
					streamWriter.Write(json);
				}
			}
			try
			{
				Stream stream = httpWebRequest.GetRequestStream();
				WebResponse response = httpWebRequest.GetResponse();
				stream = response.GetResponseStream();
				string text = null;
				using (StreamReader streamReader = new StreamReader(stream))
				{
					text = streamReader.ReadLine();
				}
				stream.Close();
				response.Close();
				int num = text.IndexOf(",\"survivorPips\":") + ",\"survivorPips\":".Length;
				int num2 = text.IndexOf("}", num);
				string rankpred = text.Substring(num, num2 - num);
				if (int.Parse(rankpred) == 0)
				{
					this.label8.Text = "Survivor Rank: 20";
				}
				if (base.InvokeRequired)
				{
					base.BeginInvoke(new Action(delegate()
					{
						if (int.Parse(rankpred) >= 0 && int.Parse(rankpred) <= 2)
						{
							this.label8.Text = "Survivor Rank: 20";
							return;
						}
						if (int.Parse(rankpred) >= 3 && int.Parse(rankpred) <= 6)
						{
							this.label8.Text = "Survivor Rank: 19";
							return;
						}
						if (int.Parse(rankpred) >= 7 && int.Parse(rankpred) <= 10)
						{
							this.label8.Text = "Survivor Rank: 18";
							return;
						}
						if (int.Parse(rankpred) >= 11 && int.Parse(rankpred) <= 14)
						{
							this.label8.Text = "Survivor Rank: 17";
							return;
						}
						if (int.Parse(rankpred) >= 15 && int.Parse(rankpred) <= 18)
						{
							this.label8.Text = "Survivor Rank: 16";
							return;
						}
						if (int.Parse(rankpred) >= 19 && int.Parse(rankpred) <= 22)
						{
							this.label8.Text = "Survivor Rank: 15";
							return;
						}
						if (int.Parse(rankpred) >= 23 && int.Parse(rankpred) <= 26)
						{
							this.label8.Text = "Survivor Rank: 14";
							return;
						}
						if (int.Parse(rankpred) >= 27 && int.Parse(rankpred) <= 30)
						{
							this.label8.Text = "Survivor Rank: 13";
							return;
						}
						if (int.Parse(rankpred) >= 31 && int.Parse(rankpred) <= 35)
						{
							this.label8.Text = "Survivor Rank: 12";
							return;
						}
						if (int.Parse(rankpred) >= 36 && int.Parse(rankpred) <= 39)
						{
							this.label8.Text = "Survivor Rank: 11";
							return;
						}
						if (int.Parse(rankpred) >= 40 && int.Parse(rankpred) <= 44)
						{
							this.label8.Text = "Survivor Rank: 10";
							return;
						}
						if (int.Parse(rankpred) >= 45 && int.Parse(rankpred) <= 49)
						{
							this.label8.Text = "Survivor Rank: 9";
							return;
						}
						if (int.Parse(rankpred) >= 50 && int.Parse(rankpred) <= 54)
						{
							this.label8.Text = "Survivor Rank: 8";
							return;
						}
						if (int.Parse(rankpred) >= 55 && int.Parse(rankpred) <= 59)
						{
							this.label8.Text = "Survivor Rank: 7";
							return;
						}
						if (int.Parse(rankpred) >= 60 && int.Parse(rankpred) <= 64)
						{
							this.label8.Text = "Survivor Rank: 6";
							return;
						}
						if (int.Parse(rankpred) >= 65 && int.Parse(rankpred) <= 69)
						{
							this.label8.Text = "Survivor Rank: 5";
							return;
						}
						if (int.Parse(rankpred) >= 70 && int.Parse(rankpred) <= 74)
						{
							this.label8.Text = "Survivor Rank: 4";
							return;
						}
						if (int.Parse(rankpred) >= 75 && int.Parse(rankpred) <= 79)
						{
							this.label8.Text = "Survivor Rank: 3";
							return;
						}
						if (int.Parse(rankpred) >= 80 && int.Parse(rankpred) <= 84)
						{
							this.label8.Text = "Survivor Rank: 2";
							return;
						}
						if (int.Parse(rankpred) >= 85)
						{
							this.label8.Text = "Survivor Rank: 1";
						}
					}));
				}
				else if (base.InvokeRequired)
				{
					base.BeginInvoke(new Action(delegate()
					{
						if (int.Parse(rankpred) >= 0 && int.Parse(rankpred) <= 2)
						{
							this.label8.Text = "Survivor Rank: 20";
							return;
						}
						if (int.Parse(rankpred) >= 3 && int.Parse(rankpred) <= 6)
						{
							this.label8.Text = "Survivor Rank: 19";
							return;
						}
						if (int.Parse(rankpred) >= 7 && int.Parse(rankpred) <= 10)
						{
							this.label8.Text = "Survivor Rank: 18";
							return;
						}
						if (int.Parse(rankpred) >= 11 && int.Parse(rankpred) <= 14)
						{
							this.label8.Text = "Survivor Rank: 17";
							return;
						}
						if (int.Parse(rankpred) >= 15 && int.Parse(rankpred) <= 18)
						{
							this.label8.Text = "Survivor Rank: 16";
							return;
						}
						if (int.Parse(rankpred) >= 19 && int.Parse(rankpred) <= 22)
						{
							this.label8.Text = "Survivor Rank: 15";
							return;
						}
						if (int.Parse(rankpred) >= 23 && int.Parse(rankpred) <= 26)
						{
							this.label8.Text = "Survivor Rank: 14";
							return;
						}
						if (int.Parse(rankpred) >= 27 && int.Parse(rankpred) <= 30)
						{
							this.label8.Text = "Survivor Rank: 13";
							return;
						}
						if (int.Parse(rankpred) >= 31 && int.Parse(rankpred) <= 35)
						{
							this.label8.Text = "Survivor Rank: 12";
							return;
						}
						if (int.Parse(rankpred) >= 36 && int.Parse(rankpred) <= 39)
						{
							this.label8.Text = "Survivor Rank: 11";
							return;
						}
						if (int.Parse(rankpred) >= 40 && int.Parse(rankpred) <= 44)
						{
							this.label8.Text = "Survivor Rank: 10";
							return;
						}
						if (int.Parse(rankpred) >= 45 && int.Parse(rankpred) <= 49)
						{
							this.label8.Text = "Survivor Rank: 9";
							return;
						}
						if (int.Parse(rankpred) >= 50 && int.Parse(rankpred) <= 54)
						{
							this.label8.Text = "Survivor Rank: 8";
							return;
						}
						if (int.Parse(rankpred) >= 55 && int.Parse(rankpred) <= 59)
						{
							this.label8.Text = "Survivor Rank: 7";
							return;
						}
						if (int.Parse(rankpred) >= 60 && int.Parse(rankpred) <= 64)
						{
							this.label8.Text = "Survivor Rank: 6";
							return;
						}
						if (int.Parse(rankpred) >= 65 && int.Parse(rankpred) <= 69)
						{
							this.label8.Text = "Survivor Rank: 5";
							return;
						}
						if (int.Parse(rankpred) >= 70 && int.Parse(rankpred) <= 74)
						{
							this.label8.Text = "Survivor Rank: 4";
							return;
						}
						if (int.Parse(rankpred) >= 75 && int.Parse(rankpred) <= 79)
						{
							this.label8.Text = "Survivor Rank: 3";
							return;
						}
						if (int.Parse(rankpred) >= 80 && int.Parse(rankpred) <= 84)
						{
							this.label8.Text = "Survivor Rank: 2";
							return;
						}
						if (int.Parse(rankpred) >= 85)
						{
							this.label8.Text = "Survivor Rank: 1";
						}
					}));
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000045F0 File Offset: 0x000027F0
		private string SendPutRequest(string json, string path)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://" + Main.server + ".bhvrdbd.com/api/v1" + path);
			httpWebRequest.ServicePoint.Expect100Continue = false;
			httpWebRequest.Method = "PUT";
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Accept = "*/*";
			httpWebRequest.Headers["Accept-Encoding"] = "deflate, gzip";
			httpWebRequest.UserAgent = "DeadByDaylight/++DeadByDaylight+Live-CL-281719 Windows/10.0.18363.1.256.64bit";
			httpWebRequest.ContentType = "application/json";
			Cookie cookie = new Cookie();
			cookie.Name = "bhvrSession";
			cookie.Value = Main.bhvrSession;
			cookie.Domain = Main.server + ".bhvrdbd.com";
			httpWebRequest.CookieContainer = new CookieContainer();
			httpWebRequest.CookieContainer.Add(cookie);
			using (Stream requestStream = httpWebRequest.GetRequestStream())
			{
				using (StreamWriter streamWriter = new StreamWriter(requestStream))
				{
					streamWriter.Write(json);
				}
			}
			string result;
			using (WebResponse response = httpWebRequest.GetResponse())
			{
				using (Stream responseStream = response.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(responseStream))
					{
						result = streamReader.ReadToEnd();
					}
				}
			}
			return result;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00004774 File Offset: 0x00002974
		private string SendGetRequest(string path)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://" + Main.server + ".bhvrdbd.com/api/v1" + path);
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
			string result;
			using (WebResponse response = httpWebRequest.GetResponse())
			{
				using (Stream responseStream = response.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(responseStream))
					{
						string text = streamReader.ReadLine();
						int startIndex = text.IndexOf("\"BonusBloodpoints\"},{\"balance\":") + "\"BonusBloodpoints\"},{\"balance\":".Length;
						text.IndexOf(",", startIndex);
						int num = text.IndexOf("\"Cells\"},{\"balance\":") + "\"Cells\"},{\"balance\":".Length;
						int num2 = text.IndexOf(",", num);
						this.currentbp = text.Substring(num, num2 - num);
						int num3 = text.IndexOf("{\"list\":[{\"balance\":") + "{\"list\":[{\"balance\":".Length;
						int num4 = text.IndexOf(",\"currency\":\"BonusBloodpoints\"}", num3);
						this.bonusbp = text.Substring(num3, num4 - num3);
						result = streamReader.ReadToEnd();
						response.Close();
						streamReader.Close();
						int num5 = Convert.ToInt32(this.currentbp);
						int num6 = Convert.ToInt32(this.bonusbp);
						this.finalbp = num5 + num6;
						if (base.InvokeRequired)
						{
							base.BeginInvoke(new Action(delegate()
							{
								this.label11.Text = "Bloodpoints: " + this.finalbp.ToString();
							}));
						}
						else
						{
							this.label11.Text = "Bloodpoints: " + this.finalbp.ToString();
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00004A3C File Offset: 0x00002C3C
		public void KillerPlus()
		{
			string json = "{\"forceReset\":false,\"killerPips\":2,\"survivorPips\":0}";
			try
			{
				this.SendPutRequest(json, "/ranks/pips");
			}
			catch
			{
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00004A74 File Offset: 0x00002C74
		public void SurvivorPlus()
		{
			string json = "{\"forceReset\":false,\"killerPips\":0,\"survivorPips\":2}";
			try
			{
				this.SendPutRequest(json, "/ranks/pips");
			}
			catch
			{
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00004AAC File Offset: 0x00002CAC
		public void KillerMinus()
		{
			string json = "{\"forceReset\":false,\"killerPips\":-2,\"survivorPips\":0}";
			try
			{
				this.SendPutRequest(json, "/ranks/pips");
			}
			catch
			{
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00004AE4 File Offset: 0x00002CE4
		public void SurvivorMinus()
		{
			string json = "{\"forceReset\":false,\"killerPips\":0,\"survivorPips\":-2}";
			try
			{
				this.SendPutRequest(json, "/ranks/pips");
			}
			catch
			{
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00004B1C File Offset: 0x00002D1C
		public void CheckRankAll()
		{
			string json = "{\"forceReset\":false,\"killerPips\":0,\"survivorPips\":0}";
			try
			{
				this.CheckRankSurvivor(json, "/ranks/pips");
				this.CheckRankKiller(json, "/ranks/pips");
			}
			catch
			{
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002493 File Offset: 0x00000693
		private void button2_Click(object sender, EventArgs e)
		{
			new Thread(delegate(object t)
			{
				this.SurvivorMinus();
				this.CheckRankAll();
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000024B2 File Offset: 0x000006B2
		private void button4_Click(object sender, EventArgs e)
		{
			new Thread(delegate(object t)
			{
				this.KillerPlus();
				this.CheckRankAll();
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000024D1 File Offset: 0x000006D1
		private void button3_Click(object sender, EventArgs e)
		{
			new Thread(delegate(object t)
			{
				this.KillerMinus();
				this.CheckRankAll();
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000024F0 File Offset: 0x000006F0
		public void CheckRankAdvanced()
		{
			new Thread(delegate(object t)
			{
				do
				{
					this.CheckRankAll();
				}
				while (!this.stoprank);
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000250F File Offset: 0x0000070F
		public void StopItSoon()
		{
			new Thread(delegate(object t)
			{
				Thread.Sleep(500);
				this.stoprank = true;
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000252E File Offset: 0x0000072E
		private void guna2Button1_Click(object sender, EventArgs e)
		{
			new Thread(delegate(object t)
			{
				this.SurvivorPlus();
				this.CheckRankAll();
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002367 File Offset: 0x00000567
		private void button6_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00004B5C File Offset: 0x00002D5C
		private void guna2Button8_Click(object sender, EventArgs e)
		{
			string text = Injector.Dump_FullProfile();
			if (text != null)
			{
				if (!Directory.Exists("Backup"))
				{
					Directory.CreateDirectory("Backup");
				}
				File.WriteAllText(".\\Backup\\" + string.Format("savefile backup.txt", Array.Empty<object>()), Extra.DecryptCdn(text, Main.encryptionKey));
				if (!Directory.Exists("FullProfile"))
				{
					Directory.CreateDirectory("FullProfile");
				}
				try
				{
					string path = ".\\FullProfile\\FullProfile.txt";
					string contents = File.ReadAllText(".\\Backup\\" + string.Format("savefile backup.txt", Array.Empty<object>()));
					File.WriteAllText(path, contents);
					SystemSounds.Asterisk.Play();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002367 File Offset: 0x00000567
		private void editor_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002367 File Offset: 0x00000567
		private void upload_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002367 File Offset: 0x00000567
		private void download_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00004C20 File Offset: 0x00002E20
		private void guna2Button10_Click(object sender, EventArgs e)
		{
			if (!File.Exists(".\\FullProfile\\FullProfile.txt"))
			{
				MessageBox.Show("ERROR: could not find FullProfile.txt");
				return;
			}
			if (Injector.Dump_FullProfile() != null & Injector.Inject_FullProfile(Extra.EncryptCdn(File.ReadAllText(".\\FullProfile\\FullProfile.txt", Encoding.Default), Main.encryptionKey)))
			{
				SystemSounds.Asterisk.Play();
				return;
			}
			MessageBox.Show("ERROR: failed to inject FullProfile.txt");
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000254D File Offset: 0x0000074D
		private void guna2Button3_Click(object sender, EventArgs e)
		{
			new Thread(delegate(object t)
			{
				this.KillerMinus();
				this.CheckRankAll();
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00004C84 File Offset: 0x00002E84
		private void guna2Button8_Click_1(object sender, EventArgs e)
		{
			if (!this.allowbuttons)
			{
				return;
			}
			this.cookiepanel.Visible = false;
			this.label23.Visible = false;
			this.label13.Visible = false;
			this.label5.Visible = true;
			this.guna2Panel_0.Visible = true;
			this.titletext.Visible = false;
			this.profilepanel.Visible = true;
			this.profilepanel.Location = new Point(0, 29);
			this.rankpanel.Visible = false;
			this.bppanel.Visible = false;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00004D1C File Offset: 0x00002F1C
		private void currency_Click_1(object sender, EventArgs e)
		{
			if (!this.allowbuttons)
			{
				return;
			}
			this.cookiepanel.Visible = false;
			this.label23.Visible = false;
			this.label13.Visible = true;
			this.label5.Visible = false;
			this.titletext.Visible = false;
			this.guna2Panel_0.Visible = true;
			this.profilepanel.Visible = false;
			this.bppanel.Location = new Point(0, 29);
			this.rankpanel.Visible = false;
			this.bppanel.Visible = true;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00004DB4 File Offset: 0x00002FB4
		private void rank_Click_1(object sender, EventArgs e)
		{
			if (!this.allowbuttons)
			{
				return;
			}
			this.cookiepanel.Visible = false;
			this.label23.Visible = false;
			this.label13.Visible = false;
			this.label5.Visible = false;
			this.guna2Panel_0.Visible = true;
			this.titletext.Visible = true;
			this.profilepanel.Visible = false;
			this.rankpanel.Location = new Point(0, 29);
			this.rankpanel.Visible = true;
			this.bppanel.Visible = false;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00004E4C File Offset: 0x0000304C
		private void guna2Button8_Click_2(object sender, EventArgs e)
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\DeadByDaylight\\Saved\\Config\\WindowsNoEditor\\Engine.ini";
			if (!File.ReadAllText(path).Contains("n.VerifyPeer=False"))
			{
				try
				{
					using (StreamWriter streamWriter = File.AppendText(path))
					{
						streamWriter.WriteLine("\n\n[/Script/Engine.NetworkSettings]");
						streamWriter.WriteLine("n.VerifyPeer=False");
					}
				}
				catch
				{
				}
			}
			FiddlerCore.Start();
			if (Main.epicgames.Checked)
			{
				Main.epicservers = true;
				Main.server = "brill.live";
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00004EEC File Offset: 0x000030EC
		private void guna2Button9_Click(object sender, EventArgs e)
		{
			if (Main.epicgames.Checked)
			{
				Main.server = "brill.live";
				Main.epicservers = true;
			}
			this.Token = Clipboard.GetText();
			Main.bhvrSession = this.Token.Replace("bhvrSession=", string.Empty);
			FiddlerCore.Stop();
			this.KillDbd();
			new Thread(delegate(object t)
			{
				this.SendGetRequest("/wallet/currencies");
			})
			{
				IsBackground = true
			}.Start();
			this.allowbuttons = true;
			this.CheckRankAdvanced();
			this.StopItSoon();
			this.stopwhile = true;
			this.cookiepanel.Visible = false;
			this.label25.Visible = false;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00004F94 File Offset: 0x00003194
		private string SendPostRequest(string json, string path)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://" + Main.server + ".bhvrdbd.com/api/v1" + path);
			httpWebRequest.ServicePoint.Expect100Continue = false;
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Accept = "*/*";
			httpWebRequest.Headers["Accept-Encoding"] = "deflate, gzip";
			httpWebRequest.UserAgent = "DeadByDaylight/++DeadByDaylight+Live-CL-281719 Windows/10.0.18363.1.256.64bit";
			httpWebRequest.ContentType = "application/json";
			Cookie cookie = new Cookie();
			cookie.Name = "bhvrSession";
			cookie.Value = Main.bhvrSession;
			cookie.Domain = Main.server + ".bhvrdbd.com";
			httpWebRequest.CookieContainer = new CookieContainer();
			httpWebRequest.CookieContainer.Add(cookie);
			using (Stream requestStream = httpWebRequest.GetRequestStream())
			{
				using (StreamWriter streamWriter = new StreamWriter(requestStream))
				{
					streamWriter.Write(json);
				}
			}
			string result;
			using (WebResponse response = httpWebRequest.GetResponse())
			{
				using (Stream responseStream = response.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(responseStream))
					{
						result = streamReader.ReadToEnd();
					}
				}
			}
			return result;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00005118 File Offset: 0x00003318
		private void guna2Button7_Click(object sender, EventArgs e)
		{
			string json = "{\"amount\":100000,\"currency\":\"BonusBloodpoints\",\"reason\":\"Sync\"}";
			this.SendPostRequest(json, "/wallet/withdraw");
			new Thread(delegate(object t)
			{
				do
				{
					try
					{
						this.SendGetRequest("/wallet/currencies");
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.ToString());
					}
				}
				while (!this.stopwhile);
				this.stopwhile = false;
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000256C File Offset: 0x0000076C
		private void guna2Button2_Click(object sender, EventArgs e)
		{
			new Thread(delegate(object t)
			{
				this.KillerPlus();
				this.CheckRankAll();
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000258B File Offset: 0x0000078B
		private void editor_Click_1(object sender, EventArgs e)
		{
			Process.Start("Editor.exe");
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002598 File Offset: 0x00000798
		private void guna2Button4_Click(object sender, EventArgs e)
		{
			new Thread(delegate(object t)
			{
				this.SurvivorMinus();
				this.CheckRankAll();
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00005158 File Offset: 0x00003358
		private void guna2Button6_Click(object sender, EventArgs e)
		{
			int total = this.bloodpointi + this.newbloopointi;
			this.stopwhile = false;
			new Thread(delegate(object t)
			{
				while (!this.stopwhile)
				{
					string json = "{\"data\":{\"rewardType\": \"Story\",\"walletToGrant\": {\"balance\":" + new Random().Next(13232, 23123).ToString() + ",\"currency\": \"Bloodpoints\"}}}";
					try
					{
						this.SendPostRequest(json, "/extensions/rewards/grantCurrency/");
						if (total >= 1000000)
						{
							this.stopwhile = true;
							SystemSounds.Asterisk.Play();
						}
					}
					catch
					{
					}
				}
			})
			{
				IsBackground = true
			}.Start();
			new Thread(delegate(object t)
			{
				while (!this.stopwhile)
				{
					try
					{
						this.SendGetRequest("/wallet/currencies");
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.ToString());
					}
				}
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000051C4 File Offset: 0x000033C4
		private void guna2Button5_Click(object sender, EventArgs e)
		{
			string json = "{\"data\":{\"list\":[{\"balance\":999999999,\"currency\":\"BonusBloodpoints\"}]}}";
			try
			{
				this.SendPostRequest(json, "/extensions/wallet/migrateCurrencies");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000025D6 File Offset: 0x000007D6
		static Main()
		{
			Class2.m8ctx7Mzu9Lmx();
			Main.encryptionKey = "5BCC2D6A95D4DF04A005504E59A9B36E";
			Main.epicservers = false;
			Main.server = "steam.live";
		}

		// Token: 0x0400001A RID: 26
		private bool allowbuttons;

		// Token: 0x0400001C RID: 28
		private bool stopwhile;

		// Token: 0x0400001D RID: 29
		private bool stoprank;

		// Token: 0x0400001E RID: 30
		private string currentbp;

		// Token: 0x0400001F RID: 31
		private string bonusbp;

		// Token: 0x04000020 RID: 32
		private int finalbp;

		// Token: 0x04000021 RID: 33
		public const int WM_NCLBUTTONDOWN = 161;

		// Token: 0x04000022 RID: 34
		public const int HTCAPTION = 2;

		// Token: 0x04000023 RID: 35
		public static string bhvrSession;

		// Token: 0x04000024 RID: 36
		private static readonly string encryptionKey;

		// Token: 0x04000025 RID: 37
		public static string krakenStateVersion;

		// Token: 0x04000026 RID: 38
		private int bloodpointi;

		// Token: 0x04000027 RID: 39
		private int newbloopointi;

		// Token: 0x04000028 RID: 40
		public static bool epicservers;

		// Token: 0x04000029 RID: 41
		public static string server;
	}
}
