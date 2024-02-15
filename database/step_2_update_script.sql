USE [QuizWebsite]
GO

ALTER TABLE Quiz
ADD time_limit_in_seconds BIGINT NULL;
GO

ALTER TABLE [user]
ADD salt VARCHAR(36) NOT NULL DEFAULT 'Salt-me-baby';
GO

ALTER TABLE [user]
ADD created_timestamp DATETIME NOT NULL DEFAULT GETDATE()
	,social_security_number VARCHAR(11)
	,mothers_maiden_name VARCHAR(30)
	,pronouns VARCHAR(20)
    ,most_recent_successful_login_timestamp DATETIME
    ,most_recent_failed_login_timestamp DATETIME
	,failed_login_attempts INT
	,got_ban_hammer BIT NOT NULL DEFAULT 0
	,ban_hammer_timestamp DATETIME
	,hammered_by_user_id BIGINT;
GO

ALTER TABLE [user]
ADD is_admininater BIT NOT NULL DEFAULT 0
GO