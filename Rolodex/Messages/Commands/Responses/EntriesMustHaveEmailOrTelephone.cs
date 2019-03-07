namespace Rolodex.Messages.Commands.Responses {
    public class EntriesMustHaveEmailOrTelephone : INewEntryResponse
    {
        public void ApplyTo(INewEntryResponseHandler handler)
            => handler.Fail("Entries must have at least one of telephone number, or email address.");
    }
}