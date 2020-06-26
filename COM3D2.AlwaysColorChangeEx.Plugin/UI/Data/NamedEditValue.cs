using System;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;

namespace CM3D2.AlwaysColorChangeEx.Plugin.UI.Data
{
	// Token: 0x02000064 RID: 100
	public class NamedEditValue : EditValue
	{
		// Token: 0x06000305 RID: 773 RVA: 0x000188D2 File Offset: 0x00016AD2
		public NamedEditValue(string name, float val1, EditRange<float> attr, Action<float> act) : base(val1, attr)
		{
			this.name = name;
			this.act = act;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000188EB File Offset: 0x00016AEB
		public NamedEditValue(string name, float val1, string format, float min, float max, Action<float> act) : base(val1, format, min, max)
		{
			this.name = name;
			this.act = act;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00018908 File Offset: 0x00016B08
		public void Update(float value)
		{
			if (!NumberUtil.Equals(this.val, value, 0.001f))
			{
				base.Set(value);
			}
		}

		// Token: 0x04000380 RID: 896
		public readonly string name;

		// Token: 0x04000381 RID: 897
		public Action<float> act;
	}
}
