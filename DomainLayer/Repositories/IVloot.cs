using DomainLayer.Domain.Clients;
using DomainLayer.Domain.Help;
using DomainLayer.Domain.Reservation;
using DomainLayer.Domain.Vloot;
using DomainLayer.OtherInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Repositories
{
    public interface IVloot
    {
        /// <summary>
        /// Add a new limousine to the database.
        /// </summary>
        /// <param name="limousine">Limousine to add to database.</param>
        public void AddVehicle(Limousine vehicle);
        /// <summary>
        /// Add a new list of limousines to the database.
        /// </summary>
        /// <param name="limousines">List of limousines to add to database.</param>
        public void AddVehicles(List<Limousine> vehicles);
        /// <summary>
        /// Retrieves all limousines from the database, as non tracking.
        /// </summary>
        /// <returns>A list of Limousine objects.</returns>
        public List<Limousine> GetVehiclesNonTracking();
        /// <summary>
        /// Hires a limousine, and saves the reservation in the database.
        /// </summary>
        /// <param name="name">Name of the limousine that will be reserved.</param>
        /// <param name="typeArangement">The type of arangement used.</param>
        /// <param name="location">Location where the limousine picks up the client.</param>
        /// <param name="clientNr">ClientNumber of the client that wants to reserve the limousine.</param>
        /// <param name="reservationDate">Date of reservation.</param>
        /// <param name="endLocation">Location where the limousine drops the client off.</param>
        /// <param name="dateLimousineNeeded">When the limousine picks up the client.</param>
        /// <param name="extraHours">Extra hours added to the price of the arangement (if applicable).</param>
        /// <param name="startHour">Star thour of the arangement (if applicable).</param>
        /// <param name="endHour">End hour of the arangement (if applicable).</param>
        /// <returns>The reservation that was added to the database</returns>
        public Reservering HireVehicle(string name, string typeArangement, Address location, int clientNr, DateTime reservationDate, Address endLocation, DateTime dateLimousineNeeded,
             int? extraHours = null, TimeSpan? startHour = null, TimeSpan? endHour = null);

        /// <summary>
        /// Retrieves all reservations from the database, as non tracking.
        /// </summary>
        /// <returns>A list of Reservation objects.</returns>        /// <summary>
        /// Retrieves all reservations from the database, as non tracking.
        /// </summary>
        /// <returns>A list of Reservation objects.</returns>
        public List<Reservering> GetReservationsNonTracking();

    }
}
