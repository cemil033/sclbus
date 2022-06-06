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
    /// Interaction logic for Group.xaml
    /// </summary>
    public partial class Groups : Page
    {
        SbDbContext context = SbDbContext.GetInstace();
        List<Group> groups = new ();
        public Groups()
        {
            InitializeComponent();
            groups=context.Groups.ToList();
            lst_drv.ItemsSource = groups;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddGroup addGroup = new AddGroup();
            addGroup.ShowDialog();
            if (addGroup.DialogResult == true)
            {
                context.Groups.Add(new Group() { Title = addGroup.tb_name.Text });
            }
            context.SaveChanges();
            groups = context.Groups.ToList();
            lst_drv.ItemsSource = groups;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (var item in lst_drv.Items)
            {
                if((item as Group).Title.Contains(tb_src.Text))
                {
                    lst_drv.SelectedItem = item;
                }
            }
        }

        private void btn_upd_Click(object sender, RoutedEventArgs e)
        {
            AddGroup addGroup = new AddGroup();
            addGroup.tb_name.Text = (lst_drv.SelectedItem as Group).Title;
            addGroup.ShowDialog();
            if(addGroup.DialogResult == true)
            {
                foreach(var item in groups)
                {
                    if(item.Id==(lst_drv.SelectedItem as Group).Id)
                    {
                        item.Title = addGroup.tb_name.Text;
                        context.Groups.Update(item);
                    }
                }
                context.SaveChanges();
                groups = context.Groups.ToList();
                lst_drv.ItemsSource = groups;
            }
        }

        private void btn_dlt_Click(object sender, RoutedEventArgs e)
        {
            var grp=context.Groups.FirstOrDefault(t=>t.Id==(lst_drv.SelectedItem as Group).Id);
            context.Groups.Remove(grp);
            context.SaveChanges();
            groups = context.Groups.ToList();
            lst_drv.ItemsSource = groups;
        }
    }
}
