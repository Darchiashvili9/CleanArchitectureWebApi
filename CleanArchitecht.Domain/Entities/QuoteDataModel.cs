using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecht.Domain.Entities
{
    public class QuoteDataModel
    {
        public int quoteId { get; set; }

        //ციტატა
        public string quoteText { get; set; } = null!;

        public List<AnswerDataModel> answers { get; set; } = null!;
    }
}
