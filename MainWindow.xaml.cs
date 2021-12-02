using CSV.Controllers;
using CSV.Data;
using CSV.Models;
using CSV.ViewModels;
using CSV.Views;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;

namespace CSV
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.Hide();
            Index index = new Index();
            index.Show();
        }
    }
}
