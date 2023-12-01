using CinemaProject.Infrastructure;
using CinemaProject.Infrastructure.Consts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace CinemaProject.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void Button_Enter(object sender, RoutedEventArgs e)
        {
            string log = TextBoxLog.Text.Trim();
            string pas = TextBoxPass.Password.Trim();

            StringBuilder sb = new StringBuilder();
            if (log.Length<=1)
                sb.AppendLine("Поле логин введено не корректно!");
            if (pas.Length <=1)
                sb.AppendLine("Поле пароль введено не корректно!");

            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString());
                return;
            }
            else
            {
                Employee emp = null;
                using (Context db = new Context())
                {
                    emp = db.Employees.Where(b => b.Login==log && b.Password==pas).Include(x=>x.Post).FirstOrDefault();
                }

                if (emp != null) {
                    UserInfoConsts.RoleName = emp.Post.NamePost;
                    UserInfoConsts.UserName = emp.NameEmployee;

                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Close();
                }
                else
                    MessageBox.Show("Нет такого пользоваткля в базе данных");
            }
        }

        private void Button_Reg(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            Close();
        }
        private void Button_Guest(object sender, RoutedEventArgs e)
        {
            UserInfoConsts.RoleId = "1";
            UserInfoConsts.RoleName = "Гость";
            UserInfoConsts.UserName = "Гость";


            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }

}
