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
    public partial class PageCatalog : Page
    {
        public PageCatalog()
        {
            InitializeComponent();
            catalog.SetBinding(ItemsControl.ItemsSourceProperty, new Binding() { Source = DBControl.Products });
            unit.SetBinding(ItemsControl.ItemsSourceProperty, new Binding() { Source = DBControl.Units });
        }
        //ADD
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlCommand command = DBControl.GetCommand("INSERT INTO \"toExam\" (name, amount, unit, cost) VALUES (@name, @amount, @unit, @cost)");
            try
            {
                command.Parameters.AddWithValue("@name", NpgsqlDbType.Varchar, name.Text.Trim());
                command.Parameters.AddWithValue("@amount", NpgsqlDbType.Integer, Convert.ToInt32(amount.Text.Trim()));
                command.Parameters.AddWithValue("@unit", NpgsqlDbType.Varchar, unit.SelectedItem.ToString());
                command.Parameters.AddWithValue("@cost", NpgsqlDbType.Integer, Convert.ToInt32(cost.Text.Trim()));
                int result = command.ExecuteNonQuery();
                if (result == 1)
                {
                    MainWindow.LoadProduct();
                    amount.Text = string.Empty;
                    cost.Text = string.Empty;
                    name.Text = string.Empty;
                    unit.SelectedIndex = -1;
                }
            }
            catch
            {
                MessageBox.Show("Insert query failed");
                return;
            }
        }
        //CLEAR
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            amount.Text = string.Empty;
            cost.Text = string.Empty;
            name.Text = string.Empty;
            unit.SelectedIndex = -1;
            catalog.SelectedIndex = -1;
        }
        //BUY
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (amount.Text.Trim() != null)
            {
                if (Convert.ToInt32(amount.Text.Trim()) <= (catalog.SelectedItem as Product).Amount)
                {
                    NpgsqlCommand cmd = DBControl.GetCommand("UPDATE \"toExam\" SET amount=@amount WHERE id=@id");
                    try
                    {
                        cmd.Parameters.AddWithValue("@amount", NpgsqlDbType.Integer, (((catalog.SelectedItem as Product).Amount - Convert.ToInt32(amount.Text.Trim()))));
                        cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, (catalog.SelectedItem as Product).Id);
                        int res = cmd.ExecuteNonQuery();
                        if (res == 1)
                        {
                            MainWindow.LoadProduct();
                            amount.Text = string.Empty;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Update query failed");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Неверно задано количество");
                    return;
                }
            }
        }
        //DEL
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            NpgsqlCommand command = DBControl.GetCommand("DELETE FROM \"toExam\" WHERE id = @id");
            try
            {
                command.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, (catalog.SelectedItem as Product).Id);
                int result = command.ExecuteNonQuery();
                if (result == 1)
                {
                    MainWindow.LoadProduct();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось удалить");
            }
        }
        //UPD
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            NpgsqlCommand cmd = DBControl.GetCommand("UPDATE \"toExam\" SET name=@name, amount=@amount, unit=@unit, cost=@cost WHERE id=@id");
            try
            {
                cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Varchar, name.Text.Trim());
                cmd.Parameters.AddWithValue("@amount", NpgsqlDbType.Integer, Convert.ToInt32(amount.Text.Trim()));
                cmd.Parameters.AddWithValue("@unit", NpgsqlDbType.Varchar, unit.SelectedItem);
                cmd.Parameters.AddWithValue("@cost", NpgsqlDbType.Integer, Convert.ToInt32(cost.Text.Trim()));
                cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, (catalog.SelectedItem as Product).Id);
                int res = cmd.ExecuteNonQuery();
                if (res == 1)
                {
                    MainWindow.LoadProduct();
                    amount.Text = string.Empty;
                }
            }
            catch
            {
                MessageBox.Show("Update query failed");
                return;
            }
        }

        private void catalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (catalog.SelectedIndex != -1)
            {
                name.Text = (catalog.SelectedItem as Product).Name;
                amount.Text = (catalog.SelectedItem as Product).Amount.ToString();
                cost.Text = (catalog.SelectedItem as Product).Cost.ToString();
                unit.SelectedItem = (catalog.SelectedItem as Product).Unit;
            }
        }
    }
}
