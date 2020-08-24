using DomainLayer.Domain.Clients;
using DomainLayer.Domain.Help;
using DomainLayer.Domain.Vloot;
using DomainLayer.OtherInterfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Domain.Reservation
{
    /// <summary>
    /// Represents the reservation of a limousine.
    /// </summary>
    public class Reservering
    {
        /// <summary>
        /// Number used to identify a reservation.
        /// </summary>
        [Key]
        public int ReservationNumber { get; set; }
        /// <summary>
        /// Date the reservation is made.
        /// </summary>
        public DateTime ReservationDate { get; private set; }
        /// <summary>
        /// Client that reserves the limousine.
        /// </summary>
        public Client Client { get; private set; }
        /// <summary>
        /// Location where the limousine picks up the client.
        /// </summary>
        public Address Location { get; private set; }
        /// <summary>
        /// The details for the reservation.
        /// </summary>
        public Details Details { get; private set; }

        /// <summary>
        /// An ampty constructor.
        /// </summary>
        public Reservering()
        {

        }
        /// <summary>
        /// A constructor tha makes a Reservation object.
        /// </summary>
        /// <param name="reservationDate">Date the reservation is made.</param>
        /// <param name="client">Client that reserves the limousine.</param>
        /// <param name="location">Location where the limousine picks up the client.</param>
        public Reservering(DateTime reservationDate, Client client, Address location)
        {
            ReservationDate = reservationDate;
            Client = client;
            if (LocationAllowed(location))
                Location = location;
            else
                throw new DomainException("Limousines zijn aleen beschikbaar in volgende locaties: Antwerpen, Gent, Brussel, Hasselt en Charleroi.");
        }

        /// <summary>
        /// Add the details for the reservation.
        /// </summary>
        /// <param name="endLocation">Location where the limousine drops the client off.</param>
        /// <param name="vehicle">The limousine that was hired.</param>
        /// <param name="dateLimousineNeeded">The date the limousine is hired.</param>
        /// <param name="arangement">The arangement used.</param>
        /// <param name="extraHours">Amount of extra hours added to the duration of the arangement (if applicable).</param>
        /// <param name="startHour">When the arangement starts (if applicable).</param>
        /// <param name="endHour">When the arangement ends (if applicable).</param>
        public void AddDetails(Address endLocation, Limousine vehicle, DateTime dateLimousineNeeded, string arangement
            , int? extraHours = null, TimeSpan? startHour = null, TimeSpan? endHour = null)
        {
           
            Details = new Details(Location, endLocation, vehicle, dateLimousineNeeded, arangement);
            CalculateDetailsPrices(extraHours, startHour, endHour);
        }

        /// <summary>
        /// Calculates the prices for the detailes.
        /// </summary>
        /// <param name="extraHours">Amount of extra hours added to the duration of the arangement (if applicable).</param>
        /// <param name="startHour">When the arangement starts (if applicable).</param>
        /// <param name="endHour">When the arangement ends (if applicable).</param>
        private void CalculateDetailsPrices(int? extraHours = null, TimeSpan? startHour = null, TimeSpan? endHour = null)
        {
            Details.CalculatePrices(Client, extraHours, startHour, endHour);
        }

        /// <summary>
        /// Checks if a location is allowed.
        /// </summary>
        /// <param name="location">The location to check.</param>
        /// <returns>A boolean that represents if a location is allowed. If true it is allowed.</returns>
        public static bool LocationAllowed(Address location)
        {
            return location.Town switch
            {
                "Antwerpen" => true,
                "Gent" => true,
                "Brussel" => true,
                "Hasselt" => true,
                "Charleroi" => true,
                _ => false,
            };
        }
    }
}
