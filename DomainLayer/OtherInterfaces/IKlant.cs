using DomainLayer.Domain.Clients;
using DomainLayer.Domain.Help;
using System.Collections.Generic;

namespace DomainLayer.OtherInterfaces
{
    public interface IKlant
    {
        static public List<ClientDiscount> StaffelDiscount { get; }
        public Address Address { get; }
        public int ClientNumber { get; set; }
        public string Name { get; }
        public string BtwNumber { get; }
        public Dictionary<int,int> NrOfReservations { get; }


        void AddReservation();
        float GetDiscount();

    }
}
