using ApplicationCore.Commons.Repository;
using ApplicationCore.Models.QuizAggregate;
using BackendLab01;

namespace Infrastructure.Memory;
public static class SeedData
{
    public static void Seed(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            var quizRepo = provider.GetService<IGenericRepository<Quiz, int>>();
            var quizItemRepo = provider.GetService<IGenericRepository<QuizItem, int>>();

            if (quizRepo is null || quizItemRepo is null)
            {
                return;
            }
            
            var mathQ1 = new QuizItem(id: 1, question: "2+2", correctAnswer: "4", incorrectAnswers: new List<string> { "3", "5", "6" });
            var mathQ2 = new QuizItem(id: 2, question: "5*3", correctAnswer: "15", incorrectAnswers: new List<string> { "7", "8", "9" });
            var mathQ3 = new QuizItem(id: 3, question: "10/2", correctAnswer: "5", incorrectAnswers: new List<string> { "4", "12", "6" });
            
            quizItemRepo.Add(mathQ1);
            quizItemRepo.Add(mathQ2);
            quizItemRepo.Add(mathQ3);
            
            var mathQuiz = new Quiz(id: 1, title: "Matematyka", items: new List<QuizItem> { mathQ1, mathQ2, mathQ3});
            quizRepo.Add(mathQuiz);

            var geoQ1 = new QuizItem(id: 4, question: "Stolica Francji?", correctAnswer: "Paryż",
                incorrectAnswers: new List<string> { "Londyn", "Berlin", "Madryt" });
            var geoQ2 = new QuizItem(id: 5, question: "Największy ocean?", correctAnswer: "Pacyfik", incorrectAnswers: new List<string> { "Atlantyk", "Indyjski", "Arktyczny" });
            var geoQ3 = new QuizItem(id: 6, question: "Najwyższa góra świata?", correctAnswer: "Mount Everest", incorrectAnswers: new List<string> { "K2", "Kilimandżaro", "Mont Blanc" });
            
            quizItemRepo.Add(geoQ1);
            quizItemRepo.Add(geoQ2);
            quizItemRepo.Add(geoQ3);

            var geoQuiz = new Quiz(id: 2, title: "Geografia", items: new List<QuizItem> { geoQ1, geoQ2, geoQ3 });
            quizRepo.Add(geoQuiz);
            
            //TODO Utwórz trzy pytania typu QuizItem
            //TODO Dodaj je do quizItemRepo
            //TODO Utwórz obiekt klasy Quiz z kolekcją pytań dodanych do quizItemRepo
            //TODO Dodaj Quiz do quizRepo
            
            QuizItem item1 = new QuizItem(id:1, question: "2+4", correctAnswer: "6", incorrectAnswers: ["5","7", "8"]);
            QuizItem item2 = new QuizItem(id:1, question: "2*4", correctAnswer: "8", incorrectAnswers: ["4","6", "9"]);
            QuizItem item3 = new QuizItem(id:1, question: "8/2", correctAnswer: "4", incorrectAnswers: ["5","7", "8"]);
            quizItemRepo?.Add(item1);
            quizItemRepo?.Add(item2);
            quizItemRepo?.Add(item3);
            Quiz quiz = new(id: 1, title: "Matematyka", items: [item1, item2, item3]);
            quizRepo?.Add(quiz);
        }
    }
}