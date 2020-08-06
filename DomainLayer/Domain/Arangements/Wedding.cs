
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using DomainLayer.Domain.Help;
using DomainLayer.OtherInterfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Domain.Arangements
{
    public class Wedding : Arangement
    {
        public int Price { get; private set; }

        public static int Duration { get; private set; } = 7;

        static public TimeSpan NightHourBegin { get; private set; } = new TimeSpan(22, 0, 0);

        static public TimeSpan NightHourEnd { get; private set; } = new TimeSpan(1, 6, 0, 0);


        static public float SecondHoursPercentage { get; private set; } = 65.0f;
        [NotMapped]
        public int? ExtraHours { get; private set; } = null;

        static public bool IsStartAllowed(TimeSpan startHour)
        {
            if (startHour.Hours > 15 || startHour.Hours < 7)
            {
                return false;
            }
            return true;
        }
        public KeyValuePair<int, List<HourType>> GetCalculatedPrice(int firstHourPrice)
        {
            if (EndHour.TotalHours == 40)
            {
                throw new DomainException("Zorg er aub voor dat de start- en eindtijd ingevuld zijn");
            }
            int returnPrice = Price;
            List<HourType> hourTypes = new List<HourType>();

            if (ExtraHours != null)
            {
                int extraHourPrice = (int)ExtraHours * (5 * (int)Math.Round((firstHourPrice * (SecondHoursPercentage / 100.0)) / 5.0));
                returnPrice += extraHourPrice;
                hourTypes.Add(new HourType("Extra uren", (int)ExtraHours, extraHourPrice));
            }

            ExtraHours = null;

            return new KeyValuePair<int, List<HourType>>(returnPrice, hourTypes);
        }
        public void SetTime(TimeSpan startHour, int? extraHours)
        {
            if (startHour.Seconds > 0 || startHour.Minutes > 0)
            {
                throw new DomainException("Zorg er a.u.b. voor dat het start uur geen minuten of seconden heeft.");
            }
            if (startHour.Hours > 15 || startHour.Hours < 7)
            {
                throw new DomainException("Het arangement wellness kan aleen tussen 7 en 12 uur in de ochtend geboekt worden.");
            }
            StartHour = startHour;

            if (extraHours < 0)
                throw new DomainException("Je kan geen negatief aantal extra uren hebben");
            ExtraHours = extraHours;

            if (extraHours != null)
            {
                EndHour = new TimeSpan(startHour.Hours + Duration + (int)extraHours, 0, 0);
                if ((EndHour.TotalHours - StartHour.TotalHours) > MaxAmountOfHours)
                {
                    throw new DomainException("Zorg er a.u.b. voor dat het eind uur niet meer dan elf uur na het start uur is. (Het Wedding arangement heeft een standaartduuratie van 8 uur)");
                }
            }
            else
            {
                EndHour = new TimeSpan(startHour.Hours + Duration, 0, 0);

            }
        }

        public TimeSpan GetEndTime()
        {
            TimeSpan toReturn = EndHour;
            StartHour = new TimeSpan(40, 0, 0);
            EndHour = new TimeSpan(40, 0, 0);
            return toReturn;
        }

        [JsonConstructor]
        public Wedding(int price)
        {
            Price = price;
            EndHourTicks = 1440000000000;
            StartHourTicks = 1440000000000;
            StartHour = new TimeSpan(StartHourTicks);
            EndHour = new TimeSpan(EndHourTicks);
        }
    }
}
