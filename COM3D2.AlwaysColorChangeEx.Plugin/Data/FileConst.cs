using System;
using System.Collections.Generic;
using System.IO;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x02000026 RID: 38
	public static class FileConst
	{
		// Token: 0x06000194 RID: 404 RVA: 0x0000E992 File Offset: 0x0000CB92
		public static bool HasInvalidChars(string filename)
		{
			return filename.IndexOfAny(FileConst.INVALID_FILENAMECHARS) > -1;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000E9A4 File Offset: 0x0000CBA4
		public static string GetResSuffix(string key)
		{
			string text;
			if (FileConst.SuffixDic.TryGetValue(key, out text))
			{
				return text;
			}
			text = FileConst.settings.resSuffix + ++FileConst.SuffixUnknownCount;
			FileConst.SuffixDic[key] = text;
			return text;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000E9F4 File Offset: 0x0000CBF4
		public static string GetTexSuffix(string propName)
		{
			string result;
			if (!FileConst.TexSuffix.TryGetValue(propName, out result))
			{
				return string.Empty;
			}
			return result;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000EA18 File Offset: 0x0000CC18
		public static string GetModelSuffix(string propName)
		{
			string result;
			if (!FileConst.modelSuffix.TryGetValue(propName, out result))
			{
				return string.Empty;
			}
			return result;
		}

		// Token: 0x04000186 RID: 390
		public static readonly string HEAD_MENU = "CM3D2_MENU";

		// Token: 0x04000187 RID: 391
		public static readonly string HEAD_MOD = "CM3D2_MOD";

		// Token: 0x04000188 RID: 392
		public static readonly string HEAD_MODEL = "CM3D2_MESH";

		// Token: 0x04000189 RID: 393
		public static readonly string HEAD_MATE = "CM3D2_MATERIAL";

		// Token: 0x0400018A RID: 394
		public static readonly string HEAD_TEX = "CM3D2_TEX";

		// Token: 0x0400018B RID: 395
		public static readonly string HEAD_PMAT = "CM3D2_PMATERIAL";

		// Token: 0x0400018C RID: 396
		public static readonly string RET = "《改行》";

		// Token: 0x0400018D RID: 397
		public static readonly string EXT_MOD = ".mod";

		// Token: 0x0400018E RID: 398
		public static readonly string EXT_MENU = ".menu";

		// Token: 0x0400018F RID: 399
		public static readonly string EXT_MATERIAL = ".mate";

		// Token: 0x04000190 RID: 400
		public static readonly string EXT_PMAT = ".pmat";

		// Token: 0x04000191 RID: 401
		public static readonly string EXT_MODEL = ".model";

		// Token: 0x04000192 RID: 402
		public static readonly string EXT_TEXTURE = ".tex";

		// Token: 0x04000193 RID: 403
		public static readonly string EXT_TXT = ".txt";

		// Token: 0x04000194 RID: 404
		public static readonly string EXT_JSON = ".json";

		// Token: 0x04000195 RID: 405
		private static readonly char[] INVALID_FILENAMECHARS = Path.GetInvalidFileNameChars();

		// Token: 0x04000196 RID: 406
		private static readonly Settings settings = Settings.Instance;

		// Token: 0x04000197 RID: 407
		public static readonly Dictionary<string, string> SuffixDic = new Dictionary<string, string>
		{
			{
				"パンツずらし",
				"_zurashi"
			},
			{
				"めくれスカート",
				"_mekure"
			},
			{
				"めくれスカート後ろ",
				"_mekure_back"
			},
			{
				"半脱ぎ",
				"_mekure_nugi"
			}
		};

		// Token: 0x04000198 RID: 408
		public static int SuffixUnknownCount;

		// Token: 0x04000199 RID: 409
		public static readonly Dictionary<string, string> TexSuffix = new Dictionary<string, string>
		{
			{
				"_MainTex",
				""
			},
			{
				"_ToonRamp",
				"_toon"
			},
			{
				"_ShadowTex",
				"_shadow"
			},
			{
				"_ShadowRateToon",
				"_rate"
			},
			{
				"_HiTex",
				"_s"
			},
			{
				"_OutlineTex",
				"_line"
			},
			{
				"_OutlineToonRamp",
				"_outoon"
			}
		};

		// Token: 0x0400019A RID: 410
		private static readonly Dictionary<string, string> modelSuffix = new Dictionary<string, string>
		{
			{
				TBody.SlotID.body.ToString(),
				"_body"
			},
			{
				TBody.SlotID.head.ToString(),
				"_head"
			},
			{
				TBody.SlotID.eye.ToString(),
				"_eye"
			},
			{
				TBody.SlotID.chikubi.ToString(),
				"_chikubi"
			},
			{
				TBody.SlotID.accHa.ToString(),
				"_accha"
			},
			{
				TBody.SlotID.hairF.ToString(),
				"_hairf"
			},
			{
				TBody.SlotID.hairR.ToString(),
				"_hairr"
			},
			{
				TBody.SlotID.hairS.ToString(),
				"_hairs"
			},
			{
				TBody.SlotID.hairT.ToString(),
				"_hairt"
			},
			{
				TBody.SlotID.hairAho.ToString(),
				"_haira"
			},
			{
				TBody.SlotID.underhair.ToString(),
				"_underh"
			},
			{
				TBody.SlotID.accHat.ToString(),
				"_acchat"
			},
			{
				TBody.SlotID.headset.ToString(),
				"_headset"
			},
			{
				TBody.SlotID.wear.ToString(),
				"_wear"
			},
			{
				TBody.SlotID.skirt.ToString(),
				"_skirt"
			},
			{
				TBody.SlotID.onepiece.ToString(),
				"_onep"
			},
			{
				TBody.SlotID.mizugi.ToString(),
				"_mizugi"
			},
			{
				TBody.SlotID.bra.ToString(),
				"_bra"
			},
			{
				TBody.SlotID.panz.ToString(),
				"_panz"
			},
			{
				TBody.SlotID.stkg.ToString(),
				"_stkg"
			},
			{
				TBody.SlotID.shoes.ToString(),
				"_shoe"
			},
			{
				TBody.SlotID.accKami_1_.ToString(),
				"_acckami1"
			},
			{
				TBody.SlotID.accKami_2_.ToString(),
				"_acckami2"
			},
			{
				TBody.SlotID.accKami_3_.ToString(),
				"_acckami3"
			},
			{
				TBody.SlotID.megane.ToString(),
				"_megane"
			},
			{
				TBody.SlotID.accHead.ToString(),
				"_acchead"
			},
			{
				TBody.SlotID.glove.ToString(),
				"_glove"
			},
			{
				TBody.SlotID.accHana.ToString(),
				"_acchana"
			},
			{
				TBody.SlotID.accMiMiL.ToString(),
				"_accmimil"
			},
			{
				TBody.SlotID.accMiMiR.ToString(),
				"_accmimir"
			},
			{
				TBody.SlotID.accKubi.ToString(),
				"_acckubi"
			},
			{
				TBody.SlotID.accKubiwa.ToString(),
				"_acckubiwa"
			},
			{
				TBody.SlotID.accKamiSubL.ToString(),
				"_acckamisl"
			},
			{
				TBody.SlotID.accKamiSubR.ToString(),
				"_acckamisr"
			},
			{
				TBody.SlotID.accNipL.ToString(),
				"_accnipl"
			},
			{
				TBody.SlotID.accNipR.ToString(),
				"_accnipr"
			},
			{
				TBody.SlotID.accUde.ToString(),
				"_accude"
			},
			{
				TBody.SlotID.accHeso.ToString(),
				"_accheso"
			},
			{
				TBody.SlotID.accAshi.ToString(),
				"_accashi"
			},
			{
				TBody.SlotID.accSenaka.ToString(),
				"_accsenaka"
			},
			{
				TBody.SlotID.accShippo.ToString(),
				"_accshippo"
			},
			{
				TBody.SlotID.accXXX.ToString(),
				"_accxxx"
			},
			{
				TBody.SlotID.seieki_naka.ToString(),
				"_snaka"
			},
			{
				TBody.SlotID.seieki_hara.ToString(),
				"_shara"
			},
			{
				TBody.SlotID.seieki_face.ToString(),
				"_sface"
			},
			{
				TBody.SlotID.seieki_mune.ToString(),
				"_smune"
			},
			{
				TBody.SlotID.seieki_hip.ToString(),
				"_ship"
			},
			{
				TBody.SlotID.seieki_ude.ToString(),
				"_sude"
			},
			{
				TBody.SlotID.seieki_ashi.ToString(),
				"_sashi"
			},
			{
				TBody.SlotID.HandItemL.ToString(),
				"_handl"
			},
			{
				TBody.SlotID.HandItemR.ToString(),
				"_handr"
			},
			{
				TBody.SlotID.kubiwa.ToString(),
				"_kubiwa"
			},
			{
				TBody.SlotID.kousoku_upper.ToString(),
				"_kousokuu"
			},
			{
				TBody.SlotID.kousoku_lower.ToString(),
				"_kousokul"
			},
			{
				TBody.SlotID.accAnl.ToString(),
				"_accanl"
			},
			{
				TBody.SlotID.accVag.ToString(),
				"_accvag"
			},
			{
				TBody.SlotID.chinko.ToString(),
				"_chinko"
			}
		};
	}
}
