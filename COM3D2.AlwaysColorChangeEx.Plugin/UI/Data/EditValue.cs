using System;

namespace CM3D2.AlwaysColorChangeEx.Plugin.UI.Data
{
	// Token: 0x02000063 RID: 99
	public class EditValue : EditValueBase<float>
	{
		// Token: 0x06000302 RID: 770 RVA: 0x000188B2 File Offset: 0x00016AB2
		internal EditValue(float val1, EditRange<float> attr) : base(val1, attr)
		{
		}

		// Token: 0x06000303 RID: 771 RVA: 0x000188BC File Offset: 0x00016ABC
		public EditValue(float val1, string format, float min, float max) : base(val1, format, min, max)
		{
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000188C9 File Offset: 0x00016AC9
		protected override bool TryParse(string edit, out float v)
		{
			return float.TryParse(edit, out v);
		}
	}
}
