using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Costura
{
	// Token: 0x0200002B RID: 43
	[CompilerGenerated]
	internal static class AssemblyLoader
	{
		// Token: 0x0600016F RID: 367 RVA: 0x00002E0A File Offset: 0x0000100A
		private static string CultureToString(CultureInfo culture)
		{
			if (culture == null)
			{
				return "";
			}
			return culture.Name;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000DFB0 File Offset: 0x0000C1B0
		private static Assembly ReadExistingAssembly(AssemblyName name)
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			Assembly[] assemblies = currentDomain.GetAssemblies();
			foreach (Assembly assembly in assemblies)
			{
				AssemblyName name2 = assembly.GetName();
				if (string.Equals(name2.Name, name.Name, StringComparison.InvariantCultureIgnoreCase) && string.Equals(AssemblyLoader.CultureToString(name2.CultureInfo), AssemblyLoader.CultureToString(name.CultureInfo), StringComparison.InvariantCultureIgnoreCase))
				{
					return assembly;
				}
			}
			return null;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000E020 File Offset: 0x0000C220
		private static void CopyTo(Stream source, Stream destination)
		{
			byte[] array = new byte[81920];
			int count;
			while ((count = source.Read(array, 0, array.Length)) != 0)
			{
				destination.Write(array, 0, count);
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000E054 File Offset: 0x0000C254
		private static Stream LoadStream(string fullName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			if (fullName.EndsWith(".compressed"))
			{
				Stream result;
				using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(fullName))
				{
					using (DeflateStream deflateStream = new DeflateStream(manifestResourceStream, CompressionMode.Decompress))
					{
						MemoryStream memoryStream = new MemoryStream();
						AssemblyLoader.CopyTo(deflateStream, memoryStream);
						memoryStream.Position = 0L;
						result = memoryStream;
					}
				}
				return result;
			}
			return executingAssembly.GetManifestResourceStream(fullName);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000E0E0 File Offset: 0x0000C2E0
		private static Stream LoadStream(Dictionary<string, string> resourceNames, string name)
		{
			string fullName;
			if (resourceNames.TryGetValue(name, out fullName))
			{
				return AssemblyLoader.LoadStream(fullName);
			}
			return null;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000E100 File Offset: 0x0000C300
		private static byte[] ReadStream(Stream stream)
		{
			byte[] array = new byte[stream.Length];
			stream.Read(array, 0, array.Length);
			return array;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000E128 File Offset: 0x0000C328
		private static Assembly ReadFromEmbeddedResources(Dictionary<string, string> assemblyNames, Dictionary<string, string> symbolNames, AssemblyName requestedAssemblyName)
		{
			string text = requestedAssemblyName.Name.ToLowerInvariant();
			if (requestedAssemblyName.CultureInfo != null && !string.IsNullOrEmpty(requestedAssemblyName.CultureInfo.Name))
			{
				text = requestedAssemblyName.CultureInfo.Name + "." + text;
			}
			byte[] rawAssembly;
			using (Stream stream = AssemblyLoader.LoadStream(assemblyNames, text))
			{
				if (stream == null)
				{
					return null;
				}
				rawAssembly = AssemblyLoader.ReadStream(stream);
			}
			using (Stream stream2 = AssemblyLoader.LoadStream(symbolNames, text))
			{
				if (stream2 != null)
				{
					byte[] rawSymbolStore = AssemblyLoader.ReadStream(stream2);
					return Assembly.Load(rawAssembly, rawSymbolStore);
				}
			}
			return Assembly.Load(rawAssembly);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000E1E8 File Offset: 0x0000C3E8
		public static Assembly ResolveAssembly(object sender, ResolveEventArgs e)
		{
			object obj = AssemblyLoader.nullCacheLock;
			lock (obj)
			{
				if (AssemblyLoader.nullCache.ContainsKey(e.Name))
				{
					return null;
				}
			}
			AssemblyName assemblyName = new AssemblyName(e.Name);
			Assembly assembly = AssemblyLoader.ReadExistingAssembly(assemblyName);
			if (assembly != null)
			{
				return assembly;
			}
			assembly = AssemblyLoader.ReadFromEmbeddedResources(AssemblyLoader.assemblyNames, AssemblyLoader.symbolNames, assemblyName);
			if (assembly == null)
			{
				object obj2 = AssemblyLoader.nullCacheLock;
				lock (obj2)
				{
					AssemblyLoader.nullCache[e.Name] = true;
				}
				if ((assemblyName.Flags & AssemblyNameFlags.Retargetable) != AssemblyNameFlags.None)
				{
					assembly = Assembly.Load(assemblyName);
				}
			}
			return assembly;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000E2C0 File Offset: 0x0000C4C0
		static AssemblyLoader()
		{
			Class2.m8ctx7Mzu9Lmx();
			AssemblyLoader.nullCacheLock = new object();
			AssemblyLoader.nullCache = new Dictionary<string, bool>();
			AssemblyLoader.assemblyNames = new Dictionary<string, string>();
			AssemblyLoader.symbolNames = new Dictionary<string, string>();
			AssemblyLoader.assemblyNames.Add("bcmakecert", "costura.bcmakecert.dll.compressed");
			AssemblyLoader.assemblyNames.Add("bouncycastle.crypto", "costura.bouncycastle.crypto.dll.compressed");
			AssemblyLoader.assemblyNames.Add("costura", "costura.costura.dll.compressed");
			AssemblyLoader.symbolNames.Add("costura", "costura.costura.pdb.compressed");
			AssemblyLoader.assemblyNames.Add("fiddlercore", "costura.fiddlercore.dll.compressed");
			AssemblyLoader.assemblyNames.Add("guna.ui2", "costura.guna.ui2.dll.compressed");
			AssemblyLoader.assemblyNames.Add("icsharpcode.sharpziplib", "costura.icsharpcode.sharpziplib.dll.compressed");
			AssemblyLoader.symbolNames.Add("icsharpcode.sharpziplib", "costura.icsharpcode.sharpziplib.pdb.compressed");
			AssemblyLoader.assemblyNames.Add("newtonsoft.json", "costura.newtonsoft.json.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.diagnostics.diagnosticsource", "costura.system.diagnostics.diagnosticsource.dll.compressed");
			AssemblyLoader.assemblyNames.Add("telerik.networkconnections", "costura.telerik.networkconnections.dll.compressed");
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000E3D8 File Offset: 0x0000C5D8
		public static void Attach()
		{
			if (Interlocked.Exchange(ref AssemblyLoader.isAttached, 1) == 1)
			{
				return;
			}
			AppDomain currentDomain = AppDomain.CurrentDomain;
			currentDomain.AssemblyResolve += AssemblyLoader.ResolveAssembly;
		}

		// Token: 0x040000DD RID: 221
		private static object nullCacheLock;

		// Token: 0x040000DE RID: 222
		private static Dictionary<string, bool> nullCache;

		// Token: 0x040000DF RID: 223
		private static Dictionary<string, string> assemblyNames;

		// Token: 0x040000E0 RID: 224
		private static Dictionary<string, string> symbolNames;

		// Token: 0x040000E1 RID: 225
		private static int isAttached;
	}
}
