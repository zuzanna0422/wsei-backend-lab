using ApplicationCore.Commons.Repository;
using ApplicationCore.Models;
using ApplicationCore.Models.QuizAggregate;
using ApplicationCore.Specifications;
using Microsoft.Extensions.Logging;

namespace BackendLab01;

public class QuizUserService: IQuizUserService
{
    private readonly IGenericRepository<Quiz, int> quizRepository;
    private readonly IGenericRepository<QuizItem, int> itemRepository;
    private readonly IGenericRepository<QuizItemUserAnswer, string> answerRepository;
    public QuizUserService(IGenericRepository<Quiz, int> quizRepository, IGenericRepository<QuizItemUserAnswer, string> answerRepository, IGenericRepository<QuizItem, int> itemRepository)
    {
        this.quizRepository = quizRepository;
        this.answerRepository = answerRepository;
        this.itemRepository = itemRepository;
    }

    public Quiz CreateAndGetQuizRandom(int count)
    {
        throw new NotImplementedException();
    }

    public Quiz? FindQuizById(int id)
    {
        var quiz = quizRepository.FindById(id);
        return quizRepository.FindById(id);
    }

    public void SaveUserAnswerForQuiz(int quizId, int userId, int quizItemId, string answer)
    {
        Console.WriteLine($"Attempting to save answer:");
        Console.WriteLine($"QuizId: {quizId}");
        Console.WriteLine($"UserId: {userId}");
        Console.WriteLine($"QuizItemId: {quizItemId}");
        Console.WriteLine($"Answer: {answer}");

        // First, verify the quiz exists
        var quiz = quizRepository.FindById(quizId);
        if (quiz == null)
        {
            Console.WriteLine($"ERROR: Quiz with ID {quizId} not found!");
            throw new ArgumentException($"Quiz with ID {quizId} does not exist");
        }

        QuizItem? item = itemRepository.FindById(quizItemId);
        if (item == null)
        {
            Console.WriteLine($"ERROR: Quiz item with ID {quizItemId} not found!");
            throw new ArgumentException($"Quiz item with ID {quizItemId} does not exist");
        }

        Console.WriteLine($"Quiz Item found:");
        Console.WriteLine($"Question: {item.Question}");
        Console.WriteLine($"Correct Answer: {item.CorrectAnswer}");

        var userAnswer = new QuizItemUserAnswer(
            quizItem: item, 
            userId: userId, 
            answer: answer, 
            quizId: quizId
        );

        answerRepository.Add(userAnswer);
        Console.WriteLine("Answer saved successfully!");
    }


    public List<QuizItemUserAnswer> GetUserAnswersForQuiz(int quizId, int userId)
    {
        // return answerRepository.FindAll()
        //     .Where(x => x.QuizId == quizId)
        //     .Where(x => x. UserId == userId)
        //     .ToList();
        return answerRepository.FindBySpecification(new QuizItemsForQuizIdFilledByUser(quizId, userId)).ToList();
    }
}