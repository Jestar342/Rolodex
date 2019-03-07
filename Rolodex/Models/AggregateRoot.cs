using System;
using System.Collections.Generic;
using System.Reflection;
using Rolodex.Messages.Events;

namespace Rolodex.Models {
    public abstract class AggregateRoot : IAggregateRoot
    {
        List<IEvent> IAggregateRoot.UnCommittedEvents { get; } = new List<IEvent>();
        public Guid EntityId { get; protected set; }

        void IAggregateRoot.ApplyAll(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                var method = GetType().GetMethod("Apply", BindingFlags.Instance | BindingFlags.NonPublic);
                method.Invoke(this, new[] {@event});
            }
        }

        protected void Add(IEvent @event) => ((IAggregateRoot) this).UnCommittedEvents.Add(@event);
    }
}