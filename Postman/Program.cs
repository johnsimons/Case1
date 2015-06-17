using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace Postman
{
    class Program
    {
        static void Main()
        {
            BusConfiguration busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("Postman");
            busConfiguration.UseSerialization<XmlSerializer>();
            busConfiguration.EnableInstallers();
            busConfiguration.UsePersistence<InMemoryPersistence>();

            using (IStartableBus bus = Bus.Create(busConfiguration))
            {
                bus.Start();

                Console.Out.WriteLine("Press key to quit");
                Console.Read();
            }
        }
    }
}
