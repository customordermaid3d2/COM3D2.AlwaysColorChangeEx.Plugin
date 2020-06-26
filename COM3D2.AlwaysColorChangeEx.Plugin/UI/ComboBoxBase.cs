using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.UI
{
	// Token: 0x02000071 RID: 113
	public abstract class ComboBoxBase
	{
		// Token: 0x060003BB RID: 955 RVA: 0x0002128A File Offset: 0x0001F48A
		protected ComboBoxBase(GUIContent buttonContent, GUIContent[] listContent, GUIStyle listStyle) : this(buttonContent, listContent, "button", "box", listStyle)
		{
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000212A9 File Offset: 0x0001F4A9
		protected ComboBoxBase(GUIContent buttonContent, GUIContent[] listContent, GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle)
		{
			this.buttonContent = buttonContent;
			this.listContent = listContent;
			this.buttonStyle = buttonStyle;
			this.boxStyle = boxStyle;
			this.listStyle = listStyle;
			this.InitIndex();
			this.InitSize();
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000212F0 File Offset: 0x0001F4F0
		protected void InitSize()
		{
			IEnumerable<int> first = from c in this.listContent
			select c.text.Length;
			int[] second = new int[1];
			int num = first.Concat(second).Max();
			this.itemWidth = (float)num * 9f;
			this.itemHeight = this.listStyle.CalcHeight(this.listContent[0], 1f);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00021364 File Offset: 0x0001F564
		protected void InitIndex()
		{
			for (int i = 0; i < this.listContent.Length; i++)
			{
				if (!(this.buttonContent.text != this.listContent[i].text))
				{
					this.selectedItemIndex = i;
					return;
				}
			}
			this.selectedItemIndex = -1;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000213B4 File Offset: 0x0001F5B4
		public int SelectItem(string item)
		{
			string b = item.ToLower();
			for (int i = 0; i < this.listContent.Length; i++)
			{
				if (!(this.listContent[i].text.ToLower() != b))
				{
					this.selectedItemIndex = i;
					return i;
				}
			}
			return -1;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x000213FF File Offset: 0x0001F5FF
		public bool IsClickedComboButton
		{
			get
			{
				return this.isClickedComboButton;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x00021407 File Offset: 0x0001F607
		public int ItemCount
		{
			get
			{
				return this.listContent.Length;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00021411 File Offset: 0x0001F611
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x0002141C File Offset: 0x0001F61C
		public int SelectedItemIndex
		{
			get
			{
				return this.selectedItemIndex;
			}
			set
			{
				if (this.selectedItemIndex == value)
				{
					return;
				}
				if (value < this.listContent.Length && value >= 0)
				{
					this.selectedItemIndex = value;
					this.buttonContent = this.listContent[this.selectedItemIndex];
					return;
				}
				this.buttonContent = GUIContent.none;
				this.selectedItemIndex = -1;
			}
		}

		// Token: 0x04000440 RID: 1088
		protected static bool forceToUnShow;

		// Token: 0x04000441 RID: 1089
		protected static int useControlID = -1;

		// Token: 0x04000442 RID: 1090
		protected bool isClickedComboButton;

		// Token: 0x04000443 RID: 1091
		protected int selectedItemIndex;

		// Token: 0x04000444 RID: 1092
		protected float itemWidth;

		// Token: 0x04000445 RID: 1093
		protected float itemHeight;

		// Token: 0x04000446 RID: 1094
		protected GUIContent buttonContent;

		// Token: 0x04000447 RID: 1095
		protected GUIContent[] listContent;

		// Token: 0x04000448 RID: 1096
		protected GUIStyle buttonStyle;

		// Token: 0x04000449 RID: 1097
		protected GUIStyle boxStyle;

		// Token: 0x0400044A RID: 1098
		protected GUIStyle listStyle;
	}
}
