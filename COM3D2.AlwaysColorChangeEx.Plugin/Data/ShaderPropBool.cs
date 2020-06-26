using System;
using CM3D2.AlwaysColorChangeEx.Plugin.UI.Data;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x02000036 RID: 54
	public class ShaderPropBool : ShaderPropFloat
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x000114E2 File Offset: 0x0000F6E2
		public ShaderPropBool(string name, PropKey key, int id) : base(name, key, id, ValType.Bool)
		{
			this.range = EditRange.boolVal;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000114F9 File Offset: 0x0000F6F9
		public ShaderPropBool(PropKey key) : base(key, ValType.Bool)
		{
			this.range = EditRange.boolVal;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0001150E File Offset: 0x0000F70E
		public void SetValue(Material m, bool val)
		{
			m.SetFloat(this.propId, val ? 1f : 0f);
		}
	}
}
