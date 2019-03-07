using System;
using System.Collections.Generic;
using Rolodex.Messages.Events;
using Rolodex.Models;

namespace Rolodex.Persistence
{
    public interface IEventRepository
    {
        List<IEvent> Events { get; }

        TEntity Get<TEntity>(Guid entityId)
            where TEntity : class, IAggregateRoot, new();

        void Save(IAggregateRoot aggregateRoot);
    }
}