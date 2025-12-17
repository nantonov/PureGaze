namespace Common.Domain.Entities;
public class Template : BaseEntity<int>
{
    public int CodeId { get; set; }

    public virtual ICollection<Topic> Topics { get; set; } = [];
}