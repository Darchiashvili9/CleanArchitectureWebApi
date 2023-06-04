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

        [HttpGet]
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
            return Ok();
        }


    }
}
