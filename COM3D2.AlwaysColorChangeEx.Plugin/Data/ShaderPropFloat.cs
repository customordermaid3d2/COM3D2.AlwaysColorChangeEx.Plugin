using System;
using System.Collections.Generic;
using CM3D2.AlwaysColorChangeEx.Plugin.UI.Data;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x02000035 RID: 53
	public class ShaderPropFloat : ShaderProp
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x00011401 File Offset: 0x0000F601
		public ShaderPropFloat(string name, PropKey key, int id) : base(name, key, id, ValType.Float)
		{
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0001140D File Offset: 0x0000F60D
		public ShaderPropFloat(PropKey key) : base(key, ValType.Float)
		{
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00011417 File Offset: 0x0000F617
		protected ShaderPropFloat(string name, PropKey key, int id, ValType valType) : base(name, key, id, valType)
		{
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00011424 File Offset: 0x0000F624
		protected ShaderPropFloat(PropKey key, ValType valType) : base(key, valType)
		{
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0001142E File Offset: 0x0000F62E
		public ShaderPropFloat(PropKey key, string format, IList<float> range, PresetOperation[] opts, float defaultVal, params float[] presetVals) : this(key, Keyword.NONE, format, range, opts, defaultVal, presetVals)
		{
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00011440 File Offset: 0x0000F640
		public ShaderPropFloat(PropKey key, Keyword kwd, string format, IList<float> range, PresetOperation[] opts, float defaultVal, params float[] presetVals) : this(key, kwd, new EditRange<float>(format, range[2], range[3]), range, opts, defaultVal, presetVals)
		{
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00011468 File Offset: 0x0000F668
		public ShaderPropFloat(PropKey key, EditRange<float> range, IList<float> sliderRange, PresetOperation[] opts, float defaultVal, params float[] presetVals) : this(key, Keyword.NONE, range, sliderRange, opts, defaultVal, presetVals)
		{
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0001147C File Offset: 0x0000F67C
		public ShaderPropFloat(PropKey key, Keyword kwd, EditRange<float> range, IList<float> sliderRange, PresetOperation[] opts, float defaultVal, params float[] presetVals) : base(key, ValType.Float)
		{
			this.range = range;
			this.keyword = kwd;
			this.sliderMin = sliderRange[0];
			this.sliderMax = sliderRange[1];
			this.opts = opts;
			this.presetVals = presetVals;
			this.defaultVal = defaultVal;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000114D3 File Offset: 0x0000F6D3
		public void SetValue(Material m, float val)
		{
			m.SetFloat(this.propId, val);
		}

		// Token: 0x0400020B RID: 523
		public EditRange<float> range;

		// Token: 0x0400020C RID: 524
		public readonly float sliderMin;

		// Token: 0x0400020D RID: 525
		public readonly float sliderMax;

		// Token: 0x0400020E RID: 526
		public readonly Keyword keyword;

		// Token: 0x0400020F RID: 527
		public float defaultVal;

		// Token: 0x04000210 RID: 528
		public readonly PresetOperation[] opts;

		// Token: 0x04000211 RID: 529
		public readonly float[] presetVals;
	}
}
