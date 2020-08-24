using DomainLayer.Domain.Clients;
using DomainLayer.Domain.Help;
using DomainLayer.OtherInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Repositories
{
    public interface IClientRepository
    {
        /// <summary>
        /// Constructor to access clients in the database.
        /// </summary>
        /// <param name="context">Context used to connect to the database.</param>
        public void AddClient(Client client);
        /// <summary>
        /// Add a new client to the database.
        /// </summary>
        /// <param name="client">Client to add to database.</param>
        public void AddClients(List<Client> clients);

        /// <summary>
        /// Add a new list of clients to the database.
        /// </summary>
        /// <param name="client">List of client to add to database.</param>
        public List<ClientDiscount> GetDiscountsForType(ClientType type);
        /// <summary>
        /// Retrieves all clients from the database, as non tracking.
        /// </summary>
        /// <returns>A list of Client objects.</returns>
        public List<Client> GetClientsNonTracking();
    }
}
