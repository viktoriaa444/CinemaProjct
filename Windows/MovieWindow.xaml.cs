using CinemaProject.Infrastructure;
using Microsoft.Office.Interop.Excel;
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
using Window = System.Windows.Window;
using Excel = Microsoft.Office.Interop.Excel;

namespace CinemaProject.Windows
{
    /// <summary>
    /// Логика взаимодействия для MovieWindow.xaml
    /// </summary>
    public partial class MovieWindow : Window
    {
        public MovieWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            var list = MovieGrid.SelectedItems.Cast<Movie>().ToList();
            if (MessageBox.Show($"Вы уверены, что хотите удалить{list.Count()} элемент/ы?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Context.GetContext().Movies.RemoveRange(list);
                    Context.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            CardMovie cardMovie = new CardMovie(null);
            cardMovie.Show();
            Close();
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            MovieGrid.ItemsSource = Context.GetContext().Clients.ToList();
        }

        private void Button_Click_Main(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            CardMovie cardMovie = new CardMovie((sender as System.Windows.Controls.Button).DataContext as Movie);
            cardMovie.Show();
            Close();
        }
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Context.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                MovieGrid.ItemsSource = Context.GetContext().Movies.ToList();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = Context.GetContext().Movies.ToList();
            textBox = textBox.Where(p => p.NameMovie.ToLower().Contains(TextBox_Search.Text.ToLower())).ToList();
            MovieGrid.ItemsSource = textBox.OrderBy(p => p.NameMovie).ToList();
        }

        private void Button_Click_UpLoad(object sender, RoutedEventArgs e)
        {

            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

            for (int j = 0; j < MovieGrid.Columns.Count; j++)
            {
                Range myRange = (Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true;
                sheet1.Columns[j + 1].ColumnWidth = 15;
                myRange.Value2 = MovieGrid.Columns[j].Header;
            }
            for (int i = 0; i < MovieGrid.Columns.Count; i++)
            {  
                for (int j = 0; j < MovieGrid.Items.Count; j++)
                {
                    TextBlock b = MovieGrid.Columns[i].GetCellContent(MovieGrid.Items[j]) as TextBlock;

                    if (b == null)
                        continue;

                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;
                }
            }

            
        }


    }
}
