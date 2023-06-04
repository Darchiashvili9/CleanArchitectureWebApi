using CleanArchitecht.Application.Common.Interfaces.Persistence;
using CleanArchitecht.Domain.ViewModels;
using CleanArchitecht.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecht.Infrastructure.Persistence
{
    public class QuizRepository : IQuizRepository
    {
        private readonly DataContext _context;

        public QuizRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<QuestionWithAnswersViewModel> GetRandomQuestion()
        {
            QuestionWithAnswersViewModel model = new QuestionWithAnswersViewModel();

            model.Question = await this.QuoteGenerator();
            model.Answers = await this.AnswerGenerator(model.Question.Id);

            return model;
        }

        public async Task<QuestionViewModel> QuoteGenerator()
        {
            var questions = await this._context.Questions.ToListAsync();

            var random = new Random();
            var randomIndex = random.Next(questions.Count);

            //რანდომად ერთს ვირჩევთ
            var question = questions[randomIndex];

            //map
            var questionViewModel = new QuestionViewModel
            {
                Id = question.Id,
                Text = question.Text
            };

            return questionViewModel;
        }

        public async Task<List<AnswerViewModel>> AnswerGenerator(int questionId)
        {
            var answers = await this._context.Answers
                .Where(a => a.Question.Id == questionId)
                .ToListAsync();

            var answersViewModel = new List<AnswerViewModel>();

            foreach (var item in answers)
            {
                var avm = new AnswerViewModel()
                {
                    Id = item.Id,
                    Text = item.Text,
                    IsCorrect = item.IsCorrect
                };

                answersViewModel.Add(avm);
            }

            var random = new Random();

            //რანდომად
            return answersViewModel.OrderBy(a => random.Next()).ToList();
        }

    }
}
