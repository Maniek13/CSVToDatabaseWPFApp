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
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void LoadCSV(object sender, RoutedEventArgs evt)
        {
            try
            {
                List<Employee> employes = new List<Employee>();

                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    using (TextFieldParser csvParser = new TextFieldParser(openFileDialog.FileName))
                    {

                        csvParser.CommentTokens = new string[] { "#" };
                        csvParser.SetDelimiters(new string[] { "," });
                        csvParser.HasFieldsEnclosedInQuotes = true;
                        csvParser.ReadLine();

                        while (!csvParser.EndOfData)
                        {
                            string[] fields = csvParser.ReadFields();
                            Employee emp = new Employee
                            {
                                ID = Convert.ToInt32(fields[0]),
                                Name = fields[1],
                                Surname = fields[2],
                                Email = fields[3],
                                Phone = Convert.ToInt64(fields[4])
                            };
                            employes.Add(emp);
                        }
                    }
                }

                EmployesController empoCtrl = new EmployesController(new EmployeeContext());
                empoCtrl.SetManyInDb(employes);
                Employes.ItemsSource = employes;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void EditRow(object sender, RoutedEventArgs e)
        {
            Edit edit = new Edit
            {
                DataContext = (Employee)Employes.SelectedItem
            };
            edit.Show();
        }
    }
}
