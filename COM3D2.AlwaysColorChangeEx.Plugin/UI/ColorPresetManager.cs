using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.UI
{
	// Token: 0x02000070 RID: 112
	public class ColorPresetManager
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00020C77 File Offset: 0x0001EE77
		public static Texture2D PresetBaseIcon
		{
			get
			{
				if (ColorPresetManager.presetBaseIcon == null)
				{
					ColorPresetManager.presetBaseIcon = ResourceHolder.Instance.LoadTex("preset_base");
				}
				return ColorPresetManager.presetBaseIcon;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x00020C9F File Offset: 0x0001EE9F
		public static Texture2D PresetEmptyIcon
		{
			get
			{
				if (ColorPresetManager.presetEmptyIcon == null)
				{
					ColorPresetManager.presetEmptyIcon = ResourceHolder.Instance.LoadTex("preset_empty");
				}
				return ColorPresetManager.presetEmptyIcon;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00020CC7 File Offset: 0x0001EEC7
		public static Texture2D PresetFocusIcon
		{
			get
			{
				if (ColorPresetManager.presetFocusIcon == null)
				{
					ColorPresetManager.presetFocusIcon = ResourceHolder.Instance.LoadTex("preset_focus");
				}
				return ColorPresetManager.presetFocusIcon;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00020CF0 File Offset: 0x0001EEF0
		// (set) Token: 0x060003AA RID: 938 RVA: 0x00020D54 File Offset: 0x0001EF54
		public GUIStyle IconStyle
		{
			get
			{
				GUIStyle result;
				if ((result = this._iconStyle) == null)
				{
					result = (this._iconStyle = new GUIStyle("label")
					{
						contentOffset = new Vector2(0f, 1f),
						margin = new RectOffset(1, 1, 1, 1),
						padding = new RectOffset(1, 1, 1, 1)
					});
				}
				return result;
			}
			set
			{
				this._iconStyle = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00020D60 File Offset: 0x0001EF60
		// (set) Token: 0x060003AC RID: 940 RVA: 0x00020D9F File Offset: 0x0001EF9F
		public GUILayoutOption BtnWidth
		{
			get
			{
				GUILayoutOption result;
				if ((result = this._btnWidth) == null)
				{
					result = (this._btnWidth = GUILayout.Width(this.BtnStyle.CalcSize(new GUIContent("Delete")).x));
				}
				return result;
			}
			set
			{
				this._btnWidth = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00020DA8 File Offset: 0x0001EFA8
		// (set) Token: 0x060003AE RID: 942 RVA: 0x00020DFE File Offset: 0x0001EFFE
		public GUIStyle BtnStyle
		{
			get
			{
				GUIStyle result;
				if ((result = this._btnStyle) == null)
				{
					result = (this._btnStyle = new GUIStyle("button")
					{
						margin = new RectOffset(2, 2, 1, 1),
						padding = new RectOffset(1, 1, 1, 1),
						alignment = TextAnchor.MiddleCenter
					});
				}
				return result;
			}
			set
			{
				this._btnStyle = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003AF RID: 943 RVA: 0x00020E07 File Offset: 0x0001F007
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x00020E0F File Offset: 0x0001F00F
		public string PresetPath { get; private set; }

		// Token: 0x060003B1 RID: 945 RVA: 0x00020E18 File Offset: 0x0001F018
		public void SetPath(string path)
		{
			this.PresetPath = path;
			this.Load();
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00020E28 File Offset: 0x0001F028
		public bool IsValid(int idx)
		{
			return 0 <= idx && idx < this.presetCodes.Count && this.presetCodes[idx].Length > 0;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00020E54 File Offset: 0x0001F054
		public void ClearColor(int idx)
		{
			if (idx < 0 && this.presetCodes.Count <= idx)
			{
				return;
			}
			this.presetCodes[idx] = string.Empty;
			this.presetIcons[idx].SetPixels32(ColorPresetManager.PresetEmptyIcon.GetPixels32(0), 0);
			this.presetIcons[idx].Apply();
			this.Save();
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00020EBA File Offset: 0x0001F0BA
		public void SetColor(int idx, string code, ref Color col)
		{
			this.presetCodes[idx] = code;
			this.SetTexColor(ref col, ColorPresetManager.PresetBaseIcon, this.presetIcons[idx]);
			this.Save();
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00020EE8 File Offset: 0x0001F0E8
		public void SetTexColor(ref Color col, Texture2D srcTex, Texture2D dstTex)
		{
			Color32[] pixels = srcTex.GetPixels32(0);
			for (int i = 0; i < pixels.Length; i++)
			{
				if ((float)pixels[i].a > 0f)
				{
					pixels[i] = col;
				}
			}
			dstTex.SetPixels32(pixels, 0);
			dstTex.Apply();
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00020F44 File Offset: 0x0001F144
		public bool Load()
		{
			if (this.PresetPath == null)
			{
				return false;
			}
			this.presetCodes.Clear();
			this.presetIcons.Clear();
			bool result = false;
			Texture2D texture2D = ColorPresetManager.PresetEmptyIcon;
			if (File.Exists(this.PresetPath))
			{
				try
				{
					string text = File.ReadAllText(this.PresetPath, Encoding.UTF8);
					string[] array = text.Split(new char[]
					{
						','
					});
					foreach (string text2 in array)
					{
						string text3 = text2.Trim();
						Color color = ColorPicker.GetColor(text3);
						Texture2D texture2D3;
						if (color.a > 0f)
						{
							this.presetCodes.Add(text3);
							Texture2D texture2D2 = ColorPresetManager.PresetBaseIcon;
							texture2D3 = new Texture2D(texture2D2.width, texture2D2.height, texture2D2.format, false);
							this.SetTexColor(ref color, texture2D2, texture2D3);
						}
						else
						{
							this.presetCodes.Add(string.Empty);
							texture2D3 = this.CreateEmpty();
						}
						this.presetIcons.Add(texture2D3);
						if (this.presetIcons.Count >= this.Count)
						{
							break;
						}
					}
					result = true;
				}
				catch (Exception ex)
				{
					LogUtil.Error(new object[]
					{
						"カラープリセットのロードに失敗しました。",
						this.PresetPath,
						ex
					});
				}
			}
			for (int j = this.presetIcons.Count; j < this.Count; j++)
			{
				Texture2D texture2D4 = new Texture2D(texture2D.width, texture2D.height, texture2D.format, false);
				texture2D4.SetPixels32(texture2D.GetPixels32(0), 0);
				texture2D4.Apply();
				this.presetIcons.Add(texture2D4);
				this.presetCodes.Add(string.Empty);
			}
			return result;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0002111C File Offset: 0x0001F31C
		private Texture2D CreateEmpty()
		{
			Texture2D texture2D = ColorPresetManager.PresetEmptyIcon;
			Texture2D texture2D2 = new Texture2D(texture2D.width, texture2D.height, texture2D.format, false);
			texture2D2.SetPixels32(texture2D.GetPixels32(0), 0);
			texture2D2.Apply();
			return texture2D2;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00021160 File Offset: 0x0001F360
		public bool Save()
		{
			if (this.PresetPath == null)
			{
				return false;
			}
			try
			{
				using (StreamWriter streamWriter = new StreamWriter(this.PresetPath, false, Encoding.UTF8, 8192))
				{
					foreach (string value in this.presetCodes)
					{
						streamWriter.Write(value);
						streamWriter.Write(',');
					}
				}
			}
			catch (IOException ex)
			{
				LogUtil.Error(new object[]
				{
					"カラープリセットの保存に失敗しました。",
					this.PresetPath,
					ex
				});
				return false;
			}
			LogUtil.Debug(new object[]
			{
				"save to color preset:",
				this.PresetPath
			});
			return true;
		}

		// Token: 0x04000435 RID: 1077
		public static readonly ColorPresetManager Instance = new ColorPresetManager();

		// Token: 0x04000436 RID: 1078
		private static Texture2D presetBaseIcon;

		// Token: 0x04000437 RID: 1079
		private static Texture2D presetEmptyIcon;

		// Token: 0x04000438 RID: 1080
		private static Texture2D presetFocusIcon;

		// Token: 0x04000439 RID: 1081
		public readonly List<Texture2D> presetIcons = new List<Texture2D>();

		// Token: 0x0400043A RID: 1082
		public readonly List<string> presetCodes = new List<string>();

		// Token: 0x0400043B RID: 1083
		private GUIStyle _iconStyle;

		// Token: 0x0400043C RID: 1084
		private GUILayoutOption _btnWidth;

		// Token: 0x0400043D RID: 1085
		private GUIStyle _btnStyle;

		// Token: 0x0400043E RID: 1086
		public int Count = 20;
	}
}
