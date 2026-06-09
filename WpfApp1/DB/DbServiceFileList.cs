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
    class DbServiceFileList : IServiceFileList
    {
        private readonly string _connectionString;
        public DbServiceFileList()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public void addList(FileList list)
        {
            string query = @"INSERT INTO Filelist (id_users, names, files)
                            VALUES (@Id_users, @Names, @Files)";
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id_users", list.Id_users);
                command.Parameters.AddWithValue("@Names", list.Name);
                command.Parameters.AddWithValue("@Files", list.Files);
                command.ExecuteNonQuery();
            }

        }

        public void deleteFileList(int list_id)
        {
            string query = "DELETE FROM Filelist WHERE id = @list_id";
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@list_id", list_id);
                command.ExecuteNonQuery();
            }
        }
        public void updateFileTask(FileList list)
        {
            string query = "UPDATE Filelist SET names = @Names, files = @Files WHERE id = @Id";
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", list.Id);
                command.Parameters.AddWithValue("@Names", list.Name);
                command.Parameters.AddWithValue("@Files", list.Files);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception("Данные не были изменены");
                }
            }
        }

        public List<FileList> GetFileList(int user_id)
        {
            List<FileList> lists = new List<FileList>();
            string query = "SELECT * FROM Filelist Where id_users = @Id_users";
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id_users", user_id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FileList list = new FileList
                        {
                            Id = (int)reader["id"],
                            Id_users = (int)reader["id_users"],
                            Name = reader["names"].ToString(),
                            Files = reader["files"].ToString()
                        };
                        lists.Add(list);
                    }
                }
            }
            return lists;
        }

        public List<FileList> GetOneFileList(int list_id)
        {
            List<FileList> lists = new List<FileList>();
            string query = "SELECT * FROM FileList WHERE Id = @List_Id";
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@List_Id", list_id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FileList list = new FileList
                            {
                                Id = (int)reader["id"],
                                Name = reader["names"].ToString(),
                                Files = reader["files"].ToString()
                            };
                            lists.Add(list);
                        }
                    }
                }
            }
            return lists;
        }

        private SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
