using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
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
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employee : Window
    {
        public Employee()
        {
            InitializeComponent();
            loudAll();
        }

        private void loudAll()
        {
            using (var context = new db_taskManegment())
            {
                var pinding = context.Tasks.Where(n => n.Status == "Progress" || n.Status == "Pending").Select(x=> new
                {
                    id = x.TaskID,
                    TaskName = x.Title,
                    TaskDescription = x.Description,
                    Status = x.Status,
                }).ToList();
                pindingCompleatedDatagrid.ItemsSource = pinding;

                var comp = context.Tasks.Where(n => n.Status == "Completed").Select(x => new
                {
                    id = x.TaskID,
                    TaskName = x.Title,
                    TaskDescription = x.Description,
                    Status = x.Status,
                }).ToList();

                CompleatedDatagrid.ItemsSource = comp;
            }
        }

        private void pindingCompleatedDatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTask = pindingCompleatedDatagrid.SelectedItem;

            if (selectedTask != null)
            {
                dynamic hiii = selectedTask;
                txtID.Text = hiii.id.ToString(); 

                string currentStatus = hiii.Status.ToString();

                var matchingItem = comboooo.Items
                    .OfType<ComboBoxItem>()
                    .FirstOrDefault(item => item.Content.ToString() == currentStatus);

                if (matchingItem != null)
                {
                    comboooo.SelectedItem = matchingItem;
                }
                else MessageBox.Show("eororrr");
            }
        }

        private void CompleatedDatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var yesss = CompleatedDatagrid.SelectedItem;
            if (yesss != null)
            {
                dynamic nooo = yesss;
                txtID.Text = nooo.id.ToString();

                string cuur = nooo.Status.ToString();

                var match = comboooo.Items.OfType<ComboBoxItem>().FirstOrDefault(i => i.Content.ToString() == cuur);
                if (match != null)
                {
                    comboooo.SelectedItem = match;
                }
                else MessageBox.Show("eororrr");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("selecte first");
                return;
            }

            if (!int.TryParse(txtID.Text, out int iddd))
            {
                MessageBox.Show("Errorrrr", "Error");
                return;
            }
            using (var context = new db_taskManegment())
            {
                var shit = context.Tasks.FirstOrDefault(u=> u.TaskID == iddd);
                if (shit != null)
                {
                    if (comboooo.SelectedItem is ComboBoxItem hi)
                    {
                        shit.Status = hi.Content.ToString();
                        context.SaveChanges();
                        loudAll();
                    }
                    else MessageBox.Show("nooooo");
                }
                
            }
        }
    }
}
