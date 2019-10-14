using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace xBot.App
{
	partial class Window
	{

		#region (GUI theme design)
		/// <summary>
		/// Set the control to be used as window drag from the current window.
		/// </summary>
		private void Window_Drag_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				WinAPI.ReleaseCapture();
				WinAPI.SendMessage(Handle, WinAPI.WM_NCLBUTTONDOWN, WinAPI.HT_CAPTION, 0);
			}
		}
		/// <summary>
		/// Colors used on TabPage Vertical.
		/// </summary>
		private Color TabPageV_ColorHover = Color.FromArgb(74, 74, 76),
			TabPageV_ColorSelected = Color.FromArgb(0, 122, 204);
		/// <summary>
		///  TabPage Vertical option click.
		/// </summary>
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
				c.Parent.Parent.Controls[currentOption[0].Name + "_Panel"].Visible = false;
			}
			currentOption = new List<Control>();
			currentOption.Add(c.Parent.Controls[c.Name.Replace("_Icon", "")]);
			currentOption.Add(c.Parent.Controls[c.Name.Contains("_Icon") ? c.Name : c.Name + "_Icon"]);
			c.Parent.Tag = currentOption;
			currentOption[0].BackColor = TabPageV_ColorSelected;
			currentOption[1].BackColor = TabPageV_ColorSelected;
			c.Parent.Parent.Controls[currentOption[1].Name.Replace("Icon", "Panel")].Visible = true;
		}
		/// <summary>
		/// Creates a custom color focus on icon and option used on TabPage Vertical.
		/// <para>Results are not truly as expected because Windows native focus...</para>
		/// </summary>
		private void TabPageV_Option_MouseEnter(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			if (c.Parent.Tag != null)
			{
				List<Control> currentOption = (List<Control>)c.Parent.Tag;
				if (c.Name != currentOption[0].Name && c.Name != currentOption[1].Name)
				{
					c.Parent.Controls[c.Name.Replace("_Icon", "")].BackColor = TabPageV_ColorHover;
					c.Parent.Controls[c.Name.Contains("_Icon") ? c.Name : c.Name + "_Icon"].BackColor = TabPageV_ColorHover;
				}
				else
				{
					c.Parent.Controls[c.Name.Replace("_Icon", "")].BackColor = TabPageV_ColorSelected;
				}
			}
		}
		/// <summary>
		/// Restore the changes made on <see cref="TabPageV_Option_MouseEnter(object, EventArgs)"/>
		/// </summary>
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
		/// <summary>
		///  TabPage Horizontal option click.
		/// </summary>
		private void TabPageH_Option_Click(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			if (c.Parent.Tag != null)
			{
				Control currentOption = (Control)c.Parent.Tag;
				if (currentOption.Name == c.Name)
					return;
				currentOption.BackColor = c.Parent.Parent.BackColor;
				c.Parent.Parent.Controls[currentOption.Name + "_Panel"].Visible = false;
			}
			c.Parent.Tag = c;
			c.BackColor = c.Parent.BackColor;
			c.Parent.Parent.Controls[c.Name + "_Panel"].Visible = true;
		}
		/// <summary>
		/// Color the label associated (by name) to the current control focused.
		/// </summary>
		private void Control_FocusEnter(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			string[] controlTypes = new string[] { "cbx", "cmbx", "rtbx", "tbx", "lstv", "btn" };
			foreach (string t in controlTypes)
			{
				if (c.Name.Contains(t))
				{
					if (c.Parent.Controls.ContainsKey(c.Name.Replace(t, "lbl")))
					{
						c.Parent.Controls[c.Name.Replace(t, "lbl")].BackColor = Color.FromArgb(30, 150, 220);
					}
					break;
				}
			}
		}
		/// <summary>
		/// Restore the changes made on <see cref="Control_FocusEnter(object, EventArgs)"/>
		/// </summary>
		private void Control_FocusLeave(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			string[] controlTypes = new string[] { "cbx", "cmbx", "rtbx", "tbx", "lstv", "btn" };
			foreach (string t in controlTypes)
			{
				if (c.Name.Contains(t))
				{
					if (c.Parent.Controls.ContainsKey(c.Name.Replace(t, "lbl")))
					{
						c.Parent.Controls[c.Name.Replace(t, "lbl")].BackColor = c.Parent.BackColor;
					}
					break;
				}
			}
		}
		/// <summary>
		/// Forces the listview header to keep his width.
		/// </summary>
		private void ListView_ColumnWidthChanging_Cancel(object sender, ColumnWidthChangingEventArgs e)
		{
			e.Cancel = true;
			e.NewWidth = ((ListView)sender).Columns[e.ColumnIndex].Width;
		}
		public void EnableControl(Control c, bool active)
		{
			WinAPI.InvokeIfRequired(c, () => {
				c.Font = new Font(c.Font, (active ? FontStyle.Regular : FontStyle.Strikeout));
			});
		}
		#endregion
	}
}
