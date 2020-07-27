
using DomainLayer.Domain.Clients;
using Newtonsoft.Json;

namespace DomainLayer.Domain.Help
{
    public class ClientDiscount
    {
        public int Id { get; set; }
        public ClientType ClientType { get; private set; }
        public int NrOfReservationsNeeded { get; private set; }
        public float Discount { get; private set; }

        [JsonConstructor]
        public ClientDiscount(ClientType clientType, int nrOfReservationsNeeded, float discount)
        {
            ClientType = clientType;
            NrOfReservationsNeeded = nrOfReservationsNeeded;
            Discount = discount;
        }
    }
}
