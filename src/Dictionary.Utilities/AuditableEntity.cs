namespace Dictionary.Utilities;

public abstract class AuditableEntity
{
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    // TODO Rename to SetCreated?
    public void Created(DateTime utcNow)
    {
        CreatedAt = utcNow;
        UpdatedAt = utcNow;
    }

    public void Updated(DateTime utcNow)
    {
        UpdatedAt = utcNow;
    }
}
