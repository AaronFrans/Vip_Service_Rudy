
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using DomainLayer.Domain.Help;
using DomainLayer.OtherInterfaces;

namespace DomainLayer.Domain.Arangements
{
    public class Wedding : IArangement
    {
        public int Price { get; private set; }
        public TimeSpan StartHour { get; private set; } = new TimeSpan(7, 0, 0);
        public TimeSpan EndHour { get; private set; } = new TimeSpan(40, 0, 0);
        public TimeSpan NightHourBegin { get; private set; } = new TimeSpan(22, 0, 0);
        public TimeSpan NightHourEnd { get; private set; } = new TimeSpan(1, 7, 0, 0);
        public int MaxAmountOfHours { get; private set; } = 11;
        public float SecondHoursPercentage { get; private set; } = 65.0f;
        public int? ExtraHours { get; private set; } = null;


        public KeyValuePair<int, List<HourType>> GetCalculatedPrice(int firstHourPrice)
        {
            if (EndHour.TotalHours == 40)
            {
                throw new DomainException("Zorg er aub voor dat de start- en eindtijd ingevuld zijn");
            }
            int returnPrice = Price;
            List<HourType> hourTypes = new List<HourType>() { new HourType("Eerste uur", 0, 0) };

            if (ExtraHours != null)
            {
                int extraHourPrice = (int)ExtraHours * (5 * (int)Math.Round((firstHourPrice * (SecondHoursPercentage / 100.0)) / 5.0));
                returnPrice += extraHourPrice;
                hourTypes.Add(new HourType("Extra uren", (int)ExtraHours, extraHourPrice));
            }

            ExtraHours = null;

            return new KeyValuePair<int, List<HourType>>(returnPrice, hourTypes);
        }
        public void SetTime(int? extraHours)
        {

            if (extraHours < 0)
                throw new DomainException("Je kan geen negatief aantal extra uren hebben");
            ExtraHours = extraHours;

            if (extraHours != null)
            {
                EndHour = new TimeSpan(15 + (int)extraHours, 0, 0);
                if ((EndHour.TotalHours - StartHour.TotalHours) > MaxAmountOfHours)
                {
                    throw new DomainException("Zorg er a.u.b. voor dat het eind uur niet meer dan elf uur na het start uur is. (Het Wedding arangement heeft een standaartduuratie van 8 uur)");
                }
            }
            else
            {
                EndHour = new TimeSpan(15, 0, 0);

            }
        }

        public TimeSpan GetEndTime()
        {
            TimeSpan toReturn = EndHour;
            EndHour = new TimeSpan(40, 0, 0);
            return toReturn;
        }

        [JsonConstructor]
        public Wedding(int price)
        {
            Price = price;
        }
    }
}
