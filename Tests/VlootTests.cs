using DataLayer.Func;
using DataLayer.Repositories;
using DomainLayer.Domain;
using DomainLayer.Domain.Arangements;
using DomainLayer.Domain.Clients;
using DomainLayer.Domain.Help;
using DomainLayer.Domain.Vloot;
using DomainLayer.OtherInterfaces;
using DomainLayer.Repositories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class LimousineTests
    {
        [TestMethod]
        public void LimousineTest_Constructor()
        {
            List<Arangement> arangements = new List<Arangement>
            {
                new Wedding(2000),
                new Wellness(3200),
                new Airport(),
                new Business(),
                new NightLife(2500)
            };
            int FirsHourprice = 130;
            int Available = 1;
            string Name = "Tesla Model X";


            Limousine limousine = new Limousine(130, "Tesla Model X", 1, arangements);

            limousine.FirstHourPrice.Should().Be(FirsHourprice);
            limousine.Name.Should().BeEquivalentTo(Name);
            limousine.Available.Should().Be(Available);
            limousine.Arangements.Count.Should().Be(arangements.Count);
            limousine.Arangements.Any(s => s.GetType().ToString().Equals(new Wedding(2000).GetType().ToString())).Should().BeTrue();
            limousine.Arangements.Any(s => s.GetType().ToString().Equals(new Wellness(3200).GetType().ToString())).Should().BeTrue();
            limousine.Arangements.Any(s => s.GetType().ToString().Equals(new Airport().GetType().ToString())).Should().BeTrue();
            limousine.Arangements.Any(s => s.GetType().ToString().Equals(new Business().GetType().ToString())).Should().BeTrue();
            limousine.Arangements.Any(s => s.GetType().ToString().Equals(new NightLife(2500).GetType().ToString())).Should().BeTrue();
        }
        [TestMethod]
        public void LimousineTest_Methods()
        {
            List<Arangement> arangements = new List<Arangement>
            {
                new Wedding(2000),
                new Wellness(3200),
                new Airport(),
                new Business(),
                new NightLife(2500)
            };
            Limousine limousine = new Limousine(100, "Tesla Model X", 10, arangements);
            int price = 2000;
            DateTime dateNeeded = new DateTime(2000, 2, 23, 10, 0, 0);

            limousine.PriceForArangement(dateNeeded, "Wedding",startHour: new TimeSpan(7,0,0)).Key.Should().Be(price);
            price = 3200;
            dateNeeded = new DateTime(2000, 2, 25, 10, 0, 0);

            limousine.PriceForArangement(dateNeeded, "Wellness", startHour: new TimeSpan(7,0,0)).Key.Should().Be(price);
            price = 2500;
            dateNeeded = new DateTime(2000, 2, 24, 10, 0, 0);

            limousine.PriceForArangement(dateNeeded, "NightLife", startHour: new TimeSpan(22, 0, 0)).Key.Should().Be(price);
            TimeSpan startHour = new TimeSpan(10, 0, 0);
            TimeSpan endHour = new TimeSpan(15, 0, 0);
            price = 360;
            dateNeeded = new DateTime(2000, 2, 27, 10, 0, 0);

            limousine.PriceForArangement(dateNeeded, "Airport", startHour: startHour, endHour: endHour).Key.Should().Be(price);
            dateNeeded = new DateTime(2000, 2, 29, 10, 0, 0);

            limousine.PriceForArangement(dateNeeded, "Business", startHour: startHour, endHour: endHour).Key.Should().Be(price);

        }
        [TestMethod]
        public void LimousineTest_Exceptions()
        {

            List<Arangement> arangements = new List<Arangement>();
            Action act = () => new Limousine(100, "Tesla Model X", 0, arangements);
            act.Should().Throw<DomainException>().WithMessage("Een limousine moet minstens 1 keer beschikbaar zijn.");

            act = () => new Limousine(100, "Tesla Model X", 1, arangements);
            act.Should().Throw<DomainException>().WithMessage("Een limousine moet minstens 1 arangement hebben.");

            arangements.Add(new Airport());
            arangements.Add(new Airport());
            act = () => new Limousine(100, "Tesla Model X", 1, arangements);
            act.Should().Throw<DomainException>().WithMessage("Een limousine kan geen arangement 2 keer hebben.");

            DateTime dateNeeded = new DateTime(2000, 2, 27, 10, 0, 0);
            arangements = new List<Arangement>
            {
                new Wedding(2000)
            };
            Limousine limousine = new Limousine(100, "Tesla Model X", 1, arangements);
            act = () => limousine.PriceForArangement(dateNeeded, "Wedding");
            act.Should().Throw<DomainException>().WithMessage($"Het arangement Wedding heeft een start uur nodig.");

            limousine.PriceForArangement(dateNeeded, "Wedding", startHour: new TimeSpan(7, 0, 0));


            act = () => limousine.PriceForArangement(dateNeeded, "Wedding",startHour: new TimeSpan(7,0,0));
            act.Should().Throw<DomainException>().WithMessage($"Limousine {limousine.Name} is niet vrij.");

            dateNeeded = new DateTime(2000, 2, 28, 10, 0, 0);
            act = () => limousine.PriceForArangement(dateNeeded, "Wellness", startHour: new TimeSpan(7,0,0));
            act.Should().Throw<DomainException>().WithMessage($"Limousine {limousine.Name} does not have a wellness arangement.");

            arangements = new List<Arangement>
            {
                new Wellness(3200)
            };
            limousine = new Limousine(100, "Tesla Model X", 10, arangements);
            act = () => limousine.PriceForArangement(dateNeeded, "Wedding", startHour: new TimeSpan(7, 0, 0));
            act.Should().Throw<DomainException>().WithMessage($"Limousine {limousine.Name} does not have a wedding arangement.");
            arangements.Add(new Wedding(2000));


            act = () => limousine.PriceForArangement(dateNeeded, "NightLife");
            act.Should().Throw<DomainException>().WithMessage($"Het arangement NightLife heeft een start uur nodig.");


            act = () => limousine.PriceForArangement(dateNeeded, "NightLife", startHour: new TimeSpan(20,0,0));
            act.Should().Throw<DomainException>().WithMessage($"Limousine {limousine.Name} does not have a nightlife arangement.");
            arangements.Add(new NightLife(2500));


            act = () => limousine.PriceForArangement(dateNeeded, "Airport");
            act.Should().Throw<DomainException>().WithMessage($"Het arangement Airport heeft een start uur nodig.");

            act = () => limousine.PriceForArangement(dateNeeded, "Airport", startHour: new TimeSpan(10, 0, 0));
            act.Should().Throw<DomainException>().WithMessage($"Het arangement Airport heeft een eind uur nodig.");

            act = () => limousine.PriceForArangement(dateNeeded, "Airport", startHour: new TimeSpan(10, 0, 0), endHour: new TimeSpan(18, 0, 0));
            act.Should().Throw<DomainException>().WithMessage($"Limousine {limousine.Name} does not have a airport arangement.");
            arangements.Add(new Airport());

            act = () => limousine.PriceForArangement(dateNeeded, "Business");
            act.Should().Throw<DomainException>().WithMessage($"Het arangement Business heeft een start uur nodig.");

            act = () => limousine.PriceForArangement(dateNeeded, "Business", startHour: new TimeSpan(10, 0, 0));
            act.Should().Throw<DomainException>().WithMessage($"Het arangement Business heeft een eind uur nodig.");

            act = () => limousine.PriceForArangement(dateNeeded, "Business", startHour: new TimeSpan(10, 0, 0), endHour: new TimeSpan(18, 0, 0));
            act.Should().Throw<DomainException>().WithMessage($"Limousine {limousine.Name} does not have a business arangement.");
            arangements.Add(new Business());
        }
    }
}
