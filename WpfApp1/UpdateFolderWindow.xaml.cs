using Microsoft.WindowsAPICodePack.Dialogs;
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
    /// Логика взаимодействия для UpdateFolderWindow.xaml
    /// </summary>
    public partial class UpdateFolderWindow : Window
    {
        private int _id_users;
        private int _id_list;
        private Window _window;
        private FileList _list;
        private IServiceFileList _IServiseFileList;


        public UpdateFolderWindow(int id_users, int id_list)
        {
            InitializeComponent();
            _window = new Window();
            _id_list = id_list;
            _id_users = id_users;
            _IServiseFileList = new DbServiceFileList();
            _list = _IServiseFileList.GetOneFileList(_id_list).ElementAt(0);
            enterlist();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void hide_button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void save_button_Click(object sender, RoutedEventArgs e)
        {
            FileList list = new FileList
            {
                Id = _id_list,
                Id_users = _id_users,
                Name = name_textbox.Text,
                Files = folder_textbox.Text
            };
            try
            {
                _IServiseFileList.updateFileTask(list);
                MessageBox.Show($"Данные были обновлены.", "Информация",
    MessageBoxButton.OK, MessageBoxImage.Question);
                this.Close();
                _window = new MainWindow(_id_users);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

        }
        private void folder_Click(object sender, RoutedEventArgs e)
        {
            var folderDialog = new CommonOpenFileDialog
            {
                Title = "Выберите папку",
                IsFolderPicker = true,
                EnsurePathExists = true,
                AllowNonFileSystemItems = false
            };
            if (folderDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                folder_textbox.Text = folderDialog.FileName;
            }
        }
        private void enterlist()
        {
            name_textbox.Text = _list.Name;
            folder_textbox.Text = _list.Files;
        }
    }
}
