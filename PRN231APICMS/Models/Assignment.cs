using System;
using System.Collections.Generic;

namespace PRN231APICMS.Models
{
    public partial class Assignment
    {
        public Assignment()
        {
            SubmittedAssignments = new HashSet<SubmittedAssignment>();
        }

        public int AssignmentId { get; set; }
        public int? SubjectId { get; set; }
        public int? WeekNumber { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }

        public virtual Subject? Subject { get; set; }
        public virtual ICollection<SubmittedAssignment> SubmittedAssignments { get; set; }
    }
}
