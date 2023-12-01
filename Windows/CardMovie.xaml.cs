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
    /// Логика взаимодействия для CardMovie.xaml
    /// </summary>
    public partial class CardMovie : Window
    {
        private Movie movie = new Movie();
        public CardMovie(Movie selectedMovie)
        {
            InitializeComponent();

            if (selectedMovie != null)
                movie = selectedMovie;
            DataContext = movie;
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrWhiteSpace(movie.NameMovie))
                sb.AppendLine("Введите название!");
            if (movie.Rating.Equals(null))
                sb.AppendLine("Введите рейтинг!");
            if (movie.Rating > 10 || movie.Rating < 0)
                sb.AppendLine("Введите ПРАВИЛЬНЫЙ рейтинг!");

            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString());
                return;
            }
            if (movie.IdMovie == 0)
                Context.GetContext().Movies.Add(movie);
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

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            MovieWindow movieWindow = new MovieWindow();
            movieWindow.Show();
            Close();
        }
    }
}
