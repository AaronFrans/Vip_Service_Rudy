using DataLayer.Func;
using DomainLayer.Domain;
using DomainLayer.Domain.Arangements;
using DomainLayer.Domain.Clients;
using DomainLayer.Domain.Help;
using DomainLayer.Domain.Reservation;
using DomainLayer.Domain.Vloot;
using DomainLayer.OtherInterfaces;
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
    public class ReserveringTests
    {
        [TestMethod]
        public void DetailsTest_Constructor()
        {
            Address location = new Address("Leusrhoek", "Gent", "20");
            List<Arangement> arangements = new List<Arangement>() { new Wedding(2000) };
            Limousine limousine = new Limousine(200, "Tesla - X", 2, arangements);
            DateTime dateNeeded = new DateTime(10, 2, 23, 10, 0, 0);
            string arrangement = "Wedding";

            Details details = new Details(location, location, limousine, dateNeeded, arrangement);
            details.StartLocation.Should().Be(location);
            details.EndLocation.Should().Be(location);
            details.Limousine.Should().Be(limousine);
            details.DateLimousineNeeded.Should().Be(dateNeeded);
            details.BtwPercentage.Should().Be(6.0f);

        }
        [TestMethod]
        public void DetailsTest_Methods()
        {
            Address location = new Address("Leusrhoek", "Gent", "20");
            List<Arangement> arangements = new List<Arangement>() { new Wedding(2000) };
            Limousine limousine = new Limousine(200, "Tesla - X", 2, arangements);
            DateTime dateNeeded = new DateTime(10, 2, 23, 10, 0, 0);
            string arrangement = "Wedding";
            Details details = new Details(location, location, limousine, dateNeeded, arrangement);

            Address address = new Address("Leusrhoek", "Beveren", "20");
            var discounts = Parser.GetDiscounts().Where(d => d.ClientType == ClientType.Vip).ToList();
            Client test = new Client(ClientType.Vip, Parser.GetDiscounts().Where(s => s.ClientType.Equals("Vip")).ToList(), new Address("Leurshoek", "Beveren", "61"), "Vip", "0862333424", new List<ReservationsPerYear>());

            int subTotal = 2000;
            float usedDiscount = 0;
            int amountWithoutBtw = 2000;
            int btwAmount = 120;
            int toPay = 2120;

            details.CalculatePrices(test, startHour: new TimeSpan(7,0,0));

            details.SubTotal.Should().Be(subTotal);
            details.UsedDiscount.Should().Be(usedDiscount);
            details.AmountWithoutBtw.Should().Be(amountWithoutBtw);
            details.BtwAmount.Should().Be(btwAmount);
            details.ToPayAmount.Should().Be(toPay);
        }
        [TestMethod]
        public void DetailsTest_Exceptions()
        {
            Address startLocation = new Address("Leusrhoek", "Beveren", "20");
            Address enddLocation = new Address("Leusrhoek", "Beveren", "20");
            List<Arangement> arangements = new List<Arangement>() { new Wedding(2000) };
            Limousine limousine = new Limousine(200, "Tesla - X", 2, arangements);
            DateTime dateNeeded = new DateTime(10, 2, 23, 10, 0, 0);
            string arrangement = "Wedding";

            Action act = () => new Details(startLocation, enddLocation, limousine, dateNeeded, arrangement);
            act.Should().Throw<DomainException>().WithMessage("Start locatie van de limousine beschikbaar in volgende locaties: Antwerpen, Gent, Brussel, Hasselt en Charleroi.");

            startLocation = new Address("Leusrhoek", "Gent", "20");
            act = () => new Details(startLocation, enddLocation, limousine, dateNeeded, arrangement);
            act.Should().Throw<DomainException>().WithMessage("Eind locatie van de limousine beschikbaar in volgende locaties: Antwerpen, Gent, Brussel, Hasselt en Charleroi.");
        }
        [TestMethod]
        public void ReserveringTest_Contructor()
        {
            DateTime date = new DateTime(2000, 1, 10, 20, 3, 1);
            Address address = new Address("Leusrhoek", "Gent", "20");
            var discounts = Parser.GetDiscounts().Where(d => d.ClientType == ClientType.Vip).ToList();
            Client test = new Client(ClientType.Vip, Parser.GetDiscounts().Where(s => s.ClientType.Equals("Vip")).ToList(), new Address("Leurshoek", "Beveren", "61"), "Vip", "0862333424", new List<ReservationsPerYear>());

            Reservering reservering = new Reservering(date, test, address);

            reservering.ReservationDate.Should().Be(date);
            reservering.Client.Type.Should().Be(test.Type);
            reservering.Location.Should().Be(address);

        }
        [TestMethod]
        public void ReserveringTest_Exceptions()
        {
            DateTime date = new DateTime(2000, 1, 10, 20, 3, 1);
            Address address = new Address("Leusrhoek", "Beveren", "20");
            var discounts = Parser.GetDiscounts().Where(d => d.ClientType == ClientType.Vip).ToList();
            Client test = new Client(ClientType.Vip, Parser.GetDiscounts().Where(s => s.ClientType.Equals("Vip")).ToList(), new Address("Leurshoek", "Beveren", "61"), "Vip", "0862333424", new List<ReservationsPerYear>());

            Action act = () => new Reservering(date, test, address);
            act.Should().Throw<DomainException>().WithMessage("Limousines zijn aleen beschikbaar in volgende locaties: Antwerpen, Gent, Brussel, Hasselt en Charleroi.");


        }
        [TestMethod]
        public void ReserveringTest_Methods()
        {
            DateTime date = new DateTime(2000, 1, 10, 20, 3, 1);
            Address address = new Address("Leusrhoek", "Gent", "20");
            var discounts = Parser.GetDiscounts().Where(d => d.ClientType == ClientType.Vip).ToList();
            Client test = new Client(ClientType.Vip, Parser.GetDiscounts().Where(s => s.ClientType.Equals("Vip")).ToList(), new Address("Leurshoek", "Beveren", "61"), "Vip", "0862333424", new List<ReservationsPerYear>());
            
            Address endLocation = new Address("Leusrhoek", "Gent", "20");
            DateTime dateNeeded = new DateTime(2000, 2, 13, 8, 0, 0);
            List<Arangement> arangements = new List<Arangement>
            {
                new Wedding(2000),
                new Wellness(3200),
                new Airport(),
                new Business(),
                new NightLife(2500)
            };
            Limousine limousine = new Limousine(130, "Tesla Model X", 1, arangements);

            Reservering reservering = new Reservering(date, test, address);
            reservering.AddDetails(endLocation, limousine, dateNeeded, "Wedding", startHour: new TimeSpan(7, 0, 0));


            reservering.Details.ToPayAmount.Should().Be(2120);

        }
    }
}
