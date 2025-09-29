# Quiz App - ASP.NET Core MVC (with jQuery version too)

This is a quiz web application I built using **ASP.NET Core MVC**. I implemented two separate approaches:

1. **Standard MVC flow** — using Razor views, page reloads, and form submissions.
2. **AJAX version** — using jQuery to fetch questions dynamically via partial views.

I built both versions in the same project to demonstrate different ways to handle UI interaction and session tracking in ASP.NET Core.

---

## Features

- Load questions from a SQL Server database
- Two separate quiz flows:
  - MVC version: server-rendered, one question per page
  - jQuery version: dynamic loading with partial views
- Session tracking for quiz progress
- Result summary at the end
- Clean separation of models and DB contexts for each flow

---

## Folder Structure (Simplified)

/Controllers
QuizController.cs --> MVC version
QuizAppController.cs --> AJAX/jQuery version

/Models --> For MVC
Question.cs, Answer.cs, UserAnswer.cs, QuizDbContext.cs

/ViewModels --> For AJAX version
Question.cs, Option.cs, UserAnswer.cs, AppDbContext.cs

/Views
/Quiz --> MVC views
/QuizApp --> AJAX views

/wwwroot/js/quiz.js --> jQuery logic


I kept two separate folders for models because both flows use similar class names (`Question`, `UserAnswer`, etc.), and I wanted to avoid conflicts.

---

## How to Run the Project

1. Clone the repo:
   ```bash
   git clone https://github.com/shaangit-tech/quiz-app.git
   cd quiz-app

2. Make sure you have SQL Server and .NET 6 SDK installed.

3. Update your database connection strings in appsettings.json:

   "ConnectionStrings": {
  "dbcs": "Server=YOUR_SERVER;Database=Database1;Trusted_Connection=True;",
  "dbcs2": "Server=YOUR_SERVER;Database=CoDb;Trusted_Connection=True;"
}

4. Run the app:
dotnet run

5. Open browser and test:

MVC version: https://localhost:5001/Quiz

AJAX version: https://localhost:5001/QuizApp


//Why I Built It This Way

I wanted to compare how the same quiz functionality could be implemented using:

-Traditional MVC (good for beginners and server-side rendering)

-jQuery + Partial Views (more dynamic, responsive feel)

//This helped me learn more about:

-Session state in ASP.NET Core

-Handling forms vs. AJAX calls

-Working with multiple DbContexts

-Keeping code organized when similar models are used in different ways


About Me

I'm a developer who enjoys learning by building real projects.
This app was part of my ASP.NET Core practice — and I decided to try both approaches in one codebase.

You can contact me via GitHub.
