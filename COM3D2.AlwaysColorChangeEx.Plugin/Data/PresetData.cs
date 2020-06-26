using System;
using System.Collections.Generic;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x02000029 RID: 41
	public class PresetData
	{
		// Token: 0x040001A8 RID: 424
		public string name;

		// Token: 0x040001A9 RID: 425
		public List<CCSlot> slots = new List<CCSlot>();

		// Token: 0x040001AA RID: 426
		public List<CCMPN> mpns = new List<CCMPN>();

		// Token: 0x040001AB RID: 427
		public List<CCMPNValue> mpnvals = new List<CCMPNValue>();

		// Token: 0x040001AC RID: 428
		public Dictionary<string, CCPartsColor> partsColors = new Dictionary<string, CCPartsColor>();

		// Token: 0x040001AD RID: 429
		public Dictionary<string, bool> delNodes;

		// Token: 0x040001AE RID: 430
		public Dictionary<string, float> boneMorph;
	}
}
