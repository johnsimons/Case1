using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Messages;
using NServiceBus;
using NServiceBus.Saga;

namespace Case1
{
    class Program
    {
        private static void Main()
        {

            BusConfiguration busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("WebFrontend");
            busConfiguration.UseSerialization<XmlSerializer>();
            busConfiguration.EnableInstallers();
            busConfiguration.UsePersistence<InMemoryPersistence>();

            using (IStartableBus bus = Bus.Create(busConfiguration))
            {
                bus.Start();

                Console.Out.WriteLine("Press key to start");
                Console.Read();

                var doc = new XmlDocument();
                doc.Load(@"customers.xml");
                int counter = 1;

                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    counter++;
                    string fullName = node.ChildNodes[0].InnerText;
                    string email = node.ChildNodes[4].InnerText;

                    bus.Send(new AddSubscriber {MagazineId=1, Email = email,Fullname = fullName});

                    if (counter%10 == 0)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(5));
                    }
                }

                
            }
        }
    }
}
