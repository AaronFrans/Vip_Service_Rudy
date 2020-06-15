
using Newtonsoft.Json;

namespace DomainLayer.Domain.Help
{
    public class ClientDiscount
    {
        public string ClientType { get; private set; }
        public int NrOfReservationsNeeded { get; private set; }
        public float Discount { get; private set; }

        [JsonConstructor]
        public ClientDiscount(string clientType, int nrOfReservationsNeeded, float discount)
        {
            ClientType = clientType;
            NrOfReservationsNeeded = nrOfReservationsNeeded;
            Discount = discount;
        }
    }
}
