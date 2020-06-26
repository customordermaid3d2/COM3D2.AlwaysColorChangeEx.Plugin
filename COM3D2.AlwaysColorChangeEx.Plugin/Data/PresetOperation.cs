using System;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x02000034 RID: 52
	public class PresetOperation
	{
		// Token: 0x060001DF RID: 479 RVA: 0x000113EB File Offset: 0x0000F5EB
		public PresetOperation(string label, Func<float, float> func)
		{
			this.label = label;
			this.func = func;
		}

		// Token: 0x04000209 RID: 521
		public readonly string label;

		// Token: 0x0400020A RID: 522
		public readonly Func<float, float> func;
	}
}
