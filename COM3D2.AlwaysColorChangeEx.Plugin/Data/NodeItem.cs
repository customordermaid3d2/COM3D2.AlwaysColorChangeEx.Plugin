using System;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x02000023 RID: 35
	public class NodeItem
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000E5E9 File Offset: 0x0000C7E9
		// (set) Token: 0x0600017F RID: 383 RVA: 0x0000E5F1 File Offset: 0x0000C7F1
		public string Name { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000E5FA File Offset: 0x0000C7FA
		// (set) Token: 0x06000181 RID: 385 RVA: 0x0000E602 File Offset: 0x0000C802
		public string DisplayName { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000E60B File Offset: 0x0000C80B
		// (set) Token: 0x06000183 RID: 387 RVA: 0x0000E613 File Offset: 0x0000C813
		public NodeItem parent { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000E61C File Offset: 0x0000C81C
		// (set) Token: 0x06000185 RID: 389 RVA: 0x0000E624 File Offset: 0x0000C824
		public int depth { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000E62D File Offset: 0x0000C82D
		// (set) Token: 0x06000187 RID: 391 RVA: 0x0000E635 File Offset: 0x0000C835
		public TBody.SlotID[] slots { get; private set; }

		// Token: 0x06000188 RID: 392 RVA: 0x0000E63E File Offset: 0x0000C83E
		public NodeItem(string dispName, int depth, params TBody.SlotID[] slots)
		{
			this.DisplayName = dispName;
			this.depth = depth;
			this.slots = (slots ?? NodeItem.EMPTY);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000E664 File Offset: 0x0000C864
		public NodeItem(string name, string dispName, int depth, params TBody.SlotID[] slots)
		{
			this.Name = name;
			this.DisplayName = dispName;
			this.depth = depth;
			this.slots = (slots ?? NodeItem.EMPTY);
		}

		// Token: 0x0400016F RID: 367
		private static readonly TBody.SlotID[] EMPTY = new TBody.SlotID[0];
	}
}
