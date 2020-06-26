using System;
using System.Collections.Generic;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.UI
{
	// Token: 0x02000074 RID: 116
	public class UIParams
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0002176C File Offset: 0x0001F96C
		public static UIParams Instance
		{
			get
			{
				return UIParams.INSTANCE;
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00021774 File Offset: 0x0001F974
		public UIParams()
		{
			this.listStyle.onHover.background = (this.listStyle.hover.background = new Texture2D(2, 2));
			this.listStyle.padding.left = (this.listStyle.padding.right = 4);
			this.listStyle.padding.top = (this.listStyle.padding.bottom = 1);
			this.listStyle.normal.textColor = (this.listStyle.onNormal.textColor = (this.listStyle.hover.textColor = (this.listStyle.onHover.textColor = (this.listStyle.active.textColor = (this.listStyle.onActive.textColor = Color.white)))));
			this.listStyle.focused.textColor = (this.listStyle.onFocused.textColor = Color.blue);
			TextAnchor alignment = TextAnchor.MiddleLeft;
			this.lStyleB.fontStyle = FontStyle.Bold;
			this.lStyleB.alignment = alignment;
			this.lStyle.fontStyle = FontStyle.Normal;
			this.lStyle.normal.textColor = this.textColor;
			this.lStyle.alignment = alignment;
			this.lStyle.wordWrap = false;
			this.lStyleS.fontStyle = FontStyle.Normal;
			this.lStyleS.normal.textColor = this.textColor;
			this.lStyleS.alignment = alignment;
			this.lStyleRS.fontStyle = FontStyle.Normal;
			this.lStyleRS.normal.textColor = this.textColor;
			this.lStyleRS.alignment = TextAnchor.MiddleRight;
			this.lStyleC.fontStyle = FontStyle.Normal;
			this.lStyleC.normal.textColor = new Color(0.82f, 0.88f, 1f, 0.98f);
			this.lStyleC.alignment = alignment;
			this.bStyle.normal.textColor = this.textColor;
			this.bStyleSC.normal.textColor = this.textColor;
			this.bStyleSC.alignment = TextAnchor.MiddleCenter;
			this.bStyleL.normal.textColor = this.textColor;
			this.bStyleL.alignment = TextAnchor.MiddleLeft;
			this.tStyle.normal.textColor = this.textColor;
			this.tStyleS.normal.textColor = this.textColor;
			this.tStyleS.alignment = TextAnchor.LowerLeft;
			this.tStyleSS.normal.textColor = this.textColor;
			this.tStyleSS.alignment = TextAnchor.MiddleLeft;
			this.tStyleL.normal.textColor = this.textColor;
			this.tStyleL.alignment = alignment;
			this.textStyle.normal.textColor = this.textColor;
			this.textStyleSC.normal.textColor = this.textColor;
			this.textStyleSC.alignment = TextAnchor.MiddleCenter;
			this.textAreaStyleS.normal.textColor = this.textColor;
			this.winStyle.alignment = TextAnchor.UpperRight;
			this.dialogStyle.alignment = TextAnchor.UpperCenter;
			this.dialogStyle.normal.textColor = this.textColor;
			this.tipsStyle.alignment = TextAnchor.MiddleCenter;
			this.tipsStyle.wordWrap = true;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00021D44 File Offset: 0x0001FF44
		public void Update()
		{
			bool flag = false;
			if (Screen.height != this.height)
			{
				this.height = Screen.height;
				flag = true;
			}
			if (Screen.width != this.width)
			{
				this.width = Screen.width;
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			this.ratio = 1f + ((float)this.width / 1280f - 1f) * 0.6f;
			this.fontSize = this.FixPx(14);
			this.fontSizeS = this.FixPx(12);
			this.fontSizeSS = this.FixPx(11);
			this.fontSizeL = this.FixPx(20);
			this.margin = this.FixPx(2);
			this.marginL = this.FixPx(10);
			this.itemHeight = this.FixPx(18);
			this.unitHeight = this.margin + this.itemHeight;
			this.lStyle.fontSize = this.fontSize;
			this.lStyleC.fontSize = this.fontSize;
			this.lStyleB.fontSize = this.fontSize;
			this.lStyleS.fontSize = this.fontSizeS;
			this.lStyleRS.fontSize = this.fontSizeS;
			this.bStyle.fontSize = this.fontSize;
			this.bStyleSC.fontSize = this.fontSizeS;
			this.bStyleL.fontSize = this.fontSize;
			this.tStyle.fontSize = this.fontSize;
			this.tStyleS.fontSize = this.fontSizeS;
			this.tStyleSS.fontSize = this.fontSizeSS;
			this.tStyleL.fontSize = this.fontSizeL;
			this.listStyle.fontSize = this.fontSizeS;
			this.textStyle.fontSize = this.fontSize;
			this.textStyleSC.fontSize = this.fontSizeS;
			this.textAreaStyleS.fontSize = this.fontSizeS;
			LogUtil.DebugF("screen=({0},{1}),margin={2},height={3},ratio={4})", new object[]
			{
				this.width,
				this.height,
				this.margin,
				this.itemHeight,
				this.ratio
			});
			this.winStyle.fontSize = this.fontSize;
			this.tipsStyle.fontSize = this.fontSize;
			this.dialogStyle.fontSize = this.fontSize;
			this.InitWinRect();
			this.InitFBRect();
			this.InitModalRect();
			this.subConWidth = this.winRect.width - (float)(this.margin * 2);
			this.optBtnHeight = GUILayout.Height((float)this.itemHeight);
			this.optSubConWidth = GUILayout.Width(this.subConWidth);
			this.optSubConHeight = GUILayout.Height(this.winRect.height - (float)this.unitHeight * 3f);
			this.optSubCon6Height = GUILayout.Height(this.winRect.height - (float)this.unitHeight * 6.6f);
			this.optSubConHalfWidth = GUILayout.Width((this.winRect.width - (float)(this.marginL * 2)) * 0.5f);
			this.optToggleSWidth = GUILayout.Width(this.tStyleS.CalcSize(new GUIContent("ＸＸＸＸＸ")).x);
			this.mainRect.Set((float)this.margin, (float)(this.unitHeight * 5 + this.margin), this.winRect.width - (float)(this.margin * 2), this.winRect.height - (float)this.unitHeight * 6.5f);
			this.textureRect.Set((float)this.margin, (float)this.unitHeight, this.winRect.width - (float)(this.margin * 2), this.winRect.height - (float)this.unitHeight * 2.5f);
			float num = this.subConWidth - 20f;
			this.optBtnWidth = GUILayout.Width(num * 0.09f);
			this.optDBtnWidth = GUILayout.Width(this.bStyle.CalcSize(new GUIContent("ＸＸ")).x);
			this.optContentWidth = GUILayout.MaxWidth(num * 0.69f);
			this.optCategoryWidth = GUILayout.MaxWidth(this.lStyleS.CalcSize(new GUIContent("xxxxxxxxxxxx")).x);
			this.nodeSelectRect.Set((float)this.margin, (float)(this.unitHeight * 2), this.winRect.width - (float)(this.margin * 2), this.winRect.height - (float)this.unitHeight * 4.5f);
			this.colorRect.Set((float)this.margin, (float)(this.unitHeight * 2), this.winRect.width - (float)this.margin, this.winRect.height - (float)(this.unitHeight * 5));
			this.labelRect.Set(0f, 0f, this.winRect.width - (float)(this.margin * 2), (float)this.itemHeight * 1.2f);
			this.subRect.Set(0f, (float)this.itemHeight, this.winRect.width - (float)(this.margin * 2), (float)this.itemHeight);
			foreach (Action<UIParams> action in this.updaters)
			{
				action(this);
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000222F4 File Offset: 0x000204F4
		public void InitWinRect()
		{
			this.winRect.Set((float)(this.width - this.FixPx(360)), (float)this.FixPx(48), (float)this.FixPx(350), (float)(this.height - this.FixPx(150)));
			this.titleBarRect.Set(0f, 0f, this.winRect.width, 24f);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0002236C File Offset: 0x0002056C
		public void InitFBRect()
		{
			this.fileBrowserRect.Set((float)(this.width - this.FixPx(620)), (float)this.FixPx(100), (float)this.FixPx(600), (float)this.FixPx(600));
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000223B8 File Offset: 0x000205B8
		public void InitModalRect()
		{
			this.modalRect.Set((float)(this.width / 2 - this.FixPx(300)), (float)(this.height / 2 - this.FixPx(300)), (float)this.FixPx(600), (float)this.FixPx(600));
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00022412 File Offset: 0x00020612
		public int FixPx(int px)
		{
			return (int)(this.ratio * (float)px);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0002241E File Offset: 0x0002061E
		public void Add(Action<UIParams> action)
		{
			action(this);
			this.updaters.Add(action);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00022433 File Offset: 0x00020633
		public bool Remove(Action<UIParams> action)
		{
			return this.updaters.Remove(action);
		}

		// Token: 0x0400044E RID: 1102
		private const int marginPx = 2;

		// Token: 0x0400044F RID: 1103
		private const int marginLPx = 10;

		// Token: 0x04000450 RID: 1104
		private const int itemHeightPx = 18;

		// Token: 0x04000451 RID: 1105
		private const int fontPx = 14;

		// Token: 0x04000452 RID: 1106
		private const int fontPxS = 12;

		// Token: 0x04000453 RID: 1107
		private const int fontPxSS = 11;

		// Token: 0x04000454 RID: 1108
		private const int fontPxL = 20;

		// Token: 0x04000455 RID: 1109
		private static readonly UIParams INSTANCE = new UIParams();

		// Token: 0x04000456 RID: 1110
		private int width;

		// Token: 0x04000457 RID: 1111
		private int height;

		// Token: 0x04000458 RID: 1112
		private float ratio;

		// Token: 0x04000459 RID: 1113
		public int margin;

		// Token: 0x0400045A RID: 1114
		public int marginL;

		// Token: 0x0400045B RID: 1115
		public int fontSize;

		// Token: 0x0400045C RID: 1116
		public int fontSizeS;

		// Token: 0x0400045D RID: 1117
		public int fontSizeSS;

		// Token: 0x0400045E RID: 1118
		public int fontSizeL;

		// Token: 0x0400045F RID: 1119
		public int itemHeight;

		// Token: 0x04000460 RID: 1120
		public int unitHeight;

		// Token: 0x04000461 RID: 1121
		public readonly GUIStyle lStyle = new GUIStyle("label");

		// Token: 0x04000462 RID: 1122
		public readonly GUIStyle lStyleB = new GUIStyle("label");

		// Token: 0x04000463 RID: 1123
		public readonly GUIStyle lStyleC = new GUIStyle("label");

		// Token: 0x04000464 RID: 1124
		public readonly GUIStyle lStyleS = new GUIStyle("label");

		// Token: 0x04000465 RID: 1125
		public readonly GUIStyle lStyleRS = new GUIStyle("label");

		// Token: 0x04000466 RID: 1126
		public readonly GUIStyle bStyle = new GUIStyle("button");

		// Token: 0x04000467 RID: 1127
		public readonly GUIStyle bStyleSC = new GUIStyle("button");

		// Token: 0x04000468 RID: 1128
		public readonly GUIStyle bStyleL = new GUIStyle("button");

		// Token: 0x04000469 RID: 1129
		public readonly GUIStyle tStyle = new GUIStyle("toggle");

		// Token: 0x0400046A RID: 1130
		public readonly GUIStyle tStyleS = new GUIStyle("toggle");

		// Token: 0x0400046B RID: 1131
		public readonly GUIStyle tStyleSS = new GUIStyle("toggle");

		// Token: 0x0400046C RID: 1132
		public readonly GUIStyle tStyleL = new GUIStyle("toggle");

		// Token: 0x0400046D RID: 1133
		public readonly GUIStyle listStyle = new GUIStyle();

		// Token: 0x0400046E RID: 1134
		public readonly GUIStyle textStyle = new GUIStyle("textField");

		// Token: 0x0400046F RID: 1135
		public readonly GUIStyle textStyleSC = new GUIStyle("textField");

		// Token: 0x04000470 RID: 1136
		public readonly GUIStyle textAreaStyleS = new GUIStyle("textArea");

		// Token: 0x04000471 RID: 1137
		public readonly GUIStyle boxStyle = new GUIStyle("box");

		// Token: 0x04000472 RID: 1138
		public readonly GUIStyle winStyle = new GUIStyle("box");

		// Token: 0x04000473 RID: 1139
		public readonly GUIStyle dialogStyle = new GUIStyle("box");

		// Token: 0x04000474 RID: 1140
		public readonly GUIStyle tipsStyle = new GUIStyle("window");

		// Token: 0x04000475 RID: 1141
		public readonly Color textColor = new Color(1f, 1f, 1f, 0.98f);

		// Token: 0x04000476 RID: 1142
		public Rect titleBarRect = default(Rect);

		// Token: 0x04000477 RID: 1143
		public Rect winRect = default(Rect);

		// Token: 0x04000478 RID: 1144
		public Rect fileBrowserRect = default(Rect);

		// Token: 0x04000479 RID: 1145
		public Rect modalRect = default(Rect);

		// Token: 0x0400047A RID: 1146
		public Rect mainRect = default(Rect);

		// Token: 0x0400047B RID: 1147
		public Rect colorRect = default(Rect);

		// Token: 0x0400047C RID: 1148
		public Rect nodeSelectRect = default(Rect);

		// Token: 0x0400047D RID: 1149
		public Rect presetSelectRect = default(Rect);

		// Token: 0x0400047E RID: 1150
		public Rect textureRect = default(Rect);

		// Token: 0x0400047F RID: 1151
		public Rect labelRect = default(Rect);

		// Token: 0x04000480 RID: 1152
		public Rect subRect = default(Rect);

		// Token: 0x04000481 RID: 1153
		public GUILayoutOption optBtnHeight;

		// Token: 0x04000482 RID: 1154
		public float subConWidth;

		// Token: 0x04000483 RID: 1155
		public GUILayoutOption optSubConWidth;

		// Token: 0x04000484 RID: 1156
		public GUILayoutOption optSubConHeight;

		// Token: 0x04000485 RID: 1157
		public GUILayoutOption optSubCon6Height;

		// Token: 0x04000486 RID: 1158
		public GUILayoutOption optSubConHalfWidth;

		// Token: 0x04000487 RID: 1159
		public GUILayoutOption optBtnWidth;

		// Token: 0x04000488 RID: 1160
		public GUILayoutOption optCategoryWidth;

		// Token: 0x04000489 RID: 1161
		public GUILayoutOption optDBtnWidth;

		// Token: 0x0400048A RID: 1162
		public GUILayoutOption optToggleSWidth;

		// Token: 0x0400048B RID: 1163
		public GUILayoutOption optContentWidth;

		// Token: 0x0400048C RID: 1164
		private readonly List<Action<UIParams>> updaters = new List<Action<UIParams>>();
	}
}
