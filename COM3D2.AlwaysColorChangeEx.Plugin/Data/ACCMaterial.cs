using System;
using System.Linq;
using CM3D2.AlwaysColorChangeEx.Plugin.UI;
using CM3D2.AlwaysColorChangeEx.Plugin.UI.Data;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x02000017 RID: 23
	public class ACCMaterial
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000A768 File Offset: 0x00008968
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x0000A770 File Offset: 0x00008970
		public ACCMaterial Original { get; private set; }

		// Token: 0x060000D4 RID: 212 RVA: 0x0000A779 File Offset: 0x00008979
		protected ACCMaterial(ShaderType type)
		{
			this.type = type;
			this.InitType();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000A7A4 File Offset: 0x000089A4
		public ACCMaterial(ACCMaterial src)
		{
			this.Original = src;
			this.renderer = src.renderer;
			this.material = src.material;
			this.name = src.name;
			this.type = src.type;
			this.renderQueue = src.renderQueue;
			this.editColors = src.editColors;
			this.pickers = src.pickers;
			this.editVals = src.editVals;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000A834 File Offset: 0x00008A34
		public ACCMaterial(Material m, Renderer r = null, int idx = -1)
		{
			this.material = m;
			this.renderer = r;
			this.matIdx = idx;
			this.name = m.name;
			this.type = ShaderType.Resolve(m.shader.name);
			if (this.type == ShaderType.UNKNOWN)
			{
				return;
			}
			this.renderQueue.Set((float)m.renderQueue);
			this.rqEdit = this.renderQueue.ToString();
			this.InitType();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000A8CC File Offset: 0x00008ACC
		private void InitType()
		{
			ShaderPropColor[] colProps = this.type.colProps;
			this.editColors = new EditColor[colProps.Length];
			this.pickers = new ColorPicker[colProps.Length];
			for (int i = 0; i < colProps.Length; i++)
			{
				ShaderPropColor shaderPropColor = colProps[i];
				Color val = (this.material != null) ? this.material.GetColor(shaderPropColor.propId) : shaderPropColor.defaultVal;
				EditColor editColor = new EditColor(val, shaderPropColor.colorType, shaderPropColor.composition);
				this.editColors[i] = editColor;
				ColorPicker colorPicker = new ColorPicker(ACCMaterial.presetMgr)
				{
					ColorTex = new Texture2D(16, 16, TextureFormat.RGB24, false)
				};
				colorPicker.SetTexColor(ref val);
				this.pickers[i] = colorPicker;
			}
			ShaderPropFloat[] fProps = this.type.fProps;
			this.editVals = new EditValue[fProps.Length];
			for (int j = 0; j < fProps.Length; j++)
			{
				float val2 = fProps[j].defaultVal;
				if (this.material != null)
				{
					val2 = this.material.GetFloat(fProps[j].propId);
				}
				this.editVals[j] = new EditValue(val2, fProps[j].range);
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000AA0E File Offset: 0x00008C0E
		private Color GetColor(int i)
		{
			if (i >= this.editColors.Length)
			{
				return Color.white;
			}
			return this.editColors[i].val;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000AA30 File Offset: 0x00008C30
		public void ChangeShader(string shaderName, int shaderIdx = -1)
		{
			Shader shader = Shader.Find(shaderName);
			if (shader == null)
			{
				LogUtil.Debug(new object[]
				{
					"Shader not found. load resource :",
					shaderName
				});
				shader = Resources.Load<Shader>("Shaders/" + shaderName);
				if (shader == null)
				{
					return;
				}
			}
			int num = this.material.renderQueue;
			this.material.shader = shader;
			this.material.renderQueue = num;
			ShaderType shaderType = (shaderIdx != -1) ? ShaderType.Resolve(shaderIdx) : ShaderType.Resolve(shaderName);
			if (shaderType == ShaderType.UNKNOWN)
			{
				return;
			}
			this.Update(shaderType);
			LogUtil.Debug(new object[]
			{
				"selected shader updated: ",
				shaderName
			});
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000AAE8 File Offset: 0x00008CE8
		public void Update(ShaderType sdrType)
		{
			if (this.type == sdrType)
			{
				return;
			}
			ShaderPropColor[] colProps = sdrType.colProps;
			EditColor[] array = new EditColor[colProps.Length];
			ColorPicker[] array2 = new ColorPicker[colProps.Length];
			int itemHeight = UIParams.Instance.itemHeight;
			for (int i = 0; i < colProps.Length; i++)
			{
				ShaderPropColor shaderPropColor = colProps[i];
				if (i < this.editColors.Length)
				{
					if (this.editColors[i].type == shaderPropColor.colorType)
					{
						array[i] = this.editColors[i];
					}
					else
					{
						array[i] = new EditColor(this.editColors[i].val, shaderPropColor.colorType, shaderPropColor.composition);
					}
					array2[i] = this.pickers[i];
				}
				else
				{
					Color val;
					if (this.material != null)
					{
						val = this.material.GetColor(shaderPropColor.propId);
					}
					else
					{
						val = ((this.Original != null) ? this.Original.GetColor(i) : shaderPropColor.defaultVal);
					}
					array[i] = new EditColor(val, shaderPropColor.colorType, shaderPropColor.composition);
					array2[i] = new ColorPicker(ACCMaterial.presetMgr)
					{
						ColorTex = new Texture2D(itemHeight, itemHeight, TextureFormat.RGB24, false)
					};
					array2[i].SetTexColor(ref array[i].val);
				}
			}
			this.editColors = array;
			this.pickers = array2;
			ShaderPropFloat[] fProps = sdrType.fProps;
			EditValue[] array3 = new EditValue[fProps.Length];
			for (int j = 0; j < fProps.Length; j++)
			{
				float val2 = (this.material != null) ? this.material.GetFloat(fProps[j].propId) : fProps[j].defaultVal;
				array3[j] = new EditValue(val2, fProps[j].range);
			}
			this.editVals = array3;
			foreach (ShaderPropTex shaderPropTex in sdrType.texProps)
			{
				if (this.material != null && !this.material.HasProperty(shaderPropTex.keyName))
				{
					this.material.SetTexture(shaderPropTex.propId, new Texture());
				}
			}
			this.type = sdrType;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000AD70 File Offset: 0x00008F70
		public bool HasChanged(ACCMaterial mate)
		{
			return this.editColors.Where((EditColor t, int i) => t.val != mate.editColors[i].val).Any<EditColor>() || this.editVals.Where((EditValue t, int i) => !NumberUtil.Equals(t.val, mate.editVals[i].val, 0.001f)).Any<EditValue>();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000ADC8 File Offset: 0x00008FC8
		public void SetColor(string propName, Color c)
		{
			try
			{
				PropKey propKey = (PropKey)Enum.Parse(typeof(PropKey), propName);
				for (int i = 0; i < this.type.colProps.Length; i++)
				{
					ShaderPropColor shaderPropColor = this.type.colProps[i];
					if (shaderPropColor.key == propKey)
					{
						this.editColors[i].Set(c);
						this.pickers[i].SetTexColor(ref c);
						return;
					}
				}
				LogUtil.Debug(new object[]
				{
					"propName mismatched:",
					propName
				});
			}
			catch (Exception ex)
			{
				LogUtil.Debug(new object[]
				{
					"unsupported propName found:",
					propName,
					ex
				});
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000AE90 File Offset: 0x00009090
		public void SetFloat(string propName, float f)
		{
			try
			{
				PropKey propKey = (PropKey)Enum.Parse(typeof(PropKey), propName);
				for (int i = 0; i < this.type.fProps.Length; i++)
				{
					ShaderPropFloat shaderPropFloat = this.type.fProps[i];
					if (shaderPropFloat.key == propKey)
					{
						this.editVals[i].Set(f);
						return;
					}
				}
				LogUtil.Debug(new object[]
				{
					"propName mismatched:",
					propName
				});
			}
			catch (Exception ex)
			{
				LogUtil.Debug(new object[]
				{
					"unsupported propName found:",
					propName,
					ex
				});
			}
		}

		// Token: 0x04000100 RID: 256
		public const int ICON_SIZE = 16;

		// Token: 0x04000101 RID: 257
		internal static readonly Settings settings = Settings.Instance;

		// Token: 0x04000102 RID: 258
		internal static readonly ColorPresetManager presetMgr = ColorPresetManager.Instance;

		// Token: 0x04000103 RID: 259
		public Renderer renderer;

		// Token: 0x04000104 RID: 260
		public int matIdx;

		// Token: 0x04000105 RID: 261
		public Material material;

		// Token: 0x04000106 RID: 262
		public string name;

		// Token: 0x04000107 RID: 263
		public ShaderType type;

		// Token: 0x04000108 RID: 264
		public readonly EditValue renderQueue = new EditValue(2000f, EditRange.renderQueue);

		// Token: 0x04000109 RID: 265
		public EditColor[] editColors;

		// Token: 0x0400010A RID: 266
		public ColorPicker[] pickers;

		// Token: 0x0400010B RID: 267
		public EditValue[] editVals;

		// Token: 0x0400010C RID: 268
		public string rqEdit;
	}
}
