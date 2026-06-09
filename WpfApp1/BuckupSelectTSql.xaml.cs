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
using WpfApp1.Backup;
using WpfApp1.Interface;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для BuckupSelectTSql.xaml
    /// </summary>
    public partial class BuckupSelectTSql : System.Windows.Window
    {
        private IServiseTSqlBackup _serviceTSqlBackup;
        private string _connecting;
        private string _name;
        public BuckupSelectTSql(string connecting, string name)
        {
            _serviceTSqlBackup = new TSqlBackup();
            _connecting = connecting;
            _name = name;
            InitializeComponent();
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



        private async void selectButton1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (select_comboBox.SelectedIndex)
                {
                    case 0:
                        MessageBox.Show("Выберите тип резервного копирования");
                        break;
                    case 1:
                        bool success = await _serviceTSqlBackup.CreateFullBackupAsync(_connecting, _name);
                        if (success)
                        {
                            MessageBox.Show("Полная резервная копия успешно создана!", "Успех",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Не удалось создать полную резервную копию.", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;
                    case 2:
                        bool successDiff = false;
                        if (successDiff)
                        {
                            MessageBox.Show("Дифференциальная резервная копия успешно создана!", "Успех",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Не удалось создать дифференциальную резервную копию.", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;
                    case 3:
                        bool successLog = false;
                        if (successLog)
                        {
                            MessageBox.Show("Резервная копия журнала транзакций успешно создана!", "Успех",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Не удалось создать резервную копию журнала транзакций.", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
