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
        public void AddVehicle(Limousine vehicle);
        public void AddVehicles(List<Limousine> vehicles);
        public List<Limousine> GetVehiclesNonTracking();
        public Reservering HireVehicle(string name, string typeArangement, Address location, int clientNr, DateTime reservationDate, Address endLocation, DateTime dateLimousineNeeded,
             int? extraHours = null, TimeSpan? startHour = null, TimeSpan? endHour = null);

        public void UpdateLimousinesAvailability(DateTime hireDate);

        public List<Reservering> GetReservations();

    }
}
