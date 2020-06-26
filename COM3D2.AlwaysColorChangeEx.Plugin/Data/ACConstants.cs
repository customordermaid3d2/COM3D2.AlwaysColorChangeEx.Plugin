﻿using System;
using System.Collections.Generic;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x02000021 RID: 33
	public static class ACConstants
	{
		// Token: 0x04000163 RID: 355
		private static readonly Settings settings = Settings.Instance;

		// Token: 0x04000164 RID: 356
		public static readonly Dictionary<TBody.SlotID, SlotInfo> SlotNames = new Dictionary<TBody.SlotID, SlotInfo>(Enum.GetNames(typeof(TBody.SlotID)).Length)
		{
			{
				TBody.SlotID.body,
				new SlotInfo(TBody.SlotID.body, MPN.MayuThick, "身体", true, ACConstants.settings.enableMask)
			},
			{
				TBody.SlotID.head,
				new SlotInfo(TBody.SlotID.head, MPN.Yorime, "頭", true, ACConstants.settings.enableMask)
			},
			{
				TBody.SlotID.eye,
				new SlotInfo(TBody.SlotID.eye, MPN.hairt, "目", true, ACConstants.settings.enableMask)
			},
			{
				TBody.SlotID.chikubi,
				new SlotInfo(TBody.SlotID.chikubi, MPN.haircolor, "乳首", true)
			},
			{
				TBody.SlotID.accHa,
				new SlotInfo(TBody.SlotID.accHa, MPN.facegloss, "歯", true)
			},
			{
				TBody.SlotID.hairF,
				new SlotInfo(TBody.SlotID.hairF, MPN.MabutaUpIn, "前髪", true)
			},
			{
				TBody.SlotID.hairR,
				new SlotInfo(TBody.SlotID.hairR, MPN.MabutaUpIn2, "後髪", true)
			},
			{
				TBody.SlotID.hairS,
				new SlotInfo(TBody.SlotID.hairS, MPN.MabutaUpOut, "横髪", true)
			},
			{
				TBody.SlotID.hairT,
				new SlotInfo(TBody.SlotID.hairT, MPN.MabutaUpMiddle, "エクステ毛", true)
			},
			{
				TBody.SlotID.hairAho,
				new SlotInfo(TBody.SlotID.hairAho, MPN.MabutaUpOut2, "アホ毛", true)
			},
			{
				TBody.SlotID.underhair,
				new SlotInfo(TBody.SlotID.underhair, MPN.moza, "アンダーヘア", true)
			},
			{
				TBody.SlotID.accHat,
				new SlotInfo(TBody.SlotID.accHat, MPN.accmimi, "帽子", true)
			},
			{
				TBody.SlotID.headset,
				new SlotInfo(TBody.SlotID.headset, MPN.chikubicolor, "ヘッドドレス", true)
			},
			{
				TBody.SlotID.wear,
				new SlotInfo(TBody.SlotID.wear, MPN.hokuro, "トップス", true)
			},
			{
				TBody.SlotID.skirt,
				new SlotInfo(TBody.SlotID.skirt, MPN.mayu, "ボトムス", true)
			},
			{
				TBody.SlotID.onepiece,
				new SlotInfo(TBody.SlotID.onepiece, MPN.accnip, "ワンピース", true)
			},
			{
				TBody.SlotID.mizugi,
				new SlotInfo(TBody.SlotID.mizugi, MPN.lip, "水着", true)
			},
			{
				TBody.SlotID.bra,
				new SlotInfo(TBody.SlotID.bra, MPN.eye, "ブラジャー", true)
			},
			{
				TBody.SlotID.panz,
				new SlotInfo(TBody.SlotID.panz, MPN.eye_hi, "パンツ", true)
			},
			{
				TBody.SlotID.stkg,
				new SlotInfo(TBody.SlotID.stkg, MPN.eye_hi_r, "靴下", true)
			},
			{
				TBody.SlotID.shoes,
				new SlotInfo(TBody.SlotID.shoes, MPN.chikubi, "靴", true)
			},
			{
				TBody.SlotID.accKami_1_,
				new SlotInfo(TBody.SlotID.accKami_1_, MPN.futae, "アクセ：前髪", true)
			},
			{
				TBody.SlotID.accKami_2_,
				new SlotInfo(TBody.SlotID.accKami_2_, MPN.futae, "アクセ：前髪：左", true)
			},
			{
				TBody.SlotID.accKami_3_,
				new SlotInfo(TBody.SlotID.accKami_3_, MPN.futae, "アクセ：前髪：右", true)
			},
			{
				TBody.SlotID.megane,
				new SlotInfo(TBody.SlotID.megane, MPN.acchana, "アクセ：メガネ", true)
			},
			{
				TBody.SlotID.accHead,
				new SlotInfo(TBody.SlotID.accHead, MPN.nose, "アクセ：アイマスク", true)
			},
			{
				TBody.SlotID.glove,
				new SlotInfo(TBody.SlotID.glove, MPN.eyewhite, "アクセ：手袋", true)
			},
			{
				TBody.SlotID.accHana,
				new SlotInfo(TBody.SlotID.accHana, MPN.matsuge_up, "アクセ：鼻", true)
			},
			{
				TBody.SlotID.accMiMiL,
				new SlotInfo(TBody.SlotID.accMiMiL, MPN.wear, "アクセ：左耳", true)
			},
			{
				TBody.SlotID.accMiMiR,
				new SlotInfo(TBody.SlotID.accMiMiR, MPN.wear, "アクセ：右耳", true)
			},
			{
				TBody.SlotID.accKubi,
				new SlotInfo(TBody.SlotID.accKubi, MPN.mizugi, "アクセ：ネックレス", true)
			},
			{
				TBody.SlotID.accKubiwa,
				new SlotInfo(TBody.SlotID.accKubiwa, MPN.bra, "アクセ：チョーカー", true)
			},
			{
				TBody.SlotID.accKamiSubL,
				new SlotInfo(TBody.SlotID.accKamiSubL, MPN.matsuge_low, "アクセ：左リボン", true)
			},
			{
				TBody.SlotID.accKamiSubR,
				new SlotInfo(TBody.SlotID.accKamiSubR, MPN.matsuge_low, "アクセ：右リボン", true)
			},
			{
				TBody.SlotID.accNipL,
				new SlotInfo(TBody.SlotID.accNipL, MPN.skirt, "アクセ：左乳首", true)
			},
			{
				TBody.SlotID.accNipR,
				new SlotInfo(TBody.SlotID.accNipR, MPN.skirt, "アクセ：右乳首", true)
			},
			{
				TBody.SlotID.accUde,
				new SlotInfo(TBody.SlotID.accUde, MPN.stkg, "アクセ：腕", true)
			},
			{
				TBody.SlotID.accHeso,
				new SlotInfo(TBody.SlotID.accHeso, MPN.panz, "アクセ：へそ", true)
			},
			{
				TBody.SlotID.accAshi,
				new SlotInfo(TBody.SlotID.accAshi, MPN.shoes, "アクセ：足首", true)
			},
			{
				TBody.SlotID.accSenaka,
				new SlotInfo(TBody.SlotID.accSenaka, MPN.headset, "アクセ：背中", true)
			},
			{
				TBody.SlotID.accShippo,
				new SlotInfo(TBody.SlotID.accShippo, MPN.glove, "アクセ：しっぽ", true)
			},
			{
				TBody.SlotID.accXXX,
				new SlotInfo(TBody.SlotID.accXXX, MPN.acckamisub, "アクセ：前穴", true)
			},
			{
				TBody.SlotID.seieki_naka,
				new SlotInfo(TBody.SlotID.seieki_naka, MPN.handitem, "精液：中", true)
			},
			{
				TBody.SlotID.seieki_hara,
				new SlotInfo(TBody.SlotID.seieki_hara, MPN.acchat, "精液：腹", true)
			},
			{
				TBody.SlotID.seieki_face,
				new SlotInfo(TBody.SlotID.seieki_face, MPN.onepiece, "精液：顔", true)
			},
			{
				TBody.SlotID.seieki_mune,
				new SlotInfo(TBody.SlotID.seieki_mune, MPN.set_maidwear, "精液：胸", true)
			},
			{
				TBody.SlotID.seieki_hip,
				new SlotInfo(TBody.SlotID.seieki_hip, MPN.set_mywear, "精液：尻", true)
			},
			{
				TBody.SlotID.seieki_ude,
				new SlotInfo(TBody.SlotID.seieki_ude, MPN.set_underwear, "精液：腕", true)
			},
			{
				TBody.SlotID.seieki_ashi,
				new SlotInfo(TBody.SlotID.seieki_ashi, MPN.set_body, "精液：足", true)
			},
			{
				TBody.SlotID.HandItemL,
				new SlotInfo(TBody.SlotID.HandItemL, MPN.acckami, "手持アイテム：左", true)
			},
			{
				TBody.SlotID.HandItemR,
				new SlotInfo(TBody.SlotID.HandItemR, MPN.acckami, "手持アイテム：右", true)
			},
			{
				TBody.SlotID.kousoku_upper,
				new SlotInfo(TBody.SlotID.kousoku_upper, MPN.megane, "拘束具：上", true)
			},
			{
				TBody.SlotID.kousoku_lower,
				new SlotInfo(TBody.SlotID.kousoku_lower, MPN.accxxx, "拘束具：下", true)
			},
			{
				TBody.SlotID.accAnl,
				new SlotInfo(TBody.SlotID.accAnl, MPN.acchead, "アナルバイブ", true)
			},
			{
				TBody.SlotID.accVag,
				new SlotInfo(TBody.SlotID.accVag, MPN.accha, "バイブ", true)
			},
			{
				TBody.SlotID.chinko,
				new SlotInfo(TBody.SlotID.chinko, MPN.null_mpn, "チ○コ", true, ACConstants.settings.enableMask)
			},
			{
				TBody.SlotID.moza,
				new SlotInfo(TBody.SlotID.moza, MPN.MayuLong, "モザイク", ACConstants.settings.enableMoza, ACConstants.settings.enableMoza)
			}
		};

		// Token: 0x04000165 RID: 357
		public static readonly Dictionary<TBody.SlotID, TBody.SlotID> OppositeSlotNames = new Dictionary<TBody.SlotID, TBody.SlotID>
		{
			{
				TBody.SlotID.accKami_2_,
				TBody.SlotID.accKami_3_
			},
			{
				TBody.SlotID.accKami_3_,
				TBody.SlotID.accKami_2_
			},
			{
				TBody.SlotID.accMiMiL,
				TBody.SlotID.accMiMiR
			},
			{
				TBody.SlotID.accMiMiR,
				TBody.SlotID.accMiMiL
			},
			{
				TBody.SlotID.accKamiSubL,
				TBody.SlotID.accKamiSubR
			},
			{
				TBody.SlotID.accKamiSubR,
				TBody.SlotID.accKamiSubL
			},
			{
				TBody.SlotID.accNipL,
				TBody.SlotID.accNipR
			},
			{
				TBody.SlotID.accNipR,
				TBody.SlotID.accNipL
			},
			{
				TBody.SlotID.HandItemL,
				TBody.SlotID.HandItemR
			},
			{
				TBody.SlotID.HandItemR,
				TBody.SlotID.HandItemL
			}
		};

		// Token: 0x04000166 RID: 358
		public static readonly Dictionary<string, NodeItem> NodeNames = new Dictionary<string, NodeItem>
		{
			{
				"Bip01 Pelvis_SCL_",
				new NodeItem("骨盤", 0, new TBody.SlotID[]
				{
					TBody.SlotID.mizugi,
					TBody.SlotID.wear,
					TBody.SlotID.onepiece,
					TBody.SlotID.skirt,
					TBody.SlotID.stkg
				})
			},
			{
				"Bip01 Spine_SCL_",
				new NodeItem("脊椎", 0, new TBody.SlotID[]
				{
					TBody.SlotID.mizugi,
					TBody.SlotID.wear,
					TBody.SlotID.onepiece,
					TBody.SlotID.skirt,
					TBody.SlotID.stkg
				})
			},
			{
				"Bip01 Spine0a_SCL_",
				new NodeItem("腹部", 1, new TBody.SlotID[]
				{
					TBody.SlotID.mizugi,
					TBody.SlotID.wear,
					TBody.SlotID.onepiece,
					TBody.SlotID.skirt,
					TBody.SlotID.stkg
				})
			},
			{
				"Bip01 Spine1_SCL_",
				new NodeItem("腰中", 2, new TBody.SlotID[]
				{
					TBody.SlotID.mizugi,
					TBody.SlotID.wear,
					TBody.SlotID.onepiece,
					TBody.SlotID.skirt,
					TBody.SlotID.stkg
				})
			},
			{
				"Bip01 Spine1a_SCL_",
				new NodeItem("胸部", 3, new TBody.SlotID[]
				{
					TBody.SlotID.mizugi,
					TBody.SlotID.wear,
					TBody.SlotID.onepiece
				})
			},
			{
				"Bip01 Neck_SCL_",
				new NodeItem("首", 4, new TBody.SlotID[]
				{
					TBody.SlotID.accKubi,
					TBody.SlotID.accKubiwa
				})
			},
			{
				"Bip01 Head",
				new NodeItem("頭", 5, new TBody.SlotID[]
				{
					TBody.SlotID.accHead,
					TBody.SlotID.headset,
					TBody.SlotID.hairT,
					TBody.SlotID.hairF,
					TBody.SlotID.hairR,
					TBody.SlotID.hairS,
					TBody.SlotID.hairAho
				})
			},
			{
				"Mune_L",
				new NodeItem("左胸下", 4, new TBody.SlotID[]
				{
					TBody.SlotID.bra,
					TBody.SlotID.mizugi,
					TBody.SlotID.wear,
					TBody.SlotID.onepiece
				})
			},
			{
				"Mune_L_sub",
				new NodeItem("左胸上", 5, new TBody.SlotID[]
				{
					TBody.SlotID.bra,
					TBody.SlotID.mizugi,
					TBody.SlotID.wear,
					TBody.SlotID.onepiece
				})
			},
			{
				"Mune_R",
				new NodeItem("右胸下", 4, new TBody.SlotID[]
				{
					TBody.SlotID.bra,
					TBody.SlotID.mizugi,
					TBody.SlotID.wear,
					TBody.SlotID.onepiece
				})
			},
			{
				"Mune_R_sub",
				new NodeItem("右胸上", 5, new TBody.SlotID[]
				{
					TBody.SlotID.bra,
					TBody.SlotID.mizugi,
					TBody.SlotID.wear,
					TBody.SlotID.onepiece
				})
			},
			{
				"Bip01",
				new NodeItem("股間", 0, new TBody.SlotID[]
				{
					TBody.SlotID.panz,
					TBody.SlotID.stkg,
					TBody.SlotID.mizugi,
					TBody.SlotID.skirt,
					TBody.SlotID.onepiece,
					TBody.SlotID.wear
				})
			},
			{
				"Hip_L",
				new NodeItem("左尻", 1, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"Hip_L_nub",
				new NodeItem("股間左部", 2, new TBody.SlotID[]
				{
					TBody.SlotID.panz,
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"Hip_R",
				new NodeItem("右尻", 1, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"Hip_R_nub",
				new NodeItem("股間右部", 2, new TBody.SlotID[]
				{
					TBody.SlotID.panz,
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"momotwist_L",
				new NodeItem("左前腿", 2, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"momoniku_L",
				new NodeItem("左後腿", 3, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"momotwist2_L",
				new NodeItem("左前腿下部", 3, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"Bip01 L Thigh_SCL_",
				new NodeItem("左ふくらはぎ", 1, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"Bip01 L Calf_SCL_",
				new NodeItem("左足下腿", 2, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"Bip01 L Foot",
				new NodeItem("左足首", 3, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"Bip01 L Toe0",
				new NodeItem("左足小指付け根", 4, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"Bip01 L Toe01",
				new NodeItem("左足小指先", 5, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"Bip01 L Toe1",
				new NodeItem("左足中指付け根", 4, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"Bip01 L Toe11",
				new NodeItem("左足中指先", 5, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"Bip01 L Toe2",
				new NodeItem("左足親指付け根", 4, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"Bip01 L Toe21",
				new NodeItem("左足親指先", 5, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"momotwist_R",
				new NodeItem("右前腿", 2, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"momoniku_R",
				new NodeItem("右後腿", 3, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"momotwist2_R",
				new NodeItem("右前腿下部", 3, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"Bip01 R Thigh_SCL_",
				new NodeItem("右ふくらはぎ", 1, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"Bip01 R Calf_SCL_",
				new NodeItem("右足下腿", 2, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"Bip01 R Foot",
				new NodeItem("右足首", 3, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"Bip01 R Toe0",
				new NodeItem("右足小指付け根", 4, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"Bip01 R Toe01",
				new NodeItem("右足小指先", 5, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"Bip01 R Toe1",
				new NodeItem("右足中指付け根", 4, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"Bip01 R Toe11",
				new NodeItem("右足中指先", 5, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"Bip01 R Toe2",
				new NodeItem("右足親指付け根", 4, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"Bip01 R Toe21",
				new NodeItem("右足親指先", 5, new TBody.SlotID[]
				{
					TBody.SlotID.stkg,
					TBody.SlotID.shoes
				})
			},
			{
				"Bip01 L Clavicle_SCL_",
				new NodeItem("左鎖骨", 4, new TBody.SlotID[]
				{
					TBody.SlotID.wear,
					TBody.SlotID.onepiece,
					TBody.SlotID.mizugi
				})
			},
			{
				"Kata_L",
				new NodeItem("左肩", 5, new TBody.SlotID[]
				{
					TBody.SlotID.wear,
					TBody.SlotID.onepiece,
					TBody.SlotID.mizugi
				})
			},
			{
				"Kata_L_nub",
				new NodeItem("左肩上腕", 6, new TBody.SlotID[]
				{
					TBody.SlotID.glove,
					TBody.SlotID.accUde,
					TBody.SlotID.wear
				})
			},
			{
				"Uppertwist_L",
				new NodeItem("左上腕A", 6, new TBody.SlotID[]
				{
					TBody.SlotID.glove,
					TBody.SlotID.accUde,
					TBody.SlotID.wear
				})
			},
			{
				"Uppertwist1_L",
				new NodeItem("左上腕B", 6, new TBody.SlotID[]
				{
					TBody.SlotID.glove,
					TBody.SlotID.accUde,
					TBody.SlotID.wear
				})
			},
			{
				"Bip01 L UpperArm",
				new NodeItem("左上腕", 5, new TBody.SlotID[]
				{
					TBody.SlotID.glove,
					TBody.SlotID.accUde,
					TBody.SlotID.wear
				})
			},
			{
				"Bip01 L Forearm",
				new NodeItem("左肘", 6, new TBody.SlotID[]
				{
					TBody.SlotID.glove,
					TBody.SlotID.accUde,
					TBody.SlotID.wear
				})
			},
			{
				"Foretwist1_L",
				new NodeItem("左前腕", 7, new TBody.SlotID[]
				{
					TBody.SlotID.glove,
					TBody.SlotID.accUde,
					TBody.SlotID.wear
				})
			},
			{
				"Foretwist_L",
				new NodeItem("左手首", 7, new TBody.SlotID[]
				{
					TBody.SlotID.glove,
					TBody.SlotID.accUde,
					TBody.SlotID.wear
				})
			},
			{
				"Bip01 L Hand",
				new NodeItem("左手", 7, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 L Finger0",
				new NodeItem("左親指付け根", 8, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 L Finger01",
				new NodeItem("左親指関節", 9, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 L Finger02",
				new NodeItem("左親指先", 10, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 L Finger1",
				new NodeItem("左人指し指付け根", 8, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 L Finger11",
				new NodeItem("左人指し指関節", 9, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 L Finger12",
				new NodeItem("左人指し指先", 10, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 L Finger2",
				new NodeItem("左中指付け根", 8, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 L Finger21",
				new NodeItem("左中指関節", 9, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 L Finger22",
				new NodeItem("左中指先", 10, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 L Finger3",
				new NodeItem("左薬指付け根", 8, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 L Finger31",
				new NodeItem("左薬指関節", 9, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 L Finger32",
				new NodeItem("左薬指先", 10, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 L Finger4",
				new NodeItem("左小指付け根", 8, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 L Finger41",
				new NodeItem("左小指関節", 9, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 L Finger42",
				new NodeItem("左小指先", 10, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 R Clavicle_SCL_",
				new NodeItem("右鎖骨", 4, new TBody.SlotID[]
				{
					TBody.SlotID.wear,
					TBody.SlotID.onepiece,
					TBody.SlotID.mizugi
				})
			},
			{
				"Kata_R",
				new NodeItem("右肩", 5, new TBody.SlotID[]
				{
					TBody.SlotID.wear,
					TBody.SlotID.onepiece,
					TBody.SlotID.mizugi
				})
			},
			{
				"Kata_R_nub",
				new NodeItem("右肩上腕", 6, new TBody.SlotID[]
				{
					TBody.SlotID.glove,
					TBody.SlotID.accUde,
					TBody.SlotID.wear
				})
			},
			{
				"Uppertwist_R",
				new NodeItem("右上腕A", 6, new TBody.SlotID[]
				{
					TBody.SlotID.glove,
					TBody.SlotID.accUde,
					TBody.SlotID.wear
				})
			},
			{
				"Uppertwist1_R",
				new NodeItem("右上腕B", 6, new TBody.SlotID[]
				{
					TBody.SlotID.glove,
					TBody.SlotID.accUde,
					TBody.SlotID.wear
				})
			},
			{
				"Bip01 R UpperArm",
				new NodeItem("右上腕", 5, new TBody.SlotID[]
				{
					TBody.SlotID.glove,
					TBody.SlotID.accUde,
					TBody.SlotID.wear
				})
			},
			{
				"Bip01 R Forearm",
				new NodeItem("右肘", 6, new TBody.SlotID[]
				{
					TBody.SlotID.glove,
					TBody.SlotID.accUde,
					TBody.SlotID.wear
				})
			},
			{
				"Foretwist1_R",
				new NodeItem("右前腕", 7, new TBody.SlotID[]
				{
					TBody.SlotID.glove,
					TBody.SlotID.accUde,
					TBody.SlotID.wear
				})
			},
			{
				"Foretwist_R",
				new NodeItem("右手首", 7, new TBody.SlotID[]
				{
					TBody.SlotID.glove,
					TBody.SlotID.accUde,
					TBody.SlotID.wear
				})
			},
			{
				"Bip01 R Hand",
				new NodeItem("右手", 7, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 R Finger0",
				new NodeItem("右親指付け根", 8, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 R Finger01",
				new NodeItem("右親指関節", 9, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 R Finger02",
				new NodeItem("右親指先", 10, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 R Finger1",
				new NodeItem("右人指し指付け根", 8, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 R Finger11",
				new NodeItem("右人指し指関節", 9, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 R Finger12",
				new NodeItem("右人指し指先", 10, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 R Finger2",
				new NodeItem("右中指付け根", 8, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 R Finger21",
				new NodeItem("右中指関節", 9, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 R Finger22",
				new NodeItem("右中指先", 10, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 R Finger3",
				new NodeItem("右薬指付け根", 8, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 R Finger31",
				new NodeItem("右薬指関節", 9, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 R Finger32",
				new NodeItem("右薬指先", 10, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 R Finger4",
				new NodeItem("右小指付け根", 8, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 R Finger41",
				new NodeItem("右小指関節", 9, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			},
			{
				"Bip01 R Finger42",
				new NodeItem("右小指先", 10, new TBody.SlotID[]
				{
					TBody.SlotID.glove
				})
			}
		};
	}
}
