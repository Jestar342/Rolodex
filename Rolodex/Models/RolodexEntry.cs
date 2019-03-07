using System;
using Rolodex.Messages.Events;

namespace Rolodex.Models
{
    public class RolodexEntry : AggregateRoot
    {
        public string CompanyName     { get; private set; }
        public string FirstNames      { get; private set; }
        public string Surname         { get; private set; }
        public string EmailAddress    { get; private set; }
        public string TelephoneNumber { get; private set; }

        public RolodexEntry(
            string         companyName,
            string         firstNames,
            string         surname,
            string         emailAddress,
            string         telephoneNumber,
            Guid           entityId,
            DateTimeOffset createdWhen
        )
            => Add(new RolodexEntryCreated
                {
                    EntityId        = entityId,
                    OccurredOn      = createdWhen,
                    CompanyName     = companyName,
                    FirstNames      = firstNames,
                    Surname         = surname,
                    EmailAddress    = emailAddress,
                    TelephoneNumber = telephoneNumber
                }
            );

        public RolodexEntry() { }

        void Apply(RolodexEntryCreated created)
        {
            CompanyName     = created.CompanyName;
            FirstNames      = created.FirstNames;
            Surname         = created.Surname;
            EmailAddress    = created.EmailAddress;
            TelephoneNumber = created.TelephoneNumber;
            EntityId              = created.EntityId;
        }
    }
}