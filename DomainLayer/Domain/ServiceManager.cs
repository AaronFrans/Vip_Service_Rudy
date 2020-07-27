using DomainLayer.Domain.Clients;
using DomainLayer.Domain.Help;
using DomainLayer.Domain.Reservation;
using DomainLayer.Domain.Vloot;
using DomainLayer.OtherInterfaces;
using System;
using System.Collections.Generic;
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

        public Reservering HireVehicle(string name, string typeArangement, Address location, Client client, DateTime reservationDate, Address endLocation, DateTime dateLimousineNeeded,
            int? extraHours = null, TimeSpan? startHour = null, TimeSpan? endHour = null)
        {
            Reservering reservering = unitOfWork.vloot.HireVehicle(name, typeArangement, location, client,reservationDate,endLocation,dateLimousineNeeded,extraHours,startHour,endHour);
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
        public Client GetClientViaNumber(string name, Address address)
        {
            return unitOfWork.clients.GetClientNonTracking(name, address);
        }

    }
}
