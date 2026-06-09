using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using WpfApp1.DB;
using WpfApp1.Model;
using WpfApp1.Interface;
using System.Windows.Forms;
using WpfApp1.Backup;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly int _id_users;
        private Window _window;
        private IServiceFileList _IServiceFileList;
        private IServiceDbList IServiceDbList;
        private IServiceFolderBuckup _IServiceFolderBuckup;
        private IServiceDbList _IServiceDbList;
        private ObservableCollection<FileList> FileLists { get; set; }
        private ObservableCollection<ServerList> ServerLists { get; set; }
        private ObservableCollection<object> AllItem { get; set; }


        public MainWindow(int id_users)
        {
            InitializeComponent();

            _window = null;
            _id_users = id_users;
            _IServiceFileList = new DbServiceFileList();
            _IServiceFolderBuckup = new FolderBuckup();
            IServiceDbList = new DbServiseDBlist();
            AllItem = new ObservableCollection<object>();

            loadData();
        }
        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            _window = new CloseWindow();
            _window.ShowDialog();
        }

        private void header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            _window = new SelectWindow(_id_users);
            _window.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            loadData();
        }

        private void loadData()
        {
            FileLists = new ObservableCollection<FileList>(_IServiceFileList.GetFileList(_id_users));
            ServerLists = new ObservableCollection<ServerList>(IServiceDbList.GetServerList(_id_users));
            AllItem.Clear();
            foreach (var filelist in FileLists) AllItem.Add(filelist);
            foreach (var serverlist in ServerLists) AllItem.Add(serverlist);
            list_listbox.ItemsSource = AllItem;
        }

        private void update_button_Click(object sender, RoutedEventArgs e)
        {
            if (list_listbox.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("Выберите файл");
            }

            if (list_listbox.SelectedItem is FileList fileList)
            {
                _window = new UpdateFolderWindow(_id_users, fileList.Id);
                _window.ShowDialog();
            }
            else if (list_listbox.SelectedItem is ServerList serverList)
            {
                _window = new UpdateDbWindow(_id_users, serverList.Id);
                _window.ShowDialog();
            }
        }

        private void delete_button_Click(object sender, RoutedEventArgs e)
        {
            if (list_listbox.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("Выберите файл");
                return;
            }
            System.Windows.MessageBox.Show("Вы точно хотите удалить файл из списка?","Удаление",MessageBoxButton.YesNo,MessageBoxImage.Information);
            if (list_listbox.SelectedItem is FileList fileList)
            {
                _IServiceFileList.deleteFileList(fileList.Id);
            }
            else if (list_listbox.SelectedItem is ServerList serverList)
            {
                IServiceDbList.deleteServerList(serverList.Id);
            }
        }

        private void backup_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (list_listbox.SelectedItem == null)
                {
                    System.Windows.MessageBox.Show("Выберите файл");
                }

                if (list_listbox.SelectedItem is FileList fileList)
                {
                    _IServiceFolderBuckup.CreateBackupAsync(fileList.Name,fileList.Files);
                }
                else if (list_listbox.SelectedItem is ServerList serverList)
                {
                    if (serverList.Is_auth == true)
                    {
                        string connecting = $@"Data Source={serverList.ServerName};
			                         Initial Catalog={serverList.Db};
                                     User Id = {serverList.LoginUsers};
                                     Password = {serverList.PasswordUsers};
			                         Integrated Security=True;
			                         TrustServerCertificate=True;";
                        _window = new BuckupSelectTSql(connecting,serverList.Name);
                        _window.ShowDialog();
                    }
                    else
                    {
                        string connecting = $"Data Source={serverList.ServerName};Initial Catalog={serverList.Db};Integrated Security=True;TrustServerCertificate=True;";
                        _window = new BuckupSelectTSql(connecting, serverList.Name);
                        _window.ShowDialog();
                    }
                        return;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void download_Click(object sender, RoutedEventArgs e)
        {
            loadData();
        }
    }
}
