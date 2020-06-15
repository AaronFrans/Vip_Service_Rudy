using DomainLayer.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Domain.Arangements
{
    public class NightLife: IArangement
    {
        public int Price { get; private set; }
        public TimeSpan StartHour { get; private set; } = new TimeSpan(20, 0, 0);
        public TimeSpan EndHour { get; private set; } = new TimeSpan(40, 0, 0);
        public TimeSpan NightHourBegin { get; private set; } = new TimeSpan(22, 0, 0);
        public TimeSpan NightHourEnd { get; private set; } = new TimeSpan(1, 7, 0, 0);
        public int MaxAmountOfHours { get; private set; } = 11;
        public float NightHourPercentage { get; private set; } = 140.0f;
        public int? ExtraHours { get; private set; } = null;


        public int GetCalculatedPrice(int firstHourPrice)
        {
            if (EndHour.TotalHours == 40)
            {
                throw new DomainException("Zorg er aub voor dat de start- en eindtijd ingevuld zijn");
            }
            int returnPrice = Price;

            if (ExtraHours != null)
            {
                returnPrice += (int)ExtraHours * (5 * (int)Math.Round((firstHourPrice * (NightHourPercentage / 100.0)) / 5.0));
            }

            ExtraHours = null;
            EndHour = new TimeSpan(40, 0, 0);

            return returnPrice;
        }
        public void SetTime(int? extraHours)
        {

            if (extraHours < 0)
                throw new DomainException("Je kan geen negatief aantal extra uren hebben");
            ExtraHours = extraHours;

            if (extraHours != null)
            {
                EndHour = new TimeSpan(24 + (int)extraHours, 0, 0);
                if ((EndHour.TotalHours - StartHour.TotalHours) > MaxAmountOfHours)
                {
                    throw new DomainException("Zorg er a.u.b. voor dat het eind uur niet meer dan elf uur na het start uur is. (Het NightLife arangement heeft een standaartduuratie van 4 uur)");
                }
            }
            else
            {
                EndHour = new TimeSpan(24, 0, 0);

            }
        }

        [JsonConstructor]
        public NightLife(int price)
        {
            Price = price;
        }
    }
}
