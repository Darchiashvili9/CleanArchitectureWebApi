using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecht.Domain.Entities
{
    public class AnswerDataModel
    {
        public int? id { get; set; }

        //ციტატის შესაძლო ავტორი
        public string text { get; set; } = null!;

        //ციტატის ავტორია თუ არა
        public bool isCorrect { get; set; }
    }
}
