﻿using Microsoft.Data.SqlClient;
using QuizWebsite.Core.Models;

namespace QuizWebsite.Data
{
    public static class UserGrabber
    {

        public static string GetUserSalt(string email)
        {
            var connectionString = ConnectionBucket.ConnectionString;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = @"
                        SELECT salt
                            FROM [user]
                            WHERE email = @email
                    ";
                    sqlCommand.Parameters.AddWithValue(parameterName: "email", value: email);

                    var salt = (string)sqlCommand.ExecuteScalar();
                    return salt;
                }
            }
        }

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

        public static void UpdatePassword(long id, string hashedPassword, string salt)
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
                            ,salt = @salt
                            WHERE id = @id
                    ";
                    sqlCommand.Parameters.AddWithValue(parameterName: "id", value: id);
                    sqlCommand.Parameters.AddWithValue(parameterName: "hashed_password", value: hashedPassword);
                    sqlCommand.Parameters.AddWithValue(parameterName: "salt", value: salt);

                    sqlCommand.ExecuteScalar();
                }
            }
        }

        public static void UpdateSalt(long id, byte[] salt)
        {
            var connectionString = ConnectionBucket.ConnectionString;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = @"
                        UPDATE [user]
                            SET salt = @salt
                            WHERE id = @id
                    ";
                    sqlCommand.Parameters.AddWithValue(parameterName: "id", value: id);
                    sqlCommand.Parameters.AddWithValue(parameterName: "hashed_password", value: salt);

                    sqlCommand.ExecuteScalar();
                }
            }
        }
    }
}