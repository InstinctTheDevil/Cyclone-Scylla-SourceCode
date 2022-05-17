namespace scyllareworked
{
	// Token: 0x02000005 RID: 5
	public partial class Login : global::System.Windows.Forms.Form
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002369 File Offset: 0x00000569
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000319C File Offset: 0x0000139C
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::scyllareworked.Login));
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.label4 = new global::System.Windows.Forms.Label();
			this.pictureBox1 = new global::System.Windows.Forms.PictureBox();
			this.guna2Button1 = new global::Guna.UI2.WinForms.Guna2Button();
			this.username = new global::Guna.UI2.WinForms.Guna2TextBox();
			this.password = new global::Guna.UI2.WinForms.Guna2TextBox();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.guna2Panel_0 = new global::Guna.UI2.WinForms.Guna2Panel();
			this.label1 = new global::System.Windows.Forms.Label();
			this.label13 = new global::System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			this.guna2Panel_0.SuspendLayout();
			base.SuspendLayout();
			this.panel1.BackColor = global::System.Drawing.Color.FromArgb(40, 52, 120);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Controls.Add(this.guna2Button1);
			this.panel1.Controls.Add(this.username);
			this.panel1.Controls.Add(this.password);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Location = new global::System.Drawing.Point(0, 13);
			this.panel1.Margin = new global::System.Windows.Forms.Padding(4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(299, 323);
			this.panel1.TabIndex = 0;
			this.panel1.Paint += new global::System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			this.label4.Anchor = (global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.label4.AutoSize = true;
			this.label4.BackColor = global::System.Drawing.Color.Transparent;
			this.label4.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.label4.Font = new global::System.Drawing.Font("Comfortaa", 20.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label4.ForeColor = global::System.Drawing.Color.FromArgb(68, 84, 175);
			this.label4.Location = new global::System.Drawing.Point(122, 17);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(103, 44);
			this.label4.TabIndex = 19;
			this.label4.Text = "Scylla";
			this.label4.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.pictureBox1.Anchor = (global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.pictureBox1.BackColor = global::System.Drawing.Color.Transparent;
			this.pictureBox1.BackgroundImage = global::scyllareworked.Properties.Resources.IconItems_flashlight1;
			this.pictureBox1.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Stretch;
			this.pictureBox1.Image = global::scyllareworked.Properties.Resources.IconItems_flashlight;
			this.pictureBox1.Location = new global::System.Drawing.Point(78, 16);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new global::System.Drawing.Size(50, 50);
			this.pictureBox1.TabIndex = 18;
			this.pictureBox1.TabStop = false;
			this.guna2Button1.Animated = true;
			this.guna2Button1.AutoRoundedCorners = true;
			this.guna2Button1.BackColor = global::System.Drawing.Color.FromArgb(40, 52, 120);
			this.guna2Button1.BorderColor = global::System.Drawing.Color.FromArgb(68, 84, 175);
			this.guna2Button1.BorderRadius = 15;
			this.guna2Button1.BorderThickness = 1;
			this.guna2Button1.CheckedState.Parent = this.guna2Button1;
			this.guna2Button1.CustomImages.Parent = this.guna2Button1;
			this.guna2Button1.FillColor = global::System.Drawing.Color.FromArgb(68, 84, 175);
			this.guna2Button1.Font = new global::System.Drawing.Font("Comfortaa", 8.999999f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.guna2Button1.ForeColor = global::System.Drawing.Color.FromArgb(102, 110, 159);
			this.guna2Button1.HoverState.Parent = this.guna2Button1;
			this.guna2Button1.Location = new global::System.Drawing.Point(26, 237);
			this.guna2Button1.Name = "guna2Button1";
			this.guna2Button1.ShadowDecoration.Parent = this.guna2Button1;
			this.guna2Button1.Size = new global::System.Drawing.Size(247, 33);
			this.guna2Button1.TabIndex = 16;
			this.guna2Button1.Text = "Login";
			this.guna2Button1.Click += new global::System.EventHandler(this.guna2Button1_Click);
			this.username.Anchor = (global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.username.Animated = true;
			this.username.AutoRoundedCorners = true;
			this.username.BackColor = global::System.Drawing.Color.Transparent;
			this.username.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.None;
			this.username.BorderColor = global::System.Drawing.Color.FromArgb(40, 52, 120);
			this.username.BorderRadius = 14;
			this.username.Cursor = global::System.Windows.Forms.Cursors.IBeam;
			this.username.DefaultText = "";
			this.username.DisabledState.BorderColor = global::System.Drawing.Color.FromArgb(208, 208, 208);
			this.username.DisabledState.FillColor = global::System.Drawing.Color.FromArgb(226, 226, 226);
			this.username.DisabledState.ForeColor = global::System.Drawing.Color.FromArgb(138, 138, 138);
			this.username.DisabledState.Parent = this.username;
			this.username.DisabledState.PlaceholderForeColor = global::System.Drawing.Color.FromArgb(138, 138, 138);
			this.username.FillColor = global::System.Drawing.Color.FromArgb(68, 84, 175);
			this.username.FocusedState.BorderColor = global::System.Drawing.Color.FromArgb(28, 40, 108);
			this.username.FocusedState.Parent = this.username;
			this.username.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.username.ForeColor = global::System.Drawing.Color.White;
			this.username.HoverState.BorderColor = global::System.Drawing.Color.RoyalBlue;
			this.username.HoverState.Parent = this.username;
			this.username.Location = new global::System.Drawing.Point(26, 110);
			this.username.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.username.Name = "username";
			this.username.PasswordChar = '\0';
			this.username.PlaceholderForeColor = global::System.Drawing.Color.Black;
			this.username.PlaceholderText = "";
			this.username.SelectedText = "";
			this.username.ShadowDecoration.Parent = this.username;
			this.username.Size = new global::System.Drawing.Size(247, 31);
			this.username.TabIndex = 6;
			this.password.Anchor = (global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.password.Animated = true;
			this.password.AutoRoundedCorners = true;
			this.password.BackColor = global::System.Drawing.Color.Transparent;
			this.password.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.None;
			this.password.BorderColor = global::System.Drawing.Color.FromArgb(40, 52, 120);
			this.password.BorderRadius = 14;
			this.password.Cursor = global::System.Windows.Forms.Cursors.IBeam;
			this.password.DefaultText = "";
			this.password.DisabledState.BorderColor = global::System.Drawing.Color.FromArgb(208, 208, 208);
			this.password.DisabledState.FillColor = global::System.Drawing.Color.FromArgb(226, 226, 226);
			this.password.DisabledState.ForeColor = global::System.Drawing.Color.FromArgb(138, 138, 138);
			this.password.DisabledState.Parent = this.password;
			this.password.DisabledState.PlaceholderForeColor = global::System.Drawing.Color.FromArgb(138, 138, 138);
			this.password.FillColor = global::System.Drawing.Color.FromArgb(68, 84, 175);
			this.password.FocusedState.BorderColor = global::System.Drawing.Color.FromArgb(28, 40, 108);
			this.password.FocusedState.Parent = this.password;
			this.password.Font = new global::System.Drawing.Font("Arial", 8.25f, global::System.Drawing.FontStyle.Italic);
			this.password.ForeColor = global::System.Drawing.Color.White;
			this.password.HoverState.BorderColor = global::System.Drawing.Color.RoyalBlue;
			this.password.HoverState.Parent = this.password;
			this.password.Location = new global::System.Drawing.Point(26, 189);
			this.password.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.password.Name = "password";
			this.password.PasswordChar = '\0';
			this.password.PlaceholderForeColor = global::System.Drawing.Color.FromArgb(78, 78, 78);
			this.password.PlaceholderText = "";
			this.password.SelectedText = "";
			this.password.ShadowDecoration.Parent = this.password;
			this.password.Size = new global::System.Drawing.Size(247, 31);
			this.password.TabIndex = 5;
			this.password.UseSystemPasswordChar = true;
			this.label3.AutoSize = true;
			this.label3.Font = new global::System.Drawing.Font("Comfortaa", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label3.ForeColor = global::System.Drawing.SystemColors.Control;
			this.label3.Location = new global::System.Drawing.Point(22, 153);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(73, 21);
			this.label3.TabIndex = 3;
			this.label3.Text = "Password";
			this.label2.AutoSize = true;
			this.label2.Font = new global::System.Drawing.Font("Comfortaa", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label2.ForeColor = global::System.Drawing.SystemColors.Control;
			this.label2.Location = new global::System.Drawing.Point(22, 74);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(80, 21);
			this.label2.TabIndex = 1;
			this.label2.Text = "Username";
			this.guna2Panel_0.BackColor = global::System.Drawing.Color.Transparent;
			this.guna2Panel_0.BorderColor = global::System.Drawing.Color.FromArgb(28, 40, 108);
			this.guna2Panel_0.BorderThickness = 1;
			this.guna2Panel_0.Controls.Add(this.label1);
			this.guna2Panel_0.Controls.Add(this.label13);
			this.guna2Panel_0.CustomBorderColor = global::System.Drawing.Color.Transparent;
			this.guna2Panel_0.FillColor = global::System.Drawing.Color.FromArgb(28, 40, 108);
			this.guna2Panel_0.Location = new global::System.Drawing.Point(0, -1);
			this.guna2Panel_0.Name = "guna2Panel1";
			this.guna2Panel_0.ShadowDecoration.Parent = this.guna2Panel_0;
			this.guna2Panel_0.Size = new global::System.Drawing.Size(299, 28);
			this.guna2Panel_0.TabIndex = 1;
			this.label1.Anchor = (global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.label1.Font = new global::System.Drawing.Font("Comfortaa", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.ForeColor = global::System.Drawing.Color.FromArgb(57, 72, 158);
			this.label1.Location = new global::System.Drawing.Point(1, 0);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(298, 26);
			this.label1.TabIndex = 21;
			this.label1.Text = "Login Window";
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.label13.Anchor = (global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.label13.Font = new global::System.Drawing.Font("Comfortaa", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label13.ForeColor = global::System.Drawing.Color.FromArgb(57, 72, 158);
			this.label13.Location = new global::System.Drawing.Point(1, 20);
			this.label13.Name = "label13";
			this.label13.Size = new global::System.Drawing.Size(298, 26);
			this.label13.TabIndex = 20;
			this.label13.Text = "Login";
			this.label13.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(8f, 19f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.Color.FromArgb(40, 52, 120);
			base.ClientSize = new global::System.Drawing.Size(298, 335);
			base.ControlBox = false;
			base.Controls.Add(this.guna2Panel_0);
			base.Controls.Add(this.panel1);
			this.Font = new global::System.Drawing.Font("Comfortaa", 8.999999f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Margin = new global::System.Windows.Forms.Padding(4);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Login";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Login";
			base.Load += new global::System.EventHandler(this.Login_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			this.guna2Panel_0.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x0400000E RID: 14
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400000F RID: 15
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000010 RID: 16
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000011 RID: 17
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000012 RID: 18
		private global::Guna.UI2.WinForms.Guna2TextBox password;

		// Token: 0x04000013 RID: 19
		public global::Guna.UI2.WinForms.Guna2TextBox username;

		// Token: 0x04000014 RID: 20
		private global::Guna.UI2.WinForms.Guna2Button guna2Button1;

		// Token: 0x04000015 RID: 21
		private global::Guna.UI2.WinForms.Guna2Panel guna2Panel_0;

		// Token: 0x04000016 RID: 22
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000017 RID: 23
		private global::System.Windows.Forms.PictureBox pictureBox1;

		// Token: 0x04000018 RID: 24
		private global::System.Windows.Forms.Label label13;

		// Token: 0x04000019 RID: 25
		private global::System.Windows.Forms.Label label1;
	}
}
