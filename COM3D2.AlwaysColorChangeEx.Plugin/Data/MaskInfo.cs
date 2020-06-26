using System;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x02000027 RID: 39
	public class MaskInfo
	{
		// Token: 0x06000199 RID: 409 RVA: 0x0000F0DD File Offset: 0x0000D2DD
		public MaskInfo(SlotInfo si, TBodySkin slot)
		{
			this.slotInfo = si;
			this.slot = slot;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000F0F3 File Offset: 0x0000D2F3
		public void UpdateState()
		{
			if (this.slot.obj == null)
			{
				this.state = SlotState.NotLoaded;
				return;
			}
			if (!this.slot.boVisible)
			{
				this.state = SlotState.Masked;
				return;
			}
			this.state = SlotState.Displayed;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000F12C File Offset: 0x0000D32C
		public string Name(bool useDisplayName)
		{
			if (!useDisplayName)
			{
				return this.slotInfo.Name;
			}
			return this.slotInfo.DisplayName;
		}

		// Token: 0x0400019B RID: 411
		public readonly SlotInfo slotInfo;

		// Token: 0x0400019C RID: 412
		public TBodySkin slot;

		// Token: 0x0400019D RID: 413
		public SlotState state;

		// Token: 0x0400019E RID: 414
		public bool value;
	}
}
