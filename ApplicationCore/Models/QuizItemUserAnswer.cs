using ApplicationCore.Commons.Repository;
using ApplicationCore.Models.QuizAggregate;

namespace ApplicationCore.Models;

public class QuizItemUserAnswer(QuizItem quizItem, int userId, int quizId, string answer)
    : IIdentity<string>
{
    public int QuizId { get; } = quizId;
    public QuizItem QuizItem { get; } = quizItem;
    public int UserId { get; } = userId;
    public string Answer { get; } = answer;

    public bool IsCorrect()
    {
        Console.WriteLine($"Checking Correctness:");
        Console.WriteLine($"Correct Answer: '{QuizItem.CorrectAnswer}'");
        Console.WriteLine($"User Answer: '{Answer}'");
        
        var result = string.Equals(
            QuizItem.CorrectAnswer?.Trim().ToLowerInvariant(), 
            Answer?.Trim().ToLowerInvariant(), 
            StringComparison.Ordinal
        );
        
        Console.WriteLine($"Is Correct: {result}");
        return result;
    }

    public string Id
    {
        get => $"{QuizId}-{UserId}-{QuizItem.Id}";
        set { }
    }
}