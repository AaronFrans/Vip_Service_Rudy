using DomainLayer.Domain.Arangements;
using DomainLayer.Domain.Vloot;
using DomainLayer.Repositories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests
{
    [TestClass]
    public class VlootTests
    {
        [TestMethod]
        public void LimousineTest_Constructor()
        {
            List<IArangement> arangements = new List<IArangement>();
            arangements.Add(new Wedding(2000));
            arangements.Add(new Wellness(3200));
            arangements.Add(new Airport());
            arangements.Add(new Business());
            arangements.Add(new NightLife(2500));
            int FirsHourprice = 130;
            string Name = "Tesla Model X";


            Limousine limousine = new Limousine(130, "Tesla Model X", arangements);

            limousine.FirstHourPrice.Should().Be(FirsHourprice);
            limousine.Name.Should().BeEquivalentTo(Name);
            limousine.Arangements.Count.Should().Be(arangements.Count);
            limousine.Arangements.Any(s => s.GetType().ToString().Equals(new Wedding(2000).GetType().ToString())).Should().Be(true);
            limousine.Arangements.Any(s => s.GetType().ToString().Equals(new Wellness(3200).GetType().ToString())).Should().Be(true);
            limousine.Arangements.Any(s => s.GetType().ToString().Equals(new Airport().GetType().ToString())).Should().Be(true);
            limousine.Arangements.Any(s => s.GetType().ToString().Equals(new Business().GetType().ToString())).Should().Be(true);
            limousine.Arangements.Any(s => s.GetType().ToString().Equals(new NightLife(2500).GetType().ToString())).Should().Be(true);
        }

        [TestMethod]
        public void LimousineTest_Methods()
        {
            List<IArangement> arangements = new List<IArangement>();
            arangements.Add(new Wedding(2000));
            arangements.Add(new Wellness(3200));
            arangements.Add(new Airport());
            arangements.Add(new Business());
            arangements.Add(new NightLife(2500));
            Limousine limousine = new Limousine(100, "Tesla Model X", arangements);
            int price = 2000;

            limousine.PriceForArangement("Wedding").Should().Be(price);
            price = 3200;

            limousine.PriceForArangement("Wellness").Should().Be(price);
            price = 2500;

            limousine.PriceForArangement("NightLife").Should().Be(price);
            TimeSpan startHour = new TimeSpan(10, 0, 0);
            TimeSpan endHour = new TimeSpan(15, 0, 0);
            price = 360;

            limousine.PriceForArangement("Airport", startHour: startHour, endHour: endHour).Should().Be(price);
            limousine.PriceForArangement("Business", startHour: startHour, endHour: endHour).Should().Be(price);

        }
    }
}
