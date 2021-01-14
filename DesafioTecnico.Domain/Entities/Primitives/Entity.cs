using System;

namespace DesafioTecnico.Domain.Entities.Primitives
{
    public abstract class Entity : IEquatable<Entity>
    {
        protected Entity() =>
            Id = Guid.NewGuid();

        public Guid Id { get; private set; }

        public bool Equals(Entity other) =>
            Id == other?.Id;
    }
}