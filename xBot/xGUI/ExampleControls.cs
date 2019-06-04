using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xGUI
{
	public partial class ExampleControls : Form
	{
		public ExampleControls()
		{
			InitializeComponent();
		}

		// TabPage Vertical
		Color TabPageV_ColorHover = Color.Red, TabPageV_ColorSelected = Color.Blue;
		private void TabPageV_Option_Click(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			List<Control> currentOption;
			if (c.Parent.Tag != null)
			{
				currentOption = (List<Control>)c.Parent.Tag;
				if (currentOption[0].Name == c.Name || currentOption[1].Name == c.Name)
					return;
				currentOption[0].BackColor = c.Parent.BackColor;
				currentOption[1].BackColor = c.Parent.BackColor;
				c.Parent.Parent.Controls[currentOption[0].Name+"_Panel"].Visible = false;
			}
			currentOption = new List<Control>();
			currentOption.Add(c.Parent.Controls[c.Name.Replace("_Icon", "")]);
			currentOption.Add(c.Parent.Controls[c.Name.Contains("_Icon") ? c.Name : c.Name+"_Icon"]);
			c.Parent.Tag = currentOption;
			currentOption[0].BackColor = TabPageV_ColorSelected;
			currentOption[1].BackColor = TabPageV_ColorSelected;
			c.Parent.Parent.Controls[currentOption[1].Name.Replace("Icon", "Panel")].Visible = true;
		}

		private void TabPageV_Option_MouseEnter(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			if (c.Parent.Tag != null)
			{
				List<Control> currentOption = (List<Control>)c.Parent.Tag;
				if (c.Name != currentOption[0].Name && c.Name != currentOption[1].Name)
				{
					c.Parent.Controls[c.Name.Replace("_Icon", "")].BackColor = TabPageV_ColorHover;
					c.Parent.Controls[c.Name.Contains("_Icon") ? c.Name : c.Name+"_Icon"].BackColor = TabPageV_ColorHover;
				}
				else
				{
					c.Parent.Controls[c.Name.Replace("_Icon", "")].BackColor = TabPageV_ColorSelected;
				}
			}
		}
		private void TabPageV_Option_MouseLeave(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			if (c.Parent.Tag != null)
			{
				List<Control> currentOption = (List<Control>)c.Parent.Tag;
				if (c.Name != currentOption[0].Name && c.Name != currentOption[1].Name)
				{
					c.Parent.Controls[c.Name.Replace("_Icon", "")].BackColor = c.Parent.BackColor;
					c.Parent.Controls[c.Name.Contains("_Icon") ? c.Name : c.Name + "_Icon"].BackColor = c.Parent.BackColor;
				}
				else
				{
					c.Parent.Controls[c.Name.Replace("_Icon", "")].BackColor = TabPageV_ColorSelected;
				}
			}
		}
		// TabPage Horizontal
		private void TabPageH_Option_Click(object sender, EventArgs e)
		{
			Button b = (Button)sender;
			if (b.Parent.Tag != null)
			{
				Button currentOption = (Button)b.Parent.Tag;
				if (currentOption.Name == b.Name)
					return;
				currentOption.BackColor = b.Parent.Parent.BackColor;
				b.Parent.Parent.Controls[currentOption.Name + "_Panel"].Visible = false;
			}
			b.Parent.Tag = b;
			b.BackColor = b.Parent.BackColor;
			b.Tag = b.FlatAppearance.MouseOverBackColor;
      b.FlatAppearance.MouseOverBackColor = b.Parent.BackColor;
			b.Parent.Parent.Controls[b.Name + "_Panel"].Visible = true;
		}
		private void TabPageH_Option_MouseLeave(object sender, EventArgs e)
		{
			Button b = (Button)sender;
			if (b.Parent.Tag != null)
			{
				Button currentOption = (Button)b.Parent.Tag;
				if (b.Name == currentOption.Name)
				{
					b.FlatAppearance.MouseOverBackColor = (Color)b.Tag;
        }
			}
		}
		// Focus design
		private void Control_FocusEnter(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			string[] controlTags = new string[] { "tbx", "cbx", "cmbx" };
			foreach (string tag in controlTags)
				if (c.Name.Contains(tag))
					c.Parent.Controls[c.Name.Replace(tag, "lbl")].BackColor = Color.FromArgb(51, 153, 255);
		}
		private void Control_FocusLeave(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			string[] controlTags = new string[] { "tbx", "cbx", "cmbx" };
			foreach (string tag in controlTags)
				if (c.Name.Contains(tag))
					c.Parent.Controls[c.Name.Replace(tag, "lbl")].BackColor = c.Parent.BackColor;
		}
		
		private void ExampleControls_Load(object sender, EventArgs e)
		{

		}
	}
}