using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Microsoft.Win32;
using scyllareworked.Properties;

namespace scyllareworked
{
	// Token: 0x02000005 RID: 5
	public partial class Login : Form
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000022DC File Offset: 0x000004DC
		public Login()
		{
			Class2.m8ctx7Mzu9Lmx();
			base..ctor();
			this.InitializeComponent();
			base.FormBorderStyle = FormBorderStyle.None;
			base.Region = Region.FromHrgn(Login.CreateRoundRectRgn(0, 0, base.Width, base.Height, 20, 20));
		}

		// Token: 0x06000010 RID: 16
		[DllImport("Gdi32.dll")]
		private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

		// Token: 0x06000011 RID: 17
		[DllImport("User32.dll")]
		public static extern bool ReleaseCapture();

		// Token: 0x06000012 RID: 18
		[DllImport("User32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

		// Token: 0x06000013 RID: 19 RVA: 0x00003058 File Offset: 0x00001258
		private void Login_Load(object sender, EventArgs e)
		{
			Login.KeyAuthApp.init();
			base.MouseDown += this.Form_MouseDown;
			try
			{
				if (Registry.CurrentUser.OpenSubKey("Scylla by Flux") != null)
				{
					RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Scylla by Flux");
					object value = registryKey.GetValue("Username Scylla");
					object value2 = registryKey.GetValue("Password Scylla");
					registryKey.Close();
					this.username.Text = value.ToString();
					this.password.Text = value2.ToString();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002318 File Offset: 0x00000518
		private void Form_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				Login.ReleaseCapture();
				Login.SendMessage(base.Handle, 161, 2, 0);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002340 File Offset: 0x00000540
		private void panel1_Paint(object sender, PaintEventArgs e)
		{
			this.panel1.Region = Region.FromHrgn(Login.CreateRoundRectRgn(0, 0, 272, 410, 10, 10));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002367 File Offset: 0x00000567
		private void loginbtn_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000030F8 File Offset: 0x000012F8
		private void guna2Button1_Click(object sender, EventArgs e)
		{
			Login.KeyAuthApp.login(this.username.Text, this.password.Text);
			if (Login.KeyAuthApp.response.success)
			{
				new Main().Show();
				base.Hide();
				RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Scylla by Flux");
				registryKey.SetValue("Username Scylla", this.username.Text);
				registryKey.SetValue("Password Scylla", this.password.Text);
				registryKey.Close();
				return;
			}
			MessageBox.Show(Login.KeyAuthApp.response.message);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002367 File Offset: 0x00000567
		private void registerbtn_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003FC8 File Offset: 0x000021C8
		static Login()
		{
			Class2.m8ctx7Mzu9Lmx();
			Login.name = "scylla";
			Login.ownerid = "I3xvWRKdLi";
			Login.secret = "fc82b3ff5768965b5076584f5c7da95b7ebb3f87104c7031a1180a5ae19ab410";
			Login.version = "1.0";
			Login.KeyAuthApp = new api(Login.name, Login.ownerid, Login.secret, Login.version);
		}

		// Token: 0x04000006 RID: 6
		private static string name;

		// Token: 0x04000007 RID: 7
		private static string ownerid;

		// Token: 0x04000008 RID: 8
		private static string secret;

		// Token: 0x04000009 RID: 9
		private static string version;

		// Token: 0x0400000A RID: 10
		public static string uzivatel;

		// Token: 0x0400000B RID: 11
		public static api KeyAuthApp;

		// Token: 0x0400000C RID: 12
		public const int WM_NCLBUTTONDOWN = 161;

		// Token: 0x0400000D RID: 13
		public const int HTCAPTION = 2;
	}
}
