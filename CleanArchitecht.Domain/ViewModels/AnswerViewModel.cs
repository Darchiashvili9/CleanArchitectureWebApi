using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecht.Domain.ViewModels
{
    public class AnswerViewModel
    {
        public int Id { get; set; }

        public string Text { get; set; } = null!;

        public bool IsCorrect { get; set; }
    }
}
