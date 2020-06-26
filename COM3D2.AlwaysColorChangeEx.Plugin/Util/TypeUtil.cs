using System;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Util
{
	// Token: 0x0200005C RID: 92
	public static class TypeUtil
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x00018050 File Offset: 0x00016250
		public static bool IsBody(MPN mpn)
		{
			return TypeUtil.BODY_START <= (int)mpn && mpn <= (MPN)TypeUtil.BODY_END;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00018074 File Offset: 0x00016274
		public static bool IsWear(MPN mpn)
		{
			return TypeUtil.WEAR_START <= (int)mpn && mpn <= (MPN)TypeUtil.WEAR_END;
		}

		// Token: 0x04000352 RID: 850
		public static readonly int BODY_START = PrivateAccessor.Get<int>(typeof(MPN_TYPE_RANGE), "BODY_START");

		// Token: 0x04000353 RID: 851
		public static readonly int BODY_END = PrivateAccessor.Get<int>(typeof(MPN_TYPE_RANGE), "BODY_END");

		// Token: 0x04000354 RID: 852
		public static readonly int FOLDER_BODY_START = PrivateAccessor.Get<int>(typeof(MPN_TYPE_RANGE), "FOLDER_BODY_START");

		// Token: 0x04000355 RID: 853
		public static readonly int FOLDER_BODY_END = PrivateAccessor.Get<int>(typeof(MPN_TYPE_RANGE), "FOLDER_BODY_END");

		// Token: 0x04000356 RID: 854
		public static readonly int WEAR_START = PrivateAccessor.Get<int>(typeof(MPN_TYPE_RANGE), "WEAR_START");

		// Token: 0x04000357 RID: 855
		public static readonly int WEAR_END = PrivateAccessor.Get<int>(typeof(MPN_TYPE_RANGE), "WEAR_END");

		// Token: 0x04000358 RID: 856
		public static readonly int SET_START = PrivateAccessor.Get<int>(typeof(MPN_TYPE_RANGE), "SET_START");

		// Token: 0x04000359 RID: 857
		public static readonly int SET_END = PrivateAccessor.Get<int>(typeof(MPN_TYPE_RANGE), "SET_END");

		// Token: 0x0400035A RID: 858
		public static readonly MaidParts.PARTS_COLOR PARTS_COLOR_START = PrivateAccessor.Get<MaidParts.PARTS_COLOR>(typeof(MaidParts.PARTS_COLOR), "NONE") + 1;

		// Token: 0x0400035B RID: 859
		public static readonly MaidParts.PARTS_COLOR PARTS_COLOR_END = PrivateAccessor.Get<MaidParts.PARTS_COLOR>(typeof(MaidParts.PARTS_COLOR), "MAX") - 1;
	}
}
