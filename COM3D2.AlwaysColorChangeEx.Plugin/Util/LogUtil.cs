using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Util
{
	// Token: 0x0200004E RID: 78
	public static class LogUtil
	{
		// Token: 0x06000271 RID: 625 RVA: 0x00015AE6 File Offset: 0x00013CE6
		public static bool IsDebug()
		{
			return false;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00015AE9 File Offset: 0x00013CE9
		public static void DebugF(string format, params object[] message)
		{
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00015AEB File Offset: 0x00013CEB
		public static void Debug(params object[] message)
		{
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00015AED File Offset: 0x00013CED
		public static void Debug(Func<string> func)
		{
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00015AF0 File Offset: 0x00013CF0
		public static string LogF(string format, params object[] message)
		{
			string text = string.Format(format, message);
			UnityEngine.Debug.Log(text);
			return text;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00015B0C File Offset: 0x00013D0C
		public static StringBuilder Log(params object[] message)
		{
			StringBuilder stringBuilder = LogUtil.CreateMessage(message, null);
			UnityEngine.Debug.Log(stringBuilder);
			return stringBuilder;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00015B28 File Offset: 0x00013D28
		public static string ErrorF(string format, params object[] message)
		{
			string text = string.Format(format, message);
			UnityEngine.Debug.LogError(text);
			return text;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00015B44 File Offset: 0x00013D44
		public static StringBuilder Error(params object[] message)
		{
			StringBuilder stringBuilder = LogUtil.CreateMessage(message, null);
			UnityEngine.Debug.LogError(stringBuilder);
			return stringBuilder;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00015B60 File Offset: 0x00013D60
		private static StringBuilder CreateMessage(IEnumerable<object> message, string prefix = null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (prefix != null)
			{
				stringBuilder.Append(prefix);
			}
			stringBuilder.Append(AlwaysColorChangeEx.PluginName).Append(':');
			foreach (object obj in message)
			{
				if (obj is Exception)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append(obj);
			}
			return stringBuilder;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00015BE4 File Offset: 0x00013DE4
		private static StringBuilder CreateMessage(string message, string prefix = null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (prefix != null)
			{
				stringBuilder.Append(prefix);
			}
			stringBuilder.Append(AlwaysColorChangeEx.PluginName).Append(':');
			stringBuilder.Append(message);
			return stringBuilder;
		}
	}
}
