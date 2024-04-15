using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace toExamApp
{
    public class DBControl
    {
        private static NpgsqlConnection Connection;
        public static void Connect(string host, string port, string username, string database, string password)
        {
            string cs = string.Format("Server = {0}; Port = {1}; User Id = {2}; Database = {3}; Password = {4}", host, port, username, database, password);

            Connection = new NpgsqlConnection(cs);
            if (Connection != null)
            {
                Connection.Open();
            }
        }
        public static NpgsqlCommand GetCommand(string sql)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = Connection;
            command.CommandText = sql;
            return command;
        }
        public static ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
        public static ObservableCollection<string> Units { get; set; } = new ObservableCollection<string>();
    }
}
