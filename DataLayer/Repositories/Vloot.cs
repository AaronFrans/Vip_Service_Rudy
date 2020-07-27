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
using DomainLayer.Domain.Clients;

namespace DataLayer.Repositories
{
    public class Vloot : IVloot
    {

        private ManagerContext context;

        public Vloot(ManagerContext context)
        {
            this.context = context;
        }

        public void AddVehicle(Limousine limousine)
        {
            context.Vehicles.Add(limousine);
        }
        public void AddVehicles(List<Limousine> limousines)
        {
            context.Vehicles.AddRange(limousines);
        }

        public List<Limousine> GetVehiclesNonTracking()
        {
            return context.Vehicles.AsNoTracking().ToList();
        }
        public Reservering HireVehicle(string name, string typeArangement, Address location, Client client, DateTime reservationDate, Address endLocation, DateTime dateLimousineNeeded,
            int? extraHours = null, TimeSpan? startHour = null, TimeSpan? endHour = null)
        {
            if (!context.Vehicles.Any(l => l.Name == (name)))
            {
                throw new DomainException($"Deze vloot heeft de gevraagde limousine niet.");
            }
            var limo = context.Vehicles
                                       .Include(l => l.Arangements)
                                       .Include(l=> l.HireDates)
                                       .Single(l => l.Name == name);

            Reservering reservation = new Reservering(reservationDate, client, location);

            reservation.AddDetails(endLocation, limo, dateLimousineNeeded, typeArangement, extraHours, startHour, endHour);

            context.Reservations.Add(reservation);
            return reservation;
        }

    }
}
