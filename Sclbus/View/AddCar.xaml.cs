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
    /// Interaction logic for AddCar.xaml
    /// </summary>
    public partial class AddCar : Window
    {
        public AddCar()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if((sender as TextBox).Text == "Name")
            //{
            //    (sender as TextBox).Text = "";
            //}
            //else if((sender as TextBox).Text == "")
            //{
            //    (sender as TextBox).Text = "Name";
            //}
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            //if ((sender as TextBox).Text == "Number")
            //{
            //    (sender as TextBox).Text = "";
            //}
            //else if ((sender as TextBox).Text == "")
            //{
            //    (sender as TextBox).Text = "Number";
            //}
        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {
            //if ((sender as TextBox).Text == "Seat Count")
            //{
            //    (sender as TextBox).Text = "";
            //}
            //else if ((sender as TextBox).Text == "")
            //{
            //    (sender as TextBox).Text = "Number";
            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
