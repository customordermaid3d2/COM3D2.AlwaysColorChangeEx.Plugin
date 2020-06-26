using System;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.TexAnim
{
	// Token: 0x0200007C RID: 124
	public static class ParseAnimUtil
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x00023934 File Offset: 0x00021B34
		public static AnimTex[] ParseAnimTex(Material m)
		{
			AnimTex[] array = null;
			int num = 0;
			foreach (TexProp texProp in ParseAnimUtil.targets)
			{
				AnimTex animTex = ParseAnimUtil.ParseAnimTex(m, texProp, null);
				if (animTex != null)
				{
					array = (array ?? new AnimTex[ParseAnimUtil.targets.Length]);
					array[num] = animTex;
				}
				num++;
			}
			return array;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0002398C File Offset: 0x00021B8C
		public static AnimTex ParseAnimTex(Material mate, TexProp texProp, Texture tex = null)
		{
			try
			{
				if (tex != null)
				{
					if (!ParseAnimUtil.IsTarget(tex))
					{
						return null;
					}
				}
				else if (!ParseAnimUtil.TryGetTargetTex(mate, texProp, out tex))
				{
					return null;
				}
				Vector2 scale = (texProp == TexProp.MainTex) ? mate.mainTextureScale : mate.GetTextureScale(texProp.PropId);
				if (mate.HasProperty(texProp.PropScrollXId) || mate.HasProperty(texProp.PropScrollYId))
				{
					Vector2 scroll;
					scroll.x = ParseAnimUtil.fitting(mate.GetFloat(texProp.PropScrollXId));
					scroll.y = ParseAnimUtil.fitting(mate.GetFloat(texProp.PropScrollYId));
					float frameSec = ParseAnimUtil.ParseFrameSecond(mate, texProp);
					ScrollTex scrollTex = new ScrollTex(scroll, tex, frameSec)
					{
						texProp = texProp
					};
					scrollTex.InitOffsetIndex(mate.GetTextureOffset(texProp.PropId));
					return scrollTex;
				}
				if (scale.x <= 0.5f || scale.y <= 0.5f)
				{
					if (ParseAnimUtil.Equals(scale.x, 0f, 1E-06f) || ParseAnimUtil.Equals(scale.y, 0f, 1E-06f))
					{
						return null;
					}
					float frameSec2 = ParseAnimUtil.ParseFrameSecond(mate, texProp);
					SlideScaledTex slideScaledTex = new SlideScaledTex(scale, tex, frameSec2)
					{
						texProp = texProp
					};
					if (slideScaledTex.imageLength > 1)
					{
						slideScaledTex.InitOffsetIndex(mate.GetTextureOffset(texProp.PropId));
						LogUtil.DebugF("{0} X:{1},Y:{2},length={3}", new object[]
						{
							texProp,
							slideScaledTex.ratioX,
							slideScaledTex.ratioY,
							slideScaledTex.imageLength
						});
						return slideScaledTex;
					}
					return slideScaledTex;
				}
			}
			catch (Exception ex)
			{
				LogUtil.Debug(new object[]
				{
					"Failed to parse texture:",
					texProp,
					ex
				});
			}
			return null;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00023B98 File Offset: 0x00021D98
		private static float fitting(float f)
		{
			if (f > 0.5f)
			{
				return 0.5f;
			}
			if (f >= -0.5f)
			{
				return f;
			}
			return -0.5f;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00023BB8 File Offset: 0x00021DB8
		private static float ParseFrameSecond(Material m, TexProp prop)
		{
			if (m.HasProperty(prop.PropFPSId))
			{
				float @float = m.GetFloat(prop.PropFPSId);
				if (@float > 0f)
				{
					return 1f / @float;
				}
			}
			return ParseAnimUtil.settings.defaultFrameSecond;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00023BFA File Offset: 0x00021DFA
		public static bool Equals(float left, float right, float epsilon = 1E-06f)
		{
			if (left < right)
			{
				return right - left < epsilon;
			}
			return left - right < epsilon;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00023C10 File Offset: 0x00021E10
		public static bool HasTargetTexName(Material m, TexProp texType)
		{
			Texture texture = (texType == TexProp.MainTex) ? m.mainTexture : m.GetTexture(texType.PropId);
			return texture != null && ParseAnimUtil.IsTarget(texture);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00023C4B File Offset: 0x00021E4B
		public static bool TryGetTargetTex(Material m, TexProp texType, out Texture tex)
		{
			tex = ((texType == TexProp.MainTex) ? m.mainTexture : m.GetTexture(texType.PropId));
			return tex != null && ParseAnimUtil.IsTarget(tex);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00023C80 File Offset: 0x00021E80
		public static bool IsTarget(Texture tex)
		{
			if (tex.name.Length <= 4)
			{
				return false;
			}
			if (tex.name[tex.name.Length - 4] == '.')
			{
				return tex.name.EndsWith(ParseAnimUtil.settings.namePostfixWithExt, StringComparison.OrdinalIgnoreCase);
			}
			return tex.name.EndsWith(ParseAnimUtil.settings.namePostfix, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x040004BB RID: 1211
		private static readonly Settings settings = Settings.Instance;

		// Token: 0x040004BC RID: 1212
		private static readonly TexProp[] targets = new TexProp[]
		{
			TexProp.MainTex,
			TexProp.ShadowTex
		};
	}
}
