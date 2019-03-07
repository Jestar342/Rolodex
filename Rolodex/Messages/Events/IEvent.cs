using System;

namespace Rolodex.Messages.Events {
    public interface IEvent
    {
        Guid EntityId { get; }

        DateTimeOffset OccurredOn { get; }
    }
}