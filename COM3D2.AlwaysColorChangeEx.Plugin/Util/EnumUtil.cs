using System;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Util
{
	// Token: 0x0200004A RID: 74
	public static class EnumUtil
	{
		// Token: 0x06000236 RID: 566 RVA: 0x0001451C File Offset: 0x0001271C
		public static bool TryParse<T>(string src, bool ignoreCase, out T result)
		{
			string[] names = Enum.GetNames(typeof(T));
			StringComparison comparisonType = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
			foreach (string text in names)
			{
				if (string.Equals(text, src, comparisonType))
				{
					result = (T)((object)Enum.Parse(typeof(T), text));
					return true;
				}
			}
			result = default(T);
			return false;
		}
	}
}
