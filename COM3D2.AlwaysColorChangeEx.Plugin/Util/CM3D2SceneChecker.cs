using System;
using System.Collections.Generic;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Util
{
	// Token: 0x02000046 RID: 70
	public class CM3D2SceneChecker
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00013CCF File Offset: 0x00011ECF
		// (set) Token: 0x0600021F RID: 543 RVA: 0x00013CD7 File Offset: 0x00011ED7
		public Func<int, bool> IsTarget { get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00013CE0 File Offset: 0x00011EE0
		// (set) Token: 0x06000221 RID: 545 RVA: 0x00013CE8 File Offset: 0x00011EE8
		public Func<int, bool> IsStockTarget { get; private set; }

		// Token: 0x06000222 RID: 546 RVA: 0x00013D64 File Offset: 0x00011F64
		public CM3D2SceneChecker()
		{
			this.IsTarget = this._isTargetNormal;
			this.IsStockTarget = this._isStockNormal;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00013E1B File Offset: 0x0001201B
		public CM3D2SceneChecker.Mode GetMode()
		{
			return this._mode;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00013E24 File Offset: 0x00012024
		public void Init()
		{
			this.CheckMode();
			Settings instance = Settings.Instance;
			switch (this._mode)
			{
			case CM3D2SceneChecker.Mode.Normal:
				this.ApplySceneArray(ref CM3D2SceneChecker._sceneAvailables, instance.disableScenes, instance.enableScenes);
				return;
			case CM3D2SceneChecker.Mode.OH:
				this.ApplySceneArray(ref CM3D2SceneChecker._sceneOHAvailables, instance.disableOHScenes, instance.enableOHScenes);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00013E84 File Offset: 0x00012084
		private void ApplySceneArray(ref bool[] scenes, IList<int> disables, IList<int> enables)
		{
			int num = scenes.Length - 1;
			if (disables != null && num < disables[0])
			{
				num = disables[0];
			}
			if (enables != null && num < enables[0])
			{
				num = enables[0];
			}
			if (num <= scenes.Length - 1)
			{
				return;
			}
			bool[] array = new bool[num + 1];
			Array.Copy(scenes, array, scenes.Length);
			for (int i = scenes.Length; i < array.Length; i++)
			{
				array[i] = true;
			}
			scenes = array;
			if (disables != null)
			{
				foreach (int num2 in disables)
				{
					scenes[num2] = false;
				}
			}
			if (enables == null)
			{
				return;
			}
			foreach (int num3 in enables)
			{
				scenes[num3] = true;
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00013F7C File Offset: 0x0001217C
		public void CheckMode()
		{
			string dataPath = Application.dataPath;
			if (dataPath.StartsWith("CM3D2OH", StringComparison.OrdinalIgnoreCase))
			{
				this._mode = CM3D2SceneChecker.Mode.OH;
				this.IsTarget = this._isTargetOH;
				this.IsStockTarget = this._isStockOH;
				return;
			}
			this._mode = CM3D2SceneChecker.Mode.Normal;
			this.IsTarget = this._isTargetNormal;
			this.IsStockTarget = this._isStockNormal;
		}

		// Token: 0x0400030F RID: 783
		private const bool DEFAULT_VAL = true;

		// Token: 0x04000310 RID: 784
		private static bool[] _sceneAvailables = new bool[]
		{
			false,
			false,
			true,
			true,
			true,
			true,
			false,
			false,
			true,
			false,
			false,
			false,
			true,
			false,
			true,
			true,
			true,
			false,
			false,
			false,
			true,
			false,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true
		};

		// Token: 0x04000311 RID: 785
		private static bool[] _sceneOHAvailables = new bool[]
		{
			false,
			false,
			true,
			true,
			true,
			true,
			false,
			false,
			true,
			false,
			true,
			true,
			true,
			false,
			true,
			true,
			true,
			false,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true
		};

		// Token: 0x04000312 RID: 786
		private readonly Func<int, bool> _isTargetNormal = (int level) => CM3D2SceneChecker._sceneAvailables.Length <= level || CM3D2SceneChecker._sceneAvailables[level];

		// Token: 0x04000313 RID: 787
		private readonly Func<int, bool> _isTargetOH = (int level) => CM3D2SceneChecker._sceneOHAvailables.Length <= level || CM3D2SceneChecker._sceneOHAvailables[level];

		// Token: 0x04000314 RID: 788
		private readonly Func<int, bool> _isStockNormal = delegate(int level)
		{
			switch (level)
			{
			case 3:
			case 5:
				break;
			case 4:
				return false;
			default:
				if (level != 27)
				{
					return false;
				}
				break;
			}
			return true;
		};

		// Token: 0x04000315 RID: 789
		private readonly Func<int, bool> _isStockOH = (int level) => level == 4 || level == 21;

		// Token: 0x04000316 RID: 790
		private CM3D2SceneChecker.Mode _mode;

		// Token: 0x02000047 RID: 71
		public enum Mode
		{
			// Token: 0x0400031E RID: 798
			Normal,
			// Token: 0x0400031F RID: 799
			OH
		}
	}
}
