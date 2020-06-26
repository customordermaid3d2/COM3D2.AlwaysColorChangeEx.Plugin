using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x02000028 RID: 40
	public class MateHandler
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000F148 File Offset: 0x0000D348
		public static MateHandler Instance
		{
			get
			{
				return MateHandler.INSTANCE;
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000F150 File Offset: 0x0000D350
		public string Read(string path = null)
		{
			if (path == null)
			{
				path = this.filepath;
			}
			string result;
			using (BufferedStream bufferedStream = new BufferedStream(new FileStream(path, FileMode.Open, FileAccess.Read), this.bufferSize))
			{
				using (BinaryReader binaryReader = new BinaryReader(bufferedStream, Encoding.UTF8))
				{
					string text = binaryReader.ReadString();
					if (!(text == "CM3D2_MATERIAL"))
					{
						string message = "正しいmateファイルではありません。ヘッダが不正です。" + text + ", file=" + path;
						throw new Exception(message);
					}
					result = this.Read(binaryReader).ToString();
				}
			}
			return result;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000F1F8 File Offset: 0x0000D3F8
		private StringBuilder Read(BinaryReader reader)
		{
			StringBuilder stringBuilder = new StringBuilder(8192);
			stringBuilder.Append(reader.ReadInt32()).Append("\r\n");
			stringBuilder.Append(reader.ReadString()).Append("\r\n");
			stringBuilder.Append(reader.ReadString()).Append("\r\n");
			stringBuilder.Append(reader.ReadString()).Append("\r\n");
			stringBuilder.Append(reader.ReadString()).Append("\r\n\r\n");
			for (;;)
			{
				string text = reader.ReadString();
				if (text == "end")
				{
					break;
				}
				string value = reader.ReadString();
				stringBuilder.Append(text).Append("\r\n");
				stringBuilder.Append('\t').Append(value).Append("\r\n");
				string a;
				if ((a = text) != null)
				{
					if (!(a == "tex"))
					{
						if (!(a == "col") && !(a == "vec"))
						{
							if (a == "f")
							{
								stringBuilder.Append('\t').Append(reader.ReadSingle()).Append("\r\n");
							}
						}
						else
						{
							stringBuilder.Append('\t').Append(reader.ReadSingle()).Append(' ').Append(reader.ReadSingle()).Append(' ').Append(reader.ReadSingle()).Append(' ').Append(reader.ReadSingle()).Append("\r\n");
						}
					}
					else
					{
						string text2 = reader.ReadString();
						stringBuilder.Append('\t').Append(text2).Append("\r\n");
						string a2;
						if ((a2 = text2) != null)
						{
							if (!(a2 == "tex2d"))
							{
								if (!(a2 == "null"))
								{
									if (a2 == "texRT")
									{
										stringBuilder.Append('\t').Append(reader.ReadString()).Append("\r\n");
										stringBuilder.Append('\t').Append(reader.ReadString()).Append("\r\n");
									}
								}
							}
							else
							{
								stringBuilder.Append('\t').Append(reader.ReadString()).Append("\r\n");
								stringBuilder.Append('\t').Append(reader.ReadString()).Append("\r\n");
								stringBuilder.Append('\t').Append(reader.ReadSingle()).Append(' ').Append(reader.ReadSingle()).Append(' ').Append(reader.ReadSingle()).Append(' ').Append(reader.ReadSingle()).Append("\r\n");
							}
						}
					}
				}
			}
			return stringBuilder;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000F4CC File Offset: 0x0000D6CC
		public void Write(string mateText, string path = null)
		{
			if (path == null)
			{
				path = this.filepath;
			}
			using (BufferedStream bufferedStream = new BufferedStream(new FileStream(path, FileMode.CreateNew, FileAccess.Write), this.bufferSize))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(bufferedStream, Encoding.UTF8))
				{
					this.Write(mateText, binaryWriter);
				}
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000F540 File Offset: 0x0000D740
		private string ReadLine(TextReader reader)
		{
			string text = reader.ReadLine();
			if (text == null)
			{
				throw new Exception("マテリアルを表すファイルの書式が正しくありません");
			}
			return text;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000F564 File Offset: 0x0000D764
		public void Write(string mateText, BinaryWriter writer)
		{
			writer.Write("CM3D2_MATERIAL");
			StringReader stringReader = new StringReader(mateText);
			string text = stringReader.ReadLine();
			int value;
			if (text == null || !int.TryParse(text.Trim(), out value))
			{
				throw new Exception("バージョンが取得できません");
			}
			writer.Write(value);
			writer.Write(this.ReadLine(stringReader).Trim());
			writer.Write(this.ReadLine(stringReader).Trim());
			writer.Write(this.ReadLine(stringReader).Trim());
			writer.Write(this.ReadLine(stringReader).Trim());
			while ((text = stringReader.ReadLine()) != null)
			{
				text = text.Trim();
				if (text.Length != 0)
				{
					writer.Write(text);
					string text2 = this.ReadLine(stringReader).Trim();
					writer.Write(text2);
					string a;
					if ((a = text) != null)
					{
						if (!(a == "tex"))
						{
							if (!(a == "col") && !(a == "vec"))
							{
								if (a == "f")
								{
									string s = this.ReadLine(stringReader).Trim();
									float value2;
									if (!float.TryParse(s, out value2))
									{
										throw new Exception("f値をfloatに変換できません。propName=" + text2);
									}
									writer.Write(value2);
								}
							}
							else
							{
								string[] array = this.ReadLine(stringReader).Split(new char[]
								{
									' '
								});
								if (array.Length != 4)
								{
									throw new Exception("Color値の指定が正しく（４値）指定されていません。propName=" + text2);
								}
								foreach (string s2 in array)
								{
									float value3;
									if (!float.TryParse(s2, out value3))
									{
										throw new Exception("color値をfloatに変換できません。propName=" + text2);
									}
									writer.Write(value3);
								}
							}
						}
						else
						{
							string text3 = this.ReadLine(stringReader).Trim();
							writer.Write(text3);
							string a2;
							if ((a2 = text3) != null)
							{
								if (!(a2 == "tex2d"))
								{
									if (!(a2 == "null"))
									{
										if (a2 == "texRT")
										{
											writer.Write(this.ReadLine(stringReader).Trim());
											writer.Write(this.ReadLine(stringReader).Trim());
										}
									}
								}
								else
								{
									writer.Write(this.ReadLine(stringReader).Trim());
									writer.Write(this.ReadLine(stringReader).Trim());
									string[] array3 = this.ReadLine(stringReader).Split(new char[]
									{
										' '
									});
									if (array3.Length != 4)
									{
										throw new Exception("オフセット、スケール値が正しく（４値）指定されていません。propName=" + text2);
									}
									for (int j = 0; j < 4; j++)
									{
										float value4;
										if (!float.TryParse(array3[j], out value4))
										{
											throw new Exception("オフセット、スケール値をfloatに変換できません。propName=" + text2);
										}
										writer.Write(value4);
									}
								}
							}
						}
					}
				}
			}
			writer.Write("end");
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000F858 File Offset: 0x0000DA58
		public string ToText(ACCMaterial target)
		{
			Material material = target.material;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("1000\r\n");
			stringBuilder.Append(target.name.ToLower()).Append("\r\n");
			stringBuilder.Append(target.name).Append("\r\n");
			string name = target.type.name;
			stringBuilder.Append(name).Append("\r\n");
			stringBuilder.Append(ShaderType.GetMateName(name)).Append("\r\n\r\n");
			ShaderType type = target.type;
			foreach (ShaderPropTex shaderPropTex in type.texProps)
			{
				stringBuilder.Append("tex\r\n");
				string keyName = shaderPropTex.keyName;
				stringBuilder.Append('\t').Append(keyName).Append("\r\n");
				Texture texture = material.GetTexture(shaderPropTex.propId);
				if (texture == null)
				{
					stringBuilder.Append("\tnull\r\n");
				}
				else
				{
					stringBuilder.Append("\ttex2d\r\n");
					stringBuilder.Append('\t').Append(texture.name).Append("\r\n");
					stringBuilder.Append('\t').Append(MateHandler.settings.txtPrefixTex).Append(texture.name).Append(".png\r\n");
					Vector2 textureOffset = material.GetTextureOffset(keyName);
					Vector2 textureScale = material.GetTextureScale(keyName);
					stringBuilder.Append("\t").Append(textureOffset.x).Append(' ').Append(textureOffset.y).Append(' ').Append(textureScale.x).Append(' ').Append(textureScale.y).Append("\r\n");
				}
			}
			foreach (ShaderPropColor shaderPropColor in type.colProps)
			{
				stringBuilder.Append("col\r\n");
				string keyName2 = shaderPropColor.keyName;
				stringBuilder.Append('\t').Append(keyName2).Append("\r\n");
				Color color = material.GetColor(keyName2);
				stringBuilder.Append('\t').Append(color.r).Append(' ').Append(color.g).Append(' ').Append(color.b).Append(' ').Append(color.a).Append("\r\n");
			}
			foreach (ShaderPropFloat shaderPropFloat in type.fProps)
			{
				stringBuilder.Append("f\r\n");
				string keyName3 = shaderPropFloat.keyName;
				stringBuilder.Append('\t').Append(keyName3).Append("\r\n");
				float @float = material.GetFloat(keyName3);
				stringBuilder.Append('\t').Append(@float).Append("\r\n");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000FB6C File Offset: 0x0000DD6C
		public static bool IsParsable(string text)
		{
			bool result;
			using (StringReader stringReader = new StringReader(text))
			{
				string text2 = stringReader.ReadLine();
				int num;
				if (string.IsNullOrEmpty(text2))
				{
					result = false;
				}
				else if (!int.TryParse(text2, out num))
				{
					result = false;
				}
				else
				{
					string text3 = stringReader.ReadLine();
					if (text3 == null || text3.Trim().Length == 0 || text3[0] == '\t')
					{
						result = false;
					}
					else
					{
						text3 = stringReader.ReadLine();
						if (text3 == null || text3.Trim().Length == 0 || text3[0] == '\t')
						{
							result = false;
						}
						else
						{
							text3 = stringReader.ReadLine();
							if (text3 == null || text3.Trim().Length == 0 || text3[0] == '\t')
							{
								result = false;
							}
							else
							{
								text3 = stringReader.ReadLine();
								if (text3 == null || text3.Trim().Length == 0 || text3[0] == '\t')
								{
									result = false;
								}
								else
								{
									result = true;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000FC6C File Offset: 0x0000DE6C
		public bool Write(ACCMaterial target, string mateText)
		{
			return this.Write(target, mateText, MateHandler.MATE_ALL);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000FC7C File Offset: 0x0000DE7C
		public bool Write(ACCMaterial target, string mateText, int apply)
		{
			using (StringReader stringReader = new StringReader(mateText))
			{
				FileUtilEx instance = FileUtilEx.Instance;
				stringReader.ReadLine();
				Material material = target.material;
				stringReader.ReadLine();
				stringReader.ReadLine();
				string text = stringReader.ReadLine();
				stringReader.ReadLine();
				if ((apply & MateHandler.MATE_SHADER) > 0 && material.shader.name != text)
				{
					target.ChangeShader(text, -1);
				}
				ShaderType type = target.type;
				string text2 = stringReader.ReadLine();
				List<string> list = new List<string>();
				while (text2 != null)
				{
					if (text2.Length != 0 && text2[0] != '\t')
					{
						list.Clear();
						string text3 = text2.Trim();
						while ((text2 = stringReader.ReadLine()) != null)
						{
							if (text2.Length != 0)
							{
								if (text2[0] != '\t')
								{
									break;
								}
								string text4 = text2.Trim();
								if (text4.Length > 0)
								{
									list.Add(text4);
								}
							}
						}
						string a;
						if (list.Count != 0 && (a = text3) != null)
						{
							if (!(a == "tex"))
							{
								if (!(a == "col") && !(a == "vec"))
								{
									if (a == "f")
									{
										if ((apply & MateHandler.MATE_FLOAT) > 0 && list.Count >= 2)
										{
											string text5 = list[0];
											ShaderProp shaderProp = type.GetShaderProp(text5);
											float value;
											if (shaderProp == null)
											{
												LogUtil.Log(new object[]
												{
													"シェーダに対応していないプロパティのためスキップします.propName=",
													text5
												});
											}
											else if (!float.TryParse(list[1], out value))
											{
												LogUtil.Log(new object[]
												{
													"指定文字列はfloatに変換できません。スキップします。propName={0}, text={1}",
													text5,
													list[1]
												});
											}
											else
											{
												material.SetFloat(shaderProp.propId, value);
												LogUtil.DebugF("float set({0})", new object[]
												{
													text5
												});
											}
										}
									}
								}
								else if ((apply & MateHandler.MATE_COLOR) > 0 && list.Count >= 2)
								{
									string text6 = list[0];
									ShaderProp shaderProp2 = type.GetShaderProp(text6);
									if (shaderProp2 == null)
									{
										LogUtil.Log(new object[]
										{
											"シェーダに対応していないプロパティのためスキップします.propName=",
											text6
										});
									}
									else
									{
										float[] array = this.ParseVals(list[1], text6, 4);
										if (array != null)
										{
											Color value2 = new Color(array[0], array[1], array[2], array[3]);
											material.SetColor(shaderProp2.propId, value2);
											LogUtil.DebugF("color set ({0})", new object[]
											{
												text6
											});
										}
									}
								}
							}
							else if ((apply & MateHandler.MATE_TEX) > 0)
							{
								if (list.Count == 2)
								{
									if (list[1] == "null")
									{
										material.SetTexture(list[0], null);
									}
								}
								else if (list.Count < 5)
								{
									string text7 = string.Empty;
									if (list.Count >= 1)
									{
										text7 = "propName=" + list[0];
									}
									LogUtil.Log(new object[]
									{
										"指定パラメータが不足しているためtexの適用をスキップします.",
										text7
									});
								}
								else
								{
									string text8 = list[0];
									string text9 = list[2];
									ShaderProp shaderProp3 = type.GetShaderProp(text8);
									if (shaderProp3 == null)
									{
										LogUtil.Log(new object[]
										{
											"シェーダに対応していないプロパティのためスキップします.propName=",
											text8
										});
									}
									else
									{
										Texture texture = material.GetTexture(shaderProp3.propId);
										if (texture == null || texture.name != text9)
										{
											if (!text9.ToLower().EndsWith(FileConst.EXT_TEXTURE, StringComparison.Ordinal))
											{
												text9 += FileConst.EXT_TEXTURE;
											}
											if (!instance.Exists(text9))
											{
												LogUtil.LogF("tex({0}) not found. (propName={1})", new object[]
												{
													text9,
													text8
												});
												continue;
											}
											Texture2D value3 = instance.LoadTexture(text9);
											material.SetTexture(shaderProp3.propId, value3);
										}
										float[] array2 = this.ParseVals(list[4], text8, 4);
										if (array2 == null)
										{
											LogUtil.DebugF("tex({0}) prop is null", new object[]
											{
												text9
											});
										}
										else
										{
											material.SetTextureOffset(shaderProp3.propId, new Vector2(array2[0], array2[1]));
											material.SetTextureScale(shaderProp3.propId, new Vector2(array2[2], array2[3]));
											LogUtil.DebugF("tex({0}) loaded to {1}", new object[]
											{
												text9,
												shaderProp3.keyName
											});
										}
									}
								}
							}
						}
					}
					else
					{
						text2 = stringReader.ReadLine();
					}
				}
			}
			return true;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00010178 File Offset: 0x0000E378
		private float[] ParseVals(string text, string propName = null, int count = 4)
		{
			string[] array = text.Split(new char[]
			{
				' '
			});
			if (array.Length < count)
			{
				LogUtil.LogF("float値が正しく（{0}個）指定されていません。スキップします。propName={1}", new object[]
				{
					count,
					propName
				});
				return null;
			}
			float[] array2 = new float[count];
			for (int i = 0; i < count; i++)
			{
				float num;
				if (!float.TryParse(array[i], out num))
				{
					LogUtil.Log(new object[]
					{
						"指定文字列はfloatに変換できません。スキップします。propName={0}, text={1}",
						propName,
						array[i]
					});
					return null;
				}
				array2[i] = num;
			}
			return array2;
		}

		// Token: 0x0400019F RID: 415
		private static readonly MateHandler INSTANCE = new MateHandler();

		// Token: 0x040001A0 RID: 416
		public static readonly int MATE_SHADER = 1;

		// Token: 0x040001A1 RID: 417
		public static readonly int MATE_COLOR = 2;

		// Token: 0x040001A2 RID: 418
		public static readonly int MATE_FLOAT = 4;

		// Token: 0x040001A3 RID: 419
		public static readonly int MATE_TEX = 8;

		// Token: 0x040001A4 RID: 420
		public static readonly int MATE_ALL = 15;

		// Token: 0x040001A5 RID: 421
		private static readonly Settings settings = Settings.Instance;

		// Token: 0x040001A6 RID: 422
		public string filepath;

		// Token: 0x040001A7 RID: 423
		public int bufferSize = 8192;
	}
}
