# Quiz Website - Roadmap

---

> you wanna make a website?
> \- Christ

## Dev Plan

### Section 1 - Proof-of-concept

- [x] 1.1 Create C# solution
- [x] 1.2 Mockup a quiz webpage with Razor .cshtml
- [x] 1.3 Create SQL DB
- [x] 1.4 Create DB tables
- [x] 1.5 Add mock quiz data to DB
- [x] 1.6 SQL query for one quiz
- [x] 1.7 Render mock quiz data on quiz webpage

### Section 2 - User Interactions

- [x] 2.1 Submitting webpage
- [x] 2.2 Bind submitted values to C# models
- [x] 2.3 Refactor!
- [x] 2.4 Calculating quiz score in webpage codebehind
- [x] 2.5 Refactor to MVC!
- [x] 2.6 Displaying quiz score on webpage

### Section 3 - Site Navigation

- [x] 3.1 SQL query for all quizzes
- [x] 3.2 Render quiz on quiz listing webpage
- [x] 3.3 Wire up navigation to quiz webpage with URL query string
- [x] 3.4 Wire up navigation to quiz listing webpage

### Section 4 - Persistent Quiz Scoring (for anon)

- [x] 4.1 SQL insert query to quiz_attempt table
- [x] 4.2 QuizWebsite.Data class, "QuizAttempter" to fire SQL insert
- [x] 4.3 Controller wireup to "QuizAttempter"
- [x] 4.4 Serverside start timestamp on HttpGet, serverside end timestamp on HttpPost
- [x] 4.5 SQL insert query to question_response table
- [x] 4.6 "QuizAttempter" fire SQL insert to question_response
- [x] 4.7 Upgrade the Alex scoring algo to return some dict
- [ ] 4.? Clientside timer
- [ ] 4.? Frontend submitted elapsed time message
- [ ] 4.? Stat display on QuizPortal page

### Section 5 - Authentication

- JB making some shit up

## Future Sections

These are still in planning...

- Quiz editing form
- Accounts and more DB tables
- Advanced quiz features (question images, quiz stats)
- Advanced account features (profile update, account creation, site administration)
- Advanced community features (quiz economy, account poverty, achievements, rankings)