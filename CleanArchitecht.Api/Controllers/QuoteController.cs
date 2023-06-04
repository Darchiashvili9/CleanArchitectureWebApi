using Azure;
using CleanArchitecht.Application.Services.QuotesService;
using CleanArchitecht.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecht.Api.Controllers
{
    [Route("quote")]
    public class QuoteController:ApiController
    {
        private readonly IQuotesService _quotesService;

        public QuoteController(IQuotesService quotesService)
        {
            _quotesService = quotesService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Create(QuoteDataModel data)
        {
            try
            {
                await this._quotesService.AddQuote(data);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }               
            
            return Ok();
        }
    }
}
