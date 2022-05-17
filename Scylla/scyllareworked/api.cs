using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace scyllareworked
{
	// Token: 0x0200000C RID: 12
	public class api
	{
		// Token: 0x06000074 RID: 116 RVA: 0x0000A22C File Offset: 0x0000842C
		public api(string name, string ownerid, string secret, string version)
		{
			Class2.m8ctx7Mzu9Lmx();
			this.app_data = new api.app_data_class();
			this.user_data = new api.user_data_class();
			this.response = new api.response_class();
			this.response_decoder = new json_wrapper(new api.response_structure());
			base..ctor();
			if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(ownerid) || string.IsNullOrWhiteSpace(secret) || string.IsNullOrWhiteSpace(version))
			{
				api.error("Application not setup correctly. Please watch video link found in Program.cs");
				Environment.Exit(0);
			}
			this.name = name;
			this.ownerid = ownerid;
			this.secret = secret;
			this.version = version;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000A2C4 File Offset: 0x000084C4
		public void init()
		{
			this.enckey = encryption.sha256(encryption.iv_key());
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("init"));
			nameValueCollection["ver"] = encryption.encrypt(this.version, this.secret, text);
			nameValueCollection["hash"] = api.checksum(Process.GetCurrentProcess().MainModule.FileName);
			nameValueCollection["enckey"] = encryption.encrypt(this.enckey, this.secret, text);
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			if (text2 == "KeyAuth_Invalid")
			{
				api.error("Application not found");
				Environment.Exit(0);
			}
			text2 = encryption.decrypt(text2, this.secret, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				this.load_app_data(response_structure.appinfo);
				this.sessionid = response_structure.sessionid;
				this.initzalized = true;
				return;
			}
			if (response_structure.message == "invalidver")
			{
				Process.Start(response_structure.download);
				Environment.Exit(0);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000A448 File Offset: 0x00008648
		public void register(string username, string pass, string key)
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("register"));
			nameValueCollection["username"] = encryption.encrypt(username, this.enckey, text);
			nameValueCollection["pass"] = encryption.encrypt(pass, this.enckey, text);
			nameValueCollection["key"] = encryption.encrypt(key, this.enckey, text);
			nameValueCollection["hwid"] = encryption.encrypt(value, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				this.load_user_data(response_structure.info);
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000A5B4 File Offset: 0x000087B4
		public void login(string username, string pass)
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("login"));
			nameValueCollection["username"] = encryption.encrypt(username, this.enckey, text);
			nameValueCollection["pass"] = encryption.encrypt(pass, this.enckey, text);
			nameValueCollection["hwid"] = encryption.encrypt(value, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				this.load_user_data(response_structure.info);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000A708 File Offset: 0x00008908
		public void upgrade(string username, string key)
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("upgrade"));
			nameValueCollection["username"] = encryption.encrypt(username, this.enckey, text);
			nameValueCollection["key"] = encryption.encrypt(key, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			response_structure.success = false;
			this.load_response_struct(response_structure);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000A838 File Offset: 0x00008A38
		public void license(string key)
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("license"));
			nameValueCollection["key"] = encryption.encrypt(key, this.enckey, text);
			nameValueCollection["hwid"] = encryption.encrypt(value, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				this.load_user_data(response_structure.info);
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000A974 File Offset: 0x00008B74
		public void setvar(string var, string data)
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("setvar"));
			nameValueCollection["var"] = encryption.encrypt(var, this.enckey, text);
			nameValueCollection["data"] = encryption.encrypt(data, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure data2 = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(data2);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000AA8C File Offset: 0x00008C8C
		public string getvar(string var)
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("getvar"));
			nameValueCollection["var"] = encryption.encrypt(var, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				return response_structure.response;
			}
			return null;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000AB9C File Offset: 0x00008D9C
		public void ban()
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("ban"));
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure data = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(data);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000AC84 File Offset: 0x00008E84
		public string var(string varid)
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("var"));
			nameValueCollection["varid"] = encryption.encrypt(varid, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				return response_structure.message;
			}
			return null;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000ADA4 File Offset: 0x00008FA4
		public List<api.msg> chatget(string channelname)
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("chatget"));
			nameValueCollection["channel"] = encryption.encrypt(channelname, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				return response_structure.messages;
			}
			return null;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000AEB4 File Offset: 0x000090B4
		public bool chatsend(string msg, string channelname)
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("chatsend"));
			nameValueCollection["message"] = encryption.encrypt(msg, this.enckey, text);
			nameValueCollection["channel"] = encryption.encrypt(channelname, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			return response_structure.success;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000AFD8 File Offset: 0x000091D8
		public bool checkblack()
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("checkblacklist"));
			nameValueCollection["hwid"] = encryption.encrypt(value, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			return response_structure.success;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000B0F4 File Offset: 0x000092F4
		public string webhook(string webid, string param, string body = "", string conttype = "")
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
				return null;
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("webhook"));
			nameValueCollection["webid"] = encryption.encrypt(webid, this.enckey, text);
			nameValueCollection["params"] = encryption.encrypt(param, this.enckey, text);
			nameValueCollection["body"] = encryption.encrypt(body, this.enckey, text);
			nameValueCollection["conttype"] = encryption.encrypt(conttype, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				return response_structure.response;
			}
			return null;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000B250 File Offset: 0x00009450
		public byte[] download(string fileid)
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first. File is empty since no request could be made.");
				Environment.Exit(0);
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("file"));
			nameValueCollection["fileid"] = encryption.encrypt(fileid, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			if (response_structure.success)
			{
				return encryption.str_to_byte_arr(response_structure.contents);
			}
			return null;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000B368 File Offset: 0x00009568
		public void log(string message)
		{
			if (!this.initzalized)
			{
				api.error("Please initzalize first");
				Environment.Exit(0);
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("log"));
			nameValueCollection["pcuser"] = encryption.encrypt(Environment.UserName, this.enckey, text);
			nameValueCollection["message"] = encryption.encrypt(message, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			api.req(nameValueCollection);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000B464 File Offset: 0x00009664
		public static string checksum(string filename)
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

		// Token: 0x06000085 RID: 133 RVA: 0x0000B4D4 File Offset: 0x000096D4
		public static void error(string message)
		{
			Process.Start(new ProcessStartInfo("cmd.exe", "/c start cmd /C \"color b && title Error && echo " + message + " && timeout /t 5\"")
			{
				CreateNoWindow = true,
				RedirectStandardOutput = true,
				RedirectStandardError = true,
				UseShellExecute = false
			});
			Environment.Exit(0);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000B524 File Offset: 0x00009724
		private static string req(NameValueCollection post_data)
		{
			string result;
			try
			{
				using (WebClient webClient = new WebClient())
				{
					byte[] bytes = webClient.UploadValues("https://keyauth.win/api/1.0/", post_data);
					result = Encoding.Default.GetString(bytes);
				}
			}
			catch (WebException ex)
			{
				if (((HttpWebResponse)ex.Response).StatusCode == (HttpStatusCode)429)
				{
					api.error("You're connecting too fast to loader, slow down.");
					Environment.Exit(0);
					result = "";
				}
				else
				{
					api.error("Connection failure. Please try again, or contact us for help.");
					Environment.Exit(0);
					result = "";
				}
			}
			return result;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000B5C0 File Offset: 0x000097C0
		private void load_app_data(api.app_data_structure data)
		{
			this.app_data.numUsers = data.numUsers;
			this.app_data.numOnlineUsers = data.numOnlineUsers;
			this.app_data.numKeys = data.numKeys;
			this.app_data.version = data.version;
			this.app_data.customerPanelLink = data.customerPanelLink;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000B624 File Offset: 0x00009824
		private void load_user_data(api.user_data_structure data)
		{
			this.user_data.username = data.username;
			this.user_data.ip = data.ip;
			this.user_data.hwid = data.hwid;
			this.user_data.createdate = data.createdate;
			this.user_data.lastlogin = data.lastlogin;
			this.user_data.subscriptions = data.subscriptions;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000026FA File Offset: 0x000008FA
		private void load_response_struct(api.response_structure data)
		{
			this.response.success = data.success;
			this.response.message = data.message;
		}

		// Token: 0x04000072 RID: 114
		public string name;

		// Token: 0x04000073 RID: 115
		public string ownerid;

		// Token: 0x04000074 RID: 116
		public string secret;

		// Token: 0x04000075 RID: 117
		public string version;

		// Token: 0x04000076 RID: 118
		private string sessionid;

		// Token: 0x04000077 RID: 119
		private string enckey;

		// Token: 0x04000078 RID: 120
		private bool initzalized;

		// Token: 0x04000079 RID: 121
		public api.app_data_class app_data;

		// Token: 0x0400007A RID: 122
		public api.user_data_class user_data;

		// Token: 0x0400007B RID: 123
		public api.response_class response;

		// Token: 0x0400007C RID: 124
		private json_wrapper response_decoder;

		// Token: 0x0200000D RID: 13
		[DataContract]
		private class response_structure
		{
			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600008A RID: 138 RVA: 0x0000271E File Offset: 0x0000091E
			// (set) Token: 0x0600008B RID: 139 RVA: 0x00002726 File Offset: 0x00000926
			[DataMember]
			public bool success { get; set; }

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x0600008C RID: 140 RVA: 0x0000272F File Offset: 0x0000092F
			// (set) Token: 0x0600008D RID: 141 RVA: 0x00002737 File Offset: 0x00000937
			[DataMember]
			public string sessionid { get; set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x0600008E RID: 142 RVA: 0x00002740 File Offset: 0x00000940
			// (set) Token: 0x0600008F RID: 143 RVA: 0x00002748 File Offset: 0x00000948
			[DataMember]
			public string contents { get; set; }

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000090 RID: 144 RVA: 0x00002751 File Offset: 0x00000951
			// (set) Token: 0x06000091 RID: 145 RVA: 0x00002759 File Offset: 0x00000959
			[DataMember]
			public string response { get; set; }

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000092 RID: 146 RVA: 0x00002762 File Offset: 0x00000962
			// (set) Token: 0x06000093 RID: 147 RVA: 0x0000276A File Offset: 0x0000096A
			[DataMember]
			public string message { get; set; }

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000094 RID: 148 RVA: 0x00002773 File Offset: 0x00000973
			// (set) Token: 0x06000095 RID: 149 RVA: 0x0000277B File Offset: 0x0000097B
			[DataMember]
			public string download { get; set; }

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x06000096 RID: 150 RVA: 0x00002784 File Offset: 0x00000984
			// (set) Token: 0x06000097 RID: 151 RVA: 0x0000278C File Offset: 0x0000098C
			[DataMember(IsRequired = false, EmitDefaultValue = false)]
			public api.user_data_structure info { get; set; }

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x06000098 RID: 152 RVA: 0x00002795 File Offset: 0x00000995
			// (set) Token: 0x06000099 RID: 153 RVA: 0x0000279D File Offset: 0x0000099D
			[DataMember(IsRequired = false, EmitDefaultValue = false)]
			public api.app_data_structure appinfo { get; set; }

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x0600009A RID: 154 RVA: 0x000027A6 File Offset: 0x000009A6
			// (set) Token: 0x0600009B RID: 155 RVA: 0x000027AE File Offset: 0x000009AE
			[DataMember]
			public List<api.msg> messages { get; set; }

			// Token: 0x0600009C RID: 156 RVA: 0x00002683 File Offset: 0x00000883
			public response_structure()
			{
				Class2.m8ctx7Mzu9Lmx();
				base..ctor();
			}
		}

		// Token: 0x0200000E RID: 14
		public class msg
		{
			// Token: 0x17000010 RID: 16
			// (get) Token: 0x0600009D RID: 157 RVA: 0x000027B7 File Offset: 0x000009B7
			// (set) Token: 0x0600009E RID: 158 RVA: 0x000027BF File Offset: 0x000009BF
			public string message { get; set; }

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x0600009F RID: 159 RVA: 0x000027C8 File Offset: 0x000009C8
			// (set) Token: 0x060000A0 RID: 160 RVA: 0x000027D0 File Offset: 0x000009D0
			public string author { get; set; }

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x060000A1 RID: 161 RVA: 0x000027D9 File Offset: 0x000009D9
			// (set) Token: 0x060000A2 RID: 162 RVA: 0x000027E1 File Offset: 0x000009E1
			public string timestamp { get; set; }

			// Token: 0x060000A3 RID: 163 RVA: 0x00002683 File Offset: 0x00000883
			public msg()
			{
				Class2.m8ctx7Mzu9Lmx();
				base..ctor();
			}
		}

		// Token: 0x0200000F RID: 15
		[DataContract]
		private class user_data_structure
		{
			// Token: 0x17000013 RID: 19
			// (get) Token: 0x060000A4 RID: 164 RVA: 0x000027EA File Offset: 0x000009EA
			// (set) Token: 0x060000A5 RID: 165 RVA: 0x000027F2 File Offset: 0x000009F2
			[DataMember]
			public string username { get; set; }

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x060000A6 RID: 166 RVA: 0x000027FB File Offset: 0x000009FB
			// (set) Token: 0x060000A7 RID: 167 RVA: 0x00002803 File Offset: 0x00000A03
			[DataMember]
			public string ip { get; set; }

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x060000A8 RID: 168 RVA: 0x0000280C File Offset: 0x00000A0C
			// (set) Token: 0x060000A9 RID: 169 RVA: 0x00002814 File Offset: 0x00000A14
			[DataMember]
			public string hwid { get; set; }

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x060000AA RID: 170 RVA: 0x0000281D File Offset: 0x00000A1D
			// (set) Token: 0x060000AB RID: 171 RVA: 0x00002825 File Offset: 0x00000A25
			[DataMember]
			public string createdate { get; set; }

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x060000AC RID: 172 RVA: 0x0000282E File Offset: 0x00000A2E
			// (set) Token: 0x060000AD RID: 173 RVA: 0x00002836 File Offset: 0x00000A36
			[DataMember]
			public string lastlogin { get; set; }

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x060000AE RID: 174 RVA: 0x0000283F File Offset: 0x00000A3F
			// (set) Token: 0x060000AF RID: 175 RVA: 0x00002847 File Offset: 0x00000A47
			[DataMember]
			public List<api.Data> subscriptions { get; set; }

			// Token: 0x060000B0 RID: 176 RVA: 0x00002683 File Offset: 0x00000883
			public user_data_structure()
			{
				Class2.m8ctx7Mzu9Lmx();
				base..ctor();
			}
		}

		// Token: 0x02000010 RID: 16
		[DataContract]
		private class app_data_structure
		{
			// Token: 0x17000019 RID: 25
			// (get) Token: 0x060000B1 RID: 177 RVA: 0x00002850 File Offset: 0x00000A50
			// (set) Token: 0x060000B2 RID: 178 RVA: 0x00002858 File Offset: 0x00000A58
			[DataMember]
			public string numUsers { get; set; }

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x060000B3 RID: 179 RVA: 0x00002861 File Offset: 0x00000A61
			// (set) Token: 0x060000B4 RID: 180 RVA: 0x00002869 File Offset: 0x00000A69
			[DataMember]
			public string numOnlineUsers { get; set; }

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x060000B5 RID: 181 RVA: 0x00002872 File Offset: 0x00000A72
			// (set) Token: 0x060000B6 RID: 182 RVA: 0x0000287A File Offset: 0x00000A7A
			[DataMember]
			public string numKeys { get; set; }

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x060000B7 RID: 183 RVA: 0x00002883 File Offset: 0x00000A83
			// (set) Token: 0x060000B8 RID: 184 RVA: 0x0000288B File Offset: 0x00000A8B
			[DataMember]
			public string version { get; set; }

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x060000B9 RID: 185 RVA: 0x00002894 File Offset: 0x00000A94
			// (set) Token: 0x060000BA RID: 186 RVA: 0x0000289C File Offset: 0x00000A9C
			[DataMember]
			public string customerPanelLink { get; set; }

			// Token: 0x060000BB RID: 187 RVA: 0x00002683 File Offset: 0x00000883
			public app_data_structure()
			{
				Class2.m8ctx7Mzu9Lmx();
				base..ctor();
			}
		}

		// Token: 0x02000011 RID: 17
		public class app_data_class
		{
			// Token: 0x1700001E RID: 30
			// (get) Token: 0x060000BC RID: 188 RVA: 0x000028A5 File Offset: 0x00000AA5
			// (set) Token: 0x060000BD RID: 189 RVA: 0x000028AD File Offset: 0x00000AAD
			public string numUsers { get; set; }

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x060000BE RID: 190 RVA: 0x000028B6 File Offset: 0x00000AB6
			// (set) Token: 0x060000BF RID: 191 RVA: 0x000028BE File Offset: 0x00000ABE
			public string numOnlineUsers { get; set; }

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x060000C0 RID: 192 RVA: 0x000028C7 File Offset: 0x00000AC7
			// (set) Token: 0x060000C1 RID: 193 RVA: 0x000028CF File Offset: 0x00000ACF
			public string numKeys { get; set; }

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x060000C2 RID: 194 RVA: 0x000028D8 File Offset: 0x00000AD8
			// (set) Token: 0x060000C3 RID: 195 RVA: 0x000028E0 File Offset: 0x00000AE0
			public string version { get; set; }

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x060000C4 RID: 196 RVA: 0x000028E9 File Offset: 0x00000AE9
			// (set) Token: 0x060000C5 RID: 197 RVA: 0x000028F1 File Offset: 0x00000AF1
			public string customerPanelLink { get; set; }

			// Token: 0x060000C6 RID: 198 RVA: 0x00002683 File Offset: 0x00000883
			public app_data_class()
			{
				Class2.m8ctx7Mzu9Lmx();
				base..ctor();
			}
		}

		// Token: 0x02000012 RID: 18
		public class user_data_class
		{
			// Token: 0x17000023 RID: 35
			// (get) Token: 0x060000C7 RID: 199 RVA: 0x000028FA File Offset: 0x00000AFA
			// (set) Token: 0x060000C8 RID: 200 RVA: 0x00002902 File Offset: 0x00000B02
			public string username { get; set; }

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000290B File Offset: 0x00000B0B
			// (set) Token: 0x060000CA RID: 202 RVA: 0x00002913 File Offset: 0x00000B13
			public string ip { get; set; }

			// Token: 0x17000025 RID: 37
			// (get) Token: 0x060000CB RID: 203 RVA: 0x0000291C File Offset: 0x00000B1C
			// (set) Token: 0x060000CC RID: 204 RVA: 0x00002924 File Offset: 0x00000B24
			public string hwid { get; set; }

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x060000CD RID: 205 RVA: 0x0000292D File Offset: 0x00000B2D
			// (set) Token: 0x060000CE RID: 206 RVA: 0x00002935 File Offset: 0x00000B35
			public string createdate { get; set; }

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x060000CF RID: 207 RVA: 0x0000293E File Offset: 0x00000B3E
			// (set) Token: 0x060000D0 RID: 208 RVA: 0x00002946 File Offset: 0x00000B46
			public string lastlogin { get; set; }

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000294F File Offset: 0x00000B4F
			// (set) Token: 0x060000D2 RID: 210 RVA: 0x00002957 File Offset: 0x00000B57
			public List<api.Data> subscriptions { get; set; }

			// Token: 0x060000D3 RID: 211 RVA: 0x00002683 File Offset: 0x00000883
			public user_data_class()
			{
				Class2.m8ctx7Mzu9Lmx();
				base..ctor();
			}
		}

		// Token: 0x02000013 RID: 19
		public class Data
		{
			// Token: 0x17000029 RID: 41
			// (get) Token: 0x060000D4 RID: 212 RVA: 0x00002960 File Offset: 0x00000B60
			// (set) Token: 0x060000D5 RID: 213 RVA: 0x00002968 File Offset: 0x00000B68
			public string subscription { get; set; }

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x060000D6 RID: 214 RVA: 0x00002971 File Offset: 0x00000B71
			// (set) Token: 0x060000D7 RID: 215 RVA: 0x00002979 File Offset: 0x00000B79
			public string expiry { get; set; }

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x060000D8 RID: 216 RVA: 0x00002982 File Offset: 0x00000B82
			// (set) Token: 0x060000D9 RID: 217 RVA: 0x0000298A File Offset: 0x00000B8A
			public string timeleft { get; set; }

			// Token: 0x060000DA RID: 218 RVA: 0x00002683 File Offset: 0x00000883
			public Data()
			{
				Class2.m8ctx7Mzu9Lmx();
				base..ctor();
			}
		}

		// Token: 0x02000014 RID: 20
		public class response_class
		{
			// Token: 0x1700002C RID: 44
			// (get) Token: 0x060000DB RID: 219 RVA: 0x00002993 File Offset: 0x00000B93
			// (set) Token: 0x060000DC RID: 220 RVA: 0x0000299B File Offset: 0x00000B9B
			public bool success { get; set; }

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x060000DD RID: 221 RVA: 0x000029A4 File Offset: 0x00000BA4
			// (set) Token: 0x060000DE RID: 222 RVA: 0x000029AC File Offset: 0x00000BAC
			public string message { get; set; }

			// Token: 0x060000DF RID: 223 RVA: 0x00002683 File Offset: 0x00000883
			public response_class()
			{
				Class2.m8ctx7Mzu9Lmx();
				base..ctor();
			}
		}
	}
}
