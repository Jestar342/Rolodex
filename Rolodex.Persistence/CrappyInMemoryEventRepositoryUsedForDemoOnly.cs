using System;
using System.Collections.Generic;
using System.Linq;
using Rolodex.Messages.Events;
using Rolodex.Models;

namespace Rolodex.Persistence
{
    public class CrappyInMemoryEventRepositoryUsedForDemoOnly : IEventRepository
    {
        public List<IEvent> Events { get; } = new List<IEvent>();

        public void Save(IAggregateRoot aggregateRoot) => Events.AddRange(aggregateRoot.UnCommittedEvents);

        public TEntity Get<TEntity>(Guid entityId) 
            where TEntity : class, IAggregateRoot, new()
        {
            var events = Events.Where(x => x.EntityId == entityId).ToArray();

            if (!events.Any())
                return null;

            var entity = new TEntity();
            entity.ApplyAll(events);
            return entity;
        }
    }
}