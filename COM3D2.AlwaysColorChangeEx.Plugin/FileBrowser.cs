using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin
{
	// Token: 0x02000008 RID: 8
	public class FileBrowser
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000064D6 File Offset: 0x000046D6
		// (set) Token: 0x0600003F RID: 63 RVA: 0x000064DE File Offset: 0x000046DE
		public string CurrentDirectory
		{
			get
			{
				return this.currentDir;
			}
			set
			{
				this.SetNewDirectory(value);
				this.SwitchDirectoryNow();
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000064ED File Offset: 0x000046ED
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000064F5 File Offset: 0x000046F5
		public string[] SelectionPatterns
		{
			get
			{
				return this.filePatterns;
			}
			set
			{
				this.filePatterns = value;
				this.ReadDirectoryContents();
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00006504 File Offset: 0x00004704
		// (set) Token: 0x06000043 RID: 67 RVA: 0x0000650C File Offset: 0x0000470C
		public Texture2D DirectoryImage { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00006515 File Offset: 0x00004715
		// (set) Token: 0x06000045 RID: 69 RVA: 0x0000651D File Offset: 0x0000471D
		public Texture2D FileImage { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00006526 File Offset: 0x00004726
		// (set) Token: 0x06000047 RID: 71 RVA: 0x0000652E File Offset: 0x0000472E
		public Texture2D NoFileImage { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00006537 File Offset: 0x00004737
		// (set) Token: 0x06000049 RID: 73 RVA: 0x0000653F File Offset: 0x0000473F
		public FileBrowserType BrowserType
		{
			get
			{
				return this.browserType;
			}
			set
			{
				this.browserType = value;
				this.ReadDirectoryContents();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00006550 File Offset: 0x00004750
		protected GUIStyle CentredText
		{
			get
			{
				GUIStyle result;
				if ((result = this.centredText) == null)
				{
					result = (this.centredText = new GUIStyle(GUI.skin.label)
					{
						alignment = TextAnchor.MiddleLeft,
						fixedHeight = GUI.skin.button.fixedHeight
					});
				}
				return result;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000065A0 File Offset: 0x000047A0
		public FileBrowser(Rect screenRect, string name, FileBrowser.FinishedCallback callback)
		{
			this.name = name;
			this.screenRect = screenRect;
			this.browserType = FileBrowserType.File;
			this.callback = callback;
			this.SetNewDirectory(Directory.GetCurrentDirectory());
			this.SwitchDirectoryNow();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000661E File Offset: 0x0000481E
		protected void SetNewDirectory(string directory)
		{
			this.newDir = directory;
			this.nextDirs.Clear();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00006634 File Offset: 0x00004834
		protected void SwitchDirectoryNow()
		{
			if (this.newDir == null || this.currentDir == this.newDir)
			{
				return;
			}
			if (Directory.Exists(this.currentDir))
			{
				this.historyDirs.AddLast(this.currentDir);
				if (this.historyDirs.Count > this.historySize)
				{
					this.historyDirs.RemoveFirst();
				}
			}
			this.currentDir = this.newDir;
			this.reloadDir();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000066AC File Offset: 0x000048AC
		private void reloadDir()
		{
			this.scrollPosition = Vector2.zero;
			this.selectedDir = (this.selectedNonMatchingDirs = (this.selectedFile = -1));
			this.ReadDirectoryContents();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000066E4 File Offset: 0x000048E4
		public void Prev()
		{
			if (!this.historyDirs.Any<string>())
			{
				return;
			}
			this.nextDirs.AddFirst(this.currentDir);
			string text = this.historyDirs.Last<string>();
			this.historyDirs.RemoveLast();
			this.currentDir = text;
			this.reloadDir();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00006738 File Offset: 0x00004938
		public void Next()
		{
			if (!this.nextDirs.Any<string>())
			{
				return;
			}
			this.historyDirs.AddLast(this.currentDir);
			string text = this.nextDirs.First<string>();
			this.nextDirs.RemoveFirst();
			this.currentDir = text;
			this.reloadDir();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000067AC File Offset: 0x000049AC
		protected void ReadDirectoryContents()
		{
			if (this.currentDir == "/")
			{
				this.currentDirParts = new string[]
				{
					string.Empty
				};
				this.currentDirMatches = false;
			}
			else
			{
				this.currentDirParts = this.currentDir.Split(new char[]
				{
					Path.DirectorySeparatorChar
				});
				if (this.SelectionPatterns != null)
				{
					this.currentDirMatches = false;
					foreach (string searchPattern in this.SelectionPatterns)
					{
						string directoryName = Path.GetDirectoryName(this.currentDir);
						if (directoryName != null)
						{
							string[] array = Directory.GetDirectories(directoryName, searchPattern);
							this.currentDirMatches = (Array.IndexOf<string>(array, this.currentDir) >= 0);
							if (this.currentDirMatches)
							{
								break;
							}
						}
					}
				}
				else
				{
					this.currentDirMatches = false;
				}
			}
			if (this.BrowserType == FileBrowserType.File || this.SelectionPatterns == null)
			{
				this.directories = Directory.GetDirectories(this.currentDir);
				this.nonMatchingDirs = new string[0];
			}
			else
			{
				List<string> list = new List<string>();
				foreach (string searchPattern2 in this.SelectionPatterns)
				{
					string[] collection = Directory.GetDirectories(this.currentDir, searchPattern2);
					list.AddRange(collection);
				}
				this.directories = list.ToArray();
				this.nonMatchingDirs = (from subDir in Directory.GetDirectories(this.currentDir)
				where Array.IndexOf<string>(this.directories, subDir) < 0
				select subDir).ToArray<string>();
				for (int k = 0; k < this.nonMatchingDirs.Length; k++)
				{
					int num = this.nonMatchingDirs[k].LastIndexOf(Path.DirectorySeparatorChar);
					this.nonMatchingDirs[k] = this.nonMatchingDirs[k].Substring(num + 1);
				}
				Array.Sort<string>(this.nonMatchingDirs);
			}
			for (int l = 0; l < this.directories.Length; l++)
			{
				this.directories[l] = this.directories[l].Substring(this.directories[l].LastIndexOf(Path.DirectorySeparatorChar) + 1);
			}
			if (this.BrowserType == FileBrowserType.Directory || this.SelectionPatterns == null)
			{
				this.files = Directory.GetFiles(this.currentDir);
				this.nonMatchingFiles = new string[0];
			}
			else
			{
				if (this.SelectionPatterns != null)
				{
					List<string> list2 = new List<string>();
					foreach (string searchPattern3 in this.SelectionPatterns)
					{
						string[] array2 = Directory.GetFiles(this.currentDir, searchPattern3);
						if (array2.Length > 0)
						{
							list2.AddRange(array2);
						}
					}
					this.files = list2.ToArray();
				}
				else
				{
					this.files = new string[0];
				}
				this.nonMatchingFiles = (from filePath in Directory.GetFiles(this.currentDir)
				where Array.IndexOf<string>(this.files, filePath) < 0
				select filePath).ToArray<string>();
				for (int n = 0; n < this.nonMatchingFiles.Length; n++)
				{
					this.nonMatchingFiles[n] = Path.GetFileName(this.nonMatchingFiles[n]);
				}
				Array.Sort<string>(this.nonMatchingFiles);
			}
			for (int num2 = 0; num2 < this.files.Length; num2++)
			{
				this.files[num2] = Path.GetFileName(this.files[num2]);
			}
			Array.Sort<string>(this.files);
			this.BuildContent();
			this.newDir = null;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00006B18 File Offset: 0x00004D18
		protected void BuildContent()
		{
			this.dirsWithImages = new GUIContent[this.directories.Length];
			for (int i = 0; i < this.dirsWithImages.Length; i++)
			{
				this.dirsWithImages[i] = new GUIContent(this.directories[i], this.DirectoryImage);
			}
			this.nonMatchingDirsWithImages = new GUIContent[this.nonMatchingDirs.Length];
			for (int j = 0; j < this.nonMatchingDirsWithImages.Length; j++)
			{
				this.nonMatchingDirsWithImages[j] = new GUIContent(this.nonMatchingDirs[j], this.DirectoryImage);
			}
			this.filesWithImages = new GUIContent[this.files.Length];
			for (int k = 0; k < this.filesWithImages.Length; k++)
			{
				this.filesWithImages[k] = new GUIContent(this.files[k], this.FileImage);
			}
			this.nonMatchingFilesWithImages = new GUIContent[this.nonMatchingFiles.Length];
			for (int l = 0; l < this.nonMatchingFilesWithImages.Length; l++)
			{
				this.nonMatchingFilesWithImages[l] = new GUIContent(this.nonMatchingFiles[l], this.NoFileImage);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00006C29 File Offset: 0x00004E29
		public void Update()
		{
			this.SwitchDirectoryNow();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00006C34 File Offset: 0x00004E34
		public void OnGUI()
		{
			GUILayout.BeginArea(this.screenRect, this.name, GUI.skin.window);
			try
			{
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				try
				{
					for (int i = 0; i < this.currentDirParts.Length; i++)
					{
						if (i == this.currentDirParts.Length - 1)
						{
							GUILayout.Label(this.currentDirParts[i], this.CentredText, new GUILayoutOption[0]);
						}
						else if (GUILayout.Button(this.currentDirParts[i], new GUILayoutOption[0]))
						{
							string directoryName = this.currentDir;
							for (int j = this.currentDirParts.Length - 1; j > i; j--)
							{
								directoryName = Path.GetDirectoryName(directoryName);
							}
							this.SetNewDirectory(directoryName);
						}
					}
					GUILayout.FlexibleSpace();
					GUI.enabled = this.historyDirs.Any<string>();
					if (GUILayout.Button("<", new GUILayoutOption[0]))
					{
						this.Prev();
					}
					GUI.enabled = this.nextDirs.Any<string>();
					if (GUILayout.Button(">", new GUILayoutOption[0]))
					{
						this.Next();
					}
				}
				finally
				{
					GUI.enabled = true;
					GUILayout.EndHorizontal();
				}
				this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition, false, true, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.box, new GUILayoutOption[0]);
				this.selectedDir = GUILayoutx.SelectionList(this.selectedDir, this.dirsWithImages, this.labelStyle, new GUILayoutx.ClickCallback(this.DirectoryClickCallback));
				if (this.selectedDir > -1)
				{
					this.selectedFile = (this.selectedNonMatchingDirs = -1);
				}
				this.selectedNonMatchingDirs = GUILayoutx.SelectionList(this.selectedNonMatchingDirs, this.nonMatchingDirsWithImages, this.labelStyle, new GUILayoutx.ClickCallback(this.NonMatchingDirectoryClickCallback));
				if (this.selectedNonMatchingDirs > -1)
				{
					this.selectedDir = (this.selectedFile = -1);
				}
				GUI.enabled = (this.BrowserType == FileBrowserType.File);
				this.selectedFile = GUILayoutx.SelectionList(this.selectedFile, this.filesWithImages, this.labelStyle, new GUILayoutx.ClickCallback(this.FileClickCallback));
				GUI.enabled = true;
				if (this.selectedFile > -1)
				{
					this.selectedDir = (this.selectedNonMatchingDirs = -1);
				}
				GUI.enabled = false;
				GUILayoutx.SelectionList(-1, this.nonMatchingFilesWithImages, this.labelStyle, null);
				GUI.enabled = true;
				GUILayout.EndScrollView();
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.Label(this.selectedName, new GUILayoutOption[0]);
				GUILayout.FlexibleSpace();
				if (GUILayout.Button("キャンセル", new GUILayoutOption[]
				{
					GUILayout.Width(120f)
				}))
				{
					this.callback(null);
				}
				if (this.BrowserType == FileBrowserType.File)
				{
					GUI.enabled = (this.selectedFile > -1);
					this.selectedName = (GUI.enabled ? this.filesWithImages[this.selectedFile].text : string.Empty);
				}
				else if (this.SelectionPatterns == null)
				{
					GUI.enabled = (this.selectedDir > -1);
					this.selectedName = (GUI.enabled ? this.dirsWithImages[this.selectedDir].text : string.Empty);
				}
				else
				{
					GUI.enabled = (this.selectedDir > -1 || (this.currentDirMatches && this.selectedNonMatchingDirs == -1 && this.selectedFile == -1));
					this.selectedName = ((this.selectedDir > -1) ? this.dirsWithImages[this.selectedDir].text : string.Empty);
				}
				if (GUILayout.Button("選択", new GUILayoutOption[]
				{
					GUILayout.Width(120f)
				}))
				{
					if (this.BrowserType == FileBrowserType.File)
					{
						this.callback(Path.Combine(this.currentDir, this.files[this.selectedFile]));
					}
					else
					{
						this.callback((this.selectedDir > -1) ? Path.Combine(this.currentDir, this.directories[this.selectedDir]) : this.currentDir);
					}
				}
				GUI.enabled = true;
				GUILayout.EndHorizontal();
			}
			finally
			{
				GUILayout.EndArea();
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00007070 File Offset: 0x00005270
		protected void FileClickCallback(int i)
		{
			FileBrowserType fileBrowserType = this.BrowserType;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00007079 File Offset: 0x00005279
		protected void DirectoryClickCallback(int i)
		{
			this.SetNewDirectory(Path.Combine(this.currentDir, this.directories[i]));
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00007094 File Offset: 0x00005294
		protected void NonMatchingDirectoryClickCallback(int i)
		{
			this.SetNewDirectory(Path.Combine(this.currentDir, this.nonMatchingDirs[i]));
		}

		// Token: 0x0400005D RID: 93
		protected string currentDir;

		// Token: 0x0400005E RID: 94
		protected string[] filePatterns;

		// Token: 0x0400005F RID: 95
		public GUIStyle labelStyle = new GUIStyle("Label");

		// Token: 0x04000060 RID: 96
		protected FileBrowserType browserType;

		// Token: 0x04000061 RID: 97
		protected string newDir;

		// Token: 0x04000062 RID: 98
		protected string[] currentDirParts;

		// Token: 0x04000063 RID: 99
		protected string[] files;

		// Token: 0x04000064 RID: 100
		protected GUIContent[] filesWithImages;

		// Token: 0x04000065 RID: 101
		protected int selectedFile;

		// Token: 0x04000066 RID: 102
		protected string selectedName = string.Empty;

		// Token: 0x04000067 RID: 103
		protected string[] nonMatchingFiles;

		// Token: 0x04000068 RID: 104
		protected GUIContent[] nonMatchingFilesWithImages;

		// Token: 0x04000069 RID: 105
		protected int selectedNonMatchingDirs;

		// Token: 0x0400006A RID: 106
		protected string[] directories;

		// Token: 0x0400006B RID: 107
		protected GUIContent[] dirsWithImages;

		// Token: 0x0400006C RID: 108
		protected int selectedDir;

		// Token: 0x0400006D RID: 109
		protected string[] nonMatchingDirs;

		// Token: 0x0400006E RID: 110
		protected GUIContent[] nonMatchingDirsWithImages;

		// Token: 0x0400006F RID: 111
		protected bool currentDirMatches;

		// Token: 0x04000070 RID: 112
		protected GUIStyle centredText;

		// Token: 0x04000071 RID: 113
		protected Rect screenRect;

		// Token: 0x04000072 RID: 114
		protected Vector2 scrollPosition;

		// Token: 0x04000073 RID: 115
		protected readonly FileBrowser.FinishedCallback callback;

		// Token: 0x04000074 RID: 116
		public readonly string name;

		// Token: 0x04000075 RID: 117
		public int historySize = 20;

		// Token: 0x04000076 RID: 118
		private readonly LinkedList<string> historyDirs = new LinkedList<string>();

		// Token: 0x04000077 RID: 119
		private readonly LinkedList<string> nextDirs = new LinkedList<string>();

		// Token: 0x02000009 RID: 9
		// (Invoke) Token: 0x0600005B RID: 91
		public delegate void FinishedCallback(string path);
	}
}
