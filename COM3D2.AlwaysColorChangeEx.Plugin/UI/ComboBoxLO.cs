using System;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.UI
{
	// Token: 0x02000073 RID: 115
	public class ComboBoxLO : ComboBoxBase
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x000215CB File Offset: 0x0001F7CB
		public ComboBoxLO(GUIContent buttonContent, GUIContent[] listContent, GUIStyle listStyle) : base(buttonContent, listContent, listStyle)
		{
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000215D6 File Offset: 0x0001F7D6
		public ComboBoxLO(GUIContent buttonContent, GUIContent[] listContent, GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle, bool labelFixed) : base(buttonContent, listContent, buttonStyle, boxStyle, listStyle)
		{
			this.labelFixed = labelFixed;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000215ED File Offset: 0x0001F7ED
		public void SetItemWidth(float width)
		{
			this.itemWidth = width;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000215F8 File Offset: 0x0001F7F8
		public int Show(GUILayoutOption buttonOpt)
		{
			if (ComboBoxBase.forceToUnShow)
			{
				ComboBoxBase.forceToUnShow = false;
				this.isClickedComboButton = false;
			}
			bool flag = false;
			int controlID = GUIUtility.GetControlID(FocusType.Passive);
			EventType typeForControl = Event.current.GetTypeForControl(controlID);
			if (typeForControl == EventType.MouseUp)
			{
				flag |= this.isClickedComboButton;
			}
			bool isClickedComboButton = this.isClickedComboButton;
			if (isClickedComboButton)
			{
				GUILayout.BeginVertical(this.boxStyle, new GUILayoutOption[]
				{
					GUILayout.Width(this.itemWidth)
				});
			}
			try
			{
				if (GUILayout.Button(this.buttonContent, this.buttonStyle, new GUILayoutOption[]
				{
					buttonOpt
				}))
				{
					if (ComboBoxBase.useControlID == -1)
					{
						ComboBoxBase.useControlID = controlID;
						this.isClickedComboButton = false;
					}
					if (ComboBoxBase.useControlID != controlID)
					{
						ComboBoxBase.forceToUnShow = true;
						ComboBoxBase.useControlID = controlID;
					}
					this.isClickedComboButton = true;
				}
				if (this.isClickedComboButton)
				{
					float height = this.itemHeight * (float)this.listContent.Length;
					int num = GUILayout.SelectionGrid(this.selectedItemIndex, this.listContent, 1, this.listStyle, new GUILayoutOption[]
					{
						GUILayout.Width(this.itemWidth),
						GUILayout.Height(height)
					});
					if (num != this.selectedItemIndex)
					{
						if (!this.labelFixed)
						{
							base.SelectedItemIndex = num;
						}
						else
						{
							this.selectedItemIndex = num;
						}
					}
				}
			}
			finally
			{
				if (isClickedComboButton)
				{
					GUILayout.EndVertical();
				}
			}
			this.isClickedComboButton &= !flag;
			return this.selectedItemIndex;
		}

		// Token: 0x0400044D RID: 1101
		private readonly bool labelFixed;
	}
}
