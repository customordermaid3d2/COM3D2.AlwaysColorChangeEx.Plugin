using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CM3D2.AlwaysColorChangeEx.Plugin.Data;
using CM3D2.AlwaysColorChangeEx.Plugin.TexAnim;
using CM3D2.AlwaysColorChangeEx.Plugin.UI;
using CM3D2.AlwaysColorChangeEx.Plugin.UI.Helper;
using CM3D2.AlwaysColorChangeEx.Plugin.Util;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityInjector;
using UnityInjector.Attributes;

namespace CM3D2.AlwaysColorChangeEx.Plugin
{
	// Token: 0x02000003 RID: 3
	[PluginName("CM3D2_ACCex")]
	[PluginFilter("COM3D2x64")]
	[PluginVersion("0.3.2.0")]
	internal class AlwaysColorChangeEx : PluginBase
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002078 File Offset: 0x00000278
		static AlwaysColorChangeEx()
		{
			try
			{
				PluginNameAttribute pluginNameAttribute = Attribute.GetCustomAttribute(typeof(AlwaysColorChangeEx), typeof(PluginNameAttribute)) as PluginNameAttribute;
				if (pluginNameAttribute != null)
				{
					AlwaysColorChangeEx.PluginName = pluginNameAttribute.Name;
				}
			}
			catch (Exception ex)
			{
				LogUtil.Error(new object[]
				{
					ex
				});
			}
			try
			{
				PluginVersionAttribute pluginVersionAttribute = Attribute.GetCustomAttribute(typeof(AlwaysColorChangeEx), typeof(PluginVersionAttribute)) as PluginVersionAttribute;
				if (pluginVersionAttribute != null)
				{
					AlwaysColorChangeEx.Version = pluginVersionAttribute.Version;
				}
			}
			catch (Exception ex2)
			{
				LogUtil.Error(new object[]
				{
					ex2
				});
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002134 File Offset: 0x00000334
		public AlwaysColorChangeEx()
		{
			this.mouseDowned = false;
			this.plugin = this;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002284 File Offset: 0x00000484
		public void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(this);
			string path = Path.Combine(base.DataPath, "..\\..\\opengl32.dll");
			string text = Path.Combine(base.DataPath, "..\\..\\Sybaris");
			if (File.Exists(path) && Directory.Exists(text))
			{
				text = Path.GetFullPath(text);
				this.settings.presetDirPath = Path.Combine(text, "Plugins\\UnityInjector\\Config\\ACCPresets");
			}
			else
			{
				this.settings.presetDirPath = Path.Combine(base.DataPath, "ACCPresets");
			}
			base.ReloadConfig();
			this.settings.Load((string key) => base.Preferences["Config"][key].Value);
			LogUtil.Log(new object[]
			{
				"PresetDir:",
				this.settings.presetDirPath
			});
			this.checker.Init();
			this.LoadPresetList();
			this.uiParams.Update();
			ShaderPropType.Initialize();
			this.sliderHelper = new SliderHelper(this.uiParams);
			this.cbHelper = new CheckboxHelper(this.uiParams);
			this.colorPresetMgr = ColorPresetManager.Instance;
			string path2 = Path.Combine(this.settings.presetDirPath, "_ColorPreset.csv");
			this.colorPresetMgr.Count = 40;
			this.colorPresetMgr.SetPath(path2);
			SceneManager.sceneLoaded += this.SceneLoaded;
			this.saveView = new ACCSaveMenuView(this.uiParams);
			this.boneSlotView = new ACCBoneSlotView(this.uiParams, this.sliderHelper);
			this.views.Add(this.boneSlotView);
			this.partsColorView = new ACCPartsColorView(this.uiParams, this.sliderHelper);
			this.views.Add(this.partsColorView);
			this.uiParams.Add(new Action<UIParams>(this.UpdateUIParams));
			if (this.settings.enableTexAnim)
			{
				this.changeDetector.Add(new Action<Maid, MaidProp>(this.animDetector.ChangeMenu));
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002474 File Offset: 0x00000674
		private void UpdateUIParams(UIParams uParams)
		{
			this.colorPresetMgr.BtnStyle.fontSize = uParams.fontSizeS;
			this.colorPresetMgr.BtnWidth = GUILayout.Width(this.colorPresetMgr.BtnStyle.CalcSize(new GUIContent("Update")).x);
			foreach (BaseView baseView in this.views)
			{
				baseView.UpdateUI(uParams);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000250C File Offset: 0x0000070C
		public void Start()
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002510 File Offset: 0x00000710
		public void OnDestroy()
		{
			this.uiHelper.SetCameraControl(true);
			this.Dispose();
			this.presetNames.Clear();
			this.uiParams.Remove(new Action<UIParams>(this.UpdateUIParams));
			SceneManager.sceneLoaded -= this.SceneLoaded;
			LogUtil.Debug(new object[]
			{
				"Destroyed"
			});
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002578 File Offset: 0x00000778
		public void SceneLoaded(Scene scene, LoadSceneMode sceneMode)
		{
			LogUtil.Debug(new object[]
			{
				scene.buildIndex,
				": ",
				scene.name
			});
			this.OnSceneLoaded(scene.buildIndex);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000025C0 File Offset: 0x000007C0
		public void OnSceneLoaded(int level)
		{
			this.fPassedTime = 0f;
			this.bUseStockMaid = false;
			foreach (BaseView baseView in this.views)
			{
				baseView.Clear();
			}
			this.changeDetector.Clear();
			if (this.checker.IsTarget(level))
			{
				this.bUseStockMaid = this.checker.IsStockTarget(level);
				this.menuType = AlwaysColorChangeEx.MenuType.None;
				this.mouseDowned = false;
				this.uiHelper.cursorContains = false;
				this.isActive = true;
				return;
			}
			if (!this.isActive)
			{
				return;
			}
			this.uiHelper.SetCameraControl(true);
			this.initialized = false;
			this.isActive = false;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000026CC File Offset: 0x000008CC
		public void Update()
		{
			this.fPassedTime += Time.deltaTime;
			if (!this.isActive)
			{
				return;
			}
			if (!this.initialized)
			{
				if (this.fPassedTime - this.fLastInitTime <= 1f)
				{
					return;
				}
				this.fLastInitTime = this.fPassedTime;
				this.initialized = this.Initialize();
				LogUtil.Debug(new object[]
				{
					"Initialized ",
					this.initialized
				});
				if (!this.initialized)
				{
					return;
				}
			}
			if (this.settings.enableTexAnim)
			{
				this.changeDetector.Detect(this.bUseStockMaid);
			}
			if (this.InputModifierKey() && Input.GetKeyDown(this.settings.toggleKey))
			{
				this.SetMenu((this.menuType == AlwaysColorChangeEx.MenuType.None) ? AlwaysColorChangeEx.MenuType.Main : AlwaysColorChangeEx.MenuType.None);
				this.mouseDowned = false;
			}
			this.UpdateCameraControl();
			if (!this.holder.CurrentActivated())
			{
				return;
			}
			this.boneSlotView.Update();
			if (this.toApplyPresetMaid != null && !this.toApplyPresetMaid.IsBusy)
			{
				Maid targetMaid = this.toApplyPresetMaid;
				this.toApplyPresetMaid = null;
				this.plugin.StartCoroutine(this.DelayFrameRecall(10, () => !this.ApplyPresetProp(targetMaid, this.currentPreset)));
			}
			if (ACCTexturesView.fileBrowser != null)
			{
				if (Input.GetKeyDown(this.settings.prevKey))
				{
					ACCTexturesView.fileBrowser.Prev();
				}
				if (Input.GetKeyDown(this.settings.nextKey))
				{
					ACCTexturesView.fileBrowser.Next();
				}
				ACCTexturesView.fileBrowser.Update();
			}
			if (this.menuType == AlwaysColorChangeEx.MenuType.Texture)
			{
				if (this.texSliderUpped || Input.GetMouseButtonUp(0))
				{
					if (ACCTexturesView.IsChangeTarget())
					{
						ACCTexturesView.UpdateTex(this.holder.CurrentMaid, this.targetMaterials);
					}
					this.texSliderUpped = false;
					return;
				}
			}
			else
			{
				ACCTexturesView.ClearTarget();
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000028AC File Offset: 0x00000AAC
		private void UpdateSelectMaid()
		{
			this.InitMaidList();
			if (this._maidList.Count == 1)
			{
				Maid maid = this._maidList[0].maid;
				string text = this._maidList[0].content.text;
				this.holder.UpdateMaid(maid, text, new Action(this.ClearMaidData));
				this.SetMenu(AlwaysColorChangeEx.MenuType.Main);
				return;
			}
			this.SetMenu(AlwaysColorChangeEx.MenuType.MaidSelect);
			this.uiParams.winRect = GUI.Window(20201, this.uiParams.winRect, new GUI.WindowFunction(this.DoSelectMaid), AlwaysColorChangeEx.Version, this.uiParams.winStyle);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002960 File Offset: 0x00000B60
		public void OnGUI()
		{
			if (!this.isActive || this.menuType == AlwaysColorChangeEx.MenuType.None)
			{
				return;
			}
			if (this.settings.SSWithoutUI && !this.uiHelper.IsEnabledUICamera())
			{
				return;
			}
			if (Event.current.type != EventType.Repaint)
			{
				if (!this.holder.CurrentActivated())
				{
					this.UpdateSelectMaid();
				}
				else if (ACCTexturesView.fileBrowser != null)
				{
					this.uiParams.fileBrowserRect = GUI.Window(20202, this.uiParams.fileBrowserRect, new GUI.WindowFunction(this.DoFileBrowser), AlwaysColorChangeEx.Version, this.uiParams.winStyle);
				}
				else if (this.saveView.showDialog)
				{
					this.uiParams.modalRect = GUI.ModalWindow(20201, this.uiParams.modalRect, new GUI.WindowFunction(this.DoSaveModDialog), "menuエクスポート", this.uiParams.dialogStyle);
				}
				else
				{
					switch (this.menuType)
					{
					case AlwaysColorChangeEx.MenuType.Main:
						this.uiParams.winRect = GUI.Window(20201, this.uiParams.winRect, new GUI.WindowFunction(this.DoMainMenu), AlwaysColorChangeEx.Version, this.uiParams.winStyle);
						break;
					case AlwaysColorChangeEx.MenuType.Color:
						this.uiParams.winRect = GUI.Window(20201, this.uiParams.winRect, new GUI.WindowFunction(this.DoColorMenu), AlwaysColorChangeEx.Version, this.uiParams.winStyle);
						break;
					case AlwaysColorChangeEx.MenuType.NodeSelect:
						this.uiParams.winRect = GUI.Window(20201, this.uiParams.winRect, new GUI.WindowFunction(this.DoNodeSelectMenu), AlwaysColorChangeEx.Version, this.uiParams.winStyle);
						break;
					case AlwaysColorChangeEx.MenuType.MaskSelect:
						this.uiParams.winRect = GUI.Window(20201, this.uiParams.winRect, new GUI.WindowFunction(this.DoMaskSelectMenu), AlwaysColorChangeEx.Version, this.uiParams.winStyle);
						break;
					case AlwaysColorChangeEx.MenuType.Save:
						this.uiParams.winRect = GUI.Window(20201, this.uiParams.winRect, new GUI.WindowFunction(this.DoSaveMenu), AlwaysColorChangeEx.Version, this.uiParams.winStyle);
						break;
					case AlwaysColorChangeEx.MenuType.PresetSelect:
						this.uiParams.winRect = GUI.Window(20201, this.uiParams.winRect, new GUI.WindowFunction(this.DoSelectPreset), AlwaysColorChangeEx.Version, this.uiParams.winStyle);
						break;
					case AlwaysColorChangeEx.MenuType.Texture:
						this.uiParams.winRect = GUI.Window(20201, this.uiParams.winRect, new GUI.WindowFunction(this.DoSelectTexture), AlwaysColorChangeEx.Version, this.uiParams.winStyle);
						break;
					case AlwaysColorChangeEx.MenuType.MaidSelect:
						this.uiParams.winRect = GUI.Window(20201, this.uiParams.winRect, new GUI.WindowFunction(this.DoSelectMaid), AlwaysColorChangeEx.Version, this.uiParams.winStyle);
						break;
					case AlwaysColorChangeEx.MenuType.BoneSlotSelect:
						this.uiParams.winRect = GUI.Window(20201, this.uiParams.winRect, new GUI.WindowFunction(this.DoSelectBoneSlot), AlwaysColorChangeEx.Version, this.uiParams.winStyle);
						break;
					case AlwaysColorChangeEx.MenuType.PartsColor:
						this.uiParams.winRect = GUI.Window(20201, this.uiParams.winRect, new GUI.WindowFunction(this.DoEditPartsColor), AlwaysColorChangeEx.Version, this.uiParams.winStyle);
						break;
					}
					this.OnTips();
				}
				if (Input.GetMouseButtonUp(0))
				{
					if (this.mouseDowned)
					{
						Input.ResetInputAxes();
						this.texSliderUpped = (this.menuType == AlwaysColorChangeEx.MenuType.Texture);
					}
					this.mouseDowned = false;
				}
				this.mouseDowned |= (this.uiHelper.cursorContains && Input.GetMouseButtonDown(0));
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002D7E File Offset: 0x00000F7E
		private void OnTips()
		{
			if (this.displayTips && this.tips != null)
			{
				GUI.Window(20203, this.tipRect, new GUI.WindowFunction(this.DoTips), this.tips, this.uiParams.tipsStyle);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002DD0 File Offset: 0x00000FD0
		public void SetTips(string message)
		{
			int num = 1;
			foreach (char c in message)
			{
				if (c == '\n')
				{
					num++;
				}
			}
			if (num == 1)
			{
				num += message.Length / 15;
			}
			float num2 = (float)(num * this.uiParams.fontSize * 19 / 14 + 30);
			if (num2 > 400f)
			{
				num2 = 400f;
			}
			this.tipRect = new Rect(this.uiParams.winRect.x + 24f, this.uiParams.winRect.yMin + 150f, this.uiParams.winRect.width - 48f, num2);
			this.displayTips = true;
			this.tips = message;
			this.plugin.StartCoroutine(this.DelaySecond(2, delegate
			{
				this.displayTips = false;
				this.tips = null;
			}));
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002EB9 File Offset: 0x000010B9
		public void DoTips(int winID)
		{
			GUI.BringWindowToFront(winID);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002EC4 File Offset: 0x000010C4
		private bool InputModifierKey()
		{
			EventModifiers modifiers = Event.current.modifiers;
			if (this.settings.toggleModifiers == EventModifiers.None)
			{
				return (modifiers & (EventModifiers.Shift | EventModifiers.Control | EventModifiers.Alt)) == EventModifiers.None;
			}
			return (modifiers & this.settings.toggleModifiers) != EventModifiers.None;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002F04 File Offset: 0x00001104
		private void UpdateCameraControl()
		{
			bool flag = false;
			if (ACCTexturesView.fileBrowser != null || this.menuType != AlwaysColorChangeEx.MenuType.None)
			{
				Vector2 point = new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y);
				flag = ((ACCTexturesView.fileBrowser != null) ? this.uiParams.fileBrowserRect : this.uiParams.winRect).Contains(point);
				if (!flag && this.saveView.showDialog)
				{
					flag = this.uiParams.modalRect.Contains(point);
				}
			}
			this.uiHelper.UpdateCameraControl(flag);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002F9C File Offset: 0x0000119C
		private void Dispose()
		{
			this.ClearMaidData();
			this.uiHelper.SetCameraControl(true);
			this.mouseDowned = false;
			ACCTexturesView.Clear();
			ResourceHolder.Instance.Clear();
			foreach (BaseView baseView in this.views)
			{
				baseView.Dispose();
			}
			this.initialized = false;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00003020 File Offset: 0x00001220
		private bool Initialize()
		{
			this.InitMaidInfo();
			this.uiParams.Update();
			ACCTexturesView.Init(this.uiParams);
			ACCMaterialsView.Init(this.uiParams);
			return true;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000304A File Offset: 0x0000124A
		private void InitMaidInfo()
		{
			this.holder.UpdateMaid(new Action(this.ClearMaidData));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00003104 File Offset: 0x00001304
		private IEnumerator DelayFrameRecall(int delayFrame, Func<bool> func)
		{
			do
			{
				for (int i = 0; i < delayFrame; i++)
				{
					yield return null;
				}
			}
			while (func());
			yield break;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000031CC File Offset: 0x000013CC
		private IEnumerator DelayFrame(int delayFrame, Action act)
		{
			for (int i = 0; i < delayFrame; i++)
			{
				yield return null;
			}
			act();
			yield break;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000327C File Offset: 0x0000147C
		private IEnumerator DelaySecond(int second, Action act)
		{
			yield return new WaitForSeconds((float)second);
			act();
			yield break;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000032A6 File Offset: 0x000014A6
		private void ClearMaidData()
		{
			ACCMaterialsView.Clear();
			this.dDelNodes.Clear();
			this.dDelNodeDisps.Clear();
			this.dMaskSlots.Clear();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000032CE File Offset: 0x000014CE
		private void SetMenu(AlwaysColorChangeEx.MenuType type)
		{
			if (this.menuType == type)
			{
				return;
			}
			this.menuType = type;
			this.uiParams.Update();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000032EC File Offset: 0x000014EC
		private GUIContent GetOrAddMaidInfo(Maid m, int idx = -1)
		{
			int instanceID = m.gameObject.GetInstanceID();
			GUIContent guicontent;
			if (this._contentDic.TryGetValue(instanceID, out guicontent))
			{
				return guicontent;
			}
			LogUtil.Debug(new object[]
			{
				"maid:",
				m.name
			});
			string text = (!m.boMAN) ? MaidHelper.GetName(m) : ("男" + (idx + 1));
			Texture2D thumIcon = m.GetThumIcon();
			guicontent = new GUIContent(text, thumIcon);
			this._contentDic[instanceID] = guicontent;
			return guicontent;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003379 File Offset: 0x00001579
		private bool IsEnabled(Maid m)
		{
			return m.isActiveAndEnabled && m.Visible;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000338C File Offset: 0x0000158C
		private void InitMaidList()
		{
			this._maidList.Clear();
			this._manList.Clear();
			CharacterMgr characterMgr = GameMain.Instance.CharacterMgr;
			if (this.bUseStockMaid)
			{
				this.AddMaidList(this._maidList, new Func<int, Maid>(characterMgr.GetStockMaid), characterMgr.GetStockMaidCount());
			}
			else
			{
				this.AddMaidList(this._maidList, new Func<int, Maid>(characterMgr.GetMaid), characterMgr.GetMaidCount());
			}
			if (this.bUseStockMaid)
			{
				this.AddMaidList(this._manList, new Func<int, Maid>(characterMgr.GetStockMan), characterMgr.GetStockManCount());
				return;
			}
			this.AddMaidList(this._manList, new Func<int, Maid>(characterMgr.GetMan), characterMgr.GetManCount());
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003448 File Offset: 0x00001648
		private void AddMaidList(ICollection<AlwaysColorChangeEx.SelectMaidData> list, Func<int, Maid> GetMaid, int count)
		{
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				Maid maid = GetMaid(i);
				if (!(maid == null) && this.IsEnabled(maid))
				{
					string text;
					if (!maid.boMAN)
					{
						text = MaidHelper.GetName(maid);
					}
					else
					{
						text = "男" + (num + 1);
						num++;
					}
					Texture2D thumIcon = maid.GetThumIcon();
					GUIContent content = new GUIContent(text, thumIcon);
					list.Add(new AlwaysColorChangeEx.SelectMaidData(maid, content));
				}
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000034C8 File Offset: 0x000016C8
		private void DoSelectMaid(int winID)
		{
			if (this.selectedMaid == null)
			{
				this.selectedMaid = this.holder.CurrentMaid;
			}
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			GUILayout.Label("メイド選択", this.uiParams.lStyleB, new GUILayoutOption[0]);
			this.scrollViewPosition = GUILayout.BeginScrollView(this.scrollViewPosition, new GUILayoutOption[]
			{
				this.uiParams.optSubConWidth,
				this.uiParams.optSubConHeight
			});
			bool enabled = false;
			try
			{
				foreach (AlwaysColorChangeEx.SelectMaidData selectMaidData in this._maidList)
				{
					GUI.enabled = this.IsEnabled(selectMaidData.maid);
					bool flag = this.selectedMaid == selectMaidData.maid;
					if (GUI.enabled && flag)
					{
						enabled = true;
					}
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					GUILayout.Space((float)this.uiParams.marginL);
					bool flag2 = GUILayout.Toggle(flag, selectMaidData.content, this.uiParams.tStyleL, new GUILayoutOption[0]);
					GUILayout.Space((float)this.uiParams.marginL);
					GUILayout.EndHorizontal();
					if (flag2 != flag)
					{
						this.selectedMaid = selectMaidData.maid;
						this.selectedName = selectMaidData.content.text;
					}
				}
				GUI.enabled = true;
				if (!this._maidList.Any<AlwaysColorChangeEx.SelectMaidData>())
				{
					GUILayout.Label("\u3000なし", this.uiParams.lStyleB, new GUILayoutOption[0]);
				}
				GUILayout.Space((float)this.uiParams.marginL);
				if (this._manList.Any<AlwaysColorChangeEx.SelectMaidData>())
				{
					GUILayout.Label("男選択", this.uiParams.lStyleB, new GUILayoutOption[0]);
					foreach (AlwaysColorChangeEx.SelectMaidData selectMaidData2 in this._manList)
					{
						Maid maid = selectMaidData2.maid;
						GUI.enabled = this.IsEnabled(maid);
						bool flag3 = this.selectedMaid == maid;
						if (GUI.enabled && flag3)
						{
							enabled = true;
						}
						GUILayout.BeginHorizontal(new GUILayoutOption[0]);
						GUILayout.Space((float)this.uiParams.marginL);
						bool flag4 = GUILayout.Toggle(flag3, selectMaidData2.content, this.uiParams.tStyleL, new GUILayoutOption[0]);
						GUILayout.Space((float)this.uiParams.marginL);
						GUILayout.EndHorizontal();
						if (flag4 != flag3)
						{
							this.selectedMaid = maid;
							this.selectedName = selectMaidData2.content.text;
						}
					}
					GUI.enabled = true;
				}
			}
			finally
			{
				GUILayout.EndScrollView();
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUI.enabled = enabled;
				if (GUILayout.Button("選択", this.uiParams.bStyle, new GUILayoutOption[]
				{
					this.uiParams.optSubConHalfWidth
				}))
				{
					this.SetMenu(AlwaysColorChangeEx.MenuType.Main);
					this.holder.UpdateMaid(this.selectedMaid, this.selectedName, new Action(this.ClearMaidData));
					this.selectedMaid = null;
					this.selectedName = null;
					this._contentDic.Clear();
				}
				GUI.enabled = true;
				if (GUILayout.Button("一覧更新", this.uiParams.bStyle, new GUILayoutOption[]
				{
					this.uiParams.optSubConHalfWidth
				}))
				{
					this.InitMaidList();
				}
				GUILayout.EndHorizontal();
				GUILayout.EndVertical();
			}
			GUI.DragWindow(this.uiParams.titleBarRect);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000038AC File Offset: 0x00001AAC
		private void DoMainMenu(int winID)
		{
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			try
			{
				Maid currentMaid = this.holder.CurrentMaid;
				GUILayout.Label("ACCex : " + this.holder.MaidName, this.uiParams.lStyle, new GUILayoutOption[0]);
				if (GUILayout.Button("メイド/男 選択", this.uiParams.bStyle, new GUILayoutOption[0]))
				{
					this.InitMaidList();
					this.SetMenu(AlwaysColorChangeEx.MenuType.MaidSelect);
				}
				GUI.enabled = !currentMaid.boMAN;
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				try
				{
					if (GUILayout.Button("マスク選択", this.uiParams.bStyle, new GUILayoutOption[]
					{
						this.uiParams.optSubConHalfWidth
					}))
					{
						this.SetMenu(AlwaysColorChangeEx.MenuType.MaskSelect);
						this.InitMaskSlots();
					}
					if (GUILayout.Button("表示ノード選択", this.uiParams.bStyle, new GUILayoutOption[]
					{
						this.uiParams.optSubConHalfWidth
					}))
					{
						if (this.dDelNodes.Any<KeyValuePair<string, bool>>())
						{
							this.dDelNodeDisps = this.GetDelNodes();
						}
						this.SetMenu(AlwaysColorChangeEx.MenuType.NodeSelect);
					}
				}
				finally
				{
					GUILayout.EndHorizontal();
				}
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				try
				{
					GUI.enabled &= (this.holder.isOfficial && this.toApplyPresetMaid == null);
					if (GUILayout.Button("プリセット保存", this.uiParams.bStyle, new GUILayoutOption[]
					{
						this.uiParams.optSubConHalfWidth
					}))
					{
						this.SetMenu(AlwaysColorChangeEx.MenuType.Save);
					}
					if (this.presetNames.Any<string>() && GUILayout.Button("プリセット適用", this.uiParams.bStyle, new GUILayoutOption[]
					{
						this.uiParams.optSubConHalfWidth
					}))
					{
						this.SetMenu(AlwaysColorChangeEx.MenuType.PresetSelect);
					}
				}
				finally
				{
					GUILayout.EndHorizontal();
				}
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				try
				{
					if (GUILayout.Button("ボーン表示", this.uiParams.bStyle, new GUILayoutOption[]
					{
						this.uiParams.optSubConHalfWidth
					}))
					{
						this.SetMenu(AlwaysColorChangeEx.MenuType.BoneSlotSelect);
					}
					if (GUILayout.Button("パーツカラー変更", this.uiParams.bStyle, new GUILayoutOption[]
					{
						this.uiParams.optSubConHalfWidth
					}))
					{
						this.SetMenu(AlwaysColorChangeEx.MenuType.PartsColor);
					}
				}
				finally
				{
					GUILayout.EndHorizontal();
				}
				GUI.enabled = true;
				GUILayout.Space((float)this.uiParams.margin);
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.Label("マテ情報変更 スロット選択", this.uiParams.lStyleC, new GUILayoutOption[0]);
				this.nameSwitched = GUILayout.Toggle(this.nameSwitched, "表示切替", this.uiParams.tStyleS, new GUILayoutOption[]
				{
					this.uiParams.optToggleSWidth
				});
				GUILayout.EndHorizontal();
				this.scrollViewPosition = GUILayout.BeginScrollView(this.scrollViewPosition, new GUILayoutOption[]
				{
					GUILayout.Width(this.uiParams.mainRect.width),
					GUILayout.Height(this.uiParams.mainRect.height)
				});
				try
				{
					TBody body = this.holder.CurrentMaid.body0;
					if (this.holder.isOfficial)
					{
						using (Dictionary<TBody.SlotID, SlotInfo>.ValueCollection.Enumerator enumerator = ACConstants.SlotNames.Values.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								SlotInfo slotInfo = enumerator.Current;
								if (slotInfo.enable && body.GetSlotLoaded(slotInfo.Id))
								{
									GUILayout.BeginHorizontal(new GUILayoutOption[0]);
									GUILayout.Space((float)this.uiParams.marginL);
									if (GUILayout.Button((!this.nameSwitched) ? slotInfo.DisplayName : slotInfo.Name, this.uiParams.bStyleL, new GUILayoutOption[]
									{
										GUILayout.ExpandWidth(true)
									}))
									{
										this.holder.CurrentSlot = slotInfo;
										this.SetMenu(AlwaysColorChangeEx.MenuType.Color);
									}
									if (this.settings.displaySlotName)
									{
										GUILayout.Label(slotInfo.Name, this.uiParams.lStyleS, new GUILayoutOption[]
										{
											this.uiParams.optCategoryWidth
										});
									}
									GUILayout.Space((float)this.uiParams.marginL);
									GUILayout.EndHorizontal();
								}
							}
							goto IL_542;
						}
					}
					int num = 0;
					int count = body.goSlot.Count;
					for (int i = 0; i < count; i++)
					{
						TBodySkin tbodySkin = body.goSlot[i];
						if (this.settings.enableMoza || i != count - 1 || !(tbodySkin.Category == "moza"))
						{
							if (tbodySkin.obj != null)
							{
								GUILayout.BeginHorizontal(new GUILayoutOption[0]);
								GUILayout.Space((float)this.uiParams.marginL);
								if (GUILayout.Button(tbodySkin.Category, this.uiParams.bStyleL, new GUILayoutOption[]
								{
									GUILayout.ExpandWidth(true)
								}))
								{
									this.holder.CurrentSlot = ACConstants.SlotNames[(TBody.SlotID)num];
									this.SetMenu(AlwaysColorChangeEx.MenuType.Color);
								}
								GUILayout.Space((float)this.uiParams.marginL);
								GUILayout.EndHorizontal();
							}
							num++;
						}
					}
					IL_542:;
				}
				finally
				{
					GUI.enabled = true;
					GUILayout.EndScrollView();
				}
			}
			finally
			{
				GUILayout.EndVertical();
			}
			GUI.DragWindow(this.uiParams.titleBarRect);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003EB8 File Offset: 0x000020B8
		private List<ACCMaterialsView> InitMaterialView(Renderer r, string menufile, int slotIdx)
		{
			Material[] materials = r.materials;
			int num = 0;
			List<ACCMaterialsView> list = new List<ACCMaterialsView>(materials.Length);
			foreach (Material m in materials)
			{
				ACCMaterialsView accmaterialsView = new ACCMaterialsView(r, m, slotIdx, num++, this.sliderHelper, this.cbHelper)
				{
					tipsCall = new Action<string>(this.SetTips)
				};
				list.Add(accmaterialsView);
				accmaterialsView.expand = (materials.Length <= 2);
			}
			return list;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003F40 File Offset: 0x00002140
		private void DoColorMenu(int winID)
		{
			TBodySkin currentSlot = this.holder.GetCurrentSlot();
			if (this.title == null)
			{
				this.title = new GUIContent("マテリアル情報変更: " + (this.holder.isOfficial ? this.holder.CurrentSlot.DisplayName : currentSlot.Category));
			}
			GUILayout.Label(this.title, this.uiParams.lStyleB, new GUILayoutOption[0]);
			if (this.holder.CurrentMaid.IsBusy)
			{
				GUILayout.Space(100f);
				GUILayout.Label("変更中...", this.uiParams.lStyleB, new GUILayoutOption[0]);
				return;
			}
			int currentMenuFileID = this.holder.GetCurrentMenuFileID();
			if (currentMenuFileID != 0 && this.slotDropped)
			{
				if (!this.changeCounter.Next())
				{
					return;
				}
				this.SetMenu(AlwaysColorChangeEx.MenuType.Main);
				LogUtil.Debug(new object[]
				{
					"select slot item dropped. return to main menu.",
					currentMenuFileID
				});
				this.slotDropped = false;
				return;
			}
			else
			{
				if (this.targetMenuId != currentMenuFileID)
				{
					this.title = null;
					string currentMenuFile = this.holder.GetCurrentMenuFile();
					this.isSavable = (currentMenuFile != null && !currentMenuFile.ToLower().EndsWith(FileConst.EXT_MOD, StringComparison.Ordinal));
					this.targetMenuId = currentMenuFileID;
					Renderer renderer = this.holder.GetRenderer(currentSlot);
					if (renderer != null)
					{
						this.targetMaterials = renderer.materials;
						this.materialViews = this.InitMaterialView(renderer, currentMenuFile, (int)currentSlot.SlotId);
					}
					else
					{
						this.targetMaterials = this.EMPTY_ARRAY;
					}
					this.slotDropped = (currentSlot.obj == null);
					this.changeCounter.Reset();
					if (this.isSavable)
					{
						this.isSavable &= (this.holder.CurrentSlot.Id != TBody.SlotID.body);
					}
				}
				if (GUILayout.Button("テクスチャ変更", this.uiParams.bStyle, new GUILayoutOption[0]))
				{
					this.texViews = this.InitTexView(this.targetMaterials);
					this.SetMenu(AlwaysColorChangeEx.MenuType.Texture);
					return;
				}
				if (this.targetMaterials.Length <= 0)
				{
					return;
				}
				this.scrollViewPosition = GUILayout.BeginScrollView(this.scrollViewPosition, new GUILayoutOption[]
				{
					GUILayout.Width(this.uiParams.colorRect.width),
					GUILayout.Height(this.uiParams.colorRect.height)
				});
				try
				{
					bool flag = Event.current.type == EventType.Layout && this.refreshCounter.Next();
					if (flag)
					{
						ClipBoardHandler.Instance.Reload();
					}
					foreach (ACCMaterialsView accmaterialsView in this.materialViews)
					{
						accmaterialsView.Show(flag);
					}
				}
				catch (Exception ex)
				{
					LogUtil.Error(new object[]
					{
						"マテリアル情報変更画面でエラーが発生しました。メイン画面へ移動します",
						ex
					});
					this.SetMenu(AlwaysColorChangeEx.MenuType.Main);
					this.targetMenuId = 0;
				}
				finally
				{
					GUILayout.EndScrollView();
					GUI.enabled = this.isSavable;
					if (GUILayout.Button("menuエクスポート", this.uiParams.bStyle, new GUILayoutOption[0]))
					{
						this.ExportMenu();
					}
					GUI.enabled = true;
					if (GUILayout.Button("閉じる", this.uiParams.bStyle, new GUILayoutOption[0]))
					{
						this.SetMenu(AlwaysColorChangeEx.MenuType.Main);
						this.targetMenuId = 0;
					}
					GUI.DragWindow(this.uiParams.titleBarRect);
				}
				return;
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000042F4 File Offset: 0x000024F4
		private void ExportMenu()
		{
			TBodySkin currentSlot = this.holder.GetCurrentSlot();
			if (currentSlot.obj == null)
			{
				string f_strMessage = "指定スロットが見つかりません。slot=" + this.holder.CurrentSlot.Name;
				NUty.WinMessageBox(NUty.GetWindowHandle(), f_strMessage, "エラー", 0);
				return;
			}
			MaidProp prop = this.holder.CurrentMaid.GetProp(this.holder.CurrentSlot.mpn);
			if (prop == null)
			{
				return;
			}
			Dictionary<TBody.SlotID, Item> dictionary = this.saveView.Load(prop.strFileName);
			if (dictionary == null)
			{
				string f_strMessage2 = "変更可能なmenuファイルがありません " + prop.strFileName;
				NUty.WinMessageBox(NUty.GetWindowHandle(), f_strMessage2, "エラー", 0);
				return;
			}
			foreach (TBody.SlotID slotID in dictionary.Keys)
			{
				List<ACCMaterial> list;
				if (slotID == this.holder.CurrentSlot.Id)
				{
					list = new List<ACCMaterial>(this.materialViews.Count);
					using (List<ACCMaterialsView>.Enumerator enumerator2 = this.materialViews.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							ACCMaterialsView accmaterialsView = enumerator2.Current;
							list.Add(accmaterialsView.edited);
						}
						goto IL_16D;
					}
					goto IL_128;
				}
				goto IL_128;
				IL_16D:
				this.saveView.SetEditedMaterials(slotID, list);
				continue;
				IL_128:
				Material[] materials = this.holder.GetMaterials(slotID);
				list = new List<ACCMaterial>(materials.Length);
				list.AddRange(from mat in materials
				select new ACCMaterial(mat, null, -1));
				goto IL_16D;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000044B8 File Offset: 0x000026B8
		private List<ACCTexturesView> InitTexView(ICollection<Material> materials)
		{
			List<ACCTexturesView> list = new List<ACCTexturesView>(materials.Count);
			int num = 0;
			foreach (Material material in materials)
			{
				try
				{
					ACCTexturesView acctexturesView = new ACCTexturesView(material, num++);
					list.Add(acctexturesView);
					acctexturesView.expand = (materials.Count <= 2);
				}
				catch (Exception ex)
				{
					LogUtil.Error(new object[]
					{
						material.name,
						ex
					});
				}
			}
			return list;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00004564 File Offset: 0x00002764
		private void DoSelectTexture(int winId)
		{
			GUILayout.Label("テクスチャ変更", this.uiParams.lStyleB, new GUILayoutOption[0]);
			this.scrollViewPosition = GUILayout.BeginScrollView(this.scrollViewPosition, new GUILayoutOption[]
			{
				GUILayout.Width(this.uiParams.textureRect.width),
				GUILayout.Height(this.uiParams.textureRect.height)
			});
			try
			{
				int currentMenuFileID = this.holder.GetCurrentMenuFileID();
				if (this.targetMenuId != currentMenuFileID)
				{
					LogUtil.DebugF("menu file changed. {0}=>{1}", new object[]
					{
						this.targetMenuId,
						currentMenuFileID
					});
					this.targetMenuId = currentMenuFileID;
					this.targetMaterials = this.holder.GetMaterials();
					this.texViews = this.InitTexView(this.targetMaterials);
				}
				foreach (ACCTexturesView acctexturesView in this.texViews)
				{
					acctexturesView.Show();
				}
			}
			catch (Exception ex)
			{
				LogUtil.Debug(new object[]
				{
					"failed to create texture change view. ",
					ex
				});
			}
			finally
			{
				GUILayout.EndScrollView();
				if (GUILayout.Button("閉じる", this.uiParams.bStyle, new GUILayoutOption[]
				{
					this.uiParams.optSubConWidth,
					this.uiParams.optBtnHeight
				}))
				{
					this.SetMenu(AlwaysColorChangeEx.MenuType.Color);
				}
				GUI.DragWindow(this.uiParams.titleBarRect);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00004720 File Offset: 0x00002920
		private void DoFileBrowser(int winId)
		{
			ACCTexturesView.fileBrowser.OnGUI();
			GUI.DragWindow(this.uiParams.titleBarRect);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000473C File Offset: 0x0000293C
		private bool InitMaskSlots()
		{
			if (this.holder.CurrentMaid == null)
			{
				return false;
			}
			foreach (SlotInfo slotInfo in ACConstants.SlotNames.Values)
			{
				if (slotInfo.enable && slotInfo.maskable)
				{
					int id = (int)slotInfo.Id;
					if (id < this.holder.CurrentMaid.body0.goSlot.Count)
					{
						TBodySkin slot = this.holder.CurrentMaid.body0.GetSlot(id);
						MaskInfo maskInfo;
						if (!this.dMaskSlots.TryGetValue(slotInfo.Id, out maskInfo))
						{
							maskInfo = new MaskInfo(slotInfo, slot);
							this.dMaskSlots[slotInfo.Id] = maskInfo;
						}
						else
						{
							maskInfo.slot = slot;
						}
						maskInfo.value = slot.boVisible;
					}
				}
			}
			return true;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000483C File Offset: 0x00002A3C
		private void DoMaskSelectMenu(int winID)
		{
			GUILayoutOption guilayoutOption = GUILayout.Width(this.uiParams.subConWidth * 0.32f);
			GUILayoutOption guilayoutOption2 = GUILayout.Width(this.uiParams.subConWidth * 0.24f);
			GUILayoutOption guilayoutOption3 = GUILayout.Width((float)this.uiParams.fontSize * 4f);
			GUILayoutOption guilayoutOption4 = GUILayout.Width((float)this.uiParams.fontSize * 10f);
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			try
			{
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.Label("マスクアイテム選択", this.uiParams.lStyleB, new GUILayoutOption[]
				{
					guilayoutOption4
				});
				this.nameSwitched = GUILayout.Toggle(this.nameSwitched, "表示切替", this.uiParams.tStyleS, new GUILayoutOption[]
				{
					this.uiParams.optToggleSWidth
				});
				GUILayout.EndHorizontal();
				if (this.holder.CurrentMaid == null)
				{
					return;
				}
				if (!this.dMaskSlots.Any<KeyValuePair<TBody.SlotID, MaskInfo>>())
				{
					this.InitMaskSlots();
				}
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				try
				{
					if (GUILayout.Button("同期", this.uiParams.bStyle, new GUILayoutOption[]
					{
						this.uiParams.optBtnHeight,
						guilayoutOption
					}))
					{
						this.InitMaskSlots();
					}
					if (GUILayout.Button("すべてON", this.uiParams.bStyle, new GUILayoutOption[]
					{
						this.uiParams.optBtnHeight,
						guilayoutOption
					}))
					{
						List<TBody.SlotID> list = new List<TBody.SlotID>(this.dMaskSlots.Keys);
						foreach (TBody.SlotID key in list)
						{
							this.dMaskSlots[key].value = false;
						}
					}
					if (GUILayout.Button("すべてOFF", this.uiParams.bStyle, new GUILayoutOption[]
					{
						this.uiParams.optBtnHeight,
						guilayoutOption
					}))
					{
						List<TBody.SlotID> list2 = new List<TBody.SlotID>(this.dMaskSlots.Keys);
						foreach (TBody.SlotID key2 in list2)
						{
							this.dMaskSlots[key2].value = true;
						}
					}
				}
				finally
				{
					GUILayout.EndHorizontal();
				}
				this.scrollViewPosition = GUILayout.BeginScrollView(this.scrollViewPosition, new GUILayoutOption[]
				{
					GUILayout.Width(this.uiParams.nodeSelectRect.width),
					GUILayout.Height(this.uiParams.nodeSelectRect.height)
				});
				GUIStyle lStyle = this.uiParams.lStyle;
				Color textColor = lStyle.normal.textColor;
				try
				{
					foreach (KeyValuePair<TBody.SlotID, MaskInfo> keyValuePair in this.dMaskSlots)
					{
						MaskInfo value = keyValuePair.Value;
						string text;
						if (!this.holder.CurrentMaid.body0.GetMask(value.slotInfo.Id))
						{
							text = "[非表示]";
							lStyle.normal.textColor = Color.magenta;
						}
						else
						{
							value.UpdateState();
							switch (value.state)
							{
							case SlotState.Displayed:
								text = "[表示中]";
								lStyle.normal.textColor = textColor;
								goto IL_3C4;
							case SlotState.Masked:
								text = "[マスク]";
								lStyle.normal.textColor = Color.cyan;
								goto IL_3C4;
							case SlotState.NotLoaded:
								text = "[未読込]";
								lStyle.normal.textColor = Color.red;
								GUI.enabled = false;
								goto IL_3C4;
							}
							text = "unknown";
							lStyle.normal.textColor = Color.red;
							GUI.enabled = false;
						}
						IL_3C4:
						GUILayout.BeginHorizontal(new GUILayoutOption[0]);
						GUILayout.Label(text, lStyle, new GUILayoutOption[]
						{
							guilayoutOption3
						});
						value.value = !GUILayout.Toggle(!value.value, value.Name(!this.nameSwitched), this.uiParams.tStyle, new GUILayoutOption[]
						{
							this.uiParams.optContentWidth,
							GUILayout.ExpandWidth(true)
						});
						GUI.enabled = true;
						GUILayout.EndHorizontal();
					}
				}
				finally
				{
					lStyle.normal.textColor = textColor;
					GUI.EndScrollView();
				}
			}
			finally
			{
				GUILayout.EndVertical();
			}
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			try
			{
				if (GUILayout.Button("一時適用", this.uiParams.bStyle, new GUILayoutOption[]
				{
					guilayoutOption2,
					this.uiParams.optBtnHeight
				}))
				{
					this.holder.SetSlotVisibles(this.holder.CurrentMaid, this.dMaskSlots, true);
				}
				if (GUILayout.Button("適用", this.uiParams.bStyle, new GUILayoutOption[]
				{
					guilayoutOption2,
					this.uiParams.optBtnHeight
				}))
				{
					this.holder.SetSlotVisibles(this.holder.CurrentMaid, this.dMaskSlots, false);
					this.holder.FixFlag();
				}
				if (GUILayout.Button("全クリア", this.uiParams.bStyle, new GUILayoutOption[]
				{
					guilayoutOption2,
					this.uiParams.optBtnHeight
				}))
				{
					this.holder.SetAllVisible();
				}
				if (GUILayout.Button("戻す", this.uiParams.bStyle, new GUILayoutOption[]
				{
					guilayoutOption2,
					this.uiParams.optBtnHeight
				}))
				{
					this.holder.FixFlag();
				}
			}
			finally
			{
				GUILayout.EndHorizontal();
			}
			if (GUILayout.Button("閉じる", this.uiParams.bStyle, new GUILayoutOption[]
			{
				this.uiParams.optSubConWidth,
				this.uiParams.optBtnHeight
			}))
			{
				this.SetMenu(AlwaysColorChangeEx.MenuType.Main);
			}
			GUI.DragWindow(this.uiParams.titleBarRect);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00004F10 File Offset: 0x00003110
		private bool InitDelNodes(TBodySkin body)
		{
			if (body == null)
			{
				if (this.holder.CurrentMaid == null)
				{
					return false;
				}
				if (0 >= this.holder.CurrentMaid.body0.goSlot.Count)
				{
					return false;
				}
				body = this.holder.CurrentMaid.body0.GetSlot(0);
			}
			Dictionary<string, bool> dicDelNodeBody = body.m_dicDelNodeBody;
			foreach (string key in ACConstants.NodeNames.Keys)
			{
				bool value;
				if (dicDelNodeBody.TryGetValue(key, out value))
				{
					this.dDelNodes[key] = value;
				}
			}
			return true;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00004FD0 File Offset: 0x000031D0
		private Dictionary<string, bool> GetDelNodes()
		{
			if (!this.dDelNodes.Any<KeyValuePair<string, bool>>())
			{
				this.InitDelNodes(null);
			}
			List<string> list = new List<string>(this.dDelNodes.Keys);
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>(this.dDelNodes);
			foreach (string key in list)
			{
				dictionary[key] = true;
			}
			foreach (TBodySkin tbodySkin in this.holder.CurrentMaid.body0.goSlot)
			{
				if (!(tbodySkin.obj == null) && tbodySkin.boVisible)
				{
					Dictionary<string, bool> dicDelNodeBody = tbodySkin.m_dicDelNodeBody;
					foreach (string text in list)
					{
						bool flag;
						if (dicDelNodeBody.TryGetValue(text, out flag))
						{
							Dictionary<string, bool> dictionary2;
							string key2;
							(dictionary2 = dictionary)[key2 = text] = (dictionary2[key2] && flag);
						}
					}
					if (tbodySkin.m_dicDelNodeParts.Any<KeyValuePair<string, Dictionary<string, bool>>>())
					{
						foreach (Dictionary<string, bool> dictionary3 in tbodySkin.m_dicDelNodeParts.Values)
						{
							foreach (KeyValuePair<string, bool> keyValuePair in dictionary3)
							{
								if (dictionary.ContainsKey(keyValuePair.Key))
								{
									Dictionary<string, bool> dictionary4;
									string key3;
									(dictionary4 = dictionary)[key3 = keyValuePair.Key] = (dictionary4[key3] & keyValuePair.Value);
								}
							}
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00005224 File Offset: 0x00003424
		private void DoNodeSelectMenu(int winID)
		{
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			GUILayoutOption guilayoutOption = GUILayout.Width((float)this.uiParams.fontSize * 10f);
			GUILayoutOption guilayoutOption2 = GUILayout.Width((float)this.uiParams.fontSize * 4f);
			try
			{
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.Label("表示ノード選択", this.uiParams.lStyleB, new GUILayoutOption[]
				{
					guilayoutOption
				});
				GUILayout.Space((float)this.uiParams.margin);
				this.nameSwitched = GUILayout.Toggle(this.nameSwitched, "表示切替", this.uiParams.tStyleS, new GUILayoutOption[]
				{
					this.uiParams.optToggleSWidth
				});
				GUILayout.EndHorizontal();
				if (this.holder.CurrentMaid == null)
				{
					return;
				}
				if (0 >= this.holder.CurrentMaid.body0.goSlot.Count)
				{
					return;
				}
				TBodySkin slot = this.holder.CurrentMaid.body0.GetSlot(0);
				if (!this.dDelNodes.Any<KeyValuePair<string, bool>>())
				{
					this.InitDelNodes(slot);
					this.dDelNodeDisps = this.GetDelNodes();
					foreach (KeyValuePair<string, bool> keyValuePair in this.dDelNodeDisps)
					{
						this.dDelNodes[keyValuePair.Key] = keyValuePair.Value;
					}
				}
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				try
				{
					GUILayoutOption guilayoutOption3 = GUILayout.Width(this.uiParams.subConWidth * 0.33f);
					if (GUILayout.Button("同期", this.uiParams.bStyle, new GUILayoutOption[]
					{
						this.uiParams.optBtnHeight,
						guilayoutOption3
					}))
					{
						this.SyncNodes();
					}
					if (GUILayout.Button("すべてON", this.uiParams.bStyle, new GUILayoutOption[]
					{
						this.uiParams.optBtnHeight,
						guilayoutOption3
					}))
					{
						List<string> list = new List<string>(this.dDelNodes.Keys);
						foreach (string key in list)
						{
							this.dDelNodes[key] = true;
						}
					}
					if (GUILayout.Button("すべてOFF", this.uiParams.bStyle, new GUILayoutOption[]
					{
						this.uiParams.optBtnHeight,
						guilayoutOption3
					}))
					{
						List<string> list2 = new List<string>(this.dDelNodes.Keys);
						foreach (string key2 in list2)
						{
							this.dDelNodes[key2] = false;
						}
					}
				}
				finally
				{
					GUILayout.EndHorizontal();
				}
				this.scrollViewPosition = GUILayout.BeginScrollView(this.scrollViewPosition, new GUILayoutOption[]
				{
					GUILayout.Width(this.uiParams.nodeSelectRect.width),
					GUILayout.Height(this.uiParams.nodeSelectRect.height)
				});
				GUIStyle lStyle = this.uiParams.lStyle;
				Color textColor = lStyle.normal.textColor;
				try
				{
					foreach (KeyValuePair<string, NodeItem> keyValuePair2 in ACConstants.NodeNames)
					{
						NodeItem value = keyValuePair2.Value;
						bool value2;
						if (!this.dDelNodes.TryGetValue(keyValuePair2.Key, out value2))
						{
							LogUtil.Debug(new object[]
							{
								"node name not found.",
								keyValuePair2.Key
							});
						}
						else
						{
							GUILayout.BeginHorizontal(new GUILayoutOption[0]);
							try
							{
								bool enabled = true;
								bool flag;
								string text;
								if (this.dDelNodeDisps.TryGetValue(keyValuePair2.Key, out flag))
								{
									if (flag)
									{
										text = "[表示中]";
										lStyle.normal.textColor = textColor;
									}
									else
									{
										text = "[非表示]";
										lStyle.normal.textColor = Color.magenta;
									}
								}
								else
								{
									text = "[不\u3000明]";
									lStyle.normal.textColor = Color.red;
									enabled = false;
								}
								GUILayout.Label(text, lStyle, new GUILayoutOption[]
								{
									guilayoutOption2
								});
								if (value.depth != 0)
								{
									GUILayout.Space((float)(this.uiParams.margin * value.depth * 3));
								}
								GUI.enabled = enabled;
								this.dDelNodes[keyValuePair2.Key] = GUILayout.Toggle(value2, (!this.nameSwitched) ? value.DisplayName : keyValuePair2.Key, this.uiParams.tStyle, new GUILayoutOption[]
								{
									this.uiParams.optContentWidth
								});
								GUI.enabled = true;
							}
							finally
							{
								GUILayout.EndHorizontal();
							}
						}
					}
				}
				finally
				{
					this.uiParams.lStyle.normal.textColor = textColor;
					GUI.EndScrollView();
				}
			}
			finally
			{
				GUILayout.EndVertical();
			}
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			try
			{
				if (GUILayout.Button("適用", this.uiParams.bStyle, new GUILayoutOption[]
				{
					this.uiParams.optSubConHalfWidth,
					this.uiParams.optBtnHeight
				}))
				{
					this.holder.SetDelNodes(this.dDelNodes, true);
					this.plugin.StartCoroutine(this.DelayFrame(3, new Action(this.SyncNodes)));
				}
				GUILayout.Space((float)this.uiParams.margin);
				if (GUILayout.Button("強制適用", this.uiParams.bStyle, new GUILayoutOption[]
				{
					this.uiParams.optSubConHalfWidth,
					this.uiParams.optBtnHeight
				}))
				{
					this.holder.SetDelNodesForce(this.dDelNodes, true);
					this.plugin.StartCoroutine(this.DelayFrame(3, new Action(this.SyncNodes)));
				}
			}
			finally
			{
				GUILayout.EndHorizontal();
			}
			if (GUILayout.Button("閉じる", this.uiParams.bStyle, new GUILayoutOption[]
			{
				this.uiParams.optSubConWidth,
				this.uiParams.optBtnHeight
			}))
			{
				this.SetMenu(AlwaysColorChangeEx.MenuType.Main);
			}
			GUI.DragWindow(this.uiParams.titleBarRect);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00005970 File Offset: 0x00003B70
		private void SyncNodes()
		{
			this.dDelNodeDisps = this.GetDelNodes();
			foreach (KeyValuePair<string, bool> keyValuePair in this.dDelNodeDisps)
			{
				this.dDelNodes[keyValuePair.Key] = keyValuePair.Value;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000059E4 File Offset: 0x00003BE4
		private void DoSaveMenu(int winID)
		{
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			try
			{
				GUILayout.Label("プリセット保存", this.uiParams.lStyleB, new GUILayoutOption[0]);
				GUILayout.Label("プリセット名", this.uiParams.lStyle, new GUILayoutOption[0]);
				string text = GUILayout.TextField(this.presetName, this.uiParams.textStyle, new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(true)
				});
				if (text != this.presetName)
				{
					this.bPresetSavable = !FileConst.HasInvalidChars(text);
					this.presetName = text;
				}
				if (this.bPresetSavable)
				{
					this.bPresetSavable &= (text.Trim().Length != 0);
				}
				this.scrollViewPosition = GUILayout.BeginScrollView(this.scrollViewPosition, new GUILayoutOption[]
				{
					this.uiParams.optSubConWidth,
					this.uiParams.optSubCon6Height
				});
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.Space((float)this.uiParams.marginL);
				GUILayout.Label("《保存済みプリセット一覧》", this.uiParams.lStyle, new GUILayoutOption[0]);
				GUILayout.EndHorizontal();
				foreach (string text2 in this.presetNames)
				{
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					GUILayout.Space((float)this.uiParams.marginL);
					if (GUILayout.Button(text2, this.uiParams.lStyleS, new GUILayoutOption[0]))
					{
						this.presetName = text2;
						this.bPresetSavable = !FileConst.HasInvalidChars(this.presetName);
					}
					GUILayout.EndHorizontal();
				}
				GUILayout.EndScrollView();
				GUI.enabled = this.bPresetSavable;
				if (GUILayout.Button("保存", this.uiParams.bStyle, new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(true)
				}))
				{
					this.SavePreset(this.presetName);
				}
				GUI.enabled = true;
				if (GUILayout.Button("閉じる", this.uiParams.bStyle, new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(true)
				}))
				{
					this.SetMenu(AlwaysColorChangeEx.MenuType.Main);
				}
			}
			finally
			{
				GUILayout.EndHorizontal();
				GUI.DragWindow(this.uiParams.titleBarRect);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00005C68 File Offset: 0x00003E68
		private void DoSelectPreset(int winId)
		{
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			try
			{
				GUILayout.Label("プリセット適用", this.uiParams.lStyleB, new GUILayoutOption[0]);
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.Space((float)this.uiParams.marginL);
				GUILayout.Label("《適用項目》", this.uiParams.lStyle, new GUILayoutOption[0]);
				GUILayout.Space((float)this.uiParams.marginL);
				this.bPresetApplyBodyProp = GUILayout.Toggle(this.bPresetApplyBodyProp, "身体設定値", this.uiParams.tStyle, new GUILayoutOption[0]);
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.Space((float)(this.uiParams.marginL * 2));
				this.bPresetApplyMask = GUILayout.Toggle(this.bPresetApplyMask, "マスク", this.uiParams.tStyle, new GUILayoutOption[0]);
				this.bPresetApplyNode = GUILayout.Toggle(this.bPresetApplyNode, "ノード表示", this.uiParams.tStyle, new GUILayoutOption[0]);
				this.bPresetApplyPartsColor = GUILayout.Toggle(this.bPresetApplyPartsColor, "無限色", this.uiParams.tStyle, new GUILayoutOption[0]);
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.Space((float)(this.uiParams.marginL * 2));
				this.bPresetApplyBody = GUILayout.Toggle(this.bPresetApplyBody, "身体", this.uiParams.tStyle, new GUILayoutOption[0]);
				this.bPresetApplyWear = GUILayout.Toggle(this.bPresetApplyWear, "衣装", this.uiParams.tStyle, new GUILayoutOption[0]);
				this.bPresetCastoff = GUILayout.Toggle(this.bPresetCastoff, "衣装外し", this.uiParams.tStyle, new GUILayoutOption[0]);
				GUILayout.EndHorizontal();
				this.scrollViewPosition = GUILayout.BeginScrollView(this.scrollViewPosition, new GUILayoutOption[]
				{
					this.uiParams.optSubConWidth,
					this.uiParams.optSubCon6Height
				});
				try
				{
					foreach (string text in this.presetNames)
					{
						GUILayout.BeginHorizontal(new GUILayoutOption[0]);
						if (GUILayout.Button(text, this.uiParams.bStyleL, new GUILayoutOption[0]) && this.ApplyPreset(text))
						{
							this.SetMenu(AlwaysColorChangeEx.MenuType.Main);
						}
						if (GUILayout.Button("削除", this.uiParams.bStyle, new GUILayoutOption[]
						{
							this.uiParams.optDBtnWidth
						}))
						{
							this.DeletePreset(text);
						}
						GUILayout.EndHorizontal();
					}
				}
				finally
				{
					GUILayout.EndScrollView();
				}
				if (GUILayout.Button("閉じる", this.uiParams.bStyle, new GUILayoutOption[0]))
				{
					this.SetMenu(AlwaysColorChangeEx.MenuType.Main);
				}
			}
			finally
			{
				GUILayout.EndHorizontal();
				GUI.DragWindow(this.uiParams.titleBarRect);
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00005FA0 File Offset: 0x000041A0
		private void DeletePreset(string presetName1)
		{
			if (!Directory.Exists(this.settings.presetDirPath))
			{
				return;
			}
			string presetFilepath = this.presetMgr.GetPresetFilepath(presetName1);
			if (!File.Exists(presetFilepath))
			{
				return;
			}
			File.Delete(presetFilepath);
			this.LoadPresetList();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00005FE4 File Offset: 0x000041E4
		private void SavePreset(string presetName1)
		{
			if (!Directory.Exists(this.settings.presetDirPath))
			{
				Directory.CreateDirectory(this.settings.presetDirPath);
			}
			try
			{
				string presetFilepath = this.presetMgr.GetPresetFilepath(presetName1);
				this.dDelNodeDisps = this.GetDelNodes();
				this.presetMgr.Save(presetFilepath, presetName1, this.dDelNodeDisps);
				this.SetMenu(AlwaysColorChangeEx.MenuType.Main);
				this.LoadPresetList();
			}
			catch (Exception ex)
			{
				LogUtil.Error(new object[]
				{
					ex
				});
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00006074 File Offset: 0x00004274
		private bool ApplyPreset(string presetName1)
		{
			LogUtil.Debug(new object[]
			{
				"Applying Preset. ",
				presetName1
			});
			string presetFilepath = this.presetMgr.GetPresetFilepath(presetName1);
			if (!File.Exists(presetFilepath))
			{
				return false;
			}
			this.currentPreset = this.presetMgr.Load(presetFilepath);
			if (this.currentPreset == null)
			{
				return false;
			}
			this.ApplyPreset(this.currentPreset);
			return true;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000060DC File Offset: 0x000042DC
		private void ApplyPreset(PresetData preset)
		{
			if (preset == null)
			{
				return;
			}
			Maid currentMaid = this.holder.CurrentMaid;
			if (preset.mpns.Any<CCMPN>())
			{
				this.presetMgr.ApplyPresetMPN(currentMaid, preset, this.bPresetApplyBody, this.bPresetApplyWear, this.bPresetCastoff);
			}
			if (this.bPresetApplyBodyProp & preset.mpnvals.Any<CCMPNValue>())
			{
				this.presetMgr.ApplyPresetMPNProp(currentMaid, preset);
			}
			currentMaid.AllProcPropSeqStart();
			this.toApplyPresetMaid = currentMaid;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00006154 File Offset: 0x00004354
		private bool ApplyPresetProp(Maid targetMaid, PresetData preset)
		{
			try
			{
				if (targetMaid.boAllProcPropBUSY)
				{
					LogUtil.Debug(new object[]
					{
						"recall apply preset"
					});
					return false;
				}
				if (this.holder.CurrentMaid != targetMaid)
				{
					return true;
				}
				if (this.bPresetApplyWear)
				{
					this.presetMgr.ApplyPresetMaterial(targetMaid, preset);
				}
				if (this.bPresetApplyNode && preset.delNodes != null)
				{
					foreach (KeyValuePair<string, bool> keyValuePair in preset.delNodes)
					{
						this.dDelNodes[keyValuePair.Key] = keyValuePair.Value;
					}
					this.holder.SetDelNodes(targetMaid, preset, false);
				}
				if (this.bPresetApplyMask)
				{
					this.holder.SetMaskSlots(targetMaid, preset);
				}
				this.holder.FixFlag(targetMaid, false);
				if (this.bPresetApplyPartsColor && preset.partsColors.Any<KeyValuePair<string, CCPartsColor>>())
				{
					this.presetMgr.ApplyPresetPartsColor(targetMaid, preset);
				}
			}
			finally
			{
				LogUtil.Debug(new object[]
				{
					"Preset applied"
				});
			}
			return true;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00006298 File Offset: 0x00004498
		private void LoadPresetList()
		{
			try
			{
				if (!Directory.Exists(this.settings.presetDirPath))
				{
					this.presetNames.Clear();
				}
				else
				{
					string[] files = Directory.GetFiles(this.settings.presetDirPath, "*.json", SearchOption.AllDirectories);
					int num = files.Count<string>();
					if (num == 0)
					{
						this.presetNames.Clear();
					}
					else
					{
						Array.Sort<string>(files);
						this.presetNames.Clear();
						this.presetNames.Capacity = num;
						foreach (string path in files)
						{
							this.presetNames.Add(Path.GetFileNameWithoutExtension(path));
						}
					}
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

		// Token: 0x06000037 RID: 55 RVA: 0x00006368 File Offset: 0x00004568
		private void DoSaveModDialog(int winId)
		{
			try
			{
				this.saveView.Show();
			}
			catch (Exception ex)
			{
				LogUtil.Debug(new object[]
				{
					"failed to display save dialog.",
					ex
				});
			}
			GUI.DragWindow(this.uiParams.titleBarRect);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000063C0 File Offset: 0x000045C0
		private void DoSelectBoneSlot(int winId)
		{
			try
			{
				this.boneSlotView.Show();
			}
			finally
			{
				if (GUILayout.Button("閉じる", this.uiParams.bStyle, new GUILayoutOption[]
				{
					this.uiParams.optSubConWidth,
					this.uiParams.optBtnHeight
				}))
				{
					this.SetMenu(AlwaysColorChangeEx.MenuType.Main);
				}
				GUI.DragWindow(this.uiParams.titleBarRect);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00006440 File Offset: 0x00004640
		private void DoEditPartsColor(int winId)
		{
			try
			{
				this.partsColorView.Show();
			}
			finally
			{
				if (GUILayout.Button("閉じる", this.uiParams.bStyle, new GUILayoutOption[]
				{
					this.uiParams.optSubConWidth,
					this.uiParams.optBtnHeight
				}))
				{
					this.SetMenu(AlwaysColorChangeEx.MenuType.Main);
				}
				GUI.DragWindow(this.uiParams.titleBarRect);
			}
		}

		// Token: 0x04000001 RID: 1
		private const string TITLE_LABEL = "ACCex : ";

		// Token: 0x04000002 RID: 2
		private const int WIN_ID_MAIN = 20201;

		// Token: 0x04000003 RID: 3
		private const int WIN_ID_DIALOG = 20202;

		// Token: 0x04000004 RID: 4
		private const int WIN_ID_TIPS = 20203;

		// Token: 0x04000005 RID: 5
		private const EventModifiers modifierKey = EventModifiers.Shift | EventModifiers.Control | EventModifiers.Alt;

		// Token: 0x04000006 RID: 6
		private const int applyDelayFrame = 10;

		// Token: 0x04000007 RID: 7
		private const int tipsSecond = 2;

		// Token: 0x04000008 RID: 8
		private const int TIPS_MARGIN = 24;

		// Token: 0x04000009 RID: 9
		public static volatile string PluginName;

		// Token: 0x0400000A RID: 10
		public static volatile string Version;

		// Token: 0x0400000B RID: 11
		internal MonoBehaviour plugin;

		// Token: 0x0400000C RID: 12
		private readonly CM3D2SceneChecker checker = new CM3D2SceneChecker();

		// Token: 0x0400000D RID: 13
		private float fPassedTime;

		// Token: 0x0400000E RID: 14
		private float fLastInitTime;

		// Token: 0x0400000F RID: 15
		private bool initialized;

		// Token: 0x04000010 RID: 16
		private bool bUseStockMaid;

		// Token: 0x04000011 RID: 17
		private bool mouseDowned;

		// Token: 0x04000012 RID: 18
		private readonly Settings settings = Settings.Instance;

		// Token: 0x04000013 RID: 19
		private readonly UIParams uiParams = UIParams.Instance;

		// Token: 0x04000014 RID: 20
		private readonly MaidHolder holder = MaidHolder.Instance;

		// Token: 0x04000015 RID: 21
		private readonly PresetManager presetMgr = new PresetManager();

		// Token: 0x04000016 RID: 22
		private AlwaysColorChangeEx.MenuType menuType;

		// Token: 0x04000017 RID: 23
		private readonly List<string> presetNames = new List<string>();

		// Token: 0x04000018 RID: 24
		private readonly Dictionary<string, bool> dDelNodes = new Dictionary<string, bool>();

		// Token: 0x04000019 RID: 25
		private Dictionary<string, bool> dDelNodeDisps = new Dictionary<string, bool>();

		// Token: 0x0400001A RID: 26
		private readonly Dictionary<TBody.SlotID, MaskInfo> dMaskSlots = new Dictionary<TBody.SlotID, MaskInfo>();

		// Token: 0x0400001B RID: 27
		private PresetData currentPreset;

		// Token: 0x0400001C RID: 28
		private string presetName = string.Empty;

		// Token: 0x0400001D RID: 29
		private bool bPresetCastoff = true;

		// Token: 0x0400001E RID: 30
		private bool bPresetApplyNode;

		// Token: 0x0400001F RID: 31
		private bool bPresetApplyMask;

		// Token: 0x04000020 RID: 32
		private bool bPresetApplyBody = true;

		// Token: 0x04000021 RID: 33
		private bool bPresetApplyWear = true;

		// Token: 0x04000022 RID: 34
		private bool bPresetApplyBodyProp = true;

		// Token: 0x04000023 RID: 35
		private bool bPresetApplyPartsColor = true;

		// Token: 0x04000024 RID: 36
		private bool bPresetSavable;

		// Token: 0x04000025 RID: 37
		private Maid toApplyPresetMaid;

		// Token: 0x04000026 RID: 38
		private bool isSavable;

		// Token: 0x04000027 RID: 39
		private bool isActive;

		// Token: 0x04000028 RID: 40
		private bool texSliderUpped;

		// Token: 0x04000029 RID: 41
		private readonly IntervalCounter changeCounter = new IntervalCounter(15);

		// Token: 0x0400002A RID: 42
		private readonly IntervalCounter refreshCounter = new IntervalCounter(60);

		// Token: 0x0400002B RID: 43
		private readonly MaidChangeDetector changeDetector = new MaidChangeDetector();

		// Token: 0x0400002C RID: 44
		private readonly AnimTargetDetector animDetector = new AnimTargetDetector();

		// Token: 0x0400002D RID: 45
		private Vector2 scrollViewPosition = Vector2.zero;

		// Token: 0x0400002E RID: 46
		private bool nameSwitched;

		// Token: 0x0400002F RID: 47
		private readonly Dictionary<int, GUIContent> _contentDic = new Dictionary<int, GUIContent>();

		// Token: 0x04000030 RID: 48
		private readonly List<AlwaysColorChangeEx.SelectMaidData> _maidList = new List<AlwaysColorChangeEx.SelectMaidData>();

		// Token: 0x04000031 RID: 49
		private readonly List<AlwaysColorChangeEx.SelectMaidData> _manList = new List<AlwaysColorChangeEx.SelectMaidData>();

		// Token: 0x04000032 RID: 50
		private int targetMenuId;

		// Token: 0x04000033 RID: 51
		private bool slotDropped;

		// Token: 0x04000034 RID: 52
		private Material[] targetMaterials;

		// Token: 0x04000035 RID: 53
		private readonly Material[] EMPTY_ARRAY = new Material[0];

		// Token: 0x04000036 RID: 54
		private List<ACCMaterialsView> materialViews;

		// Token: 0x04000037 RID: 55
		private List<ACCTexturesView> texViews;

		// Token: 0x04000038 RID: 56
		private ACCSaveMenuView saveView;

		// Token: 0x04000039 RID: 57
		private ACCBoneSlotView boneSlotView;

		// Token: 0x0400003A RID: 58
		private ACCPartsColorView partsColorView;

		// Token: 0x0400003B RID: 59
		private readonly List<BaseView> views = new List<BaseView>();

		// Token: 0x0400003C RID: 60
		private Maid selectedMaid;

		// Token: 0x0400003D RID: 61
		private string selectedName;

		// Token: 0x0400003E RID: 62
		private GUIContent title;

		// Token: 0x0400003F RID: 63
		private bool displayTips;

		// Token: 0x04000040 RID: 64
		private Rect tipRect;

		// Token: 0x04000041 RID: 65
		private string tips;

		// Token: 0x04000042 RID: 66
		private readonly UIHelper uiHelper = new UIHelper();

		// Token: 0x04000043 RID: 67
		private ColorPresetManager colorPresetMgr;

		// Token: 0x04000044 RID: 68
		private SliderHelper sliderHelper;

		// Token: 0x04000045 RID: 69
		private CheckboxHelper cbHelper;

		// Token: 0x02000004 RID: 4
		private enum MenuType
		{
			// Token: 0x04000048 RID: 72
			None,
			// Token: 0x04000049 RID: 73
			Main,
			// Token: 0x0400004A RID: 74
			Color,
			// Token: 0x0400004B RID: 75
			NodeSelect,
			// Token: 0x0400004C RID: 76
			MaskSelect,
			// Token: 0x0400004D RID: 77
			Save,
			// Token: 0x0400004E RID: 78
			PresetSelect,
			// Token: 0x0400004F RID: 79
			Texture,
			// Token: 0x04000050 RID: 80
			MaidSelect,
			// Token: 0x04000051 RID: 81
			BoneSlotSelect,
			// Token: 0x04000052 RID: 82
			PartsColor
		}

		// Token: 0x02000005 RID: 5
		internal class SelectMaidData
		{
			// Token: 0x0600003D RID: 61 RVA: 0x000064C0 File Offset: 0x000046C0
			internal SelectMaidData(Maid maid0, GUIContent content0)
			{
				this.maid = maid0;
				this.content = content0;
			}

			// Token: 0x04000053 RID: 83
			public readonly Maid maid;

			// Token: 0x04000054 RID: 84
			public readonly GUIContent content;
		}
	}
}
