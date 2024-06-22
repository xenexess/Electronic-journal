using Microsoft.Data.Sqlite;
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

namespace WpfApp1
{
    public partial class Window1 : Window
    {
        SqliteConnection connection = new SqliteConnection("Data Source=Database.db");
        public Window1()
        {
            InitializeComponent();

            List<User> users = new List<User>();

            connection.Open();
            string sqlExpression = $"SELECT Users.Id, Roles.Name, Login, Password FROM Users JOIN Roles ON Users.IdRole = Roles.Id";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())   
                    {
                        User user = new User();

                        user.Id = reader.GetInt32(0); ;
                        user.Role = reader.GetString(1);
                        user.Login = reader.GetString(2);
                        user.Password = reader.GetString(3);

                        users.Add(user);
                    }
                }

            }

            connection.Close();
            UsersTable.ItemsSource = users;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddUser add = new AddUser();
            add.ShowDialog();

            UsersTable.ItemsSource = null;
            List<User> users = new List<User>();

            connection.Open();
            string sqlExpression = $"SELECT Users.Id, Roles.Name, Login, Password FROM Users JOIN Roles ON Users.IdRole = Roles.Id";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) 
                {
                    while (reader.Read())   
                    {
                        User user = new User();

                        user.Id = reader.GetInt32(0); ;
                        user.Role = reader.GetString(1);
                        user.Login = reader.GetString(2);
                        user.Password = reader.GetString(3);

                        users.Add(user);
                    }
                }

            }

            connection.Close();
            UsersTable.ItemsSource = users;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            connection.Open();
            User selected = (User)UsersTable.SelectedItem;
            if (selected == null)
            {
                MessageBox.Show("Выберите человека");
            }
            else
            {
                string sqlExpression = $"DELETE FROM Users WHERE Id = {selected.Id}";
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                command.ExecuteNonQuery();
                UsersTable.ItemsSource = null;
                List<User> users = new List<User>();

                connection.Open();
                sqlExpression = $"SELECT Users.Id, Roles.Name, Login, Password FROM Users JOIN Roles ON Users.IdRole = Roles.Id";
                command = new SqliteCommand(sqlExpression, connection);

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) 
                    {
                        while (reader.Read())  
                        {
                            User user = new User();

                            user.Id = reader.GetInt32(0); ;
                            user.Role = reader.GetString(1);
                            user.Login = reader.GetString(2);
                            user.Password = reader.GetString(3);

                            users.Add(user);
                        }
                    }

                }

                connection.Close();
                UsersTable.ItemsSource = users;

            }

            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            GroupsWindow group = new GroupsWindow();
            group.ShowDialog();
        }
    }
}
