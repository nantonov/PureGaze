namespace PureGaze.Domain.Entities;

public class Template : BaseEntity<int>
{
    public int CodeId { get; set; }
    public Code? Code { get; set; }
    public ICollection<Topic> Topics { get; set; } = [];
}