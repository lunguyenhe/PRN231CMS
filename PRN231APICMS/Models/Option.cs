using System;
using System.Collections.Generic;

namespace PRN231APICMS.Models
{
    public partial class Option
    {
        public Option()
        {
            UserQuestions = new HashSet<UserQuestion>();
        }

        public int OptionId { get; set; }
        public int? QuestionId { get; set; }
        public string? Content { get; set; }
        public bool? IsCorrect { get; set; }

        public virtual Question? Question { get; set; }
        public virtual ICollection<UserQuestion> UserQuestions { get; set; }
    }
}
