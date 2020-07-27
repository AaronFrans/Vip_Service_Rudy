using DomainLayer.Domain.Help;
using DomainLayer.OtherInterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Domain.Arangements
{
    public class Airport: Arangement
    {

        public static  TimeSpan NightHourBegin { get; private set; } = new TimeSpan(22, 0, 0);
        public static TimeSpan NightHourEnd { get; private set; } = new TimeSpan(1, 6, 0, 0);

        public static float SecondHoursPercentage { get; private set; } = 65.0f;
        public static float NightHourPercentage { get; private set; } = 140.0f;

        [JsonConstructor]
        public Airport()
        {
            EndHourTicks = 1440000000000;
            StartHourTicks = 1440000000000;
            StartHour = new TimeSpan(StartHourTicks);
            EndHour = new TimeSpan(EndHourTicks);
        }
        public KeyValuePair<int, List<HourType>> GetPrice(int FirstHourPrice)
        {
            if (StartHour.TotalHours == 40 || EndHour.TotalHours == 40)
            {
                throw new DomainException("Zorg er aub voor dat de start- en eindtijd ingevuld zijn");
            }
            int returnPrice = FirstHourPrice;

            List<HourType> hourTypes = new List<HourType>() { new HourType("Eerste uur", 1, FirstHourPrice) };

            int totalHours = (int)(EndHour.TotalHours - StartHour.TotalHours) - 1;

            if ((StartHour.TotalHours >= NightHourBegin.TotalHours) && (StartHour.TotalHours <= NightHourEnd.TotalHours) &&
                (EndHour.TotalHours >= NightHourBegin.TotalHours) && (EndHour.TotalHours <= NightHourEnd.TotalHours))
            {
                int nightPrice = (5 * (int)Math.Round((totalHours * FirstHourPrice * (NightHourPercentage / 100.0)) / 5.0));
                returnPrice += nightPrice;
                hourTypes.Add(new HourType("Nacht uren", totalHours, nightPrice));
                hourTypes.Add(new HourType("Dag uren", 0, 0));
            }
            else if ((StartHour.TotalHours < NightHourBegin.TotalHours) &&
                     (EndHour.TotalHours < NightHourBegin.TotalHours))
            {
                int dayPrice = (5 * (int)Math.Round((totalHours * (FirstHourPrice * (SecondHoursPercentage / 100.0))) / 5.0)); ;
                returnPrice += dayPrice;
                hourTypes.Add(new HourType("Nacht uren", 0, 0));
                hourTypes.Add(new HourType("Dag uren", totalHours, dayPrice));
            }
            else if ((StartHour.TotalHours >= NightHourBegin.TotalHours) && (StartHour.TotalHours <= NightHourEnd.TotalHours))
            {
                int nightHours = (int)(NightHourEnd.TotalHours - StartHour.TotalHours);
                int nightPrice = (5 * (int)Math.Round((nightHours * (FirstHourPrice * (NightHourPercentage / 100.0))) / 5.0));
                returnPrice += nightPrice;
                if (totalHours - nightHours > 0)
                {
                    int dayHours = (totalHours - nightHours);
                    int dayPrice = (5 * (int)Math.Round((dayHours * (FirstHourPrice * (SecondHoursPercentage / 100.0))) / 5.0));
                    returnPrice += dayPrice;
                    hourTypes.Add(new HourType("Nacht uren", nightHours, nightPrice));
                    hourTypes.Add(new HourType("Dag uren", dayHours, dayPrice));
                }
                else
                {
                    hourTypes.Add(new HourType("Nacht uren", nightHours, nightPrice));
                    hourTypes.Add(new HourType("Dag uren", 0, 0));
                }
            }
            else if ((EndHour.TotalHours >= NightHourBegin.TotalHours) && (EndHour.TotalHours <= NightHourEnd.TotalHours))
            {
                int dayHours = (int)(NightHourBegin.TotalHours - StartHour.TotalHours);
                int dayPrice = (5 * (int)Math.Round((dayHours * (FirstHourPrice * (SecondHoursPercentage / 100.0))) / 5.0));
                returnPrice += dayPrice;

                int nightHours = (totalHours - dayHours);
                int nightPrice = (5 * (int)Math.Round((nightHours * (FirstHourPrice * (NightHourPercentage / 100.0))) / 5.0));
                returnPrice += nightPrice;
                hourTypes.Add(new HourType("Nacht uren", nightHours, nightPrice));
                hourTypes.Add(new HourType("Dag uren", dayHours, dayPrice));
            }
            else if ((StartHour.TotalHours < NightHourBegin.TotalHours) && (EndHour.TotalHours > NightHourBegin.TotalHours))
            {
                int dayHours = ((int)NightHourBegin.TotalHours - (int)StartHour.TotalHours) - 1;
                dayHours += (int)EndHour.TotalHours - (int)NightHourEnd.TotalHours;
                int dayPrice = (5 * (int)Math.Round((dayHours * (FirstHourPrice * (SecondHoursPercentage / 100.0))) / 5.0));
                returnPrice += dayPrice;

                int nightHours = totalHours - dayHours;
                int nightPrice = (5 * (int)Math.Round((nightHours * (FirstHourPrice * (NightHourPercentage / 100.0))) / 5.0));
                returnPrice += nightPrice;

                hourTypes.Add(new HourType("Nacht uren", nightHours, nightPrice));
                hourTypes.Add(new HourType("Dag uren", dayHours, dayPrice));
            }

            StartHour = new TimeSpan(40, 0, 0);

            return new KeyValuePair<int, List<HourType>>(returnPrice, hourTypes);

        }
        public void SetTime(TimeSpan startHour, TimeSpan endHour)
        {
            if (startHour.TotalHours < 0)
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


        public TimeSpan GetEndTime()
        {
            TimeSpan toReturn = EndHour;
            EndHour = new TimeSpan(40, 0, 0);
            return toReturn;
        }


    }
}
