using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using QuizWebsite.Core.Models;

namespace QuizWebsite.Data
{
    public static class UserGrabber
    {
        public static User GetUserByCredentials(string email, string hashedPassword)
        {
            var connectionString = ConnectionBucket.ConnectionString;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = @"
                        SELECT id, username, email
                            FROM [user]
                            WHERE email = @email AND
                            hashed_password = @hashed_password;
                    ";
                    sqlCommand.Parameters.AddWithValue(parameterName: "email", value: email);
                    sqlCommand.Parameters.AddWithValue(parameterName: "hashed_password", value: hashedPassword);

                    using (var sqlReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlReader.Read())
                        {
                            var user = new User();
                            user.Id = (long)sqlReader[name: "id"];
                            user.Username = sqlReader[name: "username"].ToString();
                            user.Email = sqlReader[name: "email"].ToString();
                            return user;
                        }
                        else
                            return null;
                    }
                }
            }
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
                        SELECT id, username, email
                            FROM [user]
                            WHERE id = @id;
                    ";
                    sqlCommand.Parameters.AddWithValue(parameterName: "id", value: id);

                    using (var sqlReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlReader.Read())
                        {
                            var user = new User();
                            user.Id = (long)sqlReader[name: "id"];
                            user.Username = sqlReader[name: "username"].ToString();
                            user.Email = sqlReader[name: "email"].ToString();
                            return user;
                        }
                        else
                            return null;
                    }
                }
            }
        }

        public static void UpdatePassword(long id, string hashedPassword)
        {
            var connectionString = ConnectionBucket.ConnectionString;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = @"
                        UPDATE [user]
                            SET hashed_password = @hashed_password
                            WHERE id = @id
                    ";
                    sqlCommand.Parameters.AddWithValue(parameterName: "id", value: id);
                    sqlCommand.Parameters.AddWithValue(parameterName: "hashed_password", value: hashedPassword);

                    sqlCommand.ExecuteScalar();
                }
            }
        }
    }
}