using System;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x02000022 RID: 34
	public class SlotInfo
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000E471 File Offset: 0x0000C671
		// (set) Token: 0x0600016D RID: 365 RVA: 0x0000E479 File Offset: 0x0000C679
		public TBody.SlotID Id { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000E482 File Offset: 0x0000C682
		// (set) Token: 0x0600016F RID: 367 RVA: 0x0000E48A File Offset: 0x0000C68A
		public MPN mpn { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000E493 File Offset: 0x0000C693
		// (set) Token: 0x06000171 RID: 369 RVA: 0x0000E49B File Offset: 0x0000C69B
		public string Name { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000E4A4 File Offset: 0x0000C6A4
		// (set) Token: 0x06000173 RID: 371 RVA: 0x0000E4AC File Offset: 0x0000C6AC
		public string DisplayName { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000E4B5 File Offset: 0x0000C6B5
		// (set) Token: 0x06000175 RID: 373 RVA: 0x0000E4BD File Offset: 0x0000C6BD
		public string LongName { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000E4C6 File Offset: 0x0000C6C6
		// (set) Token: 0x06000177 RID: 375 RVA: 0x0000E4CE File Offset: 0x0000C6CE
		public bool enable { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000E4D7 File Offset: 0x0000C6D7
		// (set) Token: 0x06000179 RID: 377 RVA: 0x0000E4DF File Offset: 0x0000C6DF
		public bool maskable { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000E4E8 File Offset: 0x0000C6E8
		// (set) Token: 0x0600017B RID: 379 RVA: 0x0000E560 File Offset: 0x0000C760
		public int No
		{
			get
			{
				if (this.no != -1)
				{
					return this.no;
				}
				try
				{
					this.no = (int)TBody.hashSlotName[this.Name];
				}
				catch (Exception ex)
				{
					LogUtil.Log(new object[]
					{
						"Initialize Error Slot name is illegal",
						this.Name,
						ex
					});
				}
				return this.no;
			}
			private set
			{
				this.no = value;
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000E569 File Offset: 0x0000C769
		public SlotInfo(TBody.SlotID id, MPN mpn, string displayName, bool enable) : this(id, mpn, displayName, enable, true)
		{
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000E578 File Offset: 0x0000C778
		public SlotInfo(TBody.SlotID id, MPN mpn, string displayName, bool enable, bool maskable)
		{
			this.Id = id;
			this.Name = this.Id.ToString();
			this.mpn = mpn;
			this.DisplayName = displayName;
			this.LongName = displayName + " [" + this.Name + "]";
			this.no = -1;
			this.enable = enable;
			this.maskable = maskable;
		}

		// Token: 0x04000167 RID: 359
		private int no;
	}
}
