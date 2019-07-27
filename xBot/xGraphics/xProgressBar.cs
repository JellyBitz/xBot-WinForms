using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace xGraphics
{
	public enum xProgressBarDisplay
	{
		Percentage,
		Text,
		Values
	}
	public class xProgressBar : Control
	{
		/// <summary>
		/// Current value.
		/// </summary>
		public ulong Value { get; set; }
		/// <summary>
		/// Maximum possible value.
		/// </summary>
		public ulong ValueMaximum { get; set; }
		/// <summary>
		/// Degradation color steps to get lighter.
		/// </summary>
		public int ColorDegradation { get; set; }
		/// <summary>
		/// Property to set to decide whether to print a % or Text.
		/// </summary>
		public xProgressBarDisplay Display { get; set; }
		/// <summary>
		/// DisplayStyle text.
		/// </summary>
		public string DisplayText { get; set; }
		/// 	/// <summary>
		/// Shadow color of the DisplayStyle.
		/// </summary>
		public Color DisplayShadow { get; set; }
		/// <summary>
		/// DisplayStyle Font.
		/// </summary>
		public Font DisplayFont { get; set; }
		private SolidBrush TextColorBrush { get; }
		private Pen FillColor { get; }
		private SolidBrush TextShadowBrush { get; }
		/// <summary>
		/// Gets the value at percentage mode.
		/// </summary>
		public double ValuePercentage
		{
			get { return (Value * 100d / ValueMaximum); }
		}
		public xProgressBar()
		{
			// http://msdn.microsoft.com/en-us/library/system.windows.forms.controlstyles.aspx
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			TextColorBrush = new SolidBrush(Color.White);
			TextShadowBrush = new SolidBrush(Color.White);
			FillColor = new Pen(Color.MidnightBlue);
			DisplayShadow = ForeColor;
			DisplayFont = new Font(FontFamily.GenericSerif, 12);
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			if (TextColorBrush.Color != ForeColor)
				TextColorBrush.Color = ForeColor;
			if (TextShadowBrush.Color != DisplayShadow)
				TextShadowBrush.Color = DisplayShadow;
			if (FillColor.Color != BackColor)
				FillColor.Color = BackColor;

			Rectangle rect = ClientRectangle;
			Graphics g = e.Graphics;

			ProgressBarRenderer.DrawHorizontalBar(g, rect);
			rect.Inflate(-2, -2);
			if (Value > 0)
			{
				int x0 = rect.X;
				int xf = (int)(rect.Width * ValuePercentage/100);
				int _r = FillColor.Color.R, _g = FillColor.Color.G, _b = FillColor.Color.B;
				for (int y = rect.Y; y <= rect.Height; y++)
				{
					Pen p = new Pen(Color.FromArgb(_r, _g, _b));
					g.DrawLine(p, x0, y, xf, y);
					if (_r + ColorDegradation < 256)
						_r += ColorDegradation;
					if (_g + ColorDegradation < 256)
						_g += ColorDegradation;
					if (_b + ColorDegradation < 256)
						_b += ColorDegradation;
				}
			}

			string text = GetDisplayText();

			SizeF len = g.MeasureString(text, DisplayFont);
			int px = Convert.ToInt32((Width / 2) - len.Width / 2);
			int py = Convert.ToInt32((Height / 2) - len.Height / 2);

			if (TextShadowBrush.Color != TextColorBrush.Color)
			{
				for (sbyte i = -1; i < 2; i++)
					for (sbyte j = -1; j < 2; j++)
						g.DrawString(text, DisplayFont, TextShadowBrush, px + i, py + j);
			}
			g.DrawString(text, DisplayFont, TextColorBrush, px, py);
		}
		private string GetDisplayText()
		{
			StringBuilder text = new StringBuilder();
			switch (Display)
			{
				case xProgressBarDisplay.Percentage:
					if (ValueMaximum > 0)
						text.Append(Math.Round(ValuePercentage, 2));
					else
						text.Append("0");
					text.Append("%");
					break;
				case xProgressBarDisplay.Values:
					text.Append(Value);
					text.Append(" / ");
					text.Append(ValueMaximum);
					break;
				case xProgressBarDisplay.Text:
				default:
					return DisplayText;
			}
			return text.ToString();
		}
	}
}
