using System;
using System.Collections.Generic;

namespace PRN231APICMS.Models
{
    public partial class User
    {
        public User()
        {
            Assignments = new HashSet<Assignment>();
            Subjects = new HashSet<Subject>();
            SubmittedAssignments = new HashSet<SubmittedAssignment>();
            UserQuestions = new HashSet<UserQuestion>();
        }

        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
        public virtual ICollection<SubmittedAssignment> SubmittedAssignments { get; set; }
        public virtual ICollection<UserQuestion> UserQuestions { get; set; }
    }
}
