using System;
using System.Threading.Tasks;
using MassTransit;
using Rolodex.Messages.Commands;
using Rolodex.Messages.Commands.Responses;
using Rolodex.Models;
using Rolodex.Persistence;

namespace Rolodex.Handlers
{
    public abstract class RequestHandler<TRequest, TResponse> : IConsumer<TRequest>
        where TRequest : class
        where TResponse : class
    {
        public Task Consume(ConsumeContext<TRequest> context)
            => context.RespondAsync(ProcessRequest(context.Message));

        public abstract TResponse ProcessRequest(TRequest entryRequest);
    }


    public class CreateRolodexEntryHandler : RequestHandler<NewEntryRequest, INewEntryResponse>
    {
        readonly IEventRepository _eventRepository;

        public CreateRolodexEntryHandler(IEventRepository eventRepository)
            => _eventRepository = eventRepository;

        public override INewEntryResponse ProcessRequest(NewEntryRequest entryRequest)
        {
            if (string.IsNullOrWhiteSpace(entryRequest.NewEmailAddress) &&
                string.IsNullOrWhiteSpace(entryRequest.NewTelephoneNumber))
                return new EntriesMustHaveEmailOrTelephone();

            if (string.IsNullOrWhiteSpace(entryRequest.NewCompany)    &&
                string.IsNullOrWhiteSpace(entryRequest.NewFirstNames) &&
                string.IsNullOrWhiteSpace(entryRequest.NewSurname))
                return new EntriesMustHaveAName();

            var newEntry = new RolodexEntry(entryRequest.NewCompany,
                                            entryRequest.NewFirstNames,
                                            entryRequest.NewSurname,
                                            entryRequest.NewEmailAddress,
                                            entryRequest.NewTelephoneNumber,
                                            Guid.NewGuid(),
                                            DateTimeOffset.UtcNow
            );

            _eventRepository.Save(newEntry);

            return new SuccessfullyCreatedEntry();
        }
    }
}