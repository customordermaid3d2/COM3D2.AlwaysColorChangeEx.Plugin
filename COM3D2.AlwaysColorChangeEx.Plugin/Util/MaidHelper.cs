using System;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Util
{
	// Token: 0x02000051 RID: 81
	public static class MaidHelper
	{
		// Token: 0x06000287 RID: 647 RVA: 0x0001602A File Offset: 0x0001422A
		public static string GetName(Maid maid)
		{
			return maid.status.fullNameJpStyle;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00016037 File Offset: 0x00014237
		public static string GetGuid(Maid maid)
		{
			return maid.status.guid;
		}
	}
}
