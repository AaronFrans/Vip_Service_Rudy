using DomainLayer.Domain.Arangements;
using DomainLayer.Domain.Help;
using DomainLayer.OtherInterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DomainLayer.Domain.Vloot
{
    public class Limousine
    {
        public int FirstHourPrice { get; private set; }
        [Key]
        public string Name { get; private set; }
        public int Available { get; private set; }
        public List<Arangement> Arangements { get; private set; }
        public List<HireDate> HireDates { get; private set; }

        public Limousine()
        {

        }
        [JsonConstructor]
        public Limousine(int firstHourPrice, string name, int available, List<Arangement> arangements)
        {
            FirstHourPrice = firstHourPrice;
            Name = name;
            if (available < 1)
            {
                throw new DomainException("Een limousine moet minstens 1 keer beschikbaar zijn.");
            }
            Available = available;
            if (arangements.Count == 0)
            {
                throw new DomainException("Een limousine moet minstens 1 arangement hebben.");
            }
            if (CheckForDoubles(arangements))
            {
                throw new DomainException("Een limousine kan geen arangement 2 keer hebben.");
            }
            Arangements = arangements;
            HireDates = new List<HireDate>();

        }

        private bool CheckForDoubles(List<Arangement> arangements)
        {

            var check = arangements.GroupBy(a => a.GetType()).OrderByDescending(g => g.Count()).First();
            if (check.Count() > 1)
                return true;
            return false;
        }

        public KeyValuePair<int, List<HourType>> PriceForArangement(DateTime hireDate, string arangementType, int? extraHours = null, TimeSpan? startHour = null, TimeSpan? endHour = null)
        {
            KeyValuePair<int, List<HourType>> toReturn = new KeyValuePair<int, List<HourType>>();



            switch (arangementType)
            {
                case "Wellness":
                    {
                        if (startHour == null)
                        {
                            throw new DomainException("Het arangement Wellness heeft een start uur nodig.");
                        }
                        toReturn = WellnessPrice((TimeSpan)startHour, hireDate);
                    }
                    break;
                case "Business":
                    {
                        if (startHour == null)
                        {
                            throw new DomainException("Het arangement Business heeft een start uur nodig.");
                        }
                        if (endHour == null)
                        {
                            throw new DomainException("Het arangement Business heeft een eind uur nodig.");
                        }
                        toReturn = BusinessPrice((TimeSpan)startHour, (TimeSpan)endHour, hireDate);
                    }
                    break;
                case "Airport":
                    {
                        if (startHour == null)
                        {
                            throw new DomainException("Het arangement Airport heeft een start uur nodig.");
                        }
                        if (endHour == null)
                        {
                            throw new DomainException("Het arangement Airport heeft een eind uur nodig.");
                        }
                        toReturn = AirportPrice((TimeSpan)startHour, (TimeSpan)endHour, hireDate);
                    }
                    break;
                case "Wedding":
                    {
                        if (startHour == null)
                        {
                            throw new DomainException("Het arangement Wedding heeft een start uur nodig.");
                        }
                        toReturn = WeddingPrice((TimeSpan)startHour, extraHours, hireDate);
                    }
                    break;
                case "NightLife":
                    {
                        if (startHour == null)
                        {
                            throw new DomainException("Het arangement NightLife heeft een start uur nodig.");
                        }
                        toReturn = NightLifePrice((TimeSpan)startHour, extraHours, hireDate);
                    }
                    break;
                default:
                    throw new DomainException("Het opgegeven arangement bestaat niet.");
            }


            return toReturn;
        }

        private void AddHireDate(DateTime hireDate)
        {
            HireDates.Add(new HireDate( hireDate));
        }
        private bool IsVehicleAvailable(DateTime hireDate)
        {
            if (HireDates.Count == Available)
            {
                if (HireDates.Any(d => d.Date.AddHours(6) <= hireDate))
                {
                    HireDates = HireDates.Where(d => d.Date.AddHours(6) >= hireDate).ToList();
                    return true;
                }
                else
                    return false;
            }
            else
                return true;
        }


        private KeyValuePair<int, List<HourType>> AirportPrice(TimeSpan startHour, TimeSpan endHour, DateTime hireDate)
        {
            if (!Arangements.Any(a => a.GetType().ToString() == typeof(Airport).ToString()))
            {
                throw new DomainException($"Limousine {Name} does not have a airport arangement.");
            }
            else
            {
                var ar = Arangements.Single(a => a.GetType().ToString() == typeof(Airport).ToString()) as Airport;
                ar.SetTime(startHour, endHour);
                var toReturn = ar.GetPrice(FirstHourPrice);
                TimeSpan end = ar.GetEndTime();
                DateTime toSetDate = new DateTime(hireDate.Year, hireDate.Month, hireDate.Day, end.Hours, end.Minutes, end.Seconds);
                if (IsVehicleAvailable(toSetDate))
                {
                    AddHireDate(hireDate);
                }
                else
                    throw new DomainException($"Limousine {Name} is niet vrij.");
                return toReturn;
            }
        }
        private KeyValuePair<int, List<HourType>> BusinessPrice(TimeSpan startHour, TimeSpan endHour, DateTime hireDate)
        {
            if (!Arangements.Any(a => a.GetType().ToString() == typeof(Business).ToString()))
            {
                throw new DomainException($"Limousine {Name} does not have a business arangement.");
            }
            else
            {
                var ar = Arangements.Single(a => a.GetType().ToString() == typeof(Business).ToString()) as Business;
                ar.SetTime(startHour, endHour);
                var toReturn = ar.GetPrice(FirstHourPrice);
                TimeSpan end = ar.GetEndTime();
                DateTime toSetDate = new DateTime(hireDate.Year, hireDate.Month, hireDate.Day, end.Hours, end.Minutes, end.Seconds);
                if (IsVehicleAvailable(toSetDate))
                {
                    AddHireDate(hireDate);
                }
                else
                    throw new DomainException($"Limousine {Name} is niet vrij.");
                return toReturn;
            }
        }
        private KeyValuePair<int, List<HourType>> WellnessPrice(TimeSpan startHour, DateTime hireDate)
        {
            if (!Arangements.Any(a => a.GetType().ToString() == typeof(Wellness).ToString()))
            {
                throw new DomainException($"Limousine {Name} does not have a wellness arangement.");
            }
            else
            {
                var ar = Arangements.Single(a => a.GetType().ToString() == typeof(Wellness).ToString()) as Wellness;
                ar.SetTime(startHour);
                TimeSpan end = ar.GetEndTime();
                DateTime toSetDate = new DateTime(hireDate.Year, hireDate.Month, hireDate.Day, end.Hours, end.Minutes, end.Seconds);
                if (IsVehicleAvailable(toSetDate))
                {
                    AddHireDate(hireDate);
                }
                else
                    throw new DomainException($"Limousine {Name} is niet vrij.");
                return new KeyValuePair<int, List<HourType>>(ar.Price, new List<HourType>());
            }
        }
        private KeyValuePair<int, List<HourType>> WeddingPrice(TimeSpan startHour, int? extraHours, DateTime hireDate)
        {
            if (!Arangements.Any(a => a.GetType().ToString() == typeof(Wedding).ToString()))
            {
                throw new DomainException($"Limousine {Name} does not have a wedding arangement.");
            }
            else
            {
                var ar = Arangements.Single(a => a.GetType().ToString() == typeof(Wedding).ToString()) as Wedding;
                ar.SetTime(startHour, extraHours);
                var toReturn = ar.GetCalculatedPrice(FirstHourPrice);
                TimeSpan end = ar.GetEndTime();
                DateTime toSetDate = new DateTime(hireDate.Year, hireDate.Month, hireDate.Day, end.Hours, end.Minutes, end.Seconds);
                if (IsVehicleAvailable(toSetDate))
                {
                    AddHireDate(hireDate);
                }
                else
                    throw new DomainException($"Limousine {Name} is niet vrij.");
                return toReturn;
            }
        }
        private KeyValuePair<int, List<HourType>> NightLifePrice(TimeSpan startHour, int? extraHours, DateTime hireDate)
        {
            if (!Arangements.Any(a => a.GetType().ToString() == typeof(NightLife).ToString()))
            {
                throw new DomainException($"Limousine {Name} does not have a nightlife arangement.");
            }
            else
            {
                var ar = Arangements.Single(a => a.GetType().ToString() == typeof(NightLife).ToString()) as NightLife;
                ar.SetTime(startHour, extraHours);
                var toReturn = ar.GetCalculatedPrice(FirstHourPrice);
                TimeSpan end = ar.GetEndTime();
                DateTime toSetDate = new DateTime(hireDate.Year, hireDate.Month, hireDate.Day, end.Hours, end.Minutes, end.Seconds);
                if (IsVehicleAvailable(toSetDate))
                {
                    AddHireDate(hireDate);
                }
                else
                    throw new DomainException($"Limousine {Name} is niet vrij.");
                return toReturn;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Limousine limousine &&
                   FirstHourPrice == limousine.FirstHourPrice &&
                   Name == limousine.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstHourPrice, Name);
        }
    }
}