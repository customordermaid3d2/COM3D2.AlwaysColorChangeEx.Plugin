using System;
using System.IO;
using CM3D2.AlwaysColorChangeEx.Plugin.Data;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Util
{
	// Token: 0x02000058 RID: 88
	public sealed class OutputUtil
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00017340 File Offset: 0x00015540
		public static OutputUtil Instance
		{
			get
			{
				return OutputUtil.INSTANCE;
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00017347 File Offset: 0x00015547
		private OutputUtil()
		{
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00017350 File Offset: 0x00015550
		public string GetModDirectory()
		{
			string fullPath = Path.GetFullPath(".\\");
			string text = Path.Combine(fullPath, "Mod");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return text;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00017384 File Offset: 0x00015584
		public string GetExportDirectory()
		{
			return this.GetACCDirectory("Export");
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00017394 File Offset: 0x00015594
		public string GetACCDirectory(string subName = null)
		{
			string modDirectory = this.GetModDirectory();
			string text = Path.Combine(modDirectory, "ACC");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			if (string.IsNullOrEmpty(subName))
			{
				return text;
			}
			text = Path.Combine(text, subName);
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return text;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x000173E8 File Offset: 0x000155E8
		public void WriteBytes(string file, byte[] imageBytes)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(File.OpenWrite(file)))
			{
				binaryWriter.Write(imageBytes);
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00017424 File Offset: 0x00015624
		public void WriteTex(string outpath, string txtPath, byte[] imageBytes)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(File.OpenWrite(outpath)))
			{
				binaryWriter.Write(FileConst.HEAD_TEX);
				binaryWriter.Write(1000);
				binaryWriter.Write(txtPath);
				binaryWriter.Write(imageBytes.Length);
				binaryWriter.Write(imageBytes);
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00017488 File Offset: 0x00015688
		public void Copy(string infilepath, string outfilepath)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(File.OpenWrite(outfilepath)))
			{
				using (FileStream fileStream = new FileStream(infilepath, FileMode.Open))
				{
					byte[] buffer = new byte[8196];
					int count;
					while ((count = fileStream.Read(buffer, 0, 8196)) >= 0)
					{
						binaryWriter.Write(buffer, 0, count);
					}
				}
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00017508 File Offset: 0x00015708
		public void WritePmat(string outpath, string name, float priority, string shader)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(File.OpenWrite(outpath)))
			{
				binaryWriter.Write(FileConst.HEAD_PMAT);
				binaryWriter.Write(1000);
				binaryWriter.Write(name.GetHashCode());
				binaryWriter.Write(name);
				binaryWriter.Write(priority);
				binaryWriter.Write(shader);
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00017578 File Offset: 0x00015778
		public void TransferMenu(BinaryReader reader, BinaryWriter writer, string header, string txtpath, Func<string, string[], string[]> replace)
		{
			writer.Write(header);
			writer.Write(reader.ReadInt32());
			reader.ReadString();
			writer.Write(txtpath);
			writer.Write(reader.ReadString());
			writer.Write(reader.ReadString());
			writer.Write(reader.ReadString());
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					reader.ReadInt32();
					for (;;)
					{
						int num = (int)reader.ReadByte();
						if (num == 0)
						{
							break;
						}
						string text = reader.ReadString();
						string[] array = new string[num - 1];
						for (int i = 0; i < num - 1; i++)
						{
							array[i] = reader.ReadString();
						}
						array = replace(text, array);
						if (array != null)
						{
							binaryWriter.Write((byte)(array.Length + 1));
							binaryWriter.Write(text);
							foreach (string value in array)
							{
								binaryWriter.Write(value);
							}
							if (text == "end")
							{
								goto IL_F8;
							}
						}
					}
					binaryWriter.Write(0);
					IL_F8:
					writer.Write((int)memoryStream.Length);
					writer.Write(memoryStream.ToArray());
				}
			}
		}

		// Token: 0x0400033D RID: 829
		private const int BUFFER_SIZE = 8196;

		// Token: 0x0400033E RID: 830
		private static readonly OutputUtil INSTANCE = new OutputUtil();
	}
}
