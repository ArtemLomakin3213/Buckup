using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class ServerList
    {
        public int Id { get; set; }
        public int Id_users { get; set; }
        public string Name { get; set; }
        public string ServerName { get; set; }
        public string Db { get; set; }
        public bool Is_auth { get; set; }  
        public string LoginUsers { get; set; }
        public string PasswordUsers { get; set; }


        public ServerList()
        {
            Id = 0;
            Name = string.Empty;
            ServerName = string.Empty;
            Db = string.Empty;
            Is_auth = false;  
            LoginUsers = string.Empty;
            PasswordUsers = string.Empty;

        }

        public override string ToString()
        {
            return $"Id : {Id} | Id_users: {Id_users} | User: {Id_users} | Server: {Name} | DB: {Db} | Auth: {Is_auth}";
        }
    }
}
