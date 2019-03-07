namespace Rolodex.Messages.Commands.Responses {
    public class SuccessfullyCreatedEntry : INewEntryResponse
    {
        public void ApplyTo(INewEntryResponseHandler handler)
            => handler.Success();
    }
}