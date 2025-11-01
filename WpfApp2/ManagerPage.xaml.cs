using Microsoft.EntityFrameworkCore;
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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Window
    {
        public ManagerPage()
        {
            InitializeComponent();
            loudAll();
        }

        private void loudAll()
        {
            using (var context = new db_taskManegment())
            {
                var shit = context.Tasks
                    .Include(t => t.User)
                    .Select(x => new
                    {
                        EnpName = x.User.Name,
                        TaskID = x.TaskID,
                        Title = x.Title,
                        decription = x.Description,
                        statue = x.Status
                    })
                    .ToList();

                ManegerDatagrid.ItemsSource = shit ;
            }
        }

        private void ManegerDatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var shit = ManegerDatagrid.SelectedItem;
            if (shit != null)
            {
                dynamic hi = shit ;
                txtTaskTitle.Text = hi.Title;
                txtTaskDesc.Text = hi.decription;
                txtTaskID.Text = hi.TaskID.ToString();
                txtEmpName.Text = hi.EnpName;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTaskID.Text)|| string.IsNullOrEmpty(txtEmpName.Text)|| string.IsNullOrEmpty(txtTaskDesc.Text)|| string.IsNullOrEmpty(txtTaskTitle.Text))
            {
                MessageBox.Show("enter all values");
                return;
            }
            if (!int.TryParse(txtTaskID.Text,out int taskID))
            {
                MessageBox.Show("noo"); 
                return;
            }

            using (var context = new db_taskManegment())
            {
                var toUpdate = context.Tasks.FirstOrDefault(x => x.TaskID == taskID);
                if (toUpdate != null)
                {
                    toUpdate.Description = txtTaskID.Text;
                    toUpdate.Title = txtTaskTitle.Text;

                    if (comboooo.SelectedItem is ComboBoxItem hi)
                    {
                        toUpdate.Status = hi.Content.ToString();
                    }
                    else MessageBox.Show("nonono");

                    context.SaveChanges();
                    loudAll();
                }
                else MessageBox.Show("nonononononononony");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string taskTitle = txtTaskTitle.Text;
            string taskDesc = txtTaskDesc.Text;

            if (!int.TryParse(txtEmpName.Text , out int empID))
            {
                MessageBox.Show("yessss");
                return;
            }

            int empid = int.Parse(txtEmpName.Text);

            var newTask = new Tasks
            {
                Title = taskTitle,
                Description = taskDesc,
                Status = "Pending",
                UserID = empid,
            };

            using(var context = new db_taskManegment())
            {
                context.Tasks.Add(newTask);
                context.SaveChanges();
                loudAll();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTaskID.Text))
            {
                MessageBox.Show("choooose");
                return;
            }
            int taskID = int.Parse(txtTaskID.Text); 
            
            using (var context = new db_taskManegment())
            {
                var ji = context.Tasks.FirstOrDefault(u=>u.TaskID == taskID);
                context.Tasks.Remove(ji);
                context.SaveChanges();
                loudAll();
            }
        }
    }
}
