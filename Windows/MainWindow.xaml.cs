using CinemaProject.Infrastructure.Consts;
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

namespace CinemaProject.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TextBlock.Text = "Имя пользователя: " + UserInfoConsts.UserName + "\nДолжность: " + UserInfoConsts.RoleName;
            if (UserInfoConsts.RoleName == "Гость")
            {
                ButtonEmployee.Visibility = Visibility.Collapsed;
                ButtonClient.Visibility = Visibility.Collapsed;
                ButtonMovie.Visibility = Visibility.Collapsed;
            }
            if (UserInfoConsts.RoleName != "Администратор/ка")
            {
                ButtonEmployee.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Click_Employee(object sender, RoutedEventArgs e)
        {
            EmployeesWindow employeesWindow = new EmployeesWindow();
            employeesWindow.Show();
            Close();
        }

        private void Button_Click_Client(object sender, RoutedEventArgs e)
        {
            ClientsWindow clientsWindow = new ClientsWindow();
            clientsWindow.Show();
            Close();
        }

        private void Button_Click_Session(object sender, RoutedEventArgs e)
        {
            SessionsWindow sessionsWindow = new SessionsWindow();
            sessionsWindow.Show();
            Close();
        }
       
        private void Button_Click_Movie(object sender, RoutedEventArgs e)
        {
            MovieWindow movieWindow = new MovieWindow();
            movieWindow.Show();
            Close();
        }
        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {

            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Close();
        }
    }
}
