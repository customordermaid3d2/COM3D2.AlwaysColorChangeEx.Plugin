using System;
using System.Reflection;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Util
{
	// Token: 0x02000044 RID: 68
	public static class ClipboardCHelper
	{
		// Token: 0x06000211 RID: 529 RVA: 0x00013ADD File Offset: 0x00011CDD
		static ClipboardCHelper()
		{
			ClipboardCHelper.Init();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00013AE4 File Offset: 0x00011CE4
		public static bool IsSupport()
		{
			return ClipboardCHelper.copyBufferProperty != null;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00013AF1 File Offset: 0x00011CF1
		// (set) Token: 0x06000214 RID: 532 RVA: 0x00013B04 File Offset: 0x00011D04
		public static string clipBoard
		{
			get
			{
				return (string)ClipboardCHelper.copyBufferProperty.GetValue(null, null);
			}
			set
			{
				ClipboardCHelper.copyBufferProperty.SetValue(null, value, null);
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00013B14 File Offset: 0x00011D14
		private static void Init()
		{
			if (ClipboardCHelper.copyBufferProperty != null)
			{
				return;
			}
			Type typeFromHandle = typeof(GUIUtility);
			ClipboardCHelper.copyBufferProperty = typeFromHandle.GetProperty("systemCopyBuffer", BindingFlags.Static | BindingFlags.NonPublic);
		}

		// Token: 0x04000302 RID: 770
		private static PropertyInfo copyBufferProperty;
	}
}
