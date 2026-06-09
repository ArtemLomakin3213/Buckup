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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для UpdateDbWindow.xaml
    /// </summary>
    public partial class UpdateDbWindow : System.Windows.Window
    {
        private int _id_users;
        private int _id_list;
        private System.Windows.Window _window;
        private ServerList _list;
        private IServiceDbList _IServiceDb;
        public UpdateDbWindow(int id_user, int id_list)
        {
            InitializeComponent();
            _id_users = id_user;
            _id_list = id_list;
            _IServiceDb = new DbServiseDBlist();
            enterList();

        }
        private void enterList()
        {
            _list = _IServiceDb.GetOneServerList(_id_list).ElementAt(0);
            name_textbox.Text = _list.Name;
            server_textbox.Text = _list.ServerName;
            bd_textbox.Text = _list.Db;
            isAuth_checkbox.IsChecked = _list.Is_auth;
            user_textbox.Text = _list.LoginUsers;
            password_textbox.Text = _list.PasswordUsers;
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

        private void save_button_Click(object sender, RoutedEventArgs e)
        {
            ServerList serverList = new()
            {
                Id = _id_list,
                Id_users = _id_users,
                Name = name_textbox.Text,
                ServerName = server_textbox.Text,
                Db = bd_textbox.Text,
                Is_auth = isAuth_checkbox.IsChecked ?? false,
                LoginUsers = user_textbox.Text ?? null,
                PasswordUsers = password_textbox.Text ?? null
            };
            try
            {
                _IServiceDb.updateServerList(serverList);
                MessageBox.Show($"Данные были обновлены", "Сохранение",
    MessageBoxButton.OK, MessageBoxImage.Question);
                this.Close();
                _window = new MainWindow(_id_users);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

        }
    }
}
