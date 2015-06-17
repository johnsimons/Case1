using System;
using Messages;
using NServiceBus;

namespace Case1
{
    class ThanksHandler : IHandleMessages<ThankYou>
    {
        public void Handle(ThankYou message)
        {
            Console.Out.WriteLine("Thank you received");
        }
    }
}