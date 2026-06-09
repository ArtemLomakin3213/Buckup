using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    class FileList
    {
        public int Id { get; set; }
        public int Id_users { get; set; }
        public string Name { get; set; }
        public string Files {  get; set; }
        public FileList()
        {
            Name = " ";
            Files = " ";
        }
        public override string ToString()
        {
            return $"{Id} - {Id_users} - {Name} - {Files}";
        }
    }
}
