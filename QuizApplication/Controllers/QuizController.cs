using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApplication.Helpers;
using QuizApplication.Models;

namespace QuizApplication.Controllers
{
    public class QuizController : Controller
    {
        private readonly QuizDbContext _db;

        public QuizController(QuizDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            HttpContext.Session.Clear(); //Reset
            return RedirectToAction("Question", new { id = 1 });
        }

        public IActionResult Question(int id)
        {
            var question = _db.Questions
                .Include(q => q.Answers)
                .FirstOrDefault(q => q.Id == id);

            if (question == null)
                return RedirectToAction("Result");

            return View(question);
        }

        [HttpPost]
        public IActionResult SubmitAnswer(int questionId, int selectedAnswerId)
        {
            var answer = _db.Answers.FirstOrDefault(a => a.Id == selectedAnswerId);
            bool isCorrect = answer != null && answer.IsCorrect;

            var userAnswers = HttpContext.Session.GetObject<List<UserAnswer>>("UserAnswers") ?? new List<UserAnswer>();

            userAnswers.Add(new UserAnswer
            {
                QuestionId = questionId,
                SelectedAnswerId = selectedAnswerId,
                IsCorrect = isCorrect
            });

            HttpContext.Session.SetObject("UserAnswers", userAnswers);

            return RedirectToAction("Question", new { id = questionId + 1 });
        }

        public IActionResult Result()
        {
            var userAnswers = HttpContext.Session.GetObject<List<UserAnswer>>("UserAnswers") ?? new List<UserAnswer>();

            var questionIds = userAnswers.Select(x => x.QuestionId).ToList();
            var questions = _db.Questions
                .Include(q => q.Answers)
                .Where(q => questionIds.Contains(q.Id))
                .ToList();

            ViewBag.UserAnswers = userAnswers;
            return View(questions);
        }
    }
}
