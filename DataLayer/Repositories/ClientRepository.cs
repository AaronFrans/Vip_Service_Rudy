﻿using DataLayer.Func;
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
    public class ClientRepository : IClientRepository
    {
        private ManagerContext context;

        public ClientRepository(ManagerContext context)
        {
            this.context = context;
        }

        public void AddClient(Client client)
        {
            if (client.ClientNumber == 0)
            {
                client.ClientNumber = context.Clients.Count() +1;
            }
            context.Clients.Add(client);
        }
        public void AddClients(List<Client> clients)
        {
            context.Clients.AddRange(clients);
        }

        public List<ClientDiscount> GetDiscountsForType(ClientType type)
        {
            return context.Clients.Include(c => c.StaffelDiscount).Single(c => c.Type == type).StaffelDiscount;
        }

        public List<Client> GetClientsNonTracking()
        {
            return context.Clients.Include(c => c.StaffelDiscount)
                                  .Include(c => c.Address)
                                  .Include(c => c.PastReservations)
                                  .AsNoTracking().ToList(); ;
        }
        public Client GetClientNonTracking(int clientNumber)
        {
            if(context.Clients.AsNoTracking().ToList().Any(c => c.ClientNumber == clientNumber))
            {
                return context.Clients.AsNoTracking()
                                      .Include(c => c.StaffelDiscount)
                                      .Include(c => c.Address)
                                      .Include(c => c.PastReservations)
                                      .AsNoTracking()
                                      .ToList()
                                      .Single(c => c.ClientNumber == clientNumber);
            }
            else return null;
        }
        public Client GetClientNonTracking(string name)
        {
            if (context.Clients.AsNoTracking().ToList().Any(c => c.Name == name))
            {
                return context.Clients.AsNoTracking()
                                      .Include(c => c.StaffelDiscount)
                                      .Include(c => c.Address)
                                      .Include(c => c.PastReservations)
                                      .AsNoTracking()
                                      .ToList()
                                      .Single(c => c.Name == name);
            }
            else return null;
        }
    }
}
