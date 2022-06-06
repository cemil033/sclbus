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
    /// Interaction logic for Students.xaml
    /// </summary>
    public partial class Students : Page
    {
        SbDbContext context = SbDbContext.GetInstace();
        List<Student> students=new();
        public Students()
        {
            InitializeComponent();
            students=context.Students.Include("Parent").Include("Group").ToList();
            lst_std.ItemsSource=students;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddStudent addStudent = new AddStudent();
            addStudent.ShowDialog();
            if (addStudent.DialogResult == true)
            {
                context.Students.Add(new Student()
                {
                    FirstName = addStudent.tb_fn.Text,
                    LastName = addStudent.tb_ln.Text,
                    Home = addStudent.tb_ha.Text,
                    HomeDescription = addStudent.tb_ha.Text,
                    OtherAddress = addStudent.tb_oa.Text,
                    OtherAddressDescription = addStudent.tb_oa.Text,
                    Parent = (addStudent.cb_prnt.SelectedItem as Parent),
                    ParentId = (addStudent.cb_prnt.SelectedItem as Parent).Id
                });
            }
            context.SaveChanges();
            students = context.Students.Include("Parent").Include("Group").ToList();
            lst_std.ItemsSource = students;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddStudent addStudent=new AddStudent();
            addStudent.tb_fn.Text = (lst_std.SelectedItem as Student).FirstName;
            addStudent.tb_ln.Text = (lst_std.SelectedItem as Student).LastName;
            addStudent.tb_ha.Text = (lst_std.SelectedItem as Student).Home;
            addStudent.tb_oa.Text = (lst_std.SelectedItem as Student).OtherAddress;
            foreach (var item in addStudent.cb_prnt.Items)
            {
                if((item as Parent).Id==(lst_std.SelectedItem as Student).Parent.Id)
                {
                    addStudent.cb_prnt.SelectedItem = item;
                }
            }
            addStudent.ShowDialog();
            if (addStudent.DialogResult == true)
            {
                foreach (var item in students)
                {
                    if(item.Id== (lst_std.SelectedItem as Student).Id)
                    {
                        item.FirstName = addStudent.tb_fn.Text;
                        item.LastName = addStudent.tb_ln.Text;
                        item.Home = addStudent.tb_ha.Text;
                        item.HomeDescription = addStudent.tb_ha.Text;
                        item.OtherAddress=addStudent.tb_oa.Text;
                        item.OtherAddressDescription=addStudent.tb_oa.Text;
                        item.Parent=context.Parents.FirstOrDefault(testc=>testc.Id==(addStudent.cb_prnt.SelectedItem as Parent).Id);
                        item.ParentId=item.Parent.Id;
                        context.Students.Update(item);
                    }
                }
                context.SaveChanges();
                students = context.Students.Include("Parent").Include("Group").ToList();
                lst_std.ItemsSource = students;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var std = context.Students.FirstOrDefault(t => t.Id == (lst_std.SelectedItem as Student).Id);
            context.Students.Remove(std);
            context.SaveChanges();
            students = context.Students.Include("Parent").Include("Group").ToList();
            lst_std.ItemsSource = students;
        }
    }
}
