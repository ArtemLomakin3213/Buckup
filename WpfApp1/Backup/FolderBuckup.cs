using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WpfApp1.DB;
using WpfApp1.Interface;
using WpfApp1.Model;

namespace WpfApp1.Backup
{
    class FolderBuckup : IServiceFolderBuckup
    {
        private BackgroundWorker _worker;
        private string _sourcePath;
        private string _zipPath;
        private IServiceFileList _IServiceFileList;

        public void CreateBackupAsync(string name,string folder)
        {

            _sourcePath = folder;

            //Выбор места сохранения бэкапа
            var saveDialog = new SaveFileDialog
            {
                Title = "Сохранить резервную копию как...",
                Filter = "ZIP архивы (*.zip)|*.zip",
                FileName = $"{name}_backup_{DateTime.Now:yyyyMMdd_HHmmss}.zip",
                OverwritePrompt = true
            };

            if (saveDialog.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Операция отменена");
                return;
            }
            _zipPath = saveDialog.FileName;

            //Запуск в фоновом потоке
            _worker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            _worker.DoWork += DoBackupWork;
           
            _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            _worker.RunWorkerAsync();
        }

        private void DoBackupWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _worker.ReportProgress(0, "Начало создания архива...");

                // Вариант с прогрессом (медленнее, но с отчетом о прогрессе)
                using (var archive = ZipFile.Open(_zipPath, ZipArchiveMode.Create))
                {
                    var files = Directory.GetFiles(_sourcePath, "*", SearchOption.AllDirectories);
                    double totalFiles = files.Length;
                    int processed = 0;

                    foreach (var file in files)
                    {
                        if (_worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        var relativePath = file.Substring(_sourcePath.Length + 1);
                        archive.CreateEntryFromFile(file, relativePath);

                        processed++;
                        int percent = (int)((processed / totalFiles) * 100);
                        
                    }
                }

                e.Result = "Архивация завершена успешно!";
            }
            catch (Exception ex)
            {
                // Удаляем частично созданный архив при ошибке
                if (File.Exists(_zipPath))
                {
                    try { File.Delete(_zipPath); } catch { }
                }
                throw new Exception($"Ошибка при создании архива: {ex.Message}", ex);
            }
        }


        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("Операция отменена пользователем");
            }
            else
            {
                MessageBox.Show(e.Result?.ToString());
            }
        }

        public void CancelBackup()
        {
            if (_worker != null && _worker.IsBusy)
            {
                _worker.CancelAsync();
            }
        }
    }
}
