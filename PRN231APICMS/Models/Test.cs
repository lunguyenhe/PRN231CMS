using System;
using System.Collections.Generic;

namespace PRN231APICMS.Models
{
    public partial class Test
    {
        public Test()
        {
            TestQuestions = new HashSet<TestQuestion>();
            UserQuestions = new HashSet<UserQuestion>();
        }

        public int TestId { get; set; }
        public string? TestName { get; set; }
        public int? TimeDate { get; set; }
        public int? SubjectId { get; set; }
        public int? WeekNumber { get; set; }
        public virtual Subject? Subject { get; set; }
        public virtual ICollection<TestQuestion> TestQuestions { get; set; }
        public virtual ICollection<UserQuestion> UserQuestions { get; set; }
    }
}
