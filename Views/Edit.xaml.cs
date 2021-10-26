using CSV.Controllers;
using CSV.Data;
using CSV.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace CSV.Views
{
    /// <summary>
    /// Logika interakcji dla klasy Edit.xaml
    /// </summary>
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
                EmployesController empoCtrl = new EmployesController(new EmployeeContext());
                empoCtrl.UpdateOne((Employee)this.DataContext);
                this.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
