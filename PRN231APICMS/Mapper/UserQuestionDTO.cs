namespace PRN231APICMS.Mapper
{
    public class UserQuestionDTO
    {
        
        public int? TestId { get; set; }
        public int? QuestionId { get; set; }
        public string? QuestionName { get; set; }
        public int? OptionId { get; set; }
        public string? OptionName { get; set; }
        public int? UserId { get; set; }
        public bool? IsCorrect { get; set; }
        public int? QuestionCount { get; set; }
    }
}
