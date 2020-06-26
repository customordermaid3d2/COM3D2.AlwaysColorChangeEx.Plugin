using System;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x0200002C RID: 44
	public class CCMPNValue
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x00010332 File Offset: 0x0000E532
		public CCMPNValue()
		{
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0001033A File Offset: 0x0000E53A
		public CCMPNValue(string mpnName, int v, int min, int max)
		{
			this.name = (MPN)Enum.Parse(typeof(MPN), mpnName);
			this.value = v;
			this.min = min;
			this.max = max;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00010373 File Offset: 0x0000E573
		public CCMPNValue(MPN name, int v, int min, int max)
		{
			this.name = name;
			this.value = v;
			this.min = min;
			this.max = max;
		}

		// Token: 0x040001B4 RID: 436
		public MPN name;

		// Token: 0x040001B5 RID: 437
		public int value;

		// Token: 0x040001B6 RID: 438
		public int min;

		// Token: 0x040001B7 RID: 439
		public int max;
	}
}
