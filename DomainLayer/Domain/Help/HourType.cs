using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Domain.Help
{
    /// <summary>
    /// Used to represent a type of hour with the price and the duration.
    /// </summary>
    public class HourType
    {
        /// <summary>
        /// the id for the hour
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The type of hours.
        /// </summary>
        public string Type { get; private set; }
        /// <summary>
        /// The duration.
        /// </summary>
        public int NrOfHours { get; private set; }
        /// <summary>
        /// The total price.
        /// </summary>
        public int TotalPrice { get; private set; }

        /// <summary>
        /// A cosntructor used to make a HourType object.
        /// </summary>
        /// <param name="type">The type of hours.</param>
        /// <param name="nrOfHours">The duration.</param>
        /// <param name="totalPrice">The total price.</param>
        public HourType(string type, int nrOfHours, int totalPrice)
        {
            Type = type;
            NrOfHours = nrOfHours;
            TotalPrice = totalPrice;
        }
    }
}
