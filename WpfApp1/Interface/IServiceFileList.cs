using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using WpfApp1.Model;

namespace WpfApp1.Interface
{
    interface IServiceFileList
    {
        void addList(FileList list);
        void deleteFileList(int list_id);
        void updateFileTask(FileList list);
        List<FileList> GetFileList(int user_id);
        List<FileList> GetOneFileList(int list_id);
    }
}
