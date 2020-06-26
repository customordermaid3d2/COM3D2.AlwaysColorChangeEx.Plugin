using System;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x0200002D RID: 45
	public class CCColor
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x00010398 File Offset: 0x0000E598
		public CCColor()
		{
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000103A0 File Offset: 0x0000E5A0
		public CCColor(float r, float g, float b, float a)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000103C5 File Offset: 0x0000E5C5
		public CCColor(Color color)
		{
			this.r = color.r;
			this.g = color.g;
			this.b = color.b;
			this.a = color.a;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00010401 File Offset: 0x0000E601
		public Color ToColor()
		{
			return new Color(this.r, this.g, this.b, this.a);
		}

		// Token: 0x040001B8 RID: 440
		public float r;

		// Token: 0x040001B9 RID: 441
		public float g;

		// Token: 0x040001BA RID: 442
		public float b;

		// Token: 0x040001BB RID: 443
		public float a;
	}
}
