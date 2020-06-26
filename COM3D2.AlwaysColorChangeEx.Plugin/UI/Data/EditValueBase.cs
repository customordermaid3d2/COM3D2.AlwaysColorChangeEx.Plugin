using System;
using System.Globalization;

namespace CM3D2.AlwaysColorChangeEx.Plugin.UI.Data
{
	// Token: 0x0200005F RID: 95
	public abstract class EditValueBase<T> where T : IComparable, IFormattable
	{
		// Token: 0x060002F7 RID: 759 RVA: 0x000184B9 File Offset: 0x000166B9
		protected EditValueBase(T val1, EditRange<T> attr)
		{
			this.range = attr;
			this.Set(val1);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x000184CF File Offset: 0x000166CF
		protected EditValueBase(T val1, string format, T min, T max)
		{
			this.range = new EditRange<T>(format, min, max);
			this.Set(val1);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000184ED File Offset: 0x000166ED
		public void Set(T val1)
		{
			this.val = val1;
			this.editVal = val1.ToString(this.range.format, NumberFormatInfo.CurrentInfo);
			this.isSync = true;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00018520 File Offset: 0x00016720
		public void SetWithCheck(T val1)
		{
			if (val1.CompareTo(this.range.editMin) == -1)
			{
				this.val = this.range.editMin;
			}
			else if (val1.CompareTo(this.range.editMax) == 1)
			{
				this.val = this.range.editMax;
			}
			else
			{
				this.val = val1;
			}
			this.editVal = this.val.ToString(this.range.format, NumberFormatInfo.CurrentInfo);
			this.isSync = true;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000185C8 File Offset: 0x000167C8
		public void Set(string editVal1)
		{
			this.editVal = editVal1;
			this.isSync = false;
			T t;
			if (this.TryParse(editVal1, out t))
			{
				if (t.CompareTo(this.range.editMin) == -1)
				{
					t = this.range.editMin;
				}
				else if (t.CompareTo(this.range.editMax) == 1)
				{
					t = this.range.editMax;
				}
				else
				{
					this.isSync = true;
				}
				this.val = t;
			}
		}

		// Token: 0x060002FC RID: 764
		protected abstract bool TryParse(string edit, out T v);

		// Token: 0x04000368 RID: 872
		public T val;

		// Token: 0x04000369 RID: 873
		public bool isSync;

		// Token: 0x0400036A RID: 874
		public string editVal;

		// Token: 0x0400036B RID: 875
		public readonly EditRange<T> range;
	}
}
