using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CM3D2.AlwaysColorChangeEx.Plugin.Data;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.UI
{
	// Token: 0x0200006D RID: 109
	public class ACCSaveMenuView
	{
		// Token: 0x06000356 RID: 854 RVA: 0x0001BF72 File Offset: 0x0001A172
		public static void Init(UIParams uiparams)
		{
			if (ACCSaveMenuView.uiParams != null)
			{
				return;
			}
			ACCSaveMenuView.uiParams = uiparams;
			ACCSaveMenuView.uiParams.Add(ACCSaveMenuView.updateUI);
			ACCSaveMenuView.InitUIParams(uiparams);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0001BF97 File Offset: 0x0001A197
		public static void Clear()
		{
			if (ACCSaveMenuView.uiParams != null)
			{
				ACCSaveMenuView.uiParams.Remove(ACCSaveMenuView.updateUI);
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0001BFB0 File Offset: 0x0001A1B0
		private static void InitUIParams(UIParams uiparam)
		{
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0001BFB2 File Offset: 0x0001A1B2
		public ACCSaveMenuView(UIParams up)
		{
			ACCSaveMenuView.Init(up);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0001BFD8 File Offset: 0x0001A1D8
		public Dictionary<TBody.SlotID, Item> Load(string filename)
		{
			this.trgtMenu = ACCMenu.Load(filename);
			this.nameInterlocked = false;
			if (this.trgtMenu != null)
			{
				this.showDialog = true;
				return this.trgtMenu.itemSlots;
			}
			AFileBase afileBase = GameUty.FileOpen(filename, null);
			if (!afileBase.IsValid())
			{
				return null;
			}
			string exportDirectory = OutputUtil.Instance.GetExportDirectory();
			string text = Path.Combine(exportDirectory, filename);
			LogUtil.Error(new object[]
			{
				"MENUファイルを出力します。",
				text
			});
			using (BinaryWriter binaryWriter = new BinaryWriter(File.OpenWrite(text)))
			{
				binaryWriter.Write(afileBase.ReadAll());
			}
			return null;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0001C08C File Offset: 0x0001A28C
		public void SetEditedMaterials(TBody.SlotID slot, List<ACCMaterial> edited)
		{
			LogUtil.DebugF("Set edited Materials. slot={0}, edited count={1}", new object[]
			{
				slot,
				edited.Count
			});
			string slotName = slot.ToString();
			this.trgtMenu.InitMaterials(slotName, edited);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0001C0DC File Offset: 0x0001A2DC
		public void Show()
		{
			if (this.trgtMenu == null)
			{
				return;
			}
			this.scrollViewPosition = GUILayout.BeginScrollView(this.scrollViewPosition, new GUILayoutOption[]
			{
				ACCSaveMenuView._optScrlWidth,
				ACCSaveMenuView._optScrlHeight
			});
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			GUILayout.Space((float)ACCSaveMenuView.uiParams.unitHeight);
			Color textColor = ACCSaveMenuView.uiParams.textStyle.normal.textColor;
			Color red = Color.red;
			try
			{
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.Label("メニュー", ACCSaveMenuView.uiParams.lStyle, new GUILayoutOption[]
				{
					ACCSaveMenuView._optLabelWidth
				});
				string editfile = this.trgtMenu.editfile;
				if (this.trgtMenu.editfileExist)
				{
					ACCSaveMenuView.uiParams.textStyle.normal.textColor = red;
				}
				this.trgtMenu.editfile = GUILayout.TextField(this.trgtMenu.editfile, ACCSaveMenuView.uiParams.textStyle, new GUILayoutOption[0]);
				if (this.trgtMenu.editfileExist)
				{
					ACCSaveMenuView.uiParams.textStyle.normal.textColor = textColor;
				}
				this.nameChanged |= (this.trgtMenu.editfile != editfile);
				GUILayout.Label(FileConst.EXT_MENU, ACCSaveMenuView.uiParams.lStyleS, new GUILayoutOption[]
				{
					ACCSaveMenuView._optExtLabelWidth
				});
				bool flag = this.nameInterlocked;
				this.nameInterlocked = GUILayout.Toggle(this.nameInterlocked, "名前連動", ACCSaveMenuView.uiParams.tStyleS, new GUILayoutOption[]
				{
					ACCSaveMenuView.uiParams.optToggleSWidth
				});
				if (this.nameInterlocked && flag != this.nameInterlocked)
				{
					this.nameChanged = true;
				}
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.Label("内部パス", ACCSaveMenuView.uiParams.lStyle, new GUILayoutOption[]
				{
					ACCSaveMenuView._optLabelWidth
				});
				this.trgtMenu.txtpath = GUILayout.TextField(this.trgtMenu.txtpath, ACCSaveMenuView.uiParams.textStyle, new GUILayoutOption[0]);
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.Label("優先度", ACCSaveMenuView.uiParams.lStyle, new GUILayoutOption[]
				{
					ACCSaveMenuView._optLabelWidth
				});
				string text = GUILayout.TextField(this.trgtMenu.priority, 10, ACCSaveMenuView.uiParams.textStyle, new GUILayoutOption[]
				{
					ACCSaveMenuView._modalHalfWidth
				});
				int num;
				if (this.trgtMenu.priority != text && int.TryParse(text, out num) && num >= 0)
				{
					this.trgtMenu.priority = num.ToString();
				}
				GUILayout.Space(ACCSaveMenuView._indentWidth);
				GUILayout.Label("カテゴリ", ACCSaveMenuView.uiParams.lStyleS, new GUILayoutOption[0]);
				GUILayout.Label(this.trgtMenu.category, ACCSaveMenuView.uiParams.lStyleS, new GUILayoutOption[0]);
				if (GUILayout.Button("↑ パス自動設定", ACCSaveMenuView.uiParams.bStyle, new GUILayoutOption[0]))
				{
					this.trgtMenu.txtpath = ACCSaveMenuView.settings.txtPrefixTex + this.trgtMenu.editfile;
				}
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.Label("アイコン", ACCSaveMenuView.uiParams.lStyle, new GUILayoutOption[]
				{
					ACCSaveMenuView._optLabelWidth
				});
				GUI.enabled = !this.nameInterlocked;
				if (this.nameInterlocked && this.nameChanged)
				{
					if (!this.trgtMenu.editfile.ToLower().EndsWith(ACCSaveMenuView.settings.iconSuffix, StringComparison.Ordinal))
					{
						this.trgtMenu.editicon = this.trgtMenu.editfile + ACCSaveMenuView.settings.iconSuffix;
					}
					else
					{
						this.trgtMenu.editicon = this.trgtMenu.editfile;
					}
				}
				if (this.trgtMenu.editfileExist)
				{
					ACCSaveMenuView.uiParams.textStyle.normal.textColor = red;
				}
				this.trgtMenu.editicon = GUILayout.TextField(this.trgtMenu.editicon, ACCSaveMenuView.uiParams.textStyle, new GUILayoutOption[0]);
				if (this.trgtMenu.editfileExist)
				{
					ACCSaveMenuView.uiParams.textStyle.normal.textColor = textColor;
				}
				GUI.enabled = true;
				GUILayout.Label(FileConst.EXT_TEXTURE, ACCSaveMenuView.uiParams.lStyleS, new GUILayoutOption[]
				{
					ACCSaveMenuView._optExtLabelWidth
				});
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.Label("名前", ACCSaveMenuView.uiParams.lStyle, new GUILayoutOption[]
				{
					ACCSaveMenuView._optLabelWidth
				});
				this.trgtMenu.name = GUILayout.TextField(this.trgtMenu.name, ACCSaveMenuView.uiParams.textStyle, new GUILayoutOption[0]);
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal(new GUILayoutOption[]
				{
					ACCSaveMenuView._optTwoLineHeight
				});
				GUILayout.Label("説明", ACCSaveMenuView.uiParams.lStyle, new GUILayoutOption[]
				{
					ACCSaveMenuView._optLabelWidth
				});
				this.trgtMenu.desc = GUILayout.TextArea(this.trgtMenu.desc, ACCSaveMenuView.uiParams.textAreaStyleS, new GUILayoutOption[]
				{
					ACCSaveMenuView._optTwoLineHeight
				});
				GUILayout.EndHorizontal();
				try
				{
					foreach (KeyValuePair<string, SlotMaterials> keyValuePair in this.trgtMenu.slotMaterials)
					{
						GUILayout.Label("マテリアル情報 (" + keyValuePair.Key + ")", ACCSaveMenuView.uiParams.lStyle, new GUILayoutOption[0]);
						foreach (TargetMaterial targetMaterial in keyValuePair.Value.materials)
						{
							if (targetMaterial != null)
							{
								GUILayout.BeginHorizontal(new GUILayoutOption[0]);
								try
								{
									GUILayout.Space(ACCSaveMenuView._indentWidth);
									if (targetMaterial.onlyModel)
									{
										ACCSaveMenuView.uiParams.lStyleS.normal.textColor = Color.cyan;
									}
									GUILayout.Label("マテリアル" + targetMaterial.matNo, ACCSaveMenuView.uiParams.lStyleS, new GUILayoutOption[]
									{
										ACCSaveMenuView._optSubLabelWidth
									});
									if (targetMaterial.onlyModel)
									{
										ACCSaveMenuView.uiParams.lStyleS.normal.textColor = textColor;
									}
									if (targetMaterial.onlyModel)
									{
										GUILayout.Label("(.mateファイル無し)", ACCSaveMenuView.uiParams.lStyleS, new GUILayoutOption[0]);
									}
									else
									{
										if (targetMaterial.editfileExist)
										{
											ACCSaveMenuView.uiParams.textStyle.normal.textColor = red;
										}
										GUI.enabled = !this.nameInterlocked;
										if (this.nameInterlocked && this.nameChanged)
										{
											targetMaterial.editfile = this.trgtMenu.editfile + targetMaterial.matNo;
										}
										targetMaterial.editfile = GUILayout.TextField(targetMaterial.editfile, ACCSaveMenuView.uiParams.textStyle, new GUILayoutOption[0]);
										if (targetMaterial.editfileExist)
										{
											ACCSaveMenuView.uiParams.textStyle.normal.textColor = textColor;
										}
										GUI.enabled = true;
										GUILayout.Label(FileConst.EXT_MATERIAL, ACCSaveMenuView.uiParams.lStyleS, new GUILayoutOption[]
										{
											ACCSaveMenuView._optExtLabelWidth
										});
										if (targetMaterial.needPmat && targetMaterial.needPmatChange)
										{
											GUILayout.Label("|" + FileConst.EXT_PMAT, ACCSaveMenuView.uiParams.lStyleS, new GUILayoutOption[]
											{
												ACCSaveMenuView._optExtLabelWidth
											});
										}
									}
									if (!targetMaterial.needPmat)
									{
										targetMaterial.editname = GUILayout.TextField(targetMaterial.editname, ACCSaveMenuView.uiParams.textStyle, new GUILayoutOption[0]);
									}
									else if (targetMaterial.needPmatChange)
									{
										targetMaterial.editname = targetMaterial.editfile;
										targetMaterial.editname = GUILayout.TextField(targetMaterial.editname, ACCSaveMenuView.uiParams.textStyle, new GUILayoutOption[0]);
									}
									else
									{
										GUI.enabled = false;
										targetMaterial.editname = GUILayout.TextField(targetMaterial.editname, ACCSaveMenuView.uiParams.textStyle, new GUILayoutOption[0]);
										GUI.enabled = true;
									}
								}
								catch (Exception ex)
								{
									LogUtil.Debug(new object[]
									{
										"failed to display material name:",
										targetMaterial.editname,
										ex
									});
								}
								finally
								{
									GUILayout.EndHorizontal();
								}
								GUILayout.BeginHorizontal(new GUILayoutOption[0]);
								try
								{
									GUILayout.Space(ACCSaveMenuView._indentWidth * 2f);
									string text2 = targetMaterial.uiTexViewed ? "－" : "＋";
									if (GUILayout.Button(text2, new GUILayoutOption[]
									{
										ACCSaveMenuView.uiParams.optBtnWidth
									}))
									{
										targetMaterial.uiTexViewed = !targetMaterial.uiTexViewed;
									}
									string str = targetMaterial.ShaderNameOrDefault("不明");
									GUILayout.Label("シェーダ : " + str, ACCSaveMenuView.uiParams.lStyleS, new GUILayoutOption[]
									{
										ACCSaveMenuView._optShaderWidth
									});
									GUILayout.Space(ACCSaveMenuView._indentWidth);
									ACCSaveMenuView.uiParams.lStyleS.normal.textColor = this.changedColor;
									GUILayout.Label(targetMaterial.shaderChanged ? "変更有" : "", ACCSaveMenuView.uiParams.lStyleS, new GUILayoutOption[0]);
									ACCSaveMenuView.uiParams.lStyleS.normal.textColor = textColor;
									if (!targetMaterial.needPmat)
									{
										GUILayout.Label("pmat不要(透過無)", ACCSaveMenuView.uiParams.lStyleS, new GUILayoutOption[]
										{
											ACCSaveMenuView._optLabelWidth
										});
									}
									else if (targetMaterial.needPmatChange)
									{
										GUI.enabled = false;
										targetMaterial.pmatExport = GUILayout.Toggle(targetMaterial.pmatExport, "pmat出力", ACCSaveMenuView.uiParams.lStyleS, new GUILayoutOption[]
										{
											ACCSaveMenuView._optLabelWidth
										});
										GUI.enabled = true;
									}
									else
									{
										GUILayout.Label("既存pmat利用", ACCSaveMenuView.uiParams.lStyleS, new GUILayoutOption[]
										{
											ACCSaveMenuView._optLabelWidth
										});
									}
								}
								catch (Exception ex2)
								{
									LogUtil.Debug(new object[]
									{
										"failed to display shader info:",
										targetMaterial.editname,
										ex2
									});
								}
								finally
								{
									GUILayout.EndHorizontal();
								}
								if (targetMaterial.uiTexViewed)
								{
									GUILayout.BeginVertical(new GUILayoutOption[0]);
									try
									{
										try
										{
											foreach (ShaderPropTex shaderPropTex in targetMaterial.editedMat.type.texProps)
											{
												TargetTexture targetTexture;
												if (targetMaterial.texDic.TryGetValue(shaderPropTex.key, out targetTexture) && !(targetTexture.tex == null))
												{
													GUILayout.BeginHorizontal(new GUILayoutOption[]
													{
														ACCSaveMenuView._optSubItemHeight
													});
													GUILayout.Space(ACCSaveMenuView._indentWidth * 4f);
													string keyName = shaderPropTex.keyName;
													GUILayout.Label(keyName, ACCSaveMenuView.uiParams.lStyleS, new GUILayoutOption[]
													{
														ACCSaveMenuView._optPropNameWidth
													});
													if (targetTexture.needOutput)
													{
														GUI.enabled = !this.nameInterlocked;
														if (this.nameInterlocked && this.nameChanged)
														{
															targetTexture.editname = targetMaterial.editfile + FileConst.GetTexSuffix(keyName);
														}
													}
													else
													{
														GUI.enabled = false;
													}
													if (!targetMaterial.onlyModel)
													{
														if (targetTexture.editnameExist)
														{
															ACCSaveMenuView.uiParams.textStyle.normal.textColor = red;
														}
														targetTexture.editname = GUILayout.TextField(targetTexture.editname, ACCSaveMenuView.uiParams.textStyle, new GUILayoutOption[0]);
														if (targetTexture.editnameExist)
														{
															ACCSaveMenuView.uiParams.textStyle.normal.textColor = textColor;
														}
														GUI.enabled = true;
														if (targetTexture.colorChanged)
														{
															ACCSaveMenuView.uiParams.lStyleS.normal.textColor = this.changedColor;
															GUILayout.Label("色変更", ACCSaveMenuView.uiParams.lStyleS, new GUILayoutOption[]
															{
																ACCSaveMenuView._optExtLabelWidth
															});
															ACCSaveMenuView.uiParams.lStyleS.normal.textColor = textColor;
														}
														else if (targetTexture.fileChanged)
														{
															ACCSaveMenuView.uiParams.lStyleS.normal.textColor = this.changedColor;
															GUILayout.Label("変更有", ACCSaveMenuView.uiParams.lStyleS, new GUILayoutOption[]
															{
																ACCSaveMenuView._optExtLabelWidth
															});
															ACCSaveMenuView.uiParams.lStyleS.normal.textColor = textColor;
														}
													}
													else
													{
														if (targetTexture.editnameExist)
														{
															ACCSaveMenuView.uiParams.lStyle.normal.textColor = red;
														}
														GUILayout.Label(targetTexture.editname, ACCSaveMenuView.uiParams.lStyleS, new GUILayoutOption[0]);
														if (targetTexture.editnameExist)
														{
															ACCSaveMenuView.uiParams.lStyle.normal.textColor = textColor;
														}
													}
													GUI.enabled = true;
													GUILayout.EndHorizontal();
												}
											}
										}
										catch (Exception ex3)
										{
											LogUtil.Debug(new object[]
											{
												"failed to display tex info.",
												ex3
											});
										}
										continue;
									}
									finally
									{
										GUI.enabled = true;
										GUILayout.EndVertical();
									}
								}
								if (this.nameInterlocked && this.nameChanged)
								{
									foreach (ShaderPropTex shaderPropTex2 in targetMaterial.editedMat.type.texProps)
									{
										TargetTexture targetTexture2;
										if (targetMaterial.texDic.TryGetValue(shaderPropTex2.key, out targetTexture2) && !(targetTexture2.tex == null) && targetTexture2.needOutput)
										{
											targetTexture2.editname = targetMaterial.editfile + FileConst.GetTexSuffix(shaderPropTex2.keyName);
										}
									}
								}
							}
						}
					}
				}
				catch (Exception ex4)
				{
					LogUtil.Error(new object[]
					{
						"failed to display material",
						ex4
					});
				}
				if (this.trgtMenu.addItems.Any<Item>())
				{
					GUILayout.Label("additem (model)", ACCSaveMenuView.uiParams.lStyle, new GUILayoutOption[0]);
					foreach (Item item in this.trgtMenu.addItems)
					{
						try
						{
							if (item.HasSlot())
							{
								GUILayout.BeginHorizontal(new GUILayoutOption[0]);
								GUILayout.Space(ACCSaveMenuView._indentWidth);
								GUILayout.Label(item.slot, ACCSaveMenuView.uiParams.lStyleS, new GUILayoutOption[]
								{
									ACCSaveMenuView._optLabelWidth
								});
								if (item.editnameExist)
								{
									ACCSaveMenuView.uiParams.textStyle.normal.textColor = red;
									ACCSaveMenuView.uiParams.lStyleS.normal.textColor = red;
								}
								if (item.HasLink())
								{
									GUILayout.Label(item.link.EditFileName(), ACCSaveMenuView.uiParams.lStyleS, new GUILayoutOption[0]);
								}
								else if (!item.needUpdate)
								{
									GUILayout.Label(item.EditFileName(), ACCSaveMenuView.uiParams.lStyleS, new GUILayoutOption[0]);
								}
								else
								{
									GUI.enabled = !this.nameInterlocked;
									if (this.nameInterlocked && this.nameChanged)
									{
										string modelSuffix = FileConst.GetModelSuffix(item.slot);
										if (this.trgtMenu.editfile.Contains(modelSuffix))
										{
											item.editname = this.trgtMenu.editfile;
										}
										else
										{
											item.editname = this.trgtMenu.editfile + modelSuffix;
										}
									}
									item.editname = GUILayout.TextField(item.editname, ACCSaveMenuView.uiParams.textStyle, new GUILayoutOption[0]);
									GUI.enabled = true;
									GUILayout.Label(FileConst.EXT_MODEL, ACCSaveMenuView.uiParams.lStyleS, new GUILayoutOption[]
									{
										ACCSaveMenuView._optExtLabelWidth
									});
								}
								if (item.editnameExist)
								{
									ACCSaveMenuView.uiParams.textStyle.normal.textColor = textColor;
									ACCSaveMenuView.uiParams.lStyleS.normal.textColor = textColor;
								}
								GUILayout.EndHorizontal();
								if (item.info.Length >= 3)
								{
									GUILayout.BeginHorizontal(new GUILayoutOption[0]);
									GUILayout.Space(ACCSaveMenuView._indentWidth);
									GUILayout.Label("追加情報", ACCSaveMenuView.uiParams.lStyleS, new GUILayoutOption[]
									{
										ACCSaveMenuView._optLabelWidth
									});
									StringBuilder stringBuilder = new StringBuilder();
									for (int k = 2; k < item.info.Length; k++)
									{
										stringBuilder.Append(item.info[k]).Append(", ");
									}
									GUILayout.Label(stringBuilder.ToString(), ACCSaveMenuView.uiParams.lStyleS, new GUILayoutOption[0]);
									GUILayout.EndHorizontal();
								}
							}
						}
						catch (Exception ex5)
						{
							LogUtil.Debug(new object[]
							{
								"failed to display item info:",
								item.slot,
								ex5
							});
						}
					}
				}
				if (this.trgtMenu.resources.Any<ResourceRef>())
				{
					try
					{
						GUILayout.Label("リソース参照", ACCSaveMenuView.uiParams.lStyle, new GUILayoutOption[0]);
						foreach (ResourceRef resourceRef in this.trgtMenu.resources)
						{
							GUILayout.BeginHorizontal(new GUILayoutOption[0]);
							GUILayout.Space(ACCSaveMenuView._indentWidth);
							GUILayout.Label(resourceRef.key, ACCSaveMenuView.uiParams.lStyleS, new GUILayoutOption[]
							{
								ACCSaveMenuView._optSubLabelWidth
							});
							if (this.nameInterlocked && this.nameChanged)
							{
								resourceRef.editname = this.trgtMenu.editfile + resourceRef.suffix;
							}
							GUI.enabled = !this.nameInterlocked;
							if (resourceRef.editfileExist)
							{
								ACCSaveMenuView.uiParams.textStyle.normal.textColor = red;
							}
							resourceRef.editname = GUILayout.TextField(resourceRef.editname, ACCSaveMenuView.uiParams.textStyle, new GUILayoutOption[0]);
							if (resourceRef.editfileExist)
							{
								ACCSaveMenuView.uiParams.textStyle.normal.textColor = textColor;
							}
							GUI.enabled = true;
							GUILayout.Label(FileConst.EXT_MENU, ACCSaveMenuView.uiParams.lStyleS, new GUILayoutOption[]
							{
								ACCSaveMenuView._optExtLabelWidth
							});
							GUILayout.EndHorizontal();
						}
					}
					catch (Exception ex6)
					{
						LogUtil.Debug(new object[]
						{
							"failed to display resource info.",
							ex6
						});
					}
				}
				this.nameChanged = false;
			}
			finally
			{
				GUILayout.EndVertical();
				GUILayout.EndScrollView();
			}
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Space((float)ACCSaveMenuView.uiParams.marginL);
			this.ignoreExist = !GUILayout.Toggle(!this.ignoreExist, "登録済確認", ACCSaveMenuView.uiParams.tStyleS, new GUILayoutOption[]
			{
				ACCSaveMenuView.uiParams.optToggleSWidth
			});
			if (GUILayout.Button("保存", ACCSaveMenuView.uiParams.bStyle, new GUILayoutOption[0]))
			{
				if (this.IsWritable(this.trgtMenu, this.ignoreExist))
				{
					if (this.SaveFiles(this.trgtMenu))
					{
						string f_strMessage = "エクスポートが完了しました。出力先=" + ACCSaveMenuView.fileUtil.GetACCDirectory(this.trgtMenu.editfile);
						NUty.WinMessageBox(NUty.GetWindowHandle(), f_strMessage, "情報", 0);
					}
				}
				else
				{
					NUty.WinMessageBox(NUty.GetWindowHandle(), "出力ファイルが登録済みか重複が存在するため、保存処理を行いませんでした。", "エラー", 0);
				}
			}
			if (GUILayout.Button("閉じる", ACCSaveMenuView.uiParams.bStyle, new GUILayoutOption[0]))
			{
				this.showDialog = false;
			}
			GUILayout.Space((float)ACCSaveMenuView.uiParams.marginL);
			GUILayout.EndHorizontal();
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0001D63C File Offset: 0x0001B83C
		private bool IsWritable(ACCMenu menu, bool ignoreExists)
		{
			if (menu.editfile.Length == 0)
			{
				return false;
			}
			bool flag = false;
			string text = ACCSaveMenuView.fileUtil.GetACCDirectory();
			text = Path.Combine(text, this.trgtMenu.editfile);
			if (!ignoreExists && Directory.Exists(text))
			{
				LogUtil.Debug(new object[]
				{
					"output directory already exist :",
					text
				});
				menu.editfileExist = true;
				return false;
			}
			bool flag2 = false;
			HashSet<string> hashSet = new HashSet<string>();
			string text2 = menu.EditFileName();
			hashSet.Add(text2);
			menu.editfileExist = false;
			if (ACCSaveMenuView.fileUtil.Exists(text2))
			{
				LogUtil.Debug(new object[]
				{
					"already exist:",
					text2
				});
				flag = true;
				menu.editfileExist = true;
			}
			menu.editiconExist = false;
			string text3 = menu.EditIconFileName();
			if (ACCSaveMenuView.fileUtil.Exists(text3))
			{
				LogUtil.Debug(new object[]
				{
					"already exist:",
					text3
				});
				flag = true;
				menu.editiconExist = true;
			}
			hashSet.Add(text3);
			foreach (KeyValuePair<string, Item> keyValuePair in menu.itemFiles)
			{
				Item value = keyValuePair.Value;
				if (value.needUpdate)
				{
					value.editnameExist = false;
					string text4 = value.EditFileName();
					if (this.HasAlreadyWritten(hashSet, text4))
					{
						flag2 = true;
						value.editnameExist = true;
					}
					else if (ACCSaveMenuView.fileUtil.Exists(text4))
					{
						LogUtil.Debug(new object[]
						{
							"already exist:",
							text4
						});
						flag = true;
						value.editnameExist = true;
					}
				}
			}
			foreach (SlotMaterials slotMaterials in menu.slotMaterials.Values)
			{
				foreach (TargetMaterial targetMaterial in slotMaterials.materials)
				{
					if (!targetMaterial.onlyModel)
					{
						targetMaterial.editfileExist = false;
						string text5 = targetMaterial.EditFileName();
						if (this.HasAlreadyWritten(hashSet, text5))
						{
							flag2 = true;
							targetMaterial.editfileExist = true;
						}
						else
						{
							if (ACCSaveMenuView.fileUtil.Exists(text5))
							{
								LogUtil.Debug(new object[]
								{
									"already exist:",
									text5
								});
								flag = true;
								targetMaterial.editfileExist = true;
							}
							if (targetMaterial.needPmatChange)
							{
								string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text5);
								string text6 = fileNameWithoutExtension + FileConst.EXT_PMAT;
								if (this.HasAlreadyWritten(hashSet, text6))
								{
									flag2 = true;
									targetMaterial.editfileExist = true;
									continue;
								}
								if (ACCSaveMenuView.fileUtil.Exists(text6))
								{
									LogUtil.Debug(new object[]
									{
										"already exist:",
										text6
									});
									flag = true;
									targetMaterial.editfileExist = true;
								}
							}
							foreach (TargetTexture targetTexture in targetMaterial.texDic.Values)
							{
								if (targetTexture.needOutput)
								{
									targetTexture.editnameExist = false;
									string text7 = targetTexture.EditFileName();
									if (this.HasAlreadyWritten(hashSet, text7))
									{
										flag2 = true;
										targetTexture.editnameExist = true;
									}
									else if (ACCSaveMenuView.fileUtil.Exists(text7))
									{
										LogUtil.Debug(new object[]
										{
											"already exist:",
											text7
										});
										flag = true;
										targetTexture.editnameExist = true;
									}
								}
							}
						}
					}
				}
			}
			foreach (ResourceRef resourceRef in menu.resFiles.Values)
			{
				resourceRef.editfileExist = false;
				string text8 = resourceRef.EditFileName();
				if (this.HasAlreadyWritten(hashSet, text8))
				{
					flag2 = true;
					resourceRef.editfileExist = true;
				}
				else if (ACCSaveMenuView.fileUtil.Exists(text8))
				{
					LogUtil.Debug(new object[]
					{
						"already exist:",
						text8
					});
					flag = true;
					resourceRef.editfileExist = true;
				}
			}
			return !flag2 && (ignoreExists || !flag);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0001DAFC File Offset: 0x0001BCFC
		public bool SaveFiles(ACCMenu menu)
		{
			string accdirectory = ACCSaveMenuView.fileUtil.GetACCDirectory(this.trgtMenu.editfile);
			if (!Directory.Exists(accdirectory))
			{
				Directory.CreateDirectory(accdirectory);
			}
			LogUtil.Debug(new object[]
			{
				"output path:",
				accdirectory
			});
			string filepath = Path.Combine(accdirectory, menu.EditFileName());
			ACCMenu.WriteMenuFile(filepath, menu);
			HashSet<string> hashSet = new HashSet<string>();
			string text = Path.Combine(accdirectory, menu.EditIconFileName());
			hashSet.Add(text);
			string txtpath = ACCSaveMenuView.settings.txtPrefixTex + text;
			ACCSaveMenuView.fileUtil.CopyTex(menu.icon, text, txtpath, null);
			LogUtil.Debug(new object[]
			{
				"tex file:",
				text
			});
			foreach (KeyValuePair<string, Item> keyValuePair in menu.itemFiles)
			{
				string key = keyValuePair.Key;
				Item value = keyValuePair.Value;
				if (value.needUpdate)
				{
					string text2 = value.EditFileName();
					if (!this.HasAlreadyWritten(hashSet, text2))
					{
						string text3 = Path.Combine(accdirectory, text2);
						SlotMaterials slotMat = menu.slotMaterials[value.slot];
						ACCSaveMenuView.fileUtil.WriteModelFile(key, text3, slotMat);
						LogUtil.Debug(new object[]
						{
							"model file:",
							text3
						});
					}
				}
			}
			foreach (SlotMaterials slotMaterials in menu.slotMaterials.Values)
			{
				foreach (TargetMaterial targetMaterial in slotMaterials.materials)
				{
					if (!targetMaterial.onlyModel)
					{
						string text4 = targetMaterial.EditFileName();
						if (this.HasAlreadyWritten(hashSet, text4))
						{
							continue;
						}
						string text5 = Path.Combine(accdirectory, text4);
						ACCSaveMenuView.fileUtil.WriteMateFile(targetMaterial.filename, text5, targetMaterial);
						LogUtil.Debug(new object[]
						{
							"mate file:",
							text5
						});
						if (targetMaterial.needPmatChange)
						{
							string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text4);
							string path = fileNameWithoutExtension + FileConst.EXT_PMAT;
							string text6 = Path.Combine(accdirectory, path);
							ACCSaveMenuView.fileUtil.WritePmat(text6, targetMaterial.editname, targetMaterial.RenderQueue(), targetMaterial.ShaderName());
							LogUtil.Debug(new object[]
							{
								"pmat file:",
								text6
							});
						}
					}
					foreach (TargetTexture targetTexture in targetMaterial.texDic.Values)
					{
						if (targetTexture.needOutput)
						{
							Texture2D texture2D = targetTexture.tex as Texture2D;
							if (texture2D == null)
							{
								LogUtil.Debug(new object[]
								{
									"tex is not Texture2D",
									targetTexture.editname
								});
							}
							else
							{
								string text7 = targetTexture.EditFileName();
								if (!this.HasAlreadyWritten(hashSet, text7))
								{
									string text8 = Path.Combine(accdirectory, text7);
									ACCSaveMenuView.fileUtil.WriteTexFile(text8, targetTexture.EditTxtPath(), texture2D.EncodeToPNG());
									LogUtil.Debug(new object[]
									{
										"tex file:",
										text8
									});
								}
							}
						}
					}
				}
			}
			foreach (ResourceRef resourceRef in menu.resFiles.Values)
			{
				string text9 = resourceRef.EditFileName();
				if (!this.HasAlreadyWritten(hashSet, text9))
				{
					string text10 = Path.Combine(accdirectory, text9);
					IEnumerable<ReplacedInfo> enumerable = ACCSaveMenuView.fileUtil.WriteMenuFile(resourceRef.filename, text10, resourceRef);
					LogUtil.Debug(new object[]
					{
						"menu file:",
						text10
					});
					foreach (ReplacedInfo replacedInfo in enumerable)
					{
						if (replacedInfo.item != null)
						{
							string replaced = replacedInfo.replaced;
							if (!this.HasAlreadyWritten(hashSet, replaced))
							{
								string outfilepath = Path.Combine(accdirectory, replaced);
								SlotMaterials slotMat2 = menu.slotMaterials[replacedInfo.item.slot];
								ACCSaveMenuView.fileUtil.WriteModelFile(replacedInfo.source, outfilepath, slotMat2);
							}
						}
						else if (replacedInfo.material != null)
						{
							TargetMaterial material = replacedInfo.material;
							string replaced2 = replacedInfo.replaced;
							if (!this.HasAlreadyWritten(hashSet, replaced2))
							{
								string outfilepath2 = Path.Combine(accdirectory, replaced2);
								ACCSaveMenuView.fileUtil.WriteMateFile(replacedInfo.source, outfilepath2, material);
								foreach (TargetTexture targetTexture2 in material.texDic.Values)
								{
									if (targetTexture2.needOutput)
									{
										Texture2D texture2D2 = targetTexture2.tex as Texture2D;
										if (texture2D2 == null)
										{
											LogUtil.Debug(new object[]
											{
												"tex is not 2D",
												targetTexture2.editname
											});
										}
										else
										{
											string text11 = targetTexture2.EditFileName();
											if (!this.HasAlreadyWritten(hashSet, text11))
											{
												Texture2D texture2D3 = null;
												Texture2D texture2D4 = null;
												if (!targetTexture2.fileChanged)
												{
													string text12 = targetTexture2.workfilename + FileConst.EXT_TEXTURE;
													if (!ACCSaveMenuView.fileUtil.Exists(text12))
													{
														LogUtil.LogF("リソース参照で使用されているtexファイル({0})が見つかりません。texファイルを出力できません。", new object[]
														{
															text12
														});
														continue;
													}
													texture2D3 = TexUtil.Instance.Load(text12);
													texture2D2 = texture2D3;
												}
												if (targetTexture2.colorChanged)
												{
													texture2D4 = ACCTexturesView.Filter(texture2D2, targetTexture2.filter);
													texture2D2 = texture2D4;
												}
												string text13 = Path.Combine(accdirectory, text11);
												ACCSaveMenuView.fileUtil.WriteTexFile(text13, targetTexture2.EditTxtPath(), texture2D2.EncodeToPNG());
												LogUtil.Debug(new object[]
												{
													"tex file:",
													text13
												});
												if (texture2D3 != null)
												{
													UnityEngine.Object.DestroyImmediate(texture2D3);
												}
												if (texture2D4 != null)
												{
													UnityEngine.Object.DestroyImmediate(texture2D4);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0001E228 File Offset: 0x0001C428
		private bool HasAlreadyWritten(ICollection<string> writtenFiles, string filename)
		{
			if (writtenFiles.Contains(filename))
			{
				LogUtil.DebugF("{0} has already been written.", new object[]
				{
					filename
				});
				return true;
			}
			writtenFiles.Add(filename);
			return false;
		}

		// Token: 0x040003E1 RID: 993
		private static readonly Settings settings = Settings.Instance;

		// Token: 0x040003E2 RID: 994
		private static readonly FileUtilEx fileUtil = FileUtilEx.Instance;

		// Token: 0x040003E3 RID: 995
		private static UIParams uiParams;

		// Token: 0x040003E4 RID: 996
		private static int _fontSize;

		// Token: 0x040003E5 RID: 997
		private static int _fontSizeS;

		// Token: 0x040003E6 RID: 998
		private static float _indentWidth;

		// Token: 0x040003E7 RID: 999
		private static GUILayoutOption _optLabelWidth;

		// Token: 0x040003E8 RID: 1000
		private static GUILayoutOption _modalHalfWidth;

		// Token: 0x040003E9 RID: 1001
		private static GUILayoutOption _optSubLabelWidth;

		// Token: 0x040003EA RID: 1002
		private static GUILayoutOption _optSubItemHeight;

		// Token: 0x040003EB RID: 1003
		private static GUILayoutOption _optExtLabelWidth;

		// Token: 0x040003EC RID: 1004
		private static GUILayoutOption _optShaderWidth;

		// Token: 0x040003ED RID: 1005
		private static GUILayoutOption _optPropNameWidth;

		// Token: 0x040003EE RID: 1006
		private static GUILayoutOption _optScrlWidth;

		// Token: 0x040003EF RID: 1007
		private static GUILayoutOption _optScrlHeight;

		// Token: 0x040003F0 RID: 1008
		private static GUILayoutOption _optTwoLineHeight;

		// Token: 0x040003F1 RID: 1009
		private static Action<UIParams> updateUI = delegate(UIParams uiparams)
		{
			ACCSaveMenuView._optLabelWidth = GUILayout.Width(uiparams.modalRect.width * 0.16f);
			ACCSaveMenuView._modalHalfWidth = GUILayout.Width(uiparams.modalRect.width * 0.34f);
			ACCSaveMenuView._optSubLabelWidth = GUILayout.Width(uiparams.modalRect.width * 0.22f);
			ACCSaveMenuView._optSubItemHeight = GUILayout.MaxHeight((float)uiparams.itemHeight * 0.8f);
			ACCSaveMenuView._fontSize = uiparams.fontSize;
			ACCSaveMenuView._fontSizeS = uiparams.fontSizeS;
			ACCSaveMenuView._optExtLabelWidth = GUILayout.Width((float)(ACCSaveMenuView._fontSizeS * 3));
			ACCSaveMenuView._optShaderWidth = GUILayout.Width((float)(ACCSaveMenuView._fontSizeS * ShaderType.MaxNameLength()) * 0.68f);
			ACCSaveMenuView._optPropNameWidth = GUILayout.Width((float)(ACCSaveMenuView._fontSizeS * 14) * 0.68f);
			ACCSaveMenuView._indentWidth = (float)uiparams.margin * 8f;
			ACCSaveMenuView._optScrlWidth = GUILayout.Width(uiparams.modalRect.width - 20f);
			ACCSaveMenuView._optScrlHeight = GUILayout.Height(uiparams.modalRect.height - 55f);
			ACCSaveMenuView._optTwoLineHeight = GUILayout.MinHeight((float)uiparams.unitHeight * 2.5f);
		};

		// Token: 0x040003F2 RID: 1010
		public ComboBox shaderCombo;

		// Token: 0x040003F3 RID: 1011
		public bool showDialog;

		// Token: 0x040003F4 RID: 1012
		private Vector2 scrollViewPosition = Vector2.zero;

		// Token: 0x040003F5 RID: 1013
		private bool nameInterlocked;

		// Token: 0x040003F6 RID: 1014
		private bool nameChanged;

		// Token: 0x040003F7 RID: 1015
		private bool ignoreExist;

		// Token: 0x040003F8 RID: 1016
		private Color changedColor = Color.red;

		// Token: 0x040003F9 RID: 1017
		public ACCMenu trgtMenu;
	}
}
