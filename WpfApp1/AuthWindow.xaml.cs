using Microsoft.VisualBasic.ApplicationServices;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        private Window _window;
        private readonly IServiceUsers _serviceUsers;
        public AuthWindow()
        {
            InitializeComponent();
            _serviceUsers = new DbServiceUsers();
            _window = null;
        }
        
        //Обработчик для перемещение формы за верхнюю панель
        private void header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        //Открытие формы для выхода из программы
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            _window = new CloseWindow();
            _window.ShowDialog();
        }
        private void Entrance_Click(object sender, RoutedEventArgs e)
        {
            _window = new CloseWindow();
            _window.ShowDialog();
        }
        //Оюработчик авторизации пользователя и вход в программу
        private void enterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string login = login_textbox.Text;
                string password = password_textbox.Password;
                    bool count = _serviceUsers.GetToName(login, password);
                    if (count)
                    {
                        int users_id = _serviceUsers.CheckIdBase(login);
                        MessageBox.Show("Вход был выполнен");
                        _window = new MainWindow(users_id);
                        this.Close();
                        _window.Show();
                    }
                    else
                    {
                        MessageBox.Show("Неверное имя пользователя или пароль","Failed",MessageBoxButton.OK,MessageBoxImage.Error);
                    }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //Обработчик для перехода в окно регистрации
        private void registrationButton_Click(object sender, RoutedEventArgs e)
        {
            _window = new RegistrationWindow();
            _window.ShowDialog();
        }
    }
}
