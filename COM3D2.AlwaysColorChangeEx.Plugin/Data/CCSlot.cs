using System;
using System.Collections.Generic;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x0200002A RID: 42
	public class CCSlot
	{
		// Token: 0x060001AA RID: 426 RVA: 0x0001028F File Offset: 0x0000E48F
		public CCSlot()
		{
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00010297 File Offset: 0x0000E497
		public CCSlot(TBody.SlotID id)
		{
			this.id = id;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000102A6 File Offset: 0x0000E4A6
		public CCSlot(string name)
		{
			this.id = (TBody.SlotID)Enum.Parse(typeof(TBody.SlotID), name);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000102C9 File Offset: 0x0000E4C9
		public void Add(CCMaterial m)
		{
			if (this.materials == null)
			{
				this.materials = new List<CCMaterial>();
			}
			this.materials.Add(m);
		}

		// Token: 0x040001AF RID: 431
		public TBody.SlotID id;

		// Token: 0x040001B0 RID: 432
		public SlotState mask;

		// Token: 0x040001B1 RID: 433
		public List<CCMaterial> materials;
	}
}
