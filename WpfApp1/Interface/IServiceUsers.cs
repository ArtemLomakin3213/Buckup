using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WpfApp1.Model;

namespace WpfApp1.Interface
{
    internal interface IServiceUsers
    {
        bool GetToName(string Username, string Password);
        void Registration(Users user);
        bool CheckUserBase(Users user);
        bool CheckEmailBase(Users user);
        int CheckIdBase(string Username);

    }  
}
