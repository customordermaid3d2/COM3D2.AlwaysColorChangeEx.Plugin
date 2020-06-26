using System;
using System.Collections;
using System.IO;
using System.Reflection;
using JsonFx.Json;
using UnityEngine.Internal;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Util.Json
{
	// Token: 0x02000043 RID: 67
	public class CustomJsonWriter : JsonWriter
	{
		// Token: 0x06000207 RID: 519 RVA: 0x0001320C File Offset: 0x0001140C
		public CustomJsonWriter(Stream output, JsonWriterSettings settings) : base(output, settings)
		{
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00013218 File Offset: 0x00011418
		protected override void Write(object value, bool isProperty)
		{
			JsonWriterSettings settings = base.Settings;
			if (isProperty && settings.PrettyPrint)
			{
				base.TextWriter.Write(' ');
			}
			if (value == null)
			{
				base.TextWriter.Write("null");
				return;
			}
			if (value is IJsonSerializable)
			{
				try
				{
					if (isProperty)
					{
						this.depth++;
						this.WriteLine();
					}
					((IJsonSerializable)value).WriteJson(this);
				}
				finally
				{
					if (isProperty)
					{
						this.depth--;
					}
				}
				return;
			}
			if (value is Enum)
			{
				this.Write((Enum)value);
				return;
			}
			Type type = value.GetType();
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Empty:
			case TypeCode.DBNull:
				base.TextWriter.Write("null");
				return;
			case TypeCode.Boolean:
				this.Write((bool)value);
				return;
			case TypeCode.Char:
				this.Write((char)value);
				return;
			case TypeCode.SByte:
				this.Write((sbyte)value);
				return;
			case TypeCode.Byte:
				this.Write((byte)value);
				return;
			case TypeCode.Int16:
				this.Write((short)value);
				return;
			case TypeCode.UInt16:
				this.Write((ushort)value);
				return;
			case TypeCode.Int32:
				this.Write((int)value);
				return;
			case TypeCode.UInt32:
				this.Write((uint)value);
				return;
			case TypeCode.Int64:
				this.Write((long)value);
				return;
			case TypeCode.UInt64:
				this.Write((ulong)value);
				return;
			case TypeCode.Single:
				this.Write((float)value);
				return;
			case TypeCode.Double:
				this.Write((double)value);
				return;
			case TypeCode.Decimal:
				this.Write((decimal)value);
				return;
			case TypeCode.DateTime:
				this.Write((DateTime)value);
				return;
			case TypeCode.String:
				this.Write((string)value);
				return;
			}
			if (value is Guid)
			{
				this.Write((Guid)value);
				return;
			}
			Uri uri = value as Uri;
			if (uri != null)
			{
				this.Write(uri);
				return;
			}
			if (value is TimeSpan)
			{
				this.Write((TimeSpan)value);
				return;
			}
			Version version = value as Version;
			if (version != null)
			{
				this.Write(version);
				return;
			}
			IDictionary dictionary = value as IDictionary;
			if (dictionary != null)
			{
				try
				{
					if (isProperty)
					{
						this.WriteLine();
					}
					this.WriteObject(dictionary);
				}
				finally
				{
				}
				return;
			}
			if (type.GetInterface("System.Collections.Generic.IDictionary`2") != null)
			{
				try
				{
					if (isProperty)
					{
						this.depth++;
						this.WriteLine();
					}
					this.WriteDictionary((IEnumerable)value);
				}
				finally
				{
					if (isProperty)
					{
						this.depth--;
					}
				}
				return;
			}
			IEnumerable enumerable = value as IEnumerable;
			if (enumerable != null)
			{
				this.WriteArray(enumerable);
				return;
			}
			try
			{
				if (isProperty)
				{
					this.WriteLine();
				}
				this.WriteObject(value, type);
			}
			finally
			{
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0001350C File Offset: 0x0001170C
		protected override void WriteArray(IEnumerable value)
		{
			bool flag = false;
			base.TextWriter.Write('[');
			this.depth++;
			try
			{
				this.WriteLine();
				foreach (object obj in value)
				{
					if (flag)
					{
						this.WriteArrayItemDelim();
						this.WriteLine();
					}
					else
					{
						flag = true;
					}
					this.WriteArrayItem(obj);
				}
			}
			finally
			{
				this.depth--;
			}
			if (flag)
			{
				this.WriteLine();
			}
			base.TextWriter.Write(']');
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000135C4 File Offset: 0x000117C4
		protected void WriteTab()
		{
			base.TextWriter.Write(base.Settings.Tab);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0001360C File Offset: 0x0001180C
		protected override void WriteDictionary(IEnumerable value)
		{
			IDictionaryEnumerator dictionaryEnumerator = value.GetEnumerator() as IDictionaryEnumerator;
			if (dictionaryEnumerator == null)
			{
				throw new JsonSerializationException(string.Format("Types which implement Generic IDictionary<TKey, TValue> must have an IEnumerator which implements IDictionaryEnumerator. ({0})", value.GetType()));
			}
			bool writeDelim = false;
			base.TextWriter.Write('{');
			this.WriteTab();
			this.depth++;
			try
			{
				while (dictionaryEnumerator.MoveNext())
				{
					this.WriteObjectProperty(Convert.ToString(dictionaryEnumerator.Entry.Key), dictionaryEnumerator.Entry.Value, delegate
					{
						if (writeDelim)
						{
							this.WriteObjectPropertyDelim();
							this.WriteLine();
							return;
						}
						writeDelim = true;
					});
				}
			}
			finally
			{
				this.depth--;
			}
			if (writeDelim)
			{
				this.WriteLine();
			}
			base.TextWriter.Write('}');
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000136F8 File Offset: 0x000118F8
		protected bool WriteObjectProperty(string key, object value, Action act)
		{
			if (!this.ignoreNull || value != null)
			{
				if (act != null)
				{
					act();
				}
				this.WriteObjectPropertyName(key);
				base.TextWriter.Write(':');
				this.WriteObjectPropertyValue(value);
				return true;
			}
			return false;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00013798 File Offset: 0x00011998
		protected override void WriteObject(object value, Type type)
		{
			bool writeDelim = false;
			base.TextWriter.Write('{');
			this.WriteTab();
			this.depth++;
			try
			{
				if (!string.IsNullOrEmpty(base.Settings.TypeHintName))
				{
					this.WriteObjectProperty(base.Settings.TypeHintName, type.FullName + ", " + type.Assembly.GetName().Name, delegate
					{
						if (writeDelim)
						{
							this.WriteObjectPropertyDelim();
							return;
						}
						writeDelim = true;
					});
				}
				bool flag = type.IsGenericType && type.Name.StartsWith("<>f__AnonymousType", StringComparison.Ordinal);
				foreach (PropertyInfo propertyInfo in type.GetProperties())
				{
					if (propertyInfo.CanRead && (propertyInfo.CanWrite || flag) && !this.IsIgnored(type, propertyInfo, value))
					{
						object value2 = propertyInfo.GetValue(value, null);
						if (!this.IsDefaultValue(propertyInfo, value2))
						{
							string text = JsonNameAttribute.GetJsonName(propertyInfo);
							if (string.IsNullOrEmpty(text))
							{
								text = propertyInfo.Name;
							}
							this.WriteObjectProperty(text, value2, delegate
							{
								if (writeDelim)
								{
									this.WriteObjectPropertyDelim();
									return;
								}
								writeDelim = true;
							});
						}
					}
				}
				foreach (FieldInfo fieldInfo in type.GetFields())
				{
					if (fieldInfo.IsPublic && !fieldInfo.IsStatic && !this.IsIgnored(type, fieldInfo, value))
					{
						object value3 = fieldInfo.GetValue(value);
						if (!this.IsDefaultValue(fieldInfo, value3))
						{
							string text2 = JsonNameAttribute.GetJsonName(fieldInfo);
							if (string.IsNullOrEmpty(text2))
							{
								text2 = fieldInfo.Name;
							}
							this.WriteObjectProperty(text2, value3, delegate
							{
								if (writeDelim)
								{
									this.WriteObjectPropertyDelim();
									this.WriteLine();
									return;
								}
								writeDelim = true;
							});
						}
					}
				}
			}
			finally
			{
				this.depth--;
			}
			if (writeDelim)
			{
				this.WriteLine();
			}
			base.TextWriter.Write('}');
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000139C0 File Offset: 0x00011BC0
		protected override void WriteLine()
		{
			if (!base.Settings.PrettyPrint)
			{
				return;
			}
			base.TextWriter.WriteLine();
			for (int i = 0; i < this.depth; i++)
			{
				this.WriteTab();
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00013A00 File Offset: 0x00011C00
		private bool IsIgnored(Type objType, MemberInfo member, object obj)
		{
			if (JsonIgnoreAttribute.IsJsonIgnore(member))
			{
				return true;
			}
			string jsonSpecifiedProperty = JsonSpecifiedPropertyAttribute.GetJsonSpecifiedProperty(member);
			if (!string.IsNullOrEmpty(jsonSpecifiedProperty))
			{
				PropertyInfo property = objType.GetProperty(jsonSpecifiedProperty);
				if (property != null)
				{
					object value = property.GetValue(obj, null);
					if (value is bool && !Convert.ToBoolean(value))
					{
						return true;
					}
				}
			}
			if (base.Settings.UseXmlSerializationAttributes)
			{
				if (JsonIgnoreAttribute.IsXmlIgnore(member))
				{
					return true;
				}
				PropertyInfo property2 = objType.GetProperty(member.Name + "Specified");
				if (property2 != null)
				{
					object value2 = property2.GetValue(obj, null);
					if (value2 is bool && !Convert.ToBoolean(value2))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00013A9C File Offset: 0x00011C9C
		private bool IsDefaultValue(MemberInfo member, object value)
		{
			DefaultValueAttribute defaultValueAttribute = Attribute.GetCustomAttribute(member, typeof(DefaultValueAttribute)) as DefaultValueAttribute;
			if (defaultValueAttribute == null)
			{
				return false;
			}
			if (defaultValueAttribute.Value == null)
			{
				return value == null;
			}
			return defaultValueAttribute.Value.Equals(value);
		}

		// Token: 0x04000300 RID: 768
		public bool ignoreNull;

		// Token: 0x04000301 RID: 769
		private int depth;
	}
}
