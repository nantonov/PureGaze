using Common.Data.Enums;

namespace Common.Domain.Entities;

    public class CodeTranslate
    {
        public int CodeId { get; set; }
        public Language Language { get; set; }
        public string Display { get; set; } = null!;
    }