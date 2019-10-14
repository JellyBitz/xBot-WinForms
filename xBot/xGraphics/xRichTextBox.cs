using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace xGraphics
{
	public class xRichTextBox : RichTextBox
	{
		[DllImport("user32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, ref Point lParam);
		[DllImport("user32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, IntPtr lParam);
		[DllImport("user32")]
		private static extern int GetCaretPos(out Point p);
		private const int WM_USER = 1024;
		private const int WM_SETREDRAW = 11;
		private const int EM_GETEVENTMASK = 1083;
		private const int EM_SETEVENTMASK = 1093;
		private const int EM_GETSCROLLPOS = 1245;
		private const int EM_SETSCROLLPOS = 1246;
		private const int WM_VSCROLL = 277;
		private const int SB_PAGEBOTTOM = 7;

		private Point _ScrollPoint;
		private bool _Painting = true;
		private IntPtr _EventMask;
		private int _SuspendIndex = 0;
		private int _SuspendLength = 0;
		private bool _ReadOnly;

		public bool AutoScroll
		{
			get{
				return _AutoScroll;
			}
			set{
				if (value)
					this.TextChanged += xRichTextBox_TextChanged_AutoScroll;
				else
					this.TextChanged -= xRichTextBox_TextChanged_AutoScroll;
				_AutoScroll = value;
			}
		}
		private bool _AutoScroll;
		public int MaxLines {	get; set;	}
		public xRichTextBox()
		{
			MaxLines = int.MaxValue;
		}
		public void SuspendPainting()
		{
			if (_Painting)
			{
				_SuspendIndex = this.SelectionStart;
				_SuspendLength = SelectionLength;
				SendMessage(this.Handle, 1245, 0, ref _ScrollPoint);
				SendMessage(this.Handle, 11, 0, IntPtr.Zero);
				_EventMask = SendMessage(this.Handle, 1083, 0, IntPtr.Zero);
				_Painting = false;
			}
		}
		public void ResumePainting()
		{
			if (!_Painting)
			{
				Select(_SuspendIndex, _SuspendLength);
				SendMessage(this.Handle, 1246, 0, ref _ScrollPoint);
				SendMessage(this.Handle, 1093, 0, _EventMask);
				SendMessage(this.Handle, 11, 1, IntPtr.Zero);
				_Painting = true;
				Invalidate();
			}
		}

		public new void AppendText(string text)
		{
			if (base.Lines.Length >= MaxLines)
			{
				SuspendPainting();
				Select(0, GetFirstCharIndexFromLine(1));
				_ReadOnly = base.ReadOnly;
				this.ReadOnly = false;
				SelectedText = "";
				this.ReadOnly = _ReadOnly;
				ResumePainting();
			}
			if (AutoScroll)
			{
				base.AppendText(text);
			}
			else
			{
				SuspendPainting();
				base.AppendText(text);
				ResumePainting();
			}
		}

		private void xRichTextBox_TextChanged_AutoScroll(object sender, EventArgs e)
		{
			//SendMessage(base.Handle, WM_VSCROLL, SB_PAGEBOTTOM, IntPtr.Zero);
			base.SelectionStart = Text.Length;
			ScrollToCaret();
		}
	}
}
