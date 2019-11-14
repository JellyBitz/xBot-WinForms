using System;
using System.ComponentModel;
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
		[Category("Appearance")]
		public xProgressBarDisplay Display { get; set; }
		[Category("Appearance")]
		public Color DisplayShadow { get; set; }
		[Category("Appearance")]
		public int BackColorDegradationLevel { get; set; }

		[Category("Behavior")]
		public ulong Value
		{
			get	{	return _Value; }
			set	{	_Value = value;	Invalidate();	}
		}
		private ulong _Value;
		[Category("Behavior")]
		public ulong ValueMaximum
		{
			get {	return _ValueMaximum; }
			set	{	_ValueMaximum = value; Invalidate(); }
		}
		private ulong _ValueMaximum;
		private SolidBrush TextColorBrush { get; }
		private Pen FillColor { get; }
		private SolidBrush TextShadowBrush { get; }
		public double ValuePercentage { get { return Value * 100.0 / ValueMaximum; } } 
		public xProgressBar() : base()
		{
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
			TextColorBrush = new SolidBrush(Color.White);
			TextShadowBrush = new SolidBrush(Color.White);
			FillColor = new Pen(Color.MidnightBlue);
			DisplayShadow = Color.Black;
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
				int xf = (int)(rect.Width * ValuePercentage / 100.0);
				int _r = FillColor.Color.R;
				int _g = FillColor.Color.G;
				int _b = FillColor.Color.B;
				int num;
				for (int y = rect.Y; y <= rect.Height; y = num + 1)
				{
					Pen p = new Pen(Color.FromArgb(_r, _g, _b));
					g.DrawLine(p, x0, y, xf, y);
					if (_r + BackColorDegradationLevel < 256)
					{
						_r += BackColorDegradationLevel;
					}
					if (_g + BackColorDegradationLevel < 256)
					{
						_g += BackColorDegradationLevel;
					}
					if (_b + BackColorDegradationLevel < 256)
					{
						_b += BackColorDegradationLevel;
					}
					num = y;
				}
			}
			string text = GetDisplayText();
			SizeF len = g.MeasureString(text, Font);
			int px = Convert.ToInt32((base.Width / 2) - len.Width / 2f);
			int py = Convert.ToInt32((base.Height / 2) - len.Height / 2f);
			if (TextShadowBrush.Color != TextColorBrush.Color)
			{
				sbyte b;
				for (sbyte j = -1; j < 2; j = (sbyte)(b + 1))
				{
					for (sbyte i = -1; i < 2; i = (sbyte)(b + 1))
					{
						g.DrawString(text, Font, TextShadowBrush, px + j, py + i);
						b = i;
					}
					b = j;
				}
			}
			g.DrawString(text, Font, TextColorBrush, px, py);
		}
		private string GetDisplayText()
		{
			switch (Display)
			{
				case xProgressBarDisplay.Percentage:
					{
						StringBuilder text = new StringBuilder();
						if (ValueMaximum == 0)
							text.Append("0");
						else
							text.Append(Math.Round(ValuePercentage, 2));
						text.Append("%");
						return text.ToString();
					}
				case xProgressBarDisplay.Values:
					{
						StringBuilder text = new StringBuilder();
						text.Append(Value);
						text.Append(" / ");
						text.Append(ValueMaximum);
						return text.ToString();
					}
				default:
					return Text;
			}
		}
	}

}
