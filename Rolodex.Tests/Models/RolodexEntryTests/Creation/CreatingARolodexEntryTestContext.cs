using System;
using System.Linq;
using Rolodex.Messages.Events;
using Rolodex.Models;

namespace Rolodex.Tests.Models.RolodexEntryTests.Creation {
    public class CreatingARolodexEntryTestContext {
        public readonly Guid           EntityId        = Guid.NewGuid();
        public readonly DateTimeOffset OccurredOn      = DateTimeOffset.UtcNow;
        public const    string         EmailAddress    = "bob@smith.com";
        public const    string         TelephoneNumber = "01234567890";
        public const    string         Surname         = "Smith";
        public const    string         FirstNames      = "Bob";
        public const    string         CompanyName     = "Bob's Company";

        public void CreateNewRolodexEntry()
            => Entry = new RolodexEntry(CompanyName,
                                        FirstNames,
                                        Surname,
                                        EmailAddress,
                                        TelephoneNumber,
                                        EntityId,
                                        OccurredOn
               );

        public IAggregateRoot EntryAsAggregateRoot
            => Entry;

        public void ReplayEvent()
        {
            Entry = new RolodexEntry();
            EntryAsAggregateRoot.ApplyAll(new[]
                {
                    new RolodexEntryCreated
                    {
                        EmailAddress    = EmailAddress,
                        FirstNames      = FirstNames,
                        CompanyName     = CompanyName,
                        TelephoneNumber = TelephoneNumber,
                        Surname         = Surname,
                        OccurredOn      = OccurredOn,
                        EntityId        = EntityId
                    }
                }
            );
        }

        public RolodexEntryCreated UnCommittedEvent
            => EntryAsAggregateRoot
              .UnCommittedEvents
              .OfType<RolodexEntryCreated>()
              .Single();

        public RolodexEntry Entry { get; set; }
    }
}