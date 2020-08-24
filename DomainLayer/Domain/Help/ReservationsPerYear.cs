using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Domain.Help
{
    /// <summary>
    /// Represents the amount of reservations per year for a client.
    /// </summary>
    public class ReservationsPerYear
    {
        /// <summary>
        /// The id of the object.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The year the reservations took place.
        /// </summary>
        public int Year { get; private set; }
        /// <summary>
        /// The number of reservations.
        /// </summary>
        public int NrOfReservations { get; set; }

        /// <summary>
        /// A constructor for a ReservationsPerYear objects.
        /// </summary>
        /// <param name="year">The year the reservations took place.</param>
        /// <param name="nrOfReservations">The number of reservations.</param>
        public ReservationsPerYear(int year, int nrOfReservations)
        {
            Year = year;
            NrOfReservations = nrOfReservations;
        }
    }
}
