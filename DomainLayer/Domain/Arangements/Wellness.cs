
using Newtonsoft.Json;
using System;
using DomainLayer.OtherInterfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Domain.Arangements
{
    public class Wellness : Arangement
    {
        public int Price { get; private set; }

        public static int Duration { get; private set; } = 10;

        [JsonConstructor]
        public Wellness(int price)
        {
            Price = price;
            EndHourTicks = 1440000000000;
            StartHourTicks = 1440000000000;
            StartHour = new TimeSpan(StartHourTicks);
            EndHour = new TimeSpan(EndHourTicks);
        }

        static public bool IsStartAllowed(TimeSpan startHour)
        {
            if (startHour.Hours > 12 || startHour.Hours < 7)
            {
                return false;
            }
            return true;
        }

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

        public TimeSpan GetEndTime()
        {
            TimeSpan toReturn = EndHour;
            StartHour = new TimeSpan(40, 0, 0);
            EndHour = new TimeSpan(40, 0, 0);
            return toReturn;
        }
    }
}
