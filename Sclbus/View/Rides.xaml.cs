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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sclbus.View
{
    /// <summary>
    /// Interaction logic for Rides.xaml
    /// </summary>
    public partial class Rides : Page
    {
        SbDbContext context=SbDbContext.GetInstace();
        List<Ride> rides = new List<Ride>();
        public Rides()
        {
            InitializeComponent();
            rides = context.Rides.ToList();
            lwcr.ItemsSource = rides;
        }

        private void btn_dlt_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in rides)
            {
                if(item.Id==(lwcr.SelectedItem as Ride).Id)
                {
                    context.Rides.Remove(item);
                }
            }
            context.SaveChanges();
            rides = context.Rides.ToList();
            lwcr.ItemsSource = rides;
        }
    }
}
