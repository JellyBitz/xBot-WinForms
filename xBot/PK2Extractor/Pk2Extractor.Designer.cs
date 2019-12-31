namespace xBot.PK2Extractor
{
	partial class Pk2Extractor
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.btnWinExit = new System.Windows.Forms.Button();
			this.rtbxLogs = new System.Windows.Forms.RichTextBox();
			this.pnlLogs = new System.Windows.Forms.Panel();
			this.panelWindow = new System.Windows.Forms.Panel();
			this.lblHeaderIcon = new System.Windows.Forms.Label();
			this.btnStart = new System.Windows.Forms.Button();
			this.tbxBlowfishKey = new System.Windows.Forms.TextBox();
			this.lblBlowfishKey = new System.Windows.Forms.Label();
			this.lblHeader = new System.Windows.Forms.Label();
			this.ToolTips = new System.Windows.Forms.ToolTip(this.components);
			this.lblProcessState = new System.Windows.Forms.Label();
			this.cbxMinimap = new System.Windows.Forms.CheckBox();
			this.pnlLogs.SuspendLayout();
			this.panelWindow.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnWinExit
			// 
			this.btnWinExit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnWinExit.FlatAppearance.BorderSize = 0;
			this.btnWinExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.btnWinExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.btnWinExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnWinExit.Font = new System.Drawing.Font("Font Awesome 5 Pro Regular", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.btnWinExit.Location = new System.Drawing.Point(363, 9);
			this.btnWinExit.Margin = new System.Windows.Forms.Padding(0);
			this.btnWinExit.Name = "btnWinExit";
			this.btnWinExit.Size = new System.Drawing.Size(24, 24);
			this.btnWinExit.TabIndex = 4;
			this.btnWinExit.TabStop = false;
			this.btnWinExit.Tag = "Font Awesome 5 Pro Regular";
			this.btnWinExit.Text = "";
			this.btnWinExit.UseCompatibleTextRendering = true;
			this.btnWinExit.UseVisualStyleBackColor = false;
			this.btnWinExit.Click += new System.EventHandler(this.Control_Click);
			// 
			// rtbxLogs
			// 
			this.rtbxLogs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.rtbxLogs.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.rtbxLogs.Font = new System.Drawing.Font("Source Sans Pro", 9.65F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rtbxLogs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.rtbxLogs.Location = new System.Drawing.Point(0, 0);
			this.rtbxLogs.Margin = new System.Windows.Forms.Padding(1);
			this.rtbxLogs.Name = "rtbxLogs";
			this.rtbxLogs.ReadOnly = true;
			this.rtbxLogs.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.rtbxLogs.Size = new System.Drawing.Size(375, 126);
			this.rtbxLogs.TabIndex = 5;
			this.rtbxLogs.TabStop = false;
			this.rtbxLogs.Tag = "Source Sans Pro";
			this.rtbxLogs.Text = " Pk2 Extractor v1.2.0 - Made by JellyBitz";
			this.rtbxLogs.TextChanged += new System.EventHandler(this.RichTextBox_TextChanged_AutoScroll);
			// 
			// pnlLogs
			// 
			this.pnlLogs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlLogs.Controls.Add(this.rtbxLogs);
			this.pnlLogs.Location = new System.Drawing.Point(10, 108);
			this.pnlLogs.Name = "pnlLogs";
			this.pnlLogs.Size = new System.Drawing.Size(377, 128);
			this.pnlLogs.TabIndex = 6;
			// 
			// panelWindow
			// 
			this.panelWindow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.panelWindow.Controls.Add(this.cbxMinimap);
			this.panelWindow.Controls.Add(this.lblHeaderIcon);
			this.panelWindow.Controls.Add(this.btnStart);
			this.panelWindow.Controls.Add(this.tbxBlowfishKey);
			this.panelWindow.Controls.Add(this.lblBlowfishKey);
			this.panelWindow.Controls.Add(this.btnWinExit);
			this.panelWindow.Controls.Add(this.pnlLogs);
			this.panelWindow.Controls.Add(this.lblHeader);
			this.panelWindow.Location = new System.Drawing.Point(1, 1);
			this.panelWindow.Name = "panelWindow";
			this.panelWindow.Size = new System.Drawing.Size(398, 247);
			this.panelWindow.TabIndex = 8;
			// 
			// lblHeaderIcon
			// 
			this.lblHeaderIcon.Image = global::xBot.Properties.Resources.ProjexNET_40x40;
			this.lblHeaderIcon.Location = new System.Drawing.Point(4, 1);
			this.lblHeaderIcon.Name = "lblHeaderIcon";
			this.lblHeaderIcon.Size = new System.Drawing.Size(40, 40);
			this.lblHeaderIcon.TabIndex = 17;
			this.lblHeaderIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Window_Drag_MouseDown);
			// 
			// btnStart
			// 
			this.btnStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnStart.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.btnStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.btnStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnStart.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.btnStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.btnStart.Location = new System.Drawing.Point(306, 50);
			this.btnStart.Margin = new System.Windows.Forms.Padding(0);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(81, 28);
			this.btnStart.TabIndex = 15;
			this.btnStart.Tag = "Source Sans Pro";
			this.btnStart.Text = "Start";
			this.btnStart.UseCompatibleTextRendering = true;
			this.btnStart.UseVisualStyleBackColor = false;
			this.btnStart.Click += new System.EventHandler(this.Control_Click);
			// 
			// tbxBlowfishKey
			// 
			this.tbxBlowfishKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.tbxBlowfishKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbxBlowfishKey.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.tbxBlowfishKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.tbxBlowfishKey.Location = new System.Drawing.Point(168, 50);
			this.tbxBlowfishKey.MaxLength = 6;
			this.tbxBlowfishKey.Name = "tbxBlowfishKey";
			this.tbxBlowfishKey.Size = new System.Drawing.Size(135, 28);
			this.tbxBlowfishKey.TabIndex = 12;
			this.tbxBlowfishKey.Tag = "Source Sans Pro";
			this.tbxBlowfishKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.ToolTips.SetToolTip(this.tbxBlowfishKey, "Leave it empty to use default Blowfish Key : 169841");
			this.tbxBlowfishKey.Enter += new System.EventHandler(this.Control_FocusEnter);
			this.tbxBlowfishKey.Leave += new System.EventHandler(this.Control_FocusLeave);
			// 
			// lblBlowfishKey
			// 
			this.lblBlowfishKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(38)))));
			this.lblBlowfishKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblBlowfishKey.Font = new System.Drawing.Font("Source Sans Pro", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.lblBlowfishKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.lblBlowfishKey.Location = new System.Drawing.Point(10, 50);
			this.lblBlowfishKey.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.lblBlowfishKey.Name = "lblBlowfishKey";
			this.lblBlowfishKey.Size = new System.Drawing.Size(160, 28);
			this.lblBlowfishKey.TabIndex = 11;
			this.lblBlowfishKey.Tag = "Source Sans Pro";
			this.lblBlowfishKey.Text = "Set the Blowfish Key";
			this.lblBlowfishKey.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.ToolTips.SetToolTip(this.lblBlowfishKey, "Default Blowfish Key : 169841");
			// 
			// lblHeader
			// 
			this.lblHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.lblHeader.Font = new System.Drawing.Font("Source Sans Pro", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.lblHeader.Location = new System.Drawing.Point(0, 0);
			this.lblHeader.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.lblHeader.Name = "lblHeader";
			this.lblHeader.Padding = new System.Windows.Forms.Padding(48, 0, 0, 0);
			this.lblHeader.Size = new System.Drawing.Size(398, 42);
			this.lblHeader.TabIndex = 16;
			this.lblHeader.Tag = "Source Sans Pro";
			this.lblHeader.Text = "Pk2 Extractor";
			this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Window_Drag_MouseDown);
			// 
			// lblProcessState
			// 
			this.lblProcessState.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblProcessState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.lblProcessState.Font = new System.Drawing.Font("Source Sans Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.lblProcessState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.lblProcessState.Location = new System.Drawing.Point(1, 250);
			this.lblProcessState.Margin = new System.Windows.Forms.Padding(0);
			this.lblProcessState.Name = "lblProcessState";
			this.lblProcessState.Size = new System.Drawing.Size(398, 15);
			this.lblProcessState.TabIndex = 9;
			this.lblProcessState.Tag = "Source Sans Pro";
			this.lblProcessState.Text = "xBot - ProjexNET";
			this.lblProcessState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblProcessState.UseMnemonic = false;
			// 
			// cbxMinimap
			// 
			this.cbxMinimap.Cursor = System.Windows.Forms.Cursors.Default;
			this.cbxMinimap.FlatAppearance.BorderSize = 0;
			this.cbxMinimap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cbxMinimap.Font = new System.Drawing.Font("Source Sans Pro", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.cbxMinimap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.cbxMinimap.Location = new System.Drawing.Point(11, 80);
			this.cbxMinimap.Margin = new System.Windows.Forms.Padding(0);
			this.cbxMinimap.Name = "cbxMinimap";
			this.cbxMinimap.Size = new System.Drawing.Size(75, 25);
			this.cbxMinimap.TabIndex = 18;
			this.cbxMinimap.Tag = "Source Sans Pro";
			this.cbxMinimap.Text = "Minimap";
			this.ToolTips.SetToolTip(this.cbxMinimap, "Extract minimap folder");
			this.cbxMinimap.UseVisualStyleBackColor = false;
			// 
			// Pk2Extractor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.ClientSize = new System.Drawing.Size(400, 268);
			this.Controls.Add(this.lblProcessState);
			this.Controls.Add(this.panelWindow);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Pk2Extractor";
			this.Padding = new System.Windows.Forms.Padding(1);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.pnlLogs.ResumeLayout(false);
			this.panelWindow.ResumeLayout(false);
			this.panelWindow.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.RichTextBox rtbxLogs;
		private System.Windows.Forms.Panel pnlLogs;
		private System.Windows.Forms.Button btnWinExit;
		private System.Windows.Forms.Panel panelWindow;
		private System.Windows.Forms.Label lblBlowfishKey;
		public System.Windows.Forms.TextBox tbxBlowfishKey;
		public System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Label lblHeader;
		private System.Windows.Forms.Label lblHeaderIcon;
		private System.Windows.Forms.ToolTip ToolTips;
		private System.Windows.Forms.Label lblProcessState;
		public System.Windows.Forms.CheckBox cbxMinimap;
	}
}