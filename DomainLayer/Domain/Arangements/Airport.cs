using DomainLayer.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Domain.Arangements
{
    public class Airport : IArangement
    {
        public TimeSpan StartHour { get; private set; } = new TimeSpan(40, 0, 0);
        public TimeSpan EndHour { get; private set; } = new TimeSpan(40, 0, 0);
        public TimeSpan NightHourBegin { get; private set; } = new TimeSpan(22, 0, 0);
        public TimeSpan NightHourEnd { get; private set; } = new TimeSpan(1, 7, 0, 0);
        public int MaxAmountOfHours { get; private set; } = 11;
        public float SecondHoursPercentage { get; private set; } = 65.0f;
        public float NightHourPercentage { get; private set; } = 140.0f;

        [JsonConstructor]
        public Airport()
        {
        }

        public int GetPrice(int FirstHourPrice)
        {
            if(StartHour.TotalHours == 40 || EndHour.TotalHours == 40)
            {
                throw new DomainException("Zorg er aub voor dat de start- en eindtijd ingevuld zijn");
            }
            int returnPrice = FirstHourPrice;

            int totalHours = (int)(EndHour.TotalHours - StartHour.TotalHours) - 1;

            if ((StartHour.TotalHours >= NightHourBegin.TotalHours) && (StartHour.TotalHours <= NightHourEnd.TotalHours) &&
                (EndHour.TotalHours >= NightHourBegin.TotalHours) && (EndHour.TotalHours <= NightHourEnd.TotalHours))
            {
                returnPrice += totalHours * (5 * (int)Math.Round(FirstHourPrice * (NightHourPercentage / 100.0) / 5.0));
            }
            else if ((StartHour.TotalHours < NightHourBegin.TotalHours)  &&
                     (EndHour.TotalHours < NightHourBegin.TotalHours))
            {
                returnPrice += totalHours * (5 * (int)Math.Round((FirstHourPrice * (SecondHoursPercentage / 100.0)) / 5.0));
            }
            else  if ((StartHour.TotalHours >= NightHourBegin.TotalHours) && (StartHour.TotalHours < NightHourEnd.TotalHours))
            {
                int nightHours = (int)(NightHourEnd.TotalHours - StartHour.TotalHours) - 1;
                returnPrice += nightHours * (5 * (int)Math.Round((FirstHourPrice * (NightHourPercentage / 100.0)) / 5.0));
                if (totalHours - nightHours > 0)
                returnPrice += (totalHours - nightHours) *(5 * (int)Math.Round(FirstHourPrice * (SecondHoursPercentage / 100.0) / 5.0));
                

            }
            else if ((EndHour.TotalHours >= NightHourBegin.TotalHours) && (EndHour.TotalHours < NightHourEnd.TotalHours))
            {
                int dayHours = (int)(NightHourBegin.TotalHours - StartHour.TotalHours) - 1;
                returnPrice += dayHours * (5 * (int)Math.Round((FirstHourPrice * (SecondHoursPercentage / 100.0)) / 5.0));
                returnPrice += (totalHours - dayHours) * (5 * (int)Math.Round((FirstHourPrice * (NightHourPercentage / 100.0)) / 5.0));

            }

            StartHour = new TimeSpan(40, 0, 0);
            EndHour = new TimeSpan(40, 0, 0);
            return returnPrice;

        }
        public void SetTime(TimeSpan startHour, TimeSpan endHour)
        {
            if(startHour.TotalHours < 0)
            {
                throw new DomainException("Zorg er a.u.b. voor dat het start uur positief.");
            }
            if (endHour < startHour)
            {
                throw new DomainException("Zorg er a.u.b. voor dat het eind uur na het start uur is.");
            }
            if (startHour.Seconds > 0 || startHour.Minutes > 0)
            {
                throw new DomainException("Zorg er a.u.b. voor dat het start uur geen minuten of seconden heeft.");
            }
            if (endHour.Seconds > 0 || endHour.Minutes > 0)
            {
                throw new DomainException("Zorg er a.u.b. voor dat het eind uur geen minuten of seconden heeft.");
            }
            if (startHour.TotalHours > 24)
            {
                throw new DomainException("Zorg er a.u.b. voor dat het start uur kleiner is dan 24 uur.");
            }
            if ((endHour.TotalHours - startHour.TotalHours) > MaxAmountOfHours)
            {
                throw new DomainException("Zorg er a.u.b. voor dat het eind uur niwet meer dan elf uur na het start uur is.");
            }
            StartHour = startHour;
            EndHour = endHour;
        }


    }
}
