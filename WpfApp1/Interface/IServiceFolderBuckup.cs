using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;

namespace WpfApp1.Interface
{
    interface IServiceFolderBuckup
    {
        void  CreateBackupAsync(string name, string folder);
    }
}
