using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CM3D2.AlwaysColorChangeEx.Plugin.Data;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.UI
{
	// Token: 0x0200006E RID: 110
	public class ACCTexturesView
	{
		// Token: 0x06000362 RID: 866 RVA: 0x0001E3C9 File Offset: 0x0001C5C9
		public static void Init(UIParams uiparams)
		{
			TextureModifier.uiParams = uiparams;
			if (ACCTexturesView.uiParams != null)
			{
				return;
			}
			ACCTexturesView.uiParams = uiparams;
			ACCTexturesView.uiParams.Add(ACCTexturesView.updateUI);
			ACCTexturesView.InitUIParams(uiparams);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0001E3F4 File Offset: 0x0001C5F4
		public static void Clear()
		{
			ACCTexturesView.textureModifier.Clear();
			if (ACCTexturesView.uiParams != null)
			{
				ACCTexturesView.uiParams.Remove(ACCTexturesView.updateUI);
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0001E418 File Offset: 0x0001C618
		private static void InitUIParams(UIParams uiparam)
		{
			ACCTexturesView.inboxStyle.normal.background = new Texture2D(1, 1);
			Color color = new Color(0.3f, 0.3f, 0.3f, 0.3f);
			Color[] pixels = ACCTexturesView.inboxStyle.normal.background.GetPixels();
			for (int i = 0; i < pixels.Length; i++)
			{
				pixels[i] = color;
			}
			ACCTexturesView.inboxStyle.normal.background.SetPixels(pixels);
			ACCTexturesView.inboxStyle.normal.background.Apply();
			ACCTexturesView.inboxStyle.padding.left = (ACCTexturesView.inboxStyle.padding.right = 2);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0001E4D1 File Offset: 0x0001C6D1
		public static bool IsChangeTarget()
		{
			return ACCTexturesView.editTarget.IsValid();
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001E4DD File Offset: 0x0001C6DD
		public static void ClearTarget()
		{
			ACCTexturesView.editTarget.Clear();
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0001E4E9 File Offset: 0x0001C6E9
		public static void UpdateTex(Maid maid, Material[] slotMaterials)
		{
			ACCTexturesView.textureModifier.UpdateTex(maid, slotMaterials, ACCTexturesView.editTarget);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0001E4FD File Offset: 0x0001C6FD
		public static bool IsChangedTexColor(Maid maid, string slot, Material material, string propName)
		{
			return ACCTexturesView.textureModifier.IsChanged(maid, slot, material, propName);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001E50D File Offset: 0x0001C70D
		public static TextureModifier.FilterParam GetFilter(Maid maid, string slot, Material material, int propId)
		{
			return ACCTexturesView.textureModifier.GetFilter(maid, slot, material, propId);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0001E51D File Offset: 0x0001C71D
		public static TextureModifier.FilterParam GetFilter(Maid maid, string slot, Material material, string propName)
		{
			return ACCTexturesView.textureModifier.GetFilter(maid, slot, material, propName);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0001E52D File Offset: 0x0001C72D
		public static Texture2D Filter(Texture2D srcTex, TextureModifier.FilterParam filterParam)
		{
			return ACCTexturesView.textureModifier.ApplyFilter(srcTex, filterParam);
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0001E53C File Offset: 0x0001C73C
		private static GUIContent[] ItemNames
		{
			get
			{
				if (ACCTexturesView.itemNames != null)
				{
					return ACCTexturesView.itemNames;
				}
				HashSet<string> hashSet = new HashSet<string>();
				List<GUIContent> list = new List<GUIContent>();
				foreach (string text in ACCTexturesView.settings.toonTexes)
				{
					string text2 = text + FileConst.EXT_TEXTURE;
					hashSet.Add(text2.ToLower());
					Texture2D texture2D = ACCTexturesView.Load(text2);
					if (!(texture2D == null))
					{
						list.Add(new GUIContent(text, texture2D, text));
					}
				}
				foreach (string text3 in ACCTexturesView.settings.toonTexAddon)
				{
					string text4 = text3;
					if (text3.LastIndexOf('.') == -1)
					{
						text4 += FileConst.EXT_TEXTURE;
					}
					string item = text4.ToLower();
					if (!hashSet.Contains(item))
					{
						Texture2D texture2D2 = ACCTexturesView.Load(text4);
						if (texture2D2 != null)
						{
							list.Add(new GUIContent(text3, texture2D2, text3));
						}
						hashSet.Add(item);
					}
				}
				ACCTexturesView.itemNames = list.ToArray();
				return ACCTexturesView.itemNames;
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001E658 File Offset: 0x0001C858
		private static Texture2D Load(string texfile)
		{
			if (!ACCTexturesView.outUtil.Exists(texfile))
			{
				return null;
			}
			Texture2D texture2D = null;
			LogUtil.Debug(new object[]
			{
				"load tex:",
				texfile
			});
			try
			{
				texture2D = ACCTexturesView.outUtil.LoadTexture(texfile);
				if (texture2D.width <= 1 || texture2D.height <= 1)
				{
					TextureScale.Point(texture2D, 90, 5);
				}
				else
				{
					TextureScale.Bilinear(texture2D, 90, 5);
				}
			}
			catch (Exception ex)
			{
				LogUtil.Debug(new object[]
				{
					ex
				});
			}
			return texture2D;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0001E6E8 File Offset: 0x0001C8E8
		private static int GetIndex(string rampName)
		{
			for (int i = 0; i < ACCTexturesView.ItemNames.Length; i++)
			{
				if (ACCTexturesView.ItemNames[i].text == rampName)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0001E744 File Offset: 0x0001C944
		public static List<ACCTexture> Load(Material mate, ShaderType type1)
		{
			if (type1 == null)
			{
				type1 = ShaderType.Resolve(mate.shader.name);
			}
			List<ACCTexture> list = new List<ACCTexture>(type1.texProps.Length);
			list.AddRange(from texProp in type1.texProps
			select ACCTexture.Create(mate, texProp, type1) into tex
			where tex != null
			select tex);
			return list;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0001E7E0 File Offset: 0x0001C9E0
		public ACCTexturesView(Material m, int matNo)
		{
			this.material = m;
			ShaderType type = ShaderType.Resolve(m.shader.name);
			this.original = ACCTexturesView.Load(m, type);
			this.edited = new List<ACCTexture>(this.original.Count);
			foreach (ACCTexture src in this.original)
			{
				this.edited.Add(new ACCTexture(src));
			}
			this.matNo = matNo;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0001E8C0 File Offset: 0x0001CAC0
		public void Show()
		{
			GUILayout.BeginVertical(ACCTexturesView.inboxStyle, new GUILayoutOption[0]);
			try
			{
				string text = (this.expand ? "- " : "+ ") + this.material.name;
				if (GUILayout.Button(text, ACCTexturesView.uiParams.lStyleC, new GUILayoutOption[0]))
				{
					this.expand = !this.expand;
				}
				if (this.expand)
				{
					foreach (ACCTexture acctexture in this.edited)
					{
						bool flag = this.matNo == ACCTexturesView.editTarget.matNo && acctexture.propKey == ACCTexturesView.editTarget.propKey;
						if (acctexture.prop.Keyword == Keyword.NONE || this.material.IsKeywordEnabled(acctexture.prop.KeywordString))
						{
							GUILayout.BeginHorizontal(new GUILayoutOption[0]);
							try
							{
								if (!ACCTexturesView.textureModifier.IsValidTarget(ACCTexturesView.holder.CurrentMaid, ACCTexturesView.holder.CurrentSlot.Name, this.material, acctexture.propName))
								{
									bool enabled = GUI.enabled;
									GUI.enabled = false;
									GUILayout.Button("+変更", ACCTexturesView.uiParams.bStyle, new GUILayoutOption[]
									{
										ACCTexturesView.buttonLWidth
									});
									GUI.enabled = enabled;
								}
								else if (flag)
								{
									if (GUILayout.Button("-変更", ACCTexturesView.uiParams.bStyle, new GUILayoutOption[]
									{
										ACCTexturesView.buttonLWidth
									}))
									{
										ACCTexturesView.editTarget.Clear();
									}
								}
								else if (GUILayout.Button("+変更", ACCTexturesView.uiParams.bStyle, new GUILayoutOption[]
								{
									ACCTexturesView.buttonLWidth
								}))
								{
									ACCTexturesView.editTarget.slotName = ACCTexturesView.holder.CurrentSlot.Name;
									ACCTexturesView.editTarget.matNo = this.matNo;
									ACCTexturesView.editTarget.propName = acctexture.propName;
									ACCTexturesView.editTarget.propKey = acctexture.propKey;
								}
								GUILayout.Label(acctexture.propName, ACCTexturesView.uiParams.lStyle, new GUILayoutOption[0]);
								if (flag && acctexture.type.hasShadow && acctexture.propKey == ShaderPropType.MainTex.key && GUILayout.Button("_ShadowTexに反映", ACCTexturesView.uiParams.bStyleSC, new GUILayoutOption[0]))
								{
									ACCTexturesView.textureModifier.DuplicateFilter(ACCTexturesView.holder.CurrentMaid, ACCTexturesView.holder.CurrentSlot.Name, this.material, acctexture.propName, "_ShadowTex");
								}
								if (GUILayout.Button("opt", ACCTexturesView.uiParams.bStyleSC, new GUILayoutOption[]
								{
									ACCTexturesView.restWidth
								}))
								{
									acctexture.expand = !acctexture.expand;
								}
							}
							finally
							{
								GUILayout.EndHorizontal();
							}
							if (flag)
							{
								ACCTexturesView.textureModifier.ProcGUI(ACCTexturesView.holder.CurrentMaid, ACCTexturesView.holder.CurrentSlot.Name, this.material, acctexture.propName);
							}
							float height = (float)ACCTexturesView.uiParams.itemHeight;
							ComboBoxLO comboBoxLO = null;
							bool flag2 = false;
							if (acctexture.toonType != 0)
							{
								if (this.combos.TryGetValue(acctexture.propName, out comboBoxLO))
								{
									if (comboBoxLO.IsClickedComboButton)
									{
										height = (float)(comboBoxLO.ItemCount * ACCTexturesView.uiParams.itemHeight) * 0.8f;
									}
								}
								else
								{
									comboBoxLO = new ComboBoxLO(new GUIContent("選"), ACCTexturesView.ItemNames, ACCTexturesView.uiParams.bStyle, ACCTexturesView.uiParams.boxStyle, ACCTexturesView.uiParams.listStyle, true);
									comboBoxLO.SetItemWidth(ACCTexturesView.comboWidth);
									this.combos[acctexture.propName] = comboBoxLO;
									comboBoxLO.SelectItem(acctexture.editname);
								}
							}
							GUILayout.BeginHorizontal(new GUILayoutOption[]
							{
								GUILayout.Height(height)
							});
							try
							{
								bool flag3 = false;
								string text2 = acctexture.editname;
								if (acctexture.toonType != 0)
								{
									int selectedItemIndex = comboBoxLO.SelectedItemIndex;
									int num = comboBoxLO.Show(ACCTexturesView.uiParams.optBtnWidth);
									if (num != selectedItemIndex)
									{
										text2 = ACCTexturesView.ItemNames[num].text;
										flag2 = true;
									}
									flag3 = comboBoxLO.IsClickedComboButton;
								}
								else
								{
									GUILayout.Label(string.Empty, new GUILayoutOption[]
									{
										ACCTexturesView.uiParams.optBtnWidth
									});
								}
								if (flag3)
								{
									ACCTexturesView.uiParams.textStyle.fontSize = (int)((double)ACCTexturesView.fontSizeS * 0.8);
								}
								text2 = GUILayout.TextField(text2, ACCTexturesView.uiParams.textStyle, new GUILayoutOption[]
								{
									ACCTexturesView.contentWidth
								});
								acctexture.SetName(text2);
								if (flag3)
								{
									ACCTexturesView.uiParams.textStyle.fontSize = ACCTexturesView.fontSize;
								}
								GUI.enabled = acctexture.dirty;
								if ((ACCTexturesView.settings.toonComboAutoApply && flag2) || GUILayout.Button("適", ACCTexturesView.uiParams.bStyle, new GUILayoutOption[]
								{
									ACCTexturesView.uiParams.optBtnWidth
								}))
								{
									Texture texture = this.ChangeTexFile(this.textureDir, acctexture.editname, this.matNo, acctexture.propName);
									if (texture != null)
									{
										acctexture.tex = texture;
									}
									acctexture.dirty = false;
								}
								GUI.enabled = true;
								if (GUILayout.Button("...", ACCTexturesView.uiParams.bStyle, new GUILayoutOption[]
								{
									ACCTexturesView.uiParams.optBtnWidth
								}))
								{
									this.OpenFileBrowser(this.matNo, acctexture);
								}
							}
							finally
							{
								GUILayout.EndHorizontal();
							}
							if (acctexture.expand)
							{
								GUILayout.BeginVertical(new GUILayoutOption[0]);
								try
								{
									Vector2 textureOffset = this.material.GetTextureOffset(acctexture.prop.propId);
									ACCTexture tex = acctexture;
									if (this.DrawSliders("offset", ref textureOffset, -1f, 1f, () => tex.original.texOffset, false))
									{
										this.material.SetTextureOffset(acctexture.prop.propId, textureOffset);
									}
									Vector2 textureScale = this.material.GetTextureScale(acctexture.prop.propId);
									if (this.DrawSliders("scale ", ref textureScale, 0.001f, 20f, () => tex.original.texScale, true))
									{
										this.material.SetTextureScale(acctexture.prop.propId, textureScale);
									}
								}
								finally
								{
									GUILayout.EndVertical();
								}
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

		// Token: 0x06000372 RID: 882 RVA: 0x0001EFC0 File Offset: 0x0001D1C0
		private bool DrawTextField(float val, ref float editedVal)
		{
			string text = val.ToString("F3");
			string text2 = GUILayout.TextField(text, ACCTexturesView.uiParams.textStyleSC, new GUILayoutOption[]
			{
				ACCTexturesView.optInputWidth
			});
			return !(text == text2) && float.TryParse(text2, out editedVal) && !NumberUtil.Equals(val, editedVal, 0.001f);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0001F024 File Offset: 0x0001D224
		private bool DrawSliders(string label, ref Vector2 val, float min, float max, Func<Vector2> GetDefault, bool useLog = false)
		{
			bool result = false;
			float rightValue = useLog ? Mathf.Log10(max) : max;
			float leftValue = useLog ? Mathf.Log10(min) : min;
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			try
			{
				GUILayout.Label(label, ACCTexturesView.uiParams.lStyleS, new GUILayoutOption[]
				{
					ACCTexturesView.optLabelWidth
				});
				float num = val.x;
				if (this.DrawTextField(val.x, ref num))
				{
					if (num > max)
					{
						num = max;
					}
					else if (num < min)
					{
						num = min;
					}
					val.x = num;
					result = true;
				}
				GUILayout.BeginVertical(new GUILayoutOption[0]);
				GUILayout.Space(ACCTexturesView.sliderMargin);
				float num2 = useLog ? Mathf.Log10(val.x) : val.x;
				float num3 = GUILayout.HorizontalSlider(num2, leftValue, rightValue, new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(true)
				});
				GUILayout.EndVertical();
				if (!NumberUtil.Equals(num2, num3, 0.001f))
				{
					val.x = (useLog ? Mathf.Pow(10f, num3) : num3);
					result = true;
				}
			}
			finally
			{
				GUILayout.EndHorizontal();
			}
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			try
			{
				float num4 = val.y;
				if (GUILayout.Button("reset", ACCTexturesView.uiParams.bStyleSC, new GUILayoutOption[]
				{
					ACCTexturesView.optLabelWidth
				}))
				{
					val = GetDefault();
					result = true;
				}
				if (this.DrawTextField(val.y, ref num4))
				{
					if (num4 > max)
					{
						num4 = max;
					}
					else if (num4 < min)
					{
						num4 = min;
					}
					val.y = num4;
					result = true;
				}
				GUILayout.BeginVertical(new GUILayoutOption[0]);
				GUILayout.Space(ACCTexturesView.sliderMargin);
				float num5 = useLog ? Mathf.Log10(val.y) : val.y;
				float num6 = GUILayout.HorizontalSlider(num5, leftValue, rightValue, new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(true)
				});
				GUILayout.EndVertical();
				if (!NumberUtil.Equals(num5, num6, 0.001f))
				{
					val.y = (useLog ? Mathf.Pow(10f, num6) : num6);
					result = true;
				}
			}
			finally
			{
				GUILayout.EndHorizontal();
			}
			return result;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0001F2F8 File Offset: 0x0001D4F8
		private void OpenFileBrowser(int matNo1, ACCTexture acctex)
		{
			ACCTexturesView.fileBrowser = new FileBrowser(new Rect(0f, 0f, ACCTexturesView.uiParams.fileBrowserRect.width, ACCTexturesView.uiParams.fileBrowserRect.height), "テクスチャファイル選択", delegate(string path)
			{
				ACCTexturesView.fileBrowser = null;
				if (path == null)
				{
					return;
				}
				acctex.filepath = (this.textureDir = Path.GetDirectoryName(path));
				string fileName = Path.GetFileName(path);
				if (acctex.editname != fileName)
				{
					acctex.editname = fileName;
					acctex.dirty = true;
				}
				this.ChangeTexFile(this.textureDir, acctex.editname, matNo1, acctex.propName);
			});
			ResourceHolder instance = ResourceHolder.Instance;
			ACCTexturesView.fileBrowser.DirectoryImage = instance.DirImage;
			ACCTexturesView.fileBrowser.FileImage = instance.PictImage;
			ACCTexturesView.fileBrowser.NoFileImage = instance.FileImage;
			ACCTexturesView.fileBrowser.labelStyle = ACCTexturesView.uiParams.listStyle;
			ACCTexturesView.fileBrowser.SelectionPatterns = new string[]
			{
				"*.tex",
				"*.png"
			};
			if (!string.IsNullOrEmpty(this.textureDir))
			{
				ACCTexturesView.fileBrowser.CurrentDirectory = this.textureDir;
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0001F3F4 File Offset: 0x0001D5F4
		private Texture ChangeTexFile(string dir, string filename, int matNo1, string propName)
		{
			Texture2D texture2D = this.material.GetTexture(propName) as Texture2D;
			string text = Path.GetExtension(filename).ToLower();
			Texture texture;
			if (text.Length == 0 || text == FileConst.EXT_TEXTURE)
			{
				string name;
				if (text.Length == 0)
				{
					name = filename;
					filename += FileConst.EXT_TEXTURE;
				}
				else
				{
					name = filename.Substring(0, filename.Length - 4);
				}
				LogUtil.Debug(new object[]
				{
					"ChangeTex:",
					filename,
					", propName:",
					propName
				});
				ACCTexturesView.holder.CurrentMaid.body0.ChangeTex(ACCTexturesView.holder.CurrentSlot.Name, matNo1, propName, filename, null, MaidParts.PARTS_COLOR.NONE);
				texture = this.material.GetTexture(propName);
				if (texture != null)
				{
					texture.name = name;
				}
			}
			else
			{
				TBodySkin slot = ACCTexturesView.holder.CurrentMaid.body0.GetSlot((int)ACCTexturesView.holder.CurrentSlot.Id);
				Material material = ACCTexturesView.holder.GetMaterial(slot, matNo1);
				if (material == null)
				{
					return null;
				}
				byte[] data = UTY.LoadImage(Path.Combine(dir, filename));
				Texture2D texture2D2 = new Texture2D(1, 1, TextureFormat.ARGB32, false);
				texture2D2.LoadImage(data);
				slot.listDEL.Add(texture2D2);
				texture2D2.name = filename;
				LogUtil.Debug(new object[]
				{
					"SetTexture:",
					filename,
					", propName:",
					propName,
					", (",
					texture2D2.width,
					",",
					texture2D2.height,
					")"
				});
				material.SetTexture(propName, texture2D2);
				texture = texture2D2;
			}
			if (texture2D == null)
			{
				return texture;
			}
			ACCTexturesView.textureModifier.RemoveCache(texture2D);
			ACCTexturesView.textureModifier.RemoveFilter(ACCTexturesView.holder.CurrentMaid, ACCTexturesView.holder.CurrentSlot.Name, this.material, texture2D);
			return texture;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0001F610 File Offset: 0x0001D810
		private void MulTexSet(string filename, int matNo1, string propName)
		{
			GameUty.SystemMaterial f_eBlendMode;
			try
			{
				f_eBlendMode = (GameUty.SystemMaterial)Enum.Parse(typeof(GameUty.SystemMaterial), propName);
			}
			catch (ArgumentException ex)
			{
				LogUtil.Debug(new object[]
				{
					ex
				});
				f_eBlendMode = GameUty.SystemMaterial.Alpha;
			}
			if (filename.EndsWith(FileConst.EXT_TEXTURE, StringComparison.OrdinalIgnoreCase))
			{
				ACCTexturesView.holder.CurrentMaid.body0.MulTexSet(ACCTexturesView.holder.CurrentSlot.Name, matNo1, "_MainTex", 1, filename, f_eBlendMode, false, 0, 0, 0f, 0f, false, null, 1f, 1024);
				ACCTexturesView.holder.CurrentMaid.body0.MulTexSet(ACCTexturesView.holder.CurrentSlot.Name, matNo1, "_ShadowTex", 1, filename, f_eBlendMode, false, 0, 0, 0f, 0f, false, null, 1f, 1024);
			}
		}

		// Token: 0x040003FB RID: 1019
		private const int IMG_WIDTH = 90;

		// Token: 0x040003FC RID: 1020
		private const int IMG_HEIGHT = 5;

		// Token: 0x040003FD RID: 1021
		private static readonly MaidHolder holder = MaidHolder.Instance;

		// Token: 0x040003FE RID: 1022
		private static readonly FileUtilEx outUtil = FileUtilEx.Instance;

		// Token: 0x040003FF RID: 1023
		public static readonly EditTarget editTarget = new EditTarget();

		// Token: 0x04000400 RID: 1024
		private static readonly Settings settings = Settings.Instance;

		// Token: 0x04000401 RID: 1025
		private static readonly TextureModifier textureModifier = TextureModifier.Instance;

		// Token: 0x04000402 RID: 1026
		private static UIParams uiParams;

		// Token: 0x04000403 RID: 1027
		private static GUILayoutOption restWidth;

		// Token: 0x04000404 RID: 1028
		private static GUILayoutOption buttonLWidth;

		// Token: 0x04000405 RID: 1029
		private static GUILayoutOption contentWidth;

		// Token: 0x04000406 RID: 1030
		private static GUILayoutOption optItemHeight;

		// Token: 0x04000407 RID: 1031
		private static GUILayoutOption optLabelWidth;

		// Token: 0x04000408 RID: 1032
		private static GUILayoutOption optInputWidth;

		// Token: 0x04000409 RID: 1033
		private static float sliderMargin;

		// Token: 0x0400040A RID: 1034
		private static float labelWidth;

		// Token: 0x0400040B RID: 1035
		private static float comboWidth;

		// Token: 0x0400040C RID: 1036
		private static readonly GUIStyle inboxStyle = new GUIStyle("box");

		// Token: 0x0400040D RID: 1037
		private static int fontSize;

		// Token: 0x0400040E RID: 1038
		private static int fontSizeS;

		// Token: 0x0400040F RID: 1039
		private static readonly Action<UIParams> updateUI = delegate(UIParams uiparams)
		{
			float num = uiparams.textureRect.width - 20f;
			ACCTexturesView.buttonLWidth = GUILayout.Width(num * 0.2f);
			ACCTexturesView.contentWidth = GUILayout.MaxWidth(num * 0.69f);
			ACCTexturesView.restWidth = GUILayout.Width(num * 0.14f);
			ACCTexturesView.comboWidth = uiparams.textureRect.width * 0.65f;
			ACCTexturesView.fontSize = uiparams.fontSize;
			ACCTexturesView.fontSizeS = uiparams.fontSizeS;
			ACCTexturesView.labelWidth = 50f;
			ACCTexturesView.optItemHeight = GUILayout.Height((float)uiparams.itemHeight);
			ACCTexturesView.optInputWidth = GUILayout.Width(60f);
			ACCTexturesView.optLabelWidth = GUILayout.Width(ACCTexturesView.labelWidth);
			ACCTexturesView.sliderMargin = (float)uiparams.margin * 4.5f;
		};

		// Token: 0x04000410 RID: 1040
		private static GUIContent[] itemNames;

		// Token: 0x04000411 RID: 1041
		public static FileBrowser fileBrowser;

		// Token: 0x04000412 RID: 1042
		private string textureDir;

		// Token: 0x04000413 RID: 1043
		private int matNo;

		// Token: 0x04000414 RID: 1044
		private List<ACCTexture> original;

		// Token: 0x04000415 RID: 1045
		private List<ACCTexture> edited;

		// Token: 0x04000416 RID: 1046
		public bool expand;

		// Token: 0x04000417 RID: 1047
		private readonly Dictionary<string, ComboBoxLO> combos = new Dictionary<string, ComboBoxLO>(2);

		// Token: 0x04000418 RID: 1048
		private Material material;
	}
}
