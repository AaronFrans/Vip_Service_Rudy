using DomainLayer.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Domain.Arangements
{
    public class Wellness : IArangement
    {
        public int Price { get; private set; }
        public TimeSpan StartHour { get; private set; } = new TimeSpan(40, 0, 0);
        public TimeSpan EndHour { get; private set; } = new TimeSpan(40, 0, 0);
        public int MaxAmountOfHours { get; private set; } = 11;
        public TimeSpan NightHourBegin { get; private set; } = new TimeSpan(22, 0, 0);
        public TimeSpan NightHourEnd { get; private set; } = new TimeSpan(1, 7, 0, 0);

        [JsonConstructor]
        public Wellness(int price)
        {
            Price = price;
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

            EndHour = StartHour.Add(new TimeSpan(10, 0, 0));
        }
    }
}
