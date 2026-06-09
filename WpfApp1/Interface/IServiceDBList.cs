using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;

namespace WpfApp1.Interface
{
    interface IServiceDbList
    {
        void addServerList(ServerList list);
        void deleteServerList(int list_id);
        void updateServerList(ServerList list);
        List<ServerList> GetServerList(int user_id);
        List<ServerList> GetOneServerList(int list_id);
    }
}
