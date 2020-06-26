using System;
using CM3D2.AlwaysColorChangeEx.Plugin.Data;
using CM3D2.AlwaysColorChangeEx.Plugin.Render;
using CM3D2.AlwaysColorChangeEx.Plugin.UI.Data;
using CM3D2.AlwaysColorChangeEx.Plugin.UI.Helper;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.UI
{
	// Token: 0x02000069 RID: 105
	public class ACCBoneSlotView : BaseView
	{
		// Token: 0x06000337 RID: 823 RVA: 0x0001A258 File Offset: 0x00018458
		public ACCBoneSlotView(UIParams uiParams, SliderHelper sliderHelper)
		{
			this.uiParams = uiParams;
			this.boneRenderer = new CustomBoneRenderer();
			this.sliderHelper = sliderHelper;
			this.picker = new ColorPicker(ColorPresetManager.Instance)
			{
				ColorTex = new Texture2D(32, uiParams.itemHeight, TextureFormat.RGB24, false)
			};
			Color white = Color.white;
			this.picker.SetTexColor(ref white);
			this.slotNames = this.CreateSlotNames();
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0001A304 File Offset: 0x00018504
		public override void UpdateUI(UIParams uParams)
		{
			this.uiParams = uParams;
			this.titleWidth = GUILayout.Width((float)this.uiParams.fontSize * 20f);
			this.titleHeight = GUILayout.Height(this.uiParams.titleBarRect.height);
			float num = this.uiParams.colorRect.width - 30f;
			this.toggleWidth = GUILayout.Width(num * 0.4f);
			this.otherWidth = GUILayout.Width(num * 0.6f);
			this.baseHeight = this.uiParams.winRect.height - (float)this.uiParams.itemHeight * 3f - this.uiParams.titleBarRect.height;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0001A3C5 File Offset: 0x000185C5
		public void Update()
		{
			this.boneRenderer.Update();
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0001A3D2 File Offset: 0x000185D2
		public override void Clear()
		{
			this.boneRenderer.Clear();
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0001A3DF File Offset: 0x000185DF
		public override void Dispose()
		{
			this.boneRenderer.Clear();
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0001A3EC File Offset: 0x000185EC
		public void Show()
		{
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			try
			{
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.Label("ボーン表示用スロット 選択", this.uiParams.lStyleB, new GUILayoutOption[]
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
						if (this.boneRenderer.IsEnabled() && this.boneRenderer.TargetId != currentMaid.GetInstanceID())
						{
							this.boneRenderer.Clear();
							this.boneVisible = false;
						}
						if (this.sliderHelper.DrawColorSlider("色設定", ref this.editColor, SliderHelper.DEFAULT_PRESET, ref this.editExpand, this.picker))
						{
							this.boneRenderer.Color = this.editColor.val;
						}
						if (57 <= currentMaid.body0.goSlot.Count)
						{
							float num = this.editExpand ? ((float)this.uiParams.unitHeight * 5f) : ((float)this.uiParams.itemHeight - (float)this.uiParams.margin * 3f);
							if (this.editExpand && this.picker.expand)
							{
								num += (float)ColorPicker.LightTex.height + (float)this.uiParams.margin * 2f;
							}
							float height = this.baseHeight - num;
							GUILayout.BeginHorizontal(new GUILayoutOption[0]);
							try
							{
								GUI.enabled = (this.selectedSlotID != -1 && this.boneRenderer.IsEnabled());
								string text;
								if (this.boneVisible)
								{
									this.colorStore.SetColor(Color.white, new Color?(Color.green));
									text = "ボーン表示";
								}
								else
								{
									text = "ボーン非表示";
								}
								try
								{
									if (GUILayout.Button(text, this.uiParams.bStyle, new GUILayoutOption[]
									{
										this.uiParams.optBtnHeight,
										this.toggleWidth
									}))
									{
										this.boneVisible = !this.boneVisible;
										this.boneRenderer.SetVisible(this.boneVisible);
										TBodySkin tbodySkin = currentMaid.body0.goSlot[this.selectedSlotID];
										if (this.boneVisible && !this.boneRenderer.IsEnabled() && tbodySkin != null && tbodySkin.obj != null)
										{
											this.boneRenderer.Setup(tbodySkin.obj, tbodySkin.RID);
											this.boneRenderer.TargetId = currentMaid.GetInstanceID();
										}
									}
								}
								finally
								{
									this.colorStore.Restore();
								}
								GUI.enabled = true;
								if (this.skipEmptySlot)
								{
									this.colorStore.SetColor(Color.white, new Color?(Color.green));
									text = "空スロット省略";
								}
								else
								{
									text = "全スロット表示";
								}
								try
								{
									if (GUILayout.Button(text, this.uiParams.bStyle, new GUILayoutOption[]
									{
										this.uiParams.optBtnHeight,
										this.toggleWidth
									}))
									{
										this.skipEmptySlot = !this.skipEmptySlot;
									}
								}
								finally
								{
									this.colorStore.Restore();
								}
							}
							finally
							{
								GUILayout.EndHorizontal();
							}
							this.scrollViewPosition = GUILayout.BeginScrollView(this.scrollViewPosition, new GUILayoutOption[]
							{
								GUILayout.Width(this.uiParams.colorRect.width),
								GUILayout.Height(height)
							});
							try
							{
								for (int i = 0; i < this.slotNames.Length; i++)
								{
									TBodySkin tbodySkin2 = currentMaid.body0.goSlot[i];
									bool flag = tbodySkin2.obj != null && tbodySkin2.morph != null && tbodySkin2.obj.activeSelf;
									if (!this.skipEmptySlot || flag)
									{
										GUILayout.BeginHorizontal(new GUILayoutOption[0]);
										try
										{
											GUI.enabled = flag;
											bool flag2 = i == this.selectedSlotID;
											bool flag3 = GUILayout.Toggle(flag2, this.slotNames[i], this.uiParams.tStyleS, new GUILayoutOption[]
											{
												this.toggleWidth
											});
											if (flag3)
											{
												if (flag2)
												{
													if (tbodySkin2.obj != null)
													{
														if (this.boneRenderer.ItemID != tbodySkin2.RID)
														{
															this.boneRenderer.Setup(tbodySkin2.obj, tbodySkin2.RID);
															this.boneRenderer.SetVisible(this.boneVisible);
															this.boneRenderer.TargetId = currentMaid.GetInstanceID();
														}
													}
													else
													{
														this.boneVisible = false;
													}
												}
												else
												{
													this.selectedSlotID = i;
													if (tbodySkin2.obj != null)
													{
														this.boneRenderer.Setup(tbodySkin2.obj, tbodySkin2.RID);
														this.boneRenderer.SetVisible(this.boneVisible);
														this.boneRenderer.TargetId = currentMaid.GetInstanceID();
													}
												}
											}
											GUI.enabled = true;
											if (flag)
											{
												string text2 = tbodySkin2.m_strModelFileName ?? string.Empty;
												GUILayout.Label(text2, this.uiParams.lStyleS, new GUILayoutOption[]
												{
													this.otherWidth
												});
											}
										}
										finally
										{
											GUILayout.EndHorizontal();
										}
									}
								}
							}
							finally
							{
								GUILayout.EndScrollView();
							}
						}
					}
				}
			}
			finally
			{
				GUILayout.EndVertical();
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0001AA18 File Offset: 0x00018C18
		private string[] CreateSlotNames()
		{
			string[] names = Enum.GetNames(typeof(TBody.SlotID));
			string[] array = new string[57];
			int num = 0;
			foreach (string text in names)
			{
				if (num >= 57)
				{
					break;
				}
				array[num++] = text;
			}
			if (LogUtil.IsDebug())
			{
				LogUtil.Debug(new object[]
				{
					"slotNames:",
					array.ToString()
				});
			}
			return array;
		}

		// Token: 0x040003A4 RID: 932
		private readonly CustomBoneRenderer boneRenderer;

		// Token: 0x040003A5 RID: 933
		private readonly SliderHelper sliderHelper;

		// Token: 0x040003A6 RID: 934
		private readonly GUIColorStore colorStore = new GUIColorStore();

		// Token: 0x040003A7 RID: 935
		private readonly string[] slotNames;

		// Token: 0x040003A8 RID: 936
		private GUILayoutOption titleWidth;

		// Token: 0x040003A9 RID: 937
		private GUILayoutOption titleHeight;

		// Token: 0x040003AA RID: 938
		private GUILayoutOption toggleWidth;

		// Token: 0x040003AB RID: 939
		private GUILayoutOption otherWidth;

		// Token: 0x040003AC RID: 940
		private float baseHeight;

		// Token: 0x040003AD RID: 941
		private EditColor editColor = new EditColor(Color.white, ColorType.rgba, EditColor.RANGE, EditColor.RANGE);

		// Token: 0x040003AE RID: 942
		private bool editExpand;

		// Token: 0x040003AF RID: 943
		private Vector2 scrollViewPosition = Vector2.zero;

		// Token: 0x040003B0 RID: 944
		private int selectedSlotID;

		// Token: 0x040003B1 RID: 945
		private bool boneVisible;

		// Token: 0x040003B2 RID: 946
		private bool skipEmptySlot = true;

		// Token: 0x040003B3 RID: 947
		private readonly ColorPicker picker;
	}
}
