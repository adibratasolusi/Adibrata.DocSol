﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Adibrata.Windows.UserControler
{
    /// <summary>
    /// Interaction logic for Address.xaml
    /// </summary>
    public partial class UCAddress : UserControl
    {
    //    public string Address
    //    {
    //        get { return }
    //        set; }
        public string PasswordValue { get; set; }
        public Boolean IsValid { get; set; }

        public UCAddress()
        {
            InitializeComponent();
        }
    }
}
