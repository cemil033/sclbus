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
    /// Interaction logic for Holidays.xaml
    /// </summary>
    public partial class Holidays : Page
    {
        int frst=0;
        public Holidays()
        {
            InitializeComponent();
            var context = SbDbContext.GetInstace();            
            if (context.Holidays != null)
            {
                foreach (var item in context.Holidays)
                {
                    clndr.SelectedDates.Add((DateTime.Parse(item.Date.ToString())));
                }
            }
            frst = 1;
            
        }

        private void clndr_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (frst == 1)
            {
                var context = SbDbContext.GetInstace();
                context.Holidays.Add(new Holiday() { Date = clndr.SelectedDates.Last()});
                context.SaveChanges();
                var context1 = SbDbContext.GetInstace();
                foreach (var item in context1.Holidays)
                {
                    clndr.SelectedDates.Add((DateTime.Parse(item.Date.ToString())));
                }                
                
            }
        }
    }
}
