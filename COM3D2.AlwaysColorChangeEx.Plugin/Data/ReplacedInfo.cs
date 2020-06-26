using System;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x02000020 RID: 32
	public class ReplacedInfo
	{
		// Token: 0x06000169 RID: 361 RVA: 0x0000CED5 File Offset: 0x0000B0D5
		public ReplacedInfo(string src, string replaced, ResourceRef res, Item item)
		{
			this.source = src;
			this.replaced = replaced;
			this.res = res;
			this.item = item;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000CEFA File Offset: 0x0000B0FA
		public ReplacedInfo(string src, string replaced, ResourceRef res, TargetMaterial tm)
		{
			this.source = src;
			this.replaced = replaced;
			this.res = res;
			this.material = tm;
		}

		// Token: 0x0400015E RID: 350
		public string source;

		// Token: 0x0400015F RID: 351
		public string replaced;

		// Token: 0x04000160 RID: 352
		public ResourceRef res;

		// Token: 0x04000161 RID: 353
		public TargetMaterial material;

		// Token: 0x04000162 RID: 354
		public Item item;
	}
}
