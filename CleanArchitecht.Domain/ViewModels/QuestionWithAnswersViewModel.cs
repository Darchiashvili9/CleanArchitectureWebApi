using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecht.Domain.ViewModels
{
    public class QuestionWithAnswersViewModel
    {
        public QuestionViewModel Question { get; set; } = null!;
        public List<AnswerViewModel> Answers { get; set; } = null!;
    }
}
