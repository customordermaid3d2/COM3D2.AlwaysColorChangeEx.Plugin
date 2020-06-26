using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin
{
	// Token: 0x0200000D RID: 13
	public sealed class Settings
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00007F44 File Offset: 0x00006144
		public float[] shininessRange()
		{
			return new float[]
			{
				this.shininessMin,
				this.shininessMax
			};
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00007F6C File Offset: 0x0000616C
		public float[] outlineWidthRange()
		{
			return new float[]
			{
				this.outlineWidthMin,
				this.outlineWidthMax
			};
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00007F94 File Offset: 0x00006194
		public float[] rimPowerRange()
		{
			return new float[]
			{
				this.rimPowerMin,
				this.rimPowerMax
			};
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00007FBC File Offset: 0x000061BC
		public float[] rimShiftRange()
		{
			return new float[]
			{
				this.rimShiftMin,
				this.rimShiftMax
			};
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00007FE4 File Offset: 0x000061E4
		public float[] hiRateRange()
		{
			return new float[]
			{
				this.hiRateMin,
				this.hiRateMax
			};
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000800C File Offset: 0x0000620C
		public float[] hiPowRange()
		{
			return new float[]
			{
				this.hiPowMin,
				this.hiPowMax
			};
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00008034 File Offset: 0x00006234
		public float[] floatVal1Range()
		{
			return new float[]
			{
				this.floatVal1Min,
				this.floatVal1Max
			};
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000805C File Offset: 0x0000625C
		public float[] floatVal2Range()
		{
			return new float[]
			{
				this.floatVal2Min,
				this.floatVal2Max
			};
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00008084 File Offset: 0x00006284
		public float[] floatVal3Range()
		{
			return new float[]
			{
				this.floatVal3Min,
				this.floatVal3Max
			};
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000080BC File Offset: 0x000062BC
		public void Load(Func<string, string> getValue)
		{
			Settings.Get(getValue("PresetPath"), ref this.presetPath);
			Settings.Get(getValue("PresetDirPath"), ref this.presetDirPath);
			Settings.GetKeyCode(getValue("ToggleWindow"), ref this.toggleKey);
			string text = null;
			if (Settings.Get(getValue("ToggleWindowModifier"), ref text) && text != null)
			{
				text = text.ToLower();
				if (text.Contains("alt"))
				{
					this.toggleModifiers |= EventModifiers.Alt;
				}
				if (text.Contains("control"))
				{
					this.toggleModifiers |= EventModifiers.Control;
				}
				if (text.Contains("shift"))
				{
					this.toggleModifiers |= EventModifiers.Shift;
				}
			}
			Settings.GetKeyCode(getValue("PrevKey"), ref this.prevKey);
			Settings.GetKeyCode(getValue("NextKey"), ref this.nextKey);
			Settings.Get(getValue("SliderShininessMax"), ref this.shininessMax);
			Settings.Get(getValue("SliderShininessMin"), ref this.shininessMin);
			Settings.Get(getValue("SliderOutlineWidthMax"), ref this.outlineWidthMax);
			Settings.Get(getValue("SliderOutlineWidthMin"), ref this.outlineWidthMin);
			Settings.Get(getValue("SliderRimPowerMax"), ref this.rimPowerMax);
			Settings.Get(getValue("SliderRimPowerMin"), ref this.rimPowerMin);
			Settings.Get(getValue("SliderRimShiftMax"), ref this.rimShiftMax);
			Settings.Get(getValue("SliderRimShiftMin"), ref this.rimShiftMin);
			Settings.Get(getValue("SliderHiRateMax"), ref this.hiRateMax);
			Settings.Get(getValue("SliderHiRateMin"), ref this.hiRateMin);
			Settings.Get(getValue("SliderHiPowMax"), ref this.hiPowMax);
			Settings.Get(getValue("SliderHiPowMin"), ref this.hiPowMin);
			Settings.Get(getValue("SliderFloatVal1Max"), ref this.floatVal1Max);
			Settings.Get(getValue("SliderFloatVal1Min"), ref this.floatVal1Min);
			Settings.Get(getValue("SliderFloatVal2Max"), ref this.floatVal2Max);
			Settings.Get(getValue("SliderFloatVal2Min"), ref this.floatVal2Min);
			Settings.Get(getValue("SliderFloatVal3Max"), ref this.floatVal3Max);
			Settings.Get(getValue("SliderFloatVal3Min"), ref this.floatVal3Min);
			Settings.Get(getValue("EditShininessMax"), ref this.shininessEditMax);
			Settings.Get(getValue("EditShininessMin"), ref this.shininessEditMin);
			Settings.Get(getValue("EditOutlineWidthMax"), ref this.outlineWidthEditMax);
			Settings.Get(getValue("EditOutlineWidthMin"), ref this.outlineWidthEditMin);
			Settings.Get(getValue("EditRimPowerMax"), ref this.rimPowerEditMax);
			Settings.Get(getValue("EditRimPowerMin"), ref this.rimPowerEditMin);
			Settings.Get(getValue("EditRimShiftMax"), ref this.rimShiftEditMax);
			Settings.Get(getValue("EditRimShiftMin"), ref this.rimShiftEditMin);
			Settings.Get(getValue("EditHiRateMax"), ref this.hiRateEditMax);
			Settings.Get(getValue("EditHiRateMin"), ref this.hiRateEditMin);
			Settings.Get(getValue("EditHiPowMax"), ref this.hiPowEditMax);
			Settings.Get(getValue("EditHiPowMin"), ref this.hiPowEditMin);
			Settings.Get(getValue("EditFloatVal1Max"), ref this.floatVal1EditMax);
			Settings.Get(getValue("EditFloatVal1Min"), ref this.floatVal1EditMin);
			Settings.Get(getValue("EditFloatVal2Max"), ref this.floatVal2EditMax);
			Settings.Get(getValue("EditFloatVal2Min"), ref this.floatVal2EditMin);
			Settings.Get(getValue("EditFloatVal3Max"), ref this.floatVal3EditMax);
			Settings.Get(getValue("EditFloatVal3Min"), ref this.floatVal3EditMin);
			Settings.GetFormat(getValue("EditShininessFormat"), ref this.shininessFmt);
			Settings.GetFormat(getValue("EditOutlineWidthFormat"), ref this.outlineWidthFmt);
			Settings.GetFormat(getValue("EditRimPowerFormat"), ref this.rimPowerFmt);
			Settings.GetFormat(getValue("EditRimShiftFormat"), ref this.rimShiftFmt);
			Settings.GetFormat(getValue("EditHiRateFormat"), ref this.hiRateFmt);
			Settings.GetFormat(getValue("EditHiPowFormat"), ref this.hiPowFmt);
			Settings.GetFormat(getValue("EditFloatVal1Format"), ref this.floatVal1Fmt);
			Settings.GetFormat(getValue("EditFloatVal2Format"), ref this.floatVal2Fmt);
			Settings.GetFormat(getValue("EditFloatVal3Format"), ref this.floatVal3Fmt);
			string empty = string.Empty;
			Settings.Get(getValue("ToonTexAddon"), ref empty);
			if (empty.Length > 0)
			{
				this.toonTexAddon = (from p in empty.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries)
				select p.Trim()).ToArray<string>();
				if (LogUtil.IsDebug())
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (string value in this.toonTexAddon)
					{
						stringBuilder.Append(value).Append(',');
					}
					LogUtil.Debug(new object[]
					{
						"loading toon addon: ",
						stringBuilder
					});
				}
			}
			empty = string.Empty;
			Settings.Get(getValue("ToonTex"), ref empty);
			if (empty.Length > 0)
			{
				this.toonTexes = (from p in empty.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries)
				select p.Trim()).ToArray<string>();
				if (LogUtil.IsDebug())
				{
					StringBuilder stringBuilder2 = new StringBuilder();
					foreach (string value2 in this.toonTexes)
					{
						stringBuilder2.Append(value2).Append(',');
					}
					LogUtil.Debug(new object[]
					{
						"loading toon texes: ",
						stringBuilder2
					});
				}
			}
			Settings.Get(getValue("ToonComboAutoApply"), ref this.toonComboAutoApply);
			Settings.Get(getValue("DisplaySlotName"), ref this.displaySlotName);
			Settings.Get(getValue("EnableMask"), ref this.enableMask);
			Settings.Get(getValue("EnableMoza"), ref this.enableMoza);
			Settings.Get(getValue("SSWithoutUI"), ref this.SSWithoutUI);
			string empty2 = string.Empty;
			Settings.Get(getValue("EnableScenes"), ref empty2);
			if (empty2.Length > 0)
			{
				Settings.ParseList(empty2, ref this.enableScenes);
			}
			empty2 = string.Empty;
			Settings.Get(getValue("EnableOHScenes"), ref empty2);
			if (empty2.Length > 0)
			{
				Settings.ParseList(empty2, ref this.enableOHScenes);
			}
			empty2 = string.Empty;
			Settings.Get(getValue("DisableScenes"), ref empty2);
			if (empty2.Length > 0)
			{
				Settings.ParseList(empty2, ref this.disableScenes);
			}
			empty2 = string.Empty;
			Settings.Get(getValue("DisableOHScenes"), ref empty2);
			if (empty2.Length > 0)
			{
				Settings.ParseList(empty2, ref this.disableOHScenes);
			}
			Settings.Get(getValue("EnableStandardShader"), ref this.enableStandard);
			Settings.Get(getValue("TextureScaleRestore"), ref this.backScale);
			Settings.Get(getValue("EnableTexAnimator"), ref this.enableTexAnim);
			Settings.Get(getValue("AnimeFPS"), ref this.animeFPS);
			Settings.Get(getValue("DetectRate"), ref this.detectRate);
			this.defaultFrameSecond = 1f / (float)this.animeFPS;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00008900 File Offset: 0x00006B00
		private static void ParseList(string valString, ref List<int> ret)
		{
			IOrderedEnumerable<int> orderedEnumerable = from val in valString.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries).Select(delegate(string p)
			{
				int result;
				if (!int.TryParse(p, out result))
				{
					return -1;
				}
				return result;
			})
			where val > 0 && val < Settings.MAX_SCENES
			orderby val descending
			select val;
			if (orderedEnumerable.Any<int>())
			{
				ret = (List<int>)orderedEnumerable;
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00008998 File Offset: 0x00006B98
		private static bool Get(string boolString, ref bool output)
		{
			bool flag;
			if (!bool.TryParse(boolString, out flag))
			{
				return false;
			}
			output = flag;
			return true;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000089B8 File Offset: 0x00006BB8
		private static void Get(string floatString, ref float output)
		{
			float num;
			if (float.TryParse(floatString, out num))
			{
				output = num;
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000089D4 File Offset: 0x00006BD4
		private static void Get(string numString, ref int output)
		{
			int num;
			if (int.TryParse(numString, out num))
			{
				output = num;
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000089F0 File Offset: 0x00006BF0
		private static void GetFormat(string format, ref string output)
		{
			if (format == null)
			{
				return;
			}
			float num;
			if (float.TryParse(Settings.VERF_VALUE.ToString(format), out num))
			{
				output = format;
				return;
			}
			if (format.Length > 0)
			{
				LogUtil.Log(new object[]
				{
					"failed to parse Format string:",
					format
				});
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00008A3F File Offset: 0x00006C3F
		private static bool Get(string stringVal, ref string output)
		{
			if (stringVal == null)
			{
				return false;
			}
			output = stringVal;
			return true;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00008A4C File Offset: 0x00006C4C
		private static void GetKeyCode(string keyString, ref KeyCode output)
		{
			if (string.IsNullOrEmpty(keyString))
			{
				return;
			}
			try
			{
				KeyCode keyCode = (KeyCode)Enum.Parse(typeof(KeyCode), keyString);
				output = keyCode;
			}
			catch (ArgumentException)
			{
			}
		}

		// Token: 0x04000081 RID: 129
		public static readonly Settings Instance = new Settings();

		// Token: 0x04000082 RID: 130
		public KeyCode toggleKey = KeyCode.F12;

		// Token: 0x04000083 RID: 131
		public EventModifiers toggleModifiers;

		// Token: 0x04000084 RID: 132
		public HashSet<KeyCode> toggleKeyModifier;

		// Token: 0x04000085 RID: 133
		public KeyCode prevKey = KeyCode.Mouse3;

		// Token: 0x04000086 RID: 134
		public KeyCode nextKey = KeyCode.Mouse4;

		// Token: 0x04000087 RID: 135
		public string presetPath;

		// Token: 0x04000088 RID: 136
		public string presetDirPath;

		// Token: 0x04000089 RID: 137
		public float shininessMax = 20f;

		// Token: 0x0400008A RID: 138
		public float shininessMin;

		// Token: 0x0400008B RID: 139
		public float outlineWidthMax = 0.1f;

		// Token: 0x0400008C RID: 140
		public float outlineWidthMin;

		// Token: 0x0400008D RID: 141
		public float rimPowerMax = 200f;

		// Token: 0x0400008E RID: 142
		public float rimPowerMin = -200f;

		// Token: 0x0400008F RID: 143
		public float rimShiftMax = 1f;

		// Token: 0x04000090 RID: 144
		public float rimShiftMin;

		// Token: 0x04000091 RID: 145
		public float hiRateMax = 1f;

		// Token: 0x04000092 RID: 146
		public float hiRateMin;

		// Token: 0x04000093 RID: 147
		public float hiPowMax = 50f;

		// Token: 0x04000094 RID: 148
		public float hiPowMin = 0.001f;

		// Token: 0x04000095 RID: 149
		public float floatVal1Max = 300f;

		// Token: 0x04000096 RID: 150
		public float floatVal1Min;

		// Token: 0x04000097 RID: 151
		public float floatVal2Max = 15f;

		// Token: 0x04000098 RID: 152
		public float floatVal2Min = -15f;

		// Token: 0x04000099 RID: 153
		public float floatVal3Max = 1f;

		// Token: 0x0400009A RID: 154
		public float floatVal3Min;

		// Token: 0x0400009B RID: 155
		public float shininessEditMax = 10000f;

		// Token: 0x0400009C RID: 156
		public float shininessEditMin = -10000f;

		// Token: 0x0400009D RID: 157
		public float outlineWidthEditMax = 1f;

		// Token: 0x0400009E RID: 158
		public float outlineWidthEditMin;

		// Token: 0x0400009F RID: 159
		public float rimPowerEditMax = 10000f;

		// Token: 0x040000A0 RID: 160
		public float rimPowerEditMin = -10000f;

		// Token: 0x040000A1 RID: 161
		public float rimShiftEditMax = 5f;

		// Token: 0x040000A2 RID: 162
		public float rimShiftEditMin;

		// Token: 0x040000A3 RID: 163
		public float hiRateEditMax = 100f;

		// Token: 0x040000A4 RID: 164
		public float hiRateEditMin;

		// Token: 0x040000A5 RID: 165
		public float hiPowEditMax = 100f;

		// Token: 0x040000A6 RID: 166
		public float hiPowEditMin = 1E-05f;

		// Token: 0x040000A7 RID: 167
		public float floatVal1EditMax = 500f;

		// Token: 0x040000A8 RID: 168
		public float floatVal1EditMin;

		// Token: 0x040000A9 RID: 169
		public float floatVal2EditMax = 50f;

		// Token: 0x040000AA RID: 170
		public float floatVal2EditMin = -50f;

		// Token: 0x040000AB RID: 171
		public float floatVal3EditMax = 50f;

		// Token: 0x040000AC RID: 172
		public float floatVal3EditMin;

		// Token: 0x040000AD RID: 173
		public string shininessFmt = "F2";

		// Token: 0x040000AE RID: 174
		public string outlineWidthFmt = "F5";

		// Token: 0x040000AF RID: 175
		public string rimPowerFmt = "F3";

		// Token: 0x040000B0 RID: 176
		public string rimShiftFmt = "F5";

		// Token: 0x040000B1 RID: 177
		public string hiRateFmt = "F2";

		// Token: 0x040000B2 RID: 178
		public string hiPowFmt = "F5";

		// Token: 0x040000B3 RID: 179
		public string floatVal1Fmt = "F2";

		// Token: 0x040000B4 RID: 180
		public string floatVal2Fmt = "F3";

		// Token: 0x040000B5 RID: 181
		public string floatVal3Fmt = "F3";

		// Token: 0x040000B6 RID: 182
		public bool enableStandard;

		// Token: 0x040000B7 RID: 183
		public bool enableTexAnim;

		// Token: 0x040000B8 RID: 184
		public string namePostfix = "_anim";

		// Token: 0x040000B9 RID: 185
		public string namePostfixWithExt = "_anim.tex";

		// Token: 0x040000BA RID: 186
		public int animeFPS = 30;

		// Token: 0x040000BB RID: 187
		public int detectRate = 60;

		// Token: 0x040000BC RID: 188
		public bool backScale;

		// Token: 0x040000BD RID: 189
		public float defaultFrameSecond = 0.0333333351f;

		// Token: 0x040000BE RID: 190
		public string fmtColor = "F3";

		// Token: 0x040000BF RID: 191
		public string menuPrefix = "";

		// Token: 0x040000C0 RID: 192
		public string iconSuffix = "_i_";

		// Token: 0x040000C1 RID: 193
		public string resSuffix = "_mekure_";

		// Token: 0x040000C2 RID: 194
		public string txtPrefixMenu = "Assets/menu/menu/";

		// Token: 0x040000C3 RID: 195
		public string txtPrefixTex = "Assets/texture/texture/";

		// Token: 0x040000C4 RID: 196
		public string[] toonTexes = new string[]
		{
			"noTex",
			"toonBlueA1",
			"toonBlueA2",
			"toonBrownA1",
			"toonGrayA1",
			"toonGreenA1",
			"toonGreenA2",
			"toonGreenA3",
			"toonOrangeA1",
			"toonPinkA1",
			"toonPinkA2",
			"toonPurpleA1",
			"toonRedA1",
			"toonRedA2",
			"toonRedmmm1",
			"toonRedmm1",
			"toonRedm1",
			"toonYellowA1",
			"toonYellowA2",
			"toonYellowA3",
			"toonYellowA4",
			"toonFace",
			"toonSkin",
			"toonBlackA1",
			"toonFace_shadow",
			"toonDress_shadow",
			"toonSkin_Shadow",
			"toonBlackmm1",
			"toonBlackm1",
			"toonGraymm1",
			"toonGraym1",
			"toonPurplemm1",
			"toonPurplem1",
			"toonSilvera1",
			"toonDressmm_shadow",
			"toonDressm_shadow"
		};

		// Token: 0x040000C5 RID: 197
		public string[] toonTexAddon = new string[0];

		// Token: 0x040000C6 RID: 198
		public bool toonComboAutoApply = true;

		// Token: 0x040000C7 RID: 199
		public bool displaySlotName;

		// Token: 0x040000C8 RID: 200
		public bool enableMask = true;

		// Token: 0x040000C9 RID: 201
		public bool enableMoza;

		// Token: 0x040000CA RID: 202
		public bool SSWithoutUI;

		// Token: 0x040000CB RID: 203
		private static readonly int MAX_SCENES = 256;

		// Token: 0x040000CC RID: 204
		public List<int> enableScenes;

		// Token: 0x040000CD RID: 205
		public List<int> disableScenes;

		// Token: 0x040000CE RID: 206
		public List<int> enableOHScenes;

		// Token: 0x040000CF RID: 207
		public List<int> disableOHScenes;

		// Token: 0x040000D0 RID: 208
		private static readonly float VERF_VALUE = 12.3f;
	}
}
