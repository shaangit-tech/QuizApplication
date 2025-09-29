namespace QuizApplication.DataModels
{
    public class UserAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int SelectedOptionId { get; set; }
        public Guid SessionId { get; set; }
        public Option SelectedOption { get; set; }
    }
}
