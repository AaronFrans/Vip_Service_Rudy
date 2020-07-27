using DomainLayer.Domain.Clients;
using DomainLayer.Domain.Help;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class HelpTests
    {
        [TestMethod]
        public void TestAddress()
        {
            string Street = "Leurshoek";
            string Town = "Beveren";
            string StreetNumber = "61";

            Address address = new Address("Leurshoek", "Beveren", "61");

            address.Street.Should().Be(Street);
            address.Town.Should().Be(Town);
            address.StreetNumber.Should().Be(StreetNumber);
        }
        [TestMethod]
        public void TestClientDiscount()
        {
            ClientType type = ClientType.Vip;
            int needed = 5; ;
            float discount = 10.0f;

            ClientDiscount clientDiscount = new ClientDiscount(ClientType.Vip, 5, 10.0f);

            clientDiscount.ClientType.Should().BeEquivalentTo(type);
            clientDiscount.NrOfReservationsNeeded.Should().Be(needed);
            clientDiscount.Discount.Should().Be(discount);
        }

        [TestMethod]
        public void TestHourType()
        {
            string type = "Eerste uur";
            int nrOfHours = 1;
            int price = 300;

            HourType hourType = new HourType("Eerste uur", 1, 300);

            hourType.Type.Should().BeEquivalentTo(type);
            hourType.NrOfHours.Should().Be(nrOfHours);
            hourType.TotalPrice.Should().Be(price);
        }
    }
}
