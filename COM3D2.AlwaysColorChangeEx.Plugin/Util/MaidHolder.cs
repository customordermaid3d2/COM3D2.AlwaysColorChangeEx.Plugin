using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CM3D2.AlwaysColorChangeEx.Plugin.Data;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Util
{
	// Token: 0x02000052 RID: 82
	public sealed class MaidHolder
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00016044 File Offset: 0x00014244
		// (set) Token: 0x0600028A RID: 650 RVA: 0x0001604C File Offset: 0x0001424C
		public string MaidName { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00016055 File Offset: 0x00014255
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0001605D File Offset: 0x0001425D
		public Maid CurrentMaid { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00016066 File Offset: 0x00014266
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0001606E File Offset: 0x0001426E
		public SlotInfo CurrentSlot { get; set; }

		// Token: 0x0600028F RID: 655 RVA: 0x00016078 File Offset: 0x00014278
		private MaidHolder()
		{
			this.MaidName = string.Empty;
			int num = PrivateAccessor.Get<int>(typeof(TBody), "strSlotNameItemCnt");
			if (num <= 0)
			{
				num = 3;
			}
			this._slotCount = TBody.m_strDefSlotName.Length / num;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000160CC File Offset: 0x000142CC
		public bool Applicable()
		{
			return this.CurrentMaid != null && !this.CurrentMaid.boAllProcPropBUSY;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000160EC File Offset: 0x000142EC
		public bool CurrentActivated()
		{
			return this.CurrentMaid != null && this.CurrentMaid.isActiveAndEnabled;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00016109 File Offset: 0x00014309
		public bool CurrentEnabled()
		{
			return this.CurrentMaid != null && this.CurrentMaid.enabled;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00016126 File Offset: 0x00014326
		public bool CheckOfficial(Maid maid)
		{
			return maid.body0.goSlot.Count == this._slotCount;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00016140 File Offset: 0x00014340
		public bool UpdateMaid(Maid maid0, string name, Action act)
		{
			if (maid0 == null)
			{
				int maidCount = GameMain.Instance.CharacterMgr.GetMaidCount();
				for (int i = 0; i < maidCount; i++)
				{
					Maid maid = GameMain.Instance.CharacterMgr.GetMaid(i);
					if (!(maid == null) && maid.enabled)
					{
						maid0 = maid;
						break;
					}
				}
			}
			if (this.CurrentMaid == maid0)
			{
				return false;
			}
			this.CurrentMaid = maid0;
			if (this.CurrentMaid != null)
			{
				this.MaidName = (name ?? MaidHelper.GetName(this.CurrentMaid));
				this.isOfficial = this.CheckOfficial(this.CurrentMaid);
			}
			else
			{
				this.MaidName = "(not selected)";
			}
			LogUtil.Debug(new object[]
			{
				"maid changed.",
				this.MaidName
			});
			act();
			return true;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00016218 File Offset: 0x00014418
		public bool UpdateMaid(Action act)
		{
			return this.UpdateMaid(null, null, act);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00016224 File Offset: 0x00014424
		public string GetCurrentMenuFile()
		{
			if (this.CurrentMaid == null)
			{
				return null;
			}
			MaidProp prop = this.CurrentMaid.GetProp(this.CurrentSlot.mpn);
			if (prop != null)
			{
				return prop.strFileName;
			}
			LogUtil.Log(new object[]
			{
				"maid prop is null",
				this.CurrentSlot.mpn
			});
			return null;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0001628C File Offset: 0x0001448C
		public int GetCurrentMenuFileID()
		{
			if (this.CurrentMaid == null)
			{
				return 0;
			}
			MaidProp prop = this.CurrentMaid.GetProp(this.CurrentSlot.mpn);
			if (prop != null)
			{
				return prop.nFileNameRID;
			}
			LogUtil.Log(new object[]
			{
				"maid prop is null",
				this.CurrentSlot.mpn
			});
			return 0;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000162F4 File Offset: 0x000144F4
		public TBodySkin GetCurrentSlot()
		{
			return this.CurrentMaid.body0.GetSlot((int)this.CurrentSlot.Id);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00016311 File Offset: 0x00014511
		public Material[] GetMaterials()
		{
			return this.GetMaterials(this.CurrentSlot.Id);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00016324 File Offset: 0x00014524
		public Material[] GetMaterials(SlotInfo slot)
		{
			return this.GetMaterials(slot.Id);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00016334 File Offset: 0x00014534
		public Material[] GetMaterials(TBody.SlotID slotID)
		{
			if (slotID < (TBody.SlotID)this.CurrentMaid.body0.goSlot.Count)
			{
				return this.GetMaterials(this.CurrentMaid.body0.GetSlot((int)slotID));
			}
			return this._emptyList;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00016379 File Offset: 0x00014579
		public Material[] GetMaterials(string slotName)
		{
			return this.GetMaterials(this.CurrentMaid.body0.GetSlot(slotName));
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00016394 File Offset: 0x00014594
		public Renderer GetRenderer(TBody.SlotID slotID)
		{
			if (slotID < (TBody.SlotID)this.CurrentMaid.body0.goSlot.Count)
			{
				return this.GetRenderer(this.CurrentMaid.body0.GetSlot((int)slotID));
			}
			return null;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x000163D4 File Offset: 0x000145D4
		public Material[] GetMaterials(TBodySkin slot)
		{
			Renderer renderer = this.GetRenderer(slot);
			if (!(renderer == null))
			{
				return renderer.materials;
			}
			return this._emptyList;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000163FF File Offset: 0x000145FF
		public Material GetMaterial(int matNo)
		{
			return this.GetMaterial(this.CurrentMaid.body0.GetSlot((int)this.CurrentSlot.Id), matNo);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00016424 File Offset: 0x00014624
		public Material GetMaterial(TBodySkin slot, int matNo)
		{
			if (slot.obj == null)
			{
				return null;
			}
			Renderer renderer = this.GetRenderer(slot.obj, matNo);
			if (!(renderer != null))
			{
				return null;
			}
			return renderer.materials[matNo];
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00016464 File Offset: 0x00014664
		public Renderer GetRenderer(TBodySkin slot)
		{
			GameObject obj = slot.obj;
			if (!(obj == null))
			{
				return this.GetRenderer(obj, 0);
			}
			return null;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000164C8 File Offset: 0x000146C8
		private Renderer GetRenderer(GameObject gobj, int matNo)
		{
			Renderer[] componentsInChildren = gobj.transform.GetComponentsInChildren<Renderer>(true);
			return componentsInChildren.FirstOrDefault((Renderer r) => r.material != null && r.materials.Length > matNo && r.material.shader != null);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00016504 File Offset: 0x00014704
		public Material GetMaterial(string slotName, int matNo)
		{
			TBodySkin slot = this.CurrentMaid.body0.GetSlot(slotName);
			if (slot.obj == null)
			{
				return null;
			}
			Renderer renderer = this.GetRenderer(slot.obj, matNo);
			if (!(renderer != null))
			{
				return null;
			}
			return renderer.materials[matNo];
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00016554 File Offset: 0x00014754
		public void SetDelNodes(PresetData preset, bool bApply)
		{
			this.SetDelNodes(this.CurrentMaid, preset.delNodes, bApply);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00016569 File Offset: 0x00014769
		public void SetDelNodes(Maid maid, PresetData preset, bool bApply)
		{
			this.SetDelNodes(maid, preset.delNodes, bApply);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00016579 File Offset: 0x00014779
		public void SetDelNodes(Dictionary<string, bool> dDelNodes, bool bApply)
		{
			this.SetDelNodes(this.CurrentMaid, dDelNodes, bApply);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0001658C File Offset: 0x0001478C
		public void SetDelNodes(Maid maid, Dictionary<string, bool> dDelNodes, bool bApply)
		{
			if (!dDelNodes.Any<KeyValuePair<string, bool>>())
			{
				return;
			}
			foreach (KeyValuePair<string, bool> keyValuePair in dDelNodes)
			{
				NodeItem nodeItem = ACConstants.NodeNames[keyValuePair.Key];
				if (keyValuePair.Value)
				{
					using (List<TBodySkin>.Enumerator enumerator2 = maid.body0.goSlot.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							TBodySkin tbodySkin = enumerator2.Current;
							if (!(tbodySkin.obj == null) && tbodySkin.m_dicDelNodeBody.ContainsKey(keyValuePair.Key))
							{
								tbodySkin.m_dicDelNodeBody[keyValuePair.Key] = true;
							}
						}
						continue;
					}
				}
				bool flag = false;
				foreach (TBody.SlotID f_nSlotNo in nodeItem.slots)
				{
					TBodySkin slot = maid.body0.GetSlot((int)f_nSlotNo);
					if (!(slot.obj == null))
					{
						if (slot.m_dicDelNodeBody.ContainsKey(keyValuePair.Key))
						{
							slot.m_dicDelNodeBody[keyValuePair.Key] = false;
						}
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					TBodySkin slot2 = maid.body0.GetSlot(0);
					if (!(slot2.obj == null) && slot2.m_dicDelNodeBody.ContainsKey(keyValuePair.Key))
					{
						slot2.m_dicDelNodeBody[keyValuePair.Key] = false;
					}
				}
			}
			if (bApply)
			{
				this.FixFlag();
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00016750 File Offset: 0x00014950
		public void SetDelNodesForce(Dictionary<string, bool> dDelNodes, bool bApply)
		{
			this.SetDelNodesForce(this.CurrentMaid, dDelNodes, bApply);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00016760 File Offset: 0x00014960
		public void SetDelNodesForce(Maid maid, Dictionary<string, bool> dDelNodes, bool bApply)
		{
			if (!dDelNodes.Any<KeyValuePair<string, bool>>())
			{
				return;
			}
			foreach (TBodySkin tbodySkin in maid.body0.goSlot)
			{
				if (!(tbodySkin.obj == null))
				{
					tbodySkin.boVisible = true;
					foreach (KeyValuePair<string, bool> keyValuePair in dDelNodes)
					{
						if (tbodySkin.m_dicDelNodeBody.ContainsKey(keyValuePair.Key))
						{
							tbodySkin.m_dicDelNodeBody[keyValuePair.Key] = keyValuePair.Value;
						}
					}
				}
			}
			if (bApply)
			{
				this.FixFlag();
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0001683C File Offset: 0x00014A3C
		private Hashtable GetMaskTable()
		{
			if (!(this.CurrentMaid == null))
			{
				return PrivateAccessor.Get<Hashtable>(this.CurrentMaid.body0, "m_hFoceHide");
			}
			return null;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00016864 File Offset: 0x00014A64
		public void SetSlotVisibles(Maid maid, Dictionary<TBody.SlotID, MaskInfo> maskDic, bool temporary)
		{
			foreach (KeyValuePair<TBody.SlotID, MaskInfo> keyValuePair in maskDic)
			{
				MaskInfo value = keyValuePair.Value;
				if (value.state != SlotState.NotLoaded)
				{
					value.slot.boVisible = value.value;
					if (!temporary)
					{
						TBodySkin slot = maid.body0.GetSlot((int)keyValuePair.Key);
						if (!value.value)
						{
							slot.listMaskSlot.Add((int)keyValuePair.Key);
						}
						else
						{
							foreach (TBodySkin tbodySkin in maid.body0.goSlot)
							{
								tbodySkin.listMaskSlot.Remove((int)keyValuePair.Key);
							}
						}
					}
				}
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00016960 File Offset: 0x00014B60
		public void SetMaskSlots(PresetData preset)
		{
			this.SetMaskSlots(this.CurrentMaid, preset.slots);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00016974 File Offset: 0x00014B74
		public void SetMaskSlots(Maid maid, PresetData preset)
		{
			this.SetMaskSlots(maid, preset.slots);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00016983 File Offset: 0x00014B83
		public void SetMaskSlots(List<CCSlot> slotList)
		{
			this.SetMaskSlots(this.CurrentMaid, slotList);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00016994 File Offset: 0x00014B94
		public void SetMaskSlots(Maid maid, List<CCSlot> slotList)
		{
			foreach (CCSlot ccslot in slotList)
			{
				if (ccslot.mask != SlotState.NotLoaded)
				{
					TBodySkin slot = maid.body0.GetSlot((int)ccslot.id);
					switch (ccslot.mask)
					{
					case SlotState.Displayed:
						foreach (TBodySkin tbodySkin in maid.body0.goSlot)
						{
							tbodySkin.listMaskSlot.Remove((int)ccslot.id);
						}
						break;
					case SlotState.Masked:
						slot.listMaskSlot.Add((int)ccslot.id);
						break;
					}
				}
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00016A84 File Offset: 0x00014C84
		public void SetAllVisible()
		{
			foreach (TBodySkin tbodySkin in this.CurrentMaid.body0.goSlot)
			{
				tbodySkin.boVisible = true;
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00016AE4 File Offset: 0x00014CE4
		public void ClearMasks()
		{
			foreach (TBodySkin tbodySkin in this.CurrentMaid.body0.goSlot)
			{
				tbodySkin.boVisible = true;
				tbodySkin.listMaskSlot.Clear();
			}
			this.FixFlag();
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00016B54 File Offset: 0x00014D54
		public void FixFlag()
		{
			this.FixFlag(this.CurrentMaid, false);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00016B63 File Offset: 0x00014D63
		public void FixFlag(Maid maid, bool propProp = false)
		{
			maid.body0.FixMaskFlag();
			maid.body0.FixVisibleFlag(false);
			if (propProp)
			{
				maid.AllProcPropSeqStart();
			}
		}

		// Token: 0x04000331 RID: 817
		public static readonly MaidHolder Instance = new MaidHolder();

		// Token: 0x04000332 RID: 818
		private readonly int _slotCount;

		// Token: 0x04000333 RID: 819
		private readonly Material[] _emptyList = new Material[0];

		// Token: 0x04000334 RID: 820
		public bool isOfficial;
	}
}
