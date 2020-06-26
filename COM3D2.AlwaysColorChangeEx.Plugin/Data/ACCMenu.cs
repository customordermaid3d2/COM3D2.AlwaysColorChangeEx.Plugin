using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Data
{
	// Token: 0x02000019 RID: 25
	public class ACCMenu
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x0000B48D File Offset: 0x0000968D
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x0000B495 File Offset: 0x00009695
		public int version { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x0000B49E File Offset: 0x0000969E
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x0000B4A6 File Offset: 0x000096A6
		public string editfile { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x0000B4AF File Offset: 0x000096AF
		// (set) Token: 0x060000EA RID: 234 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public string srcfilename { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000EB RID: 235 RVA: 0x0000B4C0 File Offset: 0x000096C0
		// (set) Token: 0x060000EC RID: 236 RVA: 0x0000B4C8 File Offset: 0x000096C8
		public List<ResourceRef> resources { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0000B4D1 File Offset: 0x000096D1
		// (set) Token: 0x060000EE RID: 238 RVA: 0x0000B4D9 File Offset: 0x000096D9
		public Dictionary<string, SlotMaterials> slotMaterials { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000EF RID: 239 RVA: 0x0000B4E2 File Offset: 0x000096E2
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x0000B4EA File Offset: 0x000096EA
		public Dictionary<string, List<ACCMaterialEx>> baseMatDic { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x0000B4F3 File Offset: 0x000096F3
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x0000B4FB File Offset: 0x000096FB
		public List<Item> addItems { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x0000B504 File Offset: 0x00009704
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x0000B50C File Offset: 0x0000970C
		public List<string> delItems { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x0000B515 File Offset: 0x00009715
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x0000B51D File Offset: 0x0000971D
		public string[] itemParam { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x0000B526 File Offset: 0x00009726
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x0000B52E File Offset: 0x0000972E
		public List<string> items { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000B537 File Offset: 0x00009737
		// (set) Token: 0x060000FA RID: 250 RVA: 0x0000B53F File Offset: 0x0000973F
		public List<string> maskItems { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000B548 File Offset: 0x00009748
		// (set) Token: 0x060000FC RID: 252 RVA: 0x0000B550 File Offset: 0x00009750
		public List<string> delNodes { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000B559 File Offset: 0x00009759
		// (set) Token: 0x060000FE RID: 254 RVA: 0x0000B561 File Offset: 0x00009761
		public List<string> showNodes { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000B56A File Offset: 0x0000976A
		// (set) Token: 0x06000100 RID: 256 RVA: 0x0000B572 File Offset: 0x00009772
		public List<string[]> delPartsNodes { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000B57B File Offset: 0x0000977B
		// (set) Token: 0x06000102 RID: 258 RVA: 0x0000B583 File Offset: 0x00009783
		public List<string[]> showPartsNodes { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000B58C File Offset: 0x0000978C
		// (set) Token: 0x06000104 RID: 260 RVA: 0x0000B594 File Offset: 0x00009794
		public Dictionary<TBody.SlotID, Item> itemSlots { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000B59D File Offset: 0x0000979D
		// (set) Token: 0x06000106 RID: 262 RVA: 0x0000B5A5 File Offset: 0x000097A5
		public Dictionary<string, Item> itemFiles { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000B5AE File Offset: 0x000097AE
		// (set) Token: 0x06000108 RID: 264 RVA: 0x0000B5B6 File Offset: 0x000097B6
		public Dictionary<string, ResourceRef> resFiles { get; private set; }

		// Token: 0x06000109 RID: 265 RVA: 0x0000B5C0 File Offset: 0x000097C0
		public ACCMenu()
		{
			this.slotMaterials = new Dictionary<string, SlotMaterials>();
			this.resources = new List<ResourceRef>();
			this.itemSlots = new Dictionary<TBody.SlotID, Item>();
			this.itemFiles = new Dictionary<string, Item>();
			this.resFiles = new Dictionary<string, ResourceRef>();
			this.addItems = new List<Item>();
			this.items = new List<string>();
			this.delItems = new List<string>();
			this.maskItems = new List<string>();
			this.delNodes = new List<string>();
			this.showNodes = new List<string>();
			this.delPartsNodes = new List<string[]>();
			this.showPartsNodes = new List<string[]>();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000B6B0 File Offset: 0x000098B0
		public void InitMaterials(string slotName, List<ACCMaterial> edited)
		{
			bool flag = false;
			for (int i = 0; i < edited.Count; i++)
			{
				TargetMaterial targetMaterial = this.GetMaterial(slotName, i);
				if (targetMaterial == null)
				{
					targetMaterial = new TargetMaterial(slotName, i, string.Empty)
					{
						onlyModel = true
					};
					this.AddSlotMaterial(targetMaterial);
					TBody.SlotID key = (TBody.SlotID)Enum.Parse(typeof(TBody.SlotID), slotName, false);
					Item item;
					this.itemSlots.TryGetValue(key, out item);
				}
				targetMaterial.Init(edited[i]);
				flag |= targetMaterial.shaderChanged;
				if (targetMaterial.onlyModel)
				{
					flag |= (targetMaterial.hasTexColorChanged | targetMaterial.hasTexFileChanged);
				}
			}
			foreach (Item item2 in this.addItems)
			{
				if (item2.slot == slotName)
				{
					item2.needUpdate = flag;
				}
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000B7B0 File Offset: 0x000099B0
		public string EditFileName()
		{
			return this.editfile + FileConst.EXT_MENU;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000B7C2 File Offset: 0x000099C2
		public string EditIconFileName()
		{
			return this.editicon + FileConst.EXT_TEXTURE;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000B7D4 File Offset: 0x000099D4
		public TargetMaterial GetMaterial(string slot, int matNo)
		{
			SlotMaterials slotMaterials;
			if (!this.slotMaterials.TryGetValue(slot, out slotMaterials))
			{
				return null;
			}
			return slotMaterials.Get(matNo);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000B7FC File Offset: 0x000099FC
		public void AddSlotMaterial(TargetMaterial tm)
		{
			LogUtil.Debug(new object[]
			{
				"Add slot material",
				tm.editname
			});
			SlotMaterials slotMaterials;
			if (!this.slotMaterials.TryGetValue(tm.slotName, out slotMaterials))
			{
				slotMaterials = new SlotMaterials();
				this.slotMaterials[tm.slotName] = slotMaterials;
			}
			slotMaterials.SetMaterial(tm);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000B85C File Offset: 0x00009A5C
		public static void WriteMenuFile(string filepath, ACCMenu menu)
		{
			try
			{
				bool flag;
				using (BinaryReader binaryReader = new BinaryReader(FileUtilEx.Instance.GetStream(menu.srcfilename, out flag), Encoding.UTF8))
				{
					string text = binaryReader.ReadString();
					if (flag || binaryReader.BaseStream.Position > 0L)
					{
						if (text == FileConst.HEAD_MENU)
						{
							ACCMenu.WriteMenuFile(binaryReader, text, filepath, menu);
							return;
						}
						StringBuilder stringBuilder = ACCMenu.headerError(text, menu.srcfilename);
						throw new ACCException(stringBuilder.ToString());
					}
				}
				using (BinaryReader binaryReader2 = new BinaryReader(new MemoryStream(FileUtilEx.Instance.LoadInternal(menu.srcfilename), false), Encoding.UTF8))
				{
					string text2 = binaryReader2.ReadString();
					if (text2 == FileConst.HEAD_MATE)
					{
						ACCMenu.WriteMenuFile(binaryReader2, text2, filepath, menu);
					}
					StringBuilder stringBuilder2 = ACCMenu.headerError(text2, menu.srcfilename);
					throw new ACCException(stringBuilder2.ToString());
				}
			}
			catch (ACCException)
			{
				throw;
			}
			catch (Exception ex)
			{
				string text3 = "menuファイルの作成に失敗しました。 file=" + filepath;
				LogUtil.Error(new object[]
				{
					text3,
					ex
				});
				throw new ACCException(text3, ex);
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000B9B8 File Offset: 0x00009BB8
		private static void WriteMenuFile(BinaryReader reader, string header, string filepath, ACCMenu menu)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					using (BinaryWriter binaryWriter2 = new BinaryWriter(File.OpenWrite(filepath)))
					{
						binaryWriter2.Write(header);
						binaryWriter2.Write(reader.ReadInt32());
						reader.ReadString();
						string text = menu.txtpath;
						if (!text.EndsWith(FileConst.EXT_TXT, StringComparison.OrdinalIgnoreCase))
						{
							text += FileConst.EXT_TXT;
						}
						binaryWriter2.Write(text);
						reader.ReadString();
						reader.ReadString();
						reader.ReadString();
						binaryWriter2.Write(menu.name);
						binaryWriter2.Write(menu.category);
						string text2 = menu.desc.Replace("\n", FileConst.RET);
						binaryWriter2.Write(text2);
						reader.ReadInt32();
						bool flag = false;
						for (;;)
						{
							int num = (int)reader.ReadByte();
							if (num == 0)
							{
								break;
							}
							string text3 = reader.ReadString();
							string[] array = new string[num - 1];
							for (int i = 0; i < num - 1; i++)
							{
								array[i] = reader.ReadString();
							}
							string key;
							switch (key = text3)
							{
							case "priority":
								array[0] = menu.priority;
								flag = true;
								break;
							case "name":
								array[0] = menu.name;
								break;
							case "setumei":
								array[0] = text2;
								break;
							case "icon":
							case "icons":
								array[0] = menu.EditIconFileName();
								break;
							case "additem":
							{
								string text4 = array[0];
								array[0] = menu.itemFiles[text4].EditFileName();
								LogUtil.Debug(new object[]
								{
									"modelfile replaces ",
									text4,
									"=>",
									array[0]
								});
								break;
							}
							case "マテリアル変更":
							{
								string slot = array[0];
								int matNo = int.Parse(array[1]);
								TargetMaterial material = menu.GetMaterial(slot, matNo);
								array[2] = material.EditFileName();
								break;
							}
							case "リソース参照":
								array[1] = menu.resFiles[array[1]].EditFileName();
								break;
							case "半脱ぎ":
								array[0] = menu.resFiles[array[0]].EditFileName();
								break;
							}
							binaryWriter.Write((byte)(array.Length + 1));
							binaryWriter.Write(text3);
							foreach (string value in array)
							{
								binaryWriter.Write(value);
							}
						}
						binaryWriter.Write(0);
						if (!flag)
						{
							binaryWriter.Write(2);
							binaryWriter.Write("priority");
							binaryWriter.Write(menu.priority);
						}
						binaryWriter2.Write((int)memoryStream.Length);
						binaryWriter2.Write(memoryStream.ToArray());
					}
				}
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000BD60 File Offset: 0x00009F60
		public static ACCMenu Load(string filename)
		{
			ACCMenu accmenu = new ACCMenu();
			LogUtil.Debug(new object[]
			{
				"loading menu file",
				filename
			});
			accmenu.srcfilename = filename;
			accmenu.editfile = Path.GetFileNameWithoutExtension(filename);
			ACCMenu result;
			try
			{
				bool flag;
				using (BinaryReader binaryReader = new BinaryReader(FileUtilEx.Instance.GetStream(accmenu.srcfilename, out flag), Encoding.UTF8))
				{
					string text = binaryReader.ReadString();
					if (flag || binaryReader.BaseStream.Position > 0L)
					{
						if (text == FileConst.HEAD_MENU)
						{
							ACCMenu.Load(binaryReader, accmenu.srcfilename, accmenu);
							LogUtil.Debug(new object[]
							{
								"menu loaded"
							});
							return accmenu;
						}
						ACCMenu.headerError(text, filename);
						return null;
					}
				}
				using (BinaryReader binaryReader2 = new BinaryReader(new MemoryStream(FileUtilEx.Instance.LoadInternal(accmenu.srcfilename), false), Encoding.UTF8))
				{
					string text2 = binaryReader2.ReadString();
					if (text2 == FileConst.HEAD_MENU)
					{
						ACCMenu.Load(binaryReader2, accmenu.srcfilename, accmenu);
						LogUtil.Debug(new object[]
						{
							"menu loaded"
						});
						result = accmenu;
					}
					else
					{
						ACCMenu.headerError(text2, filename);
						result = null;
					}
				}
			}
			catch (Exception ex)
			{
				LogUtil.Error(new object[]
				{
					"アイテムメニューファイルが読み込めませんでした。",
					filename,
					ex
				});
				result = null;
			}
			return result;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000BF28 File Offset: 0x0000A128
		private static StringBuilder headerError(string header, string filename)
		{
			if (header == FileConst.HEAD_MOD)
			{
				return LogUtil.Error(new object[]
				{
					"MODファイルは未対応。",
					filename
				});
			}
			return LogUtil.Error(new object[]
			{
				"MENUファイルのヘッダーファイルが正しくありません。",
				header,
				", ",
				filename
			});
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000BF84 File Offset: 0x0000A184
		private static void Load(BinaryReader reader, string filename, ACCMenu menu)
		{
			menu.version = reader.ReadInt32();
			menu.txtpath = reader.ReadString();
			reader.ReadString();
			reader.ReadString();
			reader.ReadString().Replace(FileConst.RET, "\n");
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
				// 뭔 스위치가 이따위로 디컴파일 되냐;;


					switch (text)
					{
					case "category":
						menu.category = array[0];
						continue;
					case "属性追加":
					case "color_set":
						continue;
					case "priority":
						menu.priority = array[0];
						continue;
					case "name":
						menu.name = array[0];
						continue;
					case "setumei":
						menu.desc = array[0].Replace(FileConst.RET, "\n");
						continue;
					case "icon":
					case "icons":
					menu.icon = array[0];
						menu.editicon = Path.GetFileNameWithoutExtension(menu.icon);
						continue;
					case "アイテムパラメータ":
						if (array.Length == 3)
						{
							menu.itemParam = array;
							continue;
						}
						continue;
					case "アイテム":
						menu.items.Add(Path.GetFileNameWithoutExtension(array[0]));
						continue;
					case "additem":
						{
						if (array.Length < 1)
						{
							continue;
						}
						Item item = new Item(array);
						if (!menu.itemFiles.TryGetValue(item.filename, out item.link))
						{
							menu.itemFiles[item.filename] = item;
						}
						menu.addItems.Add(item);
						try
						{
							if (item.slot != null)
							{
								TBody.SlotID key2 = (TBody.SlotID)Enum.Parse(typeof(TBody.SlotID), item.slot, true);
								menu.itemSlots[key2] = item;
							}
							continue;
						}
						catch (Exception ex)
						{
							LogUtil.Debug(new object[]
							{
								"failed to parse additem slot",
								item.slot,
								ex
							});
							continue;
						}
						break;
					}
					case "maskitem":
						break;
					case "delitem":
						{
						string item2 = menu.category;
						if (array.Length >= 1)
						{
							item2 = array[0];
						}
						menu.delItems.Add(item2);
						continue;
					}
					case "マテリアル変更":
						{
						string slot = array[0];
						int matNo = int.Parse(array[1]);
						string filename2 = array[2];
						menu.AddSlotMaterial(new TargetMaterial(slot, matNo, filename2));
						continue;
					}
					case "node消去":
						menu.delNodes.Add(array[0]);
						continue;
					case "node表示":
						menu.showNodes.Add(array[0]);
						continue;
					case "パーツnode消去":
						menu.delPartsNodes.Add(array);
						continue;
					case "パーツnode表示":
						menu.showPartsNodes.Add(array);
						continue;
					case "リソース参照":
						{
						ResourceRef resourceRef = new ResourceRef(array[0], array[1]);
						resourceRef.menu = menu;
						menu.resources.Add(resourceRef);
						if (!menu.resFiles.TryGetValue(resourceRef.filename, out resourceRef.link))
						{
							menu.resFiles[resourceRef.filename] = resourceRef;
							continue;
						}
						continue;
					}
					case "半脱ぎ":
						{
						ResourceRef resourceRef2 = new ResourceRef(text, array[0]);
						resourceRef2.menu = menu;
						menu.resources.Add(resourceRef2);
						if (!menu.resFiles.TryGetValue(resourceRef2.filename, out resourceRef2.link))
						{
							menu.resFiles[resourceRef2.filename] = resourceRef2;
							continue;
						}
						continue;
					}
					default:
						continue;
					}
					if (array.Length >= 1)
					{
						menu.maskItems.Add(array[0]);
					}

			}
		}

		// Token: 0x04000112 RID: 274
		public string category = string.Empty;

		// Token: 0x04000113 RID: 275
		public string priority = "1000";

		// Token: 0x04000114 RID: 276
		public string name = string.Empty;

		// Token: 0x04000115 RID: 277
		public string desc = string.Empty;

		// Token: 0x04000116 RID: 278
		public string icon = string.Empty;

		// Token: 0x04000117 RID: 279
		public string editicon = string.Empty;

		// Token: 0x04000118 RID: 280
		public bool editiconExist;

		// Token: 0x04000119 RID: 281
		public bool editfileExist;

		// Token: 0x0400011A RID: 282
		public string txtpath = string.Empty;
	}
}
