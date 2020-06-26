using System;
using CM3D2.AlwaysColorChangeEx.Plugin.Data;

namespace CM3D2.AlwaysColorChangeEx.Plugin
{
	// Token: 0x02000016 RID: 22
	public class EditTarget
	{
		// Token: 0x060000CF RID: 207 RVA: 0x0000A708 File Offset: 0x00008908
		public EditTarget()
		{
			this.Clear();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000A716 File Offset: 0x00008916
		public void Clear()
		{
			this.slotName = string.Empty;
			this.matNo = -1;
			this.propName = string.Empty;
			this.propKey = PropKey.Unkown;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000A73D File Offset: 0x0000893D
		public bool IsValid()
		{
			return this.matNo >= 0 && this.slotName.Length != 0 && this.propName.Length != 0;
		}

		// Token: 0x040000FC RID: 252
		public string slotName;

		// Token: 0x040000FD RID: 253
		public int matNo;

		// Token: 0x040000FE RID: 254
		public string propName;

		// Token: 0x040000FF RID: 255
		public PropKey propKey;
	}
}
