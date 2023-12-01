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
    /// Логика взаимодействия для CardClient.xaml
    /// </summary>
    public partial class CardClient : Window
    {   
        private Client client = new Client();
        public CardClient(Client selectedClient)
        {
            InitializeComponent();

            if (selectedClient != null)
                client = selectedClient;
            DataContext = client;
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            ClientsWindow clientsWindow = new ClientsWindow();
            clientsWindow.Show();
            Close();
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrWhiteSpace(client.LastNameClient))
                sb.AppendLine("Введите фамилию!");
            if (string.IsNullOrWhiteSpace(client.NameClient))
                sb.AppendLine("Введите имя!");
            if (string.IsNullOrWhiteSpace(client.MiddleNameClient))
                sb.AppendLine("Введите отчество!");

            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString());
                return;
            }
            if (client.IdClient ==0)
                Context.GetContext().Clients.Add(client);
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
