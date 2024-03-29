USE [master]
GO
/****** Object:  Database [QuizWebsite]    Script Date: 1/18/2024 11:40:48 PM ******/
CREATE DATABASE [QuizWebsite]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuizWebsite', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\QuizWebsite.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QuizWebsite_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\QuizWebsite_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [QuizWebsite] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuizWebsite].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuizWebsite] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuizWebsite] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuizWebsite] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuizWebsite] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuizWebsite] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuizWebsite] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QuizWebsite] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuizWebsite] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuizWebsite] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuizWebsite] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuizWebsite] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuizWebsite] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuizWebsite] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuizWebsite] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuizWebsite] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QuizWebsite] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuizWebsite] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuizWebsite] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuizWebsite] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuizWebsite] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuizWebsite] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuizWebsite] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuizWebsite] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QuizWebsite] SET  MULTI_USER 
GO
ALTER DATABASE [QuizWebsite] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuizWebsite] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuizWebsite] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuizWebsite] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QuizWebsite] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QuizWebsite] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [QuizWebsite] SET QUERY_STORE = ON
GO
ALTER DATABASE [QuizWebsite] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [QuizWebsite]
GO
/****** Object:  Table [dbo].[answer_option]    Script Date: 1/18/2024 11:40:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[answer_option](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[question_id] [bigint] NOT NULL,
	[option_text] [varchar](1000) NOT NULL,
	[is_correct] [bit] NOT NULL,
 CONSTRAINT [PK_answer_option] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[answer_text]    Script Date: 1/18/2024 11:40:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[answer_text](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[question_id] [bigint] NOT NULL,
	[answer_text] [varchar](50) NOT NULL,
 CONSTRAINT [PK_answer_text] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[question]    Script Date: 1/18/2024 11:40:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[question](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[quiz_id] [bigint] NOT NULL,
	[question_text] [varchar](1000) NOT NULL,
	[question_type_id] [tinyint] NOT NULL,
 CONSTRAINT [PK_quiz_questions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[question_response]    Script Date: 1/18/2024 11:40:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[question_response](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[quiz_attempt_id] [bigint] NOT NULL,
	[question_id] [bigint] NOT NULL,
	[answered_correctly] [bit] NOT NULL,
 CONSTRAINT [PK_question_response] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[question_type]    Script Date: 1/18/2024 11:40:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[question_type](
	[id] [tinyint] IDENTITY(1,1) NOT NULL,
	[name] [varchar](20) NOT NULL,
 CONSTRAINT [PK_quiz_question_type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[quiz]    Script Date: 1/18/2024 11:40:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[quiz](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[title] [varchar](100) NOT NULL,
	[author_user_id] [bigint] NOT NULL,
	[active] [bit] NOT NULL,
	[created_timestamp] [datetime] NOT NULL,
 CONSTRAINT [PK_quiz] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[quiz_attempt]    Script Date: 1/18/2024 11:40:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[quiz_attempt](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[quiz_id] [bigint] NOT NULL,
	[user_id] [bigint] NULL,
	[start_timestamp] [datetime] NOT NULL,
	[end_timestamp] [datetime] NOT NULL,
 CONSTRAINT [PK_quiz_attempt] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[quiz_tag]    Script Date: 1/18/2024 11:40:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[quiz_tag](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[quiz_id] [bigint] NOT NULL,
	[tag_text] [varchar](50) NOT NULL,
 CONSTRAINT [PK_quiz_tag] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 1/18/2024 11:40:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[hashed_password] [varchar](max) NOT NULL,
	[email] [varchar](100) NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[quiz] ADD  CONSTRAINT [DF_quiz_active]  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[quiz] ADD  CONSTRAINT [DF_quiz_created_timestamp]  DEFAULT (getdate()) FOR [created_timestamp]
GO
ALTER TABLE [dbo].[quiz_attempt] ADD  CONSTRAINT [DF_quiz_attempt_start_timestamp]  DEFAULT (getdate()) FOR [start_timestamp]
GO
ALTER TABLE [dbo].[answer_option]  WITH CHECK ADD  CONSTRAINT [FK_answer_option_question_id] FOREIGN KEY([question_id])
REFERENCES [dbo].[question] ([id])
GO
ALTER TABLE [dbo].[answer_option] CHECK CONSTRAINT [FK_answer_option_question_id]
GO
ALTER TABLE [dbo].[answer_text]  WITH CHECK ADD  CONSTRAINT [FK_answer_text_question_id] FOREIGN KEY([question_id])
REFERENCES [dbo].[question] ([id])
GO
ALTER TABLE [dbo].[answer_text] CHECK CONSTRAINT [FK_answer_text_question_id]
GO
ALTER TABLE [dbo].[question]  WITH CHECK ADD  CONSTRAINT [FK_question_question_type_id] FOREIGN KEY([question_type_id])
REFERENCES [dbo].[question_type] ([id])
GO
ALTER TABLE [dbo].[question] CHECK CONSTRAINT [FK_question_question_type_id]
GO
ALTER TABLE [dbo].[question]  WITH CHECK ADD  CONSTRAINT [FK_question_quiz_id] FOREIGN KEY([quiz_id])
REFERENCES [dbo].[quiz] ([id])
GO
ALTER TABLE [dbo].[question] CHECK CONSTRAINT [FK_question_quiz_id]
GO
ALTER TABLE [dbo].[question_response]  WITH CHECK ADD  CONSTRAINT [FK_question_response_question_id] FOREIGN KEY([question_id])
REFERENCES [dbo].[question] ([id])
GO
ALTER TABLE [dbo].[question_response] CHECK CONSTRAINT [FK_question_response_question_id]
GO
ALTER TABLE [dbo].[question_response]  WITH CHECK ADD  CONSTRAINT [FK_question_response_quiz_attempt_id] FOREIGN KEY([quiz_attempt_id])
REFERENCES [dbo].[quiz_attempt] ([id])
GO
ALTER TABLE [dbo].[question_response] CHECK CONSTRAINT [FK_question_response_quiz_attempt_id]
GO
ALTER TABLE [dbo].[quiz]  WITH CHECK ADD  CONSTRAINT [FK_quiz_author_user_id] FOREIGN KEY([author_user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[quiz] CHECK CONSTRAINT [FK_quiz_author_user_id]
GO
ALTER TABLE [dbo].[quiz_attempt]  WITH CHECK ADD  CONSTRAINT [FK_quiz_attempt_quiz_id] FOREIGN KEY([quiz_id])
REFERENCES [dbo].[quiz] ([id])
GO
ALTER TABLE [dbo].[quiz_attempt] CHECK CONSTRAINT [FK_quiz_attempt_quiz_id]
GO
ALTER TABLE [dbo].[quiz_attempt]  WITH CHECK ADD  CONSTRAINT [FK_quiz_attempt_user_id] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[quiz_attempt] CHECK CONSTRAINT [FK_quiz_attempt_user_id]
GO
ALTER TABLE [dbo].[quiz_tag]  WITH CHECK ADD  CONSTRAINT [FK_quiz_tag_quiz_id] FOREIGN KEY([quiz_id])
REFERENCES [dbo].[quiz] ([id])
GO
ALTER TABLE [dbo].[quiz_tag] CHECK CONSTRAINT [FK_quiz_tag_quiz_id]
GO
USE [master]
GO
ALTER DATABASE [QuizWebsite] SET  READ_WRITE 
GO
