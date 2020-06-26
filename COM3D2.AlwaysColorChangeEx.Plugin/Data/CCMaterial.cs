using System;
using System.Collections.Generic;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x0200002E RID: 46
	public class CCMaterial
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x000104B0 File Offset: 0x0000E6B0
		static CCMaterial()
		{
			CCMaterial.COLOR_DIC.Add(PropKey._Color, (CCMaterial mate) => mate.color);
			CCMaterial.COLOR_DIC.Add(PropKey._ShadowColor, (CCMaterial mate) => mate.shadowColor);
			CCMaterial.COLOR_DIC.Add(PropKey._RimColor, (CCMaterial mate) => mate.rimColor);
			CCMaterial.COLOR_DIC.Add(PropKey._OutlineColor, (CCMaterial mate) => mate.outlineColor);
			CCMaterial.FLOAT_DIC.Add(PropKey._Shininess, (CCMaterial mate) => mate.shininess);
			CCMaterial.FLOAT_DIC.Add(PropKey._OutlineWidth, (CCMaterial mate) => mate.outlineWidth);
			CCMaterial.FLOAT_DIC.Add(PropKey._RimPower, (CCMaterial mate) => mate.rimPower);
			CCMaterial.FLOAT_DIC.Add(PropKey._RimShift, (CCMaterial mate) => mate.rimShift);
			CCMaterial.FLOAT_DIC.Add(PropKey._HiRate, (CCMaterial mate) => mate.hiRate);
			CCMaterial.FLOAT_DIC.Add(PropKey._HiPow, (CCMaterial mate) => mate.hiPow);
			CCMaterial.FLOAT_DIC.Add(PropKey._Cutoff, (CCMaterial mate) => mate.cutoff);
			CCMaterial.FLOAT_DIC.Add(PropKey._Cutout, (CCMaterial mate) => mate.cutout);
			CCMaterial.FLOAT_DIC.Add(PropKey._FloatValue1, (CCMaterial mate) => mate.floatVal1);
			CCMaterial.FLOAT_DIC.Add(PropKey._FloatValue2, (CCMaterial mate) => mate.floatVal2);
			CCMaterial.FLOAT_DIC.Add(PropKey._FloatValue3, (CCMaterial mate) => mate.floatVal3);
			CCMaterial.FLOAT_DIC.Add(PropKey._ZTest, (CCMaterial mate) => mate.ztest);
			CCMaterial.FLOAT_DIC.Add(PropKey._ZTest2, (CCMaterial mate) => mate.ztest2);
			CCMaterial.FLOAT_DIC.Add(PropKey._ZTest2Alpha, (CCMaterial mate) => mate.ztest2Alpha);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000107B3 File Offset: 0x0000E9B3
		public CCMaterial()
		{
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000107BC File Offset: 0x0000E9BC
		public CCMaterial(Material m, ShaderType type)
		{
			this.name = m.name;
			this.shader = m.shader.name;
			foreach (ShaderPropColor shaderPropColor in type.colProps)
			{
				CCColor cccolor = new CCColor(m.GetColor(shaderPropColor.propId));
				switch (shaderPropColor.key)
				{
				case PropKey._Color:
					this.color = cccolor;
					break;
				case PropKey._ShadowColor:
					this.shadowColor = cccolor;
					break;
				case PropKey._RimColor:
					this.rimColor = cccolor;
					break;
				case PropKey._OutlineColor:
					this.outlineColor = cccolor;
					break;
				}
			}
			foreach (ShaderPropFloat shaderPropFloat in type.fProps)
			{
				float @float = m.GetFloat(shaderPropFloat.propId);
				switch (shaderPropFloat.key)
				{
				case PropKey._Shininess:
					this.shininess = new float?(@float);
					break;
				case PropKey._OutlineWidth:
					this.outlineWidth = new float?(@float);
					break;
				case PropKey._RimPower:
					this.rimPower = new float?(@float);
					break;
				case PropKey._RimShift:
					this.rimShift = new float?(@float);
					break;
				case PropKey._HiRate:
					this.hiRate = new float?(@float);
					break;
				case PropKey._HiPow:
					this.hiPow = new float?(@float);
					break;
				case PropKey._FloatValue1:
					this.floatVal1 = new float?(@float);
					break;
				case PropKey._FloatValue2:
					this.floatVal2 = new float?(@float);
					break;
				case PropKey._FloatValue3:
					this.floatVal3 = new float?(@float);
					break;
				case PropKey._Cutoff:
					this.cutoff = new float?(@float);
					break;
				case PropKey._Cutout:
					this.cutout = new float?(@float);
					break;
				case PropKey._ZTest:
					this.ztest = new float?(@float);
					break;
				case PropKey._ZTest2:
					this.ztest2 = new float?(@float);
					break;
				case PropKey._ZTest2Alpha:
					this.ztest2Alpha = new float?(@float);
					break;
				}
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000109EC File Offset: 0x0000EBEC
		public bool Apply(Material m)
		{
			Shader shader = Shader.Find(this.shader);
			if (shader == null)
			{
				return false;
			}
			LogUtil.Debug(new object[]
			{
				"apply shader:",
				shader.name
			});
			m.shader = shader;
			ShaderType shaderType = ShaderType.Resolve(shader.name);
			if (shaderType == ShaderType.UNKNOWN)
			{
				return false;
			}
			foreach (ShaderPropColor shaderPropColor in shaderType.colProps)
			{
				Func<CCMaterial, CCColor> func;
				if (CCMaterial.COLOR_DIC.TryGetValue(shaderPropColor.key, out func))
				{
					m.SetColor(shaderPropColor.propId, func(this).ToColor());
				}
			}
			foreach (ShaderPropFloat shaderPropFloat in shaderType.fProps)
			{
				Func<CCMaterial, float?> func2;
				if (CCMaterial.FLOAT_DIC.TryGetValue(shaderPropFloat.key, out func2))
				{
					float? num = func2(this);
					if (num != null)
					{
						m.SetFloat(shaderPropFloat.propId, num.Value);
					}
				}
			}
			return true;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00010AFB File Offset: 0x0000ECFB
		public void Add(TextureInfo ti)
		{
			if (this.texList == null)
			{
				this.texList = new List<TextureInfo>();
			}
			this.texList.Add(ti);
		}

		// Token: 0x040001BC RID: 444
		private static readonly Dictionary<PropKey, Func<CCMaterial, CCColor>> COLOR_DIC = new Dictionary<PropKey, Func<CCMaterial, CCColor>>();

		// Token: 0x040001BD RID: 445
		private static readonly Dictionary<PropKey, Func<CCMaterial, float?>> FLOAT_DIC = new Dictionary<PropKey, Func<CCMaterial, float?>>();

		// Token: 0x040001BE RID: 446
		public string name;

		// Token: 0x040001BF RID: 447
		public string shader;

		// Token: 0x040001C0 RID: 448
		public CCColor color;

		// Token: 0x040001C1 RID: 449
		public CCColor shadowColor;

		// Token: 0x040001C2 RID: 450
		public CCColor rimColor;

		// Token: 0x040001C3 RID: 451
		public CCColor outlineColor;

		// Token: 0x040001C4 RID: 452
		public float? shininess;

		// Token: 0x040001C5 RID: 453
		public float? outlineWidth;

		// Token: 0x040001C6 RID: 454
		public float? rimPower;

		// Token: 0x040001C7 RID: 455
		public float? rimShift;

		// Token: 0x040001C8 RID: 456
		public float? hiRate;

		// Token: 0x040001C9 RID: 457
		public float? hiPow;

		// Token: 0x040001CA RID: 458
		public float? cutoff;

		// Token: 0x040001CB RID: 459
		public float? cutout;

		// Token: 0x040001CC RID: 460
		public float? floatVal1;

		// Token: 0x040001CD RID: 461
		public float? floatVal2;

		// Token: 0x040001CE RID: 462
		public float? floatVal3;

		// Token: 0x040001CF RID: 463
		public float? ztest;

		// Token: 0x040001D0 RID: 464
		public float? ztest2;

		// Token: 0x040001D1 RID: 465
		public float? ztest2Alpha;

		// Token: 0x040001D2 RID: 466
		public List<TextureInfo> texList;
	}
}
