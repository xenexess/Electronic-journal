using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace WpfApp1
{
    public partial class AddUser : Window
    {
        SqliteConnection connection = new SqliteConnection("Data Source=Database.db");
        public AddUser()
        {
            InitializeComponent();
        }

        private void AddNewUser(object sender, RoutedEventArgs e)
        {
            if (LoginTextBox.Text == string.Empty || PasswordTextBox.Text == string.Empty || RoleComboBox.SelectedItem == null)
            {
                MessageBox.Show("Не все поля заполнены");
            }
            else
            {
                connection.Open();

                string sqlExpression = $"INSERT INTO Users(IdRole, Login, Password) VALUES((SELECT Roles.Id FROM Roles WHERE Roles.Name = '{RoleComboBox.Text}'),'{LoginTextBox.Text}','{PasswordTextBox.Text}' )";
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Пользователь добавлен.");
                this.Close();
            }
        }
    }
}
