using DomainLayer.Domain.Clients;
using DomainLayer.Domain.Help;
using DomainLayer.Domain.Vloot;
using DomainLayer.OtherInterfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Domain.Reservation
{
    public class Reservering
    {
        [Key]
        public int ReservationNumber { get; set; }
        public DateTime ReservationDate { get; private set; }
        public Client Client { get; private set; }
        public Address Location { get; private set; }
        public Details Details { get; private set; }


        public Reservering()
        {

        }
        public Reservering(DateTime reservationDate, Client client, Address location)
        {
            ReservationDate = reservationDate;
            Client = client;
            if (LocationAllowed(location))
                Location = location;
            else
                throw new DomainException("Limousines zijn aleen beschikbaar in volgende locaties: Antwerpen, Gent, Brussel, Hasselt en Charleroi.");
        }

        public void AddDetails(Address endLocation, Limousine vehicle, DateTime dateLimousineNeeded, string arangement
            , int? extraHours = null, TimeSpan? startHour = null, TimeSpan? endHour = null)
        {
           
            Details = new Details(Location, endLocation, vehicle, dateLimousineNeeded, arangement);
            CalculateDetailsPrices(extraHours, startHour, endHour);
        }

        private void CalculateDetailsPrices(int? extraHours = null, TimeSpan? startHour = null, TimeSpan? endHour = null)
        {
            Details.CalculatePrices(Client, extraHours, startHour, endHour);
        }

        public static bool LocationAllowed(Address location)
        {
            return location.Town switch
            {
                "Antwerpen" => true,
                "Gent" => true,
                "Brussel" => true,
                "Hasselt" => true,
                "Charleroi" => true,
                _ => false,
            };
        }
    }
}
