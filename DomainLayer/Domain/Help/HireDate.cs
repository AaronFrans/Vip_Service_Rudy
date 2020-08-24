using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Domain.Help
{
    /// <summary>
    /// Represents when a limousine was hired.
    /// </summary>
    public class HireDate
    {
        /// <summary>
        /// the id of the reservation date.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The date the limousine was hired.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// A constructor used to make a HireDate object.
        /// </summary>
        /// <param name="date">The date the limousine was hired.</param>
        public HireDate(DateTime date)
        {
            Date = date;
        }
    }
}
