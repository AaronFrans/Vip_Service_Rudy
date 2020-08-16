using DomainLayer.Domain.Help;
using DomainLayer.OtherInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DomainLayer.Domain.Clients
{
    public class Client
    {
        [Key]
        public int ClientNumber { get; set; }
        public ClientType Type { get; private set; }
        public List<ClientDiscount> StaffelDiscount { get; private set; }
        public Address Address { get; private set; }
        public string Name { get; private set; }
        public string BtwNumber { get; private set; }
        public List<ReservationsPerYear> PastReservations { get; private set; }

        public Client()
        {

        }
        public Client(ClientType type, List<ClientDiscount> staffelDiscount, Address address, string name, string btwNumber, List<ReservationsPerYear> pastReservations)
        {
            Type = type;
            StaffelDiscount = staffelDiscount;
            Address = address;
            Name = name;
            BtwNumber = btwNumber;
            PastReservations = pastReservations;
        }
        public void AddReservation()
        {
            if (PastReservations.Any(d => d.Year == DateTime.Now.Year))
            {
                PastReservations.Single(d => d.Year == DateTime.Now.Year).NrOfReservations++;
            }
            else
            {
                PastReservations.Add(new ReservationsPerYear(DateTime.Now.Year,0));
            }
        }
        public float GetDiscount()
        {
            if (PastReservations.Any(d => d.Year == DateTime.Now.Year))
            {
                if (PastReservations.Single(d => d.Year == DateTime.Now.Year).NrOfReservations == 0)
                {
                    return 0;
                }
                else
                {

                    var toReturn = StaffelDiscount.Where(s => s.NrOfReservationsNeeded < PastReservations.Single(d => d.Year == DateTime.Now.Year).NrOfReservations).Last().Discount;
                    return toReturn;
                }
            }
            else
            {
                PastReservations.Add(new ReservationsPerYear(DateTime.Now.Year, 0));

                return 0;
            }

        }

    }
}
