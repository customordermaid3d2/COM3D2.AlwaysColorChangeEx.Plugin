using System;
using System.Collections.Generic;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x02000041 RID: 65
	public class ShaderType
	{
		// Token: 0x060001FE RID: 510 RVA: 0x00012494 File Offset: 0x00010694
		public static ShaderType Resolve(string name)
		{
			ShaderType unknown;
			if (ShaderType.shaderMap.TryGetValue(name, out unknown))
			{
				return unknown;
			}
			LogUtil.Log(new object[]
			{
				"未対応シェーダのため、シェーダタイプが特定できません。",
				name
			});
			unknown = ShaderType.UNKNOWN;
			return unknown;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000124D4 File Offset: 0x000106D4
		public static ShaderType Resolve(int shaderIdx)
		{
			if (shaderIdx < ShaderType.shaders.Length && shaderIdx >= 0)
			{
				return ShaderType.shaders[shaderIdx];
			}
			LogUtil.Log(new object[]
			{
				"指定シェーダのインデックスが範囲外のため、シェーダタイプが特定できません。",
				shaderIdx
			});
			return ShaderType.UNKNOWN;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0001251C File Offset: 0x0001071C
		public static string GetMateName(string shader1)
		{
			string result;
			if (!ShaderType.shader2Map.TryGetValue(shader1, out result))
			{
				return string.Empty;
			}
			return result;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0001253F File Offset: 0x0001073F
		public static int MaxNameLength()
		{
			return ShaderType.shaders[ShaderType.SHADER_TYPE_CM3D2_MAX].name.Length;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00012558 File Offset: 0x00010758
		static ShaderType()
		{
			ShaderPropTex[] array = new ShaderPropTex[0];
			ShaderPropTex[] array2 = new ShaderPropTex[]
			{
				ShaderPropType.RenderTex
			};
			ShaderPropTex[] array3 = new ShaderPropTex[]
			{
				ShaderPropType.MainTex
			};
			ShaderPropTex[] array4 = new ShaderPropTex[]
			{
				ShaderPropType.MainTexA
			};
			ShaderPropTex[] array5 = new ShaderPropTex[]
			{
				ShaderPropType.MainTex,
				ShaderPropType.ToonRamp,
				ShaderPropType.ShadowTex,
				ShaderPropType.ShadowRateToon
			};
			ShaderPropTex[] array6 = new ShaderPropTex[]
			{
				ShaderPropType.MainTex,
				ShaderPropType.ToonRamp,
				ShaderPropType.ShadowTex,
				ShaderPropType.ShadowRateToon,
				ShaderPropType.OutlineTex,
				ShaderPropType.OutlineToonRamp
			};
			ShaderPropTex[] array7 = new ShaderPropTex[]
			{
				ShaderPropType.MainTexA,
				ShaderPropType.ToonRamp,
				ShaderPropType.ShadowTex,
				ShaderPropType.ShadowRateToon
			};
			ShaderPropTex[] array8 = new ShaderPropTex[]
			{
				ShaderPropType.MainTex,
				ShaderPropType.ToonRamp,
				ShaderPropType.ShadowTex,
				ShaderPropType.ShadowRateToon,
				ShaderPropType.HiTex
			};
			ShaderPropTex[] array9 = new ShaderPropTex[]
			{
				ShaderPropType.MainTex,
				ShaderPropType.ToonRamp,
				ShaderPropType.ShadowTex,
				ShaderPropType.ShadowRateToon,
				ShaderPropType.HiTex,
				ShaderPropType.OutlineTex,
				ShaderPropType.OutlineToonRamp
			};
			ShaderPropTex[] array10 = new ShaderPropTex[]
			{
				ShaderPropType.MainTexA,
				ShaderPropType.OcclusionMap,
				ShaderPropType.MetallicGlossMap,
				ShaderPropType.BumpMap,
				ShaderPropType.ParallaxMap,
				ShaderPropType.EmissionMap,
				ShaderPropType.DetailMask,
				ShaderPropType.DetailAlbedoMap,
				ShaderPropType.DetailNormalMap,
				ShaderPropType.SpecGlossMap
			};
			ShaderPropColor[] array11 = new ShaderPropColor[0];
			ShaderPropColor[] array12 = new ShaderPropColor[]
			{
				ShaderPropType.Color
			};
			ShaderPropColor[] array13 = new ShaderPropColor[]
			{
				ShaderPropType.ColorA
			};
			ShaderPropColor[] array14 = new ShaderPropColor[]
			{
				ShaderPropType.Color,
				ShaderPropType.ShadowColor
			};
			ShaderPropColor[] array15 = new ShaderPropColor[]
			{
				ShaderPropType.ColorA,
				ShaderPropType.ShadowColor
			};
			ShaderPropColor[] array16 = new ShaderPropColor[]
			{
				ShaderPropType.Color,
				ShaderPropType.ShadowColor,
				ShaderPropType.RimColor
			};
			ShaderPropColor[] array17 = new ShaderPropColor[]
			{
				ShaderPropType.ColorA,
				ShaderPropType.ShadowColor,
				ShaderPropType.RimColor
			};
			ShaderPropColor[] array18 = new ShaderPropColor[]
			{
				ShaderPropType.Color,
				ShaderPropType.ShadowColor,
				ShaderPropType.RimColor,
				ShaderPropType.OutlineColor
			};
			ShaderPropColor[] array19 = new ShaderPropColor[]
			{
				ShaderPropType.ColorA,
				ShaderPropType.ShadowColor,
				ShaderPropType.RimColor,
				ShaderPropType.OutlineColor
			};
			ShaderPropFloat[] props = new ShaderPropFloat[0];
			ShaderPropFloat[] props2 = new ShaderPropFloat[]
			{
				ShaderPropType.Shininess
			};
			ShaderPropFloat[] props3 = new ShaderPropFloat[]
			{
				ShaderPropType.Shininess,
				ShaderPropType.Cutoff
			};
			ShaderPropFloat[] props4 = new ShaderPropFloat[]
			{
				ShaderPropType.Shininess,
				ShaderPropType.RimPower,
				ShaderPropType.RimShift
			};
			ShaderPropFloat[] props5 = new ShaderPropFloat[]
			{
				ShaderPropType.Shininess,
				ShaderPropType.RimPower,
				ShaderPropType.RimShift,
				ShaderPropType.ZTest,
				ShaderPropType.ZTest2,
				ShaderPropType.ZTest2Alpha
			};
			ShaderPropFloat[] props6 = new ShaderPropFloat[]
			{
				ShaderPropType.Shininess,
				ShaderPropType.RimPower,
				ShaderPropType.RimShift,
				ShaderPropType.Cutoff
			};
			ShaderPropFloat[] props7 = new ShaderPropFloat[]
			{
				ShaderPropType.Shininess,
				ShaderPropType.OutlineWidth,
				ShaderPropType.RimPower,
				ShaderPropType.RimShift
			};
			ShaderPropFloat[] props8 = new ShaderPropFloat[]
			{
				ShaderPropType.Shininess,
				ShaderPropType.RimPower,
				ShaderPropType.RimShift,
				ShaderPropType.HiRate,
				ShaderPropType.HiPow
			};
			ShaderPropFloat[] props9 = new ShaderPropFloat[]
			{
				ShaderPropType.Shininess,
				ShaderPropType.OutlineWidth,
				ShaderPropType.RimPower,
				ShaderPropType.RimShift,
				ShaderPropType.HiRate,
				ShaderPropType.HiPow
			};
			ShaderPropFloat[] props10 = new ShaderPropFloat[]
			{
				ShaderPropType.Cutoff,
				ShaderPropType.OcclusionStrength,
				ShaderPropType.Glossiness,
				ShaderPropType.GlossMapScale,
				ShaderPropType.SpecularHeighlights,
				ShaderPropType.GlossyReflections,
				ShaderPropType.BumpScale,
				ShaderPropType.DetailNormalMapScale
			};
			ShaderType.count = 0;
			List<ShaderType> list = new List<ShaderType>
			{
				new ShaderType("CM3D2/Toony_Lighted", "トゥーン", array5, array16, props4, false),
				new ShaderType("CM3D2/Toony_Lighted_Trans", "トゥーン 透過", array7, array17, props6, true),
				new ShaderType("CM3D2/Toony_Lighted_Trans_NoZ", "トゥーン 透過 NoZ", array7, array17, props4, true),
				new ShaderType("CM3D2/Toony_Lighted_Trans_NoZTest", "トゥーン 透過 NoZTest", array7, array17, props5, true),
				new ShaderType("CM3D2/Toony_Lighted_Outline", "トゥーン 輪郭線", array5, array18, props7, false),
				new ShaderType("CM3D2/Toony_Lighted_Outline_Trans", "トゥーン 輪郭線 透過", array7, array19, props7, true),
				new ShaderType("CM3D2/Toony_Lighted_Outline_Tex", "トゥーン 輪郭線 Tex", array6, array18, props7, false),
				new ShaderType("CM3D2/Toony_Lighted_Hair", "トゥーン 髪", array8, array16, props8, false),
				new ShaderType("CM3D2/Toony_Lighted_Hair_Outline", "トゥーン 髪 輪郭線", array8, array18, props9, false),
				new ShaderType("CM3D2/Toony_Lighted_Hair_Outline_Tex", "トゥーン 髪 輪郭線 Tex", array9, array18, props9, false),
				new ShaderType("CM3D2/Toony_Lighted_Cutout_AtC", "トゥーン Cutout", array7, array17, props6, true),
				new ShaderType("CM3D2/Lighted", "非トゥーン", array3, array14, props2, false),
				new ShaderType("CM3D2/Lighted_Cutout_AtC", "非トゥーン Cutout", array4, array15, props3, false),
				new ShaderType("CM3D2/Lighted_Trans", "透過", array4, array15, props2, true),
				new ShaderType("Unlit/Texture", "発光", array3, array11, props, false),
				new ShaderType("Unlit/Transparent", "発光 透過", array4, array11, props, true),
				new ShaderType("Diffuse", "リアル", array3, array12, props, false),
				new ShaderType("Transparent/Diffuse", "リアル 透過", array4, array13, props, true),
				new ShaderType("CM3D2/Mosaic", "モザイク", array2, array11, new ShaderPropFloat[]
				{
					ShaderPropType.FloatValue1
				}, false),
				new ShaderType("CM3D2/Man", "ご主人様", array, array12, new ShaderPropFloat[]
				{
					ShaderPropType.FloatValue2,
					ShaderPropType.FloatValue3
				}, false),
				new ShaderType("CM3D2_Debug/Debug_CM3D2_Normal2Color", "法線", array, array12, props, false),
				new ShaderType("Standard", "Standard", array10, array12, props10, false)
			};
			IList<ShaderType> list2 = null;
			ShaderType.STANDARD = list[list.Count - 1];
			foreach (ShaderType shaderType in list)
			{
				Shader x = Shader.Find(shaderType.name);
				if (x == null)
				{
					if (list2 == null)
					{
						list2 = new List<ShaderType>();
					}
					list2.Add(shaderType);
				}
			}
			if (!Settings.Instance.enableStandard)
			{
				list.Remove(ShaderType.STANDARD);
			}
			if (list2 != null)
			{
				foreach (ShaderType item in list2)
				{
					list.Remove(item);
				}
			}
			ShaderType.shaders = list.ToArray();
			for (int i = 0; i < ShaderType.shaders.Length; i++)
			{
				ShaderType.shaders[i].idx = i;
			}
			int num = list.IndexOf(ShaderType.STANDARD);
			if (num != -1)
			{
				ShaderType.SHADER_TYPE_STANDARD = num;
				ShaderType.SHADER_TYPE_CM3D2_MAX = ShaderType.SHADER_TYPE_STANDARD - 1;
			}
			ShaderType.shaderMap = new Dictionary<string, ShaderType>(ShaderType.shaders.Length + 2);
			foreach (ShaderType shaderType2 in ShaderType.shaders)
			{
				ShaderType.shaderMap[shaderType2.name] = shaderType2;
			}
			ShaderType.shaderMap["Legacy Shaders/Transparent/Diffuse"] = ShaderType.shaderMap["Transparent/Diffuse"];
			ShaderType.shaderMap["Legacy Shaders/Diffuse"] = ShaderType.shaderMap["Diffuse"];
			ShaderType.shader2Map = new Dictionary<string, string>(ShaderType.shaders.Length + 1);
			foreach (ShaderType shaderType3 in ShaderType.shaders)
			{
				ShaderType.shader2Map[shaderType3.name] = shaderType3.name.Replace("/", "__");
			}
			ShaderType.shader2Map["CM3D2/Toony_Lighted_Hair_Outline_Tex"] = "CM3D2__Toony_Lighted_Hair_Outline";
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00012F08 File Offset: 0x00011108
		private ShaderType()
		{
			this.idx = -1;
			this.name = string.Empty;
			this.dispName = string.Empty;
			this.texProps = new ShaderPropTex[0];
			this.colProps = new ShaderPropColor[0];
			this.fProps = new ShaderPropFloat[0];
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00012F5C File Offset: 0x0001115C
		internal ShaderType(string name, string dispName, ShaderPropTex[] texProps, ShaderPropColor[] colProps, ShaderPropFloat[] props, bool isTrans = false)
		{
			this.name = name;
			this.dispName = dispName;
			this.texProps = texProps;
			this.colProps = colProps;
			this.fProps = props;
			this.isTrans = isTrans;
			if (colProps != null)
			{
				foreach (ShaderPropColor shaderPropColor in colProps)
				{
					if (shaderPropColor == ShaderPropType.ShadowColor)
					{
						this.hasShadow = true;
						return;
					}
				}
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00012FC5 File Offset: 0x000111C5
		public int KeyCount()
		{
			return this.texProps.Length + this.colProps.Length + this.fProps.Length;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00012FE4 File Offset: 0x000111E4
		public ShaderProp GetShaderProp(string propName)
		{
			try
			{
				PropKey propKey = (PropKey)Enum.Parse(typeof(PropKey), propName);
				switch (propKey)
				{
				case PropKey._MainTex:
				case PropKey._ToonRamp:
				case PropKey._ShadowTex:
				case PropKey._ShadowRateToon:
				case PropKey._OutlineTex:
				case PropKey._OutlineToonRamp:
				case PropKey._HiTex:
				case PropKey._RenderTex:
				case PropKey._BumpMap:
				case PropKey._SpecularTex:
				case PropKey._DecalTex:
				case PropKey._Detail:
				case PropKey._DetailTex:
				case PropKey._AnisoTex:
				case PropKey._ParallaxMap:
				case PropKey._Illum:
				case PropKey._Cube:
				case PropKey._ReflectionTex:
				case PropKey._MultiColTex:
				case PropKey._EnvMap:
					foreach (ShaderPropTex shaderPropTex in this.texProps)
					{
						if (shaderPropTex.key == propKey)
						{
							return shaderPropTex;
						}
					}
					break;
				case PropKey._Color:
				case PropKey._ShadowColor:
				case PropKey._RimColor:
				case PropKey._OutlineColor:
				case PropKey._SpecColor:
				case PropKey._ReflectColor:
				case PropKey._EmissionColor:
					foreach (ShaderPropColor shaderPropColor in this.colProps)
					{
						if (shaderPropColor.key == propKey)
						{
							return shaderPropColor;
						}
					}
					break;
				case PropKey._Shininess:
				case PropKey._OutlineWidth:
				case PropKey._RimPower:
				case PropKey._RimShift:
				case PropKey._HiRate:
				case PropKey._HiPow:
				case PropKey._FloatValue1:
				case PropKey._FloatValue2:
				case PropKey._FloatValue3:
				case PropKey._Parallax:
				case PropKey._Cutoff:
				case PropKey._Cutout:
				case PropKey._EmissionLM:
				case PropKey._UseMulticolTex:
				case PropKey._Strength:
				case PropKey._StencilComp:
				case PropKey._Stencil:
				case PropKey._StencilOp:
				case PropKey._StencilWriteMask:
				case PropKey._StencilReadMask:
				case PropKey._ColorMask:
				case PropKey._EnvAlpha:
				case PropKey._EnvAdd:
				case PropKey._ZTest:
				case PropKey._ZTest2:
				case PropKey._ZTest2Alpha:
					foreach (ShaderPropFloat shaderPropFloat in this.fProps)
					{
						if (shaderPropFloat.key == propKey)
						{
							return shaderPropFloat;
						}
					}
					break;
				case PropKey._SetManualRenderQueue:
					return ShaderPropType.RenderQueue;
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x040002EB RID: 747
		public static readonly int SHADER_TYPE_CM3D2_MAX;

		// Token: 0x040002EC RID: 748
		public static readonly int SHADER_TYPE_STANDARD;

		// Token: 0x040002ED RID: 749
		public static readonly ShaderType UNKNOWN = new ShaderType();

		// Token: 0x040002EE RID: 750
		public static ShaderType STANDARD;

		// Token: 0x040002EF RID: 751
		public static readonly ShaderType[] shaders;

		// Token: 0x040002F0 RID: 752
		private static readonly Dictionary<string, string> shader2Map;

		// Token: 0x040002F1 RID: 753
		private static readonly Dictionary<string, ShaderType> shaderMap;

		// Token: 0x040002F2 RID: 754
		internal static int count;

		// Token: 0x040002F3 RID: 755
		public int idx;

		// Token: 0x040002F4 RID: 756
		public bool isTrans;

		// Token: 0x040002F5 RID: 757
		public string name;

		// Token: 0x040002F6 RID: 758
		public string dispName;

		// Token: 0x040002F7 RID: 759
		public ShaderPropTex[] texProps;

		// Token: 0x040002F8 RID: 760
		public ShaderPropColor[] colProps;

		// Token: 0x040002F9 RID: 761
		public ShaderPropFloat[] fProps;

		// Token: 0x040002FA RID: 762
		public bool hasShadow;
	}
}
