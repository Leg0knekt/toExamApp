using Npgsql;
using NpgsqlTypes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace toExamApp
{
    /// <summary>
    /// Логика взаимодействия для PageAuth.xaml
    /// </summary>
    public partial class PageAuth : Page
    {
        public PageAuth()
        {
            InitializeComponent();
        }

        private void sign_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlCommand command = DBControl.GetCommand("SELECT role, log, pass FROM \"User\" WHERE (@log = log AND @pass = pass)");

            command.Parameters.AddWithValue("@log", NpgsqlDbType.Varchar, login.Text.Trim());
            command.Parameters.AddWithValue("@pass", NpgsqlDbType.Varchar, password.Password.Trim());
            NpgsqlDataReader result = command.ExecuteReader();
            if (result.HasRows)
            {
                result.Read();
                string role = result.GetString(0);
                result.Close();
                switch (role)
                {
                    case "продавец":
                        //MessageBox.Show("Поздравляю, вы продавец");
                        MainWindow.LoadProduct();
                        MainWindow.LoadUnits();
                        NavigationService.Navigate(PageControl.PCatalog);
                        break;
                    case "клиент":
                        MessageBox.Show("Поздравляю, вы не продавец");
                        //NavigationService.Navigate(PageControl.PageEmployeeMenu);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Ошибка авторизации");
                return;
            }
            result.Close();
        }
    }
}
