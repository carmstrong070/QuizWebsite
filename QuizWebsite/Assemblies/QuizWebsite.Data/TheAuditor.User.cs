using Microsoft.Data.SqlClient;
using QuizWebsite.Core.Models;

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
	                        SELECT SUM(CAST(qr.answered_correctly AS decimal)) / COUNT(qr.id) AS quiz_score
	                          FROM quiz_attempt AS qa
	                          INNER JOIN question_response AS qr ON qa.id = qr.quiz_attempt_id
	                          WHERE qa.user_id = @user_id
	                          GROUP BY qa.id
                        )
                        SELECT CAST(AVG(quiz_score) AS decimal(5,4)) from cte_quiz_scores
                    ";
                    sqlCommand.Parameters.AddWithValue(parameterName: "user_id", value: userId);


                    var averageQuizScoreObj = sqlCommand.ExecuteScalar();
                    if (decimal.TryParse(averageQuizScoreObj.ToString(), out decimal result))
                        return result;
                    return null;
                }
            }
        }

        public static decimal? GetOverallQuestionsCorrect(long userId)
        {
            var connectionString = ConnectionBucket.ConnectionString;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = @"
                        SELECT CAST((SUM(CAST(qr.answered_correctly AS decimal)) /  COUNT(qr.id)) AS decimal(5,4)) AS percent_correct
	                        FROM quiz_attempt AS qa
	                        INNER JOIN question_response AS qr ON qa.id = qr.quiz_attempt_id
	                        WHERE qa.user_id = @user_id
                    ";
                    sqlCommand.Parameters.AddWithValue(parameterName: "user_id", value: userId);


                    var overallQuestionsCorrect = sqlCommand.ExecuteScalar();
                    if (decimal.TryParse(overallQuestionsCorrect.ToString(), out decimal result))
                        return result;
                    return null;
                }
            }
        }

        public static Quiz GetLastQuizTaken(long userId)
        {
            var connectionString = ConnectionBucket.ConnectionString;
            var quiz = new Quiz();

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = @"
                        SELECT TOP (1) q.id, q.title, u.username, q.created_timestamp, q.time_limit_in_seconds
                          FROM quiz_attempt AS qa
                          INNER JOIN quiz AS q ON qa.quiz_id = q.id
                          INNER JOIN [user] AS u ON q.author_user_id = u.id
                          WHERE user_id = @user_id
                          ORDER BY end_timestamp DESC
                    ";
                    sqlCommand.Parameters.AddWithValue(parameterName: "user_id", value: userId);


                    using (var sqlReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlReader.Read())
                        {
                            quiz.QuizId = (long)sqlReader[name: "id"];
                            quiz.Title = sqlReader[name: "title"].ToString();
                            quiz.Author = sqlReader[name: "username"].ToString();
                            quiz.CreatedTimestamp = (DateTime)sqlReader[name: "created_timestamp"];

                        }
                    }
                }
            }
            return quiz;
        }
    }
}