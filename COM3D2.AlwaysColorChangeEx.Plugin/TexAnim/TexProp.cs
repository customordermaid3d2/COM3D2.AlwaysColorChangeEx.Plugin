using System;
using UnityEngine;

namespace CM3D2.AlwaysColorChangeEx.Plugin.TexAnim
{
	// Token: 0x0200007B RID: 123
	public class TexProp
	{
		// Token: 0x0600040B RID: 1035 RVA: 0x00023844 File Offset: 0x00021A44
		public TexProp(string prop, string subPrefix)
		{
			this.Prop = prop;
			this.PropId = Shader.PropertyToID(prop);
			this.PropFPSId = Shader.PropertyToID(prop + "FPS");
			this.PropScrollXId = Shader.PropertyToID(subPrefix + "ScrollX");
			this.PropScrollYId = Shader.PropertyToID(subPrefix + "ScrollY");
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x000238AC File Offset: 0x00021AAC
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x000238B4 File Offset: 0x00021AB4
		public string Prop { get; private set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x000238BD File Offset: 0x00021ABD
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x000238C5 File Offset: 0x00021AC5
		public int PropId { get; private set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x000238CE File Offset: 0x00021ACE
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x000238D6 File Offset: 0x00021AD6
		public int PropFPSId { get; private set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x000238DF File Offset: 0x00021ADF
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x000238E7 File Offset: 0x00021AE7
		public int PropScrollXId { get; private set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x000238F0 File Offset: 0x00021AF0
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x000238F8 File Offset: 0x00021AF8
		public int PropScrollYId { get; private set; }

		// Token: 0x06000416 RID: 1046 RVA: 0x00023901 File Offset: 0x00021B01
		public override string ToString()
		{
			return this.Prop;
		}

		// Token: 0x040004B4 RID: 1204
		public static readonly TexProp MainTex = new TexProp("_MainTex", "_MainAnime");

		// Token: 0x040004B5 RID: 1205
		public static readonly TexProp ShadowTex = new TexProp("_ShadowTex", "_ShadowAnime");
	}
}
