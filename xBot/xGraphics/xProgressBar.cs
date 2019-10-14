using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace xGraphics
{
	public enum xProgressBarDisplay : byte
	{
		Percentage,
		Values,
		Text
	}
	public class xProgressBar : Control
	{
		private ulong _Value;
		private ulong _ValueMaximum;

		public ulong Value
		{
			get
			{
				return _Value;
			}
			set
			{
				_Value = value;
				Invalidate();
			}
		}

		public ulong ValueMaximum
		{
			get
			{
				return _ValueMaximum;
			}
			set
			{
				_ValueMaximum = value;
				Invalidate();
			}
		}
		public int ColorDegradation {	get; set; }
		public xProgressBarDisplay Display { get; set; }
		public string DisplayText { get; set; }
		public Color DisplayShadow { get; set; }
		public Font DisplayFont { get; set; }
		private SolidBrush TextColorBrush { get; }
		private Pen FillColor { get; }
		private SolidBrush TextShadowBrush { get; }
		public double ValuePercentage { get { return (double)Value * 100.0 / (double)ValueMaximum; } } 
		public xProgressBar()
		{
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
			TextColorBrush = new SolidBrush(Color.White);
			TextShadowBrush = new SolidBrush(Color.White);
			FillColor = new Pen(Color.MidnightBlue);
			DisplayShadow = ForeColor;
			DisplayFont = new Font(FontFamily.GenericSerif, 12f);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (TextColorBrush.Color != ForeColor)
			{
				TextColorBrush.Color = ForeColor;
			}
			if (TextShadowBrush.Color != DisplayShadow)
			{
				TextShadowBrush.Color = DisplayShadow;
			}
			if (FillColor.Color != BackColor)
			{
				FillColor.Color = BackColor;
			}

			Rectangle rect = base.ClientRectangle;
			Graphics g = e.Graphics;
			ProgressBarRenderer.DrawHorizontalBar(g, rect);

			rect.Inflate(-2, -2);
			if (Value != 0)
			{
				int x0 = rect.X;
				int xf = (int)((double)rect.Width * ValuePercentage / 100.0);
				int _r = FillColor.Color.R;
				int _g = FillColor.Color.G;
				int _b = FillColor.Color.B;
				int num;
				for (int y = rect.Y; y <= rect.Height; y = num + 1)
				{
					Pen p = new Pen(Color.FromArgb(_r, _g, _b));
					g.DrawLine(p, x0, y, xf, y);
					if (_r + ColorDegradation < 256)
					{
						_r += ColorDegradation;
					}
					if (_g + ColorDegradation < 256)
					{
						_g += ColorDegradation;
					}
					if (_b + ColorDegradation < 256)
					{
						_b += ColorDegradation;
					}
					num = y;
				}
			}
			string text = GetDisplayText();
			SizeF len = g.MeasureString(text, DisplayFont);
			int px = Convert.ToInt32((float)(base.Width / 2) - len.Width / 2f);
			int py = Convert.ToInt32((float)(base.Height / 2) - len.Height / 2f);
			if (TextShadowBrush.Color != TextColorBrush.Color)
			{
				sbyte b;
				for (sbyte j = -1; j < 2; j = (sbyte)(b + 1))
				{
					for (sbyte i = -1; i < 2; i = (sbyte)(b + 1))
					{
						g.DrawString(text, DisplayFont, TextShadowBrush, px + j, py + i);
						b = i;
					}
					b = j;
				}
			}
			g.DrawString(text, DisplayFont, TextColorBrush, px, py);
		}

		private string GetDisplayText()
		{
			StringBuilder text = new StringBuilder();
			switch (Display)
			{
				case xProgressBarDisplay.Percentage:
					if (ValueMaximum != 0)
					{
						text.Append(Math.Round(ValuePercentage, 2));
					}
					else
					{
						text.Append("0");
					}
					text.Append("%");
					break;
				case xProgressBarDisplay.Values:
					text.Append(Value);
					text.Append(" / ");
					text.Append(ValueMaximum);
					break;
				default:
					return DisplayText;
			}
			return text.ToString();
		}
	}

}
