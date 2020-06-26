using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CM3D2.AlwaysColorChangeEx.Plugin.UI.Data;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x02000018 RID: 24
	public class ACCMaterialEx : ACCMaterial
	{
		// Token: 0x060000DF RID: 223 RVA: 0x0000AF5E File Offset: 0x0000915E
		private ACCMaterialEx(ShaderType type) : base(type)
		{
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000AF74 File Offset: 0x00009174
		public static ACCMaterialEx Load(string file)
		{
			bool flag;
			using (BinaryReader binaryReader = new BinaryReader(FileUtilEx.Instance.GetStream(file, out flag), Encoding.UTF8))
			{
				string text = binaryReader.ReadString();
				if (flag || binaryReader.BaseStream.Position > 0L)
				{
					if (text == FileConst.HEAD_MATE)
					{
						return ACCMaterialEx.Load(binaryReader);
					}
					StringBuilder stringBuilder = LogUtil.Log(new object[]
					{
						"指定されたファイルのヘッダが不正です。",
						text,
						file
					});
					throw new ACCException(stringBuilder.ToString());
				}
			}
			ACCMaterialEx result;
			using (BinaryReader binaryReader2 = new BinaryReader(new MemoryStream(FileUtilEx.Instance.LoadInternal(file), false), Encoding.UTF8))
			{
				string text2 = binaryReader2.ReadString();
				if (!(text2 == FileConst.HEAD_MATE))
				{
					StringBuilder stringBuilder2 = LogUtil.Log(new object[]
					{
						"指定されたファイルのヘッダが不正です。",
						text2,
						file
					});
					throw new ACCException(stringBuilder2.ToString());
				}
				result = ACCMaterialEx.Load(binaryReader2);
			}
			return result;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000B0A4 File Offset: 0x000092A4
		private static ACCMaterialEx Load(BinaryReader reader)
		{
			reader.ReadInt32();
			string text = reader.ReadString();
			string text2 = reader.ReadString();
			string name = reader.ReadString();
			ShaderType type = ShaderType.Resolve(name);
			ACCMaterialEx accmaterialEx = new ACCMaterialEx(type)
			{
				name1 = text,
				name2 = text2
			};
			reader.ReadString();
			for (;;)
			{
				string text3 = reader.ReadString();
				if (text3 == "end")
				{
					break;
				}
				string propName = reader.ReadString();
				string a;
				if ((a = text3) != null)
				{
					if (!(a == "tex"))
					{
						if (!(a == "col") && !(a == "vec"))
						{
							if (a == "f")
							{
								float f = reader.ReadSingle();
								accmaterialEx.SetFloat(propName, f);
							}
						}
						else
						{
							Color c = new Color(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
							accmaterialEx.SetColor(propName, c);
						}
					}
					else
					{
						string text4 = reader.ReadString();
						string a2;
						if ((a2 = text4) != null)
						{
							if (!(a2 == "tex2d"))
							{
								if (!(a2 == "null"))
								{
									if (a2 == "texRT")
									{
										reader.ReadString();
										reader.ReadString();
									}
								}
							}
							else
							{
								ACCTextureEx acctextureEx = new ACCTextureEx(propName);
								acctextureEx.editname = reader.ReadString();
								acctextureEx.txtpath = reader.ReadString();
								acctextureEx.texOffset.x = reader.ReadSingle();
								acctextureEx.texOffset.y = reader.ReadSingle();
								acctextureEx.texScale.x = reader.ReadSingle();
								acctextureEx.texScale.y = reader.ReadSingle();
								accmaterialEx.texDic[acctextureEx.propKey] = acctextureEx;
							}
						}
					}
				}
			}
			return accmaterialEx;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000B288 File Offset: 0x00009488
		public static void Write(string filepath, ACCMaterialEx mate)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(File.OpenWrite(filepath)))
			{
				ACCMaterialEx.Write(binaryWriter, mate);
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000B2C4 File Offset: 0x000094C4
		public static void Write(BinaryWriter writer, ACCMaterialEx mate)
		{
			writer.Write(FileConst.HEAD_MATE);
			writer.Write(1000);
			writer.Write(mate.name1);
			writer.Write(mate.name2);
			string name = mate.type.name;
			writer.Write(name);
			string mateName = ShaderType.GetMateName(name);
			writer.Write(mateName);
			foreach (ShaderPropTex shaderPropTex in mate.type.texProps)
			{
				if (shaderPropTex.key == PropKey._RenderTex)
				{
					writer.Write("null");
				}
				else
				{
					writer.Write("tex2d");
					ACCTextureEx acctextureEx = mate.texDic[shaderPropTex.key];
					writer.Write(acctextureEx.editname);
					writer.Write(acctextureEx.txtpath);
					ACCMaterialEx.OUT_UTIL.Write(writer, acctextureEx.texOffset);
					ACCMaterialEx.OUT_UTIL.Write(writer, acctextureEx.texScale);
				}
			}
			for (int j = 0; j < mate.editColors.Length; j++)
			{
				ShaderPropColor shaderPropColor = mate.type.colProps[j];
				EditColor editColor = mate.editColors[j];
				writer.Write(shaderPropColor.type.ToString());
				writer.Write(shaderPropColor.keyName);
				ACCMaterialEx.OUT_UTIL.Write(writer, editColor.val);
			}
			for (int k = 0; k < mate.editVals.Length; k++)
			{
				ShaderPropFloat shaderPropFloat = mate.type.fProps[k];
				EditValue editValue = mate.editVals[k];
				writer.Write(shaderPropFloat.type.ToString());
				writer.Write(shaderPropFloat.keyName);
				writer.Write(editValue.val);
			}
		}

		// Token: 0x0400010E RID: 270
		private static readonly FileUtilEx OUT_UTIL = FileUtilEx.Instance;

		// Token: 0x0400010F RID: 271
		public readonly Dictionary<PropKey, ACCTextureEx> texDic = new Dictionary<PropKey, ACCTextureEx>(5);

		// Token: 0x04000110 RID: 272
		public string name1;

		// Token: 0x04000111 RID: 273
		public string name2;
	}
}
