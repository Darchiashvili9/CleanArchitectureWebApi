using CleanArchitecht.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecht.Application.Common.Interfaces.Persistence
{
    public interface IQuoteRepository
    {
        Task CreateQuote(QuoteDataModel data);
    }
}
