using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Interface;
using WpfApp1.Model;

namespace WpfApp1.DB
{
    internal class DbServiseDBlist : IServiceDbList
    {
        private readonly string _connectionString;
        public DbServiseDBlist()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void addServerList(ServerList list)
        {
            string query = @"INSERT INTO ServerList (id_users,names,serverName,db,is_auth,loginUsers,passwordUsers) 
                            VALUES (@Id_users,@Names, @ServerName, @Db, @Is_auth, @LoginUsers, @PasswordUsers)";
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id_users", list.Id_users);
                command.Parameters.AddWithValue("@Names", list.Name);
                command.Parameters.AddWithValue("@ServerName", list.ServerName);
                command.Parameters.AddWithValue("@Db", list.Db);
                command.Parameters.AddWithValue("@Is_auth", list.Is_auth);
                command.Parameters.AddWithValue("@LoginUsers", list.LoginUsers);
                command.Parameters.AddWithValue("@PasswordUsers", list.PasswordUsers);
                command.ExecuteNonQuery();
            }
        }

        public void deleteServerList(int list_id)
        {
            string query = "DELETE FROM serverlist WHERE id = @List_id";
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@List_id", list_id);
                command.ExecuteNonQuery();
            }
        }

        public List<ServerList> GetOneServerList(int list_id)
        {
            List<ServerList> lists = new List<ServerList>();
            string query = "SELECT * FROM serverList WHERE id = @List_Id";
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@List_Id", list_id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ServerList list = new ServerList
                            {
                                Id = (int)reader["id"],
                                Id_users = (int)reader["id_users"],
                                Name = reader["names"].ToString(),
                                ServerName = reader["serverName"].ToString(),
                                Db = reader["db"].ToString(),
                                Is_auth = reader["is_auth"] as bool? ?? false,
                                LoginUsers = reader["loginUsers"].ToString(),
                                PasswordUsers = reader["passwordUsers"].ToString()
                            };
                            lists.Add(list);
                        }
                    }
                }
            }
            return lists;
        }

        public List<ServerList> GetServerList(int user_id)
        {
            List<ServerList> lists = new List<ServerList>();
            string query = "SELECT * FROM ServerList Where id_users = @Id_users";
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id_users", user_id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ServerList list = new ServerList
                        {
                            Id = (int)reader["id"],
                            Id_users = (int)reader["id_users"],
                            Name = reader["names"].ToString(),
                            ServerName = reader["serverName"].ToString(),
                            Db = reader["db"].ToString(),
                            Is_auth = reader["is_auth"] as bool? ?? false,
                            LoginUsers= reader["loginUsers"].ToString(),
                            PasswordUsers = reader["passwordUsers"].ToString()  

                        };
                        lists.Add(list);
                    }
                }
            }
            return lists;
        }

        public void updateServerList(ServerList list)
        {
            string query = @"UPDATE ServerList SET names = @Names, serverName = @ServerName, db = @Db,
                            is_auth = @Is_auth, loginUsers = @LoginUsers,
                            passwordUsers = @PasswordUsers WHERE id = @Id";
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", list.Id);
                command.Parameters.AddWithValue("@Id_users", list.Id_users);
                command.Parameters.AddWithValue("@Names", list.Name);
                command.Parameters.AddWithValue("@ServerName", list.ServerName);
                command.Parameters.AddWithValue("@Db", list.Db);
                command.Parameters.AddWithValue("@Is_auth", list.Is_auth);
                command.Parameters.AddWithValue("@LoginUsers", list.LoginUsers);
                command.Parameters.AddWithValue("@PasswordUsers", list.PasswordUsers);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception("The data has not been update.");
                }
            }
        }
        private SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
