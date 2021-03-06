﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using WIATest;

namespace Adibrata.Windows.UserController.UCScan
{
    /// <summary>
    /// Interaction logic for UCScanMain.xaml
    /// </summary>
    public partial class UCScanMain : UserControl
    {
        public UCScanMain()
        {
            InitializeComponent();
        }

        private byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        private System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }
        BitmapSource LoadImage(Byte[] imageData)
        {
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                var decoder = BitmapDecoder.Create(ms,
                    BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                return decoder.Frames[0];
            }
        }
        private void btnScan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //get list of devices available
                List<string> devices = WIAScanner.GetDevices();

                foreach (string device in devices)
                {
                    lbDevices.Items.Add(device);
                }
                //check if device is not available
                if (lbDevices.Items.Count == 0)
                {
                    MessageBox.Show("You do not have any WIA devices.");
                }
                else
                {
                    lbDevices.SelectedIndex = 0;
                }
                //get images from scanner
                string path = "C:\\Temp\\"; // your code goes here

                bool exists = System.IO.Directory.Exists(path);

                if (!exists)
                    System.IO.Directory.CreateDirectory(path);
                List<System.Drawing.Image> images = WIAScanner.Scan((string)lbDevices.SelectedItem);
                foreach (System.Drawing.Image img in images)
                {
                    byte[] byteImg = imageToByteArray(img);
                    picScan.Source = LoadImage(byteImg);
                    
         
                    img.Save(path + DateTime.Now.ToString("yyyy-MM-dd HHmmss") + ".jpeg");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
