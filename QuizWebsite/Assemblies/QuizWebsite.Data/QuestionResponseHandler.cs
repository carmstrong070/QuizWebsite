using Microsoft.Data.SqlClient;
using QuizWebsite.Core.Models;

namespace QuizWebsite.Data
{
    public static class QuestionResponseHandler
    {
        public static void Insert(List<QuestionResponse> questionResponses)
        {
            var connectionString = ConnectionBucket.ConnectionString;

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    var valueList = new List<string>();
                    for (int i = 0; i < questionResponses.Count; i++)
                    {
                        string valueString = $"(@quiz_attempt_id{i}, @question_id{i}, @answered_correctly{i})";
                        valueList.Add(valueString);
                        sqlCommand.Parameters.AddWithValue(parameterName: $"quiz_attempt_id{i}", value: questionResponses[i].QuizAttemptId);
                        sqlCommand.Parameters.AddWithValue(parameterName: $"question_id{i}", value: questionResponses[i].QuestionId);
                        sqlCommand.Parameters.AddWithValue(parameterName: $"answered_correctly{i}", value: questionResponses[i].AnsweredCorrectly);
                    }
                    string formattedValueList = string.Join(",", valueList);

                    sqlCommand.CommandText = $@"
                        INSERT INTO [dbo].[question_response]
                                   ([quiz_attempt_id]
                                   ,[question_id]
                                   ,[answered_correctly])
                             VALUES
                                   {formattedValueList}
                    ";

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}