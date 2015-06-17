using System;
using NServiceBus;

namespace Magazine
{
    class Program
    {
        static void Main()
        {
            BusConfiguration busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("Magazine");
            busConfiguration.UseSerialization<XmlSerializer>();
            busConfiguration.EnableInstallers();
            busConfiguration.UsePersistence<InMemoryPersistence>();

            using (IStartableBus bus = Bus.Create(busConfiguration))
            {
                bus.Start();

                bus.SendLocal(new AddMagazine {MagazineId = 1});
                Console.Out.WriteLine("Press key to quit");
                Console.Read();
            }
        }
    }
}
