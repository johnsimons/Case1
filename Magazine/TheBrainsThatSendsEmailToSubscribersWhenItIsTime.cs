using System;
using System.Collections.Generic;
using Messages;
using NServiceBus;
using NServiceBus.Saga;

namespace Magazine
{
    class TheBrainsThatSendsEmailToSubscribersWhenItIsTime : Saga<TheBrainsThatSendsEmailToSubscribersWhenItIsTime.MyData>, IAmStartedByMessages<AddMagazine>, IHandleMessages<AddSubscriber>, IHandleTimeouts<TimeToSendEmails>
    {
        public class MyData : ContainSagaData
        {
            public int MagazineId { get; set; }
            public List<Subscriber> Subscribers { get; set; }

            internal class Subscriber
            {
                public string Fullname { get; set; }
                public string Email { get; set; }
            }
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<MyData> mapper)
        {
            mapper.ConfigureMapping<AddSubscriber>(m => m.MagazineId).ToSaga(s => s.MagazineId);
            mapper.ConfigureMapping<AddMagazine>(m => m.MagazineId).ToSaga(s => s.MagazineId);
        }

        public void Handle(AddSubscriber message)
        {
            Bus.Reply(new ThankYou());
            Data.Subscribers.Add(new MyData.Subscriber {Fullname = message.Fullname, Email = message.Email});
        }

        public void Handle(AddMagazine message)
        {
            Data.MagazineId = message.MagazineId;
            Data.Subscribers = new List<MyData.Subscriber>();
            RequestTimeout<TimeToSendEmails>(TimeSpan.FromSeconds(10));
        }

        public void Timeout(TimeToSendEmails state)
        {
            foreach (var subscriber in Data.Subscribers)
            {
                Bus.Send(new EmailSubscriber { Email = subscriber.Email, Fullname = subscriber.Fullname });
            }

            Console.Out.WriteLine("Emailing {0} subscribers", Data.Subscribers.Count);

            RequestTimeout<TimeToSendEmails>(TimeSpan.FromSeconds(10));
        }
    }
}