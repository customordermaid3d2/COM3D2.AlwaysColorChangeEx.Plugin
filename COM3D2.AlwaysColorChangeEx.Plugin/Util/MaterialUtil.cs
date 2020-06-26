using System;
using System.Collections.Generic;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Util
{
	// Token: 0x02000053 RID: 83
	public static class MaterialUtil
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x00016B94 File Offset: 0x00014D94
		public static float GetRenderQueue(string matName)
		{
			float result;
			try
			{
				Dictionary<int, KeyValuePair<string, float>> dictionary = PrivateAccessor.Get<Dictionary<int, KeyValuePair<string, float>>>(typeof(ImportCM), "m_hashPriorityMaterials");
				int hashCode = matName.GetHashCode();
				KeyValuePair<string, float> keyValuePair;
				if (dictionary == null || !dictionary.TryGetValue(hashCode, out keyValuePair))
				{
					result = -1f;
				}
				else if (keyValuePair.Key == matName)
				{
					result = keyValuePair.Value;
				}
				else
				{
					result = -1f;
				}
			}
			catch (Exception ex)
			{
				LogUtil.Error(new object[]
				{
					"failed to get pmat field.",
					ex
				});
				result = 0f;
			}
			return result;
		}
	}
}
