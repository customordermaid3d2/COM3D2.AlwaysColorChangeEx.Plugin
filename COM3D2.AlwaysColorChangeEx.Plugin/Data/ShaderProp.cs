using System;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x02000033 RID: 51
	public abstract class ShaderProp
	{
		// Token: 0x060001DA RID: 474 RVA: 0x000112D4 File Offset: 0x0000F4D4
		protected ShaderProp(string name, PropKey key, int id, ValType valType)
		{
			this.name = name;
			this.key = key;
			this.keyName = key.ToString();
			this.propId = id;
			this.Init(valType);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00011320 File Offset: 0x0000F520
		protected ShaderProp(PropKey key, ValType valType)
		{
			this.key = key;
			this.keyName = key.ToString();
			this.name = this.keyName.Substring(1);
			this.propId = Shader.PropertyToID(this.keyName);
			this.Init(valType);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00011380 File Offset: 0x0000F580
		private void Init(ValType valType1)
		{
			this.valType = valType1;
			switch (valType1)
			{
			case ValType.Tex:
				this.type = PropType.tex;
				return;
			case ValType.Color:
				this.type = PropType.col;
				return;
			case ValType.Float:
			case ValType.Bool:
				this.type = PropType.f;
				return;
			default:
				return;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001DD RID: 477 RVA: 0x000113C4 File Offset: 0x0000F5C4
		// (set) Token: 0x060001DE RID: 478 RVA: 0x000113CC File Offset: 0x0000F5CC
		public Keyword Keyword
		{
			get
			{
				return this._keyword;
			}
			set
			{
				this._keyword = value;
				this.KeywordString = this.Keyword.ToString();
			}
		}

		// Token: 0x04000201 RID: 513
		private Keyword _keyword;

		// Token: 0x04000202 RID: 514
		public string KeywordString = string.Empty;

		// Token: 0x04000203 RID: 515
		public readonly string name;

		// Token: 0x04000204 RID: 516
		public readonly PropKey key;

		// Token: 0x04000205 RID: 517
		public readonly string keyName;

		// Token: 0x04000206 RID: 518
		public readonly int propId;

		// Token: 0x04000207 RID: 519
		public PropType type;

		// Token: 0x04000208 RID: 520
		public ValType valType;
	}
}
