using System;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x02000038 RID: 56
	public class ShaderPropColor : ShaderProp
	{
		// Token: 0x060001EF RID: 495 RVA: 0x0001159E File Offset: 0x0000F79E
		public ShaderPropColor(string name, PropKey key, int id, ColorType colType, bool composition = false, Keyword k = Keyword.NONE) : base(name, key, id, ValType.Color)
		{
			this.colorType = colType;
			base.Keyword = k;
			this.composition = composition;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000115C2 File Offset: 0x0000F7C2
		public ShaderPropColor(PropKey key, ColorType colType, bool composition = false, Keyword k = Keyword.NONE) : base(key, ValType.Color)
		{
			this.colorType = colType;
			base.Keyword = k;
			this.composition = composition;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000115E2 File Offset: 0x0000F7E2
		public void SetValue(Material m, Color col)
		{
			m.SetColor(this.propId, col);
		}

		// Token: 0x04000213 RID: 531
		public readonly ColorType colorType;

		// Token: 0x04000214 RID: 532
		public readonly bool composition;

		// Token: 0x04000215 RID: 533
		public Color defaultVal;
	}
}
