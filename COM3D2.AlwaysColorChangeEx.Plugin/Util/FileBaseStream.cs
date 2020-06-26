using System;
using System.IO;

namespace CM3D2.AlwaysColorChangeEx.Plugin.Util
{
	// Token: 0x0200004B RID: 75
	public class FileBaseStream : Stream
	{
		// Token: 0x06000237 RID: 567 RVA: 0x00014590 File Offset: 0x00012790
		public FileBaseStream(AFileBase file)
		{
			this.filebase = file;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0001459F File Offset: 0x0001279F
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000239 RID: 569 RVA: 0x000145A2 File Offset: 0x000127A2
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600023A RID: 570 RVA: 0x000145A5 File Offset: 0x000127A5
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600023B RID: 571 RVA: 0x000145A8 File Offset: 0x000127A8
		public override long Length
		{
			get
			{
				return (long)this.filebase.GetSize();
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600023C RID: 572 RVA: 0x000145B6 File Offset: 0x000127B6
		// (set) Token: 0x0600023D RID: 573 RVA: 0x000145C4 File Offset: 0x000127C4
		public override long Position
		{
			get
			{
				return (long)this.filebase.Tell();
			}
			set
			{
				this.filebase.Seek((int)value, true);
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000145D5 File Offset: 0x000127D5
		public override void Close()
		{
			this.filebase.Dispose();
			base.Close();
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000145E8 File Offset: 0x000127E8
		public override long Seek(long offset, SeekOrigin origin)
		{
			switch (origin)
			{
			case SeekOrigin.Begin:
				return (long)this.filebase.Seek((int)offset, true);
			case SeekOrigin.Current:
				return (long)this.filebase.Seek((int)offset, false);
			default:
				throw new NotSupportedException("unsuported");
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00014634 File Offset: 0x00012834
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num;
			if (offset == 0 && buffer.Length > count)
			{
				num = this.filebase.Read(ref buffer, count);
			}
			else
			{
				int num2 = buffer.Length - offset;
				if (num2 < count)
				{
					count = num2;
				}
				byte[] sourceArray = new byte[count];
				num = this.filebase.Read(ref sourceArray, count);
				if (num > 0)
				{
					Array.Copy(sourceArray, 0, buffer, offset, num);
				}
			}
			return num;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00014690 File Offset: 0x00012890
		public override int ReadByte()
		{
			byte[] array = new byte[1];
			if (this.Read(array, 0, 1) != 0)
			{
				return (int)array[0];
			}
			return -1;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000146B4 File Offset: 0x000128B4
		public override void Flush()
		{
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000146B6 File Offset: 0x000128B6
		public override void SetLength(long value)
		{
			throw new NotSupportedException("unsupported");
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000146C2 File Offset: 0x000128C2
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("unsupported");
		}

		// Token: 0x04000322 RID: 802
		private AFileBase filebase;
	}
}
