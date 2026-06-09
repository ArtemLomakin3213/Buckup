using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Interface
{
    interface IServiseTSqlBackup
    {
        Task<bool> CreateFullBackupAsync(string connectionString, string name);
        Task<bool> CreateDifferentialBackupAsync(string connectionString, string name);
        bool CreateTransactionLogBackupAsync(string connectionString, string name);
    }
}
