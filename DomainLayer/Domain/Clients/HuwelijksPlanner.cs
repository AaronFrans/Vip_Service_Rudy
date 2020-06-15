using DomainLayer.Domain.Help;
using DomainLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer.Domain.Clients
{
    public class HuwelijksPlanner : IKlant
    {
        public int ClientNumber { get; private set; }
        public string Name { get; private set; }
        public int? BtwNumber { get; private set; }
        public int NrOfReservations { get; private set; }
        public List<ClientDiscount> StaffelDiscount { get; private set; }

        public void AddReservation()
        {
            NrOfReservations++;
        }

        public float GetDiscount()
        {
            if (NrOfReservations == 0)
            {
                return 0;
            }
            else
            {

                var toReturn = StaffelDiscount.Where(s => s.NrOfReservationsNeeded < NrOfReservations).Last().Discount;
                return toReturn;
            }
        }

        public HuwelijksPlanner(string name, int? btwNumber, List<ClientDiscount> discounts)
        {
            Name = name;
            BtwNumber = btwNumber;
            StaffelDiscount = discounts;
            NrOfReservations = 0;
        }
    }
}

