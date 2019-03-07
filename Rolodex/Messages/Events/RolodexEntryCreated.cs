using System;
using Rolodex.Models;

namespace Rolodex.Messages.Events {
    public class RolodexEntryCreated : IEvent
    {
        public string         CompanyName     { get; set; }
        public string         FirstNames      { get; set; }
        public string         Surname         { get; set; }
        public string         EmailAddress    { get; set; }
        public string         TelephoneNumber { get; set; }
        public Guid           EntityId        { get; set; }
        public DateTimeOffset OccurredOn      { get; set; }
    }
}