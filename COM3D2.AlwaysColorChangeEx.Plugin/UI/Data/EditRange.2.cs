using System;

namespace CM3D2.AlwaysColorChangeEx.Plugin.UI.Data
{
	// Token: 0x02000062 RID: 98
	public static class EditRange
	{
		// Token: 0x0400036F RID: 879
		private static readonly Settings _settings = Settings.Instance;

		// Token: 0x04000370 RID: 880
		public static readonly EditRange<float> renderQueue = new EditRange<float>("F0", 0f, 5000f);

		// Token: 0x04000371 RID: 881
		public static readonly EditRange<float> shininess = new EditRange<float>(EditRange._settings.shininessFmt, EditRange._settings.shininessEditMin, EditRange._settings.shininessEditMax);

		// Token: 0x04000372 RID: 882
		public static readonly EditRange<float> outlineWidth = new EditRange<float>(EditRange._settings.outlineWidthFmt, EditRange._settings.outlineWidthEditMin, EditRange._settings.outlineWidthEditMax);

		// Token: 0x04000373 RID: 883
		public static readonly EditRange<float> rimPower = new EditRange<float>(EditRange._settings.rimPowerFmt, EditRange._settings.rimPowerEditMin, EditRange._settings.rimPowerEditMax);

		// Token: 0x04000374 RID: 884
		public static readonly EditRange<float> rimShift = new EditRange<float>(EditRange._settings.rimShiftFmt, EditRange._settings.rimShiftEditMin, EditRange._settings.rimShiftEditMax);

		// Token: 0x04000375 RID: 885
		public static readonly EditRange<float> hiRate = new EditRange<float>(EditRange._settings.hiRateFmt, EditRange._settings.hiRateEditMin, EditRange._settings.hiRateEditMax);

		// Token: 0x04000376 RID: 886
		public static readonly EditRange<float> hiPow = new EditRange<float>(EditRange._settings.hiPowFmt, EditRange._settings.hiPowEditMin, EditRange._settings.hiPowEditMax);

		// Token: 0x04000377 RID: 887
		public static readonly EditRange<float> floatVal1 = new EditRange<float>(EditRange._settings.floatVal1Fmt, EditRange._settings.floatVal1EditMin, EditRange._settings.floatVal1EditMax);

		// Token: 0x04000378 RID: 888
		public static readonly EditRange<float> floatVal2 = new EditRange<float>(EditRange._settings.floatVal2Fmt, EditRange._settings.floatVal2EditMin, EditRange._settings.floatVal2EditMax);

		// Token: 0x04000379 RID: 889
		public static readonly EditRange<float> floatVal3 = new EditRange<float>(EditRange._settings.floatVal3Fmt, EditRange._settings.floatVal3EditMin, EditRange._settings.floatVal3EditMax);

		// Token: 0x0400037A RID: 890
		public static readonly EditRange<int> hue = new EditRange<int>("F0", 0, 255);

		// Token: 0x0400037B RID: 891
		public static readonly EditRange<int> saturation = new EditRange<int>("F0", 0, 255);

		// Token: 0x0400037C RID: 892
		public static readonly EditRange<int> light = new EditRange<int>("F0", 0, 510);

		// Token: 0x0400037D RID: 893
		public static readonly EditRange<int> contrast = new EditRange<int>("F0", 0, 200);

		// Token: 0x0400037E RID: 894
		public static readonly EditRange<int> rate = new EditRange<int>("F0", 0, 255);

		// Token: 0x0400037F RID: 895
		public static readonly EditRange<float> boolVal = new EditRange<float>("F0", 0f, 1f);
	}
}
