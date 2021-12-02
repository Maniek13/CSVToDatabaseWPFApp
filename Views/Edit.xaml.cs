using CSV.Models;
using CSV.ViewModels;
using System;
using System.Windows;

namespace CSV.Views
{
    public partial class Edit : Window
    {
        public Edit()
        {
            InitializeComponent();
        }

        public void EditEmployee(object sender, RoutedEventArgs evt)
        {
            try
            {
                EditViewModel.Edit((Employee)this.DataContext);
                this.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
