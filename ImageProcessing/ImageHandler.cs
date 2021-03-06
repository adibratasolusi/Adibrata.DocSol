using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using Adibrata.BusinessProcess.DocumentSol.Entities;
using Adibrata.Controller;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Printing;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using PQScan.ImageToPDF;
using System.ComponentModel;
using System.Net;

namespace ImageProcessing
{
    public class ImageHandler
    {


        private string _bitmapPath;
        private Bitmap _currentBitmap;
        private Bitmap _bitmapbeforeProcessing;
        private Bitmap _bitmapPrevCropArea;
        public byte[] Pixels { get; set; }
        public int Depth { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        string path = Adibrata.Configuration.AppConfig.Config("ReportOutputPath");
        public string fullPath;

        public ImageHandler()
        {

        }

        public Bitmap CurrentBitmap
        {
            get
            {
                if (_currentBitmap == null)
                    _currentBitmap = new Bitmap(1, 1);
                return _currentBitmap;
            }
            set { _currentBitmap = value; }
        }

        public Bitmap BitmapBeforeProcessing
        {
            get { return _bitmapbeforeProcessing; }
            set { _bitmapbeforeProcessing = value; }
        }

        public string BitmapPath
        {
            get { return _bitmapPath; }
            set { _bitmapPath = value; }
        }

        public enum ColorFilterTypes
        {
            Red,
            Green,
            Blue
        };

        public void ResetBitmap()
        {
            if (_currentBitmap != null && _bitmapbeforeProcessing != null)
            {
                Bitmap temp = (Bitmap)_currentBitmap.Clone();
                _currentBitmap = (Bitmap)_bitmapbeforeProcessing.Clone();
                _bitmapbeforeProcessing = (Bitmap)temp.Clone();
            }
        }

        public void SaveBitmap(DocSolEntities ent)
        {

            DocSolEntities _ent = new DocSolEntities();
            _ent.ClassName = "ImageProcess";
            _ent.MethodName = "SaveEditImage";
            _ent.Id = ent.Id;
            _ent.FileName = DateTime.Now.ToString("yyyy-MM-dd HHmmss");
            _ent.UserName = ent.UserName;

            _ent.FileBinary = Adibrata.Framework.ImageProcessing.ImageConverterProcess.imageToByteArray((Bitmap)_currentBitmap);

            DocumentSolutionController.DocSolProcess<string>(_ent);
            MessageBox.Show("Save Succes");

        }

        public void SaveAsPdf(DocSolEntities ent)
        {
            #region for ATTACHMENT
            //for (int i = 1; i <= 1; i++)
            //{
            //    Bitmap temp = (Bitmap)_currentBitmap;
            //    PixelFormat cumacekpixel = temp.PixelFormat;
            //    Bitmap bmap = (Bitmap)temp.Clone(new Rectangle(0, 0, temp.Width, temp.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            //    string tmpPath = "c:\\Temp";
            //    StringBuilder sbFileName = new StringBuilder(8000);
            //    sbFileName.Append(tmpPath);
            //    sbFileName.Append("\\");
            //    sbFileName.Append("imgPdf_");
            //    sbFileName.Replace(".", "");
            //    sbFileName.Append(".jpg");
            //    string fullPath = sbFileName.ToString();
            //    if (!Directory.Exists(tmpPath))
            //    {
            //        Directory.CreateDirectory(tmpPath);
            //    }
            //    if (File.Exists(fullPath))
            //    {
            //        File.Delete(fullPath);
            //        bmap.Save(fullPath, ImageFormat.Jpeg);
            //    }
            //    else
            //    {
            //        bmap.Save(fullPath, ImageFormat.Jpeg);

            //    }}
            #endregion

       
            StringBuilder sbFileName = new StringBuilder(8000);
            sbFileName.Append(path);
            sbFileName.Append("\\");
            sbFileName.Append("imgPdf_");
            sbFileName.Append(DateTime.Now.ToString("yyyy-MM-dd HHmmss"));
            sbFileName.Replace(".", "");
            sbFileName.Append(".pdf");
            string fullPath = sbFileName.ToString();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
    
            Bitmap temp = (Bitmap)_currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone(new Rectangle(0, 0, temp.Width, temp.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            PdfDocument doc = new PdfDocument();
            doc.Pages.Add(new PdfPage());
            XGraphics xgr = XGraphics.FromPdfPage(doc.Pages[0]);
            XImage img = XImage.FromGdiPlusImage(bmap);
            xgr.DrawImage(img, 0, 0);
            xgr.Dispose();
            doc.Save(fullPath);
            doc.Close();

            StringBuilder sbName = new StringBuilder(8000);

            sbName.Append("imgtoPdf_");
            sbName.Append(DateTime.Now.ToString("yyyy-MM-dd HHmmss"));
            sbName.Replace(".", "");
            sbName.Append(".pdf");
            string fullName = sbName.ToString();
            var webClient = new WebClient();
            byte[] fileBytes = webClient.DownloadData(fullPath);
            try
            {
                DocSolEntities _ent = new DocSolEntities();
                _ent.ClassName = "ImageProcess";
                _ent.MethodName = "SaveEditImage";
                _ent.Id = ent.Id;
                _ent.UserName = ent.UserName;
                _ent.FileName = fullName;
                _ent.FileBinary = fileBytes;

                DocumentSolutionController.DocSolProcess<string>(_ent);
                File.Delete(fullPath);
                MessageBox.Show("Save Succes");
             
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }




            #region Ngaco
            ////      Graphics gr = Graphics.FromImage(bmap);
            ////      PdfDocument pdf = new PdfDocument();

            ////      pdf.FullPath
            ////      PdfPage pdfPage = pdf.AddPage();
            ////      gr.DrawImage(bmap, XGraphics.FromGraphics);
            ////      XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            ////graph
            ////XGraphics = pdfPage(Graphics.FromImage(temp));
            ////      graph.DrawString(font, XBrushes.Black, new XRect(0, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormat.Center);
            ////      string ;
            ////      pdf.Save(gr);
            ////      Process.Start(pdfFilename);
            ////      DocSolEntities _ent = new DocSolEntities();
            ////      _ent.ClassName = "ImageProcess";
            ////      _ent.MethodName = "SaveEditImage";
            ////      _ent.Id = ent.Id;
            ////      _ent.UserName = ent.UserName;
            ////     _ent.FileBinary= 
            //// PdfDocument doc = new PdfDocument();

            //// PdfPage pagePdf = new PdfPage();

            //// List<string> listPath = new List<string>();
            //// string tmpPath = "C:\\Temp";
            //// string newPath;
            //// //if (!Directory.Exists(tmpPath))
            //// //{
            //// //    Directory.CreateDirectory(tmpPath);
            //// //}






            ////doc.Pages.Add(new PdfPage());

            ////XImage img = XImage.FromFile(fullPath);
            ////xgr.DrawImage(bmap, 0, 0);
            ////xgr.Dispose();
            #endregion

        }



        //private void bw_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    try
        //    {
        //        string source = (e.Argument as string[])[0];
        //        string destinaton = (e.Argument as string[])[1];

        //        PdfDocument doc = new PdfDocument();
        //        doc.Pages.Add(new PdfPage());
        //        XGraphics xgr = XGraphics.FromPdfPage(doc.Pages[0]);
        //        XImage img = XImage.FromFile(source);

        //        xgr.DrawImage(img, 0, 0);
        //        doc.Save(destinaton);
        //        doc.Close();

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


        public void ClearImage()
        {
            _currentBitmap = new Bitmap(1, 1);
        }

        public void RestorePrevious()
        {
            _bitmapbeforeProcessing = _currentBitmap;
        }

        public void SetColorFilter(ColorFilterTypes colorFilterType)
        {
            Bitmap temp = (Bitmap)_currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    int nPixelR = 0;
                    int nPixelG = 0;
                    int nPixelB = 0;
                    if (colorFilterType == ColorFilterTypes.Red)
                    {
                        nPixelR = c.R;
                        nPixelG = c.G - 255;
                        nPixelB = c.B - 255;
                    }
                    else if (colorFilterType == ColorFilterTypes.Green)
                    {
                        nPixelR = c.R - 255;
                        nPixelG = c.G;
                        nPixelB = c.B - 255;
                    }
                    else if (colorFilterType == ColorFilterTypes.Blue)
                    {
                        nPixelR = c.R - 255;
                        nPixelG = c.G - 255;
                        nPixelB = c.B;
                    }

                    nPixelR = Math.Max(nPixelR, 0);
                    nPixelR = Math.Min(255, nPixelR);

                    nPixelG = Math.Max(nPixelG, 0);
                    nPixelG = Math.Min(255, nPixelG);

                    nPixelB = Math.Max(nPixelB, 0);
                    nPixelB = Math.Min(255, nPixelB);

                    bmap.SetPixel(i, j, Color.FromArgb((byte)nPixelR, (byte)nPixelG, (byte)nPixelB));
                }
            }
            _currentBitmap = (Bitmap)bmap.Clone();
        }

        public void SetGamma(double red, double green, double blue)
        {
            Bitmap temp = (Bitmap)_currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            Color c;
            byte[] redGamma = CreateGammaArray(red);
            byte[] greenGamma = CreateGammaArray(green);
            byte[] blueGamma = CreateGammaArray(blue);
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    bmap.SetPixel(i, j, Color.FromArgb(redGamma[c.R], greenGamma[c.G], blueGamma[c.B]));
                }
            }
            _currentBitmap = (Bitmap)bmap.Clone();
        }

        private byte[] CreateGammaArray(double color)
        {
            byte[] gammaArray = new byte[256];
            for (int i = 0; i < 256; ++i)
            {
                gammaArray[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / color)) + 0.5));
            }
            return gammaArray;
        }

        public void SetBrightness(int brightness)
        {
            Bitmap temp = (Bitmap)_currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            if (brightness < -255) brightness = -255;
            if (brightness > 255) brightness = 255;
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    int cR = c.R + brightness;
                    int cG = c.G + brightness;
                    int cB = c.B + brightness;

                    if (cR < 0) cR = 1;
                    if (cR > 255) cR = 255;

                    if (cG < 0) cG = 1;
                    if (cG > 255) cG = 255;

                    if (cB < 0) cB = 1;
                    if (cB > 255) cB = 255;

                    bmap.SetPixel(i, j, Color.FromArgb((byte)cR, (byte)cG, (byte)cB));
                }
            }
            _currentBitmap = (Bitmap)bmap.Clone();
        }

        public void SetContrast(double contrast)
        {
            Bitmap temp = (Bitmap)_currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone(new Rectangle(0, 0, temp.Width, temp.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            if (contrast < -100) contrast = -100;
            if (contrast > 100) contrast = 100;
            contrast = (100.0 + contrast) / 100.0;
            contrast *= contrast;
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    double pR = c.R / 255.0;
                    pR -= 0.5;
                    pR *= contrast;
                    pR += 0.5;
                    pR *= 255;
                    if (pR < 0) pR = 0;
                    if (pR > 255) pR = 255;

                    double pG = c.G / 255.0;
                    pG -= 0.5;
                    pG *= contrast;
                    pG += 0.5;
                    pG *= 255;
                    if (pG < 0) pG = 0;
                    if (pG > 255) pG = 255;

                    double pB = c.B / 255.0;
                    pB -= 0.5;
                    pB *= contrast;
                    pB += 0.5;
                    pB *= 255;
                    if (pB < 0) pB = 0;
                    if (pB > 255) pB = 255;

                    bmap.SetPixel(i, j, Color.FromArgb((byte)pR, (byte)pG, (byte)pB));
                }
            }
            _currentBitmap = (Bitmap)bmap.Clone();
        }

        public void SetGrayscale()
        {
            Bitmap temp = (Bitmap)_currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone(new Rectangle(0, 0, temp.Width, temp.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    byte gray = (byte)(.299 * c.R + .587 * c.G + .114 * c.B);

                    bmap.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            _currentBitmap = (Bitmap)bmap.Clone();
        }

        public void SetInvert()
        {
            Bitmap temp = (Bitmap)_currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    bmap.SetPixel(i, j, Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B));
                }
            }
            _currentBitmap = (Bitmap)bmap.Clone();
        }

        public void Resize(int newWidth, int newHeight)
        {
            if (newWidth != 0 && newHeight != 0)
            {
                Bitmap temp = (Bitmap)_currentBitmap.Clone();
                Bitmap bmap = new Bitmap(newWidth, newHeight, temp.PixelFormat);//delo no pixel 
                double nWidthFactor = (double)temp.Width / (double)newWidth;
                double nHeightFactor = (double)temp.Height / (double)newHeight;

                double fx, fy, nx, ny;
                int cx, cy, fr_x, fr_y;
                Color color1 = new Color();
                Color color2 = new Color();
                Color color3 = new Color();
                Color color4 = new Color();
                byte nRed, nGreen, nBlue;

                byte bp1, bp2;

                for (int x = 0; x < bmap.Width; ++x)
                {
                    for (int y = 0; y < bmap.Height; ++y)
                    {

                        fr_x = (int)Math.Floor(x * nWidthFactor);
                        fr_y = (int)Math.Floor(y * nHeightFactor);
                        cx = fr_x + 1;
                        if (cx >= temp.Width) cx = fr_x;
                        cy = fr_y + 1;
                        if (cy >= temp.Height) cy = fr_y;
                        fx = x * nWidthFactor - fr_x;
                        fy = y * nHeightFactor - fr_y;
                        nx = 1.0 - fx;
                        ny = 1.0 - fy;

                        color1 = temp.GetPixel(fr_x, fr_y);
                        color2 = temp.GetPixel(cx, fr_y);
                        color3 = temp.GetPixel(fr_x, cy);
                        color4 = temp.GetPixel(cx, cy);

                        // Blue
                        bp1 = (byte)(nx * color1.B + fx * color2.B);

                        bp2 = (byte)(nx * color3.B + fx * color4.B);

                        nBlue = (byte)(ny * (double)(bp1) + fy * (double)(bp2));

                        // Green
                        bp1 = (byte)(nx * color1.G + fx * color2.G);

                        bp2 = (byte)(nx * color3.G + fx * color4.G);

                        nGreen = (byte)(ny * (double)(bp1) + fy * (double)(bp2));

                        // Red
                        bp1 = (byte)(nx * color1.R + fx * color2.R);

                        bp2 = (byte)(nx * color3.R + fx * color4.R);

                        nRed = (byte)(ny * (double)(bp1) + fy * (double)(bp2));

                        bmap.SetPixel(x, y, System.Drawing.Color.FromArgb(255, nRed, nGreen, nBlue));
                    }
                }
                _currentBitmap = (Bitmap)bmap.Clone();
            }
        }

        public void RotateFlip(RotateFlipType rotateFlipType)
        {
            Bitmap temp = (Bitmap)_currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            bmap.RotateFlip(rotateFlipType);
            _currentBitmap = (Bitmap)bmap.Clone();
        }

        public void Crop(int xPosition, int yPosition, int width, int height)
        {
            Bitmap temp = (Bitmap)_currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            if (xPosition + width > _currentBitmap.Width)
                width = _currentBitmap.Width - xPosition;
            if (yPosition + height > _currentBitmap.Height)
                height = _currentBitmap.Height - yPosition;
            Rectangle rect = new Rectangle(xPosition, yPosition, width, height);
            _currentBitmap = (Bitmap)bmap.Clone(rect, bmap.PixelFormat);
        }

        public void DrawOutCropArea(int xPosition, int yPosition, int width, int height)
        {
            _bitmapPrevCropArea = (Bitmap)_currentBitmap;
            Bitmap bmap = (Bitmap)_bitmapPrevCropArea.Clone();
            Graphics gr = Graphics.FromImage(bmap);
            Brush cBrush = new Pen(Color.FromArgb(150, Color.White)).Brush;
            Rectangle rect1 = new Rectangle(0, 0, _currentBitmap.Width, yPosition);
            Rectangle rect2 = new Rectangle(0, yPosition, xPosition, height);
            Rectangle rect3 = new Rectangle(0, (yPosition + height), _currentBitmap.Width, _currentBitmap.Height);
            Rectangle rect4 = new Rectangle((xPosition + width), yPosition, (_currentBitmap.Width - xPosition - width), height);
            gr.FillRectangle(cBrush, rect1);
            gr.FillRectangle(cBrush, rect2);
            gr.FillRectangle(cBrush, rect3);
            gr.FillRectangle(cBrush, rect4);
            _currentBitmap = (Bitmap)bmap.Clone();
        }

        public void RemoveCropAreaDraw()
        {
            _currentBitmap = (Bitmap)_bitmapPrevCropArea.Clone();
        }

        public void InsertText(string text, int xPosition, int yPosition, string fontName, float fontSize, string fontStyle, string colorName1, string colorName2)
        {

            Bitmap temp = (Bitmap)_currentBitmap;
            PixelFormat cumacekpixel = temp.PixelFormat;
            Bitmap bmap = (Bitmap)temp.Clone(new Rectangle(0, 0, temp.Width, temp.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            Graphics gr = Graphics.FromImage(bmap);


            if (string.IsNullOrEmpty(fontName))
                fontName = "Times New Roman";
            if (fontSize.Equals(null))
                fontSize = 10.0F;
            Font font = new Font(fontName, fontSize);
            if (!string.IsNullOrEmpty(fontStyle))
            {
                FontStyle fStyle = FontStyle.Regular;
                switch (fontStyle.ToLower())
                {
                    case "bold":
                        fStyle = FontStyle.Bold;
                        break;
                    case "italic":
                        fStyle = FontStyle.Italic;
                        break;
                    case "underline":
                        fStyle = FontStyle.Underline;
                        break;
                    case "strikeout":
                        fStyle = FontStyle.Strikeout;
                        break;

                }
                font = new Font(fontName, fontSize, fStyle);
            }
            if (string.IsNullOrEmpty(colorName1))
                colorName1 = "Black";
            if (string.IsNullOrEmpty(colorName2))
                colorName2 = colorName1;
            Color color1 = Color.FromName(colorName1);
            Color color2 = Color.FromName(colorName2);
            int gW = (int)(text.Length * fontSize);
            gW = gW == 0 ? 10 : gW;
            LinearGradientBrush LGBrush = new LinearGradientBrush(new Rectangle(0, 0, gW, (int)fontSize), color1, color2, LinearGradientMode.Vertical);

            gr.DrawString(text, font, LGBrush, xPosition, yPosition);




            _currentBitmap = bmap.Clone(new Rectangle(0, 0, bmap.Width, bmap.Height), PixelFormat.Format24bppRgb);



        }

        public void InsertImage(string imagePath, int xPosition, int yPosition)
        {
            Bitmap temp = (Bitmap)_currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone(new Rectangle(0, 0, temp.Width, temp.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Graphics gr = Graphics.FromImage(bmap);
            if (!string.IsNullOrEmpty(imagePath))
            {
                Bitmap i_bitmap = (Bitmap)Bitmap.FromFile(imagePath);
                Rectangle rect = new Rectangle(xPosition, yPosition, i_bitmap.Width, i_bitmap.Height);
                gr.DrawImage(Bitmap.FromFile(imagePath), rect);
            }
            _currentBitmap = (Bitmap)bmap.Clone();
        }

        public void InsertShape(string shapeType, int xPosition, int yPosition, int width, int height, string colorName)
        {
            Bitmap temp = (Bitmap)_currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone(new Rectangle(0, 0, temp.Width, temp.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Graphics gr = Graphics.FromImage(bmap);
            if (string.IsNullOrEmpty(colorName))
                colorName = "Black";
            Pen pen = new Pen(Color.FromName(colorName));
            switch (shapeType.ToLower())
            {
                case "filledellipse":
                    gr.FillEllipse(pen.Brush, xPosition, yPosition, width, height);
                    break;
                case "filledrectangle":
                    gr.FillRectangle(pen.Brush, xPosition, yPosition, width, height);
                    break;
                case "ellipse":
                    gr.DrawEllipse(pen, xPosition, yPosition, width, height);
                    break;
                case "rectangle":
                default:
                    gr.DrawRectangle(pen, xPosition, yPosition, width, height);
                    break;

            }
            _currentBitmap = (Bitmap)bmap.Clone();
        }

        public void myPrintDocument2_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            Bitmap temp = (Bitmap)_currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone(new Rectangle(0, 0, temp.Width, temp.Height), PixelFormat.Format24bppRgb);

            e.Graphics.DrawImage(bmap, 0, 0, bmap.Width, bmap.Height);
            bmap.Dispose();
        }



    }
}
