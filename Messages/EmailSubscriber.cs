using NServiceBus;

namespace Messages
{
    public class EmailSubscriber : ICommand
    {
        public string Email { get; set; }
        public string Fullname { get; set; }
    }
}