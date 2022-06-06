using Sclbus.Models;
using System;
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
using System.Windows.Shapes;

namespace Sclbus
{
    /// <summary>
    /// Interaction logic for AddDriver.xaml
    /// </summary>
    public partial class AddDriver : Window
    {
        SbDbContext context = SbDbContext.GetInstace();
        List<Car> cars = new List<Car>();
        public AddDriver()
        {
            InitializeComponent();            
            cars=context.Cars.ToList();
            cb_cars.ItemsSource = cars;
            cb_cars.DisplayMemberPath = "Title";
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
