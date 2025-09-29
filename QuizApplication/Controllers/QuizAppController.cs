using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApplication.DataModels;

namespace QuizApplication.Controllers
{
    public class QuizAppController : Controller
    {
        private readonly AppDbContext _db;

        public QuizAppController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            Guid sessionId = Guid.NewGuid();
            HttpContext.Session.SetString("SessionId", sessionId.ToString());
            return View();
        }

        [HttpGet]
        public IActionResult GetQuestion(int index)
        {
            var question = _db.Questions
                .Include(q => q.Options)
                .Skip(index)
                .Take(1)
                .FirstOrDefault();

            if (question == null)
                return Json(new { end = true });

            return PartialView("_QuestionPartial", question);
        }

        [HttpPost]
        public IActionResult SubmitAnswer(int questionId, int selectedQuestionId)
        {
            var sessionId = Guid.Parse(HttpContext.Session.GetString("SessionId"));

            var userAnswer = new UserAnswer
            {
                QuestionId = questionId,
                SelectedOptionId = selectedQuestionId,
                SessionId = sessionId
            };

            _db.UserAnswers.Add(userAnswer);
            _db.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public IActionResult Result()
        {
            var sessionId = Guid.Parse(HttpContext.Session.GetString("SessionId"));

            var userAnswers = _db.UserAnswers
                .Where(u => u.SessionId == sessionId)
                .Include(u => u.SelectedOption)
                .ToList();

            int total = userAnswers.Count;
            //int correct = userAnswers.Count(u =>
            //_db.Options.First(o => o.Id == u.SelectedOptionId).IsCorrect);
            int correct = userAnswers.Count(u =>
            u.SelectedOption != null && u.SelectedOption.IsCorrect);

            ViewBag.Total = total;
            ViewBag.Correct = correct;

            return View();
        }
    }
}
