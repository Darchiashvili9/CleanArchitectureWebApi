using CleanArchitecht.Application.Common.Interfaces.Persistence;
using CleanArchitecht.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecht.Application.Services.QuizService
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;

        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public Task<QuestionWithAnswersViewModel> GetRandomQuestion()
        {
            var question= _quizRepository.GetRandomQuestion();
            return question;
        }
    }
}
