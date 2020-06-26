using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CM3D2.AlwaysColorChangeEx.Plugin.Data;
using CM3D2.AlwaysColorChangeEx.Plugin.UI;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Util
{
	// Token: 0x0200004C RID: 76
	public sealed class FileUtilEx
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000245 RID: 581 RVA: 0x000146CE File Offset: 0x000128CE
		public static FileUtilEx Instance
		{
			get
			{
				return FileUtilEx.INSTANCE;
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000146D5 File Offset: 0x000128D5
		private FileUtilEx()
		{
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000146DD File Offset: 0x000128DD
		public string GetModDirectory()
		{
			return FileUtilEx.UTIL.GetModDirectory();
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000146E9 File Offset: 0x000128E9
		public string GetACCDirectory()
		{
			return FileUtilEx.UTIL.GetACCDirectory(null);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000146F6 File Offset: 0x000128F6
		public string GetExportDirectory()
		{
			return FileUtilEx.UTIL.GetACCDirectory("Export");
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00014707 File Offset: 0x00012907
		public string GetACCDirectory(string subName)
		{
			return FileUtilEx.UTIL.GetACCDirectory(subName);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00014714 File Offset: 0x00012914
		public void WriteBytes(string file, byte[] imageBytes)
		{
			FileUtilEx.UTIL.WriteBytes(file, imageBytes);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00014722 File Offset: 0x00012922
		public void WriteTexFile(string filepath, string txtPath, byte[] imageBytes)
		{
			FileUtilEx.UTIL.WriteTex(filepath, txtPath, imageBytes);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00014731 File Offset: 0x00012931
		public void WritePmat(string outpath, string name, float priority, string shader)
		{
			FileUtilEx.UTIL.WritePmat(outpath, name, priority, shader);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00014742 File Offset: 0x00012942
		public void Copy(string infilepath, string outfilepath)
		{
			FileUtilEx.UTIL.Copy(infilepath, outfilepath);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00014750 File Offset: 0x00012950
		public bool Exists(string filename)
		{
			if (!GameUty.ModPriorityToModFolder)
			{
				return GameUty.FileSystem.IsExistentFile(filename) || (GameUty.FileSystemMod != null && GameUty.FileSystemMod.IsExistentFile(filename));
			}
			return (GameUty.FileSystemMod != null && GameUty.FileSystemMod.IsExistentFile(filename)) || GameUty.FileSystem.IsExistentFile(filename);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x000147AC File Offset: 0x000129AC
		public Stream GetStream(string filename)
		{
			Stream result;
			try
			{
				AFileBase afileBase = GameUty.FileOpen(filename, null);
				if (!afileBase.IsValid())
				{
					StringBuilder stringBuilder = LogUtil.Error(new object[]
					{
						"指定ファイルが見つかりません。file=",
						filename
					});
					throw new ACCException(stringBuilder.ToString());
				}
				result = new BufferedStream(new FileBaseStream(afileBase), FileUtilEx.BUFFER_SIZE);
			}
			catch (ACCException)
			{
				throw;
			}
			catch (Exception ex)
			{
				StringBuilder stringBuilder2 = LogUtil.Error(new object[]
				{
					"指定ファイルが読み込めませんでした。",
					filename,
					ex
				});
				throw new ACCException(stringBuilder2.ToString(), ex);
			}
			return result;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0001485C File Offset: 0x00012A5C
		public Stream GetStream(string filename, out bool onBuffer)
		{
			Stream result;
			try
			{
				AFileBase afileBase = GameUty.FileOpen(filename, null);
				if (!afileBase.IsValid())
				{
					StringBuilder stringBuilder = LogUtil.Error(new object[]
					{
						"指定ファイルが見つかりません。file=",
						filename
					});
					throw new ACCException(stringBuilder.ToString());
				}
				onBuffer = (afileBase.GetSize() < FileUtilEx.BUFFER_SIZE);
				result = new BufferedStream(new FileBaseStream(afileBase), FileUtilEx.BUFFER_SIZE);
			}
			catch (ACCException)
			{
				throw;
			}
			catch (Exception ex)
			{
				StringBuilder stringBuilder2 = LogUtil.Error(new object[]
				{
					"指定ファイルが読み込めませんでした。",
					filename,
					ex
				});
				throw new ACCException(stringBuilder2.ToString(), ex);
			}
			return result;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00014918 File Offset: 0x00012B18
		public byte[] LoadInternal(string filename)
		{
			byte[] result;
			try
			{
				using (AFileBase afileBase = GameUty.FileOpen(filename, null))
				{
					if (!afileBase.IsValid())
					{
						StringBuilder stringBuilder = LogUtil.Error(new object[]
						{
							"指定ファイルが見つかりません。file=",
							filename
						});
						throw new ACCException(stringBuilder.ToString());
					}
					result = afileBase.ReadAll();
				}
			}
			catch (ACCException)
			{
				throw;
			}
			catch (Exception ex)
			{
				StringBuilder stringBuilder2 = LogUtil.Error(new object[]
				{
					"指定ファイルが読み込めませんでした。",
					filename,
					ex
				});
				throw new ACCException(stringBuilder2.ToString(), ex);
			}
			return result;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000149D4 File Offset: 0x00012BD4
		public Texture2D LoadTexture(string filename)
		{
			Texture2D texture2D = TexUtil.Instance.Load(filename);
			texture2D.name = Path.GetFileNameWithoutExtension(filename);
			texture2D.wrapMode = TextureWrapMode.Clamp;
			return texture2D;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00014A04 File Offset: 0x00012C04
		public Texture2D LoadTexture(Stream stream)
		{
			byte[] array = new byte[stream.Length];
			stream.Read(array, 0, array.Length);
			Texture2D texture2D = new Texture2D(1, 1, TextureFormat.RGBA32, false);
			texture2D.LoadImage(array);
			texture2D.wrapMode = TextureWrapMode.Clamp;
			return texture2D;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00014A44 File Offset: 0x00012C44
		public void Copy(AFileBase infile, string outfilepath)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(File.OpenWrite(outfilepath)))
			{
				byte[] buffer = new byte[8196];
				int count;
				while ((count = infile.Read(ref buffer, 8196)) > 0)
				{
					binaryWriter.Write(buffer, 0, count);
				}
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00014AA4 File Offset: 0x00012CA4
		public IEnumerable<ReplacedInfo> WriteMenuFile(string infile, string outfilepath, ResourceRef res)
		{
			bool flag;
			using (BinaryReader binaryReader = new BinaryReader(FileUtilEx.Instance.GetStream(infile, out flag), Encoding.UTF8))
			{
				string text = binaryReader.ReadString();
				if (flag || binaryReader.BaseStream.Position > 0L)
				{
					if (text == FileConst.HEAD_MENU)
					{
						return this.WriteMenuFile(binaryReader, text, outfilepath, res);
					}
					StringBuilder stringBuilder = LogUtil.Error(new object[]
					{
						"menuファイルを作成しようとしましたが、参照元ファイルのヘッダが正しくありません。",
						text,
						", file=",
						infile
					});
					throw new ACCException(stringBuilder.ToString());
				}
			}
			IEnumerable<ReplacedInfo> result;
			using (BinaryReader binaryReader2 = new BinaryReader(new MemoryStream(FileUtilEx.Instance.LoadInternal(infile), false), Encoding.UTF8))
			{
				string text2 = binaryReader2.ReadString();
				if (!(text2 == FileConst.HEAD_MENU))
				{
					StringBuilder stringBuilder2 = LogUtil.Error(new object[]
					{
						"menuファイルを作成しようとしましたが、参照元ファイルのヘッダが正しくありません。",
						text2,
						", file=",
						infile
					});
					throw new ACCException(stringBuilder2.ToString());
				}
				result = this.WriteMenuFile(binaryReader2, text2, outfilepath, res);
			}
			return result;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00014BF0 File Offset: 0x00012DF0
		private List<ReplacedInfo> WriteMenuFile(BinaryReader reader, string header, string outfilepath, ResourceRef res)
		{
			List<ReplacedInfo> replaceFiles;
			using (BinaryWriter binaryWriter = new BinaryWriter(File.OpenWrite(outfilepath)))
			{
				try
				{
					FileUtilEx.UTIL.TransferMenu(reader, binaryWriter, header, res.EditTxtPath(), res.ReplaceMenuFunc());
					replaceFiles = res.replaceFiles;
				}
				catch (Exception ex)
				{
					StringBuilder stringBuilder = LogUtil.Error(new object[]
					{
						"menuファイルの作成に失敗しました。 file=",
						outfilepath,
						ex
					});
					throw new ACCException(stringBuilder.ToString(), ex);
				}
			}
			return replaceFiles;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00014C88 File Offset: 0x00012E88
		public bool WriteModelFile(string infile, string outfilepath, SlotMaterials slotMat)
		{
			bool flag;
			using (BinaryReader binaryReader = new BinaryReader(this.GetStream(infile, out flag), Encoding.UTF8))
			{
				string text = binaryReader.ReadString();
				if (flag || binaryReader.BaseStream.Position > 0L)
				{
					if (text == FileConst.HEAD_MODEL)
					{
						return this.WriteModelFile(binaryReader, text, outfilepath, slotMat);
					}
					StringBuilder stringBuilder = LogUtil.Error(new object[]
					{
						"正しいモデルファイルではありません。ヘッダが不正です。",
						text,
						", infile=",
						infile
					});
					throw new ACCException(stringBuilder.ToString());
				}
			}
			bool result;
			using (BinaryReader binaryReader2 = new BinaryReader(new MemoryStream(FileUtilEx.Instance.LoadInternal(infile), false), Encoding.UTF8))
			{
				string text2 = binaryReader2.ReadString();
				if (!(text2 == FileConst.HEAD_MODEL))
				{
					StringBuilder stringBuilder2 = LogUtil.Error(new object[]
					{
						"正しいモデルファイルではありません。ヘッダが不正です。",
						text2,
						", infile=",
						infile
					});
					throw new ACCException(stringBuilder2.ToString());
				}
				result = this.WriteModelFile(binaryReader2, text2, outfilepath, slotMat);
			}
			return result;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00014DD0 File Offset: 0x00012FD0
		private bool WriteModelFile(BinaryReader reader, string header, string outfilepath, SlotMaterials slotMat)
		{
			bool result;
			using (BinaryWriter binaryWriter = new BinaryWriter(File.OpenWrite(outfilepath)))
			{
				result = this.TransferModel(reader, header, binaryWriter, slotMat);
			}
			return result;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00014E14 File Offset: 0x00013014
		public bool TransferModel(BinaryReader reader, string header, BinaryWriter writer, SlotMaterials slotMat)
		{
			writer.Write(header);
			int num = reader.ReadInt32();
			writer.Write(num);
			writer.Write(reader.ReadString());
			writer.Write(reader.ReadString());
			int num2 = reader.ReadInt32();
			writer.Write(num2);
			for (int i = 0; i < num2; i++)
			{
				writer.Write(reader.ReadString());
				writer.Write(reader.ReadByte());
			}
			for (int j = 0; j < num2; j++)
			{
				int value = reader.ReadInt32();
				writer.Write(value);
			}
			for (int k = 0; k < num2; k++)
			{
				this.TransferVec(reader, writer, 7);
				if (num >= 2001)
				{
					bool flag = reader.ReadBoolean();
					writer.Write(flag);
					if (flag)
					{
						this.TransferVec(reader, writer, 3);
					}
				}
			}
			int num3 = reader.ReadInt32();
			int num4 = reader.ReadInt32();
			int num5 = reader.ReadInt32();
			writer.Write(num3);
			writer.Write(num4);
			writer.Write(num5);
			for (int l = 0; l < num5; l++)
			{
				writer.Write(reader.ReadString());
			}
			for (int m = 0; m < num5; m++)
			{
				this.TransferVec(reader, writer, 16);
			}
			for (int n = 0; n < num3; n++)
			{
				this.TransferVec(reader, writer, 8);
			}
			int num6 = reader.ReadInt32();
			writer.Write(num6);
			for (int num7 = 0; num7 < num6; num7++)
			{
				this.TransferVec4(reader, writer);
			}
			for (int num8 = 0; num8 < num3; num8++)
			{
				for (int num9 = 0; num9 < 4; num9++)
				{
					writer.Write(reader.ReadUInt16());
				}
				this.TransferVec4(reader, writer);
			}
			for (int num10 = 0; num10 < num4; num10++)
			{
				int num11 = reader.ReadInt32();
				writer.Write(num11);
				for (int num12 = 0; num12 < num11; num12++)
				{
					writer.Write(reader.ReadInt16());
				}
			}
			int num13 = reader.ReadInt32();
			writer.Write(num13);
			for (int num14 = 0; num14 < num13; num14++)
			{
				TargetMaterial targetMaterial = slotMat.Get(num14);
				this.TransferMaterial(reader, writer, targetMaterial, targetMaterial.onlyModel);
			}
			while (reader.PeekChar() != -1)
			{
				string text = reader.ReadString();
				writer.Write(text);
				if (text == "end")
				{
					break;
				}
				if (!(text != "morph"))
				{
					string value2 = reader.ReadString();
					writer.Write(value2);
					int num15 = reader.ReadInt32();
					writer.Write(num15);
					for (int num16 = 0; num16 < num15; num16++)
					{
						writer.Write(reader.ReadUInt16());
						this.TransferVec(reader, writer, 6);
					}
				}
			}
			return true;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x000150B4 File Offset: 0x000132B4
		public void WriteMateFile(string infile, string outfilepath, TargetMaterial trgtMat)
		{
			bool flag;
			using (BinaryReader binaryReader = new BinaryReader(this.GetStream(infile, out flag), Encoding.UTF8))
			{
				string text = binaryReader.ReadString();
				if (flag || binaryReader.BaseStream.Position > 0L)
				{
					if (text == FileConst.HEAD_MATE)
					{
						this.WriteMateFile(binaryReader, text, outfilepath, trgtMat);
						return;
					}
					StringBuilder stringBuilder = LogUtil.Error(new object[]
					{
						"正しいmateファイルではありません。ヘッダが不正です。",
						text,
						", infile=",
						infile
					});
					throw new ACCException(stringBuilder.ToString());
				}
			}
			using (BinaryReader binaryReader2 = new BinaryReader(new MemoryStream(FileUtilEx.Instance.LoadInternal(infile), false), Encoding.UTF8))
			{
				string text2 = binaryReader2.ReadString();
				if (text2 == FileConst.HEAD_MATE)
				{
					this.WriteMateFile(binaryReader2, text2, outfilepath, trgtMat);
				}
				StringBuilder stringBuilder2 = LogUtil.Error(new object[]
				{
					"正しいmateファイルではありません。ヘッダが不正です。",
					text2,
					", infile=",
					infile
				});
				throw new ACCException(stringBuilder2.ToString());
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000151F4 File Offset: 0x000133F4
		public void WriteMateFile(BinaryReader reader, string header, string outfilepath, TargetMaterial trgtMat)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(File.OpenWrite(outfilepath)))
			{
				binaryWriter.Write(header);
				binaryWriter.Write(reader.ReadInt32());
				binaryWriter.Write(reader.ReadString());
				this.TransferMaterial(reader, binaryWriter, trgtMat, true);
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00015254 File Offset: 0x00013454
		public void TransferMaterial(BinaryReader reader, BinaryWriter writer, TargetMaterial trgtMat, bool overwrite)
		{
			reader.ReadString();
			writer.Write(trgtMat.editname);
			string text = reader.ReadString();
			string value = reader.ReadString();
			if (trgtMat.shaderChanged)
			{
				text = trgtMat.ShaderNameOrDefault(text);
				value = ShaderType.GetMateName(text);
			}
			writer.Write(text);
			writer.Write(value);
			ShaderType type = trgtMat.editedMat.type;
			HashSet<PropKey> hashSet = new HashSet<PropKey>();
			for (;;)
			{
				string text2 = reader.ReadString();
				if (text2 == "end")
				{
					break;
				}
				string text3 = reader.ReadString();
				ShaderProp shaderProp = type.GetShaderProp(text3);
				if (shaderProp == null)
				{
					this.DiscardMateProp(reader, text2);
				}
				else
				{
					string a;
					if (!overwrite)
					{
						this.TransferMateProp(reader, writer, text2, text3);
					}
					else if ((a = text2) != null)
					{
						if (!(a == "tex"))
						{
							if (!(a == "col") && !(a == "vec"))
							{
								if (a == "f")
								{
									this.Write(writer, new string[]
									{
										text2,
										text3
									});
									this.Write(writer, trgtMat.editedMat.material.GetFloat(text3));
									this.DiscardMateProp(reader, text2);
								}
							}
							else
							{
								this.Write(writer, new string[]
								{
									text2,
									text3
								});
								this.Write(writer, trgtMat.editedMat.material.GetColor(text3));
								this.DiscardMateProp(reader, text2);
							}
						}
						else
						{
							TargetTexture targetTexture;
							trgtMat.texDic.TryGetValue(shaderProp.key, out targetTexture);
							if (targetTexture == null || targetTexture.tex == null || targetTexture.fileChanged || targetTexture.colorChanged)
							{
								if (targetTexture != null)
								{
									targetTexture.worksuffix = trgtMat.worksuffix;
								}
								string workfilename = null;
								this.TransferMateProp(reader, null, text2, null, ref workfilename);
								if (targetTexture != null)
								{
									targetTexture.workfilename = workfilename;
								}
								this.WriteTex(writer, text3, trgtMat, targetTexture);
							}
							else
							{
								this.TransferMateProp(reader, writer, text2, text3);
							}
						}
					}
					hashSet.Add(shaderProp.key);
				}
			}
			if (type.KeyCount() != hashSet.Count)
			{
				foreach (ShaderPropTex shaderPropTex in type.texProps)
				{
					if (!hashSet.Contains(shaderPropTex.key))
					{
						TargetTexture trgtTex;
						trgtMat.texDic.TryGetValue(shaderPropTex.key, out trgtTex);
						this.WriteTex(writer, shaderPropTex.keyName, trgtMat, trgtTex);
					}
				}
				foreach (ShaderPropColor shaderPropColor in type.colProps)
				{
					if (!hashSet.Contains(shaderPropColor.key))
					{
						this.Write(writer, new string[]
						{
							shaderPropColor.type.ToString(),
							shaderPropColor.keyName
						});
						this.Write(writer, trgtMat.editedMat.material.GetColor(shaderPropColor.propId));
					}
				}
				foreach (ShaderPropFloat shaderPropFloat in type.fProps)
				{
					if (!hashSet.Contains(shaderPropFloat.key))
					{
						this.Write(writer, new string[]
						{
							shaderPropFloat.type.ToString(),
							shaderPropFloat.keyName
						});
						this.Write(writer, trgtMat.editedMat.material.GetFloat(shaderPropFloat.propId));
					}
				}
			}
			writer.Write("end");
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000155E8 File Offset: 0x000137E8
		private void WriteTex(BinaryWriter writer, string propName, TargetMaterial tm, TargetTexture trgtTex)
		{
			this.Write(writer, new string[]
			{
				"tex"
			});
			this.Write(writer, new string[]
			{
				propName
			});
			string text = "tex2d";
			if (trgtTex == null || trgtTex.tex == null)
			{
				text = "null";
			}
			this.Write(writer, new string[]
			{
				text
			});
			string a;
			if ((a = text) != null)
			{
				if (!(a == "tex2d"))
				{
					if (!(a == "null"))
					{
						if (!(a == "texRT"))
						{
							return;
						}
						writer.Write(string.Empty);
						writer.Write(string.Empty);
					}
				}
				else if (trgtTex.fileChanged || trgtTex.colorChanged)
				{
					this.Write(writer, new string[]
					{
						trgtTex.EditFileNameNoExt()
					});
					this.Write(writer, new string[]
					{
						trgtTex.EditTxtPath()
					});
					this.Write(writer, tm.editedMat.material.GetTextureOffset(propName));
					this.Write(writer, tm.editedMat.material.GetTextureScale(propName));
					return;
				}
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00015719 File Offset: 0x00013919
		private void DiscardMateProp(BinaryReader reader, string type)
		{
			this.TransferMateProp(reader, null, type, null);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00015728 File Offset: 0x00013928
		private void TransferMateProp(BinaryReader reader, BinaryWriter writer, string type, string propName, ref string texfile)
		{
			this.Write(writer, new string[]
			{
				type,
				propName
			});
			if (type != null)
			{
				if (!(type == "tex"))
				{
					if (type == "col" || type == "vec")
					{
						this.TransferVec4(reader, writer);
						return;
					}
					if (!(type == "f"))
					{
						return;
					}
					this.Write(writer, reader.ReadSingle());
				}
				else
				{
					string text = reader.ReadString();
					this.Write(writer, new string[]
					{
						text
					});
					string a;
					if ((a = text) != null)
					{
						if (a == "tex2d")
						{
							string text2 = reader.ReadString();
							texfile = text2;
							string text3 = reader.ReadString();
							this.Write(writer, new string[]
							{
								text2,
								text3
							});
							this.TransferVec4(reader, writer);
							return;
						}
						if (!(a == "null"))
						{
							if (!(a == "texRT"))
							{
								return;
							}
							this.TransferString(reader, writer, 2);
							return;
						}
					}
				}
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00015840 File Offset: 0x00013A40
		private void TransferMateProp(BinaryReader reader, BinaryWriter writer, string type, string propName)
		{
			string text = null;
			this.TransferMateProp(reader, writer, type, propName, ref text);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0001585C File Offset: 0x00013A5C
		private void Write(BinaryWriter writer, params string[] data)
		{
			if (writer == null)
			{
				return;
			}
			foreach (string value in data)
			{
				writer.Write(value);
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00015888 File Offset: 0x00013A88
		private void Write(BinaryWriter writer, float data)
		{
			if (writer != null)
			{
				writer.Write(data);
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00015894 File Offset: 0x00013A94
		private void Write(BinaryWriter writer, byte data)
		{
			if (writer != null)
			{
				writer.Write(data);
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000158A0 File Offset: 0x00013AA0
		public void Write(BinaryWriter writer, Vector2 data)
		{
			if (writer == null)
			{
				return;
			}
			writer.Write(data.x);
			writer.Write(data.y);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000158C0 File Offset: 0x00013AC0
		private void Write(BinaryWriter writer, params float[] data)
		{
			if (writer == null)
			{
				return;
			}
			foreach (float value in data)
			{
				writer.Write(value);
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000158ED File Offset: 0x00013AED
		public void Write(BinaryWriter writer, Color color)
		{
			if (writer == null)
			{
				return;
			}
			writer.Write(color.r);
			writer.Write(color.g);
			writer.Write(color.b);
			writer.Write(color.a);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00015928 File Offset: 0x00013B28
		private void TransferVec(BinaryReader reader, BinaryWriter writer, int size = 3)
		{
			for (int i = 0; i < size; i++)
			{
				float value = reader.ReadSingle();
				if (writer != null)
				{
					writer.Write(value);
				}
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00015952 File Offset: 0x00013B52
		private void TransferVec4(BinaryReader reader, BinaryWriter writer)
		{
			this.TransferVec(reader, writer, 4);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00015960 File Offset: 0x00013B60
		private void TransferString(BinaryReader reader, BinaryWriter writer, int count)
		{
			for (int i = 0; i < count; i++)
			{
				string value = reader.ReadString();
				if (writer != null)
				{
					writer.Write(value);
				}
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0001598C File Offset: 0x00013B8C
		public void CopyTex(string infile, string outfilepath, string txtpath, TextureModifier.FilterParam filter)
		{
			Texture2D texture2D = TexUtil.Instance.Load(infile);
			Texture2D texture2D2 = (filter != null) ? ACCTexturesView.Filter(texture2D, filter) : texture2D;
			this.WriteTexFile(outfilepath, txtpath, texture2D2.EncodeToPNG());
			if (texture2D != texture2D2)
			{
				UnityEngine.Object.DestroyImmediate(texture2D2);
			}
			UnityEngine.Object.DestroyImmediate(texture2D);
		}

		// Token: 0x04000323 RID: 803
		private static readonly FileUtilEx INSTANCE = new FileUtilEx();

		// Token: 0x04000324 RID: 804
		private static readonly OutputUtil UTIL = OutputUtil.Instance;

		// Token: 0x04000325 RID: 805
		private static readonly int BUFFER_SIZE = 8192;
	}
}
