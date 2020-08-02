using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Domain.Help
{
    public class ReservationsPerYear
    {
        public int Id { get; set; }
        public int Year { get; private set; }
        public int NrOfReservations { get; set; }

        public ReservationsPerYear(int year, int nrOfReservations)
        {
            Year = year;
            NrOfReservations = nrOfReservations;
        }
    }
}
