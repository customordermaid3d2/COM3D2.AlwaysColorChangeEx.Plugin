using System;
using System.Text;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.UI
{
	// Token: 0x0200006F RID: 111
	public class ColorPicker
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0001F8B9 File Offset: 0x0001DAB9
		// (set) Token: 0x0600037A RID: 890 RVA: 0x0001F83C File Offset: 0x0001DA3C
		public Color Color
		{
			get
			{
				return this._color;
			}
			set
			{
				if (this._color != value)
				{
					this._color = value;
					this.Light = Math.Max(this._color.r, Math.Max(this._color.g, this._color.b));
					this.SearchPos(this.MapTex, ref this._color, out this._pos);
					this.SetTexColor(ref this._color);
					this.ToColorCode();
				}
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0001F8EA File Offset: 0x0001DAEA
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0001F8C1 File Offset: 0x0001DAC1
		public float Light
		{
			get
			{
				return this._light;
			}
			set
			{
				if (!ColorPicker.Equals(this._light, value))
				{
					this.Transfer(ColorPicker.MapBaseTex, this.MapTex, value);
					this._light = value;
				}
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001F8F4 File Offset: 0x0001DAF4
		private void SearchPos(Texture2D tex, ref Color col, out Vector2 destPos)
		{
			float num = 3f;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < tex.width; i++)
			{
				for (int j = 0; j < tex.height; j++)
				{
					float num4 = ColorPicker.DiffColor(tex.GetPixel(i, j), col);
					if (num4 < 0.001f)
					{
						destPos.x = (float)i;
						destPos.y = (float)(tex.height - 1 - j);
						return;
					}
					if (num4 < num)
					{
						num = num4;
						num2 = i;
						num3 = j;
					}
				}
			}
			destPos.x = (float)num2;
			destPos.y = (float)(tex.height - 1 - num3);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001F994 File Offset: 0x0001DB94
		private void Transfer(Texture2D srcTex, Texture2D dstTex, float ratio)
		{
			Color32[] pixels = srcTex.GetPixels32(0);
			Color32[] pixels2 = dstTex.GetPixels32(0);
			int num = dstTex.width * dstTex.height;
			for (int i = 0; i < num; i++)
			{
				pixels2[i].r = (byte)((float)pixels[i].r * ratio);
				pixels2[i].g = (byte)((float)pixels[i].g * ratio);
				pixels2[i].b = (byte)((float)pixels[i].b * ratio);
				pixels2[i].a = pixels[i].a;
			}
			dstTex.SetPixels32(pixels2, 0);
			dstTex.Apply();
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0001FA4E File Offset: 0x0001DC4E
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0001FA45 File Offset: 0x0001DC45
		public Texture2D ColorTex
		{
			get
			{
				if (this._colorTex == null)
				{
					this._colorTex = new Texture2D(16, 16, TextureFormat.RGB24, false);
				}
				return this._colorTex;
			}
			set
			{
				this._colorTex = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0001FA78 File Offset: 0x0001DC78
		public Texture2D MapTex
		{
			get
			{
				if (this._mapTex == null)
				{
					Texture2D texture2D = ColorPicker.MapBaseTex;
					this._mapTex = new Texture2D(texture2D.width, texture2D.height, texture2D.format, false);
					this.Transfer(ColorPicker.MapBaseTex, this._mapTex, this._light);
				}
				return this._mapTex;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0001FAD4 File Offset: 0x0001DCD4
		public GUILayoutOption IconWidth
		{
			get
			{
				GUILayoutOption result;
				if ((result = this._iconWidth) == null)
				{
					result = (this._iconWidth = GUILayout.Width((float)this.ColorTex.width));
				}
				return result;
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001FB05 File Offset: 0x0001DD05
		private Color GetMapColor(int x, int y)
		{
			return this.MapTex.GetPixel(x, this.MapTex.height - y);
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0001FB2C File Offset: 0x0001DD2C
		// (set) Token: 0x06000385 RID: 901 RVA: 0x0001FB20 File Offset: 0x0001DD20
		public GUIStyle TexStyle
		{
			get
			{
				GUIStyle result;
				if ((result = this._texStyle) == null)
				{
					result = (this._texStyle = new GUIStyle
					{
						normal = 
						{
							background = this.MapTex
						}
					});
				}
				return result;
			}
			set
			{
				this._texStyle = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0001FB70 File Offset: 0x0001DD70
		// (set) Token: 0x06000387 RID: 903 RVA: 0x0001FB64 File Offset: 0x0001DD64
		public GUIStyle TexLightStyle
		{
			get
			{
				GUIStyle result;
				if ((result = this._texLightStyle) == null)
				{
					result = (this._texLightStyle = new GUIStyle
					{
						normal = 
						{
							background = ColorPicker.LightTex
						}
					});
				}
				return result;
			}
			set
			{
				this._texLightStyle = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0001FBA7 File Offset: 0x0001DDA7
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0001FBAF File Offset: 0x0001DDAF
		public string ColorCode { get; private set; }

		// Token: 0x0600038B RID: 907 RVA: 0x0001FBB8 File Offset: 0x0001DDB8
		public bool SetColorCode(string code)
		{
			if (!ColorPicker.IsColorCode(code))
			{
				return false;
			}
			int num = Uri.FromHex(code[1]) * 16 + Uri.FromHex(code[2]);
			int num2 = Uri.FromHex(code[3]) * 16 + Uri.FromHex(code[4]);
			int num3 = Uri.FromHex(code[5]) * 16 + Uri.FromHex(code[6]);
			this.Color = new Color((float)num / 255f, (float)num2 / 255f, (float)num3 / 255f, this._color.a);
			return true;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0001FC84 File Offset: 0x0001DE84
		// (set) Token: 0x0600038C RID: 908 RVA: 0x0001FC55 File Offset: 0x0001DE55
		public static Texture2D MapBaseTex
		{
			get
			{
				if (ColorPicker.mapBaseTex == null)
				{
					ColorPicker.mapBaseTex = ColorPicker.CreateRGBMapTex(ColorPicker.size + 1, ColorPicker.size + 1);
				}
				return ColorPicker.mapBaseTex;
			}
			set
			{
				if (value != null)
				{
					ColorPicker.mapBaseTex = value;
					ColorPicker.size = Math.Min(ColorPicker.mapBaseTex.width, ColorPicker.mapBaseTex.height);
				}
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0001FCB0 File Offset: 0x0001DEB0
		public static Texture2D LightTex
		{
			get
			{
				if (ColorPicker.lightTex == null)
				{
					ColorPicker.lightTex = ColorPicker.CreateLightTex(16, ColorPicker.size, 1);
				}
				return ColorPicker.lightTex;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0001FCD6 File Offset: 0x0001DED6
		public static Texture2D CircleTex
		{
			get
			{
				if (ColorPicker.circleTex == null)
				{
					ColorPicker.circleTex = ResourceHolder.Instance.LoadTex("circle");
				}
				return ColorPicker.circleTex;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0001FCFE File Offset: 0x0001DEFE
		public static Texture2D CrossTex
		{
			get
			{
				if (ColorPicker.crossTex == null)
				{
					ColorPicker.crossTex = ResourceHolder.Instance.LoadTex("cross");
				}
				return ColorPicker.crossTex;
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0001FD28 File Offset: 0x0001DF28
		public static bool HasColorCodeClip()
		{
			string code = ClipBoardHandler.Instance.GetClipboard();
			return ColorPicker.IsColorCode(code);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0001FD4C File Offset: 0x0001DF4C
		public static bool IsColorCode(string code)
		{
			if (code.Length == 7 && code[0] == '#')
			{
				for (int i = 1; i < 7; i++)
				{
					if (!Uri.IsHexDigit(code[i]))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0001FD8C File Offset: 0x0001DF8C
		public static Color GetColor(string code)
		{
			if (!ColorPicker.IsColorCode(code))
			{
				return ColorPicker.Empty;
			}
			int num = Uri.FromHex(code[1]) * 16 + Uri.FromHex(code[2]);
			int num2 = Uri.FromHex(code[3]) * 16 + Uri.FromHex(code[4]);
			int num3 = Uri.FromHex(code[5]) * 16 + Uri.FromHex(code[6]);
			return new Color((float)num / 255f, (float)num2 / 255f, (float)num3 / 255f);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0001FE1B File Offset: 0x0001E01B
		public ColorPicker(ColorPresetManager presetMgr = null)
		{
			this.ColorCode = string.Empty;
			this._presetMgr = presetMgr;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0001FE48 File Offset: 0x0001E048
		private void ToColorCode()
		{
			int num = (int)(this._color.r * 255f);
			int num2 = (int)(this._color.g * 255f);
			int num3 = (int)(this._color.b * 255f);
			this._colorCode.Length = 0;
			this._colorCode.Append('#').Append(num.ToString("X2")).Append(num2.ToString("X2")).Append(num3.ToString("X2"));
			this.ColorCode = this._colorCode.ToString();
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001FEEC File Offset: 0x0001E0EC
		public void SetTexColor(ref Color col)
		{
			this.SetTexColor(ref col, this.texEdgeSize);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001FEFC File Offset: 0x0001E0FC
		public void SetTexColor(ref Color col, int frame)
		{
			Texture2D texture2D = (this._colorTex == null) ? this.ColorTex : this._colorTex;
			int blockWidth = texture2D.width - frame * 2;
			int blockHeight = texture2D.height - frame * 2;
			Color[] pixels = texture2D.GetPixels(frame, frame, blockWidth, blockHeight, 0);
			for (int i = 0; i < pixels.Length; i++)
			{
				pixels[i] = col;
			}
			texture2D.SetPixels(frame, frame, blockWidth, blockHeight, pixels);
			texture2D.Apply();
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001FF80 File Offset: 0x0001E180
		public bool DrawLayout()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			bool result;
			try
			{
				if (GUILayout.Button(ColorPicker.CrossTex, ColorPicker.labelStyle, new GUILayoutOption[]
				{
					ColorPicker.labelWidth
				}))
				{
					this.expand = false;
				}
				GUILayout.Label(string.Empty, this.TexStyle, new GUILayoutOption[]
				{
					GUILayout.Width((float)this._mapTex.width),
					GUILayout.Height((float)this._mapTex.height)
				});
				Rect lastRect = GUILayoutUtility.GetLastRect();
				Texture2D texture2D = ColorPicker.CircleTex;
				float num = (float)texture2D.width * 0.5f;
				GUI.DrawTexture(new Rect(lastRect.x + this._pos.x - num, lastRect.y + this._pos.y - num, (float)texture2D.width, (float)texture2D.height), texture2D, ScaleMode.StretchToFill, true, 0f);
				bool flag = this.MapPickerEvent(ref lastRect);
				GUILayout.Space(20f);
				GUILayout.Label(string.Empty, this.TexLightStyle, new GUILayoutOption[]
				{
					GUILayout.Width((float)ColorPicker.lightTex.width),
					GUILayout.Height((float)ColorPicker.lightTex.height)
				});
				lastRect = GUILayoutUtility.GetLastRect();
				GUI.DrawTexture(new Rect(lastRect.x + 1f, lastRect.y + (float)(ColorPicker.size - 1) * (1f - this._light) - num + 1f, (float)texture2D.width, (float)texture2D.height), texture2D, ScaleMode.StretchToFill, true, 0f);
				flag |= this.LightSliderEvent(ref lastRect);
				if (this._presetMgr != null)
				{
					GUILayout.Space(5f);
					flag |= this.DrawPresetLayout();
				}
				result = flag;
			}
			finally
			{
				GUILayout.EndHorizontal();
			}
			return result;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00020168 File Offset: 0x0001E368
		public bool DrawPresetLayout()
		{
			bool result = false;
			Event current = Event.current;
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			try
			{
				for (int i = 0; i < this._presetMgr.Count; i += 10)
				{
					GUILayout.BeginVertical(new GUILayoutOption[0]);
					try
					{
						int num = (i + 10 < this._presetMgr.Count) ? (i + 10) : this._presetMgr.Count;
						for (int j = i; j < num; j++)
						{
							Texture2D image = this._presetMgr.presetIcons[j];
							GUILayout.Label(image, this._presetMgr.IconStyle, new GUILayoutOption[0]);
							if (this._selectedPreset == j)
							{
								Rect lastRect = GUILayoutUtility.GetLastRect();
								Texture2D presetFocusIcon = ColorPresetManager.PresetFocusIcon;
								GUI.DrawTexture(new Rect(lastRect.x + 1f, lastRect.y + 2f, (float)presetFocusIcon.width, (float)presetFocusIcon.height), presetFocusIcon, ScaleMode.StretchToFill, true, 0f);
							}
							if (current.type == EventType.MouseDown && (current.button == 0 || current.button == 1))
							{
								Vector2 mousePosition = current.mousePosition;
								if (GUILayoutUtility.GetLastRect().Contains(mousePosition))
								{
									switch (current.button)
									{
									case 0:
										if (this._selectedPreset == j)
										{
											string colorCode = this._presetMgr.presetCodes[j];
											this.SetColorCode(colorCode);
											result = true;
										}
										else
										{
											this._selectedPreset = j;
										}
										break;
									case 1:
										this._presetMgr.ClearColor(this._selectedPreset);
										this._selectedPreset = j;
										break;
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
			}
			finally
			{
				GUILayout.EndHorizontal();
			}
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			try
			{
				GUI.enabled = (this._selectedPreset != -1);
				try
				{
					if (GUILayout.Button("Save", this._presetMgr.BtnStyle, new GUILayoutOption[]
					{
						this._presetMgr.BtnWidth
					}))
					{
						this._presetMgr.SetColor(this._selectedPreset, this.ColorCode, ref this._color);
					}
					if (GUILayout.Button("Delete", this._presetMgr.BtnStyle, new GUILayoutOption[]
					{
						this._presetMgr.BtnWidth
					}))
					{
						this._presetMgr.ClearColor(this._selectedPreset);
					}
				}
				finally
				{
					GUI.enabled = true;
				}
			}
			finally
			{
				GUILayout.EndHorizontal();
			}
			GUILayout.EndVertical();
			return result;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0002044C File Offset: 0x0001E64C
		public bool DrawPicker(ref Rect rect)
		{
			GUI.Label(rect, string.Empty, this.TexStyle);
			Texture2D texture2D = ColorPicker.CircleTex;
			float num = (float)texture2D.width * 0.5f;
			GUI.DrawTexture(new Rect(rect.x + this._pos.x - num, rect.y + this._pos.y - num, (float)texture2D.width, (float)texture2D.height), texture2D, ScaleMode.StretchToFill, true, 0f);
			return this.MapPickerEvent(ref rect);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x000204D4 File Offset: 0x0001E6D4
		private bool MapPickerEvent(ref Rect rect)
		{
			if (this._lightDragging)
			{
				return false;
			}
			Event current = Event.current;
			if (current.button == 0 && (current.type == EventType.MouseDown || current.type == EventType.MouseDrag))
			{
				Vector2 mousePosition = current.mousePosition;
				if (this._mapDragging || rect.Contains(mousePosition))
				{
					int num = (int)(mousePosition.x - rect.x);
					int num2 = (int)(mousePosition.y - rect.y);
					Color color;
					if (this._mapDragging)
					{
						int num3 = this._mapTex.width / 2;
						int num4 = Mathf.CeilToInt((float)this._mapTex.height / 2f);
						int num5 = Math.Min(num3, num4);
						float num6 = ColorPicker.Distance(num, num2, num3, num4);
						if (num6 <= (float)num5)
						{
							color = this.GetMapColor(num, num2);
						}
						else
						{
							color = ColorPicker.GetEdgeColor((float)(num - num3), (float)(-(float)(num2 - num4)), num6) * this._light;
							color.a = 1f;
							float num7 = (float)num5 / num6;
							num = (int)((float)(num - num3) * num7) + num3;
							num2 = (int)((float)(num2 - num4) * num7) + num4;
						}
					}
					else
					{
						if (current.type != EventType.MouseDown)
						{
							return false;
						}
						color = this.GetMapColor(num, num2);
						if (ColorPicker.Equals(color.a, 0f))
						{
							return false;
						}
						this._mapDragging = true;
					}
					this._color = color;
					this.SetTexColor(ref this._color, this.texEdgeSize);
					this.ToColorCode();
					this._pos.x = (float)num;
					this._pos.y = (float)num2;
					current.Use();
					return true;
				}
			}
			else if (this._mapDragging && current.type == EventType.MouseUp)
			{
				this._mapDragging = false;
			}
			return false;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00020688 File Offset: 0x0001E888
		public void DrawLightScale(ref Rect rect)
		{
			GUI.Label(rect, string.Empty, this.TexLightStyle);
			Texture2D texture2D = ColorPicker.CircleTex;
			float num = (float)texture2D.width * 0.5f;
			Rect position = new Rect(rect.x + 1f, rect.y + (float)(ColorPicker.size - 1) * (1f - this._light) - num + 1f, (float)texture2D.width, (float)texture2D.height);
			GUI.DrawTexture(position, texture2D, ScaleMode.StretchToFill, true, 0f);
			this.LightSliderEvent(ref rect);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0002071C File Offset: 0x0001E91C
		private bool LightSliderEvent(ref Rect rect)
		{
			if (this._mapDragging)
			{
				return false;
			}
			Event current = Event.current;
			if (current.button == 0 && (current.type == EventType.MouseDown || current.type == EventType.MouseDrag))
			{
				Vector2 mousePosition = current.mousePosition;
				if (current.type == EventType.MouseDown && rect.Contains(mousePosition))
				{
					this._lightDragging = true;
				}
				if (this._lightDragging)
				{
					float num = 1f - (mousePosition.y - rect.y - 1f) / (float)ColorPicker.size;
					if (1f < num)
					{
						num = 1f;
					}
					else if (num < 0f)
					{
						num = 0f;
					}
					if (!ColorPicker.Equals(this._light, num))
					{
						this.Light = num;
						this._color = this.GetMapColor((int)this._pos.x, (int)this._pos.y);
						this.SetTexColor(ref this._color, this.texEdgeSize);
						this.ToColorCode();
						current.Use();
						return true;
					}
					current.Use();
				}
			}
			else if (this._lightDragging && current.type == EventType.MouseUp)
			{
				this._lightDragging = false;
			}
			return false;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0002083C File Offset: 0x0001EA3C
		private static Texture2D CreateLightTex(int width, int height, int frameWidth)
		{
			Texture2D texture2D = new Texture2D(width + frameWidth * 2, height + frameWidth * 2, TextureFormat.RGB24, false);
			int num = height - 1;
			Color gray = Color.gray;
			for (int i = frameWidth; i < height + frameWidth; i++)
			{
				float num2 = 1f - (float)i / (float)num;
				Color color = new Color(num2, num2, num2);
				for (int j = frameWidth; j < width + frameWidth; j++)
				{
					texture2D.SetPixel(j, height - 1 - i, color);
				}
				for (int k = 0; k < frameWidth; k++)
				{
					texture2D.SetPixel(k, height - 1 - i, gray);
				}
				for (int l = width + frameWidth; l < frameWidth + frameWidth * 2; l++)
				{
					texture2D.SetPixel(l, height - 1 - i, gray);
				}
			}
			for (int m = 0; m < width + frameWidth * 2; m++)
			{
				for (int n = 0; n < frameWidth; n++)
				{
					texture2D.SetPixel(m, n, gray);
				}
				for (int num3 = height + frameWidth; num3 < height + frameWidth * 2; num3++)
				{
					texture2D.SetPixel(m, num3, gray);
				}
			}
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00020950 File Offset: 0x0001EB50
		private static Texture2D CreateRGBMapTex(int width, int height)
		{
			Texture2D texture2D = new Texture2D(width, height, TextureFormat.ARGB32, false);
			int num = width / 2;
			int num2 = height / 2;
			int num3 = Math.Min(num, num2);
			Color white = Color.white;
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					float num4 = ColorPicker.Distance(j, i, num, num2);
					float num5 = num4 / (float)num3;
					if (1f < num5)
					{
						texture2D.SetPixel(j, i, ColorPicker.Empty);
					}
					else if (ColorPicker.Equals(num5, 0f))
					{
						texture2D.SetPixel(j, i, white);
					}
					else
					{
						int num6 = j - num;
						int num7 = i - num2;
						Color edgeColor = ColorPicker.GetEdgeColor((float)num6, (float)num7, num4);
						Color color = ColorPicker.GetColor(ref white, ref edgeColor, num5);
						texture2D.SetPixel(j, i, color);
					}
				}
			}
			return texture2D;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00020A2C File Offset: 0x0001EC2C
		private static Color GetEdgeColor(float vecX, float vecY, float dist)
		{
			float num = (float)Math.Acos((double)(vecX / dist));
			if (vecY > 0f)
			{
				num = -num;
			}
			num *= 0.95492965f;
			if (-0.5f <= num && num < 0.5f)
			{
				float r = 0.5f - num;
				return new Color(r, 0f, 1f);
			}
			if (0.5f <= num && num < 1.5f)
			{
				float g = num - 0.5f;
				return new Color(0f, g, 1f);
			}
			if (1.5f <= num && num < 2.5f)
			{
				float b = 2.5f - num;
				return new Color(0f, 1f, b);
			}
			if (2.5f <= num && num <= 3f)
			{
				float r2 = num - 2.5f;
				return new Color(r2, 1f, 0f);
			}
			if (-3f <= num && num < -2.5f)
			{
				float r3 = num + 3.5f;
				return new Color(r3, 1f, 0f);
			}
			if (-2.5f <= num && num < -1.5f)
			{
				float g2 = -1.5f - num;
				return new Color(1f, g2, 0f);
			}
			float b2 = num + 1.5f;
			return new Color(1f, 0f, b2);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00020B70 File Offset: 0x0001ED70
		private static float Distance(int x1, int y1, int x2, int y2)
		{
			int num = x1 - x2;
			int num2 = y1 - y2;
			return (float)Math.Sqrt((double)(num * num + num2 * num2));
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00020B93 File Offset: 0x0001ED93
		private static bool Equals(float f1, float f2)
		{
			return Math.Abs(f1 - f2) < 0.001f;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00020BA4 File Offset: 0x0001EDA4
		private static Color GetColor(ref Color c1, ref Color c2, float ratio)
		{
			float r = c1.r + ratio * (c2.r - c1.r);
			float g = c1.g + ratio * (c2.g - c1.g);
			float b = c1.b + ratio * (c2.b - c1.b);
			return new Color(r, g, b);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00020BFE File Offset: 0x0001EDFE
		private static float DiffColor(Color c1, Color c2)
		{
			return Math.Abs(c1.r - c2.r) + Math.Abs(c1.g - c2.g) + Math.Abs(c1.b - c2.b);
		}

		// Token: 0x0400041B RID: 1051
		private const int FRAME_WIDTH = 1;

		// Token: 0x0400041C RID: 1052
		private const float RANGE_UNIT = 0.95492965f;

		// Token: 0x0400041D RID: 1053
		private readonly ColorPresetManager _presetMgr;

		// Token: 0x0400041E RID: 1054
		public bool expand;

		// Token: 0x0400041F RID: 1055
		public int texEdgeSize;

		// Token: 0x04000420 RID: 1056
		private int _selectedPreset = -1;

		// Token: 0x04000421 RID: 1057
		private Vector2 _pos;

		// Token: 0x04000422 RID: 1058
		private bool _mapDragging;

		// Token: 0x04000423 RID: 1059
		private bool _lightDragging;

		// Token: 0x04000424 RID: 1060
		private Color _color;

		// Token: 0x04000425 RID: 1061
		private float _light;

		// Token: 0x04000426 RID: 1062
		private Texture2D _colorTex;

		// Token: 0x04000427 RID: 1063
		private Texture2D _mapTex;

		// Token: 0x04000428 RID: 1064
		private GUILayoutOption _iconWidth;

		// Token: 0x04000429 RID: 1065
		private GUIStyle _texStyle;

		// Token: 0x0400042A RID: 1066
		private GUIStyle _texLightStyle;

		// Token: 0x0400042B RID: 1067
		private readonly StringBuilder _colorCode = new StringBuilder(7);

		// Token: 0x0400042C RID: 1068
		private static readonly GUIStyle labelStyle = new GUIStyle("label");

		// Token: 0x0400042D RID: 1069
		private static readonly GUILayoutOption labelWidth = GUILayout.Width(16f);

		// Token: 0x0400042E RID: 1070
		private static readonly Color Empty = Color.clear;

		// Token: 0x0400042F RID: 1071
		public static int size = 256;

		// Token: 0x04000430 RID: 1072
		private static Texture2D mapBaseTex;

		// Token: 0x04000431 RID: 1073
		private static Texture2D lightTex;

		// Token: 0x04000432 RID: 1074
		private static Texture2D circleTex;

		// Token: 0x04000433 RID: 1075
		private static Texture2D crossTex;
	}
}
