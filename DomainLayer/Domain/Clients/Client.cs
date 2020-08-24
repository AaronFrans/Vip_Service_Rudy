using DomainLayer.Domain.Help;
using DomainLayer.OtherInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DomainLayer.Domain.Clients
{
    /// <summary>
    /// A client that can make reservatinos.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Used to identify the client.
        /// </summary>
        [Key]
        public int ClientNumber { get; set; }
        /// <summary>
        /// Type of client.
        /// </summary>
        public ClientType Type { get; private set; }
        /// <summary>
        /// A list of discounts used to determine the right discount.
        /// </summary>
        public List<ClientDiscount> StaffelDiscount { get; private set; }
        /// <summary>
        /// The client's address.
        /// </summary>
        public Address Address { get; private set; }
        /// <summary>
        /// Thew client's name.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Thew client's vat number if they have one..
        /// </summary>
        public string BtwNumber { get; private set; }
        /// <summary>
        /// Thew client's past reservations.
        /// </summary>
        public List<ReservationsPerYear> PastReservations { get; private set; }

        /// <summary>
        /// An empty constructor.
        /// </summary>
        public Client()
        {

        }
        /// <summary>
        /// A constructor used to make a client object.
        /// </summary>
        /// <param name="type">The type of client.</param>
        /// <param name="staffelDiscount">The possible discounts for client.</param>
        /// <param name="address">The client's address.</param>
        /// <param name="name">The client's name.</param>
        /// <param name="btwNumber">The client's btwNumber.</param>
        /// <param name="pastReservations">The client's past reservations.</param>
        public Client(ClientType type, List<ClientDiscount> staffelDiscount, Address address, string name, string btwNumber, List<ReservationsPerYear> pastReservations)
        {
            Type = type;
            StaffelDiscount = staffelDiscount;
            Address = address;
            Name = name;
            BtwNumber = btwNumber;
            PastReservations = pastReservations;
        }
        /// <summary>
        /// Adds a reservation to the client's past reservations.
        /// </summary>
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
        /// <summary>
        /// Gets the current discount used for the client.
        /// </summary>
        /// <returns>A float representing the used discount.</returns>
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
