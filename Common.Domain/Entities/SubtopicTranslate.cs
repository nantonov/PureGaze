using Common.Data.Enums;

namespace Common.Domain.Entities;

    public class SubtopicTranslate
    {
        public int SubtopicId { get; set; }
        public Language Language { get; set; }
        public string Name { get; set; } = null!;
    }