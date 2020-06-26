using System;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x02000039 RID: 57
	public class ShaderPropTex : ShaderProp
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x000115F1 File Offset: 0x0000F7F1
		public ShaderPropTex(string name, PropKey key, int id, TexType type, Keyword k = Keyword.NONE) : base(name, key, id, ValType.Tex)
		{
			this.texType = type;
			base.Keyword = k;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0001160D File Offset: 0x0000F80D
		public ShaderPropTex(PropKey key, TexType type, Keyword k = Keyword.NONE) : base(key, ValType.Tex)
		{
			this.texType = type;
			base.Keyword = k;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00011625 File Offset: 0x0000F825
		public void SetValue(Material m, Texture2D tex)
		{
			m.SetTexture(this.propId, tex);
		}

		// Token: 0x04000216 RID: 534
		public TexType texType;
	}
}
