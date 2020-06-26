using System;
using System.Collections.Generic;
using System.Globalization;
using CM3D2.AlwaysColorChangeEx.Plugin.Data;
using CM3D2.AlwaysColorChangeEx.Plugin.UI.Data;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.UI.Helper
{
	// Token: 0x02000067 RID: 103
	public class SliderHelper
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00018F1B File Offset: 0x0001711B
		private static GUIContent CopyIcon
		{
			get
			{
				GUIContent result;
				if ((result = SliderHelper.copyIcon) == null)
				{
					result = (SliderHelper.copyIcon = new GUIContent(string.Empty, ResourceHolder.Instance.CopyImage, "カラーコードをクリップボードへコピーする"));
				}
				return result;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00018F45 File Offset: 0x00017145
		private static GUIContent PasteIcon
		{
			get
			{
				GUIContent result;
				if ((result = SliderHelper.pasteIcon) == null)
				{
					result = (SliderHelper.pasteIcon = new GUIContent(string.Empty, ResourceHolder.Instance.PasteImage, "クリップボードからカラーコードを貼付ける"));
				}
				return result;
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00018F70 File Offset: 0x00017170
		public SliderHelper(UIParams uiparams)
		{
			this.uiParams = uiparams;
			this.uiParams.Add(new Action<UIParams>(this.updateUI));
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00018FE4 File Offset: 0x000171E4
		~SliderHelper()
		{
			this.uiParams.Remove(new Action<UIParams>(this.updateUI));
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00019024 File Offset: 0x00017224
		private void updateUI(UIParams uiparams)
		{
			this.labelWidth = uiparams.colorRect.width * 0.2f;
			this.sliderMargin = (float)uiparams.margin * 4.5f;
			this.buttonMargin = (float)uiparams.margin * 3f;
			this.sliderInputWidth = uiparams.lStyleS.CalcSize(new GUIContent("zzzzzzzz")).x;
			this.optInputWidth = GUILayout.Width(this.sliderInputWidth);
			this.optItemHeight = GUILayout.Height((float)uiparams.itemHeight);
			this.optCodeWidth = GUILayout.Width(uiparams.textStyleSC.CalcSize(new GUIContent("#DDDDDD")).x);
			if (this.optCPWidth == null)
			{
				this.optCPWidth = GUILayout.Width(17f);
			}
			this.textColor = uiparams.textStyleSC.normal.textColor;
			this.bWidthOpt = GUILayout.Width(this.bStyleSS.CalcSize(new GUIContent("x")).x);
			this.bWidthWOpt = GUILayout.Width(this.bStyleSS.CalcSize(new GUIContent("xx")).x);
			this.bWidthTOpt = GUILayout.Width(this.bStyleSS.CalcSize(new GUIContent("xxx")).x);
			this.bStyleSS.normal.textColor = uiparams.bStyleSC.normal.textColor;
			this.bStyleSS.alignment = TextAnchor.MiddleCenter;
			this.bStyleSS.fontSize = uiparams.fontSizeSS;
			this.bStyleSS.contentOffset = new Vector2(0f, 1f);
			this.bStyleSS.margin.left = 1;
			this.bStyleSS.margin.right = 1;
			this.bStyleSS.padding.left = 1;
			this.bStyleSS.padding.right = 1;
			this.iconStyleSS.normal.textColor = uiparams.lStyleS.normal.textColor;
			this.iconStyleSS.alignment = TextAnchor.MiddleCenter;
			this.iconStyleSS.fontSize = uiparams.fontSizeSS;
			this.iconStyleSS.contentOffset = new Vector2(0f, 1f);
			this.iconStyleSS.margin.left = 0;
			this.iconStyleSS.margin.right = 0;
			this.iconStyleSS.padding.left = 1;
			this.iconStyleSS.padding.right = 1;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000192AC File Offset: 0x000174AC
		public void SetupFloatSlider(NamedEditValue edit, float[] vals1, float[] vals2)
		{
			this.SetupFloatSlider(edit.name, edit, edit.range.editMin, edit.range.editMax, edit.act, null, vals1, vals2);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x000192E8 File Offset: 0x000174E8
		public void SetupFloatSlider(string label, EditValue edit, float sliderMin, float sliderMax, Action<float> func, float[] vals1, float[] vals2)
		{
			this.SetupFloatSlider(label, edit, sliderMin, sliderMax, func, null, vals1, vals2);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00019308 File Offset: 0x00017508
		internal void SetupFloatSlider(ShaderPropFloat fprop, EditValue edit, Action<float> func)
		{
			this.SetupFloatSlider(fprop.name, edit, fprop.sliderMin, fprop.sliderMax, func, fprop.opts, null, fprop.presetVals);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x000193A4 File Offset: 0x000175A4
		internal void SetupFloatSlider(string label, EditValue edit, float sliderMin, float sliderMax, Action<float> func, PresetOperation[] presetOprs, float[] vals1, float[] vals2)
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label(label, this.uiParams.lStyle, new GUILayoutOption[]
			{
				this.optItemHeight
			});
			GUILayout.Space((float)this.uiParams.marginL);
			bool changed = false;
			Action<float> action = delegate(float val)
			{
				GUIContent guicontent2 = new GUIContent(val.ToString(CultureInfo.InvariantCulture));
				if (!GUILayout.Button(guicontent2, this.bStyleSS, new GUILayoutOption[]
				{
					this.getWidthOpt(guicontent2)
				}))
				{
					return;
				}
				edit.Set(val);
				changed = true;
			};
			if (vals1 != null)
			{
				foreach (float obj in vals1)
				{
					action(obj);
				}
			}
			if (vals2 != null)
			{
				foreach (float obj2 in vals2)
				{
					action(obj2);
				}
			}
			if (presetOprs != null)
			{
				foreach (PresetOperation presetOperation in presetOprs)
				{
					GUIContent guicontent = new GUIContent(presetOperation.label);
					if (GUILayout.Button(guicontent, this.bStyleSS, new GUILayoutOption[]
					{
						this.getWidthOpt(guicontent)
					}))
					{
						edit.SetWithCheck(presetOperation.func(edit.val));
						changed = true;
					}
				}
			}
			GUILayout.EndHorizontal();
			if (changed || this.DrawValueSlider(null, edit, sliderMin, sliderMax))
			{
				func(edit.val);
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00019524 File Offset: 0x00017724
		private GUILayoutOption getWidthOpt(GUIContent cont)
		{
			switch (cont.text.Length)
			{
			case 0:
			case 1:
				return this.bWidthOpt;
			case 2:
				return this.bWidthWOpt;
			case 3:
				return this.bWidthTOpt;
			default:
				return GUILayout.Width(this.bStyleSS.CalcSize(cont).x + 2f);
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00019588 File Offset: 0x00017788
		internal bool DrawColorSlider(ShaderPropColor colProp, ref EditColor edit, ColorPicker picker = null)
		{
			bool flag = true;
			float[] vals = colProp.composition ? SliderHelper.DEFAULT_PRESET2 : SliderHelper.DEFAULT_PRESET;
			return this.DrawColorSlider(colProp.name, ref edit, colProp.colorType, vals, ref flag, picker);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000195C3 File Offset: 0x000177C3
		internal bool DrawColorSlider(string label, ref EditColor edit, IEnumerable<float> vals, ref bool expand, ColorPicker picker = null)
		{
			return this.DrawColorSlider(label, ref edit, edit.type, vals, ref expand, picker);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x000195DC File Offset: 0x000177DC
		internal bool DrawColorSlider(string label, ref EditColor edit, ColorType colType, IEnumerable<float> vals, ref bool expand, ColorPicker picker = null)
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			Color color = edit.val;
			bool flag = false;
			try
			{
				if (picker != null)
				{
					picker.Color = color;
					if (GUILayout.Button(picker.ColorTex, this.uiParams.lStyleS, new GUILayoutOption[]
					{
						this.optItemHeight,
						picker.IconWidth,
						GUILayout.ExpandWidth(false)
					}))
					{
						picker.expand = !picker.expand;
					}
				}
				if (GUILayout.Button(label, this.uiParams.lStyle, new GUILayoutOption[]
				{
					this.optItemHeight
				}))
				{
					expand = !expand;
				}
				if (picker != null && ColorPicker.IsColorCode(picker.ColorCode))
				{
					if (GUILayout.Button(SliderHelper.CopyIcon, this.uiParams.lStyle, new GUILayoutOption[]
					{
						this.optCPWidth,
						this.optItemHeight
					}))
					{
						SliderHelper.clipHandler.SetClipboard(picker.ColorCode);
					}
					string text = SliderHelper.clipHandler.GetClipboard();
					GUI.enabled &= ColorPicker.IsColorCode(text);
					try
					{
						if (GUILayout.Button(SliderHelper.PasteIcon, this.uiParams.lStyle, new GUILayoutOption[]
						{
							this.optCPWidth,
							this.optItemHeight
						}))
						{
							try
							{
								if (picker.SetColorCode(text))
								{
									color = picker.Color;
									flag = true;
								}
							}
							catch (Exception ex)
							{
								LogUtil.Error(new object[]
								{
									"failed to import color-code",
									ex
								});
							}
						}
					}
					finally
					{
						GUI.enabled = true;
					}
					string text2 = GUILayout.TextField(picker.ColorCode, 7, this.uiParams.textStyleSC, new GUILayoutOption[]
					{
						this.optCodeWidth
					});
					if (text2 != picker.ColorCode && picker.SetColorCode(text2))
					{
						color = picker.Color;
						flag = true;
					}
				}
				if (!expand)
				{
					return false;
				}
				foreach (float num in vals)
				{
					float b = num;
					GUIContent guicontent = new GUIContent(b.ToString(CultureInfo.InvariantCulture));
					if (GUILayout.Button(guicontent, this.bStyleSS, new GUILayoutOption[]
					{
						this.getWidthOpt(guicontent)
					}))
					{
						color.r = (color.g = (color.b = b));
						flag = true;
					}
				}
				if (GUILayout.Button("-", this.bStyleSS, new GUILayoutOption[]
				{
					this.bWidthOpt
				}))
				{
					if (color.r < SliderHelper.DELTA)
					{
						color.r = 0f;
					}
					else
					{
						color.r -= SliderHelper.DELTA;
					}
					if (color.g < SliderHelper.DELTA)
					{
						color.g = 0f;
					}
					else
					{
						color.g -= SliderHelper.DELTA;
					}
					if (color.b < SliderHelper.DELTA)
					{
						color.b = 0f;
					}
					else
					{
						color.b -= SliderHelper.DELTA;
					}
					flag = true;
				}
				if (GUILayout.Button("+", this.bStyleSS, new GUILayoutOption[]
				{
					this.bWidthOpt
				}))
				{
					if (color.r + SliderHelper.DELTA > edit.GetRange(0).editMax)
					{
						color.r = edit.GetRange(0).editMax;
					}
					else
					{
						color.r += SliderHelper.DELTA;
					}
					if (color.g + SliderHelper.DELTA > edit.GetRange(1).editMax)
					{
						color.g = edit.GetRange(1).editMax;
					}
					else
					{
						color.g += SliderHelper.DELTA;
					}
					if (color.b + SliderHelper.DELTA > edit.GetRange(2).editMax)
					{
						color.b = edit.GetRange(2).editMax;
					}
					else
					{
						color.b += SliderHelper.DELTA;
					}
					flag = true;
				}
			}
			finally
			{
				GUILayout.EndHorizontal();
			}
			int idx = 0;
			if (colType == ColorType.rgb || colType == ColorType.rgba)
			{
				flag |= this.DrawValueSlider("R", ref edit, idx++, ref color.r);
				flag |= this.DrawValueSlider("G", ref edit, idx++, ref color.g);
				flag |= this.DrawValueSlider("B", ref edit, idx++, ref color.b);
			}
			if (colType == ColorType.rgba || colType == ColorType.a)
			{
				flag |= this.DrawValueSlider("A", ref edit, idx, ref color.a);
			}
			if (picker != null && picker.expand && picker.DrawLayout())
			{
				color = picker.Color;
				flag = true;
			}
			if (flag)
			{
				edit.Set(color);
			}
			return flag;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00019B3C File Offset: 0x00017D3C
		public bool DrawValueSlider(string label, EditValue edit)
		{
			return this.DrawValueSlider(label, edit, edit.range.editMin, edit.range.editMax);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00019BBC File Offset: 0x00017DBC
		public bool DrawValueSlider(string label, EditIntValue edit)
		{
			return this.DrawValueSlider<int>(label, edit, delegate()
			{
				int val = edit.val;
				if (this.DrawSlider(ref val, edit.range.editMin, edit.range.editMax))
				{
					edit.Set(val);
					return true;
				}
				return false;
			});
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00019C44 File Offset: 0x00017E44
		public bool DrawValueSlider(string label, EditValue edit, float min, float max)
		{
			return this.DrawValueSlider<float>(label, edit, delegate()
			{
				float val = edit.val;
				if (this.DrawSlider(ref val, min, max))
				{
					edit.Set(val);
					return true;
				}
				return false;
			});
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00019C90 File Offset: 0x00017E90
		public bool DrawValueSlider<T>(string label, EditValueBase<T> edit, Func<bool> drawSlider) where T : IComparable, IFormattable
		{
			bool flag = false;
			bool flag2 = false;
			GUILayout.BeginHorizontal(new GUILayoutOption[]
			{
				this.optItemHeight
			});
			try
			{
				this.DrawLabel(ref label);
				if (!edit.isSync)
				{
					this.SetTextColor(this.uiParams.textStyleSC, ref this.textColorRed);
					flag2 = true;
				}
				string text = GUILayout.TextField(edit.editVal, this.uiParams.textStyleSC, new GUILayoutOption[]
				{
					this.optInputWidth
				});
				if (edit.editVal != text)
				{
					edit.Set(text);
					flag |= edit.isSync;
				}
				flag |= drawSlider();
				GUILayout.Space(this.buttonMargin);
			}
			finally
			{
				GUILayout.EndHorizontal();
				if (flag2)
				{
					this.SetTextColor(this.uiParams.textStyleSC, ref this.textColor);
				}
			}
			return flag;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00019D70 File Offset: 0x00017F70
		public bool DrawValueSlider(string label, ref EditColor edit, int idx, ref float sliderVal)
		{
			bool flag = false;
			bool flag2 = false;
			GUILayout.BeginHorizontal(new GUILayoutOption[]
			{
				this.optItemHeight
			});
			try
			{
				this.DrawLabel(ref label);
				if (!edit.isSyncs[idx])
				{
					this.SetTextColor(this.uiParams.textStyleSC, ref this.textColorRed);
					flag2 = true;
				}
				EditRange<float> range = edit.GetRange(idx);
				string text = GUILayout.TextField(edit.editVals[idx], this.uiParams.textStyleSC, new GUILayoutOption[]
				{
					this.optInputWidth
				});
				if (edit.editVals[idx] != text)
				{
					edit.Set(idx, text, range);
				}
				flag |= this.DrawSlider(ref sliderVal, range.editMin, range.editMax);
				GUILayout.Space(this.buttonMargin);
			}
			catch (Exception ex)
			{
				LogUtil.DebugF("{0}, idx={1}, color={2}, vals.length={3}, syncs.length={4}, e={5}", new object[]
				{
					label,
					idx,
					edit.val,
					edit.editVals.Length,
					edit.isSyncs.Length,
					ex
				});
				throw;
			}
			finally
			{
				GUILayout.EndHorizontal();
				if (flag2)
				{
					this.SetTextColor(this.uiParams.textStyleSC, ref this.textColor);
				}
			}
			return flag;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00019EF4 File Offset: 0x000180F4
		private void DrawLabel(ref string label)
		{
			if (label != null)
			{
				float x = this.uiParams.lStyleS.CalcSize(new GUIContent(label)).x;
				float num = this.labelWidth - this.sliderInputWidth - x;
				if (num > 0f)
				{
					GUILayout.Space(num);
				}
				GUILayout.Label(label, this.uiParams.lStyleS, new GUILayoutOption[]
				{
					GUILayout.Width(x)
				});
				return;
			}
			GUILayout.Space(this.labelWidth - this.sliderInputWidth);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0001A014 File Offset: 0x00018214
		private bool DrawSlider(ref float sliderVal, float min, float max)
		{
			float val = sliderVal;
			bool flag = this.DrawSlider(delegate()
			{
				float num = GUILayout.HorizontalSlider(val, min, max, new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(true)
				});
				if (!NumberUtil.Equals(num, val, this.epsilon))
				{
					if (val >= min && max >= val)
					{
						val = num;
						return true;
					}
					if (min < num && num < max)
					{
						val = num;
						return true;
					}
				}
				return false;
			});
			if (flag)
			{
				sliderVal = val;
			}
			return flag;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0001A0F0 File Offset: 0x000182F0
		private bool DrawSlider(ref int sliderVal, int min, int max)
		{
			int val = sliderVal;
			bool flag = this.DrawSlider(delegate()
			{
				int num = (int)GUILayout.HorizontalSlider((float)val, (float)min, (float)max, new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(true)
				});
				if (num != val)
				{
					if (val >= min && max >= val)
					{
						val = num;
						return true;
					}
					if (min < num && num < max)
					{
						val = num;
						return true;
					}
				}
				return false;
			});
			if (flag)
			{
				sliderVal = val;
			}
			return flag;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0001A138 File Offset: 0x00018338
		private bool DrawSlider(Func<bool> func)
		{
			bool flag = false;
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			try
			{
				GUILayout.Space(this.sliderMargin);
				flag |= func();
			}
			finally
			{
				GUILayout.EndVertical();
			}
			return flag;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0001A180 File Offset: 0x00018380
		private void SetTextColor(GUIStyle style, ref Color c)
		{
			style.normal.textColor = c;
			style.focused.textColor = c;
			style.active.textColor = c;
			style.hover.textColor = c;
		}

		// Token: 0x0400038B RID: 907
		private static GUIContent copyIcon;

		// Token: 0x0400038C RID: 908
		private static GUIContent pasteIcon;

		// Token: 0x0400038D RID: 909
		private static readonly ClipBoardHandler clipHandler = ClipBoardHandler.Instance;

		// Token: 0x0400038E RID: 910
		private readonly UIParams uiParams;

		// Token: 0x0400038F RID: 911
		public float epsilon = 1E-06f;

		// Token: 0x04000390 RID: 912
		public Color textColor;

		// Token: 0x04000391 RID: 913
		public Color textColorRed = Color.red;

		// Token: 0x04000392 RID: 914
		private float sliderMargin;

		// Token: 0x04000393 RID: 915
		private float buttonMargin;

		// Token: 0x04000394 RID: 916
		private float labelWidth;

		// Token: 0x04000395 RID: 917
		private float sliderInputWidth;

		// Token: 0x04000396 RID: 918
		private readonly GUIStyle bStyleSS = new GUIStyle("button");

		// Token: 0x04000397 RID: 919
		private readonly GUIStyle iconStyleSS = new GUIStyle("label");

		// Token: 0x04000398 RID: 920
		private GUILayoutOption bWidthOpt;

		// Token: 0x04000399 RID: 921
		private GUILayoutOption bWidthWOpt;

		// Token: 0x0400039A RID: 922
		private GUILayoutOption bWidthTOpt;

		// Token: 0x0400039B RID: 923
		private GUILayoutOption optItemHeight;

		// Token: 0x0400039C RID: 924
		private GUILayoutOption optInputWidth;

		// Token: 0x0400039D RID: 925
		private GUILayoutOption optCPWidth;

		// Token: 0x0400039E RID: 926
		private GUILayoutOption optCodeWidth;

		// Token: 0x0400039F RID: 927
		private static readonly float DELTA = 0.1f;

		// Token: 0x040003A0 RID: 928
		public static readonly float[] DEFAULT_PRESET = new float[]
		{
			0f,
			0.5f,
			1f
		};

		// Token: 0x040003A1 RID: 929
		public static readonly float[] DEFAULT_PRESET2 = new float[]
		{
			0f,
			0.5f,
			1f,
			1.5f,
			2f
		};
	}
}
