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
    /// Логика взаимодействия для CardSession.xaml
    /// </summary>
    public partial class CardSession : Window
    {
        private Session session = new Session();
        public CardSession(Session selectedSession)
        {
            InitializeComponent();

            ComboBoxHall.ItemsSource = Context.GetContext().Halls.ToList();
            ComboBoxName.ItemsSource = Context.GetContext().Movies.ToList();

            if (selectedSession != null)
                session = selectedSession;
            DataContext = session;
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            SessionsWindow sessionsWindow = new SessionsWindow();
            sessionsWindow.Show();
            Close();
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            if (string.IsNullOrWhiteSpace(session.Movie.NameMovie))
                sb.AppendLine("Введите фамилию!");
            if (session.Movie.Rating > 10 || session.Movie.Rating < 0)
                sb.AppendLine("Введите имя!");
            if (session.Cost.Equals(null) )
                sb.AppendLine("Введите отчество!");
            if (session.Hall == null)
                sb.AppendLine("Введите ПРАВИЛЬНО пол!");
            if (session.Session_start_time == null)
                sb.AppendLine("Введите логин!");
            if (session.Session_end_time == null)
                sb.AppendLine("Ввыберете должность!");


            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString());
                return;
            }
            if (session.IdSession == 0)
                Context.GetContext().Sessions.Add(session);
            try
            {
                Context.GetContext().SaveChanges();
                MessageBox.Show("Данные были успешно добавлены");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
