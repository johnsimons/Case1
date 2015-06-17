using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Saga;

namespace Messages
{
    public class AddSubscriber : ICommand
    {
        public string Email { get; set; }
        public string Fullname { get; set; }
        [Unique]
        public int MagazineId { get; set; }
    }
}
