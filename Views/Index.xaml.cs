using CSV.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using CSV.Models;
using Microsoft.Win32;

namespace CSV.Views
{
    public partial class Index : Window
    {
        public Index()
        {
            InitializeComponent();
        }

        private async void LoadCSV(object sender, RoutedEventArgs evt)
        {
            try
            {
                List<Employee> employes = new List<Employee>();
                OpenFileDialog openFileDialog = new OpenFileDialog();

                LoadCsv.IsEnabled = false;
                Grid.Visibility = Visibility.Hidden;
                EditBtn.Visibility = Visibility.Hidden;
                Loading.Visibility = Visibility.Visible;

                if (openFileDialog.ShowDialog() == true)
                {
                    employes = IndexViewModel.LoadFromCsv(openFileDialog.FileName);
                }

                if(employes.Count > 0)
                {
                    Employes.ItemsSource = employes;

                    Grid.Visibility = Visibility.Visible;
                    EditBtn.Visibility = Visibility.Visible;
                }

                Loading.Visibility = Visibility.Hidden;
                LoadCsv.IsEnabled = true;
                LoadCsv.Content = "Load next";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EditRow(object sender, RoutedEventArgs e)
        {
            try
            {
                Employee emp = (Employee)Employes.SelectedItem;

                if (emp != null)
                {
                    Edit edit = new Edit
                    {
                        DataContext = emp
                    };

                    edit.Show();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
