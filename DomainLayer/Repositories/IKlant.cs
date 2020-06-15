using DomainLayer.Domain.Help;
using System.Collections.Generic;

namespace DomainLayer.Interfaces
{
    public interface IKlant
    {
        public List<ClientDiscount> StaffelDiscount { get; }
        public int ClientNumber { get; }
        public string Name { get; }
        public int? BtwNumber { get; }
        public int NrOfReservations { get; }

        void AddReservation();
        float GetDiscount();

    }
}
