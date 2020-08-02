using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Domain.Help
{
    public class HourType
    {
        public int Id { get; set; }
        public string Type { get; private set; }
        public int NrOfHours { get; private set; }
        public int TotalPrice { get; private set; }

        public HourType(string type, int nrOfHours, int totalPrice)
        {
            Type = type;
            NrOfHours = nrOfHours;
            TotalPrice = totalPrice;
        }
    }
}
