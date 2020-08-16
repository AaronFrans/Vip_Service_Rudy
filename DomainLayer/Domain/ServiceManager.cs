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
    public class ServiceManager
    {
        private IUnitOfWork unitOfWork;

        public ServiceManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Reservering HireVehicle(string name, string typeArangement, Address location, int clientNr, DateTime reservationDate, Address endLocation, DateTime dateLimousineNeeded,
            int? extraHours = null, TimeSpan? startHour = null, TimeSpan? endHour = null)
        {
            Reservering reservering = unitOfWork.Vloot.HireVehicle(name, typeArangement, location, clientNr, reservationDate,endLocation,dateLimousineNeeded,extraHours,startHour,endHour);
            unitOfWork.Complete();

            return reservering;
        }
        public List<Limousine> GetVehicles()
        {
            return unitOfWork.Vloot.GetVehiclesNonTracking();
        }
        public void AddVehicle(Limousine vehicle)
        {
            unitOfWork.Vloot.AddVehicle(vehicle);
            unitOfWork.Complete();
        }
        public void AddVehicles(List<Limousine> vehicles)
        {
            unitOfWork.Vloot.AddVehicles(vehicles);
            unitOfWork.Complete();
        }

        public void AddClient(Client client)
        {
            unitOfWork.Clients.AddClient(client);
            unitOfWork.Complete();
        }
        public void AddClients(List<Client> clients)
        {
            unitOfWork.Clients.AddClients(clients);
            unitOfWork.Complete();
        }
        public List<Client> GetClients()
        {
            return unitOfWork.Clients.GetClientsNonTracking();
        }

        public List<ClientDiscount> GetDiscountsForType(ClientType type)
        {
            return unitOfWork.Clients.GetDiscountsForType(type);
        }

        public List<Reservering> GetReservations()
        {
            return unitOfWork.Vloot.GetReservations();
        }

    }
} 
