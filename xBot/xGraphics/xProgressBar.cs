using System;
using System.Drawing;
using System.Windows.Forms;

namespace xGraphics
{
	public enum ProgressBarDisplayText
	{
		Percentage,
		Text,
		Values
	}
	public class xProgressBar : ProgressBar
	{
		/// <summary>
		/// Property to set to decide whether to print a % or Text.
		/// </summary>
		public ProgressBarDisplayText DisplayStyle { get; set; }

		/// <summary>
		/// DisplayStyle text.
		/// </summary>
		public string DisplayText { get; set; }
		/// <summary>
		/// DisplayStyle Font.
		/// </summary>
		public Font TextFont { get; set; }
		private SolidBrush TextColorBrush { get; }
		private Pen FillColor { get; }
		/// <summary>
		/// Shadow color of the DisplayStyle.
		/// </summary>
		public Color TextShadow { get; set; }
		private SolidBrush TextShadowBrush { get; }
		/// <summary>
		/// Gets the value at percentage mode.
		/// </summary>
		public float ValuePercentage
		{
			get { return (Value * 100f / Maximum); }
		}
		public xProgressBar()
		{
			// http://msdn.microsoft.com/en-us/library/system.windows.forms.controlstyles.aspx
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			TextColorBrush = new SolidBrush(Color.White);
			TextShadowBrush = new SolidBrush(Color.White);
			FillColor = new Pen(Color.MidnightBlue);
			TextShadow = ForeColor;
			TextFont = new Font(FontFamily.GenericSerif,12);
    }
		protected override void OnPaint(PaintEventArgs e)
		{
			if (TextColorBrush.Color != ForeColor)
				TextColorBrush.Color = ForeColor;
			if (TextShadowBrush.Color != TextShadow)
				TextShadowBrush.Color = TextShadow;
			if (FillColor.Color != BackColor)
				FillColor.Color = BackColor;
			
			Rectangle rect = ClientRectangle;
			Graphics g = e.Graphics;

			ProgressBarRenderer.DrawHorizontalBar(g, rect);
			rect.Inflate(-2, -2);
			if (Value > 0)
			{
				int x0 = rect.X;
				int xf = (int)(Value * rect.Size.Width/Maximum);
				int _r = FillColor.Color.R, _g = FillColor.Color.G, _b = FillColor.Color.B;
				for (int y = rect.Y; y <= rect.Height; y++)
				{
					Pen p = new Pen(Color.FromArgb(_r,_g,_b));
					g.DrawLine(p, x0, y, xf, y);
					if (_r + Step < 256)
						_r+= Step;
					if (_g + Step < 256)
						_g+= Step;
					if (_b + Step < 256)
						_b+= Step;
				}
      }

			string text = DisplayStyle == ProgressBarDisplayText.Percentage ?(Math.Round(ValuePercentage,2)+"%"):(DisplayStyle == ProgressBarDisplayText.Values?this.Value+" / "+this.Maximum:DisplayText);

			SizeF len = g.MeasureString(text, TextFont);
			int px = Convert.ToInt32((Width / 2) - len.Width / 2);
			int py = Convert.ToInt32((Height / 2) - len.Height / 2);

			if (TextShadowBrush.Color != TextColorBrush.Color)
			{
				g.DrawString(text, TextFont, TextShadowBrush, px + 1, py + 1);
				g.DrawString(text, TextFont, TextShadowBrush, px - 1, py - 1);
				g.DrawString(text, TextFont, TextShadowBrush, px - 1, py);
				g.DrawString(text, TextFont, TextShadowBrush, px, py + 1);
				g.DrawString(text, TextFont, TextShadowBrush, px - 1, py + 1);
				g.DrawString(text, TextFont, TextShadowBrush, px + 1, py - 1);
			}
			g.DrawString(text, TextFont, TextColorBrush, px, py);
		}
	}
}
