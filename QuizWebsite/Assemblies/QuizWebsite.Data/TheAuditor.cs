using Microsoft.Data.SqlClient;

namespace QuizWebsite.Data
{
    public static class TheAuditor
    {
        public static TimeSpan? GetAverageCompletionTime(long quizId)
        {
            var connectionString = ConnectionBucket.ConnectionString;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = @"
                        SELECT AVG(DATEDIFF(SECOND, qa.start_timestamp, qa.end_timestamp)) AS elapsed_seconds
                          FROM [dbo].[quiz_attempt] AS qa
                          WHERE qa.quiz_id = @quiz_id
                          GROUP BY quiz_id
                    ";
                    sqlCommand.Parameters.AddWithValue(parameterName: "quiz_id", value: quizId);

                    var averageObj = sqlCommand.ExecuteScalar();
                    if (averageObj != null)
                        return new TimeSpan(0, 0, (int)averageObj);
                    else
                        return null;
                }
            }
        }
    }
}
