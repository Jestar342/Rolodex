namespace Rolodex.Messages.Commands {
    public class NewEntryRequest
    {
        public string NewFirstNames      { get; set; }
        public string NewSurname         { get; set; }
        public string NewEmailAddress    { get; set; }
        public string NewCompany         { get; set; }
        public string NewTelephoneNumber { get; set; }
    }
}