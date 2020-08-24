using DomainLayer.Domain.Arangements;
using DomainLayer.Domain.Clients;
using DomainLayer.Domain.Help;
using DomainLayer.Domain.Vloot;
using DomainLayer.OtherInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Domain.Reservation
{
    /// <summary>
    /// Represents the details for a reservation.
    /// </summary>
    public class Details
    {
        /// <summary>
        /// The id for the details.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Location where the limousine picks up the client.
        /// </summary>
        public Address StartLocation { get; private set; }
        /// <summary>
        /// Location where the limousine drops the client off.
        /// </summary>
        public Address EndLocation { get; private set; }
        /// <summary>
        /// The limousine that was hired.
        /// </summary>
        public Limousine Limousine { get; private set; }
        /// <summary>
        /// The date the limousine is hired.
        /// </summary>
        public DateTime DateLimousineNeeded { get; private set; }
        /// <summary>
        /// The arangement used.
        /// </summary>
        public string Arangement { get; private set; }
        /// <summary>
        /// A list of HourTypes.
        /// </summary>
        public List<HourType> Hours { get; private set; }
        /// <summary>
        /// The subtotal of the reservation.
        /// </summary>
        public int SubTotal { get; private set; }
        /// <summary>
        /// The used discount of the reservation.
        /// </summary>
        public float UsedDiscount { get; private set; }
        /// <summary>
        /// The price without vat.
        /// </summary>
        public int AmountWithoutBtw { get; private set; }
        /// <summary>
        /// The vat percentage.
        /// </summary>
        public float BtwPercentage { get; private set; }
        /// <summary>
        /// The vat price.
        /// </summary>
        public int BtwAmount { get; private set; }
        /// <summary>
        /// The to pay amount.
        /// </summary>
        public int ToPayAmount { get; private set; }

        /// <summary>
        /// An empty constructor.
        /// </summary>
        public Details()
        {

        }
        /// <summary>
        /// A constructor that makes a Details object.
        /// </summary>
        /// <param name="startLocation">Location where the limousine picks up the client.</param>
        /// <param name="endLocation">Location where the limousine drops the client off.</param>
        /// <param name="limousine">The limousine that was hired.</param>
        /// <param name="dateLimousineNeeded">The date the limousine is hired.</param>
        /// <param name="arangement">The arangement used.</param>
        public Details(Address startLocation, Address endLocation, Limousine limousine, DateTime dateLimousineNeeded, string arangement)
        {
            if (!Reservering.LocationAllowed(startLocation))
                throw new DomainException("Start locatie van de limousine beschikbaar in volgende locaties: Antwerpen, Gent, Brussel, Hasselt en Charleroi.");
            if (!Reservering.LocationAllowed(endLocation))
                throw new DomainException("Eind locatie van de limousine beschikbaar in volgende locaties: Antwerpen, Gent, Brussel, Hasselt en Charleroi.");
            StartLocation = startLocation;
            EndLocation = endLocation;
            Limousine = limousine;
            DateLimousineNeeded = dateLimousineNeeded;
            Arangement = arangement;
            BtwPercentage = 6.0f;
        }

        /// <summary>
        /// Calculates all prices.
        /// </summary>
        /// <param name="client">The client that reserved the limousine.</param>
        /// <param name="extraHours">Amount of extra hours added to the duration of the arangement (if applicable).</param>
        /// <param name="startHour">When the arangement starts (if applicable).</param>
        /// <param name="endHour">When the arangement ends (if applicable).</param>
        public void CalculatePrices(Client client, int? extraHours = null, TimeSpan? startHour = null, TimeSpan? endHour = null)
        {
            var result = Limousine.PriceForArangement(DateLimousineNeeded, Arangement, extraHours, startHour, endHour);

            Hours = result.Value;
            SubTotal = result.Key;
            UsedDiscount = client.GetDiscount();
            AmountWithoutBtw = (int)(SubTotal - (SubTotal * (UsedDiscount / 100.0f)));
            BtwAmount = (int)(AmountWithoutBtw * (BtwPercentage / 100.0f));
            ToPayAmount = AmountWithoutBtw + BtwAmount;
        }
    }
}
