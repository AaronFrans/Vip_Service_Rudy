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
        public void AddClient(Client client);
        public void AddClients(List<Client> clients);

        public List<ClientDiscount> GetDiscountsForType(ClientType type);
        public List<Client> GetClientsNonTracking();
    }
}
