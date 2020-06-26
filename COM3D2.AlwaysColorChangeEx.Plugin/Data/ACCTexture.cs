using System;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x02000024 RID: 36
	public class ACCTexture
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000E69F File Offset: 0x0000C89F
		// (set) Token: 0x0600018C RID: 396 RVA: 0x0000E6A7 File Offset: 0x0000C8A7
		public ACCTexture original { get; private set; }

		// Token: 0x0600018D RID: 397 RVA: 0x0000E6B0 File Offset: 0x0000C8B0
		private ACCTexture(PropKey propKey)
		{
			this.propKey = propKey;
			this.propName = propKey.ToString();
			switch (propKey)
			{
			case PropKey._ToonRamp:
			case PropKey._OutlineToonRamp:
				this.toonType = 1;
				return;
			case PropKey._ShadowTex:
			case PropKey._OutlineTex:
				break;
			case PropKey._ShadowRateToon:
				this.toonType = 2;
				break;
			default:
				return;
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000E72C File Offset: 0x0000C92C
		protected ACCTexture(string propName)
		{
			this.propName = propName;
			this.propKey = (PropKey)Enum.Parse(typeof(PropKey), propName);
			switch (this.propKey)
			{
			case PropKey._ToonRamp:
			case PropKey._OutlineToonRamp:
				this.toonType = 1;
				return;
			case PropKey._ShadowTex:
			case PropKey._OutlineTex:
				break;
			case PropKey._ShadowRateToon:
				this.toonType = 2;
				break;
			default:
				return;
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000E7B8 File Offset: 0x0000C9B8
		public ACCTexture(Texture tex, Material mate, ShaderPropTex texProp, ShaderType type) : this(texProp.key)
		{
			if (tex == null)
			{
				tex = new Texture2D(2, 2)
				{
					name = string.Empty
				};
			}
			this.tex = tex;
			this.type = type;
			this.prop = texProp;
			this.editname = tex.name;
			if (tex is Texture2D)
			{
				this.texOffset = mate.GetTextureOffset(this.prop.propId);
				this.texScale = mate.GetTextureScale(this.prop.propId);
				return;
			}
			LogUtil.DebugF("propName({0}): texture type:{1}", new object[]
			{
				this.propName,
				tex.GetType()
			});
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000E870 File Offset: 0x0000CA70
		public static ACCTexture Create(Material mate, ShaderPropTex texProp, ShaderType type)
		{
			Texture texture = mate.GetTexture(texProp.propId);
			if (texture == null)
			{
				LogUtil.Debug(new object[]
				{
					"tex is null. ",
					texProp.key
				});
			}
			return new ACCTexture(texture, mate, texProp, type);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000E8C0 File Offset: 0x0000CAC0
		public ACCTexture(ACCTexture src)
		{
			this.original = src;
			this.propName = src.propName;
			this.type = src.type;
			this.prop = src.prop;
			this.propKey = src.propKey;
			this.editname = src.editname;
			this.filepath = src.filepath;
			this.texOffset = src.texOffset;
			this.texScale = src.texScale;
			this.toonType = src.toonType;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000E967 File Offset: 0x0000CB67
		public bool SetName(string name)
		{
			if (string.Equals(this.editname, name, StringComparison.CurrentCultureIgnoreCase))
			{
				return false;
			}
			this.editname = name;
			this.dirty = true;
			return true;
		}

		// Token: 0x04000175 RID: 373
		public const int RAMP = 1;

		// Token: 0x04000176 RID: 374
		public const int SHADOW_RATE = 2;

		// Token: 0x04000177 RID: 375
		public const int NONE = 0;

		// Token: 0x04000178 RID: 376
		public Texture tex;

		// Token: 0x04000179 RID: 377
		public ShaderType type;

		// Token: 0x0400017A RID: 378
		public ShaderPropTex prop;

		// Token: 0x0400017B RID: 379
		public PropKey propKey;

		// Token: 0x0400017C RID: 380
		public string propName;

		// Token: 0x0400017D RID: 381
		public string editname = string.Empty;

		// Token: 0x0400017E RID: 382
		public string filepath;

		// Token: 0x0400017F RID: 383
		public Vector2 texOffset = Vector2.zero;

		// Token: 0x04000180 RID: 384
		public Vector2 texScale = Vector2.one;

		// Token: 0x04000181 RID: 385
		public int toonType;

		// Token: 0x04000182 RID: 386
		public bool dirty;

		// Token: 0x04000183 RID: 387
		public bool expand;
	}
}
