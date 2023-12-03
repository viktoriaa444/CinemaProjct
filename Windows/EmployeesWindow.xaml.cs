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
            CardEmployee cardEmployee = new CardEmployee((sender as System.Windows.Controls.Button).DataContext as Employee);
            cardEmployee.Show();
            Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = Context.GetContext().Employees.ToList();
            textBox = textBox.Where(p => p.Login.ToLower().Contains(TextBox_Search.Text.ToLower())).ToList();
            EmployeeGrid.ItemsSource = textBox.OrderBy(p=>p.Login).ToList();
        }

        private void Button_Click_Upload(object sender, RoutedEventArgs e)
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

            for (int j = 0; j < EmployeeGrid.Columns.Count; j++)
            {
                Range myRange = (Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true;
                sheet1.Columns[j + 1].ColumnWidth = 15;
                myRange.Value2 = EmployeeGrid.Columns[j].Header;
            }
            for (int i = 0; i < EmployeeGrid.Columns.Count; i++)
            { 
                for (int j = 0; j < EmployeeGrid.Items.Count; j++)
                {
                    TextBlock b = EmployeeGrid.Columns[i].GetCellContent(EmployeeGrid.Items[j]) as TextBlock;

                    if (b == null)
                        continue;

                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;
                }
            }
        }
    }
}
