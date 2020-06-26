using System;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.TexAnim
{
	// Token: 0x02000079 RID: 121
	public class SlideScaledTex : AnimTex
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x000235C8 File Offset: 0x000217C8
		public SlideScaledTex(Vector2 scale, Texture tex, float frameSec) : base(frameSec)
		{
			this.SetScale(ref scale);
			base.Tex = tex;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000235E0 File Offset: 0x000217E0
		public void SetScale(ref Vector2 scale1)
		{
			this.scale = scale1;
			this.ratioX = (int)Math.Round((double)(1f / this.scale.x), 3);
			this.ratioY = (int)Math.Round((double)(1f / this.scale.y), 3);
			this.imageLength = this.ratioX * this.ratioY;
			this.offsets = new Vector2[this.imageLength];
			for (int i = 0; i < this.imageLength; i++)
			{
				this.offsets[i].x = this.scale.x * (float)(i % this.ratioX);
				this.offsets[i].y = this.scale.y * (float)((this.imageLength - 1 - i) / this.ratioX);
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000236C4 File Offset: 0x000218C4
		public override Vector2 nextOffset()
		{
			if (++this.frameNo >= this.imageLength)
			{
				this.frameNo = 0;
			}
			return this.offsets[this.frameNo];
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00023708 File Offset: 0x00021908
		public override void InitOffsetIndex(Vector2 offset)
		{
			for (int i = 0; i < this.imageLength; i++)
			{
				if (this.offsets[i] == offset)
				{
					this.frameNo = i;
					return;
				}
			}
			this.frameNo = 0;
		}

		// Token: 0x040004AA RID: 1194
		private int frameNo;

		// Token: 0x040004AB RID: 1195
		private Vector2 scale;

		// Token: 0x040004AC RID: 1196
		private Vector2[] offsets;

		// Token: 0x040004AD RID: 1197
		public int ratioX;

		// Token: 0x040004AE RID: 1198
		public int ratioY;

		// Token: 0x040004AF RID: 1199
		public int imageLength;
	}
}
