using System;

namespace Rolodex.Web.ViewModels {
    public class RolodexViewModel {
        public string EmailAddress { get; set; }
        public string CompanyName { get; set; }
        public Guid EntityId { get; set; }
        public string FirstNames { get; set; }
        public string Surname { get; set; }
        public string TelephoneNumber { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}