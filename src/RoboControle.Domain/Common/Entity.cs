namespace RoboControle.Domain.Common;
public abstract class Entity
{
    protected Entity() { }

    public Ulid Id { get; private init; } = Ulid.NewUlid();
}