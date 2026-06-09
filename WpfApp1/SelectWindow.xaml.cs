using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// <summary>
    /// Логика взаимодействия для SelectWindow.xaml
    /// </summary>
    public partial class SelectWindow : Window
    {
        private Window _window;
        private int _id_users;
        public SelectWindow(int id_users)
        {
            InitializeComponent();
            _window = new Window();
            _id_users = id_users;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void selectButton_Click(object sender, RoutedEventArgs e)
        {
            switch (select_comboBox.SelectedIndex)
            {
                case 0:
                    MessageBox.Show($"Не был выбран файл", "Ошибка",
MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case 1:
                    _window = new FolderWindow(_id_users);
                    this.Close();
                    _window.Show();
                    break;
                case 2:
                    _window = new DBWidows(_id_users);
                    this.Close();
                    _window.Show();
                    break;
                default:
                    break;
            }
        }
    }
}
