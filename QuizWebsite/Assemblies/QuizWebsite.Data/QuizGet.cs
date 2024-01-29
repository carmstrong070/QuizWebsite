﻿using Microsoft.Data.SqlClient;
using QuizWebsite.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWebsite.Data
{
    public static class QuizGet
    {
#if JAHREL
        public static string ConnectionString { get; set; } = "data source=JAHREL-PC\\SQLEXPRESS;initial catalog=QuizWebsite;persist security info=False;connect timeout=1000;integrated security=SSPI;encrypt=False";
#else
        public static string ConnectionString { get; set; } = "data source=BLD\\SQLEXPRESS;initial catalog=QuizWebsite;persist security info=False;connect timeout=1000;integrated security=SSPI;encrypt=False";
#endif
        public static Quiz GetQuiz()
        {
            var quiz = new Quiz();

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = @"
                        SELECT q.title, u.username
                            FROM quiz AS q
                                INNER JOIN [user] AS u ON q.author_user_id = u.id
                            WHERE q.id = @quiz_id;

                        SELECT que.id, que.question_text, que.question_type_id, qt.name AS question_type_name
                            FROM question AS que
                                INNER JOIN question_type AS qt ON que.question_type_id = qt.id
                            WHERE quiz_id = @quiz_id;

                        SELECT ao.id, ao.option_text, ao.is_correct, ao.question_id, qt.name AS question_type_name
                            FROM answer_option AS ao
                                INNER JOIN question AS q ON ao.question_id = q.id
                                INNER JOIN question_type AS qt ON q.question_type_id = qt.id
                            WHERE q.quiz_id = @quiz_id;

                        SELECT at.answer_text, at.question_id, qt.name AS question_type_name
                            FROM answer_text AS at
                                INNER JOIN question AS q ON at.question_id = q.id
                                INNER JOIN question_type AS qt ON q.question_type_id = qt.id
                            WHERE q.quiz_id = @quiz_id;
                    ";
                    sqlCommand.Parameters.AddWithValue(parameterName: "quiz_id", value: 4);

                    using (var sqlReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlReader.Read())
                        {
                            quiz.Title = sqlReader[name: "title"].ToString();
                            quiz.Author = sqlReader[name: "username"].ToString();
                        }

                        sqlReader.NextResult();

                        while (sqlReader.Read())
                        {
                            string questionTypeName = sqlReader["question_type_name"].ToString();

                            switch (questionTypeName)
                            {
                                case "single_select":
                                case "multi_select":
                                    var selectQuestion = new SelectQuestion();
                                    selectQuestion.QuestionId = (long)sqlReader["id"];
                                    selectQuestion.QuestionText = sqlReader["question_text"].ToString();
                                    selectQuestion.QuestionTypeName = sqlReader["question_type_name"].ToString();
                                    quiz.Questions.Add(selectQuestion);
                                    break;
                                case "free_response":
                                case "fill_in_blank":
                                    var textQuestion = new TextQuestion();
                                    textQuestion.QuestionId = (long)sqlReader["id"];
                                    textQuestion.QuestionText = sqlReader["question_text"].ToString();
                                    textQuestion.QuestionTypeName = sqlReader["question_type_name"].ToString();
                                    quiz.Questions.Add(textQuestion);
                                    break;
                            }
                        }

                        sqlReader.NextResult();

                        while (sqlReader.Read())
                        {
                            var answerOption = new AnswerOption();
                            answerOption.Id = (long)sqlReader["id"];
                            answerOption.OptionText = sqlReader[name: "option_text"].ToString();
                            answerOption.IsCorrect = (bool)sqlReader[name: "is_correct"];
                            long associatedQuestionId = (long)sqlReader[name: "question_id"];

                            ((SelectQuestion)quiz.Questions.FirstOrDefault(x => x.QuestionId == associatedQuestionId)).AnswerOptions.Add(answerOption);
                        }

                        sqlReader.NextResult();

                        while (sqlReader.Read())
                        {
                            long associatedQuestionId = (long)sqlReader[name: "question_id"];

                            var associatedQuestion = quiz.Questions.FirstOrDefault(x => x.QuestionId == associatedQuestionId);
                            ((TextQuestion)associatedQuestion).AnswerText = sqlReader["answer_text"].ToString();
                            //((TextQuestion)quiz.Questions.FirstOrDefault(x => x.QuestionId == associatedQuestionId)).AnswerText = sqlReader["answer_text"].ToString();
                        }
                    }
                }
            }

            return quiz;
        }
    }
}