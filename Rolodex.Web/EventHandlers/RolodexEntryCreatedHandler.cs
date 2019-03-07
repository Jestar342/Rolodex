using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Rolodex.Messages.Events;
using Rolodex.Web.ViewModels;

namespace Rolodex.Web.EventHandlers
{
    public class RolodexEntryCreatedHandler : IMessageConsumer<RolodexEntryCreated>
    {
        readonly List<RolodexViewModel> _viewModels;

        public RolodexEntryCreatedHandler(List<RolodexViewModel> viewModels) { _viewModels = viewModels; }
        public void Consume(RolodexEntryCreated message)
            => _viewModels.Add(new RolodexViewModel
            {
                EmailAddress = message.EmailAddress,
                CompanyName = message.CompanyName,
                EntityId = message.EntityId,
                FirstNames = message.FirstNames,
                Surname = message.Surname,
                TelephoneNumber = message.TelephoneNumber,
                CreatedOn = message.OccurredOn
            });
    }
}