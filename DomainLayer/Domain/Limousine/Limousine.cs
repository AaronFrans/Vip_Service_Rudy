using DomainLayer.Domain.Arangements;
using DomainLayer.Domain.Help;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DomainLayer.Domain.Vloot
{
    /// <summary>
    /// Represents a limousine that can be hired.
    /// </summary>
    public class Limousine
    {
        /// <summary>
        /// The price the first hour costs.
        /// </summary>
        public int FirstHourPrice { get; private set; }
        /// <summary>
        /// The name of the limousine.
        /// </summary>
        [Key]
        public string Name { get; private set; }
        /// <summary>
        /// The total amount of times this type of limousine is available.
        /// </summary>
        public int Available { get; private set; }
        /// <summary>
        /// The available arangemnets for this type of limousine.
        /// </summary>
        public List<Arangement> Arangements { get; private set; }
        /// <summary>
        /// The past reservations for this type of limousine.
        /// </summary>
        public List<HireDate> HireDates { get; private set; }

        /// <summary>
        /// An empty constructor.
        /// </summary>
        public Limousine()
        {

        }
        /// <summary>
        /// A constructor used to make a ?Limousine object.
        /// </summary>
        /// <param name="firstHourPrice">The price the first hour costs.</param>
        /// <param name="name">The name of the limousine.</param>
        /// <param name="available">The total amount of times this type of limousine is available.</param>
        /// <param name="arangements">The available arangemnets for this type of limousine.</param>
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

        /// <summary>
        /// Checks if an arangement shows up more than once.
        /// </summary>
        /// <param name="arangements">The arangements to check.</param>
        /// <returns>A boolean representing if an arangements shows up twice. If true there are multiple.</returns>
        private bool CheckForDoubles(List<Arangement> arangements)
        {

            var check = arangements.GroupBy(a => a.GetType()).OrderByDescending(g => g.Count()).First();
            if (check.Count() > 1)
                return true;
            return false;
        }
        /// <summary>
        /// Checks if an arangements is available for this limousine.
        /// </summary>
        /// <param name="arangementName">Arangement to check.</param>
        /// <returns>>A boolean representing if an arangements is available. If true it is available.</returns>
        public bool HasArangement(string arangementName)
        {
            switch (arangementName)
            {
                case "Wellness":
                    {
                        if (Arangements.Any(a => a.GetType().ToString() == typeof(Wellness).ToString()))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case "Business":
                    {
                        if (Arangements.Any(a => a.GetType().ToString() == typeof(Business).ToString()))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case "Airport":
                    {
                        if (Arangements.Any(a => a.GetType().ToString() == typeof(Airport).ToString()))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case "Wedding":
                    {
                        if (Arangements.Any(a => a.GetType().ToString() == typeof(Wedding).ToString()))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case "Nightlife":
                    {
                        if (Arangements.Any(a => a.GetType().ToString() == typeof(Nightlife).ToString()))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                default:
                    return false;
            }


        }
        /// <summary>
        /// Gets the price for a given arangement.
        /// </summary>
        /// <param name="hireDate">Date the limousine will be hired.</param>
        /// <param name="arangementType">Type of arangement to use.</param>
        /// <param name="extraHours">Amount of extra hours (if applicable).</param>
        /// <param name="startHour">When the arangement starts (if applicable).</param>
        /// <param name="endHour">When the arangement ends (if applicable).</param>
        /// <returns>A KeyValuePair where the key is the total price and the value is a list of HourTypes objects.</returns>
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
                case "Nightlife":
                    {
                        if (startHour == null)
                        {
                            throw new DomainException("Het arangement Nightlife heeft een start uur nodig.");
                        }
                        toReturn = NightlifePrice((TimeSpan)startHour, extraHours, hireDate);
                    }
                    break;
                default:
                    throw new DomainException("Het opgegeven arangement bestaat niet.");
            }

            return toReturn;
        }

        /// <summary>
        /// Add a HireDate to the HireDates property.
        /// </summary>
        /// <param name="hireDate">Date the limousine will be hired.</param>
        private void AddHireDate(DateTime hireDate)
        {
            HireDates.Add(new HireDate(hireDate));
        }
        /// <summary>
        /// Checks if a limousine is available for a givcen date.
        /// </summary>
        /// <param name="hireDate">Date the limousine will be hired.</param>
        /// <returns>A boolean that represents if the limousine is availble. If true it is available.</returns>
        public bool IsVehicleAvailable(DateTime hireDate)
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


        /// <summary>
        /// Calculates price for the airport arangement.
        /// </summary>
        /// <param name="startHour">When the arangemnt starts</param>
        /// <param name="endHour">When the arangemnt ends</param>
        /// <param name="hireDate">When the limousine is to be hired.</param>
        /// <returns>A KeyValuePair where the key is the total price and the value is a list of HourTypes objects.</returns>
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
        /// <summary>
        /// Calculates price for the business arangement.
        /// </summary>
        /// <param name="startHour">When the arangemnt starts</param>
        /// <param name="endHour">When the arangemnt ends</param>
        /// <param name="hireDate">When the limousine is to be hired.</param>
        /// <returns>A KeyValuePair where the key is the total price and the value is a list of HourTypes objects.</returns>
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
        /// <summary>
        /// Calculates price for the wellness arangement.
        /// </summary>
        /// <param name="startHour">When the arangemnt starts</param>
        /// <param name="hireDate">When the limousine is to be hired.</param>
        /// <returns>A KeyValuePair where the key is the total price and the value is a list of HourTypes objects.</returns>
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
        /// <summary>
        /// Calculates price for the wedding arangement.
        /// </summary>
        /// <param name="startHour">When the arangemnt starts</param>
        /// <param name="extraHours">Extra hours to add to the duration of the arangement.</param>
        /// <param name="hireDate">When the limousine is to be hired.</param>
        /// <returns>A KeyValuePair where the key is the total price and the value is a list of HourTypes objects.</returns>
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
        /// <summary>
        /// Calculates price for the nightlife arangement.
        /// </summary>
        /// <param name="startHour">When the arangemnt starts</param>
        /// <param name="extraHours">Extra hours to add to the duration of the arangement.</param>
        /// <param name="hireDate">When the limousine is to be hired.</param>
        /// <returns>A KeyValuePair where the key is the total price and the value is a list of HourTypes objects.</returns>
        private KeyValuePair<int, List<HourType>> NightlifePrice(TimeSpan startHour, int? extraHours, DateTime hireDate)
        {
            if (!Arangements.Any(a => a.GetType().ToString() == typeof(Nightlife).ToString()))
            {
                throw new DomainException($"Limousine {Name} does not have a nightlife arangement.");
            }
            else
            {
                var ar = Arangements.Single(a => a.GetType().ToString() == typeof(Nightlife).ToString()) as Nightlife;
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