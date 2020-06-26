using System;
using CM3D2.AlwaysColorChangeEx.Plugin.UI.Data;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;
using UnityEngine;
using UnityEngine.Rendering;

namespace CM3D2.AlwaysColorChangeEx.Plugin.UI.Helper
{
	// Token: 0x02000065 RID: 101
	public class CheckboxHelper
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00018924 File Offset: 0x00016B24
		private static GUIContent[] CompareFuncs
		{
			get
			{
				if (CheckboxHelper.compareFuncs != null)
				{
					return CheckboxHelper.compareFuncs;
				}
				string[] names = Enum.GetNames(typeof(CompareFunction));
				CheckboxHelper.compareFuncs = new GUIContent[names.Length];
				int num = 0;
				foreach (string text in names)
				{
					CheckboxHelper.compareFuncs[num++] = new GUIContent(text);
				}
				return CheckboxHelper.compareFuncs;
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0001898C File Offset: 0x00016B8C
		public CheckboxHelper(UIParams uiparams)
		{
			this.uiParams = uiparams;
			this.uiParams.Add(new Action<UIParams>(this.updateUI));
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000189E8 File Offset: 0x00016BE8
		~CheckboxHelper()
		{
			this.uiParams.Remove(new Action<UIParams>(this.updateUI));
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00018A28 File Offset: 0x00016C28
		private void updateUI(UIParams uiparams)
		{
			this.optItemHeight = GUILayout.Height((float)uiparams.itemHeight);
			this.bStyleLeft.fontStyle = uiparams.lStyleC.fontStyle;
			this.bStyleLeft.fontSize = uiparams.fontSize;
			this.bStyleLeft.normal.textColor = uiparams.lStyleC.normal.textColor;
			this.bStyleLeft.alignment = TextAnchor.MiddleLeft;
			this.bStyleCenter.fontStyle = uiparams.lStyleC.fontStyle;
			this.bStyleCenter.fontSize = uiparams.fontSize;
			this.bStyleCenter.normal.textColor = uiparams.lStyleC.normal.textColor;
			this.bStyleCenter.alignment = TextAnchor.MiddleCenter;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00018AF0 File Offset: 0x00016CF0
		internal void ShowComboBox(string label, EditValue edit, Action<int> func)
		{
			int num = (int)edit.val;
			if (this.ShowComboBox(label, CheckboxHelper.CompareFuncs, ref this.compareCombo, ref num, func))
			{
				edit.Set((float)num);
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00018B24 File Offset: 0x00016D24
		internal bool ShowComboBox(string label, GUIContent[] items, ref ComboBoxLO combo, ref int idx, Action<int> func)
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			bool result;
			try
			{
				GUILayout.Label(label, this.uiParams.lStyle, new GUILayoutOption[]
				{
					this.optItemHeight
				});
				GUILayout.Space((float)this.uiParams.marginL);
				if (combo == null)
				{
					GUIContent buttonContent = (idx >= 0 && idx < items.Length) ? items[idx] : GUIContent.none;
					combo = new ComboBoxLO(buttonContent, items, this.uiParams.bStyleSC, this.uiParams.boxStyle, this.uiParams.listStyle, false);
				}
				else
				{
					combo.SelectedItemIndex = idx;
				}
				combo.Show(GUILayout.ExpandWidth(true));
				int selectedItemIndex = combo.SelectedItemIndex;
				if (idx == selectedItemIndex || selectedItemIndex == -1)
				{
					result = false;
				}
				else
				{
					idx = selectedItemIndex;
					func(selectedItemIndex);
					result = true;
				}
			}
			finally
			{
				GUILayout.EndHorizontal();
			}
			return result;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00018C10 File Offset: 0x00016E10
		internal void ShowCheckBox(string label, EditValue edit, Action<float> func)
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			try
			{
				GUILayout.Label(label, this.uiParams.lStyle, new GUILayoutOption[]
				{
					this.optItemHeight
				});
				GUILayout.Space((float)this.uiParams.marginL);
				float num = edit.val;
				GUIContent content = NumberUtil.Equals(num, 0f, 0.001f) ? ResourceHolder.Instance.Checkoff : ResourceHolder.Instance.Checkon;
				if (GUILayout.Button(content, this.bStyleCenter, new GUILayoutOption[]
				{
					GUILayout.Width(50f)
				}))
				{
					num = 1f - num;
					edit.Set(num);
					func(num);
				}
			}
			finally
			{
				GUILayout.EndHorizontal();
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00018CE0 File Offset: 0x00016EE0
		internal bool ShowCheckBox(string label, ref bool val, Action<bool> func)
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			bool result;
			try
			{
				GUILayout.Label(label, this.uiParams.lStyle, new GUILayoutOption[]
				{
					this.optItemHeight
				});
				GUIContent content = val ? ResourceHolder.Instance.Checkon : ResourceHolder.Instance.Checkoff;
				if (!GUILayout.Button(content, this.bStyleCenter, new GUILayoutOption[]
				{
					GUILayout.Width(50f)
				}))
				{
					result = false;
				}
				else
				{
					val = !val;
					func(val);
					result = true;
				}
			}
			finally
			{
				GUILayout.EndHorizontal();
			}
			return result;
		}

		// Token: 0x04000382 RID: 898
		private static GUIContent[] compareFuncs;

		// Token: 0x04000383 RID: 899
		private readonly UIParams uiParams;

		// Token: 0x04000384 RID: 900
		private readonly GUIStyle bStyleLeft = new GUIStyle("label");

		// Token: 0x04000385 RID: 901
		private readonly GUIStyle bStyleCenter = new GUIStyle("label");

		// Token: 0x04000386 RID: 902
		private GUILayoutOption optItemHeight;

		// Token: 0x04000387 RID: 903
		public ComboBoxLO compareCombo;
	}
}
