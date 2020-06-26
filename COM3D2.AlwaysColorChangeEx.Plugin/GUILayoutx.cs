using System;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin
{
	// Token: 0x0200000A RID: 10
	public static class GUILayoutx
	{
		// Token: 0x0600005E RID: 94 RVA: 0x000070B0 File Offset: 0x000052B0
		public static int SelectionList(int selected, GUIContent[] list, GUIStyle elementStyle, GUILayoutx.ClickCallback callback = null)
		{
			for (int i = 0; i < list.Length; i++)
			{
				Rect rect = GUILayoutUtility.GetRect(list[i], elementStyle);
				bool flag = rect.Contains(Event.current.mousePosition);
				if (flag && Event.current.type == EventType.MouseDown)
				{
					selected = i;
					if (callback != null)
					{
						callback(i);
					}
					Event.current.Use();
				}
				else if (Event.current.type == EventType.Repaint)
				{
					elementStyle.Draw(rect, list[i], flag, false, i == selected, false);
				}
			}
			return selected;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00007130 File Offset: 0x00005330
		public static int SelectionList(int selected, string[] list, GUIStyle elementStyle, GUILayoutx.ClickCallback callback = null)
		{
			elementStyle.active.textColor = new Color(0.8f, 1f, 1f);
			for (int i = 0; i < list.Length; i++)
			{
				Rect rect = GUILayoutUtility.GetRect(new GUIContent(list[i]), elementStyle);
				bool flag = rect.Contains(Event.current.mousePosition);
				if (flag && Event.current.type == EventType.MouseDown)
				{
					selected = i;
					if (callback != null)
					{
						callback(i);
					}
					Event.current.Use();
				}
				else if (Event.current.type == EventType.Repaint)
				{
					elementStyle.Draw(rect, list[i], flag, false, i == selected, false);
				}
			}
			return selected;
		}

		// Token: 0x0200000B RID: 11
		// (Invoke) Token: 0x06000061 RID: 97
		public delegate void ClickCallback(int index);
	}
}
