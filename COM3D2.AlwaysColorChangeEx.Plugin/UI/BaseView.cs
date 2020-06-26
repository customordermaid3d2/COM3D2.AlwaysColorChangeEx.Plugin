using System;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;

namespace CM3D2.AlwaysColorChangeEx.Plugin.UI
{
	// Token: 0x02000068 RID: 104
	public abstract class BaseView
	{
		// Token: 0x06000332 RID: 818
		public abstract void UpdateUI(UIParams uiparams);

		// Token: 0x06000333 RID: 819 RVA: 0x0001A240 File Offset: 0x00018440
		public virtual void Clear()
		{
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0001A242 File Offset: 0x00018442
		public virtual void Dispose()
		{
		}

		// Token: 0x040003A2 RID: 930
		protected static readonly MaidHolder holder = MaidHolder.Instance;

		// Token: 0x040003A3 RID: 931
		protected UIParams uiParams;
	}
}
