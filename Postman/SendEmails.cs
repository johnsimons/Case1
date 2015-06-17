using System;
using Messages;
using NServiceBus;

namespace Postman
{
    class SendEmails: IHandleMessages<EmailSubscriber>
    {
        public void Handle(EmailSubscriber message)
        {
            Console.Out.WriteLine("Sending email to {0} at {1}", message.Fullname, message.Email);
        }
    }
}