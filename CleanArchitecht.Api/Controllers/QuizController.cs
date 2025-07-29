using Azure;
using CleanArchitecht.Application.Services.QuizService;
using CleanArchitecht.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecht.Api.Controllers
{
    [Route("quiz")]
    public class QuizController : ApiController
    {
        private readonly IQuizService quizService;

        public QuizController(IQuizService quizService)
        {
            this.quizService = quizService;
        }

        [HttpGet("GetRandomQuestion")]
        public async Task<IActionResult> GetRandomQuestion()
        {
            QuestionWithAnswersViewModel question;
            try
            {
                question = await this.quizService.GetRandomQuestion();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(question);
        }

        [HttpGet("GetTestDeutschland")]
        public IActionResult GetTestDeutschland()
        {
            QuestionWithAnswersViewModel question = new();

            question.Question = new QuestionViewModel { Id = 1, Text = "What is the capital of Deutschland?" };
            question.Answers = new List<AnswerViewModel>
            {
                new AnswerViewModel { Id = 1, Text = "Munchen", IsCorrect = false },
                new AnswerViewModel { Id = 2, Text = "Koln", IsCorrect = false },
                new AnswerViewModel { Id = 3, Text = "Berlin", IsCorrect = true },
                new AnswerViewModel { Id = 4, Text = "Stuttgart", IsCorrect = false }
            };

            return Ok(question);
        }
    }
}