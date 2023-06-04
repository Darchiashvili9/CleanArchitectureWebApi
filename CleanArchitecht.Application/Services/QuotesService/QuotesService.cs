using CleanArchitecht.Application.Common.Interfaces.Authentication;
using CleanArchitecht.Application.Common.Interfaces.Persistence;
using CleanArchitecht.Application.Services.Authentication;
using CleanArchitecht.Domain.Entities;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecht.Application.Services.QuotesService
{
    public class QuotesService : IQuotesService
    {
        private readonly IQuoteRepository _quoteRepository;

        public QuotesService(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }

        public async Task AddQuote(QuoteDataModel data)
        {
            await _quoteRepository.CreateQuote(data);
        
        }
    }
}
