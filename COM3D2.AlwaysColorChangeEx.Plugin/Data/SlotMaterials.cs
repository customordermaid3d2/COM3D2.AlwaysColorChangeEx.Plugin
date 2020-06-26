using System;
using System.Collections.Generic;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x0200001A RID: 26
	public class SlotMaterials
	{
		// Token: 0x06000114 RID: 276 RVA: 0x0000C450 File Offset: 0x0000A650
		public TargetMaterial Get(int matNo)
		{
			if (matNo >= this.materials.Count)
			{
				return null;
			}
			return this.materials[matNo];
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000C470 File Offset: 0x0000A670
		public void SetMaterial(TargetMaterial tm)
		{
			int num = tm.matNo - this.materials.Count;
			if (num == 0)
			{
				this.materials.Add(tm);
				return;
			}
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					this.materials.Add(null);
				}
				this.materials.Add(tm);
				return;
			}
			this.materials[tm.matNo] = tm;
		}

		// Token: 0x0400012D RID: 301
		public readonly List<TargetMaterial> materials = new List<TargetMaterial>();
	}
}
