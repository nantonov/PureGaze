using Common.Data.Enums;

namespace Common.Domain.Entities;

    public class TopicTranslate
    {
        public int TopicId { get; set; }
        public Language Language { get; set; }
        public string Name { get; set; } = null!;
    }