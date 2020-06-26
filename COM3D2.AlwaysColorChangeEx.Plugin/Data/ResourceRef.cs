using System;
using System.Collections.Generic;
using System.IO;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x0200001E RID: 30
	public class ResourceRef
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000CBB7 File Offset: 0x0000ADB7
		// (set) Token: 0x06000153 RID: 339 RVA: 0x0000CBBF File Offset: 0x0000ADBF
		public string key { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000CBC8 File Offset: 0x0000ADC8
		// (set) Token: 0x06000155 RID: 341 RVA: 0x0000CBD0 File Offset: 0x0000ADD0
		public string suffix { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000CBD9 File Offset: 0x0000ADD9
		// (set) Token: 0x06000157 RID: 343 RVA: 0x0000CBE1 File Offset: 0x0000ADE1
		public string filename { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000CBEA File Offset: 0x0000ADEA
		// (set) Token: 0x06000159 RID: 345 RVA: 0x0000CBF2 File Offset: 0x0000ADF2
		public string editname { get; set; }

		// Token: 0x0600015A RID: 346 RVA: 0x0000CBFB File Offset: 0x0000ADFB
		public ResourceRef(string key, string filename)
		{
			this.key = key;
			this.suffix = FileConst.GetResSuffix(key);
			this.filename = filename;
			this.editname = Path.GetFileNameWithoutExtension(filename);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000CC29 File Offset: 0x0000AE29
		public string EditFileName()
		{
			return this.editname + FileConst.EXT_MENU;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000CC3B File Offset: 0x0000AE3B
		public string EditTxtPath()
		{
			return Settings.Instance.txtPrefixMenu + this.editname + FileConst.EXT_TXT;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000CE04 File Offset: 0x0000B004
		public Func<string, string[], string[]> ReplaceMenuFunc()
		{
			this.replaceFiles = new List<ReplacedInfo>();
			return delegate(string key, string[] param)
			{
				if (key != null)
				{
					if (!(key == "additem"))
					{
						if (!(key == "マテリアル変更"))
						{
							return param;
						}
					}
					else
					{
						if (param.Length < 2)
						{
							return param;
						}
						string text = param[1];
						try
						{
							TBody.SlotID key2 = (TBody.SlotID)Enum.Parse(typeof(TBody.SlotID), text, false);
							Item item = this.menu.itemSlots[key2];
							if (item.needUpdate)
							{
								string text2 = param[0];
								string text3;
								if (item.filename == text2)
								{
									item.worksuffix = null;
									text3 = item.EditFileName();
								}
								else
								{
									item.worksuffix = this.suffix;
									text3 = item.EditFileName();
									this.replaceFiles.Add(new ReplacedInfo(text2, text3, this, item));
								}
								param[0] = text3;
							}
							return param;
						}
						catch (Exception ex)
						{
							LogUtil.Debug(new object[]
							{
								"failed to parse SlotID:",
								text,
								ex
							});
							return param;
						}
					}
					string slot = param[0];
					int matNo = int.Parse(param[1]);
					string text4 = param[2];
					TargetMaterial material = this.menu.GetMaterial(slot, matNo);
					material.worksuffix = null;
					if (material.filename == text4)
					{
						param[2] = material.EditFileName();
					}
					else if (material.shaderChanged || material.hasParamChanged || material.hasTexFileChanged || material.hasTexColorChanged)
					{
						material.worksuffix = this.suffix;
						string text5 = material.EditFileName();
						param[2] = text5;
						ReplacedInfo item2 = new ReplacedInfo(text4, text5, this, material);
						this.replaceFiles.Add(item2);
					}
				}
				return param;
			};
		}

		// Token: 0x0400014E RID: 334
		public ACCMenu menu;

		// Token: 0x0400014F RID: 335
		public ResourceRef link;

		// Token: 0x04000150 RID: 336
		public bool editfileExist;

		// Token: 0x04000151 RID: 337
		public List<ReplacedInfo> replaceFiles;
	}
}
