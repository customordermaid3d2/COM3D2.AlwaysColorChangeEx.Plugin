using System;
using System.Collections.Generic;
using System.Linq;
using CM3D2.AlwaysColorChangeEx.Plugin.Data;
using CM3D2.AlwaysColorChangeEx.Plugin.UI.Data;
using CM3D2.AlwaysColorChangeEx.Plugin.UI.Helper;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.UI
{
	// Token: 0x0200006B RID: 107
	public class ACCPartsColorView : BaseView
	{
		// Token: 0x0600034A RID: 842 RVA: 0x0001B65D File Offset: 0x0001985D
		public ACCPartsColorView(UIParams uiParams, SliderHelper sliderHelper)
		{
			this.uiParams = uiParams;
			this.sliderHelper = sliderHelper;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0001B694 File Offset: 0x00019894
		public override void UpdateUI(UIParams uParams)
		{
			this.uiParams = uParams;
			this.titleWidth = GUILayout.Width((float)this.uiParams.fontSize * 20f);
			this.titleHeight = GUILayout.Height(this.uiParams.titleBarRect.height);
			this.viewHeight = this.uiParams.winRect.height - (float)this.uiParams.unitHeight - (float)this.uiParams.margin * 2f - this.uiParams.titleBarRect.height;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0001B727 File Offset: 0x00019927
		public void Update()
		{
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0001B72C File Offset: 0x0001992C
		public void Show()
		{
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			try
			{
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.Label("パーツカラー", this.uiParams.lStyleB, new GUILayoutOption[]
				{
					this.titleWidth,
					this.titleHeight
				});
				GUILayout.EndHorizontal();
				Maid currentMaid = BaseView.holder.CurrentMaid;
				if (!(currentMaid == null))
				{
					if (currentMaid.IsBusy)
					{
						GUILayout.Space(100f);
						GUILayout.Label("変更中...", this.uiParams.lStyleB, new GUILayoutOption[0]);
						GUILayout.Space(this.uiParams.colorRect.height - 105f);
					}
					else
					{
						this.scrollViewPosition = GUILayout.BeginScrollView(this.scrollViewPosition, new GUILayoutOption[]
						{
							GUILayout.Width(this.uiParams.colorRect.width),
							GUILayout.Height(this.viewHeight)
						});
						try
						{
							if (!this.editPartColors.Any<ACCPartsColorView.EditParts>())
							{
								for (MaidParts.PARTS_COLOR parts_COLOR = MaidParts.PARTS_COLOR.EYE_L; parts_COLOR < MaidParts.PARTS_COLOR.MATSUGE_UP; parts_COLOR++)
								{
									MaidParts.PartsColor partsColor = currentMaid.Parts.GetPartsColor(parts_COLOR);
									this.editPartColors.Add(new ACCPartsColorView.EditParts(ref partsColor, this.presetMgr));
								}
							}
							for (MaidParts.PARTS_COLOR parts_COLOR2 = MaidParts.PARTS_COLOR.EYE_L; parts_COLOR2 < MaidParts.PARTS_COLOR.MATSUGE_UP; parts_COLOR2++)
							{
								MaidParts.PartsColor partsColor2 = currentMaid.Parts.GetPartsColor(parts_COLOR2);
								int index = (int)parts_COLOR2;
								ACCPartsColorView.EditParts editParts = this.editPartColors[index];
								GUILayout.BeginHorizontal(new GUILayoutOption[0]);
								try
								{
									if (GUILayout.Button(parts_COLOR2.ToString(), this.uiParams.lStyleB, new GUILayoutOption[0]))
									{
										editParts.expand = !editParts.expand;
									}
									string text = partsColor2.m_bUse ? "未使用" : "使用中";
									GUILayout.Label(text, this.uiParams.lStyleRS, new GUILayoutOption[0]);
								}
								finally
								{
									GUILayout.EndHorizontal();
								}
								if (editParts.expand)
								{
									if (partsColor2.m_nShadowRate != editParts.shadowRate.val)
									{
										editParts.shadowRate.Set(partsColor2.m_nShadowRate);
									}
									if (this.sliderHelper.DrawValueSlider("影率", editParts.shadowRate))
									{
										partsColor2.m_nShadowRate = editParts.shadowRate.val;
										currentMaid.Parts.SetPartsColor(parts_COLOR2, partsColor2);
										editParts.SetParts(partsColor2);
									}
									if (editParts.HasMainChanged(ref partsColor2))
									{
										editParts.SetMain(partsColor2);
									}
									if (editParts.HasShadowChanged(ref partsColor2))
									{
										editParts.SetShadow(partsColor2);
									}
									if (partsColor2.m_nMainContrast != editParts.c.val)
									{
										editParts.c.Set(partsColor2.m_nMainContrast);
									}
									if (this.sliderHelper.DrawValueSlider("主C", editParts.c))
									{
										partsColor2.m_nMainContrast = editParts.c.val;
										currentMaid.Parts.SetPartsColor(parts_COLOR2, partsColor2);
										editParts.SetParts(partsColor2);
									}
									if (partsColor2.m_nShadowContrast != editParts.shadowC.val)
									{
										editParts.shadowC.Set(partsColor2.m_nShadowContrast);
									}
									if (this.sliderHelper.DrawValueSlider("影C", editParts.shadowC))
									{
										partsColor2.m_nShadowContrast = editParts.shadowC.val;
										currentMaid.Parts.SetPartsColor(parts_COLOR2, partsColor2);
										editParts.SetParts(partsColor2);
									}
									if (this.sliderHelper.DrawColorSlider("主色", ref editParts.main, SliderHelper.DEFAULT_PRESET, ref editParts.mainExpand, editParts.mainPicker))
									{
										editParts.ReflectMain();
										currentMaid.Parts.SetPartsColor(parts_COLOR2, editParts.parts);
									}
									if (this.sliderHelper.DrawColorSlider("影色", ref editParts.shadow, SliderHelper.DEFAULT_PRESET, ref editParts.shadowExpand, editParts.shadowPicker))
									{
										editParts.ReflectShadow();
										currentMaid.Parts.SetPartsColor(parts_COLOR2, editParts.parts);
									}
								}
							}
						}
						finally
						{
							GUI.EndScrollView();
						}
					}
				}
			}
			finally
			{
				GUILayout.EndVertical();
			}
		}

		// Token: 0x040003CF RID: 975
		private readonly SliderHelper sliderHelper;

		// Token: 0x040003D0 RID: 976
		private readonly ColorPresetManager presetMgr = ColorPresetManager.Instance;

		// Token: 0x040003D1 RID: 977
		private Vector2 scrollViewPosition = Vector2.zero;

		// Token: 0x040003D2 RID: 978
		private readonly List<ACCPartsColorView.EditParts> editPartColors = new List<ACCPartsColorView.EditParts>();

		// Token: 0x040003D3 RID: 979
		private GUILayoutOption titleWidth;

		// Token: 0x040003D4 RID: 980
		private GUILayoutOption titleHeight;

		// Token: 0x040003D5 RID: 981
		private float viewHeight;

		// Token: 0x0200006C RID: 108
		private class EditParts
		{
			// Token: 0x0600034E RID: 846 RVA: 0x0001BB78 File Offset: 0x00019D78
			public EditParts(ref MaidParts.PartsColor pc, ColorPresetManager presetMgr)
			{
				this.mainPicker = new ColorPicker(presetMgr)
				{
					ColorTex = new Texture2D(32, 20, TextureFormat.RGB24, false),
					texEdgeSize = 2
				};
				this.shadowPicker = new ColorPicker(presetMgr)
				{
					ColorTex = new Texture2D(32, 20, TextureFormat.RGB24, false),
					texEdgeSize = 2
				};
				Color white = Color.white;
				this.mainPicker.SetTexColor(ref white, 0);
				this.shadowPicker.SetTexColor(ref white, 0);
				this.c.Set(pc.m_nMainContrast);
				this.shadowC.Set(pc.m_nShadowContrast);
				this.shadowRate.Set(pc.m_nShadowRate);
				this.parts = pc;
				this.SetMain(pc);
				this.SetShadow(pc);
			}

			// Token: 0x0600034F RID: 847 RVA: 0x0001BCBC File Offset: 0x00019EBC
			public void SetParts(MaidParts.PartsColor parts1)
			{
				if (this.HasMainChanged(ref parts1))
				{
					this.SetMain(parts1);
				}
				if (this.HasShadowChanged(ref parts1))
				{
					this.SetShadow(parts1);
				}
				if (this.c.val != parts1.m_nMainContrast)
				{
					this.c.Set(parts1.m_nMainContrast);
				}
				if (this.shadowC.val != parts1.m_nShadowContrast)
				{
					this.shadowC.Set(parts1.m_nShadowContrast);
				}
				if (this.shadowRate.val != parts1.m_nShadowRate)
				{
					this.shadowRate.Set(parts1.m_nShadowRate);
				}
				this.parts = parts1;
			}

			// Token: 0x06000350 RID: 848 RVA: 0x0001BD64 File Offset: 0x00019F64
			public void ReflectMain()
			{
				Vector4 vector = ColorUtil.RGB2HSL(ref this.main.val);
				this.parts.m_nMainHue = (int)(255f * vector.x);
				this.parts.m_nMainChroma = (int)(255f * vector.y);
				this.parts.m_nMainBrightness = (int)(510f * vector.z);
			}

			// Token: 0x06000351 RID: 849 RVA: 0x0001BDD0 File Offset: 0x00019FD0
			public void ReflectShadow()
			{
				Vector4 vector = ColorUtil.RGB2HSL(ref this.shadow.val);
				this.parts.m_nShadowHue = (int)(255f * vector.x);
				this.parts.m_nShadowChroma = (int)(255f * vector.y);
				this.parts.m_nShadowBrightness = (int)(510f * vector.z);
			}

			// Token: 0x06000352 RID: 850 RVA: 0x0001BE3C File Offset: 0x0001A03C
			public void SetMain(MaidParts.PartsColor parts1)
			{
				Color color = ColorUtil.HSL2RGB((float)parts1.m_nMainHue / 255f, (float)parts1.m_nMainChroma / 255f, (float)parts1.m_nMainBrightness / 510f, 1f);
				this.main.Set(color);
				this.mainPicker.Color = color;
			}

			// Token: 0x06000353 RID: 851 RVA: 0x0001BE98 File Offset: 0x0001A098
			public void SetShadow(MaidParts.PartsColor parts1)
			{
				Color color = ColorUtil.HSL2RGB((float)parts1.m_nShadowHue / 255f, (float)parts1.m_nShadowChroma / 255f, (float)parts1.m_nShadowBrightness / 510f, 1f);
				this.shadow.Set(color);
				this.shadowPicker.Color = color;
			}

			// Token: 0x06000354 RID: 852 RVA: 0x0001BEF2 File Offset: 0x0001A0F2
			public bool HasMainChanged(ref MaidParts.PartsColor parts1)
			{
				return this.parts.m_nMainBrightness != parts1.m_nMainBrightness || this.parts.m_nMainChroma != parts1.m_nMainChroma || this.parts.m_nMainHue != parts1.m_nMainHue;
			}

			// Token: 0x06000355 RID: 853 RVA: 0x0001BF32 File Offset: 0x0001A132
			public bool HasShadowChanged(ref MaidParts.PartsColor parts1)
			{
				return this.parts.m_nShadowBrightness != parts1.m_nShadowBrightness || this.parts.m_nShadowChroma != parts1.m_nShadowChroma || this.parts.m_nShadowHue != parts1.m_nShadowHue;
			}

			// Token: 0x040003D6 RID: 982
			public MaidParts.PartsColor parts;

			// Token: 0x040003D7 RID: 983
			public EditColor main = new EditColor(Color.white, ColorType.rgb, false);

			// Token: 0x040003D8 RID: 984
			public bool mainExpand = true;

			// Token: 0x040003D9 RID: 985
			public readonly ColorPicker mainPicker;

			// Token: 0x040003DA RID: 986
			public EditColor shadow = new EditColor(Color.white, ColorType.rgb, false);

			// Token: 0x040003DB RID: 987
			public bool shadowExpand = true;

			// Token: 0x040003DC RID: 988
			public readonly ColorPicker shadowPicker;

			// Token: 0x040003DD RID: 989
			public readonly EditIntValue c = new EditIntValue(100, EditRange.contrast);

			// Token: 0x040003DE RID: 990
			public readonly EditIntValue shadowC = new EditIntValue(100, EditRange.contrast);

			// Token: 0x040003DF RID: 991
			public readonly EditIntValue shadowRate = new EditIntValue(128, EditRange.rate);

			// Token: 0x040003E0 RID: 992
			public bool expand;
		}
	}
}
