using System;
using System.Collections.Generic;
using Rolodex.Messages.Events;

namespace Rolodex.Models {
    public interface IAggregateRoot
    {
        List<IEvent> UnCommittedEvents { get; }
        Guid EntityId { get; }
        void         ApplyAll(IEnumerable<IEvent> events);
    }
}