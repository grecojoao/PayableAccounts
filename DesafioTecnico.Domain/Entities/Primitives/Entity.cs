using System;
using Flunt.Notifications;

namespace DesafioTecnico.Domain.Entities.Primitives
{
    public abstract class Entity : Notifiable, IId, IEquatable<Entity>
    {
        protected Entity() =>
            Id = Guid.NewGuid();

        public Guid Id { get; }

        public bool Equals(Entity other) =>
            Id == other?.Id;
    }
}