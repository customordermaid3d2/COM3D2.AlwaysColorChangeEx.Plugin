using System;
using System.Reflection;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Util
{
	// Token: 0x02000059 RID: 89
	public sealed class PrivateAccessor
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002CD RID: 717 RVA: 0x000176D8 File Offset: 0x000158D8
		public static PrivateAccessor Instance
		{
			get
			{
				return PrivateAccessor.INSTANCE;
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x000176DF File Offset: 0x000158DF
		private PrivateAccessor()
		{
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000176E8 File Offset: 0x000158E8
		public static T Get<T>(object instance, string fieldName)
		{
			try
			{
				FieldInfo field = instance.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (field != null)
				{
					return (T)((object)field.GetValue(instance));
				}
			}
			catch (Exception ex)
			{
				LogUtil.Debug(new object[]
				{
					ex
				});
			}
			return default(T);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00017748 File Offset: 0x00015948
		public static T Get<T>(Type type, string fieldName)
		{
			try
			{
				FieldInfo field = type.GetField(fieldName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				if (field != null)
				{
					return (T)((object)field.GetValue(null));
				}
			}
			catch (Exception ex)
			{
				LogUtil.Debug(new object[]
				{
					ex
				});
			}
			return default(T);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000177A4 File Offset: 0x000159A4
		public static void Set<T>(object instance, string fieldName, T value)
		{
			try
			{
				FieldInfo field = instance.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (field != null)
				{
					field.SetValue(instance, value);
				}
			}
			catch (Exception ex)
			{
				LogUtil.Debug(new object[]
				{
					ex
				});
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000177F8 File Offset: 0x000159F8
		public static void Set<T>(Type type, string fieldName, T value)
		{
			try
			{
				FieldInfo field = type.GetField(fieldName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				if (field != null)
				{
					field.SetValue(null, value);
				}
			}
			catch (Exception ex)
			{
				LogUtil.Debug(new object[]
				{
					ex
				});
			}
		}

		// Token: 0x0400033F RID: 831
		private static readonly PrivateAccessor INSTANCE = new PrivateAccessor();
	}
}
