using System;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.TexAnim
{
	// Token: 0x0200007A RID: 122
	public class ScrollTex : AnimTex
	{
		// Token: 0x06000408 RID: 1032 RVA: 0x00023750 File Offset: 0x00021950
		public ScrollTex(Vector2 scroll, Texture tex, float frameSec) : base(frameSec)
		{
			this.scrollRatio = scroll;
			this.zeroX = (Math.Abs(scroll.x) < 1E-07f);
			this.zeroY = (Math.Abs(scroll.y) < 1E-07f);
			base.Tex = tex;
			this.tex.wrapMode = TextureWrapMode.Repeat;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x000237C0 File Offset: 0x000219C0
		public override Vector2 nextOffset()
		{
			if (!this.zeroX)
			{
				this.offset.x = Mathf.Repeat(this.offset.x + this.scrollRatio.x, 1f);
			}
			if (!this.zeroY)
			{
				this.offset.y = Mathf.Repeat(this.offset.y + this.scrollRatio.y, 1f);
			}
			return this.offset;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0002383B File Offset: 0x00021A3B
		public override void InitOffsetIndex(Vector2 offset1)
		{
			this.offset = offset1;
		}

		// Token: 0x040004B0 RID: 1200
		public Vector2 scrollRatio;

		// Token: 0x040004B1 RID: 1201
		private Vector2 offset;

		// Token: 0x040004B2 RID: 1202
		private readonly bool zeroX = true;

		// Token: 0x040004B3 RID: 1203
		private readonly bool zeroY = true;
	}
}
