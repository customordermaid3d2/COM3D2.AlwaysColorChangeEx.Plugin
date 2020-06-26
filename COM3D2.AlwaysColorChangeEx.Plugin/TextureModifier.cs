using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CM3D2.AlwaysColorChangeEx.Plugin.UI;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin
{
	// Token: 0x0200000E RID: 14
	public class TextureModifier
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00008E31 File Offset: 0x00007031
		public static TextureModifier Instance
		{
			get
			{
				return TextureModifier.INSTANCE;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00008E38 File Offset: 0x00007038
		public void Clear()
		{
			this._originalTexCache.Clear();
			this._filterParams.Clear();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00008E50 File Offset: 0x00007050
		public bool IsValidTarget(Maid maid, string slotName, Material material, string propName)
		{
			return this.GetKey(maid, slotName, material, propName) != null;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00008E64 File Offset: 0x00007064
		public void ProcGUI(Maid maid, string slotName, Material material, string propName)
		{
			Texture2D texture2D = material.GetTexture(propName) as Texture2D;
			if (texture2D == null || string.IsNullOrEmpty(texture2D.name))
			{
				return;
			}
			StringBuilder stringBuilder = this.CreateKey(new string[]
			{
				MaidHelper.GetGuid(maid),
				slotName,
				material.name,
				texture2D.name
			});
			TextureModifier.FilterParam orAdd = this._filterParams.GetOrAdd(stringBuilder.ToString());
			orAdd.ProcGUI(texture2D);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00008EDD File Offset: 0x000070DD
		public void Update(Maid maid, Dictionary<string, List<Material>> slotMaterials, List<Texture2D> textures, EditTarget texEdit)
		{
			this._originalTexCache.Refresh(textures.ToArray());
			this.FilterTexture(slotMaterials, textures, maid, texEdit);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00008F04 File Offset: 0x00007104
		private StringBuilder CreateKey(params string[] names)
		{
			int num = names.Sum((string name) => name.Length);
			num += names.Length;
			StringBuilder stringBuilder = new StringBuilder(num);
			for (int i = 0; i < names.Length; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append('/');
				}
				stringBuilder.Append(names[i]);
			}
			return stringBuilder;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00008F68 File Offset: 0x00007168
		public bool UpdateTex(Maid maid, Material[] slotMaterials, EditTarget texEdit)
		{
			if (slotMaterials.Length <= texEdit.matNo)
			{
				return false;
			}
			Material mat = slotMaterials[texEdit.matNo];
			return this.UpdateTex(maid, mat, texEdit);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00008F94 File Offset: 0x00007194
		public bool UpdateTex(Maid maid, Material mat, EditTarget texEdit)
		{
			Texture2D texture2D = mat.GetTexture(texEdit.propName) as Texture2D;
			if (texture2D == null || string.IsNullOrEmpty(texture2D.name))
			{
				return false;
			}
			StringBuilder stringBuilder = this.CreateKey(new string[]
			{
				MaidHelper.GetGuid(maid),
				texEdit.slotName,
				mat.name,
				texture2D.name
			});
			TextureModifier.FilterParam orAdd = this._filterParams.GetOrAdd(stringBuilder.ToString());
			if (!orAdd.IsDirty)
			{
				return false;
			}
			this.FilterTexture(texture2D, orAdd);
			return true;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00009023 File Offset: 0x00007223
		public bool RemoveCache(Texture2D tex2D)
		{
			return this._originalTexCache.Remove(tex2D.name);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00009038 File Offset: 0x00007238
		public bool RemoveFilter(Maid maid, string slotName, Material mat, string propName)
		{
			Texture2D texture2D = mat.GetTexture(propName) as Texture2D;
			return !(texture2D == null) && !string.IsNullOrEmpty(texture2D.name) && this.RemoveFilter(maid, slotName, mat, texture2D);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00009078 File Offset: 0x00007278
		public bool RemoveFilter(Maid maid, string slotName, Material mat, Texture2D tex2D)
		{
			StringBuilder stringBuilder = this.CreateKey(new string[]
			{
				MaidHelper.GetGuid(maid),
				slotName,
				mat.name,
				tex2D.name
			});
			return this._filterParams.Remove(stringBuilder.ToString());
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000090C8 File Offset: 0x000072C8
		public bool IsChanged(Maid maid, string slotName, Material mat, string propName)
		{
			Texture2D texture2D = mat.GetTexture(propName) as Texture2D;
			return !(texture2D == null) && !string.IsNullOrEmpty(texture2D.name) && this.IsChanged(maid, slotName, mat.name, texture2D.name);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00009110 File Offset: 0x00007310
		public bool IsChanged(Maid maid, string slotName, string matName, string texName)
		{
			StringBuilder stringBuilder = this.CreateKey(new string[]
			{
				MaidHelper.GetGuid(maid),
				slotName,
				matName,
				texName
			});
			TextureModifier.FilterParam filterParam = this._filterParams.Get(stringBuilder.ToString());
			return filterParam != null && !filterParam.HasNotChanged();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00009164 File Offset: 0x00007364
		public TextureModifier.FilterParam GetFilter(Maid maid, string slotName, Material mat, int propId)
		{
			Texture2D texture2D = mat.GetTexture(propId) as Texture2D;
			if (texture2D == null || string.IsNullOrEmpty(texture2D.name))
			{
				return null;
			}
			return this.GetFilter(maid, slotName, mat.name, texture2D.name);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000091AC File Offset: 0x000073AC
		public TextureModifier.FilterParam GetFilter(Maid maid, string slotName, Material mat, string propName)
		{
			Texture2D texture2D = mat.GetTexture(propName) as Texture2D;
			if (texture2D == null || string.IsNullOrEmpty(texture2D.name))
			{
				return null;
			}
			return this.GetFilter(maid, slotName, mat.name, texture2D.name);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000091F4 File Offset: 0x000073F4
		public TextureModifier.FilterParam GetFilter(Maid maid, string slotName, string matName, string texName)
		{
			StringBuilder stringBuilder = this.CreateKey(new string[]
			{
				MaidHelper.GetGuid(maid),
				slotName,
				matName,
				texName
			});
			return this._filterParams.Get(stringBuilder.ToString());
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00009238 File Offset: 0x00007438
		public bool DuplicateFilter(Maid maid, string slotName, Material mat, string fromPropName, string toPropName)
		{
			Texture2D texture2D = mat.GetTexture(fromPropName) as Texture2D;
			if (texture2D == null || string.IsNullOrEmpty(texture2D.name))
			{
				return false;
			}
			TextureModifier.FilterParam filter = this.GetFilter(maid, slotName, mat.name, texture2D.name);
			Texture2D texture2D2 = mat.GetTexture(toPropName) as Texture2D;
			if (texture2D2 == null || string.IsNullOrEmpty(texture2D2.name))
			{
				return false;
			}
			StringBuilder stringBuilder = this.CreateKey(new string[]
			{
				MaidHelper.GetGuid(maid),
				slotName,
				mat.name,
				texture2D2.name
			});
			TextureModifier.FilterParam filter2 = new TextureModifier.FilterParam(filter);
			this._filterParams.Add(stringBuilder.ToString(), filter2);
			this.FilterTexture(texture2D2, filter2);
			return true;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00009300 File Offset: 0x00007500
		public bool ApplyFilter(Maid maid, string slotName, Material mat, string propName, TextureModifier.FilterParam filter)
		{
			Texture2D texture2D = mat.GetTexture(propName) as Texture2D;
			if (texture2D == null || string.IsNullOrEmpty(texture2D.name))
			{
				return false;
			}
			StringBuilder stringBuilder = this.CreateKey(new string[]
			{
				MaidHelper.GetGuid(maid),
				slotName,
				mat.name,
				texture2D.name
			});
			TextureModifier.FilterParam filter2 = new TextureModifier.FilterParam(filter);
			this._filterParams.Add(stringBuilder.ToString(), filter2);
			this.FilterTexture(texture2D, filter2);
			return true;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00009384 File Offset: 0x00007584
		private string GetKey(Maid maid, string slotName, Material material, string propName)
		{
			if (maid == null || material == null || string.IsNullOrEmpty(propName))
			{
				return null;
			}
			Texture2D texture2D = material.GetTexture(propName) as Texture2D;
			if (texture2D == null || string.IsNullOrEmpty(texture2D.name))
			{
				return null;
			}
			return this.CreateKey(new string[]
			{
				MaidHelper.GetGuid(maid),
				slotName,
				material.name,
				texture2D.name
			}).ToString();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00009408 File Offset: 0x00007608
		public static Texture2D convert(RenderTexture rtex)
		{
			Texture2D texture2D = new Texture2D(rtex.width, rtex.height, TextureFormat.ARGB32, false, false);
			RenderTexture active = RenderTexture.active;
			try
			{
				RenderTexture.active = rtex;
				texture2D.ReadPixels(new Rect(0f, 0f, (float)rtex.width, (float)rtex.height), 0, 0);
				texture2D.Apply();
			}
			finally
			{
				RenderTexture.active = active;
			}
			return texture2D;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000947C File Offset: 0x0000767C
		private TextureModifier.FilterParam GetFilterParam(Maid maid, string slotName, Material material, string propName)
		{
			string key = this.GetKey(maid, slotName, material, propName);
			return this._filterParams.GetOrAdd(key);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000094A4 File Offset: 0x000076A4
		private void FilterTexture(IDictionary<string, List<Material>> slotMaterials, List<Texture2D> textures, Maid maid, EditTarget texEdit)
		{
			List<Material> slotMaterials2;
			if (slotMaterials.TryGetValue(texEdit.slotName, out slotMaterials2))
			{
				this.FilterTexture(slotMaterials2, textures, maid, texEdit);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000094D0 File Offset: 0x000076D0
		private void FilterTexture(ICollection<Material> slotMaterials, List<Texture2D> textures, Maid maid, EditTarget texEdit)
		{
			Material material = null;
			Texture2D texture2D = null;
			if (slotMaterials != null)
			{
				material = slotMaterials.ElementAtOrDefault(texEdit.matNo);
				if (material != null)
				{
					texture2D = (material.GetTexture(texEdit.propName) as Texture2D);
				}
			}
			if (material == null || texture2D == null)
			{
				return;
			}
			TextureModifier.FilterParam filterParam = this.GetFilterParam(maid, texEdit.slotName, material, texEdit.propName);
			if (!filterParam.Dirty.Value)
			{
				return;
			}
			this.FilterTexture(texture2D, filterParam);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00009550 File Offset: 0x00007750
		private void FilterTexture(Texture2D texture, TextureModifier.FilterParam filter)
		{
			TextureModifier.TextureHolder orAdd = this._originalTexCache.GetOrAdd(texture);
			orAdd.dirty = false;
			filter.ClearDirtyFlag();
			this.FilterTexture(texture, orAdd.texture, filter);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00009588 File Offset: 0x00007788
		public Texture2D ApplyFilter(Texture2D srcTex, TextureModifier.FilterParam filter)
		{
			Texture2D texture2D = UnityEngine.Object.Instantiate<Texture2D>(srcTex);
			this.FilterTexture(texture2D, srcTex, filter);
			return texture2D;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00009734 File Offset: 0x00007934
		public void FilterTexture(Texture2D dstTex, Texture2D srcTex, TextureModifier.FilterParam filterParam)
		{
			float outputBase = filterParam.OutputMin * 0.01f;
			float outputScale = (filterParam.OutputMax - filterParam.OutputMin) * 0.01f;
			float num = filterParam.InputMax - filterParam.InputMin;
			if (num < 0.001f)
			{
				num = 0.01f;
			}
			float num2 = filterParam.InputMid;
			if (num2 < 0.001f)
			{
				num2 = 0.01f;
			}
			float inputExp = Mathf.Log(num2 * 0.01f) / Mathf.Log(0.5f);
			float inputBase = -filterParam.InputMin / num;
			float inputScale = 1f / (num * 0.01f);
			float hue = filterParam.Hue / 360f;
			float saturation = filterParam.Saturation / 100f;
			float lightness = filterParam.Lightness / 100f;
			this.Filter(dstTex, srcTex, delegate(Color32 color)
			{
				Color c = color;
				c.r = Mathf.Clamp01(c.r * inputScale + inputBase);
				c.g = Mathf.Clamp01(c.g * inputScale + inputBase);
				c.b = Mathf.Clamp01(c.b * inputScale + inputBase);
				if (!ColorUtil.Equals(inputExp, 1f))
				{
					c.r = Mathf.Pow(c.r, inputExp);
					c.g = Mathf.Pow(c.g, inputExp);
					c.b = Mathf.Pow(c.b, inputExp);
				}
				Vector4 vector = ColorUtil.RGB2HSL(ref c);
				vector.x = (vector.x + hue) % 1f;
				vector.y *= saturation;
				vector.z *= lightness;
				c = ColorUtil.HSL2RGB(ref vector);
				c.r = c.r * outputScale + outputBase;
				c.g = c.g * outputScale + outputBase;
				c.b = c.b * outputScale + outputBase;
				return c;
			});
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00009868 File Offset: 0x00007A68
		private void Filter(Texture2D dstTexture, Texture2D srcTexture, Func<Color32, Color32> mapFunc)
		{
			if (dstTexture == null || srcTexture == null || dstTexture.width != srcTexture.width || dstTexture.height != srcTexture.height)
			{
				return;
			}
			int num = dstTexture.width * dstTexture.height;
			Color32[] pixels = srcTexture.GetPixels32(0);
			Color32[] pixels2 = dstTexture.GetPixels32(0);
			for (int i = 0; i < num; i++)
			{
				pixels2[i] = mapFunc(pixels[i]);
			}
			dstTexture.SetPixels32(pixels2);
			dstTexture.Apply();
		}

		// Token: 0x040000D6 RID: 214
		private static readonly TextureModifier INSTANCE = new TextureModifier();

		// Token: 0x040000D7 RID: 215
		private readonly TextureModifier.FilterParams _filterParams = new TextureModifier.FilterParams();

		// Token: 0x040000D8 RID: 216
		private readonly TextureModifier.OriginalTextureCache _originalTexCache = new TextureModifier.OriginalTextureCache();

		// Token: 0x040000D9 RID: 217
		public static UIParams uiParams;

		// Token: 0x040000DA RID: 218
		public static GUIStyle lStyle;

		// Token: 0x040000DB RID: 219
		private static readonly OutputUtil OUT_UTIL = OutputUtil.Instance;

		// Token: 0x0200000F RID: 15
		private class FilterParams
		{
			// Token: 0x060000A5 RID: 165 RVA: 0x0000992D File Offset: 0x00007B2D
			public void Clear()
			{
				this._params.Clear();
			}

			// Token: 0x060000A6 RID: 166 RVA: 0x0000993A File Offset: 0x00007B3A
			public void Add(string key, TextureModifier.FilterParam filter)
			{
				this._params[key] = filter;
			}

			// Token: 0x060000A7 RID: 167 RVA: 0x0000994C File Offset: 0x00007B4C
			public TextureModifier.FilterParam Get(string key)
			{
				TextureModifier.FilterParam result;
				if (!this._params.TryGetValue(key, out result))
				{
					return null;
				}
				return result;
			}

			// Token: 0x060000A8 RID: 168 RVA: 0x0000996C File Offset: 0x00007B6C
			public TextureModifier.FilterParam GetOrAdd(string key)
			{
				TextureModifier.FilterParam filterParam;
				if (this._params.TryGetValue(key, out filterParam))
				{
					return filterParam;
				}
				filterParam = new TextureModifier.FilterParam();
				this._params[key] = filterParam;
				return filterParam;
			}

			// Token: 0x060000A9 RID: 169 RVA: 0x0000999F File Offset: 0x00007B9F
			public bool Remove(string key)
			{
				return this._params.Remove(key);
			}

			// Token: 0x040000DD RID: 221
			private readonly Dictionary<string, TextureModifier.FilterParam> _params = new Dictionary<string, TextureModifier.FilterParam>();
		}

		// Token: 0x02000010 RID: 16
		public class FilterParam
		{
			// Token: 0x17000009 RID: 9
			// (get) Token: 0x060000AB RID: 171 RVA: 0x000099C0 File Offset: 0x00007BC0
			public bool IsDirty
			{
				get
				{
					return this.Dirty.Value;
				}
			}

			// Token: 0x060000AC RID: 172 RVA: 0x000099D0 File Offset: 0x00007BD0
			public FilterParam()
			{
				this.Dirty = new TextureModifier.DirtyFlag();
				this.Hue = new TextureModifier.DirtyValue(this.Dirty, TextureModifier.FilterParam.HueRange, TextureModifier.FilterParam.HueRange.Min);
				this.Saturation = new TextureModifier.DirtyValue(this.Dirty, TextureModifier.FilterParam.SaturRange, TextureModifier.FilterParam.SaturRange.Max * 0.5f);
				this.Lightness = new TextureModifier.DirtyValue(this.Dirty, TextureModifier.FilterParam.LightRange, TextureModifier.FilterParam.LightRange.Max * 0.5f);
				this.InputMin = new TextureModifier.DirtyValue(this.Dirty, TextureModifier.FilterParam.InpMinRange, TextureModifier.FilterParam.InpMinRange.Min);
				this.InputMax = new TextureModifier.DirtyValue(this.Dirty, TextureModifier.FilterParam.InpMaxRange, TextureModifier.FilterParam.InpMaxRange.Max);
				this.InputMid = new TextureModifier.DirtyValue(this.Dirty, TextureModifier.FilterParam.InpMidRange, TextureModifier.FilterParam.InpMidRange.Max * 0.5f);
				this.OutputMin = new TextureModifier.DirtyValue(this.Dirty, TextureModifier.FilterParam.OutMinRange, TextureModifier.FilterParam.OutMinRange.Min);
				this.OutputMax = new TextureModifier.DirtyValue(this.Dirty, TextureModifier.FilterParam.OutMaxRange, TextureModifier.FilterParam.OutMaxRange.Max);
			}

			// Token: 0x060000AD RID: 173 RVA: 0x00009B00 File Offset: 0x00007D00
			public FilterParam(TextureModifier.FilterParam filter)
			{
				this.Dirty = new TextureModifier.DirtyFlag();
				this.Hue = new TextureModifier.DirtyValue(this.Dirty, TextureModifier.FilterParam.HueRange, filter.Hue.Value);
				this.Saturation = new TextureModifier.DirtyValue(this.Dirty, TextureModifier.FilterParam.SaturRange, filter.Saturation.Value);
				this.Lightness = new TextureModifier.DirtyValue(this.Dirty, TextureModifier.FilterParam.LightRange, filter.Lightness.Value);
				this.InputMin = new TextureModifier.DirtyValue(this.Dirty, TextureModifier.FilterParam.InpMinRange, filter.InputMin.Value);
				this.InputMax = new TextureModifier.DirtyValue(this.Dirty, TextureModifier.FilterParam.InpMaxRange, filter.InputMax.Value);
				this.InputMid = new TextureModifier.DirtyValue(this.Dirty, TextureModifier.FilterParam.InpMidRange, filter.InputMid.Value);
				this.OutputMin = new TextureModifier.DirtyValue(this.Dirty, TextureModifier.FilterParam.OutMinRange, filter.OutputMin.Value);
				this.OutputMax = new TextureModifier.DirtyValue(this.Dirty, TextureModifier.FilterParam.OutMaxRange, filter.OutputMax.Value);
			}

			// Token: 0x060000AE RID: 174 RVA: 0x00009C28 File Offset: 0x00007E28
			public void Clear()
			{
				this.Dirty.Value = false;
				this.Hue.Value = TextureModifier.FilterParam.HueRange.Min;
				this.Saturation.Value = TextureModifier.FilterParam.SaturRange.Max * 0.5f;
				this.Lightness.Value = TextureModifier.FilterParam.LightRange.Max * 0.5f;
				this.InputMin.Value = TextureModifier.FilterParam.InpMinRange.Min;
				this.InputMax.Value = TextureModifier.FilterParam.InpMaxRange.Max;
				this.InputMid.Value = TextureModifier.FilterParam.InpMidRange.Max * 0.5f;
				this.OutputMin.Value = TextureModifier.FilterParam.OutMinRange.Min;
				this.OutputMax.Value = TextureModifier.FilterParam.OutMaxRange.Max;
			}

			// Token: 0x060000AF RID: 175 RVA: 0x00009CFC File Offset: 0x00007EFC
			public bool HasNotChanged()
			{
				return this.Hue.Value < TextureModifier.FilterParam.THRESHOLD && this.InputMin.Value < TextureModifier.FilterParam.THRESHOLD && this.OutputMin.Value < TextureModifier.FilterParam.THRESHOLD && TextureModifier.FilterParam.InpMaxRange.Max - this.InputMax.Value < TextureModifier.FilterParam.THRESHOLD && TextureModifier.FilterParam.OutMaxRange.Max - this.OutputMax.Value < TextureModifier.FilterParam.THRESHOLD && Math.Abs(this.Lightness.Value - 100f) < TextureModifier.FilterParam.THRESHOLD && Math.Abs(this.Saturation.Value - 100f) < TextureModifier.FilterParam.THRESHOLD && Math.Abs(this.InputMid.Value - 50f) < TextureModifier.FilterParam.THRESHOLD;
			}

			// Token: 0x060000B0 RID: 176 RVA: 0x00009DDB File Offset: 0x00007FDB
			public void ClearDirtyFlag()
			{
				this.Dirty.Value = false;
			}

			// Token: 0x060000B1 RID: 177 RVA: 0x00009DEC File Offset: 0x00007FEC
			public void ProcGUI(Texture2D tex2d)
			{
				float num = (float)TextureModifier.uiParams.margin;
				this.guiSlider(num, this.Hue);
				this.guiSlider(num, this.Saturation);
				this.guiSlider(num, this.Lightness);
				if (this.guiSlider(num, this.InputMin) && this.InputMin.Value > this.InputMax.Value)
				{
					this.InputMax.Value = this.InputMin.Value;
				}
				if (this.guiSlider(num, this.InputMax) && this.InputMax.Value < this.InputMin.Value)
				{
					this.InputMin.Value = this.InputMax.Value;
				}
				this.guiSlider(num, this.InputMid);
				if (this.guiSlider(num, this.OutputMin) && this.OutputMin.Value > this.OutputMax.Value)
				{
					this.OutputMax.Value = this.OutputMin.Value;
				}
				if (this.guiSlider(num, this.OutputMax) && this.OutputMin.Value > this.OutputMax.Value)
				{
					this.OutputMin.Value = this.OutputMax.Value;
				}
				GUILayout.Space(num);
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				try
				{
					GUILayout.Space(num * 4f);
					if (GUILayout.Button("リセット", TextureModifier.uiParams.bStyleSC, new GUILayoutOption[0]))
					{
						this.Clear();
						this.Dirty.Value = true;
					}
					if (GUILayout.Button("png出力", TextureModifier.uiParams.bStyleSC, new GUILayoutOption[0]))
					{
						try
						{
							byte[] imageBytes = tex2d.EncodeToPNG();
							string exportDirectory = TextureModifier.OUT_UTIL.GetExportDirectory();
							DateTime now = DateTime.Now;
							string text = tex2d.name + "_" + now.ToString("MMddHHmmss") + ".png";
							string text2 = Path.Combine(exportDirectory, text);
							TextureModifier.OUT_UTIL.WriteBytes(text2, imageBytes);
							this.endTicks = now.Ticks + 100000000L;
							this.message = text + "を出力しました";
							LogUtil.Log(new object[]
							{
								"png ファイルを出力しました。file=",
								text2
							});
						}
						catch (Exception ex)
						{
							this.endTicks = DateTime.Now.Ticks + 100000000L;
							this.message = "png出力に失敗しました。";
							LogUtil.Log(new object[]
							{
								this.message,
								ex
							});
						}
					}
					GUILayout.Space(num * 4f);
				}
				finally
				{
					GUILayout.EndHorizontal();
				}
				if (!string.IsNullOrEmpty(this.message))
				{
					GUILayout.Label(this.message, TextureModifier.uiParams.lStyleS, new GUILayoutOption[0]);
					if (this.endTicks <= DateTime.Now.Ticks)
					{
						this.message = string.Empty;
						return;
					}
				}
				else
				{
					GUILayout.Space(num * 2f);
				}
			}

			// Token: 0x060000B2 RID: 178 RVA: 0x0000A11C File Offset: 0x0000831C
			private bool guiSlider(float margin, TextureModifier.DirtyValue dirtyValue)
			{
				float num = dirtyValue.Value;
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				try
				{
					GUILayout.Label(dirtyValue.Name, TextureModifier.uiParams.lStyle, new GUILayoutOption[]
					{
						GUILayout.Width(64f)
					});
					GUILayout.Label(num.ToString("F0"), TextureModifier.uiParams.lStyle, new GUILayoutOption[]
					{
						GUILayout.Width(32f)
					});
					GUILayout.BeginVertical(new GUILayoutOption[0]);
					GUILayout.Space(margin * 5f);
					num = GUILayout.HorizontalSlider(num, dirtyValue.Min, dirtyValue.Max, new GUILayoutOption[0]);
					GUILayout.EndVertical();
					GUILayout.Space(margin * 3f);
				}
				finally
				{
					GUILayout.EndHorizontal();
				}
				if (NumberUtil.Equals(dirtyValue.Value, num, 0.001f))
				{
					return false;
				}
				dirtyValue.Value = num;
				return true;
			}

			// Token: 0x040000DE RID: 222
			private static readonly TextureModifier.NamedRange HueRange = new TextureModifier.NamedRange("色相", 0f, 360f);

			// Token: 0x040000DF RID: 223
			private static readonly TextureModifier.NamedRange SaturRange = new TextureModifier.NamedRange("彩度", 0f, 200f);

			// Token: 0x040000E0 RID: 224
			private static readonly TextureModifier.NamedRange LightRange = new TextureModifier.NamedRange("明度", 0f, 200f);

			// Token: 0x040000E1 RID: 225
			private static readonly TextureModifier.NamedRange InpMinRange = new TextureModifier.NamedRange("InpMin", 0f, 100f);

			// Token: 0x040000E2 RID: 226
			private static readonly TextureModifier.NamedRange InpMaxRange = new TextureModifier.NamedRange("InpMax", 0f, 100f);

			// Token: 0x040000E3 RID: 227
			private static readonly TextureModifier.NamedRange InpMidRange = new TextureModifier.NamedRange("InpMid", 0f, 100f);

			// Token: 0x040000E4 RID: 228
			private static readonly TextureModifier.NamedRange OutMinRange = new TextureModifier.NamedRange("OutMin", 0f, 100f);

			// Token: 0x040000E5 RID: 229
			private static readonly TextureModifier.NamedRange OutMaxRange = new TextureModifier.NamedRange("OutMax", 0f, 100f);

			// Token: 0x040000E6 RID: 230
			public TextureModifier.DirtyFlag Dirty;

			// Token: 0x040000E7 RID: 231
			public TextureModifier.DirtyValue Hue;

			// Token: 0x040000E8 RID: 232
			public TextureModifier.DirtyValue Saturation;

			// Token: 0x040000E9 RID: 233
			public TextureModifier.DirtyValue Lightness;

			// Token: 0x040000EA RID: 234
			public TextureModifier.DirtyValue InputMin;

			// Token: 0x040000EB RID: 235
			public TextureModifier.DirtyValue InputMax;

			// Token: 0x040000EC RID: 236
			public TextureModifier.DirtyValue InputMid;

			// Token: 0x040000ED RID: 237
			public TextureModifier.DirtyValue OutputMin;

			// Token: 0x040000EE RID: 238
			public TextureModifier.DirtyValue OutputMax;

			// Token: 0x040000EF RID: 239
			private static readonly float THRESHOLD = 0.01f;

			// Token: 0x040000F0 RID: 240
			private string message;

			// Token: 0x040000F1 RID: 241
			private long endTicks;
		}

		// Token: 0x02000011 RID: 17
		public class DirtyFlag
		{
			// Token: 0x040000F2 RID: 242
			public bool Value;
		}

		// Token: 0x02000012 RID: 18
		public class NamedRange
		{
			// Token: 0x1700000A RID: 10
			// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000A2F3 File Offset: 0x000084F3
			// (set) Token: 0x060000B6 RID: 182 RVA: 0x0000A2FB File Offset: 0x000084FB
			public string Name { get; private set; }

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000A304 File Offset: 0x00008504
			// (set) Token: 0x060000B8 RID: 184 RVA: 0x0000A30C File Offset: 0x0000850C
			public float Min { get; private set; }

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000A315 File Offset: 0x00008515
			// (set) Token: 0x060000BA RID: 186 RVA: 0x0000A31D File Offset: 0x0000851D
			public float Max { get; private set; }

			// Token: 0x060000BB RID: 187 RVA: 0x0000A326 File Offset: 0x00008526
			public NamedRange(string name, float min, float max)
			{
				this.Name = name;
				this.Min = min;
				this.Max = max;
			}
		}

		// Token: 0x02000013 RID: 19
		public class DirtyValue
		{
			// Token: 0x1700000D RID: 13
			// (get) Token: 0x060000BC RID: 188 RVA: 0x0000A343 File Offset: 0x00008543
			public string Name
			{
				get
				{
					return this.range.Name;
				}
			}

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x060000BD RID: 189 RVA: 0x0000A350 File Offset: 0x00008550
			public float Min
			{
				get
				{
					return this.range.Min;
				}
			}

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x060000BE RID: 190 RVA: 0x0000A35D File Offset: 0x0000855D
			public float Max
			{
				get
				{
					return this.range.Max;
				}
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x060000BF RID: 191 RVA: 0x0000A36A File Offset: 0x0000856A
			// (set) Token: 0x060000C0 RID: 192 RVA: 0x0000A374 File Offset: 0x00008574
			public float Value
			{
				get
				{
					return this.val;
				}
				set
				{
					float obj = Mathf.Clamp(value, this.Min, this.Max);
					if (this.val.Equals(obj))
					{
						return;
					}
					this.val = obj;
					this.dirtyFlag.Value = true;
				}
			}

			// Token: 0x060000C1 RID: 193 RVA: 0x0000A3B6 File Offset: 0x000085B6
			public DirtyValue(TextureModifier.DirtyFlag dirtyFlag, TextureModifier.NamedRange range, float val)
			{
				this.range = range;
				this.dirtyFlag = dirtyFlag;
				this.val = val;
			}

			// Token: 0x060000C2 RID: 194 RVA: 0x0000A3D3 File Offset: 0x000085D3
			public static implicit operator float(TextureModifier.DirtyValue dirtyValue)
			{
				return dirtyValue.val;
			}

			// Token: 0x040000F6 RID: 246
			private TextureModifier.DirtyFlag dirtyFlag;

			// Token: 0x040000F7 RID: 247
			private float val;

			// Token: 0x040000F8 RID: 248
			private TextureModifier.NamedRange range;
		}

		// Token: 0x02000014 RID: 20
		private class TextureHolder
		{
			// Token: 0x040000F9 RID: 249
			public bool dirty;

			// Token: 0x040000FA RID: 250
			public Texture2D texture;
		}

		// Token: 0x02000015 RID: 21
		private class OriginalTextureCache
		{
			// Token: 0x060000C4 RID: 196 RVA: 0x0000A3E4 File Offset: 0x000085E4
			public void Clear()
			{
				foreach (TextureModifier.TextureHolder textureHolder in this._cacheDic.Values)
				{
					try
					{
						UnityEngine.Object.Destroy(textureHolder.texture);
					}
					catch
					{
					}
				}
			}

			// Token: 0x060000C5 RID: 197 RVA: 0x0000A470 File Offset: 0x00008670
			public void Refresh(Texture2D[] maidTextures)
			{
				List<string> list = new List<string>();
				using (Dictionary<string, TextureModifier.TextureHolder>.KeyCollection.Enumerator enumerator = this._cacheDic.Keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string key = enumerator.Current;
						if (!maidTextures.Any((Texture2D t) => t.name == key))
						{
							list.Add(key);
						}
					}
				}
				foreach (string key2 in list)
				{
					TextureModifier.TextureHolder textureHolder;
					if (this._cacheDic.TryGetValue(key2, out textureHolder))
					{
						UnityEngine.Object.Destroy(textureHolder.texture);
					}
					this._cacheDic.Remove(key2);
				}
				foreach (Texture2D texture2D in maidTextures)
				{
					if (!this._cacheDic.ContainsKey(texture2D.name))
					{
						TextureModifier.TextureHolder value = new TextureModifier.TextureHolder
						{
							texture = UnityEngine.Object.Instantiate<Texture2D>(texture2D),
							dirty = false
						};
						this._cacheDic[texture2D.name] = value;
					}
				}
			}

			// Token: 0x060000C6 RID: 198 RVA: 0x0000A5C8 File Offset: 0x000087C8
			public TextureModifier.TextureHolder GetOrAdd(Texture2D tex)
			{
				TextureModifier.TextureHolder textureHolder;
				if (this._cacheDic.TryGetValue(tex.name, out textureHolder))
				{
					return textureHolder;
				}
				textureHolder = new TextureModifier.TextureHolder
				{
					texture = UnityEngine.Object.Instantiate<Texture2D>(tex)
				};
				this._cacheDic[tex.name] = textureHolder;
				return textureHolder;
			}

			// Token: 0x060000C7 RID: 199 RVA: 0x0000A614 File Offset: 0x00008814
			public bool Remove(string texName)
			{
				TextureModifier.TextureHolder textureHolder;
				if (!this._cacheDic.TryGetValue(texName, out textureHolder))
				{
					return false;
				}
				UnityEngine.Object.DestroyImmediate(textureHolder.texture);
				return this._cacheDic.Remove(texName);
			}

			// Token: 0x060000C8 RID: 200 RVA: 0x0000A64A File Offset: 0x0000884A
			public void SetDirty(Texture2D texture)
			{
				if (texture != null)
				{
					this.SetDirty(texture.name);
				}
			}

			// Token: 0x060000C9 RID: 201 RVA: 0x0000A661 File Offset: 0x00008861
			public void SetDirty(string name)
			{
				this._cacheDic[name].dirty = true;
			}

			// Token: 0x060000CA RID: 202 RVA: 0x0000A675 File Offset: 0x00008875
			public bool IsDirty(Texture2D texture)
			{
				return texture != null && this.IsDirty(texture.name);
			}

			// Token: 0x060000CB RID: 203 RVA: 0x0000A690 File Offset: 0x00008890
			public bool IsDirty(string name)
			{
				TextureModifier.TextureHolder textureHolder;
				return this._cacheDic.TryGetValue(name, out textureHolder) && textureHolder.dirty;
			}

			// Token: 0x060000CC RID: 204 RVA: 0x0000A6B5 File Offset: 0x000088B5
			public Texture2D GetOriginalTexture(Texture2D texture)
			{
				if (!(texture != null))
				{
					return null;
				}
				return this.GetOriginalTexture(texture.name);
			}

			// Token: 0x060000CD RID: 205 RVA: 0x0000A6D0 File Offset: 0x000088D0
			public Texture2D GetOriginalTexture(string name)
			{
				TextureModifier.TextureHolder textureHolder;
				if (!this._cacheDic.TryGetValue(name, out textureHolder))
				{
					return null;
				}
				return textureHolder.texture;
			}

			// Token: 0x040000FB RID: 251
			private readonly Dictionary<string, TextureModifier.TextureHolder> _cacheDic = new Dictionary<string, TextureModifier.TextureHolder>();
		}
	}
}
