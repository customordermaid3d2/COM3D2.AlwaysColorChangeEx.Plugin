using System;
using System.Collections.Generic;
using System.Linq;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Util
{
	// Token: 0x02000049 RID: 73
	public static class EnumExt<T> where T : struct, IConvertible
	{
		// Token: 0x06000234 RID: 564 RVA: 0x000144A8 File Offset: 0x000126A8
		public static void Exec(Action<T> action)
		{
			foreach (T obj in EnumExt<T>.Values)
			{
				action(obj);
			}
		}

		// Token: 0x04000321 RID: 801
		private static readonly List<T> Values = Enum.GetValues(typeof(T)).Cast<T>().ToList<T>();
	}
}
