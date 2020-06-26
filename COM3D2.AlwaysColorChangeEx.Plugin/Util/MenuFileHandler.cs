using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Util
{
	// Token: 0x02000054 RID: 84
	public class MenuFileHandler
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x00016C34 File Offset: 0x00014E34
		public List<MenuFileHandler.ChangeInfo> Parse(string filename)
		{
			if (!GameUty.FileSystem.IsExistentFile(filename))
			{
				return null;
			}
			List<MenuFileHandler.ChangeInfo> result;
			using (BinaryReader binaryReader = new BinaryReader(FileUtilEx.Instance.GetStream(filename), Encoding.UTF8))
			{
				string a = binaryReader.ReadString();
				if (a != "CM3D2_MENU")
				{
					throw new Exception("header invalid. " + filename);
				}
				binaryReader.ReadInt32();
				binaryReader.ReadString();
				binaryReader.ReadString();
				binaryReader.ReadString();
				binaryReader.ReadString();
				binaryReader.ReadInt32();
				List<MenuFileHandler.ChangeInfo> list = new List<MenuFileHandler.ChangeInfo>();
				bool flag = true;
				while (flag)
				{
					int num = (int)binaryReader.ReadByte();
					if (num == 0)
					{
						break;
					}
					string text = binaryReader.ReadString();
					string[] array = new string[num - 1];
					for (int i = 0; i < num - 1; i++)
					{
						array[i] = binaryReader.ReadString();
					}
					text = text.ToLower();
					string key;
					switch (key = text)
					{
					case "end":
						flag = false;
						break;
					case "アイテムパラメータ":
						if (array.Length == 3)
						{
						}
						break;
					case "additem":
						this.Add(list, array[0], -1, null);
						break;
					case "delitem":
						this.Add(list, array[0], -1, null);
						break;
					case "tex":
					case "テクスチャ変更":
						this.Add(list, array[0], int.Parse(array[1]), array[2]);
						break;
					case "マテリアル変更":
						this.Add(list, array[0], int.Parse(array[1]), null);
						break;
					case "shader":
						this.Add(list, array[0], int.Parse(array[1]), null);
						break;
					case "useredit":
					{
						string slot = array[2];
						this.Add(list, slot, int.Parse(array[3]), array[4]);
						break;
					}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00017134 File Offset: 0x00015334
		public void Add(List<MenuFileHandler.ChangeInfo> items, string slot, int matNo = -1, string propName = null)
		{
			foreach (MenuFileHandler.ChangeInfo changeInfo in items)
			{
				if (changeInfo.slot == slot)
				{
					changeInfo.Add(matNo, propName);
					return;
				}
			}
			items.Add(new MenuFileHandler.ChangeInfo(slot, matNo, propName));
		}

		// Token: 0x02000055 RID: 85
		public class ChangeInfo
		{
			// Token: 0x060002B9 RID: 697 RVA: 0x000171AC File Offset: 0x000153AC
			public ChangeInfo(string slot, int matNo = -1, string propName = null)
			{
				this.slot = slot;
				if (matNo != -1)
				{
					this.matInfos = new List<MenuFileHandler.MateInfo>
					{
						new MenuFileHandler.MateInfo(matNo, propName)
					};
				}
			}

			// Token: 0x060002BA RID: 698 RVA: 0x000171E4 File Offset: 0x000153E4
			public void Add(int matNo, string propName = null)
			{
				if (this.matInfos == null)
				{
					return;
				}
				if (matNo == -1)
				{
					this.matInfos = null;
					return;
				}
				foreach (MenuFileHandler.MateInfo mateInfo in this.matInfos)
				{
					if (mateInfo.matNo == matNo)
					{
						mateInfo.Add(propName);
						return;
					}
				}
				this.matInfos.Add(new MenuFileHandler.MateInfo(matNo, propName));
			}

			// Token: 0x04000338 RID: 824
			public string slot;

			// Token: 0x04000339 RID: 825
			public List<MenuFileHandler.MateInfo> matInfos;
		}

		// Token: 0x02000056 RID: 86
		public class MateInfo
		{
			// Token: 0x060002BB RID: 699 RVA: 0x0001726C File Offset: 0x0001546C
			public MateInfo(int matNo, string propName = null)
			{
				this.matNo = matNo;
				if (propName != null)
				{
					this.propNames = new List<string>
					{
						propName
					};
				}
			}

			// Token: 0x060002BC RID: 700 RVA: 0x0001729D File Offset: 0x0001549D
			public void Add(string propName)
			{
				if (this.propNames == null)
				{
					return;
				}
				if (propName == null)
				{
					this.propNames = null;
					return;
				}
				if (this.propNames == null)
				{
					this.propNames = new List<string>();
				}
				this.propNames.Add(propName);
			}

			// Token: 0x060002BD RID: 701 RVA: 0x000172D4 File Offset: 0x000154D4
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("matNo=");
				stringBuilder.Append(this.matNo).Append(", propNames=").Append(this.propNames);
				return stringBuilder.ToString();
			}

			// Token: 0x0400033A RID: 826
			public int matNo;

			// Token: 0x0400033B RID: 827
			public List<string> propNames;
		}
	}
}
