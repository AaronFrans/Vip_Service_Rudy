
using DomainLayer.Domain.Clients;
using Newtonsoft.Json;

namespace DomainLayer.Domain.Help
{
    /// <summary>
    /// A class used to represent the amount of reservations needed to use a discount.
    /// </summary>
    public class ClientDiscount
    {
        /// <summary>
        /// The id for the client discount.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The type of client the discount is for.
        /// </summary>
        public ClientType ClientType { get; private set; }
        /// <summary>
        /// The amount of reservations needed.
        /// </summary>
        public int NrOfReservationsNeeded { get; private set; }
        /// <summary>
        /// The discount gained.
        /// </summary>
        public float Discount { get; private set; }

        /// <summary>
        /// A constructor used tro make a client discount object.
        /// </summary>
        /// <param name="clientType">The type of client the discount is for.</param>
        /// <param name="nrOfReservationsNeeded">The amount of reservations needed.</param>
        /// <param name="discount">The discount gained.</param>
        [JsonConstructor]
        public ClientDiscount(ClientType clientType, int nrOfReservationsNeeded, float discount)
        {
            ClientType = clientType;
            NrOfReservationsNeeded = nrOfReservationsNeeded;
            Discount = discount;
        }
    }
}
