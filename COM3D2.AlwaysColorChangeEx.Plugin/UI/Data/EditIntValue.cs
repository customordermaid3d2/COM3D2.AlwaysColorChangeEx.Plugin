using System;

namespace CM3D2.AlwaysColorChangeEx.Plugin.UI.Data
{
	// Token: 0x02000060 RID: 96
	public class EditIntValue : EditValueBase<int>
	{
		// Token: 0x060002FD RID: 765 RVA: 0x00018658 File Offset: 0x00016858
		internal EditIntValue(int val1, EditRange<int> attr) : base(val1, attr)
		{
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00018662 File Offset: 0x00016862
		public EditIntValue(int val1, string format, int min, int max) : base(val1, format, min, max)
		{
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0001866F File Offset: 0x0001686F
		protected override bool TryParse(string edit, out int v)
		{
			return int.TryParse(edit, out v);
		}
	}
}
