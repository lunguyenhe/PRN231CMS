using System;
using System.Collections.Generic;

namespace PRN231APICMS.Models
{
    public partial class TestQuestion
    {
        public int TestQuestionId { get; set; }
        public int? TestId { get; set; }
        public int? QuestionId { get; set; }

        public virtual Question? Question { get; set; }
    }
}
