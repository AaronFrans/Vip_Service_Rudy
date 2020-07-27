using DomainLayer.Domain.Help;
using DomainLayer.OtherInterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Domain.Arangements
{
    public class NightLife: Arangement
    {
        public int Price { get; private set; }

        static public TimeSpan NightHourBegin { get; private set; } = new TimeSpan(22, 0, 0);
       
        static public TimeSpan NightHourEnd { get; private set; } = new TimeSpan(1, 6, 0, 0);


        static public float NightHourPercentage { get; private set; } = 140.0f;

        public int? ExtraHours { get; private set; } = null;


        public KeyValuePair<int, List<HourType>> GetCalculatedPrice(int firstHourPrice)
        {
            if (EndHour.TotalHours == 40)
            {
                throw new DomainException("Zorg er aub voor dat de start- en eindtijd ingevuld zijn");
            }
            int returnPrice = Price;
            List<HourType> hourTypes = new List<HourType>() { new HourType("Eerste uur", 0, 0)};

            if (ExtraHours != null)
            {
                int extraHourPrice = (int)ExtraHours * (5 * (int)Math.Round((firstHourPrice * (NightHourPercentage / 100.0)) / 5.0));
                returnPrice += extraHourPrice;
                hourTypes.Add(new HourType("Extra uren", (int)ExtraHours, extraHourPrice));
            }

            ExtraHours = null;

            return new KeyValuePair<int, List<HourType>>(returnPrice, hourTypes);
        }
        public void SetTime(TimeSpan startHour,int? extraHours)
        {
            if (startHour.Seconds > 0 || startHour.Minutes > 0)
            {
                throw new DomainException("Zorg er a.u.b. voor dat het start uur geen minuten of seconden heeft.");
            }
            if (startHour.Hours > 24 || startHour.Hours < 20)
            {
                throw new DomainException("Het arangement wellness kan aleen tussen 7 en 12 uur in de ochtend geboekt worden.");
            }
            StartHour = startHour;

            if (extraHours < 0)
                throw new DomainException("Je kan geen negatief aantal extra uren hebben");
            ExtraHours = extraHours;

            if (extraHours != null)
            {
                EndHour = new TimeSpan(startHour.Hours + 7 + (int)extraHours, 0, 0);
                if ((EndHour.TotalHours - StartHour.TotalHours) > MaxAmountOfHours)
                {
                    throw new DomainException("Zorg er a.u.b. voor dat het eind uur niet meer dan elf uur na het start uur is. (Het NightLife arangement heeft een standaartduuratie van 4 uur)");
                }
            }
            else
            {
                EndHour = new TimeSpan(startHour.Hours + 7, 0, 0);

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
        public NightLife(int price)
        {
            Price = price;
            EndHourTicks = 1440000000000;
            StartHourTicks = 1440000000000;
            StartHour = new TimeSpan(StartHourTicks);
            EndHour = new TimeSpan(EndHourTicks);
        }
    }
}
