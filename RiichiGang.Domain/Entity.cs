using System;

namespace RiichiGang.Domain
{
    public abstract class Entity
    {
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? DeletedAt { get; protected set; }

        public Entity()
        {
            CreatedAt = DateTime.UtcNow;
            DeletedAt = null;
        }

        public void Delete()
        {
            if (DeletedAt != null)
                throw new InvalidOperationException("Entidade já deletada");

            DeletedAt = DateTime.UtcNow;
        }
    }
}