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
            Reservering reservering = unitOfWork.vloot.HireVehicle(name, typeArangement, location, clientNr, reservationDate,endLocation,dateLimousineNeeded,extraHours,startHour,endHour);
            unitOfWork.Complete();

            return reservering;
        }
        public List<Limousine> GetVehicles()
        {
            return unitOfWork.vloot.GetVehiclesNonTracking();
        }
        public void AddVehicle(Limousine vehicle)
        {
            unitOfWork.vloot.AddVehicle(vehicle);
            unitOfWork.Complete();
        }
        public void AddVehicles(List<Limousine> vehicles)
        {
            unitOfWork.vloot.AddVehicles(vehicles);
            unitOfWork.Complete();
        }

        public void AddClient(Client client)
        {
            unitOfWork.clients.AddClient(client);
            unitOfWork.Complete();
        }
        public void AddClients(List<Client> clients)
        {
            unitOfWork.clients.AddClients(clients);
            unitOfWork.Complete();
        }

        public Client GetClientViaNumber(int clientNumber)
        {
            return unitOfWork.clients.GetClientNonTracking(clientNumber);
        }
        public Client GetClientViaInfo(string name)
        {
            return unitOfWork.clients.GetClientNonTracking(name);
        }
        public List<Client> GetClients()
        {
            return unitOfWork.clients.GetClientsNonTracking();
        }

        public List<ClientDiscount> GetDiscountsForType(ClientType type)
        {
            return unitOfWork.clients.GetDiscountsForType(type);
        }

        public void UpdateVehicles(DateTime hireDate)
        {
            unitOfWork.vloot.UpdateLimousinesAvailability(hireDate);
            unitOfWork.Complete();
        }

    }
} 
