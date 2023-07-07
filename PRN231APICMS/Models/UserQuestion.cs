using System;
using System.Collections.Generic;

namespace PRN231APICMS.Models
{
    public partial class UserQuestion
    {
        public int UserQuestions { get; set; }
        public int? TestId { get; set; }
        public int? QuestionId { get; set; }
        public int? OptionId { get; set; }
        public int? UserId { get; set; }

        public virtual Option? Option { get; set; }
        public virtual Question? Question { get; set; }
        public virtual Test? Test { get; set; }
        public virtual User? User { get; set; }
    }
}
