using System;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x0200001D RID: 29
	public class ColorSet
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600014B RID: 331 RVA: 0x0000CB7C File Offset: 0x0000AD7C
		// (set) Token: 0x0600014C RID: 332 RVA: 0x0000CB84 File Offset: 0x0000AD84
		public string key { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000CB8D File Offset: 0x0000AD8D
		// (set) Token: 0x0600014E RID: 334 RVA: 0x0000CB95 File Offset: 0x0000AD95
		public string suffix { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000CB9E File Offset: 0x0000AD9E
		// (set) Token: 0x06000150 RID: 336 RVA: 0x0000CBA6 File Offset: 0x0000ADA6
		public string filename { get; private set; }

		// Token: 0x0400014A RID: 330
		public ACCMenu menu;
	}
}
