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
    /// Логика взаимодействия для EmployeesWindow.xaml
    /// </summary>
    public partial class EmployeesWindow : Window
    {
        public EmployeesWindow()
        {
            InitializeComponent();
            //EmployeeGrid.ItemsSource = Context.GetContext().Employees.ToList();
        }

        private void Button_Click_Main(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            CardEmployee cardEmployees = new CardEmployee(null);
            cardEmployees.Show();
            Close();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            var list = EmployeeGrid.SelectedItems.Cast<Employee>().ToList();
            if (MessageBox.Show($"Вы уверены, что хотите удалить{list.Count()} элемент/ы?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Context.GetContext().Employees.RemoveRange(list);
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
            EmployeeGrid.ItemsSource = Context.GetContext().Employees.ToList();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Context.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                EmployeeGrid.ItemsSource = Context.GetContext().Employees.ToList();
            }
        }
        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            CardEmployee cardEmployee = new CardEmployee((sender as Button).DataContext as Employee);
            cardEmployee.Show();
            Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = Context.GetContext().Employees.ToList();
            textBox = textBox.Where(p => p.Login.ToLower().Contains(TextBox_Search.Text.ToLower())).ToList();
            EmployeeGrid.ItemsSource = textBox.OrderBy(p=>p.Login).ToList();
        }
    }
}
