using Microsoft.Data.SqlClient;
using QuizWebsite.Core.Models;

namespace QuizWebsite.Data
{
    public static class AdmininaterTools
    {
        public static List<User> GetAllUsers(string username = null)
        {
            var users = new List<User>();

            var connectionString = ConnectionBucket.ConnectionString;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = $@"
                        SELECT id
                              ,username
                              ,email
                              ,created_timestamp
                              ,is_admininater
                          FROM [user]
                          {(string.IsNullOrWhiteSpace(username) ? "" : "WHERE username LIKE '%' + @username + '%'")}
                    ";

                    if (!string.IsNullOrWhiteSpace(username))
                        sqlCommand.Parameters.AddWithValue(parameterName: "username", value: username);

                    using (var sqlReader = sqlCommand.ExecuteReader())
                    {

                        while (sqlReader.Read())
                        {
                            var user = new User();
                            user.Id = (long)sqlReader[name: "id"];
                            user.Username = sqlReader[name: "username"].ToString();
                            user.Email = sqlReader[name: "email"].ToString();
                            user.CreatedTimestamp = (DateTime)sqlReader[name: "created_timestamp"];
                            user.IsAdmininater = (bool)sqlReader[name: "is_admininater"];
                            users.Add(user);
                        }
                    }
                }
            }
            return users;
        }
    }
}
