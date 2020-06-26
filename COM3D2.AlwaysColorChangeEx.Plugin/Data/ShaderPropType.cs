using System;
using System.Collections.Generic;
using CM3D2.AlwaysColorChangeEx.Plugin.UI.Data;
using UnityEngine.Rendering;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x0200003A RID: 58
	public static class ShaderPropType
	{
		// Token: 0x060001F5 RID: 501 RVA: 0x00011678 File Offset: 0x0000F878
		public static void Initialize()
		{
			if (ShaderPropType._initialized)
			{
				return;
			}
			ShaderPropType.RenderQueue = new ShaderPropFloat(PropKey._SetManualRenderQueue, EditRange.renderQueue, new float[]
			{
				0f,
				5000f
			}, ShaderPropType.PRESET_PM, 3000f, new float[]
			{
				2000f,
				3000f
			});
			ShaderPropType.Shininess = new ShaderPropFloat(PropKey._Shininess, EditRange.shininess, ShaderPropType.settings.shininessRange(), ShaderPropType.PRESET_RATIO, 0f, new float[]
			{
				0f,
				0.1f,
				0.5f,
				1f,
				5f
			});
			ShaderPropType.OutlineWidth = new ShaderPropFloat(PropKey._OutlineWidth, EditRange.outlineWidth, ShaderPropType.settings.outlineWidthRange(), null, 0.0001f, new float[]
			{
				0.0001f,
				0.001f,
				0.002f
			});
			ShaderPropType.RimPower = new ShaderPropFloat(PropKey._RimPower, EditRange.rimPower, ShaderPropType.settings.rimPowerRange(), ShaderPropType.PRESET_INV, 0f, new float[]
			{
				0f,
				25f,
				50f,
				100f
			});
			ShaderPropType.RimShift = new ShaderPropFloat(PropKey._RimShift, EditRange.rimShift, ShaderPropType.settings.rimShiftRange(), ShaderPropType.PRESET_RATIO, 0f, new float[]
			{
				0f,
				0.25f,
				0.5f,
				1f
			});
			ShaderPropType.HiRate = new ShaderPropFloat(PropKey._HiRate, EditRange.hiRate, ShaderPropType.settings.hiRateRange(), ShaderPropType.PRESET_RATIO, 0f, new float[]
			{
				0f,
				0.5f,
				1f
			});
			ShaderPropType.HiPow = new ShaderPropFloat(PropKey._HiPow, EditRange.hiPow, ShaderPropType.settings.hiPowRange(), ShaderPropType.PRESET_RATIO, 0.001f, new float[]
			{
				0.001f,
				1f,
				50f
			});
			ShaderPropType.FloatValue1 = new ShaderPropFloat(PropKey._FloatValue1, EditRange.floatVal1, ShaderPropType.settings.hiPowRange(), null, 10f, new float[]
			{
				0f,
				100f,
				200f
			});
			ShaderPropType.FloatValue2 = new ShaderPropFloat(PropKey._FloatValue2, EditRange.floatVal2, ShaderPropType.settings.hiPowRange(), ShaderPropType.PRESET_INV, 1f, new float[]
			{
				-15f,
				0f,
				1f,
				15f
			});
			ShaderPropType.FloatValue3 = new ShaderPropFloat(PropKey._FloatValue3, EditRange.floatVal3, ShaderPropType.settings.hiPowRange(), ShaderPropType.PRESET_RATIO, 1f, new float[]
			{
				0f,
				0.5f,
				1f
			});
			ShaderPropType.Parallax = new ShaderPropFloat(PropKey._Parallax, "F3", new float[]
			{
				0.005f,
				0.08f,
				0.001f,
				0.1f
			}, ShaderPropType.PRESET_RATIO, 0.02f, new float[]
			{
				0.02f
			});
			ShaderPropType.Cutoff = new ShaderPropFloat(PropKey._Cutoff, "F3", new float[]
			{
				0f,
				1f,
				0f,
				1f
			}, ShaderPropType.PRESET_RATIO, 0.5f, new float[]
			{
				0f,
				0.5f,
				1f
			});
			ShaderPropType.Cutout = new ShaderPropFloat(PropKey._Cutout, "F3", new float[]
			{
				0f,
				1f,
				0f,
				1f
			}, ShaderPropType.PRESET_RATIO, 0.5f, new float[]
			{
				0f,
				0.5f,
				1f
			});
			ShaderPropType.EmissionLM = new ShaderPropBool(PropKey._EmissionLM);
			ShaderPropType.UseMulticolTex = new ShaderPropBool(PropKey._UseMulticolTex);
			ShaderPropType.ZTest = new ShaderPropEnum(PropKey._ZTest, typeof(CompareFunction), 4, 0, 8);
			ShaderPropType.ZTest2 = new ShaderPropBool(PropKey._ZTest2);
			ShaderPropType.ZTest2Alpha = new ShaderPropFloat(PropKey._ZTest2Alpha, "F3", new float[]
			{
				0f,
				1f,
				0f,
				1f
			}, ShaderPropType.PRESET_RATIO, 0.8f, new float[]
			{
				0f,
				0.8f,
				1f
			});
			ShaderPropType.Strength = new ShaderPropFloat(PropKey._Strength, "F2", new float[]
			{
				0f,
				1f,
				0f,
				1f
			}, ShaderPropType.PRESET_RATIO, 0.2f, new float[]
			{
				0.2f
			});
			ShaderPropType.StencilComp = new ShaderPropFloat(PropKey._StencilComp, "F0", new float[]
			{
				0f,
				255f,
				0f,
				255f
			}, ShaderPropType.PRESET_RATIO, 8f, new float[]
			{
				8f
			});
			PropKey key = PropKey._Stencil;
			string format = "F0";
			IList<float> range = new float[]
			{
				0f,
				255f,
				0f,
				255f
			};
			PresetOperation[] preset_RATIO = ShaderPropType.PRESET_RATIO;
			float defaultVal = 0f;
			float[] presetVals = new float[1];
			ShaderPropType.Stencil = new ShaderPropFloat(key, format, range, preset_RATIO, defaultVal, presetVals);
			PropKey key2 = PropKey._StencilOp;
			string format2 = "F0";
			IList<float> range2 = new float[]
			{
				0f,
				255f,
				0f,
				255f
			};
			PresetOperation[] preset_RATIO2 = ShaderPropType.PRESET_RATIO;
			float defaultVal2 = 0f;
			float[] presetVals2 = new float[1];
			ShaderPropType.StencilOp = new ShaderPropFloat(key2, format2, range2, preset_RATIO2, defaultVal2, presetVals2);
			ShaderPropType.StencilWriteMask = new ShaderPropFloat(PropKey._StencilWriteMask, "F0", new float[]
			{
				0f,
				255f,
				0f,
				255f
			}, ShaderPropType.PRESET_RATIO, 255f, new float[]
			{
				255f
			});
			ShaderPropType.StencilReadMask = new ShaderPropFloat(PropKey._StencilReadMask, "F0", new float[]
			{
				0f,
				255f,
				0f,
				255f
			}, ShaderPropType.PRESET_RATIO, 255f, new float[]
			{
				255f
			});
			ShaderPropType.ColorMask = new ShaderPropFloat(PropKey._ColorMask, "F0", new float[]
			{
				0f,
				255f,
				0f,
				255f
			}, ShaderPropType.PRESET_RATIO, 255f, new float[]
			{
				255f
			});
			PropKey key3 = PropKey._EnvAlpha;
			string format3 = "F1";
			IList<float> range3 = new float[]
			{
				0f,
				1f,
				0f,
				1f
			};
			PresetOperation[] preset_RATIO3 = ShaderPropType.PRESET_RATIO;
			float defaultVal3 = 0f;
			float[] presetVals3 = new float[1];
			ShaderPropType.EnvAlpha = new ShaderPropFloat(key3, format3, range3, preset_RATIO3, defaultVal3, presetVals3);
			ShaderPropType.EnvAdd = new ShaderPropFloat(PropKey._EnvAdd, "F1", new float[]
			{
				1f,
				2f,
				1f,
				2f
			}, ShaderPropType.PRESET_RATIO, 1f, new float[]
			{
				1f
			});
			ShaderPropType.Glossiness = new ShaderPropFloat(PropKey._Glossiness, "F3", new float[]
			{
				0f,
				1f,
				0f,
				1f
			}, ShaderPropType.PRESET_RATIO, 0.5f, new float[]
			{
				0f,
				0.5f,
				1f
			});
			ShaderPropType.GlossMapScale = new ShaderPropFloat(PropKey._GlossMapScale, "F3", new float[]
			{
				0f,
				1f,
				0f,
				1f
			}, ShaderPropType.PRESET_RATIO, 1f, new float[]
			{
				0f,
				0.5f,
				1f
			});
			ShaderPropType.SmoothnessTexChannel = new ShaderPropEnum(PropKey._SmoothnessTextureChannel, new string[]
			{
				"Metallic Alpha",
				"Albedo Alpha"
			}, 0, 0, 1);
			ShaderPropType.EmissionScaleUI = new ShaderPropFloat(PropKey._EmissionScaleUI, "F3", new float[]
			{
				0f,
				1f,
				0f,
				1f
			}, ShaderPropType.PRESET_RATIO, 0f, new float[]
			{
				0f,
				0.5f,
				1f
			});
			ShaderPropType.Metallic = new ShaderPropFloat(PropKey._Metallic, "F3", new float[]
			{
				0f,
				1f,
				0f,
				1f
			}, ShaderPropType.PRESET_RATIO, 0f, new float[]
			{
				0f,
				0.5f,
				1f
			});
			ShaderPropType.BumpScale = new ShaderPropFloat(PropKey._BumpScale, "F3", new float[]
			{
				0.1f,
				10f,
				0.01f,
				100f
			}, ShaderPropType.PRESET_RATIO, 1f, new float[]
			{
				0.1f,
				1f,
				10f
			});
			ShaderPropType.OcclusionStrength = new ShaderPropFloat(PropKey._OcclusionStrength, "F3", new float[]
			{
				0f,
				1f,
				0f,
				1f
			}, ShaderPropType.PRESET_RATIO, 1f, new float[]
			{
				0f,
				0.5f,
				1f
			});
			ShaderPropType.DetailNormalMapScale = new ShaderPropFloat(PropKey._DetailNormalMapScale, "F3", new float[]
			{
				0.1f,
				10f,
				0.01f,
				100f
			}, ShaderPropType.PRESET_RATIO, 1f, new float[]
			{
				0.1f,
				1f,
				10f
			});
			ShaderPropType.Mode = new ShaderPropFloat(PropKey._Mode, "F0", new float[]
			{
				0f,
				4f,
				0f,
				4f
			}, ShaderPropType.PRESET_EMPTY, 0f, new float[]
			{
				4f
			});
			ShaderPropType.ZWrite = new ShaderPropFloat(PropKey._ZWrite, "F0", new float[]
			{
				0f,
				1f,
				0f,
				1f
			}, ShaderPropType.PRESET_EMPTY, 0f, new float[]
			{
				1f
			});
			ShaderPropType.UVSec = new ShaderPropEnum(PropKey._UVSec, new string[]
			{
				"UV0",
				"UV1"
			}, 0, 0, 1);
			ShaderPropType.SrcBlend = new ShaderPropEnum(PropKey._SrcBlend, typeof(BlendMode), 0, 0, 1);
			ShaderPropType.DstBlend = new ShaderPropEnum(PropKey._DstBlend, typeof(BlendMode), 1, 0, 1);
			ShaderPropType.SpecularHeighlights = new ShaderPropBool(PropKey._SpecularHighlights);
			ShaderPropType.GlossyReflections = new ShaderPropBool(PropKey._GlossyReflections);
			ShaderPropType._initialized = true;
		}

		// Token: 0x04000217 RID: 535
		private static readonly PresetOperation sliderL = new PresetOperation("<", (float val) => val * 0.9f);

		// Token: 0x04000218 RID: 536
		private static readonly PresetOperation sliderR = new PresetOperation(">", (float val) => val * 1.1f);

		// Token: 0x04000219 RID: 537
		private static readonly PresetOperation invert = new PresetOperation("x-1", (float val) => val * -1f);

		// Token: 0x0400021A RID: 538
		private static readonly PresetOperation plus1 = new PresetOperation("+", (float val) => val + 1f);

		// Token: 0x0400021B RID: 539
		private static readonly PresetOperation plus10 = new PresetOperation("++", (float val) => val + 10f);

		// Token: 0x0400021C RID: 540
		private static readonly PresetOperation minus1 = new PresetOperation("-", (float val) => val - 1f);

		// Token: 0x0400021D RID: 541
		private static readonly PresetOperation minus10 = new PresetOperation("--", (float val) => val - 10f);

		// Token: 0x0400021E RID: 542
		private static readonly PresetOperation[] PRESET_EMPTY = new PresetOperation[0];

		// Token: 0x0400021F RID: 543
		private static readonly PresetOperation[] PRESET_RATIO = new PresetOperation[]
		{
			ShaderPropType.sliderL,
			ShaderPropType.sliderR
		};

		// Token: 0x04000220 RID: 544
		private static readonly PresetOperation[] PRESET_INV = new PresetOperation[]
		{
			ShaderPropType.invert
		};

		// Token: 0x04000221 RID: 545
		private static readonly PresetOperation[] PRESET_PM = new PresetOperation[]
		{
			ShaderPropType.minus10,
			ShaderPropType.minus1,
			ShaderPropType.plus1,
			ShaderPropType.plus10
		};

		// Token: 0x04000222 RID: 546
		private static readonly Settings settings = Settings.Instance;

		// Token: 0x04000223 RID: 547
		private static bool _initialized;

		// Token: 0x04000224 RID: 548
		internal static ShaderPropFloat RenderQueue;

		// Token: 0x04000225 RID: 549
		internal static ShaderPropFloat Shininess;

		// Token: 0x04000226 RID: 550
		internal static ShaderPropFloat OutlineWidth;

		// Token: 0x04000227 RID: 551
		internal static ShaderPropFloat RimPower;

		// Token: 0x04000228 RID: 552
		internal static ShaderPropFloat RimShift;

		// Token: 0x04000229 RID: 553
		internal static ShaderPropFloat HiRate;

		// Token: 0x0400022A RID: 554
		internal static ShaderPropFloat HiPow;

		// Token: 0x0400022B RID: 555
		internal static ShaderPropFloat FloatValue1;

		// Token: 0x0400022C RID: 556
		internal static ShaderPropFloat FloatValue2;

		// Token: 0x0400022D RID: 557
		internal static ShaderPropFloat FloatValue3;

		// Token: 0x0400022E RID: 558
		internal static ShaderPropFloat Parallax;

		// Token: 0x0400022F RID: 559
		internal static ShaderPropFloat Cutoff;

		// Token: 0x04000230 RID: 560
		internal static ShaderPropFloat Cutout;

		// Token: 0x04000231 RID: 561
		internal static ShaderPropEnum ZTest;

		// Token: 0x04000232 RID: 562
		internal static ShaderPropBool ZTest2;

		// Token: 0x04000233 RID: 563
		internal static ShaderPropFloat ZTest2Alpha;

		// Token: 0x04000234 RID: 564
		internal static ShaderPropBool EmissionLM;

		// Token: 0x04000235 RID: 565
		internal static ShaderPropBool UseMulticolTex;

		// Token: 0x04000236 RID: 566
		internal static ShaderPropFloat Strength;

		// Token: 0x04000237 RID: 567
		internal static ShaderPropFloat StencilComp;

		// Token: 0x04000238 RID: 568
		internal static ShaderPropFloat Stencil;

		// Token: 0x04000239 RID: 569
		internal static ShaderPropFloat StencilOp;

		// Token: 0x0400023A RID: 570
		internal static ShaderPropFloat StencilWriteMask;

		// Token: 0x0400023B RID: 571
		internal static ShaderPropFloat StencilReadMask;

		// Token: 0x0400023C RID: 572
		internal static ShaderPropFloat ColorMask;

		// Token: 0x0400023D RID: 573
		internal static ShaderPropFloat EnvAlpha;

		// Token: 0x0400023E RID: 574
		internal static ShaderPropFloat EnvAdd;

		// Token: 0x0400023F RID: 575
		internal static ShaderPropFloat EmissionScaleUI;

		// Token: 0x04000240 RID: 576
		internal static ShaderPropFloat Glossiness;

		// Token: 0x04000241 RID: 577
		internal static ShaderPropFloat GlossMapScale;

		// Token: 0x04000242 RID: 578
		internal static ShaderPropEnum SmoothnessTexChannel;

		// Token: 0x04000243 RID: 579
		internal static ShaderPropFloat Metallic;

		// Token: 0x04000244 RID: 580
		internal static ShaderPropFloat BumpScale;

		// Token: 0x04000245 RID: 581
		internal static ShaderPropFloat OcclusionStrength;

		// Token: 0x04000246 RID: 582
		internal static ShaderPropFloat DetailNormalMapScale;

		// Token: 0x04000247 RID: 583
		internal static ShaderPropEnum UVSec;

		// Token: 0x04000248 RID: 584
		internal static ShaderPropFloat Mode;

		// Token: 0x04000249 RID: 585
		internal static ShaderPropEnum SrcBlend;

		// Token: 0x0400024A RID: 586
		internal static ShaderPropEnum DstBlend;

		// Token: 0x0400024B RID: 587
		internal static ShaderPropFloat ZWrite;

		// Token: 0x0400024C RID: 588
		internal static ShaderPropBool SpecularHeighlights;

		// Token: 0x0400024D RID: 589
		internal static ShaderPropBool GlossyReflections;

		// Token: 0x0400024E RID: 590
		internal static readonly ShaderPropColor Color = new ShaderPropColor(PropKey._Color, ColorType.rgb, true, Keyword.NONE);

		// Token: 0x0400024F RID: 591
		internal static readonly ShaderPropColor ColorA = new ShaderPropColor(PropKey._Color, ColorType.rgba, true, Keyword.NONE);

		// Token: 0x04000250 RID: 592
		internal static readonly ShaderPropColor ShadowColor = new ShaderPropColor(PropKey._ShadowColor, ColorType.rgb, true, Keyword.NONE);

		// Token: 0x04000251 RID: 593
		internal static readonly ShaderPropColor RimColor = new ShaderPropColor(PropKey._RimColor, ColorType.rgb, true, Keyword.NONE);

		// Token: 0x04000252 RID: 594
		internal static readonly ShaderPropColor OutlineColor = new ShaderPropColor(PropKey._OutlineColor, ColorType.rgb, false, Keyword.NONE);

		// Token: 0x04000253 RID: 595
		internal static readonly ShaderPropColor SpecColor = new ShaderPropColor(PropKey._SpecColor, ColorType.rgb, false, Keyword.NONE);

		// Token: 0x04000254 RID: 596
		internal static readonly ShaderPropColor ReflectColor = new ShaderPropColor(PropKey._ReflectColor, ColorType.rgba, false, Keyword.NONE);

		// Token: 0x04000255 RID: 597
		internal static readonly ShaderPropColor EmissionColor = new ShaderPropColor(PropKey._EmissionColor, ColorType.rgb, false, Keyword.NONE);

		// Token: 0x04000256 RID: 598
		internal static readonly ShaderPropTex MainTex = new ShaderPropTex(PropKey._MainTex, TexType.rgb, Keyword.NONE);

		// Token: 0x04000257 RID: 599
		internal static readonly ShaderPropTex MainTexA = new ShaderPropTex(PropKey._MainTex, TexType.rgba, Keyword.NONE);

		// Token: 0x04000258 RID: 600
		internal static readonly ShaderPropTex ToonRamp = new ShaderPropTex(PropKey._ToonRamp, TexType.rgb, Keyword.NONE);

		// Token: 0x04000259 RID: 601
		internal static readonly ShaderPropTex ShadowTex = new ShaderPropTex(PropKey._ShadowTex, TexType.rgb, Keyword.NONE);

		// Token: 0x0400025A RID: 602
		internal static readonly ShaderPropTex ShadowRateToon = new ShaderPropTex(PropKey._ShadowRateToon, TexType.rgb, Keyword.NONE);

		// Token: 0x0400025B RID: 603
		internal static readonly ShaderPropTex OutlineTex = new ShaderPropTex(PropKey._OutlineTex, TexType.rgb, Keyword.NONE);

		// Token: 0x0400025C RID: 604
		internal static readonly ShaderPropTex OutlineToonRamp = new ShaderPropTex(PropKey._OutlineToonRamp, TexType.rgb, Keyword.NONE);

		// Token: 0x0400025D RID: 605
		internal static readonly ShaderPropTex HiTex = new ShaderPropTex(PropKey._HiTex, TexType.rgb, Keyword.NONE);

		// Token: 0x0400025E RID: 606
		internal static readonly ShaderPropTex RenderTex = new ShaderPropTex(PropKey._RenderTex, TexType.nulltex, Keyword.NONE);

		// Token: 0x0400025F RID: 607
		internal static readonly ShaderPropTex BumpMap = new ShaderPropTex(PropKey._BumpMap, TexType.bump, Keyword._NORMALMAP);

		// Token: 0x04000260 RID: 608
		internal static readonly ShaderPropTex SpecularTex = new ShaderPropTex(PropKey._SpecularTex, TexType.nulltex, Keyword.NONE);

		// Token: 0x04000261 RID: 609
		internal static readonly ShaderPropTex DecalTex = new ShaderPropTex(PropKey._DecalTex, TexType.rgba, Keyword.NONE);

		// Token: 0x04000262 RID: 610
		internal static readonly ShaderPropTex Detail = new ShaderPropTex(PropKey._Detail, TexType.rgb, Keyword.NONE);

		// Token: 0x04000263 RID: 611
		internal static readonly ShaderPropTex DetailTex = new ShaderPropTex(PropKey._DetailTex, TexType.rgb, Keyword.NONE);

		// Token: 0x04000264 RID: 612
		internal static readonly ShaderPropTex AnisoTex = new ShaderPropTex(PropKey._AnisoTex, TexType.nulltex, Keyword.NONE);

		// Token: 0x04000265 RID: 613
		internal static readonly ShaderPropTex Illum = new ShaderPropTex(PropKey._Illum, TexType.a, Keyword.NONE);

		// Token: 0x04000266 RID: 614
		internal static readonly ShaderPropTex Cube = new ShaderPropTex(PropKey._Cube, TexType.cube, Keyword.NONE);

		// Token: 0x04000267 RID: 615
		internal static readonly ShaderPropTex ReflectionTex = new ShaderPropTex(PropKey._ReflectionTex, TexType.rgb, Keyword.NONE);

		// Token: 0x04000268 RID: 616
		internal static readonly ShaderPropTex MultiColTex = new ShaderPropTex(PropKey._MultiColTex, TexType.rgba, Keyword.NONE);

		// Token: 0x04000269 RID: 617
		internal static readonly ShaderPropTex EnvMap = new ShaderPropTex(PropKey._EnvMap, TexType.cube, Keyword.NONE);

		// Token: 0x0400026A RID: 618
		internal static readonly ShaderPropTex MetallicGlossMap = new ShaderPropTex(PropKey._MetallicGlossMap, TexType.rgb, Keyword._METALLICGLOSSMAP);

		// Token: 0x0400026B RID: 619
		internal static readonly ShaderPropTex OcclusionMap = new ShaderPropTex(PropKey._OcclusionMap, TexType.rgb, Keyword.NONE);

		// Token: 0x0400026C RID: 620
		internal static readonly ShaderPropTex ParallaxMap = new ShaderPropTex(PropKey._ParallaxMap, TexType.a, Keyword._PARALLAXMAP);

		// Token: 0x0400026D RID: 621
		internal static readonly ShaderPropTex EmissionMap = new ShaderPropTex(PropKey._EmissionMap, TexType.a, Keyword._EMISSION);

		// Token: 0x0400026E RID: 622
		internal static readonly ShaderPropTex SpecGlossMap = new ShaderPropTex(PropKey._SpecGlossMap, TexType.rgb, Keyword._SPECGLOSSMAP);

		// Token: 0x0400026F RID: 623
		internal static readonly ShaderPropTex DetailMask = new ShaderPropTex(PropKey._DetailMask, TexType.rgb, Keyword.NONE);

		// Token: 0x04000270 RID: 624
		internal static readonly ShaderPropTex DetailAlbedoMap = new ShaderPropTex(PropKey._DetailAlbedoMap, TexType.rgb, Keyword._DETAIL_MULX2);

		// Token: 0x04000271 RID: 625
		internal static readonly ShaderPropTex DetailNormalMap = new ShaderPropTex(PropKey._DetailNormalMap, TexType.bump, Keyword._NORMALMAP);
	}
}
