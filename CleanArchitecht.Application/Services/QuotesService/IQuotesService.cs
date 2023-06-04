using CleanArchitecht.Application.Services.Authentication;
using CleanArchitecht.Domain.Entities;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecht.Application.Services.QuotesService
{
    public interface IQuotesService
    {
        Task AddQuote(QuoteDataModel data);
    }
}
