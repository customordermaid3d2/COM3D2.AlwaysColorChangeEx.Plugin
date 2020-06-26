using System;
using System.Collections.Generic;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.TexAnim
{
	// Token: 0x02000077 RID: 119
	public class AnimTargetDetector
	{
		// Token: 0x060003F8 RID: 1016 RVA: 0x0002322C File Offset: 0x0002142C
		public void ChangeMenu(Maid maid, MaidProp prop)
		{
			if (prop.nFileNameRID == 0 || this.NoAnimMenuId.Contains(prop.nFileNameRID))
			{
				return;
			}
			List<MenuFileHandler.ChangeInfo> list = this.menuHandler.Parse(prop.strFileName);
			if (list == null)
			{
				return;
			}
			bool flag = false;
			foreach (MenuFileHandler.ChangeInfo changeInfo in list)
			{
				TBody.SlotID f_nSlotNo;
				if (EnumUtil.TryParse<TBody.SlotID>(changeInfo.slot, true, out f_nSlotNo))
				{
					TBodySkin slot = maid.body0.GetSlot((int)f_nSlotNo);
					if (slot != null && !(slot.obj == null))
					{
						TexAnimator texAnimator = slot.obj.transform.GetComponentInChildren<TexAnimator>(false);
						if (texAnimator == null)
						{
							Material[] materials = this.GetMaterials(slot);
							List<AnimItem> list2 = this.ParseMaidSlot(slot, materials, changeInfo.matInfos);
							if (list2 != null)
							{
								LogUtil.Debug(new object[]
								{
									"AddComponent for ",
									slot,
									", from ",
									prop.name
								});
								texAnimator = slot.obj.AddComponent<TexAnimator>();
								texAnimator.name = "TexAnimator";
								texAnimator.SetTargets(list2);
								flag = true;
							}
						}
						else if (changeInfo.matInfos == null)
						{
							flag |= texAnimator.ParseMaterials();
						}
						else
						{
							flag |= texAnimator.ParseMaterials(changeInfo.matInfos);
						}
					}
				}
			}
			if (!flag)
			{
				this.NoAnimMenuId.Add(prop.nFileNameRID);
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x000233C4 File Offset: 0x000215C4
		public List<AnimItem> ParseMaidSlot(TBodySkin slot, Material[] mates, IList<MenuFileHandler.MateInfo> miList)
		{
			if (mates == null)
			{
				return null;
			}
			List<AnimItem> list = null;
			try
			{
				if (miList == null)
				{
					int num = 0;
					foreach (Material material in mates)
					{
						AnimTex[] array = ParseAnimUtil.ParseAnimTex(material);
						if (array != null)
						{
							if (list == null)
							{
								list = new List<AnimItem>();
							}
							list.Add(new AnimItem(material, num, array));
						}
						num++;
					}
				}
				else
				{
					foreach (MenuFileHandler.MateInfo mateInfo in miList)
					{
						Material material2 = mates[mateInfo.matNo];
						AnimTex[] array2 = ParseAnimUtil.ParseAnimTex(material2);
						if (array2 != null)
						{
							if (list == null)
							{
								list = new List<AnimItem>();
							}
							list.Add(new AnimItem(material2, mateInfo.matNo, array2));
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogUtil.Debug(new object[]
				{
					"slotId:",
					slot,
					ex.Message
				});
			}
			return list;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x000234CC File Offset: 0x000216CC
		private Material[] GetMaterials(TBodySkin slot)
		{
			Renderer[] componentsInChildren = slot.obj.transform.GetComponentsInChildren<Renderer>(true);
			foreach (Renderer renderer in componentsInChildren)
			{
				if (renderer.material != null && renderer.materials.Length > 0 && renderer.material.shader != null)
				{
					return renderer.materials;
				}
			}
			return null;
		}

		// Token: 0x040004A3 RID: 1187
		private readonly HashSet<int> NoAnimMenuId = new HashSet<int>();

		// Token: 0x040004A4 RID: 1188
		private readonly MenuFileHandler menuHandler = new MenuFileHandler();
	}
}
