using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace scyllareworked
{
	// Token: 0x02000016 RID: 22
	public class json_wrapper
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x000029D1 File Offset: 0x00000BD1
		public static bool is_serializable(Type to_check)
		{
			return to_check.IsSerializable || to_check.IsDefined(typeof(DataContractAttribute), true);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000B99C File Offset: 0x00009B9C
		public json_wrapper(object obj_to_work_with)
		{
			Class2.m8ctx7Mzu9Lmx();
			base..ctor();
			this.current_object = obj_to_work_with;
			Type type = this.current_object.GetType();
			this.serializer = new DataContractJsonSerializer(type);
			if (!json_wrapper.is_serializable(type))
			{
				throw new Exception(string.Format("the object {0} isn't a serializable", this.current_object));
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000B9F4 File Offset: 0x00009BF4
		public object string_to_object(string json)
		{
			object result;
			using (MemoryStream memoryStream = new MemoryStream(Encoding.Default.GetBytes(json)))
			{
				result = this.serializer.ReadObject(memoryStream);
			}
			return result;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000029EF File Offset: 0x00000BEF
		public T string_to_generic<T>(string json)
		{
			return (T)((object)this.string_to_object(json));
		}

		// Token: 0x040000A4 RID: 164
		private DataContractJsonSerializer serializer;

		// Token: 0x040000A5 RID: 165
		private object current_object;
	}
}
