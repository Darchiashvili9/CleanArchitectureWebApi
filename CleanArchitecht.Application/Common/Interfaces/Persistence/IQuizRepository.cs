using CleanArchitecht.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecht.Application.Common.Interfaces.Persistence
{
    public interface IQuizRepository
    {
        Task<QuestionWithAnswersViewModel> GetRandomQuestion();
        Task<QuestionViewModel> QuoteGenerator();
        Task<List<AnswerViewModel>> AnswerGenerator(int questionId);
    }

}
