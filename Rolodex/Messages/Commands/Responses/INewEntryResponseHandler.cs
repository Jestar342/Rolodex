namespace Rolodex.Messages.Commands.Responses {
    public interface INewEntryResponseHandler
    {
        void Fail(string message);
        void Success();
    }
}