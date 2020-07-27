using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Domain.Help
{
    public class HourType
    {
        public int Id { get; set; }
        public string Type;
        public int NrOfHours;
        public int TotalPrice;

        public HourType(string type, int nrOfHours, int totalPrice)
        {
            Type = type;
            NrOfHours = nrOfHours;
            TotalPrice = totalPrice;
        }
    }
}
