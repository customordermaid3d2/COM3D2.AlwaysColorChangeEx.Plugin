using System;
using System.Reflection;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Util
{
	// Token: 0x0200005B RID: 91
	public class TexUtil
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00017D7C File Offset: 0x00015F7C
		public static TexUtil Instance
		{
			get
			{
				if (TexUtil._lazy != null)
				{
					return TexUtil._lazy;
				}
				lock (TexUtil.LOCK)
				{
					if (TexUtil._lazy == null)
					{
						TexUtil._lazy = new TexUtil();
					}
				}
				return TexUtil._lazy;
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00017DDC File Offset: 0x00015FDC
		private TexUtil()
		{
			Type typeFromHandle = typeof(ImportCM);
			try
			{
				MethodInfo method = typeFromHandle.GetMethod("LoadTexture", new Type[]
				{
					typeof(string)
				});
				if (method != null && method.ReturnType == typeof(byte[]))
				{
					this.LoadTex = (Func<string, byte[]>)Delegate.CreateDelegate(typeof(Func<string, byte[]>), method);
					LogUtil.Debug(new object[]
					{
						"using old mode (tex access API)"
					});
				}
			}
			catch (Exception ex)
			{
				LogUtil.Error(new object[]
				{
					"failed to initialize tex access API",
					ex
				});
			}
			if (this.LoadTex != null)
			{
				return;
			}
			MethodInfo method2 = typeFromHandle.GetMethod("CreateTexture", new Type[]
			{
				typeof(string)
			});
			if (method2 == null)
			{
				return;
			}
			if (method2.ReturnType == typeof(Texture2D))
			{
				this.CreateTex = (Func<string, Texture2D>)Delegate.CreateDelegate(typeof(Func<string, Texture2D>), method2);
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00017EF8 File Offset: 0x000160F8
		public Texture2D Load(string file)
		{
			if (this.LoadTex == null)
			{
				return this.CreateTex(file);
			}
			Texture2D texture2D = new Texture2D(1, 1, TextureFormat.RGBA32, false);
			texture2D.LoadImage(this.LoadTex(file));
			return texture2D;
		}

		// Token: 0x0400034E RID: 846
		private static volatile TexUtil _lazy;

		// Token: 0x0400034F RID: 847
		private static readonly object LOCK = new object();

		// Token: 0x04000350 RID: 848
		private Func<string, byte[]> LoadTex;

		// Token: 0x04000351 RID: 849
		private Func<string, Texture2D> CreateTex;
	}
}
