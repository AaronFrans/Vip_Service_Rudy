using DomainLayer.Domain.Help;
using DomainLayer.OtherInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer.Domain.Clients
{
    public class Vip : IKlant
    {
        public Address Address { get; private set; }
        public int ClientNumber { get; set; }
        public string Name { get; private set; }
        public string BtwNumber { get; private set; }
        public Dictionary<int, int> NrOfReservations { get; private set; }
        public List<ClientDiscount> StaffelDiscount { get; private set; }

        public void AddReservation()
        {
            if (NrOfReservations.Keys.Any(d => d == DateTime.Now.Year))
            {
                NrOfReservations[DateTime.Now.Year]++;
            }
            else
            {
                NrOfReservations.Add(DateTime.Now.Year, 1);
            }
        }

        public float GetDiscount()
        {
            if (NrOfReservations.Keys.Any(d => d == DateTime.Now.Year))
            {
                if (NrOfReservations[DateTime.Now.Year] == 0)
                {
                    return 0;
                }
                else
                {

                    var toReturn = StaffelDiscount.Where(s => s.NrOfReservationsNeeded < NrOfReservations[DateTime.Now.Year]).Last().Discount;
                    return toReturn;
                }
            }
            else
            {
                NrOfReservations.Add(DateTime.Now.Year, 0);
                return 0;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Vip vip &&
                   EqualityComparer<Address>.Default.Equals(Address, vip.Address) &&
                   Name == vip.Name &&
                   BtwNumber == vip.BtwNumber &&
                   EqualityComparer<Dictionary<int, int>>.Default.Equals(NrOfReservations, vip.NrOfReservations);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Address, Name, BtwNumber, NrOfReservations);
        }

        public Vip(Address address, string name, string btwNumber, Dictionary<int, int> nrOfReservations, List<ClientDiscount> staffelDiscount)
        {
            Address = address;
            Name = name;
            BtwNumber = btwNumber;
            NrOfReservations = nrOfReservations;
            StaffelDiscount = staffelDiscount;
        }
    }
}
