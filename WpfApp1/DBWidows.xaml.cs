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
using WpfApp1.DB;
using WpfApp1.Interface;
using WpfApp1.Model;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для DBWidows.xaml
    /// </summary>
    public partial class DBWidows : Window
    {
        private int _id_users;
        private IServiceDbList _serviceDbList;
        public DBWidows( int id_users)
        {
            _id_users = id_users;
            InitializeComponent();
            _serviceDbList = new DbServiseDBlist();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void hide_button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void close_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            ServerList list = new ServerList()
            {
                Id_users = _id_users,
                Name = name_textbox.Text,
                ServerName = server_textbox.Text,
                Db = db_textbox.Text,
                Is_auth = isAuth_checkbox.IsChecked ?? false,
                LoginUsers = user_textbox.Text,
                PasswordUsers = password_textbox.Text
            };
            try
            {
                _serviceDbList.addServerList(list);
                System.Windows.MessageBox.Show("Файл был успешно добавлен");
                this.Close();

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
    }
}
