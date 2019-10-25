namespace xBot.App
{
	partial class Ads
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
			this.lblHeader = new System.Windows.Forms.Label();
			this.lblHeaderIcon = new System.Windows.Forms.Label();
			this.panelAdvertising = new System.Windows.Forms.Panel();
			this.pbxBanner = new System.Windows.Forms.PictureBox();
			this.btnWinExit = new System.Windows.Forms.Button();
			this.lblAdName = new System.Windows.Forms.Label();
			this.ToolTipLink = new System.Windows.Forms.ToolTip(this.components);
			this.ToolTips = new System.Windows.Forms.ToolTip(this.components);
			this.panelAdvertising.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbxBanner)).BeginInit();
			this.SuspendLayout();
			// 
			// lblHeader
			// 
			this.lblHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.lblHeader.Font = new System.Drawing.Font("Source Sans Pro", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.lblHeader.Location = new System.Drawing.Point(1, 1);
			this.lblHeader.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.lblHeader.Name = "lblHeader";
			this.lblHeader.Padding = new System.Windows.Forms.Padding(48, 0, 0, 0);
			this.lblHeader.Size = new System.Drawing.Size(598, 42);
			this.lblHeader.TabIndex = 0;
			this.lblHeader.Tag = "Source Sans Pro";
			this.lblHeader.Text = "Advertising xx ...";
			this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblHeader.UseMnemonic = false;
			// 
			// lblHeaderIcon
			// 
			this.lblHeaderIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.lblHeaderIcon.Image = global::xBot.Properties.Resources.ProjexNET_40x40;
			this.lblHeaderIcon.Location = new System.Drawing.Point(5, 2);
			this.lblHeaderIcon.Name = "lblHeaderIcon";
			this.lblHeaderIcon.Size = new System.Drawing.Size(40, 40);
			this.lblHeaderIcon.TabIndex = 0;
			// 
			// panelAdvertising
			// 
			this.panelAdvertising.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.panelAdvertising.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.panelAdvertising.Controls.Add(this.pbxBanner);
			this.panelAdvertising.Font = new System.Drawing.Font("Source Sans Pro", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panelAdvertising.Location = new System.Drawing.Point(1, 43);
			this.panelAdvertising.Name = "panelAdvertising";
			this.panelAdvertising.Padding = new System.Windows.Forms.Padding(9, 7, 9, 9);
			this.panelAdvertising.Size = new System.Drawing.Size(598, 356);
			this.panelAdvertising.TabIndex = 0;
			this.panelAdvertising.Tag = "Source Sans Pro";
			// 
			// pbxBanner
			// 
			this.pbxBanner.Location = new System.Drawing.Point(9, 7);
			this.pbxBanner.Name = "pbxBanner";
			this.pbxBanner.Size = new System.Drawing.Size(580, 340);
			this.pbxBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbxBanner.TabIndex = 0;
			this.pbxBanner.TabStop = false;
			this.pbxBanner.Click += new System.EventHandler(this.Control_Click);
			// 
			// btnWinExit
			// 
			this.btnWinExit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnWinExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.btnWinExit.Enabled = false;
			this.btnWinExit.FlatAppearance.BorderSize = 0;
			this.btnWinExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.btnWinExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
			this.btnWinExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnWinExit.Font = new System.Drawing.Font("Font Awesome 5 Pro Regular", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.btnWinExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.btnWinExit.Location = new System.Drawing.Point(561, 10);
			this.btnWinExit.Margin = new System.Windows.Forms.Padding(0);
			this.btnWinExit.Name = "btnWinExit";
			this.btnWinExit.Size = new System.Drawing.Size(24, 24);
			this.btnWinExit.TabIndex = 0;
			this.btnWinExit.Tag = "Font Awesome 5 Pro Regular";
			this.btnWinExit.Text = "";
			this.btnWinExit.UseCompatibleTextRendering = true;
			this.btnWinExit.UseVisualStyleBackColor = false;
			this.btnWinExit.Click += new System.EventHandler(this.Control_Click);
			// 
			// lblAdName
			// 
			this.lblAdName.AutoEllipsis = true;
			this.lblAdName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.lblAdName.Font = new System.Drawing.Font("Source Sans Pro", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
			this.lblAdName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
			this.lblAdName.Location = new System.Drawing.Point(204, 10);
			this.lblAdName.Name = "lblAdName";
			this.lblAdName.Size = new System.Drawing.Size(341, 24);
			this.lblAdName.TabIndex = 24;
			this.lblAdName.Tag = "Source Sans Pro";
			this.lblAdName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblAdName.UseMnemonic = false;
			// 
			// ToolTipLink
			// 
			this.ToolTipLink.AutoPopDelay = 5000;
			this.ToolTipLink.InitialDelay = 250;
			this.ToolTipLink.ReshowDelay = 100;
			this.ToolTipLink.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.ToolTipLink.ToolTipTitle = "Go to website";
			// 
			// ToolTips
			// 
			this.ToolTips.AutomaticDelay = 100;
			this.ToolTips.AutoPopDelay = 5000;
			this.ToolTips.InitialDelay = 100;
			this.ToolTips.ReshowDelay = 20;
			// 
			// Ads
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.ClientSize = new System.Drawing.Size(600, 400);
			this.Controls.Add(this.lblAdName);
			this.Controls.Add(this.btnWinExit);
			this.Controls.Add(this.panelAdvertising);
			this.Controls.Add(this.lblHeaderIcon);
			this.Controls.Add(this.lblHeader);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Ads";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Tag = "Font Awesome 5 Pro Regular";
			this.Text = "Ads";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ads_Closing);
			this.Load += new System.EventHandler(this.Ads_Load);
			this.panelAdvertising.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbxBanner)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label lblHeader;
		private System.Windows.Forms.Label lblHeaderIcon;
		private System.Windows.Forms.Panel panelAdvertising;
		private System.Windows.Forms.Button btnWinExit;
		private System.Windows.Forms.Label lblAdName;
		private System.Windows.Forms.PictureBox pbxBanner;
		private System.Windows.Forms.ToolTip ToolTipLink;
		private System.Windows.Forms.ToolTip ToolTips;
	}
}