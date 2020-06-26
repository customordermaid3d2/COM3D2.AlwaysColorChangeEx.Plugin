using System;
using System.Collections.Generic;
using System.Linq;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Util
{
	// Token: 0x0200004F RID: 79
	public class MaidChangeDetector
	{
		// Token: 0x0600027B RID: 635 RVA: 0x00015C20 File Offset: 0x00013E20
		public void Add(Action<Maid, MaidProp> notifier)
		{
			this.notifiers.Add(notifier);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00015C2E File Offset: 0x00013E2E
		public bool Remove(Action<Maid, MaidProp> notifier)
		{
			return this.notifiers.Remove(notifier);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00015C3C File Offset: 0x00013E3C
		public void Clear()
		{
			MaidChangeDetector.cache.Clear();
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00015C48 File Offset: 0x00013E48
		public void Detect(bool useStockMaid = false)
		{
			if (!this.notifiers.Any<Action<Maid, MaidProp>>())
			{
				return;
			}
			if (this.counter.Next())
			{
				this.DetectMaidTarget(useStockMaid);
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00015C71 File Offset: 0x00013E71
		private bool IsEnabled(Maid m)
		{
			return m.isActiveAndEnabled && m.Visible;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00015C84 File Offset: 0x00013E84
		public void DetectAllTarget(bool useStockMaid = false)
		{
			CharacterMgr characterMgr = GameMain.Instance.CharacterMgr;
			if (useStockMaid)
			{
				this.DetectTarget(new Func<int, Maid>(characterMgr.GetStockMaid), characterMgr.GetStockMaidCount());
			}
			else
			{
				this.DetectTarget(new Func<int, Maid>(characterMgr.GetMaid), characterMgr.GetMaidCount());
			}
			this.DetectTarget(new Func<int, Maid>(characterMgr.GetMan), characterMgr.GetManCount());
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00015CEC File Offset: 0x00013EEC
		public void DetectMaidTarget(bool useStockMaid)
		{
			CharacterMgr characterMgr = GameMain.Instance.CharacterMgr;
			if (useStockMaid)
			{
				this.DetectTarget(new Func<int, Maid>(characterMgr.GetStockMaid), characterMgr.GetStockMaidCount());
				return;
			}
			this.DetectTarget(new Func<int, Maid>(characterMgr.GetMaid), characterMgr.GetMaidCount());
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00015DA0 File Offset: 0x00013FA0
		private void DetectTarget(Func<int, Maid> GetMaid, int maidCount)
		{
			for (int i = 0; i < maidCount; i++)
			{
				Maid maid = GetMaid(i);
				if (!(maid == null) && !maid.IsAllProcPropBusy && MaidChangeDetector.SLOT_COUNT == maid.body0.goSlot.Count)
				{
					int instanceID = maid.GetInstanceID();
					MaidChangeDetector.MenuCache mc;
					if (!this.IsEnabled(maid))
					{
						MaidChangeDetector.cache.Remove(instanceID);
					}
					else if (!MaidChangeDetector.cache.TryGetValue(instanceID, out mc))
					{
						MaidProp[] array = PrivateAccessor.Get<MaidProp[]>(maid, "m_aryMaidProp");
						if (array != null)
						{
							mc = new MaidChangeDetector.MenuCache();
							mc.SetProps(array);
							MaidChangeDetector.cache[instanceID] = mc;
						}
					}
					else
					{
						for (int j = 43; j < mc.maidProps.Length; j++)
						{
							MaidProp maidProp = mc.maidProps[j];
							if (mc.menuIds[j] != maidProp.nFileNameRID)
							{
								int mpn1 = j;
								LogUtil.Debug(() => string.Concat(new object[]
								{
									"Item changed [",
									(MPN)mpn1,
									"] ",
									mc.maidProps[mpn1].strFileName
								}));
								mc.menuIds[j] = maidProp.nFileNameRID;
								foreach (Action<Maid, MaidProp> action in this.notifiers)
								{
									action(maid, maidProp);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00015F50 File Offset: 0x00014150
		static MaidChangeDetector()
		{
			int num = PrivateAccessor.Get<int>(typeof(TBody), "strSlotNameItemCnt");
			if (num <= 0)
			{
				num = 3;
			}
			MaidChangeDetector.SLOT_COUNT = TBody.m_strDefSlotName.Length / num;
		}

		// Token: 0x0400032A RID: 810
		private static readonly int SLOT_COUNT;

		// Token: 0x0400032B RID: 811
		private static readonly List<MPN> list = Enum.GetValues(typeof(MPN)).Cast<MPN>().ToList<MPN>();

		// Token: 0x0400032C RID: 812
		private static readonly Dictionary<int, MaidChangeDetector.MenuCache> cache = new Dictionary<int, MaidChangeDetector.MenuCache>();

		// Token: 0x0400032D RID: 813
		private readonly IntervalCounter counter = new IntervalCounter(60);

		// Token: 0x0400032E RID: 814
		private readonly List<Action<Maid, MaidProp>> notifiers = new List<Action<Maid, MaidProp>>();

		// Token: 0x02000050 RID: 80
		internal class MenuCache
		{
			// Token: 0x06000285 RID: 645 RVA: 0x00015FD0 File Offset: 0x000141D0
			public void SetProps(MaidProp[] props)
			{
				this.maidProps = props;
				for (int i = 43; i < this.maidProps.Length; i++)
				{
					this.menuIds[i] = this.maidProps[i].nFileNameRID;
				}
			}

			// Token: 0x0400032F RID: 815
			internal readonly int[] menuIds = new int[MaidChangeDetector.list.Count];

			// Token: 0x04000330 RID: 816
			internal MaidProp[] maidProps;
		}
	}
}
