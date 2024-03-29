USE [QuizWebsite]
GO
SET IDENTITY_INSERT [dbo].[user] ON 

INSERT [dbo].[user] ([id], [username], [hashed_password], [email]) VALUES (1, N'username1', N'123456', N'email1@email.com')
INSERT [dbo].[user] ([id], [username], [hashed_password], [email]) VALUES (2, N'username2', N'!@#$%^', N'email2@email.com')
SET IDENTITY_INSERT [dbo].[user] OFF
GO
SET IDENTITY_INSERT [dbo].[quiz] ON 

INSERT [dbo].[quiz] ([id], [title], [author_user_id], [active], [created_timestamp]) VALUES (4, N'quiz1', 1, 1, CAST(N'2024-01-18T23:42:36.297' AS DateTime))
INSERT [dbo].[quiz] ([id], [title], [author_user_id], [active], [created_timestamp]) VALUES (5, N'quiz2', 1, 1, CAST(N'2024-01-18T23:42:49.587' AS DateTime))
INSERT [dbo].[quiz] ([id], [title], [author_user_id], [active], [created_timestamp]) VALUES (6, N'quiz3', 2, 1, CAST(N'2024-01-18T23:42:58.467' AS DateTime))
INSERT [dbo].[quiz] ([id], [title], [author_user_id], [active], [created_timestamp]) VALUES (7, N'quiz4', 2, 0, CAST(N'2024-01-18T23:43:06.500' AS DateTime))
SET IDENTITY_INSERT [dbo].[quiz] OFF
GO
SET IDENTITY_INSERT [dbo].[quiz_tag] ON 

INSERT [dbo].[quiz_tag] ([id], [quiz_id], [tag_text]) VALUES (1, 4, N'fucking dumb')
INSERT [dbo].[quiz_tag] ([id], [quiz_id], [tag_text]) VALUES (2, 5, N'nsfw')
INSERT [dbo].[quiz_tag] ([id], [quiz_id], [tag_text]) VALUES (3, 6, N'basically porn')
INSERT [dbo].[quiz_tag] ([id], [quiz_id], [tag_text]) VALUES (4, 7, N'n/a')
SET IDENTITY_INSERT [dbo].[quiz_tag] OFF
GO
SET IDENTITY_INSERT [dbo].[question_type] ON 

INSERT [dbo].[question_type] ([id], [name]) VALUES (1, N'single_select')
INSERT [dbo].[question_type] ([id], [name]) VALUES (2, N'multi_select')
INSERT [dbo].[question_type] ([id], [name]) VALUES (3, N'free_response')
INSERT [dbo].[question_type] ([id], [name]) VALUES (4, N'fill_in_blank')
SET IDENTITY_INSERT [dbo].[question_type] OFF
GO
SET IDENTITY_INSERT [dbo].[question] ON 

INSERT [dbo].[question] ([id], [quiz_id], [question_text], [question_type_id]) VALUES (1, 4, N'single select question', 1)
INSERT [dbo].[question] ([id], [quiz_id], [question_text], [question_type_id]) VALUES (2, 4, N'multi select question', 2)
INSERT [dbo].[question] ([id], [quiz_id], [question_text], [question_type_id]) VALUES (3, 4, N'free response question', 3)
INSERT [dbo].[question] ([id], [quiz_id], [question_text], [question_type_id]) VALUES (4, 4, N'fill in the blank question', 4)
INSERT [dbo].[question] ([id], [quiz_id], [question_text], [question_type_id]) VALUES (5, 5, N'q1', 1)
INSERT [dbo].[question] ([id], [quiz_id], [question_text], [question_type_id]) VALUES (6, 5, N'q2', 1)
INSERT [dbo].[question] ([id], [quiz_id], [question_text], [question_type_id]) VALUES (7, 5, N'q3', 1)
INSERT [dbo].[question] ([id], [quiz_id], [question_text], [question_type_id]) VALUES (8, 5, N'q4', 1)
INSERT [dbo].[question] ([id], [quiz_id], [question_text], [question_type_id]) VALUES (9, 6, N'only question', 3)
INSERT [dbo].[question] ([id], [quiz_id], [question_text], [question_type_id]) VALUES (10, 7, N'only question', 3)
SET IDENTITY_INSERT [dbo].[question] OFF
GO
SET IDENTITY_INSERT [dbo].[answer_text] ON 

INSERT [dbo].[answer_text] ([id], [question_id], [answer_text]) VALUES (1, 3, N'answer')
INSERT [dbo].[answer_text] ([id], [question_id], [answer_text]) VALUES (2, 4, N'answer')
INSERT [dbo].[answer_text] ([id], [question_id], [answer_text]) VALUES (3, 9, N'answer')
INSERT [dbo].[answer_text] ([id], [question_id], [answer_text]) VALUES (4, 10, N'answer')
SET IDENTITY_INSERT [dbo].[answer_text] OFF
GO
SET IDENTITY_INSERT [dbo].[answer_option] ON 

INSERT [dbo].[answer_option] ([id], [question_id], [option_text], [is_correct]) VALUES (1, 1, N'option 1 (correct answer)', 1)
INSERT [dbo].[answer_option] ([id], [question_id], [option_text], [is_correct]) VALUES (2, 1, N'option 2', 0)
INSERT [dbo].[answer_option] ([id], [question_id], [option_text], [is_correct]) VALUES (3, 1, N'option 3', 0)
INSERT [dbo].[answer_option] ([id], [question_id], [option_text], [is_correct]) VALUES (4, 1, N'option 4', 0)
INSERT [dbo].[answer_option] ([id], [question_id], [option_text], [is_correct]) VALUES (6, 2, N'option 1 (correct)', 1)
INSERT [dbo].[answer_option] ([id], [question_id], [option_text], [is_correct]) VALUES (7, 2, N'option 2 (correct)', 1)
INSERT [dbo].[answer_option] ([id], [question_id], [option_text], [is_correct]) VALUES (8, 2, N'option 3', 0)
INSERT [dbo].[answer_option] ([id], [question_id], [option_text], [is_correct]) VALUES (9, 2, N'option 4', 0)
INSERT [dbo].[answer_option] ([id], [question_id], [option_text], [is_correct]) VALUES (10, 2, N'option 5', 0)
INSERT [dbo].[answer_option] ([id], [question_id], [option_text], [is_correct]) VALUES (11, 2, N'option 6', 0)
INSERT [dbo].[answer_option] ([id], [question_id], [option_text], [is_correct]) VALUES (12, 5, N'option 1 (correct)', 1)
INSERT [dbo].[answer_option] ([id], [question_id], [option_text], [is_correct]) VALUES (14, 6, N'option 1 (wrong)', 0)
INSERT [dbo].[answer_option] ([id], [question_id], [option_text], [is_correct]) VALUES (15, 7, N'option 1 (correct)', 1)
INSERT [dbo].[answer_option] ([id], [question_id], [option_text], [is_correct]) VALUES (16, 8, N'option 1 (correct)', 1)
SET IDENTITY_INSERT [dbo].[answer_option] OFF
GO
