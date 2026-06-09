using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using WpfApp1.Interface;
using WpfApp1.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Text.RegularExpressions;

namespace WpfApp1.DB
{
    //Класс для работы с пользователями
    class DbServiceUsers : IServiceUsers
    {
        private readonly string _connectionString;
        public DbServiceUsers()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        //Проверка наличия почты в базе
        public bool CheckEmailBase(Users user)
        {
            string commandString = "SELECT COUNT(*) FROM [Users] WHERE email = @Email";
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new(commandString, connection))
            {
                command.Parameters.AddWithValue("@Email", user.Email);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
        //Получение id по имени пользователя
        public int CheckIdBase(string Username)
        {
            string commandString = "SELECT id FROM Users WHERE username = @username;";
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new(commandString, connection))
            {
                command.Parameters.AddWithValue("@username", Username);
                object result = command.ExecuteScalar();
                int user_id = result != null ? Convert.ToInt32(result) : 0;
                return user_id;
            }
        }
        //Проверка наличия пользователя по username
        public bool CheckUserBase(Users user)
        {
            string commandString = "SELECT COUNT(*) FROM [Users] WHERE username = @username";
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new(commandString, connection))
            {
                command.Parameters.AddWithValue("@Username", user.Username);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
        //Авторизация пользователя
        public bool GetToName(string Username, string Password)
        {
            string commandString = "SELECT COUNT(*) FROM [Users] WHERE username = @Username AND password = @Password;";
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new(commandString, connection))
            {
                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@Password", Password);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
        //Регистрация пользователя
        public void Registration(Users user)
        {
            string commandString = "INSERT INTO [Users] (username, password, email) VALUES (@Username, @Password, @Email)";
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new(commandString, connection))
            {
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.ExecuteNonQuery();
            }
        }

        //Подключение к БД
        private SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
