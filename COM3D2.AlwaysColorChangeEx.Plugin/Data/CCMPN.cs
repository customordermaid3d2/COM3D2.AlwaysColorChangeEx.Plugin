using System;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x0200002B RID: 43
	public class CCMPN
	{
		// Token: 0x060001AE RID: 430 RVA: 0x000102EA File Offset: 0x0000E4EA
		public CCMPN()
		{
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000102F2 File Offset: 0x0000E4F2
		public CCMPN(string mpnName, string filename)
		{
			this.name = (MPN)Enum.Parse(typeof(MPN), mpnName);
			this.filename = filename;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0001031C File Offset: 0x0000E51C
		public CCMPN(MPN name, string filename)
		{
			this.name = name;
			this.filename = filename;
		}

		// Token: 0x040001B2 RID: 434
		public MPN name;

		// Token: 0x040001B3 RID: 435
		public string filename;
	}
}
