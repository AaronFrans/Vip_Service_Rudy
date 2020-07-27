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
        public void TestClient()
        {
            List<ClientDiscount> discounts = Parser.GetDiscounts().Where(s => s.ClientType.Equals("Vip")).ToList();
            float excpectedFirstDiscount = 0.00f;
            float excpectedSecondDiscount = 0.00f;
            float excpectedThirdDiscount = 5.00f;
            int expectedNrOfReservations = 3;
            string expectedBtwNumber = "0862333424";
            string excpectedName = "Vip";
            Address excpectedAddress = new Address("Leurshoek", "Beveren", "61");
            ClientType type = ClientType.Vip;

            Client test = new Client(ClientType.Vip, Parser.GetDiscounts().Where(s => s.ClientType == ClientType.Vip).ToList(), new Address("Leurshoek", "Beveren", "61"), "Vip", "0862333424", new List<ReservationsPerYear>());

            float firstDiscount = test.GetDiscount();
            test.AddReservation();
            test.AddReservation();
            float secondDiscount = test.GetDiscount();
            test.AddReservation();
            float thirdDiscount = test.GetDiscount();

            firstDiscount.Should().Be(excpectedFirstDiscount);
            secondDiscount.Should().Be(excpectedSecondDiscount);
            thirdDiscount.Should().Be(excpectedThirdDiscount);
            test.PastReservations.Single(d => d.Year == DateTime.Now.Year).NrOfReservations.Should().Be(expectedNrOfReservations);
            test.BtwNumber.Should().BeEquivalentTo(expectedBtwNumber);
            test.Name.Should().BeEquivalentTo(excpectedName);
            test.Address.Should().Be(excpectedAddress);
            test.Type.Should().Be(type);
        }
    }
}
