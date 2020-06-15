using DomainLayer.Domain.Arangements;
using DomainLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer.Domain.Vloot
{
    public class Limousine
    {
        public int FirstHourPrice { get; private set; }
        public string Name { get; private set; }
        public List<IArangement> Arangements { get; private set; }

        public Limousine(int firstHourPrice, string name, List<IArangement> arangements)
        {
            FirstHourPrice = firstHourPrice;
            Name = name;
            if (arangements.Count == 0)
            {
                throw new DomainException("Een limousine moet minstens 1 arangement hebben.");
            }
            Arangements = arangements;
        }



        public int PriceForArangement(string arangementType, int? extraHours = null, TimeSpan? startHour = null, TimeSpan? endHour = null)
        {
            int price = 0;

            switch (arangementType)
            {
                case "Wellness":
                    price = WellnessPrice();
                    break;
                case "Business":
                    {
                        if(startHour == null)
                        {
                            throw new DomainException("Het arangement Business heeft een start uur nodig.");
                        }
                        if (endHour == null)
                        {
                            throw new DomainException("Het arangement Business heeft een eind uur nodig.");
                        }
                        price = BusinessPrice((TimeSpan)startHour, (TimeSpan)endHour);
                    }
                    break;
                case "Airport":
                    {
                        if (startHour == null)
                        {
                            throw new DomainException("Het arangement Business heeft een start uur nodig.");
                        }
                        if (endHour == null)
                        {
                            throw new DomainException("Het arangement Business heeft een eind uur nodig.");
                        }
                        price = AirportPrice((TimeSpan)startHour, (TimeSpan)endHour);
                    }
                    break;
                case "Wedding":
                    price = WeddingPrice(extraHours);
                    break;
                case "NightLife":
                    price = NightLifePrice(extraHours);
                    break;
                default:
                    throw new DomainException("Het opgegeven arangement bestaat niet.");
            }

            return price;
        }

        private int AirportPrice(TimeSpan startHour, TimeSpan endHour)
        {
            if (!Arangements.Any(a => a.GetType().ToString() == typeof(Airport).ToString()))
            {
                throw new DomainException($"Limousine {Name} does not have a airport arangement.");
            }
            else
            {
                var ar = Arangements.Single(a => a.GetType().ToString() == typeof(Airport).ToString()) as Airport;
                ar.SetTime(startHour, endHour);
                return ar.GetPrice(FirstHourPrice);
            }
        }
        private int BusinessPrice(TimeSpan startHour, TimeSpan endHour)
        {
            if (!Arangements.Any(a => a.GetType().ToString() == typeof(Business).ToString()))
            {
                throw new DomainException($"Limousine {Name} does not have a business arangement.");
            }
            else
            {
                var ar = Arangements.Single(a => a.GetType().ToString() == typeof(Business).ToString()) as Business;
                ar.SetTime(startHour, endHour);
                return ar.GetPrice(FirstHourPrice);
            }
        }
        private int WellnessPrice()
        {
            if (!Arangements.Any(a => a.GetType().ToString() == typeof(Wellness).ToString()))
            {
                throw new DomainException($"Limousine {Name} does not have a wellness arangement.");
            }
            else
            {
                var ar = Arangements.Single(a => a.GetType().ToString() == typeof(Wellness).ToString()) as Wellness;
                return ar.Price;
            }
        }
        private int WeddingPrice(int? extraHours)
        {
            if (!Arangements.Any(a => a.GetType().ToString() == typeof(Wedding).ToString()))
            {
                throw new DomainException($"Limousine {Name} does not have a wedding arangement.");
            }
            else
            {
                var ar = Arangements.Single(a => a.GetType().ToString() == typeof(Wedding).ToString()) as Wedding;
                ar.SetTime(extraHours);
                return ar.GetCalculatedPrice(FirstHourPrice);
            }
        }
        private int NightLifePrice(int? extraHours)
        {
            if (!Arangements.Any(a => a.GetType().ToString() == typeof(NightLife).ToString()))
            {
                throw new DomainException($"Limousine {Name} does not have a nightlife arangement.");
            }
            else
            {
                var ar = Arangements.Single(a => a.GetType().ToString() == typeof(NightLife).ToString()) as NightLife;
                ar.SetTime(extraHours);
                return ar.GetCalculatedPrice(FirstHourPrice);
            }
        }
    }
}
