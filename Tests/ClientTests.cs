using DataLayer.Func;
using DomainLayer.Domain.Clients;
using DomainLayer.Domain.Help;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class ClientTests
    {
        [TestMethod]
        public void DiscountTest()
        {
            JsonParser.GetDiscounts().Should().NotBeEmpty();
            JsonParser.GetDiscounts().Count.Should().Be(26);
        }
        [TestMethod]
        public void TestVip()
        {
            List<ClientDiscount> discounts = JsonParser.GetDiscounts().Where(s => s.ClientType.Equals("Vip")).ToList();
            Vip test = new Vip("Vip", 0862333424, discounts);
            float excpectedFirstDiscount = 0.00f;
            float excpectedSecondDiscount = 0.00f;
            float excpectedThirdDiscount = 5.00f;
            int expectedNrOfReservations = 3;
            int expectedBtwNumber = 0862333424;
            string excpectedName = "Vip";

            float firstDiscount = test.GetDiscount();
            test.AddReservation();
            test.AddReservation();
            float secondDiscount = test.GetDiscount();
            test.AddReservation();
            float thirdDiscount = test.GetDiscount();

            firstDiscount.Should().Be(excpectedFirstDiscount);
            secondDiscount.Should().Be(excpectedSecondDiscount);
            thirdDiscount.Should().Be(excpectedThirdDiscount);
            test.NrOfReservations.Should().Be(expectedNrOfReservations);
            test.BtwNumber.Should().Be(expectedBtwNumber);
            test.Name.Should().BeEquivalentTo(excpectedName);
        }
        [TestMethod]
        public void TestEvenementenBureau()
        {
            List<ClientDiscount> discounts = JsonParser.GetDiscounts().Where(s => s.ClientType.Equals("EvenementenBureau")).ToList();
            EvenementenBureau test = new EvenementenBureau("EvenementenBureau", 0862333424, discounts);
            float excpectedFirstDiscount = 0.00f;
            float excpectedSecondDiscount = 0.00f;
            float excpectedThirdDiscount = 1.00f;
            int expectedNrOfReservations = 6;
            int expectedBtwNumber = 0862333424;
            string excpectedName = "EvenementenBureau";

            float firstDiscount = test.GetDiscount();
            test.AddReservation();
            test.AddReservation();
            float secondDiscount = test.GetDiscount();
            test.AddReservation();
            test.AddReservation();
            test.AddReservation();
            test.AddReservation();
            float thirdDiscount = test.GetDiscount();

            firstDiscount.Should().Be(excpectedFirstDiscount);
            secondDiscount.Should().Be(excpectedSecondDiscount);
            thirdDiscount.Should().Be(excpectedThirdDiscount);
            test.NrOfReservations.Should().Be(expectedNrOfReservations);
            test.BtwNumber.Should().Be(expectedBtwNumber);
            test.Name.Should().BeEquivalentTo(excpectedName);
        }
        [TestMethod]
        public void TestConcertPromotor()
        {
            List<ClientDiscount> discounts = JsonParser.GetDiscounts().Where(s => s.ClientType.Equals("ConcertPromotor")).ToList();
            ConcertPromotor test = new ConcertPromotor("ConcertPromotor", 0862333424, discounts);
            float excpectedFirstDiscount = 0.00f;
            float excpectedSecondDiscount = 0.00f;
            float excpectedThirdDiscount = 10.00f;
            int expectedNrOfReservations = 4;
            int expectedBtwNumber = 0862333424;
            string excpectedName = "ConcertPromotor";

            float firstDiscount = test.GetDiscount();
            test.AddReservation();
            test.AddReservation();
            float secondDiscount = test.GetDiscount();
            test.AddReservation();
            test.AddReservation();
            float thirdDiscount = test.GetDiscount();

            firstDiscount.Should().Be(excpectedFirstDiscount);
            secondDiscount.Should().Be(excpectedSecondDiscount);
            thirdDiscount.Should().Be(excpectedThirdDiscount);
            test.NrOfReservations.Should().Be(expectedNrOfReservations);
            test.BtwNumber.Should().Be(expectedBtwNumber);
            test.Name.Should().BeEquivalentTo(excpectedName);
        }
        [TestMethod]
        public void TestHuwelijksPlanner()
        {
            List<ClientDiscount> discounts = JsonParser.GetDiscounts().Where(s => s.ClientType.Equals("HuwelijksPlanner")).ToList();
            HuwelijksPlanner test = new HuwelijksPlanner("HuwelijksPlanner", 0862333424, discounts);
            float excpectedFirstDiscount = 0.00f;
            float excpectedSecondDiscount = 0.00f;
            float excpectedThirdDiscount = 7.50f;
            int expectedNrOfReservations = 6;
            int expectedBtwNumber = 0862333424;
            string excpectedName = "HuwelijksPlanner";

            float firstDiscount = test.GetDiscount();
            test.AddReservation();
            test.AddReservation();
            test.AddReservation();
            test.AddReservation();
            float secondDiscount = test.GetDiscount();
            test.AddReservation();
            test.AddReservation();
            float thirdDiscount = test.GetDiscount();

            firstDiscount.Should().Be(excpectedFirstDiscount);
            secondDiscount.Should().Be(excpectedSecondDiscount);
            thirdDiscount.Should().Be(excpectedThirdDiscount);
            test.NrOfReservations.Should().Be(expectedNrOfReservations);
            test.BtwNumber.Should().Be(expectedBtwNumber);
            test.Name.Should().BeEquivalentTo(excpectedName);
        }
        [TestMethod]
        public void TestOrganisatie()
        {

            List<ClientDiscount> discounts = JsonParser.GetDiscounts().Where(s => s.ClientType.Equals("Organisatie")).ToList();
            Organisatie test = new Organisatie("Organisatie", 0862333424, discounts);
            float excpectedFirstDiscount = 0.00f;
            float excpectedSecondDiscount = 0.00f;
            float excpectedThirdDiscount = 7.50f;
            int expectedNrOfReservations = 11;
            int expectedBtwNumber = 0862333424;
            string excpectedName = "Organisatie";

            float firstDiscount = test.GetDiscount();
            test.AddReservation();
            test.AddReservation();
            float secondDiscount = test.GetDiscount();
            test.AddReservation();
            test.AddReservation();
            test.AddReservation();
            test.AddReservation();
            test.AddReservation();
            test.AddReservation();
            test.AddReservation();
            test.AddReservation();
            test.AddReservation();
            float thirdDiscount = test.GetDiscount();

            firstDiscount.Should().Be(excpectedFirstDiscount);
            secondDiscount.Should().Be(excpectedSecondDiscount);
            thirdDiscount.Should().Be(excpectedThirdDiscount);
            test.NrOfReservations.Should().Be(expectedNrOfReservations);
            test.BtwNumber.Should().Be(expectedBtwNumber);
            test.Name.Should().BeEquivalentTo(excpectedName);
        }
        [TestMethod]
        public void TestParticulier()
        {
            List<ClientDiscount> discounts = JsonParser.GetDiscounts().Where(s => s.ClientType.Equals("Particulier")).ToList();
            Particulier test = new Particulier("Particulier", 0862333424, discounts);
            float excpectedFirstDiscount = 0.00f;
            float excpectedSecondDiscount = 0.00f;
            float excpectedThirdDiscount = 2.50f;
            int expectedNrOfReservations = 4;
            int expectedBtwNumber = 0862333424;
            string excpectedName = "Particulier";

            float firstDiscount = test.GetDiscount();
            test.AddReservation();
            test.AddReservation();
            float secondDiscount = test.GetDiscount();
            test.AddReservation();
            test.AddReservation();
            float thirdDiscount = test.GetDiscount();

            firstDiscount.Should().Be(excpectedFirstDiscount);
            secondDiscount.Should().Be(excpectedSecondDiscount);
            thirdDiscount.Should().Be(excpectedThirdDiscount);
            test.NrOfReservations.Should().Be(expectedNrOfReservations);
            test.BtwNumber.Should().Be(expectedBtwNumber);
            test.Name.Should().BeEquivalentTo(excpectedName);
        }
    }
}
