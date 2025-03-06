using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using static BackendLab01.Pages.Summary;

namespace BackendLab01.Pages
{
    
    public class QuizModel : PageModel
    {
        private readonly IQuizUserService _userService;

        private readonly ILogger _logger;
        public QuizModel(IQuizUserService userService, ILogger<QuizModel> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [BindProperty]
        public string Question { get; set; }
        [BindProperty]
        public List<string> Answers { get; set; }

        [BindProperty] public String UserAnswer { get; set; } = string.Empty;
        
        [BindProperty]
        public int QuizId { get; set; }
        
        [BindProperty]
        public int ItemId { get; set; }
        
        [TempData]
        public string ErrorMessage { get; set; }
        
        public IActionResult OnGet(int quizId, int itemId)
        {
            QuizId = quizId;
            ItemId = itemId;
            var quiz = _userService.FindQuizById(quizId);
            if (quiz == null || quiz.Items.Count == 0 || itemId > quiz.Items.Count)
            {
                _logger.LogWarning("Quiz Id {QuizId} not found", quizId);
                return RedirectToPage("Summary", new {quizId = quizId, itemId = itemId});
            }
            var quizItem = quiz?.Items[itemId - 1];
            Question = quizItem?.Question;
            Answers = new List<string>();
            if (quizItem is not null)
            {
                Answers.AddRange(quizItem?.IncorrectAnswers);
                Answers.Add(quizItem?.CorrectAnswer);
            }
            var quizItemId = quiz.Items[itemId - 1];
            if (quizItemId != null)
            {
                Question = quizItemId?.Question;
                Answers = new List<string>(quizItemId.IncorrectAnswers) { quizItem.CorrectAnswer };
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(UserAnswer))
            {
                ErrorMessage = "Musisz odpowiedziec zanim przejdziesz dalej!";
                _logger.LogWarning("Uzytkonik nie podal odpowiedzi");
            }
            _userService.SaveUserAnswerForQuiz(
                quizId: QuizId, 
                userId: 1, 
                quizItemId: ItemId, 
                answer: UserAnswer
            );
            _logger.LogInformation("Zapisuję odpowiedź: QuizId={QuizId}, UserId={UserId}, ItemId={ItemId}, Answer={UserAnswer}", 
                QuizId, 1, ItemId, UserAnswer);
                return RedirectToPage("Item", new { quizId = QuizId, itemId = ItemId + 1 });
        }
    }
}
