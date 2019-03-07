namespace Rolodex.Messages.Commands.Responses {
    public class EntriesMustHaveAName : INewEntryResponse {
        public void ApplyTo(INewEntryResponseHandler handler)
            => handler.Fail("Entries require at least one of company name, first name(s), or surname.");
    }
}