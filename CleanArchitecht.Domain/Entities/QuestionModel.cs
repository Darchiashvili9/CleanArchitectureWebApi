using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecht.Domain.Entities
{
    public class QuestionModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; } = null!;
    }
}
