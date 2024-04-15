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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            try
            {
                DBControl.Connect("localhost", "5432", "postgres", "toExam", "1234");
                AppFrame.Navigate(PageControl.PAuth);
            }
            catch
            {
                MessageBox.Show("Не удалось установить соединение");
                return;
            }
        }
        public static void LoadProduct()
        {
            DBControl.Products.Clear();
            NpgsqlCommand cmd = DBControl.GetCommand("SELECT id, name, amount, unit, cost FROM \"toExam\", \"Unit\" WHERE (\"toExam\".unit = \"Unit\".unitname)");
            try
            {
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DBControl.Products.Add(new Product(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4)));
                    }
                }
                reader.Close();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка");
                return;
            }

        }
        public static void LoadUnits()
        {
            DBControl.Units.Clear();
            NpgsqlCommand command = DBControl.GetCommand("SELECT unitname FROM \"Unit\"");
            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        //DBControl.Units.Add(new Unit(reader.GetString(0)));
                        DBControl.Units.Add(reader.GetString(0));
                    }
                    reader.Close();
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так");
                return;
            }
        }
    }
}
