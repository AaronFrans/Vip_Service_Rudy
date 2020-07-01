using DataLayer.Func;
using DomainLayer.Domain.Clients;
using DomainLayer.Domain.Help;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class ClientTests
    {

        [TestMethod]
        public void TestVip()
        {
            List<ClientDiscount> discounts = Parser.GetDiscounts().Where(s => s.ClientType.Equals("Vip")).ToList();
            Vip test = new Vip(new Address("Leurshoek", "Beveren", "61"), "Vip", "0862333424", new Dictionary<int, int>(), discounts);
            float excpectedFirstDiscount = 0.00f;
            float excpectedSecondDiscount = 0.00f;
            float excpectedThirdDiscount = 5.00f;
            int expectedNrOfReservations = 3;
            string expectedBtwNumber = "0862333424";
            string excpectedName = "Vip";
            Address address = new Address("Leurshoek", "Beveren", "61");

            float firstDiscount = test.GetDiscount();
            test.AddReservation();
            test.AddReservation();
            float secondDiscount = test.GetDiscount();
            test.AddReservation();
            float thirdDiscount = test.GetDiscount();

            firstDiscount.Should().Be(excpectedFirstDiscount);
            secondDiscount.Should().Be(excpectedSecondDiscount);
            thirdDiscount.Should().Be(excpectedThirdDiscount);
            test.NrOfReservations[DateTime.Now.Year].Should().Be(expectedNrOfReservations);
            test.BtwNumber.Should().BeEquivalentTo(expectedBtwNumber);
            test.Name.Should().BeEquivalentTo(excpectedName);
            test.Address.Should().Be(address);
        }
        [TestMethod]
        public void TestEvenementenBureau()
        {
            List<ClientDiscount> discounts = Parser.GetDiscounts().Where(s => s.ClientType.Equals("EvenementenBureau")).ToList();
            EvenementenBureau test = new EvenementenBureau(new Address("Leurshoek", "Beveren", "61"), "EvenementenBureau", "0862333424", new Dictionary<int, int>(), discounts);
            float excpectedFirstDiscount = 0.00f;
            float excpectedSecondDiscount = 0.00f;
            float excpectedThirdDiscount = 1.00f;
            int expectedNrOfReservations = 6;
            string expectedBtwNumber = "0862333424";
            string excpectedName = "EvenementenBureau";
            Address address = new Address("Leurshoek", "Beveren", "61");

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
            test.NrOfReservations[DateTime.Now.Year].Should().Be(expectedNrOfReservations);
            test.BtwNumber.Should().BeEquivalentTo(expectedBtwNumber);
            test.Name.Should().BeEquivalentTo(excpectedName);
            test.Address.Should().Be(address);
        }
        [TestMethod]
        public void TestConcertPromotor()
        {
            List<ClientDiscount> discounts = Parser.GetDiscounts().Where(s => s.ClientType.Equals("ConcertPromotor")).ToList();
            ConcertPromotor test = new ConcertPromotor(new Address("Leurshoek", "Beveren", "61"), "ConcertPromotor", "0862333424", new Dictionary<int, int>(), discounts);
            float excpectedFirstDiscount = 0.00f;
            float excpectedSecondDiscount = 0.00f;
            float excpectedThirdDiscount = 10.00f;
            int expectedNrOfReservations = 4;
            string expectedBtwNumber = "0862333424";
            string excpectedName = "ConcertPromotor";
            Address address = new Address("Leurshoek", "Beveren", "61");

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
            test.NrOfReservations[DateTime.Now.Year].Should().Be(expectedNrOfReservations);
            test.BtwNumber.Should().BeEquivalentTo(expectedBtwNumber);
            test.Name.Should().BeEquivalentTo(excpectedName);
            test.Address.Should().Be(address);
        }
        [TestMethod]
        public void TestHuwelijksPlanner()
        {
            List<ClientDiscount> discounts = Parser.GetDiscounts().Where(s => s.ClientType.Equals("HuwelijksPlanner")).ToList();
            HuwelijksPlanner test = new HuwelijksPlanner(new Address("Leurshoek", "Beveren", "61"), "HuwelijksPlanner", "0862333424", new Dictionary<int, int>(), discounts);
            float excpectedFirstDiscount = 0.00f;
            float excpectedSecondDiscount = 0.00f;
            float excpectedThirdDiscount = 7.50f;
            int expectedNrOfReservations = 6;
            string expectedBtwNumber = "0862333424";
            string excpectedName = "HuwelijksPlanner";
            Address address = new Address("Leurshoek", "Beveren", "61");

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
            test.NrOfReservations[DateTime.Now.Year].Should().Be(expectedNrOfReservations);
            test.BtwNumber.Should().BeEquivalentTo(expectedBtwNumber);
            test.Name.Should().BeEquivalentTo(excpectedName);
            test.Address.Should().Be(address);
        }
        [TestMethod]
        public void TestOrganisatie()
        {

            List<ClientDiscount> discounts = Parser.GetDiscounts().Where(s => s.ClientType.Equals("Organisatie")).ToList();
            Organisatie test = new Organisatie(new Address("Leurshoek", "Beveren", "61"), "Organisatie", "0862333424", new Dictionary<int, int>(), discounts);
            float excpectedFirstDiscount = 0.00f;
            float excpectedSecondDiscount = 0.00f;
            float excpectedThirdDiscount = 7.50f;
            int expectedNrOfReservations = 11;
            string expectedBtwNumber = "0862333424";
            string excpectedName = "Organisatie";
            Address address = new Address("Leurshoek", "Beveren", "61");

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
            test.NrOfReservations[DateTime.Now.Year].Should().Be(expectedNrOfReservations);
            test.BtwNumber.Should().BeEquivalentTo(expectedBtwNumber);
            test.Name.Should().BeEquivalentTo(excpectedName);
            test.Address.Should().Be(address);
        }
        [TestMethod]
        public void TestParticulier()
        {
            List<ClientDiscount> discounts = Parser.GetDiscounts().Where(s => s.ClientType.Equals("Particulier")).ToList();
            Particulier test = new Particulier(new Address("Leurshoek", "Beveren", "61"), "Particulier", "0862333424", new Dictionary<int, int>(), discounts);
            float excpectedFirstDiscount = 0.00f;
            float excpectedSecondDiscount = 0.00f;
            float excpectedThirdDiscount = 2.50f;
            int expectedNrOfReservations = 4;
            string expectedBtwNumber = "0862333424";
            string excpectedName = "Particulier";
            Address address = new Address("Leurshoek", "Beveren", "61");

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
            test.NrOfReservations[DateTime.Now.Year].Should().Be(expectedNrOfReservations);
            test.BtwNumber.Should().BeEquivalentTo(expectedBtwNumber);
            test.Name.Should().BeEquivalentTo(excpectedName);
            test.Address.Should().Be(address);
        }

    }
}
