using System;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Util
{
	// Token: 0x02000057 RID: 87
	public sealed class NumberUtil
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00017314 File Offset: 0x00015514
		public static NumberUtil Instance
		{
			get
			{
				return NumberUtil.INSTANCE;
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0001731B File Offset: 0x0001551B
		private NumberUtil()
		{
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00017323 File Offset: 0x00015523
		public static bool Equals(float f1, float f2, float epsilon = 0.001f)
		{
			return ((f1 < f2) ? (f2 - f1) : (f1 - f2)) < epsilon;
		}

		// Token: 0x0400033C RID: 828
		private static readonly NumberUtil INSTANCE = new NumberUtil();
	}
}
