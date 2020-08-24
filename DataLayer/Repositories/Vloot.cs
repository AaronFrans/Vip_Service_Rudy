using DomainLayer.Domain.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using DomainLayer.Domain.Reservation;
using DomainLayer.OtherInterfaces;
using DomainLayer.Repositories;
using DomainLayer.Domain;
using DomainLayer.Domain.Vloot;
using DataLayer.Func;
using Microsoft.EntityFrameworkCore;
using DomainLayer.Domain.Clients;

namespace DataLayer.Repositories
{
    /// <summary>
    /// Repository for the Limousine and Reservation classes. Implements IVloot interface.
    /// </summary>
    public class Vloot : IVloot
    {

        private ManagerContext context;

        /// <summary>
        /// Constructor to access limousines and reservations in the database.
        /// </summary>
        /// <param name="context">Context used to connect to the database.</param>
        public Vloot(ManagerContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Add a new limousine to the database.
        /// </summary>
        /// <param name="limousine">Limousine to add to database.</param>
        public void AddVehicle(Limousine limousine)
        {
            context.Vehicles.Add(limousine);
        }
        /// <summary>
        /// Add a new list of limousines to the database.
        /// </summary>
        /// <param name="limousines">List of limousines to add to database.</param>
        public void AddVehicles(List<Limousine> limousines)
        {
            context.Vehicles.AddRange(limousines);
        }
        /// <summary>
        /// Retrieves all limousines from the database, as non tracking.
        /// </summary>
        /// <returns>A list of Limousine objects.</returns>
        public List<Limousine> GetVehiclesNonTracking()
        {
            return context.Vehicles.Include(s => s.HireDates)
                                   .Include(s => s.Arangements)
                                   .AsNoTracking().ToList();
        }
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
            int? extraHours = null, TimeSpan? startHour = null, TimeSpan? endHour = null)
        {
            if (!context.Vehicles.AsNoTracking().Any(l => l.Name == (name)))
            {
                throw new DomainException($"Deze Vloot heeft de gevraagde limousine niet.");
            }
            var limo = context.Vehicles.Include(l => l.Arangements)
                                       .Include(l => l.HireDates)
                                       .Single(l => l.Name == name);

            var client = context.Clients.Include(c => c.StaffelDiscount)
                                        .Include(c => c.Address)
                                        .Include(c => c.PastReservations)
                                        .Single(s => s.ClientNumber == clientNr);

            Reservering reservation = new Reservering(reservationDate, client, location);

            reservation.AddDetails(endLocation, limo, dateLimousineNeeded, typeArangement, extraHours, startHour, endHour);

            context.Reservations.Add(reservation);
            return reservation;
        }
        /// <summary>
        /// Retrieves all reservations from the database, as non tracking.
        /// </summary>
        /// <returns>A list of Reservation objects.</returns>        /// <summary>
        /// Retrieves all reservations from the database, as non tracking.
        /// </summary>
        /// <returns>A list of Reservation objects.</returns>
        public List<Reservering> GetReservationsNonTracking()
        {
                return context.Reservations.AsNoTracking()
                                           .Include(r => r.Client)
                                           .Include(r => r.Client.Address)
                                           .Include(r => r.Client.StaffelDiscount)
                                           .Include(r => r.Client.PastReservations)
                                           .Include(r => r.Location)
                                           .Include(r => r.Details)
                                           .Include(r => r.Details.StartLocation)
                                           .Include(r => r.Details.EndLocation)
                                           .Include(r => r.Details.Limousine)
                                           .Include(r => r.Details.Limousine.Arangements)
                                           .Include(r => r.Details.Limousine.HireDates)
                                           .Include(r => r.Details.Hours).ToList();
            
        }
    }
}
