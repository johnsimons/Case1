using NServiceBus;

namespace Magazine
{
    class AddMagazine : ICommand
    {
        public int MagazineId { get; set; }

    }
}