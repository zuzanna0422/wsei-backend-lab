using ApplicationCore.Commons.Specification;
using ApplicationCore.Models;

namespace ApplicationCore.Specifications;

public class QuizItemsForQuizIdFilledByUser: BaseSpecification<QuizItemUserAnswer>
{
    public QuizItemsForQuizIdFilledByUser(int quizId, int userId) 
        : base(answer => 
            answer.QuizId == quizId && 
            answer.UserId == userId)
    {
        Console.WriteLine($"Creating Specification:");
        Console.WriteLine($"QuizId: {quizId}");
        Console.WriteLine($"UserId: {userId}");

        // Ensure QuizItem is included
        AddInclude(answer => answer.QuizItem);
    }
}