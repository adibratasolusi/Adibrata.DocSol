﻿namespace ImageBoxSample
{
  partial class MainForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.imageBox = new Cyotek.Windows.Forms.ImageBox();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.positionToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.imageSizeToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.zoomToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.showImageRegionToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.showSourceImageRegionToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnDownload = new System.Windows.Forms.Button();
            this.dlg = new System.Windows.Forms.SaveFileDialog();
            this.btnNote = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkWatermark = new System.Windows.Forms.CheckBox();
            this.txtNote = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.imageBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.propertyGrid);
            this.splitContainer1.Size = new System.Drawing.Size(1007, 416);
            this.splitContainer1.SplitterDistance = 707;
            this.splitContainer1.TabIndex = 0;
            // 
            // imageBox
            // 
            this.imageBox.AutoScroll = true;
            this.imageBox.AutoSize = false;
            this.imageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox.Location = new System.Drawing.Point(0, 0);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(707, 416);
            this.imageBox.TabIndex = 0;
            this.imageBox.ZoomChanged += new System.EventHandler(this.imageBox_ZoomChanged);
            this.imageBox.Scroll += new System.Windows.Forms.ScrollEventHandler(this.imageBox_Scroll);
            this.imageBox.Paint += new System.Windows.Forms.PaintEventHandler(this.imageBox_Paint);
            this.imageBox.Resize += new System.EventHandler(this.imageBox_Resize);
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.SelectedObject = this.imageBox;
            this.propertyGrid.Size = new System.Drawing.Size(296, 416);
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.Click += new System.EventHandler(this.propertyGrid_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.positionToolStripStatusLabel,
            this.imageSizeToolStripStatusLabel,
            this.zoomToolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 441);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1007, 25);
            this.statusStrip.TabIndex = 1;
            // 
            // positionToolStripStatusLabel
            // 
            this.positionToolStripStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.positionToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.positionToolStripStatusLabel.Image = global::Cyotek.Windows.Forms.Properties.Resources.Object_Position;
            this.positionToolStripStatusLabel.Name = "positionToolStripStatusLabel";
            this.positionToolStripStatusLabel.Size = new System.Drawing.Size(20, 20);
            // 
            // imageSizeToolStripStatusLabel
            // 
            this.imageSizeToolStripStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.imageSizeToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.imageSizeToolStripStatusLabel.Image = global::Cyotek.Windows.Forms.Properties.Resources.Object_Size;
            this.imageSizeToolStripStatusLabel.Name = "imageSizeToolStripStatusLabel";
            this.imageSizeToolStripStatusLabel.Size = new System.Drawing.Size(20, 20);
            // 
            // zoomToolStripStatusLabel
            // 
            this.zoomToolStripStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.zoomToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.zoomToolStripStatusLabel.Image = global::Cyotek.Windows.Forms.Properties.Resources.magnifier_zoom;
            this.zoomToolStripStatusLabel.Name = "zoomToolStripStatusLabel";
            this.zoomToolStripStatusLabel.Size = new System.Drawing.Size(20, 20);
            // 
            // showImageRegionToolStripButton
            // 
            this.showImageRegionToolStripButton.CheckOnClick = true;
            this.showImageRegionToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showImageRegionToolStripButton.Image = global::Cyotek.Windows.Forms.Properties.Resources.zone;
            this.showImageRegionToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showImageRegionToolStripButton.Name = "showImageRegionToolStripButton";
            this.showImageRegionToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.showImageRegionToolStripButton.Text = "Show Image Region";
            this.showImageRegionToolStripButton.Click += new System.EventHandler(this.showImageRegionToolStripButton_Click);
            // 
            // showSourceImageRegionToolStripButton
            // 
            this.showSourceImageRegionToolStripButton.CheckOnClick = true;
            this.showSourceImageRegionToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.showSourceImageRegionToolStripButton.Image = global::Cyotek.Windows.Forms.Properties.Resources.zone;
            this.showSourceImageRegionToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showSourceImageRegionToolStripButton.Name = "showSourceImageRegionToolStripButton";
            this.showSourceImageRegionToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.showSourceImageRegionToolStripButton.Text = "Show Source Image Region";
            this.showSourceImageRegionToolStripButton.Click += new System.EventHandler(this.showImageRegionToolStripButton_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showImageRegionToolStripButton,
            this.showSourceImageRegionToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1007, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(920, 2);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 3;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnNote
            // 
            this.btnNote.Location = new System.Drawing.Point(299, 1);
            this.btnNote.Name = "btnNote";
            this.btnNote.Size = new System.Drawing.Size(75, 23);
            this.btnNote.TabIndex = 4;
            this.btnNote.Text = "Add Note";
            this.btnNote.UseVisualStyleBackColor = true;
            this.btnNote.Click += new System.EventHandler(this.btnNote_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(108, 1);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Note :";
            // 
            // chkWatermark
            // 
            this.chkWatermark.AutoSize = true;
            this.chkWatermark.Location = new System.Drawing.Point(215, 6);
            this.chkWatermark.Name = "chkWatermark";
            this.chkWatermark.Size = new System.Drawing.Size(78, 17);
            this.chkWatermark.TabIndex = 7;
            this.chkWatermark.Text = "Watermark";
            this.chkWatermark.UseVisualStyleBackColor = true;
            this.chkWatermark.CheckedChanged += new System.EventHandler(this.chkWatermark_CheckedChanged);
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(108, 3);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(100, 20);
            this.txtNote.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 466);
            this.Controls.Add(this.chkWatermark);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnNote);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainForm";
            this.Text = "Image Viewer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private Cyotek.Windows.Forms.ImageBox imageBox;
    private System.Windows.Forms.PropertyGrid propertyGrid;
    private System.Windows.Forms.StatusStrip statusStrip;
    private System.Windows.Forms.ToolStripStatusLabel positionToolStripStatusLabel;
    private System.Windows.Forms.ToolStripStatusLabel imageSizeToolStripStatusLabel;
    private System.Windows.Forms.ToolStripStatusLabel zoomToolStripStatusLabel;
    private System.Windows.Forms.ToolStripButton showImageRegionToolStripButton;
    private System.Windows.Forms.ToolStripButton showSourceImageRegionToolStripButton;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.Button btnDownload;
    private System.Windows.Forms.SaveFileDialog dlg;
    private System.Windows.Forms.Button btnNote;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.CheckBox chkWatermark;
    private System.Windows.Forms.TextBox txtNote;
  }
}

