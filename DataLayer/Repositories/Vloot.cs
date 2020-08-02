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
            return context.Vehicles.Include(s => s.HireDates)
                                   .Include(s => s.Arangements)
                                   .AsNoTracking().ToList();
        }
        public void UpdateLimousinesAvailability(DateTime hireDate)
        {
            context.Vehicles.Include(c => c.HireDates).ForEachAsync(l => l.IsVehicleAvailable(hireDate));
        }

        public Reservering HireVehicle(string name, string typeArangement, Address location, int clientNr, DateTime reservationDate, Address endLocation, DateTime dateLimousineNeeded,
            int? extraHours = null, TimeSpan? startHour = null, TimeSpan? endHour = null)
        {
            if (!context.Vehicles.AsNoTracking().Any(l => l.Name == (name)))
            {
                throw new DomainException($"Deze vloot heeft de gevraagde limousine niet.");
            }
            var limo = context.Vehicles.Include(l => l.Arangements)
                                       .Include(l => l.HireDates)
                                       .Single(l => l.Name == name);

            var client = context.Clients.Include(c => c.StaffelDiscount)
                                        .Include(c => c.Address)
                                        .Include(c => c.PastReservations)
                                        .Single(s => s.ClientNumber == clientNr);

            Reservering reservation = new Reservering(reservationDate, client, location);

            reservation.AddDetails(endLocation, limo, dateLimousineNeeded, typeArangement, extraHours, startHour, endHour);

            context.Reservations.Add(reservation);
            return reservation;
        }

        public List<Reservering> GetReserveringen(int clientNr)
        {
            if (context.Reservations.Any(r => r.Client.ClientNumber == clientNr))
            {
                return context.Reservations.AsNoTracking()
                                           .Include(r => r.Client)
                                           .Include(r => r.Location)
                                           .Include(r => r.Details)
                                           .Include(r => r.Details.StartLocation)
                                           .Include(r => r.Details.EndLocation)
                                           .Include(r => r.Details.Limousine)
                                           .Include(r => r.Details.Hours)
                                           .Where(r => r.Client.ClientNumber == clientNr).ToList();
            }
            else return null;
        }

        public List<Reservering> GetReserveringen(DateTime reservationDate)
        {
            if (context.Reservations.Any(r => r.ReservationDate == reservationDate))
            {
                return context.Reservations.AsNoTracking()
                                           .Include(r => r.Client)
                                           .Include(r => r.Location)
                                           .Include(r => r.Details)
                                           .Include(r => r.Details.StartLocation)
                                           .Include(r => r.Details.EndLocation)
                                           .Include(r => r.Details.Limousine)
                                           .Include(r => r.Details.Hours)
                                           .Where(r => r.ReservationDate == reservationDate).ToList();
            }
            else return null;
        }

        public List<Reservering> GetReserveringen(int clientNr, DateTime reservationDate)
        {
            if (context.Reservations.Any(r => r.ReservationDate == reservationDate && r.Client.ClientNumber == clientNr))
            {
                return context.Reservations.AsNoTracking()
                                           .Include(r => r.Client)
                                           .Include(r => r.Location)
                                           .Include(r => r.Details)
                                           .Include(r => r.Details.StartLocation)
                                           .Include(r => r.Details.EndLocation)
                                           .Include(r => r.Details.Limousine)
                                           .Include(r => r.Details.Hours)
                                           .Where(r => r.ReservationDate == reservationDate && r.Client.ClientNumber == clientNr).ToList();
            }
            else return null;
        }
    }
}
