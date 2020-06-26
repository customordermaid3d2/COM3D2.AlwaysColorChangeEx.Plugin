using System;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.TexAnim
{
	// Token: 0x02000078 RID: 120
	public abstract class AnimTex
	{
		// Token: 0x060003FC RID: 1020 RVA: 0x0002355A File Offset: 0x0002175A
		protected AnimTex(float frameSecond)
		{
			this.changeFrameSecond = frameSecond;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x00023569 File Offset: 0x00021769
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x00023571 File Offset: 0x00021771
		public Texture Tex
		{
			get
			{
				return this.tex;
			}
			set
			{
				this.tex = value;
				this.texId = this.tex.GetInstanceID();
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0002358B File Offset: 0x0002178B
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x00023593 File Offset: 0x00021793
		public int texId { get; private set; }

		// Token: 0x06000401 RID: 1025 RVA: 0x0002359C File Offset: 0x0002179C
		public virtual bool updateTime(float deltaTime)
		{
			this.dTime += deltaTime;
			if (this.dTime <= this.changeFrameSecond)
			{
				return false;
			}
			this.dTime = 0f;
			return true;
		}

		// Token: 0x06000402 RID: 1026
		public abstract Vector2 nextOffset();

		// Token: 0x06000403 RID: 1027
		public abstract void InitOffsetIndex(Vector2 offset);

		// Token: 0x040004A5 RID: 1189
		private float dTime;

		// Token: 0x040004A6 RID: 1190
		public float changeFrameSecond;

		// Token: 0x040004A7 RID: 1191
		public TexProp texProp;

		// Token: 0x040004A8 RID: 1192
		protected Texture tex;
	}
}
