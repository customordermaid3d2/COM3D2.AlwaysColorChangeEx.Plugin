using System;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x02000032 RID: 50
	public class RQResolver
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00010DAA File Offset: 0x0000EFAA
		public static RQResolver Instance
		{
			get
			{
				return RQResolver.INSTANCE;
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00010DB4 File Offset: 0x0000EFB4
		public RQResolver()
		{
			this._slotRq = new float[][]
			{
				new float[]
				{
					970f
				},
				new float[]
				{
					970f,
					990f
				},
				RQResolver.EMPTY_RQ,
				new float[]
				{
					980f
				},
				RQResolver.EMPTY_RQ,
				RQResolver.EMPTY_RQ,
				RQResolver.EMPTY_RQ,
				new float[]
				{
					3171f
				},
				new float[]
				{
					3101f
				},
				new float[]
				{
					3171f
				},
				new float[]
				{
					3091f
				},
				new float[]
				{
					3021f
				},
				new float[]
				{
					3131f
				},
				new float[]
				{
					3061f
				},
				new float[]
				{
					3071f
				},
				new float[]
				{
					3251f
				},
				new float[]
				{
					3141f
				},
				new float[]
				{
					3221f
				},
				RQResolver.EMPTY_RQ,
				RQResolver.EMPTY_RQ,
				new float[]
				{
					3211f
				},
				new float[]
				{
					3261f
				},
				new float[]
				{
					3201f
				},
				new float[]
				{
					3271f
				},
				new float[]
				{
					3136f,
					3121f
				},
				new float[]
				{
					3281f
				},
				new float[]
				{
					3181f
				},
				new float[]
				{
					3191f
				},
				new float[]
				{
					3051f
				},
				new float[]
				{
					3151f
				},
				new float[]
				{
					3081f
				},
				new float[]
				{
					3161f,
					3176f
				},
				new float[]
				{
					3111f
				},
				new float[]
				{
					3041f
				},
				new float[]
				{
					3031f
				},
				new float[]
				{
					2898f
				},
				new float[]
				{
					3231f
				},
				RQResolver.EMPTY_RQ,
				RQResolver.EMPTY_RQ,
				new float[]
				{
					3010f
				},
				new float[]
				{
					3241f
				},
				new float[]
				{
					3301f
				},
				new float[]
				{
					3301f
				},
				new float[]
				{
					3015f
				},
				new float[]
				{
					3015f
				},
				new float[]
				{
					3015f
				},
				new float[]
				{
					3015f
				},
				new float[]
				{
					3015f
				},
				new float[]
				{
					3015f
				},
				new float[]
				{
					3015f
				},
				new float[]
				{
					3136f,
					3121f
				},
				new float[]
				{
					3201f
				},
				new float[]
				{
					3271f
				},
				new float[]
				{
					3261f
				},
				new float[]
				{
					3261f
				},
				new float[]
				{
					3281f
				},
				new float[]
				{
					3005f
				},
				RQResolver.EMPTY_RQ
			};
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0001129F File Offset: 0x0000F49F
		public float[] Resolve(int slotId)
		{
			if (this._slotRq.Length < slotId)
			{
				return RQResolver.EMPTY_RQ;
			}
			return this._slotRq[slotId];
		}

		// Token: 0x040001FE RID: 510
		private static readonly RQResolver INSTANCE = new RQResolver();

		// Token: 0x040001FF RID: 511
		private readonly float[][] _slotRq;

		// Token: 0x04000200 RID: 512
		private static readonly float[] EMPTY_RQ = new float[0];
	}
}
