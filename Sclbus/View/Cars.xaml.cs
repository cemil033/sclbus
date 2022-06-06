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
    /// Interaction logic for Cars.xaml
    /// </summary>
    public partial class Cars : Page
    {
        SbDbContext context = new SbDbContext();
        List<Car> cars =new();
       
        public Cars()
        {
            InitializeComponent();
            cars = context.Cars.Include("Driver").ToList();
            lwcr.ItemsSource = cars;         
                 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddCar addCar = new AddCar();
            addCar.ShowDialog();
            if (addCar.DialogResult == true)
            {
                
                context.Cars.Add(new Car() { Title=addCar.tb_name.Text,Number=addCar.tb_nm.Text,SeatCount=int.Parse(addCar.tb_scnt.Text)});
                context.SaveChanges();
                cars = context.Cars.Include("Driver").ToList();
                lwcr.ItemsSource = cars;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddCar addCar = new AddCar();
            addCar.tb_scnt.Text = (lwcr.SelectedItem as Car).SeatCount.ToString();
            addCar.tb_name.Text= (lwcr.SelectedItem as Car).Title.ToString();
            addCar.tb_nm.Text= (lwcr.SelectedItem as Car).Number.ToString();
            addCar.ShowDialog();
            if(addCar.DialogResult == true)
            {
                
                var crs = new Car()
                {
                    Id = (lwcr.SelectedItem as Car).Id,
                    Title = addCar.tb_name.Text,
                    Number = addCar.tb_nm.Text,
                    SeatCount = int.Parse(addCar.tb_scnt.Text)
                };
                foreach (var item in cars)
                {
                    if(item.Id== (lwcr.SelectedItem as Car).Id)
                    {
                        item.Title=addCar.tb_name.Text;
                        item.Number=addCar.tb_nm.Text;
                        item.SeatCount = int.Parse(addCar.tb_scnt.Text);                        
                        context.Update(item);
                        context.Update(item.Driver);
                    }
                }
                context.SaveChanges();
                cars = context.Cars.Include("Driver").ToList();
                lwcr.ItemsSource = cars;
            }
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {           
           
            var carss = context.Cars.FirstOrDefault(x => x.Id.Equals((lwcr.SelectedItem as Car).Id));
            context.Cars.Remove(carss);
            context.SaveChanges();
            cars = context.Cars.Include("Driver").ToList();
            lwcr.ItemsSource = cars;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            foreach (var car in lwcr.Items)
            {
                if((car as Car).Title.Contains(tb_src.Text))
                {
                    lwcr.SelectedItem = car;
                }
            }
        }
    }
}
