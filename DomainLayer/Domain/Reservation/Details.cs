﻿using DomainLayer.Domain.Arangements;
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
        public Address StartLocation;
        public Address EndLocation;
        public Limousine Limousine;
        public DateTime DateLimousineNeeded;
        public string Arangement;
        public List<HourType> Hours;
        public int SubTotal;
        public float UsedDiscount;
        public int AmountWithoutBtw;
        public float BtwPercentage;
        public int BtwAmount;
        public int ToPayAmount;

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
