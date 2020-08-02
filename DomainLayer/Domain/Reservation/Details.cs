using DomainLayer.Domain.Arangements;
using DomainLayer.Domain.Clients;
using DomainLayer.Domain.Help;
using DomainLayer.Domain.Vloot;
using DomainLayer.OtherInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Domain.Reservation
{
    public class Details
    {
        public int Id { get; set; }
        public Address StartLocation { get; private set; }
        public Address EndLocation { get; private set; }
        public Limousine Limousine { get; private set; }
        public DateTime DateLimousineNeeded { get; private set; }
        public string Arangement { get; private set; }
        public List<HourType> Hours { get; private set; }
        public int SubTotal { get; private set; }
        public float UsedDiscount { get; private set; }
        public int AmountWithoutBtw { get; private set; }
        public float BtwPercentage { get; private set; }
        public int BtwAmount { get; private set; }
        public int ToPayAmount { get; private set; }

        public Details()
        {
                
        }
        public Details(Address startLocation, Address endLocation, Limousine limousine, DateTime dateLimousineNeeded, string arangement)
        {
            if (!Reservering.LocationAllowed(startLocation))
                throw new DomainException("Start locatie van de limousine beschikbaar in volgende locaties: Antwerpen, Gent, Brussel, Hasselt en Charleroi.");
            if (!Reservering.LocationAllowed(endLocation))
                throw new DomainException("Eind locatie van de limousine beschikbaar in volgende locaties: Antwerpen, Gent, Brussel, Hasselt en Charleroi.");
            StartLocation = startLocation;
            EndLocation = endLocation;
            Limousine = limousine;
            DateLimousineNeeded = dateLimousineNeeded;
            Arangement = arangement;
            BtwPercentage = 6.0f;
        }

        public void CalculatePrices(Client client, int? extraHours = null, TimeSpan? startHour = null, TimeSpan? endHour = null)
        {
            var result = Limousine.PriceForArangement(DateLimousineNeeded, Arangement, extraHours, startHour, endHour);

            Hours = result.Value;
            SubTotal = result.Key;
            UsedDiscount = client.GetDiscount();
            AmountWithoutBtw = (int)(SubTotal - (SubTotal * (UsedDiscount / 100.0f)));
            BtwAmount = (int)(AmountWithoutBtw * (BtwPercentage / 100.0f));
            ToPayAmount = AmountWithoutBtw + BtwAmount;
        }
    }
}
