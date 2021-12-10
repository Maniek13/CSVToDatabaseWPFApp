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
                LoadData.IsEnabled = false;

                Grid.Visibility = Visibility.Hidden;
                EditBtn.Visibility = Visibility.Hidden;
                Loading.Visibility = Visibility.Visible;
                

          
                if (openFileDialog.ShowDialog() == true)
                {
                    _fileName = openFileDialog.FileName;
                }

                if(_fileName == null)
                {
                    LoadCsv.IsEnabled = true;
                    LoadData.IsEnabled = true;

                    if(_employees != null)
                    {
                        Grid.Visibility = Visibility.Visible;
                        EditBtn.Visibility = Visibility.Visible;
                    }
                
                    Loading.Visibility = Visibility.Hidden;
                    return;
                }

                Thread thread = new Thread(CSVLoad)
                {
                    IsBackground = true
                };

                thread.Start();

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

                _fileName = null;
                Loading.Visibility = Visibility.Hidden;
                LoadCsv.IsEnabled = true;
                LoadData.IsEnabled = true;
                LoadCsv.Content = "Load next one";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void LoadFromData(object sender, RoutedEventArgs evt)
        {
            try
            {
                LoadCsv.IsEnabled = false;
                LoadData.IsEnabled = false;
                Grid.Visibility = Visibility.Hidden;
                EditBtn.Visibility = Visibility.Hidden;
                Loading.Visibility = Visibility.Visible;

                Thread thread = new Thread(DataLoad)
                {
                    IsBackground = true
                };

                thread.Start();

                await Task.Run(() =>
                {
                    while (!_loaded)
                    {
                        DelayAction(500);
                    }

                    _loaded = false;
                });

                if (_employees.Count > 0)
                {
                    Employes.ItemsSource = _employees;

                    Grid.Visibility = Visibility.Visible;
                    EditBtn.Visibility = Visibility.Visible;
                }

                Loading.Visibility = Visibility.Hidden;
                LoadCsv.IsEnabled = true;
                LoadData.IsEnabled = true;
                LoadCsv.Content = "Load next one";

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

        private async void CSVLoad()
        {
            _employees = await IndexViewModel.LoadFromCsv(_fileName);
            _loaded = true;
        }

        private async void DataLoad()
        {
            _employees = await IndexViewModel.LoadFromData();
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
