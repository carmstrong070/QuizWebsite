using Microsoft.Data.SqlClient;
using QuizWebsite.Core.Models;

namespace QuizWebsite.Data
{
    public static class QuizAttemptHandler
    {
        public static long Do(QuizAttempt quizAttempt)
        {
            var connectionString = ConnectionBucket.ConnectionString;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = @"
                        INSERT INTO [dbo].[quiz_attempt]
                                   ([quiz_id]
                                   ,[user_id]
                                   ,[start_timestamp]
                                   ,[end_timestamp])
                             OUTPUT inserted.id
                             VALUES
                                   (@quiz_id
                                   ,@user_id
                                   ,@start_timestamp
                                   ,@end_timestamp)
                    ";
                    sqlCommand.Parameters.AddWithValue(parameterName: "quiz_id", value: quizAttempt.QuizId);
                    if (quizAttempt.UserId.HasValue)
                        sqlCommand.Parameters.AddWithValue(parameterName: "user_id", value: quizAttempt.UserId.Value);
                    else
                        sqlCommand.Parameters.AddWithValue(parameterName: "user_id", value: DBNull.Value);
                    sqlCommand.Parameters.AddWithValue(parameterName: "start_timestamp", value: quizAttempt.start_timestamp);
                    sqlCommand.Parameters.AddWithValue(parameterName: "end_timestamp", value: quizAttempt.end_timestamp);
                    var attemptId = (long)sqlCommand.ExecuteScalar();
                    return attemptId;
                }
            }
        }
    }
}