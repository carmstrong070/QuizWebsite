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
                              ,got_ban_hammer
                              ,hammer_timestamp
                              ,hammered_by_user_id
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
                            user.GotBanHammer = (bool)sqlReader[name: "got_ban_hammer"];
                            if (sqlReader[name: "hammer_timestamp"] != DBNull.Value)
                                user.HammerTimestamp = (DateTime)sqlReader[name: "hammer_timestamp"];
                            if (sqlReader[name: "hammered_by_user_id"] != DBNull.Value)
                                user.HammeredByUserId = (long)sqlReader[name: "hammered_by_user_id"];
                            users.Add(user);
                        }
                    }
                }
            }
            return users;
        }

        public static User GetUserById(long id)
        {
            var connectionString = ConnectionBucket.ConnectionString;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = @"
                        SELECT username
                               ,email
                               ,created_timestamp
                               ,is_admininater 
                               ,got_ban_hammer
                               ,hammer_timestamp
                               ,hammered_by_user_id
                            FROM [user]
                            WHERE id = @id;
                    ";
                    sqlCommand.Parameters.AddWithValue(parameterName: "id", value: id);

                    using (var sqlReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlReader.Read())
                        {
                            var user = new User();
                            user.Id = id;
                            user.Username = sqlReader[name: "username"].ToString();
                            user.Email = sqlReader[name: "email"].ToString();
                            user.CreatedTimestamp = (DateTime)sqlReader[name: "created_timestamp"];
                            user.IsAdmininater = (bool)sqlReader[name: "is_admininater"];
                            user.GotBanHammer = (bool)sqlReader[name: "got_ban_hammer"];
                            if (sqlReader[name: "hammer_timestamp"] != DBNull.Value)
                                user.HammerTimestamp = (DateTime)sqlReader[name: "hammer_timestamp"];
                            if (sqlReader[name: "hammered_by_user_id"] != DBNull.Value)
                                user.HammeredByUserId = (long)sqlReader[name: "hammered_by_user_id"];
                            return user;
                        }
                        else
                            return null;
                    }
                }
            }
        }
        public static void Ban(long id, long admininaterId)
        {
            var connectionString = ConnectionBucket.ConnectionString;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = @"
                        UPDATE [user]
                            SET got_ban_hammer = 1
                                ,hammer_timestamp = GETDATE()
                                ,hammered_by_user_id = @hammered_by_user_id
                            WHERE id = @id
                    ";

                    sqlCommand.Parameters.AddWithValue(parameterName: "id", value: id);
                    sqlCommand.Parameters.AddWithValue(parameterName: "hammered_by_user_id", value: admininaterId);

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        public static void Unban(long id)
        {
            var connectionString = ConnectionBucket.ConnectionString;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = @"
                        UPDATE [user]
                            SET got_ban_hammer = 0
                                ,hammer_timestamp = NULL
                                ,hammered_by_user_id = NULL
                            WHERE id = @id
                    ";

                    sqlCommand.Parameters.AddWithValue(parameterName: "id", value: id);

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
