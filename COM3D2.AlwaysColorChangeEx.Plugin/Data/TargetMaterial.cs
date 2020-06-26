using System;
using System.Collections.Generic;
using System.IO;
using CM3D2.AlwaysColorChangeEx.Plugin.UI;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x0200001B RID: 27
	public class TargetMaterial
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000C4EE File Offset: 0x0000A6EE
		// (set) Token: 0x06000118 RID: 280 RVA: 0x0000C4F6 File Offset: 0x0000A6F6
		public string slotName { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000C4FF File Offset: 0x0000A6FF
		// (set) Token: 0x0600011A RID: 282 RVA: 0x0000C507 File Offset: 0x0000A707
		public int matNo { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000C510 File Offset: 0x0000A710
		// (set) Token: 0x0600011C RID: 284 RVA: 0x0000C518 File Offset: 0x0000A718
		public string filename { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600011D RID: 285 RVA: 0x0000C521 File Offset: 0x0000A721
		// (set) Token: 0x0600011E RID: 286 RVA: 0x0000C529 File Offset: 0x0000A729
		public string editfile { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600011F RID: 287 RVA: 0x0000C532 File Offset: 0x0000A732
		// (set) Token: 0x06000120 RID: 288 RVA: 0x0000C53A File Offset: 0x0000A73A
		public bool editfileExist { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000C543 File Offset: 0x0000A743
		// (set) Token: 0x06000122 RID: 290 RVA: 0x0000C54B File Offset: 0x0000A74B
		public bool pmatExport { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000C554 File Offset: 0x0000A754
		// (set) Token: 0x06000124 RID: 292 RVA: 0x0000C55C File Offset: 0x0000A75C
		public bool uiTexViewed { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000C565 File Offset: 0x0000A765
		// (set) Token: 0x06000126 RID: 294 RVA: 0x0000C56D File Offset: 0x0000A76D
		public bool needPmat { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000C576 File Offset: 0x0000A776
		// (set) Token: 0x06000128 RID: 296 RVA: 0x0000C57E File Offset: 0x0000A77E
		public bool needPmatChange { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000C587 File Offset: 0x0000A787
		// (set) Token: 0x0600012A RID: 298 RVA: 0x0000C58F File Offset: 0x0000A78F
		public bool onlyModel { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600012B RID: 299 RVA: 0x0000C598 File Offset: 0x0000A798
		// (set) Token: 0x0600012C RID: 300 RVA: 0x0000C5A0 File Offset: 0x0000A7A0
		public bool shaderChanged { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000C5A9 File Offset: 0x0000A7A9
		// (set) Token: 0x0600012E RID: 302 RVA: 0x0000C5B1 File Offset: 0x0000A7B1
		public bool hasParamChanged { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600012F RID: 303 RVA: 0x0000C5BA File Offset: 0x0000A7BA
		// (set) Token: 0x06000130 RID: 304 RVA: 0x0000C5C2 File Offset: 0x0000A7C2
		public bool hasTexColorChanged { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000C5CB File Offset: 0x0000A7CB
		// (set) Token: 0x06000132 RID: 306 RVA: 0x0000C5D3 File Offset: 0x0000A7D3
		public bool hasTexFileChanged { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000133 RID: 307 RVA: 0x0000C5DC File Offset: 0x0000A7DC
		// (set) Token: 0x06000134 RID: 308 RVA: 0x0000C5E4 File Offset: 0x0000A7E4
		public ACCMaterial editedMat { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0000C5ED File Offset: 0x0000A7ED
		// (set) Token: 0x06000136 RID: 310 RVA: 0x0000C5F5 File Offset: 0x0000A7F5
		public ACCMaterialEx srcMat { get; set; }

		// Token: 0x06000137 RID: 311 RVA: 0x0000C5FE File Offset: 0x0000A7FE
		public TargetMaterial(string slot, int matNo, string filename)
		{
			this.slotName = slot;
			this.matNo = matNo;
			this.filename = filename;
			this.editfile = Path.GetFileNameWithoutExtension(filename);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000C63E File Offset: 0x0000A83E
		public string EditFileName()
		{
			if (this.worksuffix == null)
			{
				return this.editfile + FileConst.EXT_MATERIAL;
			}
			return this.editfile + this.worksuffix + FileConst.EXT_MATERIAL;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000C670 File Offset: 0x0000A870
		public void Init(ACCMaterial edited)
		{
			this.editedMat = edited;
			if (!this.onlyModel && !string.IsNullOrEmpty(this.filename))
			{
				LogUtil.Debug(new object[]
				{
					"load material file",
					this.filename
				});
				this.srcMat = ACCMaterialEx.Load(this.filename);
				this.shaderChanged = (this.editedMat.type != this.srcMat.type);
			}
			if (edited.type.isTrans)
			{
				if (Math.Abs(edited.renderQueue.val - 2000f) < 0.01f)
				{
					this.needPmat = false;
				}
				else
				{
					this.needPmat = true;
					string matName = (this.srcMat != null) ? this.srcMat.name2 : edited.name;
					float renderQueue = MaterialUtil.GetRenderQueue(matName);
					if (renderQueue < 0f)
					{
						this.needPmatChange = true;
					}
					LogUtil.DebugF("render queue: src={0}, edited={1}", new object[]
					{
						renderQueue,
						edited.renderQueue
					});
					this.needPmatChange |= !NumberUtil.Equals(edited.renderQueue.val, renderQueue, 0.01f);
					this.pmatExport = this.needPmatChange;
				}
			}
			if (!this.shaderChanged && this.srcMat != null)
			{
				this.hasParamChanged = this.editedMat.HasChanged(this.srcMat);
			}
			this.editname = this.editedMat.material.name;
			Maid currentMaid = MaidHolder.Instance.CurrentMaid;
			foreach (ShaderPropTex shaderPropTex in this.editedMat.type.texProps)
			{
				LogUtil.Debug(new object[]
				{
					"propName:",
					shaderPropTex.key
				});
				Texture texture = this.editedMat.material.GetTexture(shaderPropTex.propId);
				TextureModifier.FilterParam filter = ACCTexturesView.GetFilter(currentMaid, this.slotName, this.editedMat.material, shaderPropTex.propId);
				bool flag = filter != null && !filter.HasNotChanged();
				bool flag2 = false;
				ACCTextureEx acctextureEx;
				if (texture != null && this.srcMat != null && this.srcMat.texDic.TryGetValue(shaderPropTex.key, out acctextureEx))
				{
					flag2 = (acctextureEx.editname != texture.name);
				}
				TargetTexture value = new TargetTexture(flag, flag2, texture)
				{
					filter = filter
				};
				this.texDic[shaderPropTex.key] = value;
				this.hasTexColorChanged = (this.hasTexColorChanged || flag);
				this.hasTexFileChanged = (this.hasTexFileChanged || flag2);
			}
			LogUtil.Debug(new object[]
			{
				"target material initialized"
			});
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000C94A File Offset: 0x0000AB4A
		public float RenderQueue()
		{
			return this.editedMat.renderQueue.val;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000C95C File Offset: 0x0000AB5C
		public string ShaderName()
		{
			return this.editedMat.type.name;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000C96E File Offset: 0x0000AB6E
		public string ShaderNameOrDefault(string defaultVal)
		{
			if (this.editedMat == null)
			{
				return defaultVal;
			}
			return this.editedMat.type.name;
		}

		// Token: 0x0400012E RID: 302
		public string editname = string.Empty;

		// Token: 0x0400012F RID: 303
		public string worksuffix;

		// Token: 0x04000130 RID: 304
		public readonly Dictionary<PropKey, TargetTexture> texDic = new Dictionary<PropKey, TargetTexture>(5);
	}
}
