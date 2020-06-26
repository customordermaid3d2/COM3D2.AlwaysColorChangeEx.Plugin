using System;
using System.Collections.Generic;
using System.Linq;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.TexAnim
{
	// Token: 0x0200007D RID: 125
	public class TexAnimator : MonoBehaviour
	{
		// Token: 0x06000421 RID: 1057 RVA: 0x00023D1C File Offset: 0x00021F1C
		private void Start()
		{
			if (!this.targets.Any<AnimItem>())
			{
				this.ParseMaterials();
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00023D32 File Offset: 0x00021F32
		private void OnDestroy()
		{
			this.Clear();
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00023D3C File Offset: 0x00021F3C
		private void Update()
		{
			foreach (AnimItem animItem in this.targets)
			{
				animItem.Animate(Time.deltaTime);
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00023D94 File Offset: 0x00021F94
		public bool ParseMaterials()
		{
			if (this.targets.Any<AnimItem>())
			{
				this.Clear();
			}
			Material[] materials = this.GetMaterials();
			bool flag = false;
			if (materials != null)
			{
				int num = 0;
				foreach (Material mate in materials)
				{
					flag |= this.ParseMaterial(mate, num++);
				}
			}
			return flag;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00023DF0 File Offset: 0x00021FF0
		public bool ParseMaterials(IEnumerable<MenuFileHandler.MateInfo> mis)
		{
			Material[] materials = this.GetMaterials();
			bool flag = false;
			foreach (MenuFileHandler.MateInfo mateInfo in mis)
			{
				if (materials != null && mateInfo.matNo < materials.Length)
				{
					flag |= this.ParseMaterial(materials[mateInfo.matNo], mateInfo.matNo);
				}
			}
			return flag;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00023E60 File Offset: 0x00022060
		private bool ParseMaterial(Material mate, int matNo)
		{
			AnimTex[] array = ParseAnimUtil.ParseAnimTex(mate);
			if (array != null)
			{
				AnimItem target = this.GetTarget(matNo);
				if (target != null)
				{
					target.UpdateTexes(mate, array);
				}
				else
				{
					this.targets.Add(new AnimItem(mate, matNo, array));
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00023ED0 File Offset: 0x000220D0
		private Material[] GetMaterials()
		{
			Renderer[] componentsInChildren = base.gameObject.transform.GetComponentsInChildren<Renderer>(true);
			Renderer renderer = componentsInChildren.FirstOrDefault((Renderer r) => r.material != null && r.materials.Length > 0 && r.material.shader != null);
			if (!(renderer == null))
			{
				return renderer.materials;
			}
			return null;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00023F24 File Offset: 0x00022124
		private AnimItem GetTarget(int matNo)
		{
			foreach (AnimItem animItem in this.targets)
			{
				if (animItem.matNo == matNo)
				{
					return animItem;
				}
			}
			return null;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00023F80 File Offset: 0x00022180
		public void RemoveTarget(int matNo)
		{
			AnimItem target = this.GetTarget(matNo);
			if (target != null)
			{
				target.Deactivate();
				this.targets.Remove(target);
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00023FAB File Offset: 0x000221AB
		public void SetTargets(List<AnimItem> items)
		{
			if (this.targets.Any<AnimItem>())
			{
				this.Clear();
			}
			this.targets.AddRange(items);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00023FCC File Offset: 0x000221CC
		public void Clear()
		{
			foreach (AnimItem animItem in this.targets)
			{
				animItem.Deactivate();
			}
			this.targets.Clear();
		}

		// Token: 0x040004BD RID: 1213
		private readonly List<AnimItem> targets = new List<AnimItem>();
	}
}
