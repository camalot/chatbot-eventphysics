namespace MapMaker {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose ( bool disposing ) {
			if ( disposing && ( components != null ) ) {
				components.Dispose ( );
			}
			base.Dispose ( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ( ) {
			this.designer = new System.Windows.Forms.PictureBox();
			this.addBox = new System.Windows.Forms.Button();
			this.save = new System.Windows.Forms.Button();
			this.loadImage = new System.Windows.Forms.Button();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.mapSettingsTab = new System.Windows.Forms.TabPage();
			this.scriptSettingsTab = new System.Windows.Forms.TabPage();
			this.mapBoxProperties = new System.Windows.Forms.PropertyGrid();
			this.propertyGrid2 = new System.Windows.Forms.PropertyGrid();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.mapTree = new System.Windows.Forms.TreeView();
			((System.ComponentModel.ISupportInitialize)(this.designer)).BeginInit();
			this.tabControl.SuspendLayout();
			this.mapSettingsTab.SuspendLayout();
			this.scriptSettingsTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// designer
			// 
			this.designer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.designer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.designer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.designer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.designer.Location = new System.Drawing.Point(115, 12);
			this.designer.Name = "designer";
			this.designer.Size = new System.Drawing.Size(900, 506);
			this.designer.TabIndex = 0;
			this.designer.TabStop = false;
			// 
			// addBox
			// 
			this.addBox.Location = new System.Drawing.Point(12, 12);
			this.addBox.Name = "addBox";
			this.addBox.Size = new System.Drawing.Size(75, 23);
			this.addBox.TabIndex = 1;
			this.addBox.Text = "Add Box";
			this.addBox.UseVisualStyleBackColor = true;
			this.addBox.Click += new System.EventHandler(this.AddBox_Click);
			// 
			// save
			// 
			this.save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.save.Location = new System.Drawing.Point(12, 495);
			this.save.Name = "save";
			this.save.Size = new System.Drawing.Size(75, 23);
			this.save.TabIndex = 2;
			this.save.Text = "Save";
			this.save.UseVisualStyleBackColor = true;
			this.save.Click += new System.EventHandler(this.Save_Click);
			// 
			// loadImage
			// 
			this.loadImage.Location = new System.Drawing.Point(12, 50);
			this.loadImage.Name = "loadImage";
			this.loadImage.Size = new System.Drawing.Size(75, 23);
			this.loadImage.TabIndex = 3;
			this.loadImage.Text = "Load Image";
			this.loadImage.UseVisualStyleBackColor = true;
			this.loadImage.Click += new System.EventHandler(this.LoadImage_Click);
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.mapSettingsTab);
			this.tabControl.Controls.Add(this.scriptSettingsTab);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Right;
			this.tabControl.Location = new System.Drawing.Point(1021, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(273, 537);
			this.tabControl.TabIndex = 4;
			// 
			// mapSettingsTab
			// 
			this.mapSettingsTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.mapSettingsTab.Controls.Add(this.splitContainer1);
			this.mapSettingsTab.Location = new System.Drawing.Point(4, 22);
			this.mapSettingsTab.Margin = new System.Windows.Forms.Padding(0);
			this.mapSettingsTab.Name = "mapSettingsTab";
			this.mapSettingsTab.Size = new System.Drawing.Size(265, 511);
			this.mapSettingsTab.TabIndex = 0;
			this.mapSettingsTab.Text = "Map";
			// 
			// scriptSettingsTab
			// 
			this.scriptSettingsTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.scriptSettingsTab.Controls.Add(this.propertyGrid2);
			this.scriptSettingsTab.Location = new System.Drawing.Point(4, 22);
			this.scriptSettingsTab.Margin = new System.Windows.Forms.Padding(0);
			this.scriptSettingsTab.Name = "scriptSettingsTab";
			this.scriptSettingsTab.Size = new System.Drawing.Size(265, 511);
			this.scriptSettingsTab.TabIndex = 1;
			this.scriptSettingsTab.Text = "Settings";
			// 
			// mapBoxProperties
			// 
			this.mapBoxProperties.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.mapBoxProperties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mapBoxProperties.HelpVisible = false;
			this.mapBoxProperties.LineColor = System.Drawing.Color.Gray;
			this.mapBoxProperties.Location = new System.Drawing.Point(0, 0);
			this.mapBoxProperties.Margin = new System.Windows.Forms.Padding(0);
			this.mapBoxProperties.Name = "mapBoxProperties";
			this.mapBoxProperties.Size = new System.Drawing.Size(265, 324);
			this.mapBoxProperties.TabIndex = 0;
			this.mapBoxProperties.ViewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.mapBoxProperties.ViewBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.mapBoxProperties.ViewForeColor = System.Drawing.SystemColors.ControlLightLight;
			// 
			// propertyGrid2
			// 
			this.propertyGrid2.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.propertyGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid2.HelpVisible = false;
			this.propertyGrid2.LineColor = System.Drawing.Color.Gray;
			this.propertyGrid2.Location = new System.Drawing.Point(0, 0);
			this.propertyGrid2.Margin = new System.Windows.Forms.Padding(0);
			this.propertyGrid2.Name = "propertyGrid2";
			this.propertyGrid2.Size = new System.Drawing.Size(265, 511);
			this.propertyGrid2.TabIndex = 1;
			this.propertyGrid2.ViewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.propertyGrid2.ViewBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.propertyGrid2.ViewForeColor = System.Drawing.SystemColors.ControlLightLight;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.mapTree);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.mapBoxProperties);
			this.splitContainer1.Size = new System.Drawing.Size(265, 511);
			this.splitContainer1.SplitterDistance = 183;
			this.splitContainer1.TabIndex = 1;
			// 
			// mapTree
			// 
			this.mapTree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.mapTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.mapTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mapTree.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.mapTree.FullRowSelect = true;
			this.mapTree.Location = new System.Drawing.Point(0, 0);
			this.mapTree.Name = "mapTree";
			this.mapTree.Size = new System.Drawing.Size(265, 183);
			this.mapTree.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ClientSize = new System.Drawing.Size(1294, 537);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.loadImage);
			this.Controls.Add(this.save);
			this.Controls.Add(this.addBox);
			this.Controls.Add(this.designer);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(1310, 576);
			this.MinimumSize = new System.Drawing.Size(1310, 576);
			this.Name = "MainForm";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.designer)).EndInit();
			this.tabControl.ResumeLayout(false);
			this.mapSettingsTab.ResumeLayout(false);
			this.scriptSettingsTab.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox designer;
		private System.Windows.Forms.Button addBox;
		private System.Windows.Forms.Button save;
		private System.Windows.Forms.Button loadImage;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage mapSettingsTab;
		private System.Windows.Forms.PropertyGrid mapBoxProperties;
		private System.Windows.Forms.TabPage scriptSettingsTab;
		private System.Windows.Forms.PropertyGrid propertyGrid2;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TreeView mapTree;
	}
}

