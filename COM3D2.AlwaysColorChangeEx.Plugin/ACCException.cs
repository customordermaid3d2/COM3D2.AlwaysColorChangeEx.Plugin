using System;
using System.Runtime.Serialization;

namespace CM3D2.AlwaysColorChangeEx.Plugin
{
	// Token: 0x02000002 RID: 2
	public class ACCException : Exception
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public ACCException()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public ACCException(string message) : base(message)
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		public ACCException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000206B File Offset: 0x0000026B
		protected ACCException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
