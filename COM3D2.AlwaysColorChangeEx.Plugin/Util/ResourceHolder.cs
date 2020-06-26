using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Util
{
	// Token: 0x0200005A RID: 90
	public sealed class ResourceHolder
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x00017854 File Offset: 0x00015A54
		private ResourceHolder()
		{
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00017874 File Offset: 0x00015A74
		public Texture2D PictImage
		{
			get
			{
				if (!this.pictImage)
				{
					return this.pictImage = this.LoadTex("picture");
				}
				return this.pictImage;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x000178AC File Offset: 0x00015AAC
		public Texture2D FileImage
		{
			get
			{
				if (!this.fileImage)
				{
					return this.fileImage = this.LoadTex("file");
				}
				return this.dirImage;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x000178E4 File Offset: 0x00015AE4
		public Texture2D DirImage
		{
			get
			{
				if (!this.dirImage)
				{
					return this.dirImage = this.LoadTex("folder");
				}
				return this.dirImage;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0001791C File Offset: 0x00015B1C
		public Texture2D CopyImage
		{
			get
			{
				if (!this.copyImage)
				{
					return this.copyImage = this.LoadTex("copy");
				}
				return this.copyImage;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x00017954 File Offset: 0x00015B54
		public Texture2D PasteImage
		{
			get
			{
				if (!this.pasteImage)
				{
					return this.pasteImage = this.LoadTex("paste");
				}
				return this.pasteImage;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0001798C File Offset: 0x00015B8C
		public Texture2D PlusImage
		{
			get
			{
				if (!this.plusImage)
				{
					return this.plusImage = this.LoadTex("plus");
				}
				return this.plusImage;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002DB RID: 731 RVA: 0x000179C4 File Offset: 0x00015BC4
		public Texture2D MinusImage
		{
			get
			{
				if (!this.minusImage)
				{
					return this.minusImage = this.LoadTex("minus");
				}
				return this.minusImage;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002DC RID: 732 RVA: 0x000179FC File Offset: 0x00015BFC
		public Texture2D CheckonImage
		{
			get
			{
				if (!this.checkonImage)
				{
					return this.checkonImage = this.LoadTex("checkon");
				}
				return this.checkonImage;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00017A34 File Offset: 0x00015C34
		public Texture2D CheckoffImage
		{
			get
			{
				if (!this.checkoffImage)
				{
					return this.checkoffImage = this.LoadTex("checkoff");
				}
				return this.checkoffImage;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00017A6C File Offset: 0x00015C6C
		public GUIContent Checkon
		{
			get
			{
				GUIContent result;
				if ((result = this.checkon) == null)
				{
					result = (this.checkon = new GUIContent(this.CheckonImage));
				}
				return result;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00017A98 File Offset: 0x00015C98
		public GUIContent Checkoff
		{
			get
			{
				GUIContent result;
				if ((result = this.checkoff) == null)
				{
					result = (this.checkoff = new GUIContent(this.CheckoffImage));
				}
				return result;
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00017AC4 File Offset: 0x00015CC4
		public Texture2D LoadTex(string name)
		{
			Texture2D result;
			try
			{
				using (Stream manifestResourceStream = this.asmbl.GetManifestResourceStream(name + ".png"))
				{
					Texture2D texture2D = this.fileUtil.LoadTexture(manifestResourceStream);
					texture2D.name = name;
					LogUtil.Debug(new object[]
					{
						"resource file image loaded :",
						name
					});
					result = texture2D;
				}
			}
			catch (Exception ex)
			{
				LogUtil.Log(new object[]
				{
					"アイコンリソースのロードに失敗しました。空として扱います",
					name,
					ex
				});
				result = new Texture2D(2, 2);
			}
			return result;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00017B74 File Offset: 0x00015D74
		internal byte[] LoadBytes(string path)
		{
			try
			{
				byte[] array = new byte[8192];
				using (Stream manifestResourceStream = this.asmbl.GetManifestResourceStream(path))
				{
					if (manifestResourceStream != null)
					{
						using (MemoryStream memoryStream = new MemoryStream((int)manifestResourceStream.Length))
						{
							int count;
							while ((count = manifestResourceStream.Read(array, 0, array.Length)) > 0)
							{
								memoryStream.Write(array, 0, count);
							}
							return memoryStream.ToArray();
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogUtil.Log(new object[]
				{
					"リソースのロードに失敗しました。path=",
					path,
					ex
				});
				throw;
			}
			return new byte[0];
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00017C40 File Offset: 0x00015E40
		public void Clear()
		{
			if (this.pictImage != null)
			{
				UnityEngine.Object.DestroyImmediate(this.pictImage);
			}
			if (this.dirImage != null)
			{
				UnityEngine.Object.DestroyImmediate(this.dirImage);
			}
			if (this.fileImage != null)
			{
				UnityEngine.Object.DestroyImmediate(this.fileImage);
			}
			if (this.copyImage != null)
			{
				UnityEngine.Object.DestroyImmediate(this.copyImage);
			}
			if (this.pasteImage != null)
			{
				UnityEngine.Object.DestroyImmediate(this.pasteImage);
			}
			if (this.plusImage != null)
			{
				UnityEngine.Object.DestroyImmediate(this.plusImage);
			}
			if (this.minusImage != null)
			{
				UnityEngine.Object.DestroyImmediate(this.minusImage);
			}
			if (this.checkonImage != null)
			{
				UnityEngine.Object.DestroyImmediate(this.checkonImage);
			}
			if (this.checkoffImage != null)
			{
				UnityEngine.Object.DestroyImmediate(this.checkoffImage);
			}
			this.pictImage = null;
			this.dirImage = null;
			this.fileImage = null;
			this.copyImage = null;
			this.pasteImage = null;
			this.plusImage = null;
			this.minusImage = null;
			this.checkonImage = null;
			this.checkoffImage = null;
		}

		// Token: 0x04000340 RID: 832
		public static readonly ResourceHolder Instance = new ResourceHolder();

		// Token: 0x04000341 RID: 833
		private readonly FileUtilEx fileUtil = FileUtilEx.Instance;

		// Token: 0x04000342 RID: 834
		private readonly Assembly asmbl = Assembly.GetExecutingAssembly();

		// Token: 0x04000343 RID: 835
		private Texture2D dirImage;

		// Token: 0x04000344 RID: 836
		private Texture2D fileImage;

		// Token: 0x04000345 RID: 837
		private Texture2D pictImage;

		// Token: 0x04000346 RID: 838
		private Texture2D copyImage;

		// Token: 0x04000347 RID: 839
		private Texture2D pasteImage;

		// Token: 0x04000348 RID: 840
		private Texture2D plusImage;

		// Token: 0x04000349 RID: 841
		private Texture2D minusImage;

		// Token: 0x0400034A RID: 842
		private Texture2D checkonImage;

		// Token: 0x0400034B RID: 843
		private Texture2D checkoffImage;

		// Token: 0x0400034C RID: 844
		private GUIContent checkon;

		// Token: 0x0400034D RID: 845
		private GUIContent checkoff;
	}
}
