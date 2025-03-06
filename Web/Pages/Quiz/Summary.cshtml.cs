using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BackendLab01.Pages;

public class Summary : PageModel
{
    private readonly IQuizUserService _quizUserService;
    private readonly ILogger<Summary> _logger;

    public Summary(IQuizUserService quizUserService, ILogger<Summary> logger)
    {
        _quizUserService = quizUserService ?? throw new ArgumentNullException(nameof(quizUserService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public int CorrectAnswersCount { get; set; }
    public IActionResult OnGet(int quizId)
    {
        var userId = 1;
        CorrectAnswersCount = _quizUserService.CountCorrectAnswersForQuizFilledByUser(quizId, userId);
        
        _logger.LogInformation("Uzytkownik {UserId} zdobyl {CorrectAnswerCount} poprawnych odpowiedzi w quizie {QuizId}", userId, CorrectAnswersCount, quizId);
        return Page();
    }
}