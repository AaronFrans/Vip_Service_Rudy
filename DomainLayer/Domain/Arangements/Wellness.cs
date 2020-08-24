
using Newtonsoft.Json;
using System;
using DomainLayer.OtherInterfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Domain.Arangements
{
    /// <summary>
    /// An arangment that has a default price and duration.
    /// </summary>
    public class Wellness : Arangement
    {
        /// <summary>
        /// The default price of the arangement.
        /// </summary>
        public int Price { get; private set; }

        /// <summary>
        /// The amount of hours the arangement lasts by default.
        /// </summary>
        public static int Duration { get; private set; } = 10;

        /// <summary>
        /// Determines if the given start time is allowed.
        /// </summary>
        /// <param name="startHour">The start time.</param>
        /// <returns>A boolean representing whether the given start time is allowed. If true it is allowed.</returns>
        static public bool IsStartAllowed(TimeSpan startHour)
        {
            if (startHour.Hours > 12 || startHour.Hours < 7)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Constructor for wellness object. Sets start hour and end hour.
        /// </summary>
        /// <param name="price">The default price of the arangment.</param>
        [JsonConstructor]
        public Wellness(int price)
        {
            Price = price;
            EndHourTicks = 1440000000000;
            StartHourTicks = 1440000000000;
            StartHour = new TimeSpan(StartHourTicks);
            EndHour = new TimeSpan(EndHourTicks);
        }

        /// <summary>
        /// Sets the start and end time.
        /// </summary>
        /// <param name="startHour">When the arangement starts.</param>
        /// <param name="extraHours">Extra hours to add to the duration of the arangment.</param>
        public void SetTime(TimeSpan startHour)
        {
            if (startHour.Seconds > 0 || startHour.Minutes > 0)
            {
                throw new DomainException("Zorg er a.u.b. voor dat het start uur geen minuten of seconden heeft.");
            }
            if (startHour.Hours > 12 || startHour.Hours < 7)
            {
                throw new DomainException("Het arangement wellness kan aleen tussen 7 en 12 uur in de ochtend geboekt worden.");
            }
            StartHour = startHour;

            EndHour = StartHour.Add(new TimeSpan(Duration, 0, 0));
        }
        /// <summary>
        /// Returns when the arangement ends and resets the arangment.
        /// </summary>
        /// <returns>The and time of the arangement.</returns>
        public TimeSpan GetEndTime()
        {
            TimeSpan toReturn = EndHour;
            StartHour = new TimeSpan(40, 0, 0);
            EndHour = new TimeSpan(40, 0, 0);
            return toReturn;
        }
    }
}
