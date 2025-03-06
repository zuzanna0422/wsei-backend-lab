using ApplicationCore.Models;
using ApplicationCore.Models.QuizAggregate;

namespace BackendLab01;

public interface IQuizUserService
{
    Quiz CreateAndGetQuizRandom(int count);

    Quiz? FindQuizById(int id);

    void SaveUserAnswerForQuiz(int quizId, int userId, int quizItemId, string answer);

    List<QuizItemUserAnswer> GetUserAnswersForQuiz(int quizId, int userId);

    public int CountCorrectAnswersForQuizFilledByUser(int quizId, int userId)
    {
        var userAnswers = GetUserAnswersForQuiz(quizId, userId);
    
        Console.WriteLine($"Total user answers: {userAnswers.Count}");
    
        foreach (var answer in userAnswers)
        {
            Console.WriteLine($"Answer Details:");
            Console.WriteLine($"  QuizItem ID: {answer.QuizItem.Id}");
            Console.WriteLine($"  Question: {answer.QuizItem.Question}");
            Console.WriteLine($"  Correct Answer: {answer.QuizItem.CorrectAnswer}");
            Console.WriteLine($"  User Answer: {answer.Answer}");
            Console.WriteLine($"  Is Correct: {answer.IsCorrect()}");
        }

        var correctCount = userAnswers.Count(e => e.IsCorrect());
    
        Console.WriteLine($"Correct answers count: {correctCount}");
    
        return correctCount;
    }
}