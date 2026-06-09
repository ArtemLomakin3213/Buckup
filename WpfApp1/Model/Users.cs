using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    class Users
    {
        private int Id {  get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Users()
        {
            Username = " ";
            Password = " ";
            Email = " ";
        }
        public override string ToString()
        {
            return $"{Id} - {Username} - {Password} - {Email}";
        }
    }
}
