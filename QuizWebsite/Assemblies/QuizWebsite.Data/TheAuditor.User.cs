using Microsoft.Data.SqlClient;

namespace QuizWebsite.Data
{
    public static partial class TheAuditor
    {
        public static TimeSpan? GetTotalTimeQuizzing(long userId)
        {
            var connectionString = ConnectionBucket.ConnectionString;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = @"
                        SELECT SUM(DATEDIFF(SECOND, qa.start_timestamp, qa.end_timestamp)) AS total_seconds
                          FROM [dbo].[quiz_attempt] AS qa
                          WHERE qa.user_id = @user_id
                    ";
                    sqlCommand.Parameters.AddWithValue(parameterName: "user_id", value: userId);

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
