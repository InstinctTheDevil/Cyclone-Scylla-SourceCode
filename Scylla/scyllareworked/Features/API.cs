using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace scyllareworked.Features
{
	// Token: 0x02000022 RID: 34
	internal class API
	{
		// Token: 0x0600013B RID: 315 RVA: 0x0000C3AC File Offset: 0x0000A5AC
		public static void Log(string username, string action)
		{
			if (!Constants.Initialized)
			{
				MessageBox.Show("Please initialize your application first!");
				Process.GetCurrentProcess().Kill();
			}
			if (string.IsNullOrWhiteSpace(action))
			{
				MessageBox.Show("Missing log information!");
				Process.GetCurrentProcess().Kill();
			}
			using (WebClient webClient = new WebClient())
			{
				try
				{
					Security.Start();
					webClient.Proxy = null;
					Encoding @default = Encoding.Default;
					WebClient webClient2 = webClient;
					string apiUrl = Constants.ApiUrl;
					NameValueCollection nameValueCollection = new NameValueCollection();
					nameValueCollection["token"] = Encryption.EncryptService(Constants.Token);
					nameValueCollection["aid"] = Encryption.smethod_0(OnProgramStart.AID);
					nameValueCollection["username"] = Encryption.smethod_0(username);
					nameValueCollection["pcuser"] = Encryption.smethod_0(Environment.UserName);
					nameValueCollection["session_id"] = Constants.IV;
					nameValueCollection["api_id"] = Constants.APIENCRYPTSALT;
					nameValueCollection["api_key"] = Constants.APIENCRYPTKEY;
					nameValueCollection["data"] = Encryption.smethod_0(action);
					nameValueCollection["session_key"] = Constants.Key;
					nameValueCollection["secret"] = Encryption.smethod_0(OnProgramStart.Secret);
					nameValueCollection["type"] = Encryption.smethod_0("log");
					Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
					Security.End();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					Process.GetCurrentProcess().Kill();
				}
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public static bool AIO(string AIO)
		{
			return API.AIOLogin(AIO) || API.smethod_0(AIO);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000C55C File Offset: 0x0000A75C
		public static bool AIOLogin(string AIO)
		{
			if (!Constants.Initialized)
			{
				MessageBox.Show("Please initialize your application first!");
				Process.GetCurrentProcess().Kill();
			}
			if (string.IsNullOrWhiteSpace(AIO))
			{
				MessageBox.Show("Missing user login information!");
				Process.GetCurrentProcess().Kill();
			}
			string[] array = new string[0];
			bool result;
			using (WebClient webClient = new WebClient())
			{
				try
				{
					Security.Start();
					webClient.Proxy = null;
					Encoding @default = Encoding.Default;
					WebClient webClient2 = webClient;
					string apiUrl = Constants.ApiUrl;
					NameValueCollection nameValueCollection = new NameValueCollection();
					nameValueCollection["token"] = Encryption.EncryptService(Constants.Token);
					nameValueCollection["timestamp"] = Encryption.EncryptService(DateTime.Now.ToString());
					nameValueCollection["aid"] = Encryption.smethod_0(OnProgramStart.AID);
					nameValueCollection["session_id"] = Constants.IV;
					nameValueCollection["api_id"] = Constants.APIENCRYPTSALT;
					nameValueCollection["api_key"] = Constants.APIENCRYPTKEY;
					nameValueCollection["username"] = Encryption.smethod_0(AIO);
					nameValueCollection["password"] = Encryption.smethod_0(AIO);
					nameValueCollection["hwid"] = Encryption.smethod_0(Constants.HWID());
					nameValueCollection["session_key"] = Constants.Key;
					nameValueCollection["secret"] = Encryption.smethod_0(OnProgramStart.Secret);
					nameValueCollection["type"] = Encryption.smethod_0("login");
					array = Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
					if (array[0] != Constants.Token)
					{
						MessageBox.Show("Security error has been triggered!");
						Process.GetCurrentProcess().Kill();
					}
					if (Security.MaliciousCheck(array[1]))
					{
						MessageBox.Show("Possible malicious activity detected!");
						Process.GetCurrentProcess().Kill();
					}
					if (Constants.Breached)
					{
						MessageBox.Show("Possible malicious activity detected!");
						Process.GetCurrentProcess().Kill();
					}
					string a = array[2];
					if (a == "success")
					{
						Security.End();
						User.ID = array[3];
						User.Username = array[4];
						User.Password = array[5];
						User.Email = array[6];
						User.HWID = array[7];
						User.UserVariable = array[8];
						User.Rank = array[9];
						User.IP = array[10];
						User.Expiry = array[11];
						User.LastLogin = array[12];
						User.RegisterDate = array[13];
						string[] array2 = array[14].Split(new char[]
						{
							'~'
						});
						for (int i = 0; i < array2.Length; i++)
						{
							string[] array3 = array2[i].Split(new char[]
							{
								'^'
							});
							try
							{
								App.Variables.Add(array3[0], array3[1]);
							}
							catch
							{
							}
						}
						return true;
					}
					if (a == "invalid_details")
					{
						Security.End();
						return false;
					}
					if (a == "time_expired")
					{
						MessageBox.Show("Your subscription has expired!");
						Security.End();
						return false;
					}
					if (a == "hwid_updated")
					{
						MessageBox.Show("New machine has been binded, re-open the application!");
						Security.End();
						return false;
					}
					if (a == "invalid_hwid")
					{
						MessageBox.Show("This user is binded to another computer, please contact support!");
						Security.End();
						return false;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					Security.End();
					Process.GetCurrentProcess().Kill();
				}
				result = false;
			}
			return result;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000C90C File Offset: 0x0000AB0C
		public static bool smethod_0(string AIO)
		{
			if (!Constants.Initialized)
			{
				MessageBox.Show("Please initialize your application first!");
				Security.End();
				Process.GetCurrentProcess().Kill();
			}
			if (string.IsNullOrWhiteSpace(AIO))
			{
				MessageBox.Show("Invalid registrar information!");
				Process.GetCurrentProcess().Kill();
			}
			bool result;
			using (WebClient webClient = new WebClient())
			{
				try
				{
					Security.Start();
					webClient.Proxy = null;
					Encoding @default = Encoding.Default;
					WebClient webClient2 = webClient;
					string apiUrl = Constants.ApiUrl;
					NameValueCollection nameValueCollection = new NameValueCollection();
					nameValueCollection["token"] = Encryption.EncryptService(Constants.Token);
					nameValueCollection["timestamp"] = Encryption.EncryptService(DateTime.Now.ToString());
					nameValueCollection["aid"] = Encryption.smethod_0(OnProgramStart.AID);
					nameValueCollection["session_id"] = Constants.IV;
					nameValueCollection["api_id"] = Constants.APIENCRYPTSALT;
					nameValueCollection["api_key"] = Constants.APIENCRYPTKEY;
					nameValueCollection["session_key"] = Constants.Key;
					nameValueCollection["secret"] = Encryption.smethod_0(OnProgramStart.Secret);
					nameValueCollection["type"] = Encryption.smethod_0("register");
					nameValueCollection["username"] = Encryption.smethod_0(AIO);
					nameValueCollection["password"] = Encryption.smethod_0(AIO);
					nameValueCollection["email"] = Encryption.smethod_0(AIO);
					nameValueCollection["license"] = Encryption.smethod_0(AIO);
					nameValueCollection["hwid"] = Encryption.smethod_0(Constants.HWID());
					string[] array = Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
					if (array[0] != Constants.Token)
					{
						MessageBox.Show("Security error has been triggered!");
						Security.End();
						Process.GetCurrentProcess().Kill();
					}
					if (Security.MaliciousCheck(array[1]))
					{
						MessageBox.Show("Possible malicious activity detected!");
						Process.GetCurrentProcess().Kill();
					}
					if (Constants.Breached)
					{
						MessageBox.Show("Possible malicious activity detected!");
						Process.GetCurrentProcess().Kill();
					}
					Security.End();
					string a = array[2];
					if (a == "success")
					{
						return true;
					}
					if (a == "error")
					{
						return false;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					Process.GetCurrentProcess().Kill();
				}
				result = false;
			}
			return result;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000CB8C File Offset: 0x0000AD8C
		public static bool Login(string username, string password)
		{
			if (!Constants.Initialized)
			{
				MessageBox.Show("Please initialize your application first!");
				Process.GetCurrentProcess().Kill();
			}
			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
			{
				MessageBox.Show("Missing user login information!");
				Process.GetCurrentProcess().Kill();
			}
			string[] array = new string[0];
			bool result;
			using (WebClient webClient = new WebClient())
			{
				try
				{
					Security.Start();
					webClient.Proxy = null;
					Encoding @default = Encoding.Default;
					WebClient webClient2 = webClient;
					string apiUrl = Constants.ApiUrl;
					NameValueCollection nameValueCollection = new NameValueCollection();
					nameValueCollection["token"] = Encryption.EncryptService(Constants.Token);
					nameValueCollection["aid"] = Encryption.smethod_0(OnProgramStart.AID);
					nameValueCollection["session_id"] = Constants.IV;
					nameValueCollection["api_id"] = Constants.APIENCRYPTSALT;
					nameValueCollection["api_key"] = Constants.APIENCRYPTKEY;
					nameValueCollection["username"] = Encryption.smethod_0(username);
					nameValueCollection["password"] = Encryption.smethod_0(password);
					nameValueCollection["hwid"] = Encryption.smethod_0(Constants.HWID());
					nameValueCollection["session_key"] = Constants.Key;
					nameValueCollection["secret"] = Encryption.smethod_0(OnProgramStart.Secret);
					nameValueCollection["type"] = Encryption.smethod_0("login");
					array = Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
					if (array[0] != Constants.Token)
					{
						MessageBox.Show("Security error has been triggered!");
						Process.GetCurrentProcess().Kill();
					}
					if (Constants.Breached)
					{
						MessageBox.Show("Possible malicious activity detected!");
						Process.GetCurrentProcess().Kill();
					}
					string a = array[2];
					if (a == "success")
					{
						User.ID = array[3];
						User.Username = array[4];
						User.Password = array[5];
						User.Email = array[6];
						User.HWID = array[7];
						User.UserVariable = array[8];
						User.Rank = array[9];
						User.IP = array[10];
						User.Expiry = array[11];
						User.LastLogin = array[12];
						User.RegisterDate = array[13];
						string[] array2 = array[14].Split(new char[]
						{
							'~'
						});
						for (int i = 0; i < array2.Length; i++)
						{
							string[] array3 = array2[i].Split(new char[]
							{
								'^'
							});
							try
							{
								App.Variables.Add(array3[0], array3[1]);
							}
							catch
							{
							}
						}
						Security.End();
						return true;
					}
					if (a == "invalid_details")
					{
						MessageBox.Show("Sorry, your username/password does not match!");
						Security.End();
						return false;
					}
					if (a == "time_expired")
					{
						MessageBox.Show("Your subscription has expired!");
						Security.End();
						return false;
					}
					if (a == "hwid_updated")
					{
						MessageBox.Show("New machine has been binded, re-open the application!");
						Security.End();
						return false;
					}
					if (a == "invalid_hwid")
					{
						MessageBox.Show("This user is binded to another computer, please contact support!");
						Security.End();
						return false;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					Security.End();
					Process.GetCurrentProcess().Kill();
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000CF10 File Offset: 0x0000B110
		public static bool Register(string username, string password, string email, string license)
		{
			if (!Constants.Initialized)
			{
				MessageBox.Show("Please initialize your application first!");
				Security.End();
				Process.GetCurrentProcess().Kill();
			}
			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(license))
			{
				MessageBox.Show("Invalid registrar information!");
				Process.GetCurrentProcess().Kill();
			}
			bool result;
			using (WebClient webClient = new WebClient())
			{
				try
				{
					Security.Start();
					webClient.Proxy = null;
					Encoding @default = Encoding.Default;
					WebClient webClient2 = webClient;
					string apiUrl = Constants.ApiUrl;
					NameValueCollection nameValueCollection = new NameValueCollection();
					nameValueCollection["token"] = Encryption.EncryptService(Constants.Token);
					nameValueCollection["aid"] = Encryption.smethod_0(OnProgramStart.AID);
					nameValueCollection["session_id"] = Constants.IV;
					nameValueCollection["api_id"] = Constants.APIENCRYPTSALT;
					nameValueCollection["api_key"] = Constants.APIENCRYPTKEY;
					nameValueCollection["session_key"] = Constants.Key;
					nameValueCollection["secret"] = Encryption.smethod_0(OnProgramStart.Secret);
					nameValueCollection["type"] = Encryption.smethod_0("register");
					nameValueCollection["username"] = Encryption.smethod_0(username);
					nameValueCollection["password"] = Encryption.smethod_0(password);
					nameValueCollection["email"] = Encryption.smethod_0(email);
					nameValueCollection["license"] = Encryption.smethod_0(license);
					nameValueCollection["hwid"] = Encryption.smethod_0(Constants.HWID());
					string[] array = Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
					if (array[0] != Constants.Token)
					{
						MessageBox.Show("Security error has been triggered!");
						Security.End();
						Process.GetCurrentProcess().Kill();
					}
					if (Constants.Breached)
					{
						MessageBox.Show("Possible malicious activity detected!");
						Process.GetCurrentProcess().Kill();
					}
					string a = array[2];
					if (a == "success")
					{
						Security.End();
						return true;
					}
					if (a == "invalid_license")
					{
						MessageBox.Show("License does not exist!");
						Security.End();
						return false;
					}
					if (a == "email_used")
					{
						MessageBox.Show("Email has already been used!");
						Security.End();
						return false;
					}
					if (a == "invalid_username")
					{
						MessageBox.Show("You entered an invalid/used username!");
						Security.End();
						return false;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					Process.GetCurrentProcess().Kill();
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000D1C0 File Offset: 0x0000B3C0
		public static bool ExtendSubscription(string username, string password, string license)
		{
			if (!Constants.Initialized)
			{
				MessageBox.Show("Please initialize your application first!");
				Security.End();
				Process.GetCurrentProcess().Kill();
			}
			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(license))
			{
				MessageBox.Show("Invalid registrar information!");
				Process.GetCurrentProcess().Kill();
			}
			bool result;
			using (WebClient webClient = new WebClient())
			{
				try
				{
					Security.Start();
					webClient.Proxy = null;
					Encoding @default = Encoding.Default;
					WebClient webClient2 = webClient;
					string apiUrl = Constants.ApiUrl;
					NameValueCollection nameValueCollection = new NameValueCollection();
					nameValueCollection["token"] = Encryption.EncryptService(Constants.Token);
					nameValueCollection["timestamp"] = Encryption.EncryptService(DateTime.Now.ToString());
					nameValueCollection["aid"] = Encryption.smethod_0(OnProgramStart.AID);
					nameValueCollection["session_id"] = Constants.IV;
					nameValueCollection["api_id"] = Constants.APIENCRYPTSALT;
					nameValueCollection["api_key"] = Constants.APIENCRYPTKEY;
					nameValueCollection["session_key"] = Constants.Key;
					nameValueCollection["secret"] = Encryption.smethod_0(OnProgramStart.Secret);
					nameValueCollection["type"] = Encryption.smethod_0("extend");
					nameValueCollection["username"] = Encryption.smethod_0(username);
					nameValueCollection["password"] = Encryption.smethod_0(password);
					nameValueCollection["license"] = Encryption.smethod_0(license);
					string[] array = Encryption.DecryptService(@default.GetString(webClient2.UploadValues(apiUrl, nameValueCollection))).Split("|".ToCharArray());
					if (array[0] != Constants.Token)
					{
						MessageBox.Show("Security error has been triggered!");
						Security.End();
						Process.GetCurrentProcess().Kill();
					}
					if (Security.MaliciousCheck(array[1]))
					{
						MessageBox.Show("Possible malicious activity detected!");
						Process.GetCurrentProcess().Kill();
					}
					if (Constants.Breached)
					{
						MessageBox.Show("Possible malicious activity detected!");
						Process.GetCurrentProcess().Kill();
					}
					string a = array[2];
					if (a == "success")
					{
						Security.End();
						return true;
					}
					if (a == "invalid_token")
					{
						MessageBox.Show("Token does not exist!");
						Security.End();
						return false;
					}
					if (a == "invalid_details")
					{
						MessageBox.Show("Your user details are invalid!");
						Security.End();
						return false;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					Process.GetCurrentProcess().Kill();
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00002683 File Offset: 0x00000883
		public API()
		{
			Class2.m8ctx7Mzu9Lmx();
			base..ctor();
		}
	}
}
