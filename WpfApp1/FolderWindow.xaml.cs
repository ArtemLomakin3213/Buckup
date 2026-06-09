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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack;
using Microsoft.WindowsAPICodePack.Dialogs;
using WpfApp1.DB;
using WpfApp1.Interface;
using WpfApp1.Model;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для FolderWindow.xaml
    /// </summary>
    public partial class FolderWindow : Window
    {
        private int _id_users;
        private IServiceFileList _IServiceFileList;
        public FolderWindow(int id_user)
        {
            InitializeComponent();
            _id_users = id_user;
            _IServiceFileList = new DbServiceFileList();
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
            FileList list = new FileList
            {
                Id_users = _id_users,
                Name = name_textbox.Text,
                Files = folder_textbox.Text
            };
            try
            {
                _IServiceFileList.addList(list);
                System.Windows.MessageBox.Show("Файл был успешно добавлен");
                this.Close();

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
    }
}
