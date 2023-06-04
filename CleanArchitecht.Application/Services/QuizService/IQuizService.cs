using CleanArchitecht.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecht.Application.Services.QuizService
{
    public interface IQuizService
    {
        Task<QuestionWithAnswersViewModel> GetRandomQuestion();
    }
}
