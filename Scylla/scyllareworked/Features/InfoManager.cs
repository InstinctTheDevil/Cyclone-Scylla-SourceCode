using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Threading;

namespace scyllareworked.Features
{
	// Token: 0x02000028 RID: 40
	internal class InfoManager
	{
		// Token: 0x0600015A RID: 346 RVA: 0x00002D54 File Offset: 0x00000F54
		public InfoManager()
		{
			Class2.m8ctx7Mzu9Lmx();
			base..ctor();
			this.lastGateway = this.GetGatewayMAC();
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00002D6D File Offset: 0x00000F6D
		public void StartListener()
		{
			this.timer = new Timer(delegate(object _)
			{
				this.OnCallBack();
			}, null, 5000, -1);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000DA10 File Offset: 0x0000BC10
		private void OnCallBack()
		{
			this.timer.Dispose();
			if (this.GetGatewayMAC() == this.lastGateway)
			{
				this.lastGateway = this.GetGatewayMAC();
			}
			this.timer = new Timer(delegate(object _)
			{
				this.OnCallBack();
			}, null, 5000, -1);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000DA68 File Offset: 0x0000BC68
		public static IPAddress GetDefaultGateway()
		{
			return (from a in (from n in NetworkInterface.GetAllNetworkInterfaces()
			where n.OperationalStatus == OperationalStatus.Up
			where n.NetworkInterfaceType != NetworkInterfaceType.Loopback
			select n).SelectMany(delegate(NetworkInterface n)
			{
				IPInterfaceProperties ipproperties = n.GetIPProperties();
				if (ipproperties == null)
				{
					return null;
				}
				return ipproperties.GatewayAddresses;
			}).Select(delegate(GatewayIPAddressInformation g)
			{
				if (g == null)
				{
					return null;
				}
				return g.Address;
			})
			where a != null
			select a).FirstOrDefault<IPAddress>();
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000DB34 File Offset: 0x0000BD34
		private string GetArpTable()
		{
			string pathRoot = Path.GetPathRoot(Environment.SystemDirectory);
			string result;
			using (Process process = Process.Start(new ProcessStartInfo
			{
				FileName = pathRoot + "Windows\\System32\\arp.exe",
				Arguments = "-a",
				UseShellExecute = false,
				RedirectStandardOutput = true,
				CreateNoWindow = true
			}))
			{
				using (StreamReader standardOutput = process.StandardOutput)
				{
					result = standardOutput.ReadToEnd();
				}
			}
			return result;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000DBCC File Offset: 0x0000BDCC
		private string GetGatewayMAC()
		{
			string arg = InfoManager.GetDefaultGateway().ToString();
			return new Regex(string.Format("({0} [\\W]*) ([a-z0-9-]*)", arg)).Match(this.GetArpTable()).Groups[2].ToString();
		}

		// Token: 0x040000D5 RID: 213
		private Timer timer;

		// Token: 0x040000D6 RID: 214
		private string lastGateway;
	}
}
