using System;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.UI.Helper
{
	// Token: 0x02000066 RID: 102
	public class GUIColorStore
	{
		// Token: 0x06000310 RID: 784 RVA: 0x00018D88 File Offset: 0x00016F88
		public void SetColor(Color contentColor, Color? backgroundColor)
		{
			this._backgroundColor = new Color?(GUI.backgroundColor);
			this._contentColor = new Color?(GUI.contentColor);
			if (backgroundColor != null)
			{
				GUI.backgroundColor = backgroundColor.Value;
			}
			GUI.contentColor = contentColor;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00018DC5 File Offset: 0x00016FC5
		public void SetColor(ref Color contentColor, ref Color? backgroundColor)
		{
			this._backgroundColor = new Color?(GUI.backgroundColor);
			this._contentColor = new Color?(GUI.contentColor);
			if (backgroundColor != null)
			{
				GUI.backgroundColor = backgroundColor.Value;
			}
			GUI.contentColor = contentColor;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00018E05 File Offset: 0x00017005
		public void SetColor(ref Color contentColor, ref Color backgroundColor)
		{
			this._backgroundColor = new Color?(GUI.backgroundColor);
			this._contentColor = new Color?(GUI.contentColor);
			GUI.backgroundColor = backgroundColor;
			GUI.contentColor = contentColor;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00018E3D File Offset: 0x0001703D
		public void SetBGColor(ref Color? backgroundColor)
		{
			if (backgroundColor == null)
			{
				return;
			}
			this._backgroundColor = new Color?(GUI.backgroundColor);
			GUI.backgroundColor = backgroundColor.Value;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00018E63 File Offset: 0x00017063
		public void SetBGColor(ref Color backgroundColor)
		{
			this._backgroundColor = new Color?(GUI.backgroundColor);
			GUI.backgroundColor = backgroundColor;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00018E80 File Offset: 0x00017080
		public void SetContentColor(ref Color contentColor)
		{
			this._contentColor = new Color?(GUI.contentColor);
			GUI.contentColor = contentColor;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00018EA0 File Offset: 0x000170A0
		public void Restore()
		{
			if (this._backgroundColor != null)
			{
				GUI.backgroundColor = this._backgroundColor.Value;
				this._backgroundColor = null;
			}
			if (this._contentColor == null)
			{
				return;
			}
			GUI.contentColor = this._contentColor.Value;
			this._contentColor = null;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00018F00 File Offset: 0x00017100
		public static GUIColorStore Default
		{
			get
			{
				return GUIColorStore.INSTANCE;
			}
		}

		// Token: 0x04000388 RID: 904
		private Color? _backgroundColor;

		// Token: 0x04000389 RID: 905
		private Color? _contentColor;

		// Token: 0x0400038A RID: 906
		private static readonly GUIColorStore INSTANCE = new GUIColorStore();
	}
}
