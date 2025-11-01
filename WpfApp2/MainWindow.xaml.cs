using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = txtName.Text;
            string pass = txtPass.Password;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
            {
                eroroo.Content = "erorr";
            }
            else
            {
                using (var context = new db_taskManegment())
                {
                    var hi = context.User.FirstOrDefault(u => u.Email == email && u.Password == pass);
                    if (hi != null)
                    {
                        if (hi.Role == "Employee")
                        {
                            Employee emp = new Employee();
                            emp.Show();
                            this.Close();
                        }
                        else if (hi.Role == "Manager")
                        {
                            ManagerPage manager = new ManagerPage();
                            manager.Show();
                            this.Close();
                        }
                    }
                    else eroroo.Content = "wrong email and password_-_-_-_-";

                }
            }
        }
    }
}