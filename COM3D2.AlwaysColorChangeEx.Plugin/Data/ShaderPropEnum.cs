using System;
using CM3D2.AlwaysColorChangeEx.Plugin.UI.Data;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x02000037 RID: 55
	public class ShaderPropEnum : ShaderPropFloat
	{
		// Token: 0x060001EC RID: 492 RVA: 0x0001152B File Offset: 0x0000F72B
		public ShaderPropEnum(PropKey key, Type enumType, int defaultVal, int min, int max) : base(key, ValType.Enum)
		{
			this.names = Enum.GetNames(enumType);
			this.range = new EditRange<float>("F0", (float)min, (float)max);
			this.defaultVal = (float)defaultVal;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0001155F File Offset: 0x0000F75F
		public ShaderPropEnum(PropKey key, string[] enumNames, int defaultVal, int min, int max) : base(key, ValType.Enum)
		{
			this.names = enumNames;
			this.range = new EditRange<float>("F0", (float)min, (float)max);
			this.defaultVal = (float)defaultVal;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0001158E File Offset: 0x0000F78E
		public void SetValue(Material m, int enumVal)
		{
			m.SetFloat(this.propId, (float)enumVal);
		}

		// Token: 0x04000212 RID: 530
		public string[] names;
	}
}
