namespace PureGaze.Domain.Entities;

public class BaseEntity<TEntityId> where TEntityId : struct
{
    public TEntityId Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.ToUniversalTime();
    public DateTime? UpdatedAt { get; set; }
}