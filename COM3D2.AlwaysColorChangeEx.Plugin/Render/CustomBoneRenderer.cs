using System;
using System.Collections.Generic;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Render
{
	// Token: 0x02000075 RID: 117
	public class CustomBoneRenderer
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0002244D File Offset: 0x0002064D
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x00022455 File Offset: 0x00020655
		public Color Color
		{
			get
			{
				return this._color;
			}
			set
			{
				this._color = value;
				this._lineMaterial.color = this._color;
				this.SetColor(ref this._color);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0002247B File Offset: 0x0002067B
		// (set) Token: 0x060003DA RID: 986 RVA: 0x00022483 File Offset: 0x00020683
		public Color OffColor
		{
			get
			{
				return this._offColor;
			}
			set
			{
				this._offColor = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0002248C File Offset: 0x0002068C
		// (set) Token: 0x060003DC RID: 988 RVA: 0x00022494 File Offset: 0x00020694
		public int ItemID { get; private set; }

		// Token: 0x060003DD RID: 989 RVA: 0x000224A0 File Offset: 0x000206A0
		~CustomBoneRenderer()
		{
			this.ClearCache();
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003DE RID: 990 RVA: 0x000224CC File Offset: 0x000206CC
		// (set) Token: 0x060003DF RID: 991 RVA: 0x000224D4 File Offset: 0x000206D4
		public int TargetId { get; set; }

		// Token: 0x060003E0 RID: 992 RVA: 0x000224DD File Offset: 0x000206DD
		public bool IsEnabled()
		{
			return this._meshRenderer != null && this._rootBone != null;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000224FB File Offset: 0x000206FB
		public bool IsVisible()
		{
			return this._isVisible;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00022503 File Offset: 0x00020703
		public void SetVisible(bool visible)
		{
			if (this._isVisible != visible)
			{
				this.SetVisibleAll(visible);
			}
			this._isVisible = visible;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0002251C File Offset: 0x0002071C
		private void SetVisibleAll(bool visible)
		{
			foreach (GameObject gameObject in this._cache)
			{
				gameObject.SetActive(visible);
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00022570 File Offset: 0x00020770
		public void Setup(GameObject go, int id = -1)
		{
			this.ItemID = id;
			this.Clear();
			this._boneNames.Clear();
			this._meshRenderer = go.GetComponentInChildren<SkinnedMeshRenderer>(false);
			if (this._meshRenderer == null)
			{
				return;
			}
			if (this._lineMaterial == null)
			{
				this._lineMaterial = this.CreateMaterial();
				this._lineMaterial.color = this._color;
			}
			if (this._sublineMaterial == null)
			{
				this._sublineMaterial = this.CreateMaterial();
				this._sublineMaterial.color = this._offColor;
			}
			foreach (Transform transform in this._meshRenderer.bones)
			{
				if (!(transform == null))
				{
					this._boneNames.Add(transform.name.EndsWith("_SCL_") ? transform.name.Substring(0, transform.name.Length - "_SCL_".Length) : transform.name);
				}
			}
			Transform transform2 = this._meshRenderer.rootBone;
			if (transform2 != null)
			{
				this._rootBone = transform2;
				this.SetupBone(transform2);
			}
			else
			{
				transform2 = go.transform;
				foreach (object obj in transform2)
				{
					Transform transform3 = (Transform)obj;
					if (transform3.childCount != 0)
					{
						int count = this._lineDict.Count;
						this.SetupBone(transform3);
						this._rootBone = transform3;
						if (this._lineDict.Count - count > 1)
						{
							break;
						}
					}
				}
			}
			foreach (GameObject gameObject in this._cache)
			{
				gameObject.SetActive(this._isVisible);
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00022770 File Offset: 0x00020970
		private void SetupBone(Transform bone)
		{
			if (this._lineDict.ContainsKey(bone.name))
			{
				return;
			}
			LineRenderer lineRenderer = this.CreateComponent(this._lineMaterial, this._lineWidth, this._lineWidth * 0.2f);
			lineRenderer.gameObject.name = "___LINE_" + bone.name;
			this._lineDict.Add(bone.name, lineRenderer);
			if (!this._boneNames.Contains(bone.name))
			{
				lineRenderer.materials = new Material[]
				{
					this._sublineMaterial
				};
			}
			foreach (object obj in bone)
			{
				Transform transform = (Transform)obj;
				if (transform.childCount != 0 && !transform.name.StartsWith("___"))
				{
					this.SetupBone(transform);
				}
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0002286C File Offset: 0x00020A6C
		private void UpdateVisible(bool visible)
		{
			if (this._skipVisible != visible)
			{
				return;
			}
			this._skipVisible = !visible;
			this.SetVisibleAll(visible);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0002288C File Offset: 0x00020A8C
		public void Update()
		{
			if (this._rootBone == null)
			{
				if (this._isVisible)
				{
					this.SetVisible(false);
				}
				return;
			}
			if (!this._meshRenderer.gameObject.activeSelf)
			{
				this.UpdateVisible(false);
				return;
			}
			this.UpdateVisible(true);
			if (this._rootBone.gameObject.activeSelf)
			{
				this.UpdatePosition(this._rootBone, true, false);
			}
			foreach (object obj in this._rootBone)
			{
				Transform transform = (Transform)obj;
				if (transform.childCount != 0 && transform.gameObject.activeSelf)
				{
					this.UpdatePosition(transform, false, true);
				}
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0002295C File Offset: 0x00020B5C
		private void EmptyBone(LineRenderer renderer)
		{
			renderer.positionCount = 0;
			renderer.enabled = false;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0002296C File Offset: 0x00020B6C
		public void UpdatePosition(Transform bone, bool isRoot = false, bool isRecursive = false)
		{
			LineRenderer lineRenderer;
			if (!this._lineDict.TryGetValue(bone.name, out lineRenderer))
			{
				return;
			}
			if (bone.childCount == 0)
			{
				this.EmptyBone(lineRenderer);
				return;
			}
			lineRenderer.SetPosition(0, bone.position);
			Vector3? vector = null;
			if (bone.childCount == 1)
			{
				Transform child = bone.GetChild(0);
				if (child.name.StartsWith("___"))
				{
					this.EmptyBone(lineRenderer);
					return;
				}
				vector = new Vector3?(child.position);
				if (vector == bone.position)
				{
					vector = new Vector3?(bone.position + bone.rotation * this.UNIT_VECTOR3);
				}
			}
			else
			{
				if (bone.childCount == 2)
				{
					Transform child2 = bone.GetChild(0);
					Transform child3 = bone.GetChild(1);
					if (child2.name.EndsWith("_SCL_") || child2.name.StartsWith("___"))
					{
						vector = new Vector3?(child3.position);
					}
					else if (child3.name.EndsWith("_SCL_") || child3.name.StartsWith("___"))
					{
						vector = new Vector3?(child2.position);
					}
				}
				if (vector == null)
				{
					float num = 0.1f;
					if (!isRoot)
					{
						foreach (object obj in bone)
						{
							Transform transform = (Transform)obj;
							if (!transform.name.StartsWith("___"))
							{
								float magnitude = (transform.position - bone.position).magnitude;
								if (magnitude > num)
								{
									num = magnitude;
								}
							}
						}
					}
					vector = new Vector3?(bone.position + bone.rotation * new Vector3(-num, 0f, 0f));
					List<LineRenderer> list;
					if (!this._offsetlineDict.TryGetValue(bone.name, out list))
					{
						list = new List<LineRenderer>();
						this._offsetlineDict[bone.name] = list;
						foreach (object obj2 in bone)
						{
							Transform transform2 = (Transform)obj2;
							if (!transform2.name.StartsWith("___"))
							{
								LineRenderer lineRenderer2 = this.CreateComponent(this._sublineMaterial, this._lineWidth * 0.1f, this._lineWidth * 0.1f);
								list.Add(lineRenderer2);
								lineRenderer2.gameObject.name = "offsetLine";
								lineRenderer2.gameObject.SetActive(this._isVisible);
							}
						}
					}
					int num2 = 0;
					foreach (object obj3 in bone)
					{
						Transform transform3 = (Transform)obj3;
						if (!transform3.name.StartsWith("___"))
						{
							LineRenderer lineRenderer3 = list[num2++];
							lineRenderer3.SetPosition(0, vector.Value);
							lineRenderer3.SetPosition(1, transform3.position);
						}
					}
				}
			}
			lineRenderer.SetPosition(1, vector.Value);
			if (!isRecursive)
			{
				return;
			}
			foreach (object obj4 in bone)
			{
				Transform bone2 = (Transform)obj4;
				this.UpdatePosition(bone2, false, true);
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00022D4C File Offset: 0x00020F4C
		public void Clear()
		{
			this._lineDict.Clear();
			this._offsetlineDict.Clear();
			this.ClearCache();
			this._rootBone = null;
			this.TargetId = -1;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00022D78 File Offset: 0x00020F78
		private void ClearCache()
		{
			foreach (GameObject obj in this._cache)
			{
				UnityEngine.Object.Destroy(obj);
			}
			this._cache.Clear();
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00022DD8 File Offset: 0x00020FD8
		private void SetColor(ref Color color1)
		{
			foreach (string key in this._boneNames)
			{
				LineRenderer lineRenderer;
				if (this._lineDict.TryGetValue(key, out lineRenderer))
				{
					lineRenderer.material.color = color1;
				}
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00022E48 File Offset: 0x00021048
		public void SetRQ(int rq)
		{
			foreach (GameObject gameObject in this._cache)
			{
				LineRenderer component = gameObject.GetComponent<LineRenderer>();
				component.material.renderQueue = rq;
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00022EA8 File Offset: 0x000210A8
		private LineRenderer CreateComponent(Material material, float startWidth, float endWidth)
		{
			GameObject gameObject = new GameObject();
			this._cache.Add(gameObject);
			LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
			lineRenderer.materials = new Material[]
			{
				material
			};
			lineRenderer.startWidth = startWidth;
			lineRenderer.endWidth = endWidth;
			lineRenderer.positionCount = 2;
			return lineRenderer;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00022EF8 File Offset: 0x000210F8
		private Material CreateMaterial()
		{
			Shader shader = Shader.Find("Hidden/Internal-Colored");
			Material material = new Material(shader)
			{
				hideFlags = HideFlags.HideAndDontSave
			};
			material.SetInt("_ZTest", 0);
			material.SetInt("_SrcBlend", 5);
			material.SetInt("_DstBlend", 10);
			material.SetInt("_Cull", 0);
			material.SetInt("_ZWrite", 0);
			material.renderQueue = 5000;
			return material;
		}

		// Token: 0x0400048D RID: 1165
		private const string NAME_LINE_PREFIX = "___LINE_";

		// Token: 0x0400048E RID: 1166
		private const string NAME_SCL = "_SCL_";

		// Token: 0x0400048F RID: 1167
		private readonly Vector3 UNIT_VECTOR3 = new Vector3(-0.1f, 0f, 0f);

		// Token: 0x04000490 RID: 1168
		private readonly Dictionary<string, LineRenderer> _lineDict = new Dictionary<string, LineRenderer>();

		// Token: 0x04000491 RID: 1169
		private readonly Dictionary<string, List<LineRenderer>> _offsetlineDict = new Dictionary<string, List<LineRenderer>>();

		// Token: 0x04000492 RID: 1170
		private readonly List<GameObject> _cache = new List<GameObject>();

		// Token: 0x04000493 RID: 1171
		private Material _lineMaterial;

		// Token: 0x04000494 RID: 1172
		private Material _sublineMaterial;

		// Token: 0x04000495 RID: 1173
		private readonly float _lineWidth = 0.006f;

		// Token: 0x04000496 RID: 1174
		private Color _color = Color.white;

		// Token: 0x04000497 RID: 1175
		private Color _offColor = new Color(0.6f, 0.6f, 0.6f);

		// Token: 0x04000498 RID: 1176
		private SkinnedMeshRenderer _meshRenderer;

		// Token: 0x04000499 RID: 1177
		private Transform _rootBone;

		// Token: 0x0400049A RID: 1178
		private readonly HashSet<string> _boneNames = new HashSet<string>();

		// Token: 0x0400049B RID: 1179
		private bool _isVisible;

		// Token: 0x0400049C RID: 1180
		private bool _skipVisible;
	}
}
