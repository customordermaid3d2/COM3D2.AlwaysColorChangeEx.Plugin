using System;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x02000031 RID: 49
	public class CCPartsColor
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x00010C6E File Offset: 0x0000EE6E
		public CCPartsColor()
		{
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00010C78 File Offset: 0x0000EE78
		public CCPartsColor(MaidParts.PartsColor pc)
		{
			this.bUse = pc.m_bUse;
			this.mainHue = pc.m_nMainHue;
			this.mainChroma = pc.m_nMainChroma;
			this.mainBrightness = pc.m_nMainBrightness;
			this.mainContrast = pc.m_nMainContrast;
			this.shadowRate = pc.m_nShadowRate;
			this.shadowHue = pc.m_nShadowHue;
			this.shadowChroma = pc.m_nShadowChroma;
			this.shadowBrightness = pc.m_nShadowBrightness;
			this.shadowContrast = pc.m_nShadowContrast;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00010D10 File Offset: 0x0000EF10
		public MaidParts.PartsColor ToStruct()
		{
			return new MaidParts.PartsColor
			{
				m_bUse = this.bUse,
				m_nMainHue = this.mainHue,
				m_nMainChroma = this.mainChroma,
				m_nMainBrightness = this.mainBrightness,
				m_nMainContrast = this.mainContrast,
				m_nShadowRate = this.shadowRate,
				m_nShadowHue = this.shadowHue,
				m_nShadowChroma = this.shadowChroma,
				m_nShadowBrightness = this.shadowBrightness,
				m_nShadowContrast = this.shadowContrast
			};
		}

		// Token: 0x040001F4 RID: 500
		public bool bUse;

		// Token: 0x040001F5 RID: 501
		public int mainHue;

		// Token: 0x040001F6 RID: 502
		public int mainChroma;

		// Token: 0x040001F7 RID: 503
		public int mainBrightness;

		// Token: 0x040001F8 RID: 504
		public int mainContrast;

		// Token: 0x040001F9 RID: 505
		public int shadowRate;

		// Token: 0x040001FA RID: 506
		public int shadowHue;

		// Token: 0x040001FB RID: 507
		public int shadowChroma;

		// Token: 0x040001FC RID: 508
		public int shadowBrightness;

		// Token: 0x040001FD RID: 509
		public int shadowContrast;
	}
}
