using System;
using System.Collections.Generic;

namespace scyllareworked.Features
{
	// Token: 0x0200001C RID: 28
	internal class App
	{
		// Token: 0x060000FD RID: 253 RVA: 0x0000BF4C File Offset: 0x0000A14C
		public static string GrabVariable(string name)
		{
			string result;
			try
			{
				if (User.ID == null && User.HWID == null && User.IP == null && Constants.Breached)
				{
					Constants.Breached = true;
					result = "User is not logged in, possible breach detected!";
				}
				else
				{
					result = App.Variables[name];
				}
			}
			catch
			{
				result = "N/A";
			}
			return result;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00002683 File Offset: 0x00000883
		public App()
		{
			Class2.m8ctx7Mzu9Lmx();
			base..ctor();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00002AB3 File Offset: 0x00000CB3
		static App()
		{
			Class2.m8ctx7Mzu9Lmx();
			App.Error = null;
			App.Variables = new Dictionary<string, string>();
		}

		// Token: 0x040000A9 RID: 169
		public static string Error;

		// Token: 0x040000AA RID: 170
		public static Dictionary<string, string> Variables;
	}
}
