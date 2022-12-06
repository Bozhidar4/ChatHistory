namespace ChatHistory.Domain.Core
{
    public abstract class Entity<T>
    {
        public T? Id { get; internal protected set; }

        public Entity(T id)
        {
            Id = id;
        }
    }
}
