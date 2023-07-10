using PRN231APICMS.Models;

namespace PRN231APICMS.Mapper
{
    public class SubmitAssignmentDTO
    {
        public int SubmissionId { get; set; }
        public int? AssignmentId { get; set; }
        public int? UserId { get; set; }
        public string? Email { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public string? File { get; set; }

    }
}
