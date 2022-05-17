using System;
using System.Reflection;

// Token: 0x0200002E RID: 46
internal class Class1
{
	// Token: 0x06000179 RID: 377 RVA: 0x0000E40C File Offset: 0x0000C60C
	internal static void ywHtx7MMiQjH9(int typemdt)
	{
		Type type = Class1.module_0.ResolveType(33554432 + typemdt);
		foreach (FieldInfo fieldInfo in type.GetFields())
		{
			MethodInfo method = (MethodInfo)Class1.module_0.ResolveMethod(fieldInfo.MetadataToken + 100663296);
			fieldInfo.SetValue(null, (MulticastDelegate)Delegate.CreateDelegate(type, method));
		}
	}

	// Token: 0x0600017A RID: 378 RVA: 0x00002683 File Offset: 0x00000883
	public Class1()
	{
		Class2.m8ctx7Mzu9Lmx();
		base..ctor();
	}

	// Token: 0x0600017B RID: 379 RVA: 0x00002E1B File Offset: 0x0000101B
	static Class1()
	{
		Class2.m8ctx7Mzu9Lmx();
		Class1.module_0 = typeof(Class1).Assembly.ManifestModule;
	}

	// Token: 0x040000E2 RID: 226
	internal static Module module_0;

	// Token: 0x0200002F RID: 47
	// (Invoke) Token: 0x0600017D RID: 381
	internal delegate void Delegate0(object o);
}
