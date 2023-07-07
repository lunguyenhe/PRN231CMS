using System;
using System.Collections.Generic;

namespace PRN231APICMS.Models
{
    public partial class SubmittedAssignment
    {
        public int SubmissionId { get; set; }
        public int? AssignmentId { get; set; }
        public int? UserId { get; set; }
        public DateTime? SubmissionDate { get; set; }

        public virtual Assignment? Assignment { get; set; }
        public virtual User? User { get; set; }
    }
}
