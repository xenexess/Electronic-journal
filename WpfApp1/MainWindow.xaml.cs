using Microsoft.Data.Sqlite;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        SqliteConnection connection = new SqliteConnection("Data Source=Database.db");
        public MainWindow()
        {
            InitializeComponent();
        }


        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text.ToLower() == "логин" || (sender as TextBox).Text.ToLower() == "пароль")
            {
                (sender as TextBox).Text = string.Empty;
                (sender as TextBox).Foreground = Brushes.Black;
            }
        }
        private void LoginTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text.ToLower() == string.Empty)
            {
                switch ((sender as TextBox).Name)
                {
                    case "LoginTextBox":
                        (sender as TextBox).Text = "Логин";
                        break;
                    case "PasswordTextBox":
                        (sender as TextBox).Text = "Пароль";
                        break;
                }
                (sender as TextBox).Foreground = Brushes.Gray;
            }
        }

        private void Login(object sender, RoutedEventArgs e)
        {

            if ((LoginTextBox.Text != string.Empty && LoginTextBox.Text.ToLower() != "логин") &&
                (PasswordTextBox.Text != string.Empty && PasswordTextBox.Text.ToLower() != "пароль"))
            {

                List<string> userData = new List<string>();


                connection.Open();
                string sqlExpression = $"SELECT Users.Id, Roles.Name FROM Users JOIN Roles ON Users.IdRole = Roles.Id WHERE Users.Login = '{LoginTextBox.Text}' AND Users.Password = '{PasswordTextBox.Text}'";
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) 
                    {
                        while (reader.Read())   
                        {
                            userData.Add(reader.GetInt32(0).ToString());
                            userData.Add(reader.GetString(1));
                        }
                    }

                    else
                    {
                        userData.Add("null");
                    }

                    switch (userData[1])
                    {
                        case "Администратор":
                            Window1 adminWindow = new Window1();
                            adminWindow.Show();
                            this.Close();
                            break;
                        case "Преподаватель":
                            adminWindow = new Window1();
                            adminWindow.Show();
                            this.Close();
                            break;
                        case "Студент":
                            adminWindow = new Window1();
                            adminWindow.Show();
                            this.Close();
                            break;

                    }
                }

                

              
                connection.Close();

            }


        }

        private void LoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}