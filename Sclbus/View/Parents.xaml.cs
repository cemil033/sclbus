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
    /// Interaction logic for Parents.xaml
    /// </summary>
    public partial class Parents : Page
    {
        SbDbContext context=SbDbContext.GetInstace();
        List<Parent> parents=new();
        public Parents()
        {
            InitializeComponent();
            parents=context.Parents.ToList();
            lst_prn.ItemsSource = parents;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddParent addParent = new AddParent();
            addParent.ShowDialog();
            if (addParent.DialogResult == true)
            {
                context.Parents.Add(new Parent()
                {
                    FirstName = addParent.tb_fn.Text,
                    LastName = addParent.tb_ln.Text,
                    Phone = addParent.tb_phn.Text,
                    UserName = addParent.tb_un.Text,
                    Password = addParent.tb_psw.Text
                });
                context.SaveChanges();
            }
            parents = context.Parents.ToList();
            lst_prn.ItemsSource = parents;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddParent addParent=new AddParent();
            addParent.tb_fn.Text = (lst_prn.SelectedItem as Parent).FirstName;
            addParent.tb_ln.Text = (lst_prn.SelectedItem as Parent).LastName;
            addParent.tb_phn.Text = (lst_prn.SelectedItem as Parent).Phone;
            addParent.tb_un.Text = (lst_prn.SelectedItem as Parent).UserName;
            addParent.tb_psw.Text = (lst_prn.SelectedItem as Parent).Password;
            addParent.ShowDialog();
            if (addParent.DialogResult == true)
            {
                foreach (var item in parents)
                {
                    if(item.Id== (lst_prn.SelectedItem as Parent).Id)
                    {
                        item.FirstName= addParent.tb_fn.Text;
                        item.LastName= addParent.tb_ln.Text;
                        item.Phone=addParent.tb_phn.Text;
                        item.UserName=addParent.tb_un.Text;
                        item.Password=addParent.tb_psw.Text;
                        context.Parents.Update(item);
                    }
                }
                context.SaveChanges();
                parents = context.Parents.ToList();
                lst_prn.ItemsSource = parents;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var prnt =context.Parents.FirstOrDefault(t=>t.Id==(lst_prn.SelectedItem as Parent).Id);
            context.Parents.Remove(prnt);
            context.SaveChanges();
            parents = context.Parents.ToList();
            lst_prn.ItemsSource = parents;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            foreach(var item in lst_prn.Items)
            {
                if((item as Parent).FirstName.Contains(tb_src.Text))
                {
                    lst_prn.SelectedItem = item;
                }
            }
        }
    }
}
