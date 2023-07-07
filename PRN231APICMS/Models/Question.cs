using System;
using System.Collections.Generic;

namespace PRN231APICMS.Models
{
    public partial class Question
    {
        public Question()
        {
            Options = new HashSet<Option>();
            TestQuestions = new HashSet<TestQuestion>();
            UserQuestions = new HashSet<UserQuestion>();
        }

        public int QuestionId { get; set; }
        public int? SubjectId { get; set; }
        public string? Content { get; set; }
        public DateTime? AssignedDate { get; set; }

        public virtual ICollection<Option> Options { get; set; }
        public virtual ICollection<TestQuestion> TestQuestions { get; set; }
        public virtual ICollection<UserQuestion> UserQuestions { get; set; }
    }
}
