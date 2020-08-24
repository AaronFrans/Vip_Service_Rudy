using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainLayer.Domain.Arangements
{
    /// <summary>
    /// Used to determine the price of a reservation.
    /// </summary>
    public abstract class Arangement
    {
        /// <summary>
        /// Id of the arangement.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Start time represented in ticks.
        /// </summary>
        public long StartHourTicks { get; protected set; }
        /// <summary>
        /// End time represented in ticks.
        /// </summary>
        public long EndHourTicks { get; protected set; }

        /// <summary>
        /// Start time represented in days, hours, minutes and seconds.
        /// </summary>
        [NotMapped]
        public TimeSpan StartHour { get; protected set; }
        /// <summary>
        /// End time represented in days, hours, minutes and seconds.
        /// </summary>
        [NotMapped]
        public TimeSpan EndHour { get; protected set; }
        
        /// <summary>
        /// The maximum duration for an arangement.
        /// </summary>
        static public int MaxAmountOfHours { get; private set; } = 11;


    }
}
