using System;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Util
{
	// Token: 0x0200004D RID: 77
	public class IntervalCounter
	{
		// Token: 0x0600026D RID: 621 RVA: 0x00015A44 File Offset: 0x00013C44
		public IntervalCounter(int interval0)
		{
			if (interval0 < 0)
			{
				this.Next = (() => false);
				return;
			}
			if (interval0 == 0)
			{
				this.Next = (() => true);
				return;
			}
			this.Next = delegate()
			{
				if (this.nextCount++ <= interval0)
				{
					return false;
				}
				this.nextCount = 0;
				return true;
			};
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00015ADD File Offset: 0x00013CDD
		public void Reset()
		{
			this.nextCount = 0;
		}

		// Token: 0x04000326 RID: 806
		private int nextCount;

		// Token: 0x04000327 RID: 807
		public readonly Func<bool> Next;
	}
}
