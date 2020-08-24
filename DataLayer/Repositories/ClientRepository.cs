using DataLayer.Func;
using DomainLayer.Domain.Clients;
using DomainLayer.Domain.Help;
using DomainLayer.OtherInterfaces;
using DomainLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repositories
{
    /// <summary>
    /// Repository for the Client class. Implements IClientRepository interface.
    /// </summary>
    public class ClientRepository : IClientRepository
    {
        private ManagerContext context;

        /// <summary>
        /// Constructor to access clients in the database.
        /// </summary>
        /// <param name="context">Context used to connect to the database.</param>
        public ClientRepository(ManagerContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Add a new client to the database.
        /// </summary>
        /// <param name="client">Client to add to database.</param>
        public void AddClient(Client client)
        {
            if (client.ClientNumber == 0)
            {
                client.ClientNumber = context.Clients.Count() +1;
            }
            context.Clients.Add(client);
        }
        /// <summary>
        /// Add a new list of clients to the database.
        /// </summary>
        /// <param name="client">List of client to add to database.</param>
        public void AddClients(List<Client> clients)
        {
            context.Clients.AddRange(clients);
        }
        /// <summary>
        /// Retrieve from the database the discounts for a given client type.
        /// </summary>
        /// <param name="type">The ClientType to retrieve the discounts for.</param>
        /// <returns>A list of ClientDiscount objects.</returns>
        public List<ClientDiscount> GetDiscountsForType(ClientType type)
        {
            List<ClientDiscount> toReturn = null;
            if(context.Clients.Include(c => c.StaffelDiscount).Any(c => c.Type == type))
            {
                toReturn = context.Clients.Include(c => c.StaffelDiscount).Single(c => c.Type == type).StaffelDiscount;
            }
            else
            {
                toReturn =  Parser.GetDiscounts().Where(d => d.ClientType == type).ToList();
            }
            return toReturn;
        }
        /// <summary>
        /// Retrieves all clients from the database, as non tracking.
        /// </summary>
        /// <returns>A list of Client objects.</returns>
        public List<Client> GetClientsNonTracking()
        {
            return context.Clients.Include(c => c.StaffelDiscount)
                                  .Include(c => c.Address)
                                  .Include(c => c.PastReservations)
                                  .AsNoTracking().ToList(); ;
        }
       }
}
