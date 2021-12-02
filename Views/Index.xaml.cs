using CSV.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using CSV.Models;
using Microsoft.Win32;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Controls;

namespace CSV.Views
{
    public partial class Index : Window
    {
        private List<Employee> _employees;
        private string _fileName;
        private bool _loaded = false;
        public Index()
        {
            InitializeComponent();
        }

        private async void LoadCSV(object sender, RoutedEventArgs evt)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                LoadCsv.IsEnabled = false;
                Grid.Visibility = Visibility.Hidden;
                EditBtn.Visibility = Visibility.Hidden;
                Loading.Visibility = Visibility.Visible;
                EditBtn.IsEnabled = false;

          
                if (openFileDialog.ShowDialog() == true)
                {
                    _fileName = openFileDialog.FileName;

                    Thread thread = new Thread(Load)
                    {
                        IsBackground = true
                    };

                    thread.Start();
                }

                await Task.Run(() =>
                {
                    while (!_loaded)
                    {
                        DelayAction(500);
                    }

                    _loaded = false;
                });

                if(_employees.Count > 0)
                {
                    Employes.ItemsSource = _employees;

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

        private static void DelayAction(int millisecond)
        {
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(millisecond);
            timer.Start();
        }

        private async void Load()
        {
            _employees = await IndexViewModel.LoadFromCsv(_fileName);
            _loaded = true;
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

        private void selectChange(object sender, SelectionChangedEventArgs evt)
        {
            EditBtn.IsEnabled = true;
        }
    }
}
