using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CM3D2.AlwaysColorChangeEx.Plugin.Data;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;
using CM3D2.AlwaysColorChangeEx.Plugin.Util.Json;
using JsonFx.Json;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin
{
	// Token: 0x0200000C RID: 12
	public class PresetManager
	{
		// Token: 0x06000064 RID: 100 RVA: 0x000071D2 File Offset: 0x000053D2
		public string GetPresetFilepath(string presetName)
		{
			return Path.Combine(this._settings.presetDirPath, presetName + FileConst.EXT_JSON);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000071F0 File Offset: 0x000053F0
		public PresetData Load(string fileName)
		{
			PresetData result;
			try
			{
				using (FileStream fileStream = File.OpenRead(fileName))
				{
					JsonReader jsonReader = new JsonReader(fileStream);
					result = (PresetData)jsonReader.Deserialize(typeof(PresetData));
				}
			}
			catch (Exception ex)
			{
				LogUtil.Log(new object[]
				{
					"ACCプリセットの読み込みに失敗しました",
					ex
				});
				result = null;
			}
			return result;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00007270 File Offset: 0x00005470
		public void Save(string fileName, string presetName, Dictionary<string, bool> dDelNodes)
		{
			Maid currentMaid = this._holder.CurrentMaid;
			PresetData presetData = new PresetData
			{
				name = presetName
			};
			foreach (SlotInfo slotInfo in ACConstants.SlotNames.Values)
			{
				if (slotInfo.enable)
				{
					TBodySkin slot = currentMaid.body0.GetSlot((int)slotInfo.Id);
					SlotState mask;
					if (slot.obj == null)
					{
						mask = SlotState.NotLoaded;
					}
					else if (!slot.boVisible)
					{
						mask = SlotState.Masked;
					}
					else
					{
						mask = SlotState.Displayed;
					}
					Material[] materials = this._holder.GetMaterials(slot);
					if (materials.Length != 0)
					{
						CCSlot ccslot = new CCSlot(slotInfo.Id)
						{
							mask = mask
						};
						foreach (Material material in materials)
						{
							ShaderType shaderType = ShaderType.Resolve(material.shader.name);
							if (shaderType != ShaderType.UNKNOWN)
							{
								CCMaterial ccmaterial = new CCMaterial(material, shaderType);
								ccslot.Add(ccmaterial);
								foreach (ShaderPropTex shaderPropTex in shaderType.texProps)
								{
									Texture2D texture2D = material.GetTexture(shaderPropTex.propId) as Texture2D;
									if (!(texture2D == null) && !string.IsNullOrEmpty(texture2D.name))
									{
										TextureInfo textureInfo = new TextureInfo();
										ccmaterial.Add(textureInfo);
										textureInfo.propName = shaderPropTex.keyName;
										textureInfo.texFile = texture2D.name;
										TextureModifier.FilterParam filter = this._texModifier.GetFilter(currentMaid, slotInfo.Id.ToString(), material.name, texture2D.name);
										if (filter != null && !filter.HasNotChanged())
										{
											textureInfo.filter = new TexFilter(filter);
										}
										Vector2 textureOffset = material.GetTextureOffset(shaderPropTex.propId);
										if (Math.Abs(textureOffset.x) > 0.001f)
										{
											textureInfo.offsetX = new float?(textureOffset.x);
										}
										if (Math.Abs(textureOffset.y) > 0.001f)
										{
											textureInfo.offsetY = new float?(textureOffset.y);
										}
										Vector2 textureScale = material.GetTextureScale(shaderPropTex.propId);
										if (Math.Abs(textureScale.x) > 0.001f)
										{
											textureInfo.scaleX = new float?(textureScale.x);
										}
										if (Math.Abs(textureScale.y) > 0.001f)
										{
											textureInfo.scaleY = new float?(textureScale.y);
										}
									}
								}
							}
						}
						presetData.slots.Add(ccslot);
					}
				}
			}
			for (int k = TypeUtil.BODY_START; k <= TypeUtil.BODY_END; k++)
			{
				MPN mpn = (MPN)Enum.ToObject(typeof(MPN), k);
				MaidProp prop = currentMaid.GetProp(mpn);
				if (prop != null)
				{
					if (prop.type == 1 || prop.type == 2)
					{
						presetData.mpnvals.Add(new CCMPNValue(mpn, prop.value, prop.min, prop.max));
					}
					else if (prop.type == 3 && prop.nFileNameRID != 0)
					{
						presetData.mpns.Add(new CCMPN(mpn, prop.strFileName));
					}
				}
			}
			for (int l = TypeUtil.WEAR_START; l <= TypeUtil.WEAR_END; l++)
			{
				MPN mpn2 = (MPN)Enum.ToObject(typeof(MPN), l);
				MaidProp prop2 = currentMaid.GetProp(mpn2);
				if (prop2 != null && prop2.nFileNameRID != 0)
				{
					presetData.mpns.Add(new CCMPN(mpn2, prop2.strFileName));
				}
			}
			for (MaidParts.PARTS_COLOR parts_COLOR = TypeUtil.PARTS_COLOR_START; parts_COLOR <= TypeUtil.PARTS_COLOR_END; parts_COLOR++)
			{
				MaidParts.PartsColor partsColor = currentMaid.Parts.GetPartsColor(parts_COLOR);
				presetData.partsColors[parts_COLOR.ToString()] = new CCPartsColor(partsColor);
			}
			presetData.delNodes = new Dictionary<string, bool>(dDelNodes);
			LogUtil.Debug(new object[]
			{
				"create preset...",
				fileName
			});
			this.SavePreset(fileName, presetData);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000076D0 File Offset: 0x000058D0
		public void SavePreset(string fileName, PresetData preset)
		{
			if (File.Exists(fileName))
			{
				File.Delete(fileName);
			}
			JsonWriterSettings settings = new JsonWriterSettings
			{
				MaxDepth = 200,
				PrettyPrint = true
			};
			using (FileStream fileStream = File.OpenWrite(fileName))
			{
				using (CustomJsonWriter customJsonWriter = new CustomJsonWriter(fileStream, settings))
				{
					customJsonWriter.ignoreNull = true;
					customJsonWriter.Write(preset);
				}
			}
			LogUtil.Debug(new object[]
			{
				"preset saved...",
				fileName
			});
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00007774 File Offset: 0x00005974
		public void ApplyPresetMPN(Maid maid, PresetData preset, bool applyBody, bool applyWear, bool castoff)
		{
			foreach (CCMPN ccmpn in preset.mpns)
			{
				if ((applyBody || !TypeUtil.IsBody(ccmpn.name)) && (applyWear || !TypeUtil.IsWear(ccmpn.name)) && this._fileUtil.Exists(ccmpn.filename))
				{
					MaidProp prop = maid.GetProp(ccmpn.name);
					if (ccmpn.filename.Equals(prop.strFileName, StringComparison.OrdinalIgnoreCase))
					{
						LogUtil.Debug(new object[]
						{
							"apply preset skip. mpn:",
							ccmpn.name,
							", file:",
							ccmpn.filename
						});
					}
					else if (ccmpn.name == MPN.MayuThick)
					{
						LogUtil.Log(new object[]
						{
							"ACCexプリセットのbodyメニューの適用は現在未対応です。スキップします。",
							ccmpn.filename
						});
					}
					else if (ccmpn.filename.EndsWith("_del.menu", StringComparison.OrdinalIgnoreCase))
					{
						if (castoff && (prop.nFileNameRID != 0 || !CM3.dicDelItem[ccmpn.name].Equals(ccmpn.filename, StringComparison.OrdinalIgnoreCase)) && PresetManager.SetProp != null)
						{
							PresetManager.SetProp(maid, ccmpn.name, ccmpn.filename, 0);
						}
					}
					else if (PresetManager.SetProp != null)
					{
						PresetManager.SetProp(maid, ccmpn.name, ccmpn.filename, 0);
					}
				}
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00007918 File Offset: 0x00005B18
		public void ApplyPresetMPNProp(Maid maid, PresetData preset)
		{
			foreach (CCMPNValue ccmpnvalue in preset.mpnvals)
			{
				MaidProp prop = maid.GetProp(ccmpnvalue.name);
				if (prop != null)
				{
					prop.value = ccmpnvalue.value;
					if (prop.min > ccmpnvalue.min)
					{
						prop.min = ccmpnvalue.min;
					}
					if (prop.max < ccmpnvalue.max)
					{
						prop.max = ccmpnvalue.max;
					}
				}
				else
				{
					LogUtil.Debug(new object[]
					{
						"failed to apply MaidProp. mpn:",
						ccmpnvalue.name
					});
				}
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000079DC File Offset: 0x00005BDC
		public void ApplyPresetMaterial(Maid maid, PresetData preset)
		{
			if (maid == null)
			{
				maid = this._holder.CurrentMaid;
			}
			if (maid == null)
			{
				return;
			}
			foreach (CCSlot ccslot in preset.slots)
			{
				int id = (int)ccslot.id;
				if (id < maid.body0.goSlot.Count)
				{
					TBodySkin slot = maid.body0.GetSlot(id);
					Material[] materials = this._holder.GetMaterials(slot);
					if (slot.obj == null)
					{
						LogUtil.Debug(new object[]
						{
							"slot.obj null. name=",
							ccslot.id
						});
					}
					if (materials.Any<Material>())
					{
						string text = ccslot.id.ToString();
						int num = -1;
						foreach (CCMaterial ccmaterial in ccslot.materials)
						{
							if (++num < materials.Length)
							{
								Material material = materials[num];
								if (ccmaterial.name != material.name)
								{
									LogUtil.DebugF("Material name mismatched. skipping apply preset-slot={0}, matNo={1}, name=({2}<=>{3})", new object[]
									{
										ccslot.id,
										num,
										ccmaterial.name,
										material.name
									});
									continue;
								}
								ccmaterial.Apply(material);
								List<TextureInfo> texList = ccmaterial.texList;
								if (texList == null)
								{
									continue;
								}
								using (List<TextureInfo>.Enumerator enumerator3 = texList.GetEnumerator())
								{
									while (enumerator3.MoveNext())
									{
										TextureInfo textureInfo = enumerator3.Current;
										Texture texture = material.GetTexture(textureInfo.propName);
										if (texture == null || texture.name != textureInfo.texFile)
										{
											string text2 = textureInfo.texFile;
											if (text2.LastIndexOf('.') == -1)
											{
												text2 += FileConst.EXT_TEXTURE;
											}
											if (this._fileUtil.Exists(text2))
											{
												maid.body0.ChangeTex(text, num, textureInfo.propName, text2, null, MaidParts.PARTS_COLOR.NONE);
												Texture texture2 = material.GetTexture(textureInfo.propName);
												if (texture2 != null)
												{
													texture2.name = textureInfo.texFile;
												}
											}
											else
											{
												LogUtil.Debug(new object[]
												{
													"texture file not found. file=",
													text2
												});
											}
										}
										if (textureInfo.offsetX != null || textureInfo.offsetY != null)
										{
											Vector2 textureOffset = material.GetTextureOffset(textureInfo.propName);
											if (textureInfo.offsetX != null)
											{
												textureOffset.x = textureInfo.offsetX.Value;
											}
											if (textureInfo.offsetY != null)
											{
												textureOffset.y = textureInfo.offsetY.Value;
											}
											material.SetTextureOffset(textureInfo.propName, textureOffset);
										}
										if (textureInfo.scaleX != null || textureInfo.scaleY != null)
										{
											Vector2 textureScale = material.GetTextureScale(textureInfo.propName);
											if (textureInfo.scaleX != null)
											{
												textureScale.x = textureInfo.scaleX.Value;
											}
											if (textureInfo.scaleY != null)
											{
												textureScale.y = textureInfo.scaleY.Value;
											}
											material.SetTextureScale(textureInfo.propName, textureScale);
										}
										if (textureInfo.filter != null)
										{
											TextureModifier.FilterParam filter = textureInfo.filter.ToFilter();
											this._texModifier.ApplyFilter(maid, text, material, textureInfo.propName, filter);
										}
									}
									continue;
								}
							}
							LogUtil.LogF("ACCPresetに指定されたマテリアル番号に対応するマテリアルが見つかりません。スキップします。 slot={0}, matNo={1}, name={2}", new object[]
							{
								ccslot.id,
								num,
								ccmaterial.name
							});
							break;
						}
					}
				}
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00007E40 File Offset: 0x00006040
		public void ApplyPresetPartsColor(Maid maid, PresetData preset)
		{
			foreach (KeyValuePair<string, CCPartsColor> keyValuePair in preset.partsColors)
			{
				try
				{
					MaidParts.PARTS_COLOR f_eColorType;
					if (EnumUtil.TryParse<MaidParts.PARTS_COLOR>(keyValuePair.Key, true, out f_eColorType))
					{
						maid.Parts.SetPartsColor(f_eColorType, keyValuePair.Value.ToStruct());
					}
				}
				catch (ArgumentException ex)
				{
					LogUtil.Debug(new object[]
					{
						ex
					});
				}
			}
		}

		// Token: 0x0400007B RID: 123
		private readonly Settings _settings = Settings.Instance;

		// Token: 0x0400007C RID: 124
		private readonly MaidHolder _holder = MaidHolder.Instance;

		// Token: 0x0400007D RID: 125
		private readonly TextureModifier _texModifier = TextureModifier.Instance;

		// Token: 0x0400007E RID: 126
		private readonly FileUtilEx _fileUtil = FileUtilEx.Instance;

		// Token: 0x0400007F RID: 127
		private static readonly Action<Maid, MPN, string, int> SetProp = delegate(Maid maid, MPN mpn, string str, int id)
		{
			maid.SetProp(mpn, str, id, false, false);
		};
	}
}
