using DataLayer.Func;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void DiscountTest()
        {
            var discounts = Parser.GetDiscounts();
            discounts.Should().NotBeEmpty();
            discounts.Count.Should().Be(26);
        }
        [TestMethod]
        public void ClientTest()
        {
            var clients = Parser.GetClients();
            clients.Should().NotBeEmpty();
            clients.Count.Should().Be(23);
        }
    }
}
