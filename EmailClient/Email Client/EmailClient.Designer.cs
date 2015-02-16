namespace Email_Client
{
    partial class EmailClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmailClient));
            this.EmailTab = new System.Windows.Forms.TabControl();
            this.ComposeMailTab = new System.Windows.Forms.TabPage();
            this.SmtpClear = new System.Windows.Forms.Button();
            this.Send = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.MailMessage = new System.Windows.Forms.RichTextBox();
            this.FormattingToolStrip = new System.Windows.Forms.ToolStrip();
            this.FontStyle = new System.Windows.Forms.ToolStripComboBox();
            this.FontSize = new System.Windows.Forms.ToolStripComboBox();
            this.Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Bold = new System.Windows.Forms.ToolStripButton();
            this.Italic = new System.Windows.Forms.ToolStripButton();
            this.Underline = new System.Windows.Forms.ToolStripButton();
            this.Separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FontColor = new System.Windows.Forms.ToolStripButton();
            this.FontBackgroundColor = new System.Windows.Forms.ToolStripButton();
            this.AttachmentToolStrip = new System.Windows.Forms.ToolStrip();
            this.AddAttachment = new System.Windows.Forms.ToolStripButton();
            this.DeleteAttachment = new System.Windows.Forms.ToolStripButton();
            this.Attachments = new System.Windows.Forms.ListView();
            this.images = new System.Windows.Forms.ImageList(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.Subject = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Bcc = new System.Windows.Forms.TextBox();
            this.Cc = new System.Windows.Forms.TextBox();
            this.To = new System.Windows.Forms.TextBox();
            this.From = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.EmailToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.EmailTimer = new System.Windows.Forms.Timer(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.ProgressLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.EmailTab.SuspendLayout();
            this.ComposeMailTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.FormattingToolStrip.SuspendLayout();
            this.AttachmentToolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // EmailTab
            // 
            this.EmailTab.Controls.Add(this.ComposeMailTab);
            this.EmailTab.Location = new System.Drawing.Point(26, 12);
            this.EmailTab.Name = "EmailTab";
            this.EmailTab.SelectedIndex = 0;
            this.EmailTab.Size = new System.Drawing.Size(656, 507);
            this.EmailTab.TabIndex = 0;
            // 
            // ComposeMailTab
            // 
            this.ComposeMailTab.Controls.Add(this.SmtpClear);
            this.ComposeMailTab.Controls.Add(this.Send);
            this.ComposeMailTab.Controls.Add(this.groupBox1);
            this.ComposeMailTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComposeMailTab.Location = new System.Drawing.Point(4, 22);
            this.ComposeMailTab.Name = "ComposeMailTab";
            this.ComposeMailTab.Padding = new System.Windows.Forms.Padding(3);
            this.ComposeMailTab.Size = new System.Drawing.Size(648, 481);
            this.ComposeMailTab.TabIndex = 1;
            this.ComposeMailTab.Text = "Compose Mail";
            this.ComposeMailTab.ToolTipText = "Compose Email";
            this.ComposeMailTab.UseVisualStyleBackColor = true;
            this.ComposeMailTab.Click += new System.EventHandler(this.ComposeMailTab_Click);
            // 
            // SmtpClear
            // 
            this.SmtpClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SmtpClear.Location = new System.Drawing.Point(526, 433);
            this.SmtpClear.Name = "SmtpClear";
            this.SmtpClear.Size = new System.Drawing.Size(79, 36);
            this.SmtpClear.TabIndex = 3;
            this.SmtpClear.Text = "Clear";
            this.EmailToolTip.SetToolTip(this.SmtpClear, "Clear text in fields");
            this.SmtpClear.UseVisualStyleBackColor = true;
            this.SmtpClear.Click += new System.EventHandler(this.SmtpClear_Click);
            // 
            // Send
            // 
            this.Send.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Send.Location = new System.Drawing.Point(430, 433);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(79, 36);
            this.Send.TabIndex = 2;
            this.Send.Text = "Send";
            this.EmailToolTip.SetToolTip(this.Send, "Send an email message");
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.MailMessage);
            this.groupBox1.Controls.Add(this.FormattingToolStrip);
            this.groupBox1.Controls.Add(this.AttachmentToolStrip);
            this.groupBox1.Controls.Add(this.Attachments);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.Subject);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.Bcc);
            this.groupBox1.Controls.Add(this.Cc);
            this.groupBox1.Controls.Add(this.To);
            this.groupBox1.Controls.Add(this.From);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Silver;
            this.groupBox1.Location = new System.Drawing.Point(26, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(597, 408);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Email Document Message";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(9, 201);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 15);
            this.label11.TabIndex = 15;
            this.label11.Text = "Message :";
            // 
            // MailMessage
            // 
            this.MailMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MailMessage.Location = new System.Drawing.Point(89, 198);
            this.MailMessage.Name = "MailMessage";
            this.MailMessage.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.MailMessage.Size = new System.Drawing.Size(485, 200);
            this.MailMessage.TabIndex = 14;
            this.MailMessage.Text = "";
            this.EmailToolTip.SetToolTip(this.MailMessage, "Type message here");
            this.MailMessage.SelectionChanged += new System.EventHandler(this.MailMessage_SelectionChanged);
            this.MailMessage.TextChanged += new System.EventHandler(this.MailMessage_TextChanged);
            // 
            // FormattingToolStrip
            // 
            this.FormattingToolStrip.AutoSize = false;
            this.FormattingToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.FormattingToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.FormattingToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FontStyle,
            this.FontSize,
            this.Separator1,
            this.Bold,
            this.Italic,
            this.Underline,
            this.Separator2,
            this.FontColor,
            this.FontBackgroundColor});
            this.FormattingToolStrip.Location = new System.Drawing.Point(89, 168);
            this.FormattingToolStrip.Name = "FormattingToolStrip";
            this.FormattingToolStrip.Size = new System.Drawing.Size(374, 25);
            this.FormattingToolStrip.TabIndex = 13;
            // 
            // FontStyle
            // 
            this.FontStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FontStyle.Items.AddRange(new object[] {
            "Arial",
            "Courier New",
            "Times New Roman",
            "Verdana"});
            this.FontStyle.Name = "FontStyle";
            this.FontStyle.Size = new System.Drawing.Size(150, 25);
            this.FontStyle.ToolTipText = "Font";
            this.FontStyle.SelectedIndexChanged += new System.EventHandler(this.FontStyle_SelectedIndexChanged);
            // 
            // FontSize
            // 
            this.FontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FontSize.Items.AddRange(new object[] {
            "8",
            "10",
            "12",
            "14",
            "18",
            "24",
            "32",
            "36"});
            this.FontSize.Name = "FontSize";
            this.FontSize.Size = new System.Drawing.Size(75, 25);
            this.FontSize.ToolTipText = "Font Size";
            this.FontSize.SelectedIndexChanged += new System.EventHandler(this.FontSize_SelectedIndexChanged);
            this.FontSize.Click += new System.EventHandler(this.FontSize_Click);
            // 
            // Separator1
            // 
            this.Separator1.Name = "Separator1";
            this.Separator1.Size = new System.Drawing.Size(6, 25);
            // 
            // Bold
            // 
            this.Bold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Bold.Image = global::Email_Client.Properties.Resources.bold;
            this.Bold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Bold.Name = "Bold";
            this.Bold.Size = new System.Drawing.Size(23, 22);
            this.Bold.ToolTipText = "Bold";
            this.Bold.Click += new System.EventHandler(this.Bold_Click);
            // 
            // Italic
            // 
            this.Italic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Italic.Image = global::Email_Client.Properties.Resources.italic;
            this.Italic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Italic.Name = "Italic";
            this.Italic.Size = new System.Drawing.Size(23, 22);
            this.Italic.ToolTipText = "Italic";
            this.Italic.Click += new System.EventHandler(this.Italic_Click);
            // 
            // Underline
            // 
            this.Underline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Underline.Image = global::Email_Client.Properties.Resources.underline;
            this.Underline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Underline.Name = "Underline";
            this.Underline.Size = new System.Drawing.Size(23, 22);
            this.Underline.ToolTipText = "Underline";
            this.Underline.Click += new System.EventHandler(this.Underline_Click);
            // 
            // Separator2
            // 
            this.Separator2.Name = "Separator2";
            this.Separator2.Size = new System.Drawing.Size(6, 25);
            // 
            // FontColor
            // 
            this.FontColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FontColor.Image = global::Email_Client.Properties.Resources.fontcolor;
            this.FontColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FontColor.Name = "FontColor";
            this.FontColor.Size = new System.Drawing.Size(23, 22);
            this.FontColor.ToolTipText = "Font Color";
            this.FontColor.Click += new System.EventHandler(this.FontColor_Click);
            // 
            // FontBackgroundColor
            // 
            this.FontBackgroundColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FontBackgroundColor.Image = global::Email_Client.Properties.Resources.fontbackcolor;
            this.FontBackgroundColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FontBackgroundColor.Name = "FontBackgroundColor";
            this.FontBackgroundColor.Size = new System.Drawing.Size(23, 22);
            this.FontBackgroundColor.ToolTipText = "Font Background Color";
            this.FontBackgroundColor.Click += new System.EventHandler(this.FontBackgroundColor_Click);
            // 
            // AttachmentToolStrip
            // 
            this.AttachmentToolStrip.AutoSize = false;
            this.AttachmentToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.AttachmentToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.AttachmentToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddAttachment,
            this.DeleteAttachment});
            this.AttachmentToolStrip.Location = new System.Drawing.Point(455, 101);
            this.AttachmentToolStrip.Name = "AttachmentToolStrip";
            this.AttachmentToolStrip.Size = new System.Drawing.Size(65, 25);
            this.AttachmentToolStrip.TabIndex = 12;
            this.AttachmentToolStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.AttachmentToolStrip_ItemClicked);
            // 
            // AddAttachment
            // 
            this.AddAttachment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddAttachment.Image = global::Email_Client.Properties.Resources.add;
            this.AddAttachment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddAttachment.Name = "AddAttachment";
            this.AddAttachment.Size = new System.Drawing.Size(23, 22);
            this.AddAttachment.Tag = "Add";
            this.AddAttachment.ToolTipText = "Add attachment from directory";
            this.AddAttachment.Click += new System.EventHandler(this.AddAttachment_Click);
            // 
            // DeleteAttachment
            // 
            this.DeleteAttachment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DeleteAttachment.Enabled = false;
            this.DeleteAttachment.Image = global::Email_Client.Properties.Resources.delete;
            this.DeleteAttachment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteAttachment.Name = "DeleteAttachment";
            this.DeleteAttachment.Size = new System.Drawing.Size(23, 22);
            this.DeleteAttachment.Tag = "Delete";
            this.DeleteAttachment.ToolTipText = "Remove an attachment";
            // 
            // Attachments
            // 
            this.Attachments.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Attachments.HideSelection = false;
            this.Attachments.Location = new System.Drawing.Point(89, 106);
            this.Attachments.MultiSelect = false;
            this.Attachments.Name = "Attachments";
            this.Attachments.Size = new System.Drawing.Size(363, 58);
            this.Attachments.SmallImageList = this.images;
            this.Attachments.TabIndex = 11;
            this.EmailToolTip.SetToolTip(this.Attachments, "Attachments list");
            this.Attachments.UseCompatibleStateImageBehavior = false;
            this.Attachments.View = System.Windows.Forms.View.SmallIcon;
            this.Attachments.SelectedIndexChanged += new System.EventHandler(this.Attachments_SelectedIndexChanged);
            // 
            // images
            // 
            this.images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("images.ImageStream")));
            this.images.TransparentColor = System.Drawing.Color.Transparent;
            this.images.Images.SetKeyName(0, "attachment.ico");
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(5, 108);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 15);
            this.label10.TabIndex = 10;
            this.label10.Text = "Attachments :";
            // 
            // Subject
            // 
            this.Subject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Subject.Location = new System.Drawing.Point(87, 78);
            this.Subject.Name = "Subject";
            this.Subject.Size = new System.Drawing.Size(206, 21);
            this.Subject.TabIndex = 9;
            this.EmailToolTip.SetToolTip(this.Subject, "Type the subject for this message");
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(9, 81);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 15);
            this.label9.TabIndex = 8;
            this.label9.Text = "Subject :";
            // 
            // Bcc
            // 
            this.Bcc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Bcc.Location = new System.Drawing.Point(368, 51);
            this.Bcc.Name = "Bcc";
            this.Bcc.Size = new System.Drawing.Size(206, 21);
            this.Bcc.TabIndex = 7;
            this.EmailToolTip.SetToolTip(this.Bcc, "Type recipients\' email addresses, separated by semicolons or commas");
            // 
            // Cc
            // 
            this.Cc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cc.Location = new System.Drawing.Point(87, 51);
            this.Cc.Name = "Cc";
            this.Cc.Size = new System.Drawing.Size(206, 21);
            this.Cc.TabIndex = 6;
            this.EmailToolTip.SetToolTip(this.Cc, "Type recipients\' email addresses, separated by semicolons or commas");
            // 
            // To
            // 
            this.To.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.To.Location = new System.Drawing.Point(368, 24);
            this.To.Name = "To";
            this.To.Size = new System.Drawing.Size(206, 21);
            this.To.TabIndex = 5;
            this.EmailToolTip.SetToolTip(this.To, "Type recipients\' email addresses, separated by semicolons or commas");
            this.To.TextChanged += new System.EventHandler(this.To_TextChanged);
            // 
            // From
            // 
            this.From.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.From.Location = new System.Drawing.Point(87, 24);
            this.From.Name = "From";
            this.From.Size = new System.Drawing.Size(206, 21);
            this.From.TabIndex = 4;
            this.EmailToolTip.SetToolTip(this.From, "Type sender email address");
            this.From.TextChanged += new System.EventHandler(this.From_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(309, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 15);
            this.label8.TabIndex = 3;
            this.label8.Text = "Bcc :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(16, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "Cc :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(309, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "To :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(16, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "From :";
            // 
            // EmailTimer
            // 
            this.EmailTimer.Enabled = true;
            this.EmailTimer.Interval = 10;
            this.EmailTimer.Tick += new System.EventHandler(this.EmailTimer_Tick);
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProgressLabel});
            this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip.Location = new System.Drawing.Point(0, 525);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(706, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 1;
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(61, 17);
            this.ProgressLabel.Text = "Email Client";
            // 
            // EmailClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 547);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.EmailTab);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "EmailClient";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Email Document";
            this.EmailTab.ResumeLayout(false);
            this.ComposeMailTab.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.FormattingToolStrip.ResumeLayout(false);
            this.FormattingToolStrip.PerformLayout();
            this.AttachmentToolStrip.ResumeLayout(false);
            this.AttachmentToolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl EmailTab;
        private System.Windows.Forms.TabPage ComposeMailTab;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox From;
        private System.Windows.Forms.TextBox To;
        private System.Windows.Forms.TextBox Bcc;
        private System.Windows.Forms.TextBox Cc;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox Subject;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListView Attachments;
        private System.Windows.Forms.ToolStrip AttachmentToolStrip;
        private System.Windows.Forms.ToolStripButton AddAttachment;
        private System.Windows.Forms.ToolStripButton DeleteAttachment;
        private System.Windows.Forms.ToolStrip FormattingToolStrip;
        private System.Windows.Forms.ToolStripComboBox FontStyle;
        private System.Windows.Forms.ToolStripComboBox FontSize;
        private System.Windows.Forms.ToolStripButton Bold;
        private System.Windows.Forms.ToolStripSeparator Separator1;
        private System.Windows.Forms.ToolStripButton Italic;
        private System.Windows.Forms.ToolStripButton Underline;
        private System.Windows.Forms.ToolStripSeparator Separator2;
        private System.Windows.Forms.ToolStripButton FontColor;
        private System.Windows.Forms.ToolStripButton FontBackgroundColor;
        private System.Windows.Forms.RichTextBox MailMessage;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button Send;
        private System.Windows.Forms.ToolTip EmailToolTip;
        private System.Windows.Forms.Timer EmailTimer;
        private System.Windows.Forms.ImageList images;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel ProgressLabel;
        private System.Windows.Forms.Button SmtpClear;
    }
}

