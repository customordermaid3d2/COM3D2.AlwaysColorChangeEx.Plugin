using System;
using System.IO;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x0200001F RID: 31
	public class Item
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000CE1D File Offset: 0x0000B01D
		// (set) Token: 0x06000160 RID: 352 RVA: 0x0000CE25 File Offset: 0x0000B025
		public string slot { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000CE2E File Offset: 0x0000B02E
		// (set) Token: 0x06000162 RID: 354 RVA: 0x0000CE36 File Offset: 0x0000B036
		public string filename { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000163 RID: 355 RVA: 0x0000CE3F File Offset: 0x0000B03F
		// (set) Token: 0x06000164 RID: 356 RVA: 0x0000CE47 File Offset: 0x0000B047
		public string[] info { get; private set; }

		// Token: 0x06000165 RID: 357 RVA: 0x0000CE50 File Offset: 0x0000B050
		public Item(string[] info)
		{
			this.filename = info[0];
			this.editname = Path.GetFileNameWithoutExtension(this.filename);
			if (info.Length > 1)
			{
				this.slot = info[1];
			}
			this.info = info;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000CE88 File Offset: 0x0000B088
		public string EditFileName()
		{
			if (this.worksuffix == null)
			{
				return this.editname + FileConst.EXT_MODEL;
			}
			return this.editname + this.worksuffix + FileConst.EXT_MODEL;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000CEB9 File Offset: 0x0000B0B9
		public bool HasSlot()
		{
			return this.slot != null;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000CEC7 File Offset: 0x0000B0C7
		public bool HasLink()
		{
			return this.link != null;
		}

		// Token: 0x04000156 RID: 342
		public Item link;

		// Token: 0x04000157 RID: 343
		public bool needUpdate;

		// Token: 0x04000158 RID: 344
		public string worksuffix;

		// Token: 0x04000159 RID: 345
		public string editname;

		// Token: 0x0400015A RID: 346
		public bool editnameExist;
	}
}
