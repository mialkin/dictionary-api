namespace Dictionary.Api.Domain;

public abstract class AuditableEntity // TODO Move this class to another DLL?
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
