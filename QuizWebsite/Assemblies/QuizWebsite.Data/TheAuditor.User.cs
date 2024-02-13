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

                    var totalObj = sqlCommand.ExecuteScalar();
                    if (totalObj != null)
                        return new TimeSpan(0, 0, (int)totalObj);
                    else
                        return null;
                }
            }
        }

        public static decimal? GetAverageQuizScore(long userId)
        {
            var connectionString = ConnectionBucket.ConnectionString;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = @"
                        WITH cte_quiz_scores (quiz_score)
                        AS 
                        (
	                        SELECT (SUM(CAST(qr.answered_correctly AS decimal)) / COUNT(qr.id)) * 100 AS quiz_score
	                          FROM quiz_attempt AS qa
	                          INNER JOIN question_response AS qr ON qa.id = qr.quiz_attempt_id
	                          WHERE qa.user_id = @user_id
	                          GROUP BY qa.id
                        )
                        SELECT AVG(quiz_score) from cte_quiz_scores
                    ";
                    sqlCommand.Parameters.AddWithValue(parameterName: "user_id", value: userId);


                    var averageQuizScoreObj = sqlCommand.ExecuteScalar();
                    if (decimal.TryParse(averageQuizScoreObj.ToString(), out decimal result))
                        return result;
                    return null;
                }
            }
        }
    }
}