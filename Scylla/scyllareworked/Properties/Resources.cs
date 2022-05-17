using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace scyllareworked.Properties
{
	// Token: 0x02000018 RID: 24
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x060000ED RID: 237 RVA: 0x00002683 File Offset: 0x00000883
		internal Resources()
		{
			Class2.m8ctx7Mzu9Lmx();
			base..ctor();
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00002A19 File Offset: 0x00000C19
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("scyllareworked.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x1700002F RID: 47
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00002A46 File Offset: 0x00000C46
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00002A4E File Offset: 0x00000C4E
		internal static Bitmap IconItems_flashlight
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("IconItems_flashlight", Resources.resourceCulture);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00002A69 File Offset: 0x00000C69
		internal static Bitmap IconItems_flashlight1
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("IconItems_flashlight1", Resources.resourceCulture);
			}
		}

		// Token: 0x040000A6 RID: 166
		private static ResourceManager resourceMan;

		// Token: 0x040000A7 RID: 167
		private static CultureInfo resourceCulture;
	}
}
