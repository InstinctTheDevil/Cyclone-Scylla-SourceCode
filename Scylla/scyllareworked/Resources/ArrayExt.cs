using System;

namespace scyllareworked.Resources
{
	// Token: 0x0200001A RID: 26
	public static class ArrayExt
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x0000BA3C File Offset: 0x00009C3C
		public static T[] Subset<T>(this T[] array, int start, int count)
		{
			T[] array2 = new T[count];
			Array.Copy(array, start, array2, 0, count);
			return array2;
		}
	}
}
