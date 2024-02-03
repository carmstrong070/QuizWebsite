USE [QuizWebsite]
GO

ALTER TABLE Quiz
ADD time_limit_in_seconds bigint NULL;