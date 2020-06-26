using System;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x02000030 RID: 48
	public class TexFilter
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x00010B24 File Offset: 0x0000ED24
		public TexFilter()
		{
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00010B2C File Offset: 0x0000ED2C
		public TexFilter(TextureModifier.FilterParam fp)
		{
			this.Hue = fp.Hue;
			this.Saturation = fp.Saturation;
			this.Lightness = fp.Lightness;
			this.InputMin = fp.InputMin;
			this.InputMax = fp.InputMax;
			this.InputMid = fp.InputMid;
			this.OutputMin = fp.OutputMin;
			this.OutputMax = fp.OutputMax;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00010BD0 File Offset: 0x0000EDD0
		public TextureModifier.FilterParam ToFilter()
		{
			return new TextureModifier.FilterParam
			{
				Hue = 
				{
					Value = this.Hue
				},
				Saturation = 
				{
					Value = this.Saturation
				},
				Lightness = 
				{
					Value = this.Lightness
				},
				InputMin = 
				{
					Value = this.InputMin
				},
				InputMax = 
				{
					Value = this.InputMax
				},
				InputMid = 
				{
					Value = this.InputMid
				},
				OutputMin = 
				{
					Value = this.OutputMin
				},
				OutputMax = 
				{
					Value = this.OutputMax
				}
			};
		}

		// Token: 0x040001EC RID: 492
		public float Hue;

		// Token: 0x040001ED RID: 493
		public float Saturation;

		// Token: 0x040001EE RID: 494
		public float Lightness;

		// Token: 0x040001EF RID: 495
		public float InputMin;

		// Token: 0x040001F0 RID: 496
		public float InputMax;

		// Token: 0x040001F1 RID: 497
		public float InputMid;

		// Token: 0x040001F2 RID: 498
		public float OutputMin;

		// Token: 0x040001F3 RID: 499
		public float OutputMax;
	}
}
