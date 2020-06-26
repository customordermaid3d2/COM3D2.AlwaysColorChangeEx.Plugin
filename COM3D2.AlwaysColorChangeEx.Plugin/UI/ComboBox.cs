using System;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.UI
{
	// Token: 0x02000072 RID: 114
	public class ComboBox : ComboBoxBase
	{
		// Token: 0x060003C6 RID: 966 RVA: 0x00021477 File Offset: 0x0001F677
		public ComboBox(Rect rect, GUIContent buttonContent, GUIContent[] listContent, GUIStyle listStyle) : base(buttonContent, listContent, listStyle)
		{
			this.rect = rect;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0002148A File Offset: 0x0001F68A
		public ComboBox(Rect rect, GUIContent buttonContent, GUIContent[] listContent, GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle) : base(buttonContent, listContent, buttonStyle, boxStyle, listStyle)
		{
			this.rect = rect;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000214A4 File Offset: 0x0001F6A4
		public int Show()
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
			if (GUI.Button(this.rect, this.buttonContent, this.buttonStyle))
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
				Rect position = new Rect(this.rect.x, this.rect.y + this.itemHeight, this.rect.width, this.itemHeight * (float)this.listContent.Length);
				GUI.Box(position, string.Empty, this.boxStyle);
				int num = GUI.SelectionGrid(position, this.selectedItemIndex, this.listContent, 1, this.listStyle);
				if (num != this.selectedItemIndex)
				{
					base.SelectedItemIndex = num;
				}
			}
			this.isClickedComboButton &= !flag;
			return this.selectedItemIndex;
		}

		// Token: 0x0400044C RID: 1100
		public Rect rect;
	}
}
