using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class GroupsWindow : Window
    {
        SqliteConnection connection = new SqliteConnection("Data Source=Database.db");
        public GroupsWindow()
        {
            InitializeComponent();

            List<Group> groups = new List<Group>();

            connection.Open();
            string sqlExpression = $"SELECT * FROM Groups";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) 
                {
                    while (reader.Read())   
                    {
                        Group group = new Group();

                        group.Id = reader.GetInt32(0);
                        group.TeacherId = reader.GetInt32(1);
                        group.GroupName = reader.GetString(2);
                        groups.Add(group);
                    }
                }

            }

            connection.Close();
            GroupsTable.ItemsSource = groups;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddGroup add = new AddGroup();
            add.ShowDialog();

            GroupsTable.ItemsSource = null;
            List<Group> groups = new List<Group>();
            connection.Open();
            string sqlExpression = $"SELECT * FROM Groups";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) 
                {
                    while (reader.Read())   
                    {
                        Group group = new Group();

                        group.Id = reader.GetInt32(0);
                        group.TeacherId = reader.GetInt32(1);
                        group.GroupName = reader.GetString(2);
                        groups.Add(group);
                    }
                }
            }

            connection.Close();
            GroupsTable.ItemsSource = groups;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            connection.Open();
            Group selected = (Group)GroupsTable.SelectedItem;
            if (selected == null)
            {
                MessageBox.Show("Выберите группу");
            }
            else
            {
                string sqlExpression = $"DELETE FROM Groups WHERE Id = {selected.Id}";
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                command.ExecuteNonQuery();
                GroupsTable.ItemsSource = null;
                List<Group> groups = new List<Group>();

                connection.Open();
                sqlExpression = $"SELECT * FROM Groups";
                command = new SqliteCommand(sqlExpression, connection);

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) 
                    {
                        while (reader.Read())   
                        {
                            Group group = new Group();

                            group.Id = reader.GetInt32(0);
                            group.TeacherId = reader.GetInt32(1);
                            group.GroupName = reader.GetString(2);
                            groups.Add(group);
                        }
                    }
                }

                connection.Close();
                GroupsTable.ItemsSource = groups;

            }
        }
    }
}
