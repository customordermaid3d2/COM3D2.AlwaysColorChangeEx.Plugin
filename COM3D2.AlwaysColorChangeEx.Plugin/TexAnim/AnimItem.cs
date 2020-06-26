using System;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.TexAnim
{
	// Token: 0x02000076 RID: 118
	public class AnimItem
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x00022FF5 File Offset: 0x000211F5
		public AnimItem(Material mat, int matNo, AnimTex[] texes)
		{
			this.material = mat;
			this.matNo = matNo;
			this.texes = texes;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00023014 File Offset: 0x00021214
		public void Animate(float deltaTime)
		{
			if (this.material == null)
			{
				return;
			}
			for (int i = 0; i < this.texes.Length; i++)
			{
				AnimTex animTex = this.texes[i];
				if (animTex != null && animTex.updateTime(deltaTime))
				{
					if (this.HasTarget(ref animTex))
					{
						this.material.SetTextureOffset(animTex.texProp.PropId, animTex.nextOffset());
					}
					this.texes[i] = animTex;
				}
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00023088 File Offset: 0x00021288
		public void Deactivate()
		{
			if (this.material == null)
			{
				return;
			}
			if (AnimItem.settings.backScale)
			{
				for (int i = 0; i < this.texes.Length; i++)
				{
					AnimTex animTex = this.texes[i];
					if (animTex != null)
					{
						this.RestoreTexPos(animTex);
						this.texes[i] = null;
					}
				}
			}
			this.material = null;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x000230E8 File Offset: 0x000212E8
		private bool HasTarget(ref AnimTex anmTex)
		{
			Texture texture = this.material.GetTexture(anmTex.texProp.PropId);
			if (texture == null)
			{
				anmTex = null;
				return false;
			}
			if (texture.GetInstanceID() != anmTex.texId)
			{
				AnimTex animTex = ParseAnimUtil.ParseAnimTex(this.material, anmTex.texProp, texture);
				if (animTex == null)
				{
					if (AnimItem.settings.backScale)
					{
						this.RestoreTexPos(anmTex);
					}
					anmTex = null;
					return false;
				}
				anmTex = animTex;
			}
			return true;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00023160 File Offset: 0x00021360
		private void RestoreTexPos(AnimTex anmTex)
		{
			int propId = anmTex.texProp.PropId;
			int propFPSId = anmTex.texProp.PropFPSId;
			this.material.SetTextureScale(propId, Vector2.one);
			this.material.SetTextureOffset(propId, Vector2.zero);
			if (this.material.HasProperty(propFPSId))
			{
				this.material.SetFloat(propFPSId, -1f);
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x000231C8 File Offset: 0x000213C8
		public void UpdateTexes(Material mate, AnimTex[] texes1)
		{
			if (this.material != mate)
			{
				this.material = mate;
				this.texes = texes1;
				return;
			}
			for (int i = 0; i < this.texes.Length; i++)
			{
				if (this.texes[i] == null && texes1[i] != null)
				{
					this.texes[i] = texes1[i];
				}
			}
		}

		// Token: 0x0400049F RID: 1183
		private static readonly Settings settings = Settings.Instance;

		// Token: 0x040004A0 RID: 1184
		public Material material;

		// Token: 0x040004A1 RID: 1185
		public int matNo;

		// Token: 0x040004A2 RID: 1186
		public AnimTex[] texes;
	}
}
