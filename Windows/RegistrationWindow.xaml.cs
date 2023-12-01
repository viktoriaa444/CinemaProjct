using CinemaProject.Infrastructure;
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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private Employee employee = new Employee();
        public RegistrationWindow()
        {
            InitializeComponent();

            ComboboxPost.ItemsSource = Context.GetContext().Posts.ToList();
            DataContext = employee;
            TextBoxDate.ToolTip = "Вводите данные в формате: ДД.ММ.ГГГГ, либо выбирете дату из календаря";
            TextBoxFloor.ToolTip = "Можете ввести Женский или Мужской, либо ничего не вводить.";
        }
        private void Button_Back(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Close();
        }
        private void Button_Save(object sender, RoutedEventArgs e)
        {
            Employee emp = null;
            using (Context db = new Context())
            {
                emp = db.Employees.Where(b => b.Login == employee.Login).FirstOrDefault();
            }

            StringBuilder sb = new StringBuilder();

            if (string.IsNullOrWhiteSpace(employee.LastNameEmployee))
                sb.AppendLine("Введите фамилию!");
            if (string.IsNullOrWhiteSpace(employee.NameEmployee))
                sb.AppendLine("Введите имя!");
            if (string.IsNullOrWhiteSpace(employee.MiddleNameEmployee))
                sb.AppendLine("Введите отчество!");
            if (employee.Floor != "Мужской" && employee.Floor != "Женский" && employee.Floor != null)
                sb.AppendLine("Введите ПРАВИЛЬНО пол!");
            if (employee.DateOfBirth == null)
                sb.AppendLine("Введите дату!");
            if (string.IsNullOrWhiteSpace(employee.Login))
                sb.AppendLine("Введите логин!");
            if (emp != null)
                sb.AppendLine("Ваш логин совпадает с логином другого пользователя придумайте новый!");
            if (string.IsNullOrWhiteSpace(employee.Password))
                sb.AppendLine("Введите пороль!");
            if (employee.Post == null)
                sb.AppendLine("Ввыберете должность!");


            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString());
                return;
            }
            if (employee.IdEmployee == 0)
                Context.GetContext().Employees.Add(employee);
            try
            {
                Context.GetContext().SaveChanges();
                MessageBox.Show("Данные были успешно добавлены, перейдите в окно авторизации.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
