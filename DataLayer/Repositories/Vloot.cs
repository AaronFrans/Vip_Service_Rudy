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

namespace DataLayer.Repositories
{
    public class Vloot : IVloot
    {

        private ManagerContext context;

        public Vloot(ManagerContext context)
        {
            this.context = context;
        }

        public void AddVehicle(IVehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
            context.SaveChanges();
        }
        public void AddVehicles(List<IVehicle> vehicles)
        {
            context.Vehicles.AddRange(vehicles);
            context.SaveChanges();
        }

        public List<IVehicle> GetVehiclesNonTracking()
        {
            return context.Vehicles.AsNoTracking().ToList();
        }

        public void HireVehicle(string name, string typeArangement, Address location, IKlant client, DateTime reservationDate, Address endLocation, DateTime dateLimousineNeeded,
            int? extraHours = null, TimeSpan? startHour = null, TimeSpan? endHour = null)
        {
            if (!context.Vehicles.Any(l => (l as Limousine).Name == (name)))
            {
                throw new DomainException($"Deze vloot heeft de gevraagde limousine niet.");
            }
            var limo = context.Vehicles.Single(l => (l as Limousine).Name == name);

            Reservering reservation = new Reservering(reservationDate, client, location);

            reservation.AddDetails(endLocation, limo, dateLimousineNeeded, typeArangement, extraHours, startHour, endHour);\

            context.Reservations.Add(reservation);
        }

    }
}
