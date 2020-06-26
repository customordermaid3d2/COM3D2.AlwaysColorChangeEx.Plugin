using System;
using System.IO;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x0200001C RID: 28
	public class TargetTexture
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000C98A File Offset: 0x0000AB8A
		// (set) Token: 0x0600013E RID: 318 RVA: 0x0000C992 File Offset: 0x0000AB92
		public bool colorChanged { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000C99B File Offset: 0x0000AB9B
		// (set) Token: 0x06000140 RID: 320 RVA: 0x0000C9A3 File Offset: 0x0000ABA3
		public bool fileChanged { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000C9AC File Offset: 0x0000ABAC
		// (set) Token: 0x06000142 RID: 322 RVA: 0x0000C9B4 File Offset: 0x0000ABB4
		public bool needOutput { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000C9BD File Offset: 0x0000ABBD
		// (set) Token: 0x06000144 RID: 324 RVA: 0x0000C9C5 File Offset: 0x0000ABC5
		public Texture tex { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000C9CE File Offset: 0x0000ABCE
		// (set) Token: 0x06000146 RID: 326 RVA: 0x0000C9D6 File Offset: 0x0000ABD6
		public TextureModifier.FilterParam filter { get; set; }

		// Token: 0x06000147 RID: 327 RVA: 0x0000C9E0 File Offset: 0x0000ABE0
		public TargetTexture(bool color, bool file, Texture tex)
		{
			this.colorChanged = color;
			this.fileChanged = file;
			this.tex = tex;
			if (tex == null)
			{
				return;
			}
			this.editname = Path.GetFileNameWithoutExtension(tex.name);
			this.needOutput |= this.colorChanged;
			if (this.needOutput || !this.fileChanged)
			{
				return;
			}
			string text = tex.name;
			if (!text.Contains("."))
			{
				text += FileConst.EXT_TEXTURE;
			}
			this.needOutput |= !FileUtilEx.Instance.Exists(text);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000CA82 File Offset: 0x0000AC82
		public string EditFileNameNoExt()
		{
			if (this.worksuffix == null)
			{
				return this.editname;
			}
			return this.editname + this.worksuffix;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000CAA4 File Offset: 0x0000ACA4
		public string EditFileName()
		{
			if (this.editname.EndsWith(FileConst.EXT_TEXTURE, StringComparison.OrdinalIgnoreCase))
			{
				if (this.worksuffix == null)
				{
					return this.editname;
				}
				return this.editname.Substring(0, this.editname.Length - 4) + this.worksuffix + FileConst.EXT_TEXTURE;
			}
			else
			{
				if (this.worksuffix == null)
				{
					return this.editname + FileConst.EXT_TEXTURE;
				}
				return this.editname + this.worksuffix + FileConst.EXT_TEXTURE;
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000CB2C File Offset: 0x0000AD2C
		public string EditTxtPath()
		{
			if (this.worksuffix == null)
			{
				return Settings.Instance.txtPrefixTex + this.editname + FileConst.EXT_TXT;
			}
			return Settings.Instance.txtPrefixTex + this.editname + this.worksuffix + FileConst.EXT_TXT;
		}

		// Token: 0x04000141 RID: 321
		public string editname;

		// Token: 0x04000142 RID: 322
		public bool editnameExist;

		// Token: 0x04000143 RID: 323
		public string worksuffix;

		// Token: 0x04000144 RID: 324
		public string workfilename;
	}
}
