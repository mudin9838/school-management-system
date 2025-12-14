namespace SchoolManagement.Domain.Common;

public class BaseEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();//It is marked as protected set to prevent direct modification from outside the class.
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
}
