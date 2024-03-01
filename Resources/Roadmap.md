# Quiz Website - Roadmap

---

> you wanna make a website?
> \- Christ

## Dev Plan

### Section 1 - Proof-of-concept

> Meet Visual Studio and SQL Management Studio.

- [x] 1.1 Create C# solution
- [x] 1.2 Mockup a quiz webpage with Razor .cshtml
- [x] 1.3 Create SQL DB
- [x] 1.4 Create DB tables
- [x] 1.5 Add mock quiz data to DB
- [x] 1.6 SQL query for one quiz
- [x] 1.7 Render mock quiz data on quiz webpage

### Section 2 - User Interactions

> Where there is data, there are forms.

- [x] 2.1 Submitting webpage
- [x] 2.2 Bind submitted values to C# models
- [x] 2.3 Refactor!
- [x] 2.4 Calculating quiz score in webpage codebehind
- [x] 2.5 Refactor to MVC!
- [x] 2.6 Displaying quiz score on webpage

### Section 3 - Site Navigation

> Do you know da wae?

- [x] 3.1 SQL query for all quizzes
- [x] 3.2 Render quiz on quiz listing webpage
- [x] 3.3 Wire up navigation to quiz webpage with URL query string
- [x] 3.4 Wire up navigation to quiz listing webpage

### Section 4 - Persistent Quiz Attempts (for anon)

> Laying the foundation for stats.

- [x] 4.1 SQL insert query to quiz_attempt table
- [x] 4.2 QuizWebsite.Data class, "QuizAttempter" to fire SQL insert
- [x] 4.3 Controller wireup to "QuizAttempter"
- [x] 4.4 Serverside start timestamp on HttpGet, serverside end timestamp on HttpPost
- [x] 4.5 SQL insert query to question_response table
- [x] 4.6 "QuizAttempter" fire SQL insert to question_response
- [x] 4.7 Upgrade the Alex scoring algo to return some dict
- [x] 4.8 Global stats display on QuizPortal page
- [ ] 4.? Clientside timer
- [ ] 4.? Frontend submitted elapsed time message

### Section 5 - Authentication

> Jump through Microsoft security hoops.

- [x] 5.1 Add switch auth on in Program.cs
- [x] 5.2 Add configuration to appsettings.json
- [x] 5.3 Add authentication system and models
- [x] 5.4 Data layer queries for authenticating credentials
- [x] 5.5 Webpage and view model for login
- [x] 5.6 Controller wireup to authentication sign in
- [x] 5.7 Controller wireup to authentication sign out

### Section 6 - Basic Account Features

> Everyone liked that.

- [x] 6.1 Webpage and view model for user sign up
- [x] 6.2 Data layer queries for creating a new user
- [x] 6.3 Controller wireup to sign up
- [x] 6.4 Webpage and view model for user to change account details
- [x] 6.5 Data layer queries for updating an existing user
- [x] 6.6 Controller wireup for user to edit their account details
- [x] 6.7 Refactor layouts for authenticated user vs anon user

### Section 7 - Stats

> Metrics for e-peen size.

- [x] 7.1 Display user's personal stats
  - [x] 7.1a Time spent in quizzes overall
  - [x] 7.1b Average quiz score
  - [x] 7.1c Average questions correct
  - [x] 7.1d Latest quiz taken
- [x] 7.2 Display quiz stats after completion
  - [x] 7.2a Display global stats for overall score after quiz completion
  - [x] 7.2b Display global stats for each question after quiz completion
- [x] 7.3 Display stats in quiz portal (i.e., Number of completions)

### Section 8 - Admins

> Build more muscle memory for the dev stack.

- [x] 8.0 Create Admin role
- [x] 8.1 Create view to see all users
  - [x] 8.1a Make a sweet username search filter for user table
- [x] 8.2 Create view to edit user
- [x] 8.3 Data layer queries to view/edit users
- [x] 8.4 Wire up privileged user edit modal "save changes" button
- [x] 8.5 Lock down admin pages

### Section 9 - Software Maintenance

> "We'll do that later" is now.

- [x] 9.0 Make JavaScript page-specific
  - [x] 9.0a Create separate .js files for admin, quiz, and site-wide
  - [x] 9.0b Frontend pages should only reference the files they need
  - [x] 9.0c Use minified .js files
  - [ ] 9.0d Create JavaScript namespace for Quiz Website functionality
- [x] 9.1 Refactor MVC
  - [x] 9.1a Come up with new sub-namespaces under Models, Views, and Controllers folders
  - [x] 9.1b Update references
  - [x] 9.1c Update routes
- [ ] 9.2 Add Service layer to the stack

  - [ ] 9.2a Move business logic and SqlConnection code into Service layer
  - [ ] 9.2b Wireup Web and Data layer to new Service layer
  - [ ] 9.2c Refactor Controller, UserManager, and UserHandler interactions

  ```
  CURRENT

    Create
    Web AdmininaterController -> Web Auth UserManager (Bus Logic) -> Data UserHandler

    Edit
    Web AdmininaterController (Bus Logic) -> Data UserHandler
  TARGET

    Create/Edit
    Web AdmininaterController -> Service (Bus Logic) -> Data UserHandler
    Web SignUpController -> Service (Bus Logic) -> Data UserHandler

    Sign In (No sign up, no edit)
    Web WhateverController -> Web Auth UserManager -> Service (Bus Logic) -> Data UserHandler
  ```

- [x] 9.3 Nuke unused authentication classes

### Section 10 - Prettify

> Paint this bitch.

- [ ] 10.0 Update Login page appearance
- [ ] 10.1 Update Quiz Portal page appearance
- [ ] 10.2 Update Quiz page appearance
- [ ] 10.3 Add user messaging system
- [ ] 10.x Add support for 404 errors
- [ ] 10.x Add support for 500 errors

### Section 11 - Final Boss

> An actual engineering problem.

- [ ] 11.1 Create quiz editing view
- [ ] 11.2 Data layer queries for creating a new quiz
- [ ] 11.3 Create view to look at all of a users quizzes

## Future Sections

These are still in planning...

- Quiz editing form
- Advanced quiz features (question images, quiz stats)
- Advanced account features (profile update, account creation, site administration)
- Advanced community features (quiz economy, account poverty, achievements, rankings)
- Recommend next quizzes to take (By author, favorite authors, tagging, etc...)

## Ideation

- Calculate average completion time for quizzes
  - Prominently display (averageCompletionTime x 0.75) to the quiz taker as the average completion time
- Personalized user stats
  - Average time spent per question
  - Closest better user on taken quizzes

## Stats

- Perfectionists (number of attempts with 100% correct responses) per quiz
- Average completion time
- Average score per attempt
- Fastest Perfect Score
- Biggest dipshit (most incorrect and slowest time)

## Deployment planning

Basic

- SQL DB hosting
  - DB backup schedule?
- IIS MVC hosting
  - Site admins?

For the Internet

- DNS Licensing
- Google SEO
