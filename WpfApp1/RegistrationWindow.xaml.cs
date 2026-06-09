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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private Window _window;
        private Users user;
        private readonly IServiceUsers _IUser;
        public RegistrationWindow()
        {
            InitializeComponent();
            _IUser = new DbServiceUsers();
            _window = null;
            user = new Users();
        }

        private void header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        //

        private void Entrance_Click(object sender, RoutedEventArgs e)
        {
            _window = new CloseWindow();
            _window.ShowDialog();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Проверка на веденные данные
                if (login_textbox.Text != "" && email_textbox.Text != "" && password_textbox.Text != "")
                {
                    user.Username = login_textbox.Text;
                    user.Email = email_textbox.Text;
                    user.Password = password_textbox.Text;
                }
                else
                {
                    MessageBox.Show("Данные не введены");
                    return;
                }
                //Проверка на дубликаты данных
                if (_IUser.CheckUserBase(user))
                {
                    MessageBox.Show("Пользователь под этим именем уже используется");
                    return;
                }
                else if (_IUser.CheckEmailBase(user))
                {
                    MessageBox.Show("Почта уже используется");
                    return;
                }
                else
                {
                    _IUser.Registration(user);
                    MessageBox.Show("Регистрация прошла успешно");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
