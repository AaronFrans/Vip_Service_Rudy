using DomainLayer.Domain.Clients;
using DomainLayer.Domain.Help;
using DomainLayer.Domain.Reservation;
using DomainLayer.Domain.Vloot;
using DomainLayer.OtherInterfaces;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace DomainLayer.Domain
{
    /// <summary>
    /// Manages the database.
    /// </summary>
    public class ServiceManager
    {
        private IUnitOfWork unitOfWork;

        /// <summary>
        /// A constructor to make a IUnitOfWork object.
        /// </summary>
        /// <param name="unitOfWork">The link to the database.</param>
        public ServiceManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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
            Reservering reservering = unitOfWork.Vloot.HireVehicle(name, typeArangement, location, clientNr, reservationDate,endLocation,dateLimousineNeeded,extraHours,startHour,endHour);
            unitOfWork.Complete();

            return reservering;
        }
        /// <summary>
        /// Retrieves all limousines from the database.
        /// </summary>
        /// <returns>A list of Limousine objects.</returns>
        public List<Limousine> GetVehicles()
        {
            return unitOfWork.Vloot.GetVehiclesNonTracking();
        }
        /// <summary>
        /// Add a new limousine to the database.
        /// </summary>
        /// <param name="vehicle">Limousine to add to database.</param>
        public void AddVehicle(Limousine vehicle)
        {
            unitOfWork.Vloot.AddVehicle(vehicle);
            unitOfWork.Complete();
        }
        /// <summary>
        /// Add a new list of limousines to the database.
        /// </summary>
        /// <param name="vehicles">List of limousines to add to database.</param>
        public void AddVehicles(List<Limousine> vehicles)
        {
            unitOfWork.Vloot.AddVehicles(vehicles);
            unitOfWork.Complete();
        }


        /// <summary>
        /// Add a new client to the database.
        /// </summary>
        /// <param name="client">Client to add to database.</param>
        public void AddClient(Client client)
        {
            unitOfWork.Clients.AddClient(client);
            unitOfWork.Complete();
        }
        /// <summary>
        /// Add a new list of clients to the database.
        /// </summary>
        /// <param name="client">List of client to add to database.</param>
        public void AddClients(List<Client> clients)
        {
            unitOfWork.Clients.AddClients(clients);
            unitOfWork.Complete();
        }
        /// <summary>
        /// Retrieves all clients from the database.
        /// </summary>
        /// <returns>A list of Client objects.</returns>
        public List<Client> GetClients()
        {
            return unitOfWork.Clients.GetClientsNonTracking();
        }

        /// <summary>
        /// Retrieve from the database the discounts for a given client type.
        /// </summary>
        /// <param name="type">The ClientType to retrieve the discounts for.</param>
        /// <returns>A list of ClientDiscount objects.</returns>
        public List<ClientDiscount> GetDiscountsForType(ClientType type)
        {
            return unitOfWork.Clients.GetDiscountsForType(type);
        }

        /// <summary>
        /// Retrieves all reservations from the database, as non tracking.
        /// </summary>
        /// <returns>A list of Reservation objects.</returns>
        public List<Reservering> GetReservations()
        {
            return unitOfWork.Vloot.GetReservationsNonTracking();
        }

    }
} 
