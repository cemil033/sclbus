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
    /// Interaction logic for Drivers.xaml
    /// </summary>
    public partial class Drivers : Page
    {
        SbDbContext sbdbcontext = SbDbContext.GetInstace();
        List<Driver> drivers=new List<Driver>();
        public Drivers()
        {
            InitializeComponent();            
            drivers = sbdbcontext.Drivers.Include("Car").ToList();            
            lst_drv.ItemsSource = drivers;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            AddDriver addDriver = new AddDriver();
            addDriver.ShowDialog();
            if (addDriver.DialogResult == true)
            {
                Car? car = (addDriver.cb_cars.SelectedItem as Car);
                Driver driver1;
                if (car != null) {
                    driver1 = new Driver()
                    {
                        FirstName = addDriver.tb_fn.Text,
                        LastName = addDriver.tb_ln.Text,
                        Phone = addDriver.tb_phn.Text,
                        UserName = addDriver.tb_un.Text,
                        Password = addDriver.tb_psw.Text,
                        License = addDriver.tb_ls.Text,
                        CarId=car.Id,                        
                        Car =  car ,
                        Cars=new List<Car>() { car}
                    };
                    
                    sbdbcontext.Drivers.Add(driver1);
                    sbdbcontext.Cars.Update(car);
                }
                else
                {
                    driver1 = new Driver()
                    {
                        FirstName = addDriver.tb_fn.Text,
                        LastName = addDriver.tb_ln.Text,
                        Phone = addDriver.tb_phn.Text,
                        UserName = addDriver.tb_un.Text,
                        Password = addDriver.tb_psw.Text,
                        License = addDriver.tb_ls.Text                        
                    };
                    
                    sbdbcontext.Drivers.Add(driver1);
                }                
                sbdbcontext.SaveChanges();
                drivers = sbdbcontext.Drivers.Include("Car").ToList();
                lst_drv.ItemsSource = drivers;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (var driver in lst_drv.Items)
            {
                if((driver as Driver).FirstName.Contains(tb_src.Text)){
                    lst_drv.SelectedItem = driver;
                }
            }
        }

        private void btn_upd_Click(object sender, RoutedEventArgs e)
        {            
            var adddriv = new AddDriver();
            adddriv.tb_fn.Text = (lst_drv.SelectedItem as Driver).FirstName;
            adddriv.tb_ln.Text = (lst_drv.SelectedItem as Driver).LastName;
            adddriv.tb_phn.Text = (lst_drv.SelectedItem as Driver).Phone;
            adddriv.tb_un.Text = (lst_drv.SelectedItem as Driver).UserName;
            adddriv.tb_psw.Text = (lst_drv.SelectedItem as Driver).Password;
            adddriv.tb_ls.Text = (lst_drv.SelectedItem as Driver).License;
            foreach (var item in adddriv.cb_cars.Items)
            {
                if((item as Car).Id== (lst_drv.SelectedItem as Driver).CarId)
                {
                    adddriv.cb_cars.SelectedItem = item;
                }
            }
            adddriv.ShowDialog();
            if (adddriv.DialogResult ==true)
            {
                Car? car = (adddriv.cb_cars.SelectedItem as Car);
                Driver driver1;

                if (car != null)
                {
                    var context = SbDbContext.GetInstace();
                    foreach (var item in drivers)
                    {
                        if((lst_drv.SelectedItem as Driver).Id == item.Id)
                        {
                            item.FirstName=adddriv.tb_fn.Text;
                            item.LastName=adddriv.tb_ln.Text;
                            item.Phone=adddriv.tb_phn.Text;
                            item.UserName = adddriv.tb_un.Text;
                            item.Password=adddriv.tb_psw.Text;
                            item.License=adddriv.tb_ls.Text;
                            item.Car=car;
                            item.CarId=car.Id;
                            context.Drivers.Update(item);
                        }
                    }
                    context.Cars.Update(car);
                    context.SaveChanges();
                }
                else
                {
                    driver1 = new Driver()
                    {
                        Id = (lst_drv.SelectedItem as Driver).Id,
                        FirstName = adddriv.tb_fn.Text,
                        LastName = adddriv.tb_ln.Text,
                        Phone = adddriv.tb_phn.Text,
                        UserName = adddriv.tb_un.Text,
                        Password = adddriv.tb_psw.Text,
                        License = adddriv.tb_ls.Text
                    };

                    sbdbcontext.Drivers.Update(driver1);
                }
                sbdbcontext.SaveChanges();
                drivers = sbdbcontext.Drivers.Include("Car").ToList();
                lst_drv.ItemsSource = drivers;
            }
        }

        private void btn_dlt_Click(object sender, RoutedEventArgs e)
        {
            var context = SbDbContext.GetInstace();
            var driver = context.Drivers.FirstOrDefault(x=>x.Id.Equals((lst_drv.SelectedItem as Driver).Id));
            context.Drivers.Remove(driver);
            context.SaveChanges();
            drivers = sbdbcontext.Drivers.Include("Car").ToList();
            lst_drv.ItemsSource = drivers;
        }

        
    }
}
