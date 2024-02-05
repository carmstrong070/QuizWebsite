USE [QuizWebsite]
GO

ALTER TABLE Quiz
ADD time_limit_in_seconds bigint NULL;

ALTER TABLE [user]
ADD salt VARCHAR(36) NOT NULL DEFAULT 'Salt-me-baby';