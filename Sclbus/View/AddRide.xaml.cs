using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for AddRide.xaml
    /// </summary>
    public partial class AddRide : Page
    {
        SbDbContext context =SbDbContext.GetInstace();
        List<Driver> drivers = new List<Driver>();
        List<Student> students = new List<Student>();
        Ride Ride;
        public AddRide()
        {
            InitializeComponent();            
            Ride = new Ride();            
            drivers=context.Drivers.Include("Car").ToList();
            cb_drv.ItemsSource = drivers;            
            students=context.Students.Include("Parent").Include("Group").ToList();
            lst_noadd.ItemsSource = students.Where(t=>t.Ride==null);
        }

        private void cb_drv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_drv.SelectedItem != null)
            {
                tb_dfn.Text=(cb_drv.SelectedItem as Driver).ToString();
                tb_cn.Text = (cb_drv.SelectedItem as Driver).Car?.Title;
                tb_cnmb.Text = (cb_drv.SelectedItem as Driver).Car?.Number;
                tb_ms.Text= (cb_drv.SelectedItem as Driver).Car.SeatCount.ToString();
                tb_st.Text=Ride.Students.Count.ToString();
                Ride.Type = (cb_drv.SelectedItem as Driver).ToString();

            }
        }

        private void btn_dlt_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(tb_st.Text) < int.Parse(tb_ms.Text))
            {
                if (lst_noadd.SelectedItem != null)
                {
                    var std = (lst_noadd.SelectedItem as Student);
                    std.RideId = Ride.Id;
                    std.Ride = Ride;
                    context.Students.Update(std);
                    students = context.Students.Include("Parent").Include("Group").ToList();
                    lst_noadd.ItemsSource = students.Where(t => t.Ride == null);
                    lst_add.ItemsSource = students.Where(t => t.RideId == Ride.Id);
                    tb_st.Text = Ride.Students.Count.ToString();
                }
            }
            else
            {
                MessageBox.Show("Yer Yoxdur");
            }
        }

        private void btn_dlt_Click_1(object sender, RoutedEventArgs e)
        {

            if (lst_noadd.SelectedItem != null)
            {
                var std = (lst_add.SelectedItem as Student);
                std.Ride = null;
                std.RideId = null;
                context.Students.Update(std);
                students = context.Students.Include("Parent").Include("Group").ToList();
                lst_noadd.ItemsSource = students.Where(t => t.Ride == null);
                lst_add.ItemsSource = students.Where(t => t.RideId == Ride.Id);                
                tb_st.Text = Ride.Students.Count.ToString();
            }
            
        }

        private void btn_creat_Click(object sender, RoutedEventArgs e)
        {
            Ride.Driver=(cb_drv.SelectedItem as Driver);
            Ride.DriverId = (cb_drv.SelectedItem as Driver).Id;            
            context.Rides.Add(Ride);
            context.SaveChanges();
            MessageBox.Show("Created!");
        }
    }
}
