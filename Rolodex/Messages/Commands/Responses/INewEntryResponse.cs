namespace Rolodex.Messages.Commands.Responses {
    public interface INewEntryResponse
    {
        void ApplyTo(INewEntryResponseHandler handler);
    }
}