using ApplicationCore.Commons.Repository;
using ApplicationCore.Models;
using ApplicationCore.Models.QuizAggregate;
using BackendLab01;
using Infrastructure.Memory;
using Infrastructure.Memory.Generators;
using Infrastructure.Memory.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IGenericRepository<Quiz, int>>(new MemoryGenericRepository<Quiz, int>(new IntGenerator()));
builder.Services.AddSingleton<IGenericRepository<QuizItem, int>>(new MemoryGenericRepository<QuizItem, int>(new IntGenerator()));
builder.Services.AddSingleton<IGenericRepository<QuizItemUserAnswer, string>>(new MemoryGenericRepository<QuizItemUserAnswer, string>());

builder.Services.AddSingleton<IQuizUserService>(provider =>
{
    var quizRepo = provider.GetRequiredService<IGenericRepository<Quiz, int>>();
    var quizItemRepo = provider.GetRequiredService<IGenericRepository<QuizItem, int>>();
    var answerRepo = provider.GetRequiredService<IGenericRepository<QuizItemUserAnswer, string>>();
    return new QuizUserService(quizRepo, answerRepo, quizItemRepo);
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.Seed();
app.Run();