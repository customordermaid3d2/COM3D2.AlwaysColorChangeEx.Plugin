using System;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Util
{
	// Token: 0x0200005D RID: 93
	internal class UIHelper
	{
		// Token: 0x060002EB RID: 747 RVA: 0x00018098 File Offset: 0x00016298
		internal bool IsEnabledUICamera()
		{
			return UICamera.currentCamera != null && UICamera.currentCamera.enabled;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000180B3 File Offset: 0x000162B3
		internal void SetCameraControl(bool enable)
		{
			if (this.cmrCtrlChanged != enable)
			{
				return;
			}
			GameMain.Instance.MainCamera.SetControl(enable);
			UICamera.InputEnable = enable;
			this.cmrCtrlChanged = !enable;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000180DF File Offset: 0x000162DF
		internal void UpdateCameraControl(bool contains)
		{
			this.cursorContains = contains;
			if (this.cursorContains)
			{
				if (GameMain.Instance.MainCamera.GetControl())
				{
					this.SetCameraControl(false);
					return;
				}
			}
			else
			{
				this.SetCameraControl(true);
			}
		}

		// Token: 0x0400035C RID: 860
		internal bool cmrCtrlChanged;

		// Token: 0x0400035D RID: 861
		internal bool cursorContains;
	}
}
