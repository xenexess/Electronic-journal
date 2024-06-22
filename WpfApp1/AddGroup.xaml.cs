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
    public partial class AddGroup : Window
    {
        SqliteConnection connection = new SqliteConnection("Data Source=Database.db");
        public AddGroup()
        {
            InitializeComponent();

            connection.Open();
            string sqlExpression = $"SELECT Users.Id FROM Users WHERE Users.IdRole = 2";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) 
                {
                    while (reader.Read())   
                    {
                        TeacherTextBox.Items.Add(reader.GetInt32(0));
                    }
                }

            }

            connection.Close();

        }

        private void AddNewUser(object sender, RoutedEventArgs e)
        {
            if (GroupTextBox.Text == string.Empty || TeacherTextBox.Text == string.Empty)
            {
                MessageBox.Show("Не все поля заполнены");
            }
            else
            {
                connection.Open();

                string sqlExpression = $"INSERT INTO Groups(IdTeacher, Name) VALUES((SELECT Users.Id FROM Users WHERE Users.Id = {int.Parse(TeacherTextBox.Text)}), '{GroupTextBox.Text}' )";
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Группа добавлена.");
                this.Close();
            }
        }
    }
}
