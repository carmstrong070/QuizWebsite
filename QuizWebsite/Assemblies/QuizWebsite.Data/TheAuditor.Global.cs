using Microsoft.Data.SqlClient;

namespace QuizWebsite.Data
{
    public static partial class TheAuditor
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

        /// <summary>
        /// gets global average score for a quiz
        /// </summary>
        /// <param name="quizId"></param>
        /// <returns></returns>
        public static decimal? GetAverageQuizScore(long quizId)
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
	                            WHERE qa.quiz_id = @quiz_id
	                            GROUP BY qa.id
                        )
                        SELECT CAST(AVG(quiz_score) AS decimal(5,4)) from cte_quiz_scores
                    ";
                    sqlCommand.Parameters.AddWithValue(parameterName: "quiz_id", value: quizId);


                    var averageQuizScoreObj = sqlCommand.ExecuteScalar();
                    if (decimal.TryParse(averageQuizScoreObj.ToString(), out decimal result))
                        return result;
                    return null;
                }
            }
        }

        public static Dictionary<long, decimal> GetAverageQuestionScore(long quizId)
        {
            var AverageQuestionScore = new Dictionary<long, decimal>();

            var connectionString = ConnectionBucket.ConnectionString;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = @"
                        SELECT question_id
                              ,SUM(CAST(answered_correctly AS decimal)) / COUNT(question_id) as percent_correct
                          FROM question_response as qr
                          INNER JOIN quiz_attempt AS qa ON qa.id = qr.quiz_attempt_id
                          WHERE qa.quiz_id = @quiz_id
                          GROUP BY question_id
                    ";
                    sqlCommand.Parameters.AddWithValue(parameterName: "quiz_id", value: quizId);

                    using (var sqlReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlReader.Read())
                        {
                            AverageQuestionScore.Add((long)sqlReader[name: "question_id"], (decimal)sqlReader[name: "percent_correct"]);
                        }
                    }
                }
            }
            return AverageQuestionScore;
        }
    }
}
