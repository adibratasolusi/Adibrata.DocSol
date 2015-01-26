using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Cyotek.Windows.Forms;
using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.Controller;
using Adibrata.Framework.ImageProcessing;
using System.Data;

namespace ImageBoxSample
{
    // ImageBox sample project
    // http://cyotek.com/article/display/creating-a-scrollable-and-zoomable-image-viewer-in-csharp-part-2
    // Preview image based on bwpx.icns - http://paularmstrongdesigns.com/projects/bwpx-icns/
    // Large preview image from http://www.crazythemes.com/colorful-abstract-widescreen-wallpapers-vol2/2153
    // Zone and Magnifier-Zoom icons from Fugue Icons - http://p.yusukekamiyamane.com/

    public partial class MainForm : Form
    {
        #region  Public Constructors
        public Image img { get; set; }
        public Int64 DocTransBinaryId { get; set; }
        public string UserName { get; set; }
        bool isFirstWatermark = false;
        public MainForm()
        {
            this.InitializeComponent();
            this.UpdateStatusBar();


        }
        public void showDlg()
        {
            DataTable dt = new DataTable();
            imageBox.Image = img;
            imageBox.SizeToFit = true;
            imageBox.SizeToFit = false;
            DocSolEntities _ent = new DocSolEntities();
            _ent.ClassName = "ImageProcess";
            _ent.MethodName = "DocTransBinaryNoteView";
            _ent.Id = this.DocTransBinaryId;
            dt = DocumentSolutionController.DocSolProcess<DataTable>(_ent);
            string note = dt.Rows[0]["Note"].ToString();
            if (note == "-")
            {
                isFirstWatermark = true;
            }
            txtNote.Text = note;
            this.ShowDialog();
        }
        #endregion  Public Constructors

        #region  Event Handlers

        private void imageBox_Paint(object sender, PaintEventArgs e)
        {
            // highlight the image
            if (showImageRegionToolStripButton.Checked)
                this.DrawBox(e.Graphics, Color.CornflowerBlue, ((ImageBox)sender).GetImageViewPort());

            // show the region that will be drawn from the source image
            if (showSourceImageRegionToolStripButton.Checked)
                this.DrawBox(e.Graphics, Color.Firebrick, new Rectangle(((ImageBox)sender).GetImageViewPort().Location, ((ImageBox)sender).GetSourceImageRegion().Size));
        }

        private void imageBox_Scroll(object sender, ScrollEventArgs e)
        {
            this.UpdateStatusBar();
        }

        private void showImageRegionToolStripButton_Click(object sender, EventArgs e)
        {
            imageBox.Invalidate();
        }

        #endregion  Event Handlers

        #region  Private Methods

        private void DrawBox(Graphics graphics, Color color, Rectangle rectangle)
        {
            int offset;
            int penWidth;

            offset = 9;
            penWidth = 2;

            using (SolidBrush brush = new SolidBrush(Color.FromArgb(64, color)))
                graphics.FillRectangle(brush, rectangle);

            using (Pen pen = new Pen(color, penWidth))
            {
                pen.DashStyle = DashStyle.Dot;
                graphics.DrawLine(pen, rectangle.Left, rectangle.Top - offset, rectangle.Left, rectangle.Bottom + offset);
                graphics.DrawLine(pen, rectangle.Left + rectangle.Width, rectangle.Top - offset, rectangle.Left + rectangle.Width, rectangle.Bottom + offset);
                graphics.DrawLine(pen, rectangle.Left - offset, rectangle.Top, rectangle.Right + offset, rectangle.Top);
                graphics.DrawLine(pen, rectangle.Left - offset, rectangle.Bottom, rectangle.Right + offset, rectangle.Bottom);
            }
        }

        #endregion  Private Methods

        private void UpdateStatusBar()
        {
            positionToolStripStatusLabel.Text = imageBox.AutoScrollPosition.ToString();
            imageSizeToolStripStatusLabel.Text = imageBox.GetImageViewPort().ToString();
            zoomToolStripStatusLabel.Text = string.Format("{0}%", imageBox.Zoom);
        }

        private void imageBox_ZoomChanged(object sender, EventArgs e)
        {
            this.UpdateStatusBar();
        }

        private void imageBox_Resize(object sender, EventArgs e)
        {
            this.UpdateStatusBar();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {

            dlg.Title = "Select a picture";
            dlg.AddExtension = true;
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
            "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
            "Portable Network Graphic (*.png)|*.png|" +
            "All files (*.*)|*.*";
            dlg.ShowDialog();
            if (dlg.FileName != "")
            {
                img.Save(dlg.FileName);
            }


        }

        private void propertyGrid_Click(object sender, EventArgs e)
        {

        }

        private void btnNote_Click(object sender, EventArgs e)
        {
            if (chkWatermark.Checked == true)
            {
                if (isFirstWatermark == true)
                {

                    DocSolEntities _ent = new DocSolEntities();
                    _ent.ClassName = "ImageProcess";
                    _ent.MethodName = "UpdateBinaryDocTransBinary";
                    _ent.Id = this.DocTransBinaryId;
                    _ent.Note = txtNote.Text.Trim();
                    _ent.UserName = this.UserName;
                    _ent.NewFileBinary = ImageConverterProcess.imageToByteArray(WaterMarkProcess.SetWatermark(img, txtNote.Text));
                    _ent.OldFileBinary = ImageConverterProcess.imageToByteArray(this.img);
                    DocumentSolutionController.DocSolProcess<string>(_ent);
                }
                else
                {
                    DocSolEntities _ent = new DocSolEntities();
                    _ent.ClassName = "ImageProcess";
                    _ent.MethodName = "DocTransBinaryReplaceFileBinary";
                    _ent.Id = this.DocTransBinaryId;
                    _ent.UserName = this.UserName;
                    _ent.FileBinary = ImageConverterProcess.imageToByteArray(WaterMarkProcess.SetWatermark(img, txtNote.Text));
                    DocumentSolutionController.DocSolProcess<string>(_ent);
                }
            }
            else
            {
                DocSolEntities _ent = new DocSolEntities();
                _ent.ClassName = "ImageProcess";
                _ent.MethodName = "UpdateNoteDocTransBinary";
                _ent.Id = this.DocTransBinaryId;
                _ent.UserName = this.UserName;
                _ent.Note = txtNote.Text.Trim();
                DocumentSolutionController.DocSolProcess<string>(_ent);
            }
        }

        private void chkWatermark_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
