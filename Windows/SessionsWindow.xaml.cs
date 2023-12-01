using CinemaProject.Infrastructure;
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
    /// Логика взаимодействия для SessionsWindow.xaml
    /// </summary>
    public partial class SessionsWindow : Window
    {
        public SessionsWindow()
        {
            InitializeComponent();
            SessionGrid.ItemsSource = Context.GetContext().Sessions.ToList();

            if (UserInfoConsts.RoleName == "Гость")
            {
                DeleteBtn.Visibility = Visibility.Collapsed;
                ExBtn.Visibility = Visibility.Collapsed;
                AddBtn.Visibility = Visibility.Collapsed;
            }

            //var allName = Context.GetContext().Movies.ToList();
            //allName.Insert(0, new Movie { NameMovie = "Все названия" });
            //ComboBox_Search.ItemsSource = allName;
            //ComboBox_Search.SelectedIndex = 0;

            //var comboBox = Context.GetContext().Sessions.ToList();
            //SessionGrid.ItemsSource = comboBox;
        }

        private void Button_Click_Main(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            CardSession cardSession = new CardSession(null);
            cardSession.Show();
            Close();
        }
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            var list = SessionGrid.SelectedItems.Cast<Session>().ToList();
            if (MessageBox.Show($"Вы уверены, что хотите удалить{list.Count()} элемент/ы?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Context.GetContext().Sessions.RemoveRange(list);
                    Context.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            SessionGrid.ItemsSource = Context.GetContext().Sessions.ToList();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Context.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                SessionGrid.ItemsSource = Context.GetContext().Sessions.ToList();
            }
        }
        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            CardSession cardSession = new CardSession((sender as Button).DataContext as Session);
            cardSession.Show();
            Close();
        }


        private void ComboBox_Search_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var comboBox = Context.GetContext().Sessions.ToList();
            //if (ComboBox_Search.SelectedIndex >0)
            //    comboBox = comboBox.Where(p => p.Movies.Contains(ComboBox_Search.SelectedItem as Movie)).ToList();
            //SessionGrid.ItemsSource = comboBox.OrderBy(p => p.Movie).ToList();
        }
    }
}
