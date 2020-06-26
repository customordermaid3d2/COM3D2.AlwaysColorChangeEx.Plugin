using System;
using CM3D2.AlwaysColorChangeEx.Plugin.Data;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.UI.Data
{
	// Token: 0x0200005E RID: 94
	public class EditColor
	{
		// Token: 0x060002EF RID: 751 RVA: 0x00018118 File Offset: 0x00016318
		public EditColor(Color val1, ColorType type, EditRange<float> range, EditRange<float> range_a)
		{
			this.type = type;
			this.range = range;
			this.rangeA = range_a;
			this.Set(val1);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0001813D File Offset: 0x0001633D
		public EditColor(Color val1, ColorType type = ColorType.rgb, bool rangeOver = true)
		{
			this.type = type;
			this.range = (rangeOver ? EditColor.RANGE_2 : EditColor.RANGE);
			this.rangeA = EditColor.RANGE;
			this.Set(val1);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00018174 File Offset: 0x00016374
		private string[] ToEdit(ref Color c0)
		{
			switch (this.type)
			{
			case ColorType.rgba:
				return new string[]
				{
					c0.r.ToString(this.range.format),
					c0.g.ToString(this.range.format),
					c0.b.ToString(this.range.format),
					c0.a.ToString(this.rangeA.format)
				};
			case ColorType.rgb:
				return new string[]
				{
					c0.r.ToString(this.range.format),
					c0.g.ToString(this.range.format),
					c0.b.ToString(this.range.format)
				};
			case ColorType.a:
				return new string[]
				{
					c0.a.ToString(this.rangeA.format)
				};
			default:
				return EditColor.empty;
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00018288 File Offset: 0x00016488
		public void Set(Color val1)
		{
			this.val = val1;
			this.editVals = this.ToEdit(ref this.val);
			if (this.isSyncs == null)
			{
				this.isSyncs = new bool[this.editVals.Length];
			}
			for (int i = 0; i < this.isSyncs.Length; i++)
			{
				this.isSyncs[i] = true;
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000182E8 File Offset: 0x000164E8
		public float GetValue(int idx)
		{
			if (this.type == ColorType.a)
			{
				return this.val.a;
			}
			switch (idx)
			{
			case 0:
				return this.val.r;
			case 1:
				return this.val.g;
			case 2:
				return this.val.b;
			case 3:
				return this.val.a;
			default:
				return 0f;
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0001835C File Offset: 0x0001655C
		public EditRange<float> GetRange(int idx)
		{
			switch (this.type)
			{
			case ColorType.rgba:
				if (idx != 3)
				{
					return this.range;
				}
				return this.rangeA;
			case ColorType.rgb:
				return this.range;
			}
			return this.rangeA;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x000183A4 File Offset: 0x000165A4
		public void Set(int idx, string editVal1, EditRange<float> er = null)
		{
			if (idx >= this.editVals.Length)
			{
				return;
			}
			this.editVals[idx] = editVal1;
			if (er == null)
			{
				er = this.GetRange(idx);
			}
			bool flag = false;
			float num;
			if (float.TryParse(editVal1, out num))
			{
				if (er.editMin > num)
				{
					num = er.editMin;
				}
				else if (er.editMax < num)
				{
					num = er.editMax;
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					if (this.type == ColorType.a)
					{
						this.val.a = num;
					}
					else
					{
						switch (idx)
						{
						case 0:
							this.val.r = num;
							break;
						case 1:
							this.val.g = num;
							break;
						case 2:
							this.val.b = num;
							break;
						case 3:
							this.val.a = num;
							break;
						}
					}
				}
			}
			this.isSyncs[idx] = flag;
		}

		// Token: 0x0400035E RID: 862
		internal static readonly EditRange<float> RANGE_2 = new EditRange<float>("F3", 0f, 2f);

		// Token: 0x0400035F RID: 863
		internal static readonly EditRange<float> RANGE = new EditRange<float>("F3", 0f, 1f);

		// Token: 0x04000360 RID: 864
		private static readonly string[] empty = new string[0];

		// Token: 0x04000361 RID: 865
		private readonly EditRange<float> range;

		// Token: 0x04000362 RID: 866
		private readonly EditRange<float> rangeA;

		// Token: 0x04000363 RID: 867
		public bool hasAlpha;

		// Token: 0x04000364 RID: 868
		public Color val;

		// Token: 0x04000365 RID: 869
		public readonly ColorType type;

		// Token: 0x04000366 RID: 870
		public bool[] isSyncs;

		// Token: 0x04000367 RID: 871
		public string[] editVals;
	}
}
