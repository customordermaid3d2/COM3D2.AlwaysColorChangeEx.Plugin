using System;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Util
{
	// Token: 0x02000048 RID: 72
	public static class ColorUtil
	{
		// Token: 0x0600022C RID: 556 RVA: 0x00014053 File Offset: 0x00012253
		public static bool Equals(float f1, float f2)
		{
			return Mathf.Abs(f1 - f2) < 0.001f;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00014064 File Offset: 0x00012264
		public static bool IsColorCode(string code)
		{
			if (code.Length == 7 && code[0] == '#')
			{
				for (int i = 1; i < 7; i++)
				{
					if (!Uri.IsHexDigit(code[i]))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000140A4 File Offset: 0x000122A4
		public static Vector4 RGB2HSL(ref Color c)
		{
			float num = Mathf.Clamp01(c.r);
			float num2 = Mathf.Clamp01(c.g);
			float num3 = Mathf.Clamp01(c.b);
			float num4 = Mathf.Max(num, Mathf.Max(num2, num3));
			float num5 = Mathf.Min(num, Mathf.Min(num2, num3));
			float num6 = 0f;
			float y = 0f;
			float num7 = (num4 + num5) * 0.5f;
			float num8 = num4 - num5;
			if (ColorUtil.Equals(num8, 0f))
			{
				return new Vector4(num6, y, num7, c.a);
			}
			y = ((num7 > 0.5f) ? (num8 / (2f - num4 - num5)) : (num8 / (num4 + num5)));
			if (ColorUtil.Equals(num4, num))
			{
				num6 = (num2 - num3) / num8 + ((num2 < num3) ? 6f : 0f);
			}
			else if (ColorUtil.Equals(num4, num2))
			{
				num6 = (num3 - num) / num8 + 2f;
			}
			else
			{
				num6 = (num - num2) / num8 + 4f;
			}
			num6 /= 6f;
			return new Vector4(num6, y, num7, c.a);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000141BC File Offset: 0x000123BC
		public static Color HSL2RGB(float h, float s, float l, float a)
		{
			Color result;
			result.a = a;
			if (ColorUtil.Equals(s, 0f))
			{
				result.r = l;
				result.g = l;
				result.b = l;
			}
			else
			{
				float num = (l < 0.5f) ? (l * (1f + s)) : (l + s - l * s);
				float x = 2f * l - num;
				result.r = ColorUtil.Hue(x, num, h + 0.333333343f);
				result.g = ColorUtil.Hue(x, num, h);
				result.b = ColorUtil.Hue(x, num, h - 0.333333343f);
			}
			return result;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00014257 File Offset: 0x00012457
		public static Color HSL2RGB(ref Vector4 hsl)
		{
			return ColorUtil.HSL2RGB(hsl.x, hsl.y, hsl.z, hsl.w);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00014278 File Offset: 0x00012478
		public static Vector3 RGB2HSV(ref Color c)
		{
			float num = Mathf.Clamp01(c.r);
			float num2 = Mathf.Clamp01(c.g);
			float num3 = Mathf.Clamp01(c.b);
			float num4 = Mathf.Max(num, Mathf.Max(num2, num3));
			float num5 = Mathf.Min(num, Mathf.Min(num2, num3));
			float num6 = num4 - num5;
			float y;
			if (ColorUtil.Equals(num4, 0f))
			{
				y = 0f;
			}
			else
			{
				y = num6 / num4;
			}
			float num7;
			if (ColorUtil.Equals(num4, num))
			{
				num7 = (num3 - num2) / num6;
			}
			else if (ColorUtil.Equals(num4, num2))
			{
				num7 = (num3 - num) / num6 + 2f;
			}
			else
			{
				num7 = (num - num2) / num6 + 4f;
			}
			num7 /= 6f;
			if (num7 < 0f)
			{
				num7 += 1f;
			}
			float z = num4;
			return new Vector3(num7, y, z);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00014350 File Offset: 0x00012550
		public static Color HSV2RGB(float h, float s, float v)
		{
			if (ColorUtil.Equals(s, 0f))
			{
				return new Color(v, v, v);
			}
			if (ColorUtil.Equals(v, 0f))
			{
				return Color.black;
			}
			float num = h * 6f;
			int num2 = (int)Mathf.Floor(num);
			float num3 = num - (float)num2;
			float num4 = v * (1f - s);
			float num5 = v * (1f - s * num3);
			float num6 = v * (1f - s * (1f - num3));
			switch (num2)
			{
			case 0:
			case 6:
				return new Color(v, num6, num4);
			case 1:
			case 7:
				return new Color(num5, v, num4);
			case 2:
				return new Color(num4, v, num6);
			case 3:
				return new Color(num4, num5, v);
			case 4:
				return new Color(num6, num4, v);
			case 5:
				return new Color(v, num4, num5);
			default:
				throw new ArgumentException("failed to convert Color(HSV to RGB)");
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00014438 File Offset: 0x00012638
		private static float Hue(float x, float y, float t)
		{
			if (t < 0f)
			{
				t += 1f;
			}
			else if (t > 1f)
			{
				t -= 1f;
			}
			if (t < 0.166666672f)
			{
				return x + (y - x) * 6f * t;
			}
			if (t < 0.5f)
			{
				return y;
			}
			if (t < 0.6666667f)
			{
				return x + (y - x) * 6f * (0.6666667f - t);
			}
			return x;
		}

		// Token: 0x04000320 RID: 800
		private const float EPSILON = 0.001f;
	}
}
