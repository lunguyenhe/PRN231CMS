using System;
using System.Collections.Generic;

namespace PRN231APICMS.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Assignments = new HashSet<Assignment>();
            Tests = new HashSet<Test>();
        }

        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public string? Description { get; set; }
        public int? Weeks { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
    }
}
