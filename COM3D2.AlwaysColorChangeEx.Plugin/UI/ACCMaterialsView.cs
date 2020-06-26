using System;
using CM3D2.AlwaysColorChangeEx.Plugin.Data;
using CM3D2.AlwaysColorChangeEx.Plugin.UI.Data;
using CM3D2.AlwaysColorChangeEx.Plugin.UI.Helper;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.UI
{
	// Token: 0x0200006A RID: 106
	internal class ACCMaterialsView
	{

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0001AA94 File Offset: 0x00018C94
		private static GUIContent[] ShaderNames
		{
			get
			{
				if (ACCMaterialsView.shaderNames != null)
				{
					return ACCMaterialsView.shaderNames;
				}
				int num = ShaderType.shaders.Length;
				ACCMaterialsView.shaderNames = new GUIContent[num];
				foreach (ShaderType shaderType in ShaderType.shaders)
				{
					ACCMaterialsView.shaderNames[shaderType.idx] = new GUIContent(shaderType.name, shaderType.dispName);
				}
				return ACCMaterialsView.shaderNames;
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0001AAFB File Offset: 0x00018CFB
		public static void Init(UIParams uiparams)
		{
			if (ACCMaterialsView.uiParams != null)
			{
				return;
			}
			ACCMaterialsView.uiParams = uiparams;
			ACCMaterialsView.uiParams.Add(ACCMaterialsView.updateUI);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0001AB1A File Offset: 0x00018D1A
		public static void Clear()
		{
			if (ACCMaterialsView.uiParams != null)
			{
				ACCMaterialsView.uiParams.Remove(ACCMaterialsView.updateUI);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0001AB33 File Offset: 0x00018D33
		private static GUIContent PlusIcon
		{
			get
			{
				GUIContent result;
				if ((result = ACCMaterialsView.plusIcon) == null)
				{
					result = (ACCMaterialsView.plusIcon = new GUIContent(ACCMaterialsView.resHolder.PlusImage));
				}
				return result;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0001AB53 File Offset: 0x00018D53
		private static GUIContent MinusIcon
		{
			get
			{
				GUIContent result;
				if ((result = ACCMaterialsView.minusIcon) == null)
				{
					result = (ACCMaterialsView.minusIcon = new GUIContent(ACCMaterialsView.resHolder.MinusImage));
				}
				return result;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0001AB73 File Offset: 0x00018D73
		private static GUIContent CopyIcon
		{
			get
			{
				GUIContent result;
				if ((result = ACCMaterialsView.copyIcon) == null)
				{
					result = (ACCMaterialsView.copyIcon = new GUIContent("コピー", ACCMaterialsView.resHolder.CopyImage, "マテリアル情報をクリップボードへコピーする"));
				}
				return result;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0001ABA0 File Offset: 0x00018DA0
		private static GUIContent[] PasteIcons
		{
			get
			{
				GUIContent[] result;
				if ((result = ACCMaterialsView.pasteIcons) == null)
				{
					result = (ACCMaterialsView.pasteIcons = new GUIContent[]
					{
						new GUIContent("全貼付", ACCMaterialsView.resHolder.PasteImage, "クリップボードからマテリアル情報を貼付ける"),
						new GUIContent("指定貼付", ACCMaterialsView.resHolder.PasteImage, "クリップボードからマテリアル情報を貼付ける")
					});
				}
				return result;
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0001ABFC File Offset: 0x00018DFC
		private static Texture2D Copy(Texture2D src)
		{
			Texture2D texture2D = new Texture2D(src.width, src.height);
			Color32[] pixels = src.GetPixels32();
			for (int i = 0; i < pixels.Length; i++)
			{
				pixels[i].r = (byte)(pixels[i].r / 2);
				pixels[i].g = (byte)(pixels[i].g / 2);
				pixels[i].b = (byte)(pixels[i].b / 2);
				pixels[i].a = (byte)(pixels[i].a / 2);
			}
			texture2D.SetPixels32(pixels);
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0001ACAA File Offset: 0x00018EAA
		public ACCMaterialsView(Renderer r, Material m, int slotIdx, int idx, SliderHelper sliderHelper, CheckboxHelper cbHelper)
		{
			this.slotIdx = slotIdx;
			this.matIdx = idx;
			this.edited = new ACCMaterial(m, r, idx);
			this.sliderHelper = sliderHelper;
			this.cbHelper = cbHelper;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0001AD6C File Offset: 0x00018F6C
		public void Show(bool reload)
		{
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			try
			{
				//ACCMaterialsView.<>c__DisplayClass4 CS$<>8__locals1 = new ACCMaterialsView.<>c__DisplayClass4();
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				try
				{
					GUIContent content = this.expand ? ACCMaterialsView.MinusIcon : ACCMaterialsView.PlusIcon;
					if (GUILayout.Button(content, ACCMaterialsView.bStyleLeft, new GUILayoutOption[]
					{
						ACCMaterialsView.optUnitHeight,
						ACCMaterialsView.optIconWidth
					}))
					{
						this.expand = !this.expand;
					}
					if (GUILayout.Button(this.edited.name, ACCMaterialsView.bStyleLeft, new GUILayoutOption[]
					{
						ACCMaterialsView.optUnitHeight
					}))
					{
						this.expand = !this.expand;
					}
					if (!this.expand)
					{
						return;
					}
				}
				finally
				{
					GUILayout.EndHorizontal();
				}
				if (this.edited.type == ShaderType.UNKNOWN)
				{
					GUILayout.Label("shader: " + this.edited.material.shader.name, new GUILayoutOption[0]);
				}
				else
				{
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					try
					{
						if (GUILayout.Button(ACCMaterialsView.CopyIcon, new GUILayoutOption[]
						{
							ACCMaterialsView.optUnitHeight,
							ACCMaterialsView.optButonWidthS
						}))
						{
							ACCMaterialsView.clipHandler.SetClipboard(MateHandler.Instance.ToText(this.edited));
							if (this.tipsCall != null)
							{
								this.tipsCall("マテリアル情報をクリップボードに\nコピーしました");
							}
						}
						GUI.enabled &= ACCMaterialsView.clipHandler.isMateText;
						GUIContent[] array = ACCMaterialsView.PasteIcons;
						if (GUILayout.Button(array[0], new GUILayoutOption[]
						{
							ACCMaterialsView.optUnitHeight,
							ACCMaterialsView.optButonWidthS
						}))
						{
							try
							{
								MateHandler.Instance.Write(this.edited, ACCMaterialsView.clipHandler.mateText);
								if (this.tipsCall != null)
								{
									this.tipsCall("マテリアル情報を貼付けました");
								}
							}
							catch (Exception ex)
							{
								LogUtil.Error(new object[]
								{
									"failed to import mateText",
									ex
								});
							}
						}
						ACCMaterialsView.includeOthers = GUILayout.Toggle(ACCMaterialsView.includeOthers, "CF", ACCMaterialsView.uiParams.tStyleSS, new GUILayoutOption[0]);
						ACCMaterialsView.includeShader = GUILayout.Toggle(ACCMaterialsView.includeShader, "S", ACCMaterialsView.uiParams.tStyleSS, new GUILayoutOption[0]);
						ACCMaterialsView.includeTex = GUILayout.Toggle(ACCMaterialsView.includeTex, "T", ACCMaterialsView.uiParams.tStyleSS, new GUILayoutOption[0]);
						GUI.enabled &= (ACCMaterialsView.includeTex | ACCMaterialsView.includeShader | ACCMaterialsView.includeOthers);
						if (GUILayout.Button(array[1], new GUILayoutOption[]
						{
							ACCMaterialsView.optUnitHeight,
							ACCMaterialsView.optButonWidth
						}))
						{
							try
							{
								int num = 0;
								if (ACCMaterialsView.includeTex)
								{
									num |= MateHandler.MATE_TEX;
								}
								if (ACCMaterialsView.includeShader)
								{
									num |= MateHandler.MATE_SHADER;
								}
								if (ACCMaterialsView.includeOthers)
								{
									num |= (MateHandler.MATE_COLOR | MateHandler.MATE_FLOAT);
								}
								LogUtil.DebugF("material pasting from cp... tex={0}, shader={1}, others={2}", new object[]
								{
									ACCMaterialsView.includeTex,
									ACCMaterialsView.includeShader,
									ACCMaterialsView.includeOthers
								});
								MateHandler.Instance.Write(this.edited, ACCMaterialsView.clipHandler.mateText, num);
							}
							catch (Exception ex2)
							{
								LogUtil.Error(new object[]
								{
									"failed to import mateText",
									ex2
								});
							}
							if (this.tipsCall != null)
							{
								this.tipsCall("マテリアル情報を貼付けました");
							}
						}
					}
					finally
					{
						GUI.enabled = true;
						GUILayout.EndHorizontal();
					}
					var material = this.edited.material;
					int idx = this.edited.type.idx;
					if (this.shaderCombo == null)
					{
						GUIContent buttonContent = (idx >= 0 && idx < ACCMaterialsView.ShaderNames.Length) ? ACCMaterialsView.ShaderNames[idx] : GUIContent.none;
						this.shaderCombo = new ComboBoxLO(buttonContent, ACCMaterialsView.ShaderNames, ACCMaterialsView.uiParams.bStyleSC, ACCMaterialsView.uiParams.boxStyle, ACCMaterialsView.uiParams.listStyle, false);
					}
					else
					{
						this.shaderCombo.SelectedItemIndex = idx;
					}
					this.shaderCombo.Show(GUILayout.ExpandWidth(true));
					int selectedItemIndex = this.shaderCombo.SelectedItemIndex;
					if (idx != selectedItemIndex && selectedItemIndex != -1)
					{
						LogUtil.Debug(new object[]
						{
							"shader changed",
							idx,
							"=>",
							selectedItemIndex
						});
						string text = ACCMaterialsView.ShaderNames[selectedItemIndex].text;
						this.edited.ChangeShader(text, selectedItemIndex);
					}
					if (reload)
					{
						this.edited.renderQueue.Set(material.renderQueue);
					}
					this.sliderHelper.SetupFloatSlider("RQ", this.edited.renderQueue, this.edited.renderQueue.range.editMin, this.edited.renderQueue.range.editMax, delegate(float rq)
					{
						material.SetFloat(ShaderPropType.RenderQueue.propId, rq);
						material.renderQueue = (int)rq;
					}, ShaderPropType.RenderQueue.opts, ShaderPropType.RenderQueue.presetVals, ACCMaterialsView.rqResolver.Resolve(this.slotIdx));
					ShaderType type = this.edited.type;
					for (int i = 0; i < type.colProps.Length; i++)
					{
						ShaderPropColor shaderPropColor = type.colProps[i];
						EditColor editColor = this.edited.editColors[i];
						ColorPicker picker = this.edited.pickers[i];
						if (reload)
						{
							editColor.Set(material.GetColor(shaderPropColor.propId));
						}
						if (this.sliderHelper.DrawColorSlider(shaderPropColor, ref editColor, picker))
						{
							material.SetColor(shaderPropColor.propId, editColor.val);
						}
					}
					for (int j = 0; j < type.fProps.Length; j++)
					{
						//ACCMaterialsView.<>c__DisplayClass8 CS$<>8__locals2 = new ACCMaterialsView.<>c__DisplayClass8();
						//CS$<>8__locals2.CS$<>8__locals5 = CS$<>8__locals1;
						var prop = type.fProps[j];
						if (reload)
						{
							this.edited.editVals[j].Set(material.GetFloat(prop.propId));
						}
						switch (prop.valType)
						{
						case ValType.Float:
						{
							ShaderPropFloat fprop = prop;
							this.sliderHelper.SetupFloatSlider(fprop, this.edited.editVals[j], delegate(float val)
							{
								fprop.SetValue(material, val);
							});
							break;
						}
						case ValType.Bool:
							this.cbHelper.ShowCheckBox(prop.name, this.edited.editVals[j], delegate(float val)
							{
								prop.SetValue(material, val);
							});
							break;
						case ValType.Enum:
							this.cbHelper.ShowComboBox(prop.name, this.edited.editVals[j], delegate(int val)
							{
								prop.SetValue(material, (float)val);
							});
							break;
						}
					}
				}
			}
			finally
			{
				GUILayout.EndVertical();
			}
		}

		// Token: 0x040003B4 RID: 948
		private static GUIContent[] shaderNames;

		// Token: 0x040003B5 RID: 949
		private static readonly ResourceHolder resHolder = ResourceHolder.Instance;

		// Token: 0x040003B6 RID: 950
		private static GUIContent plusIcon;

		// Token: 0x040003B7 RID: 951
		private static GUIContent minusIcon;

		// Token: 0x040003B8 RID: 952
		private static GUIContent copyIcon;

		// Token: 0x040003B9 RID: 953
		private static GUIContent[] pasteIcons;

		// Token: 0x040003BA RID: 954
		private static UIParams uiParams;

		// Token: 0x040003BB RID: 955
		private static readonly GUIStyle bStyleLeft = new GUIStyle("label");

		// Token: 0x040003BC RID: 956
		private static GUILayoutOption optUnitHeight;

		// Token: 0x040003BD RID: 957
		private static GUILayoutOption optButonWidthS;

		// Token: 0x040003BE RID: 958
		private static GUILayoutOption optButonWidth;

		// Token: 0x040003BF RID: 959
		private static GUILayoutOption optIconWidth;

		// Token: 0x040003C0 RID: 960
		private static readonly Action<UIParams> updateUI = delegate(UIParams uiparams)
		{
			ACCMaterialsView.optIconWidth = GUILayout.Width(16f);
			ACCMaterialsView.optButonWidth = GUILayout.Width((uiparams.textureRect.width - 20f) * 0.23f);
			ACCMaterialsView.optButonWidthS = GUILayout.Width((uiparams.textureRect.width - 20f) * 0.2f);
			ACCMaterialsView.optUnitHeight = GUILayout.Height((float)uiparams.unitHeight);
			ACCMaterialsView.bStyleLeft.fontStyle = uiparams.lStyleC.fontStyle;
			ACCMaterialsView.bStyleLeft.fontSize = uiparams.fontSize;
			ACCMaterialsView.bStyleLeft.normal.textColor = uiparams.lStyleC.normal.textColor;
			ACCMaterialsView.bStyleLeft.alignment = TextAnchor.MiddleLeft;
		};

		// Token: 0x040003C1 RID: 961
		private static bool includeTex;

		// Token: 0x040003C2 RID: 962
		private static bool includeShader = true;

		// Token: 0x040003C3 RID: 963
		private static bool includeOthers = true;

		// Token: 0x040003C4 RID: 964
		internal static readonly RQResolver rqResolver = RQResolver.Instance;

		// Token: 0x040003C5 RID: 965
		private static readonly ClipBoardHandler clipHandler = ClipBoardHandler.Instance;

		// Token: 0x040003C6 RID: 966
		internal readonly SliderHelper sliderHelper;

		// Token: 0x040003C7 RID: 967
		internal readonly CheckboxHelper cbHelper;

		// Token: 0x040003C8 RID: 968
		public readonly ACCMaterial edited;

		// Token: 0x040003C9 RID: 969
		public ComboBoxLO shaderCombo;

		// Token: 0x040003CA RID: 970
		public bool expand;

		// Token: 0x040003CB RID: 971
		private int matIdx;

		// Token: 0x040003CC RID: 972
		internal int slotIdx;

		// Token: 0x040003CD RID: 973
		public Action<string> tipsCall;
	}
}
