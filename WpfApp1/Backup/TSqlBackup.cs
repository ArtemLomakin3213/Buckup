using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Microsoft.WindowsAPICodePack.Dialogs;
using WpfApp1.Interface;

namespace WpfApp1.Backup
{
    class TSqlBackup : IServiseTSqlBackup
    {

        private string _connectionString;

        private bool _isBusy;
        private int _progress;
        private string _status;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsBusy
        {
            get => _isBusy;
            private set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public int Progress
        {
            get => _progress;
            private set
            {
                _progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }

        public string Status
        {
            get => _status;
            private set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public TSqlBackup()
        {
            _connectionString = null;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task<bool> CreateFullBackupAsync(string connectionString, string name)
        {
            _connectionString = connectionString;
            try
            {
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var folderDialog = new CommonOpenFileDialog
                {
                    Title = "Выберите папку",
                    IsFolderPicker = true,
                    EnsurePathExists = true,
                    AllowNonFileSystemItems = false
                };
                if (folderDialog.ShowDialog() != CommonFileDialogResult.Ok)
                {
                    MessageBox.Show("Операция отменена");
                    return false;
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string backupPath = Path.Combine(folderDialog.FileName, $"{name}_FullBackup_{timestamp}.bak");
                    string sql = $@"BACKUP DATABASE [{connection.Database}] TO DISK = '{backupPath}' WITH STATS = 2, NAME = 'Full Backup of {connection.Database} ({timestamp})';";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        // Для отслеживания прогресса можно подписаться на событие
                        command.StatementCompleted += (s, e) =>
                        {
                            Progress += 2; // STATS = 2 означает каждые 2%
                            Status = $"Выполняется бэкап... {Progress}%";
                        };

                        await command.ExecuteNonQueryAsync();
                    }
                }

                Status = "Резервное копирование успешно завершено";
                Progress = 100;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                Status = "Ошибка при создании бэкапа";
                return false;
            }
        }

        public async Task<bool> CreateDifferentialBackupAsync(string connectionString, string name)
        {
            _connectionString = connectionString;
            try
            {
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                //Выбор места сохранения бэкапа
                var folderDialog = new CommonOpenFileDialog
                {
                    Title = "Выберите папку",
                    IsFolderPicker = true,
                    EnsurePathExists = true,
                    AllowNonFileSystemItems = false
                };
                if (folderDialog.ShowDialog() != CommonFileDialogResult.Ok)
                {
                    MessageBox.Show("Операция отменена");
                    return false;
                }
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string backupPath = Path.Combine(folderDialog.FileName, $"{name}_FullBackup_{timestamp}.bak");
                    string sql = $@"BACKUP DATABASE [{connection.Database}] TO DISK = '{backupPath}' WITH DIFFERENTIAL,COMPRESSION, STATS = 1, NAME = 'Differential Backup of {connection.Database} ({timestamp})';";

                    using (var command = new SqlCommand(sql, connection))
                    {
                       await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }

        }

        public bool CreateTransactionLogBackupAsync(string connectionString, string name)
        {
            //_connectionString = connectionString;

            //return await ExecuteBackupOperationAsync("TransactionLog", async (connection, databaseName) =>
            //{
            //    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            //    //Выбор места сохранения бэкапа
            //    var saveDialog = new SaveFileDialog
            //    {
            //        Title = "Сохранить резервную копию как...",
            //        Filter = "ZIP архивы (*.zip)|*.zip",
            //        FileName = $"{name}_TransactionBackup_{timestamp}.bak",
            //        OverwritePrompt = true
            //    };

            //    if (saveDialog.ShowDialog() != DialogResult.OK)
            //    {
            //        return "Операция отменена";
            //    }
            //    string backupPath = saveDialog.FileName;
            //    string sql = $@"
            //        BACKUP LOG [{databaseName}] 
            //        TO DISK = '{backupPath}' 
            //        WITH COMPRESSION, 
            //        NAME = 'Transaction Log Backup of {databaseName} ({timestamp})',
            //        STATS = 1;";

            //    using (var command = new SqlCommand(sql, connection))
            //    {
            //        command.StatementCompleted += (s, e) =>
            //        {
            //            Progress += 1;
            //        };

            //        await command.ExecuteNonQueryAsync();
            //    }

               return false;
            //});
        }

    }
}



