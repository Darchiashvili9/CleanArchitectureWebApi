using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecht.Domain.Entities
{
    public class AnswerModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public virtual QuestionModel Question { get; set; } = null!;

        [Required]
        public string Text { get; set; } = null!;

        [Required]
        public bool IsCorrect { get; set; }
    }
}
