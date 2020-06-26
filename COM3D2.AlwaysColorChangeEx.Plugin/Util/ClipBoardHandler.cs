using System;
using CM3D2.AlwaysColorChangeEx.Plugin.Data;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Util
{
	// Token: 0x02000045 RID: 69
	public class ClipBoardHandler
	{
		// Token: 0x06000216 RID: 534 RVA: 0x00013B64 File Offset: 0x00011D64
		private ClipBoardHandler()
		{
			if (!ClipboardCHelper.IsSupport())
			{
				LogUtil.Debug(new object[]
				{
					"ClipboardCHelper disabled. using direct GUIUtility.systemCopyBuffer"
				});
				this.GetClipboard = (() => GUIUtility.systemCopyBuffer);
				this.SetClipboard = delegate(string text)
				{
					GUIUtility.systemCopyBuffer = text;
				};
				return;
			}
			LogUtil.Debug(new object[]
			{
				"ClipboardCHelper enabled."
			});
			this.GetClipboard = (() => ClipboardCHelper.clipBoard);
			this.SetClipboard = delegate(string text)
			{
				ClipboardCHelper.clipBoard = text;
			};
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00013C38 File Offset: 0x00011E38
		public void Reload()
		{
			string text = this.GetClipboard();
			if (text.Length < 20 || text.Length > 3333)
			{
				this.mateText = null;
				this.isMateText = false;
				this.prevLength = 0;
				return;
			}
			if (this.prevLength == text.Length)
			{
				return;
			}
			this.prevLength = text.Length;
			this.mateText = text;
			this.isMateText = MateHandler.IsParsable(text);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00013CAC File Offset: 0x00011EAC
		public void Clear()
		{
			this.mateText = null;
			this.prevLength = 0;
			this.isMateText = false;
		}

		// Token: 0x04000303 RID: 771
		private const int MIN_LENGTH = 20;

		// Token: 0x04000304 RID: 772
		private const int MAX_LENGTH = 3333;

		// Token: 0x04000305 RID: 773
		public static readonly ClipBoardHandler Instance = new ClipBoardHandler();

		// Token: 0x04000306 RID: 774
		public string mateText;

		// Token: 0x04000307 RID: 775
		public bool isMateText;

		// Token: 0x04000308 RID: 776
		private int prevLength;

		// Token: 0x04000309 RID: 777
		public Func<string> GetClipboard;

		// Token: 0x0400030A RID: 778
		public Action<string> SetClipboard;
	}
}
