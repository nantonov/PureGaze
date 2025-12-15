using Common.Data.Enums;

namespace Common.Domain.Entities;

    public class AnswerTranslate
    {
        public int AnswerId { get; set; }
        public Language Language { get; set; }
        public string Content { get; set; } = null!;
    }