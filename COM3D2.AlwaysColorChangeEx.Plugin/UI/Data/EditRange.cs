using System;

namespace CM3D2.AlwaysColorChangeEx.Plugin.UI.Data
{
	// Token: 0x02000061 RID: 97
	public class EditRange<T>
	{
		// Token: 0x06000300 RID: 768 RVA: 0x00018678 File Offset: 0x00016878
		internal EditRange(string fmt, T min, T max)
		{
			this.format = fmt;
			this.editMin = min;
			this.editMax = max;
		}

		// Token: 0x0400036C RID: 876
		public readonly T editMin;

		// Token: 0x0400036D RID: 877
		public readonly T editMax;

		// Token: 0x0400036E RID: 878
		public readonly string format;
	}
}
