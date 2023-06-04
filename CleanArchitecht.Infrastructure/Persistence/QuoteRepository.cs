using CleanArchitecht.Application.Common.Interfaces.Persistence;
using CleanArchitecht.Domain.Entities;
using CleanArchitecht.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecht.Infrastructure.Persistence
{
    internal class QuoteRepository : IQuoteRepository
    {
        private readonly DataContext _context;

        public QuoteRepository(DataContext context)
        {
            _context = context;
        }

        public async Task CreateQuote(QuoteDataModel data)
        {
            QuestionModel questionModel = new QuestionModel();
            //citata
            questionModel.Text = data.quoteText;



            List<AnswerModel> answersModel = new List<AnswerModel>();
            data.answers.ForEach((el) =>
            {
                AnswerModel answerModel = new AnswerModel();

                //citatata
                answerModel.Question = questionModel;
               
                //citatis shesadzlo avtori
                answerModel.Text = el.text;
                
                answerModel.IsCorrect = el.isCorrect;

                answersModel.Add(answerModel);
            });

            this._context.Questions.Add(questionModel);
            this._context.Answers.AddRange(answersModel);

            await this._context.SaveChangesAsync();
        }
    }
}
