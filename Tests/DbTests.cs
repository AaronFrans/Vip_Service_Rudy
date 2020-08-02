using DataLayer.Func;
using DataLayer.Repositories;
using DataLayer.UoW;
using DomainLayer.Domain;
using DomainLayer.Domain.Arangements;
using DomainLayer.Domain.Clients;
using DomainLayer.Domain.Help;
using DomainLayer.Domain.Vloot;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests
{
    [TestClass]
    public class DbTests
    {
        [TestMethod]
        public void AddLimousineTests()
        {
            ManagerContextTest mct = new ManagerContextTest(true);
            ServiceManager sm = new ServiceManager(new UnitOfWork(mct));

            List<Arangement> arangements = new List<Arangement>();
            arangements.Add(new Business());
            arangements.Add(new Wellness(3000));
            arangements.Add(new Airport());

            Limousine l1 = new Limousine(200, "test1", 3, arangements);

            Action act = () => sm.AddVehicle(l1);
            act.Should().NotThrow<Exception>();

            arangements = new List<Arangement>();
            arangements.Add(new Business());
            arangements.Add(new Wellness(1020));
            arangements.Add(new Airport());
            arangements.Add(new Wedding(3050));
            Limousine l2 = new Limousine(150, "test2", 1, arangements);

            arangements = new List<Arangement>();
            arangements.Add(new Business());
            arangements.Add(new Wellness(3500));
            arangements.Add(new Airport());
            arangements.Add(new Wedding(9000));
            Limousine l3 = new Limousine(210, "test3", 4, arangements);

            arangements = new List<Arangement>();
            arangements.Add(new Business());
            arangements.Add(new Wellness(3500));
            arangements.Add(new Airport());
            arangements.Add(new Wedding(2000));
            arangements.Add(new NightLife(3210));
            Limousine l4 = new Limousine(290, "test4", 7, arangements);

            List<Limousine> ls = new List<Limousine>() { l2, l3, l4 };
            act = () => sm.AddVehicles(ls);
            act.Should().NotThrow<Exception>();
        }
        [TestMethod]
        public void GetLimousineTests()
        {
            ManagerContextTest mct = new ManagerContextTest(true);
            ServiceManager sm = new ServiceManager(new UnitOfWork(mct));

            var ls = sm.GetVehicles();

            ls.Count.Should().Be(4);

            ls[0].Arangements.Count.Should().Be(3);
            ls[0].HireDates.Count.Should().Be(0);
            ls[0].Name.Should().BeEquivalentTo("test1");
            ls[0].Available.Should().Be(3);
            ls[0].FirstHourPrice.Should().Be(200);
        }


        [TestMethod]
        public void AddClientTests()
        {
            ManagerContextTest mct = new ManagerContextTest(false);
            ServiceManager sm = new ServiceManager(new UnitOfWork(mct));

            Client c1 = new Client(ClientType.Vip, Parser.GetDiscounts().Where(s => s.ClientType == ClientType.Vip).ToList(), new Address("Leurshoek", "Beveren", "61"), "Aaron Frans", null, new List<ReservationsPerYear>());
            c1.ClientNumber = 1;

            Action act = () => sm.AddClient(c1);
            act.Should().NotThrow<Exception>();

            Client c2 = new Client(ClientType.ConcertPromotor, Parser.GetDiscounts().Where(s => s.ClientType == ClientType.ConcertPromotor).ToList(), new Address("Leurshoek", "Beveren", "10"), "Jeff Jeff", "BE0823665992", new List<ReservationsPerYear>());
            c2.ClientNumber = 2;
            Client c3 = new Client(ClientType.Particulier, Parser.GetDiscounts().Where(s => s.ClientType == ClientType.Particulier).ToList(), new Address("Kerkstraat", "Gent", "61"), "Jane Doe", null, new List<ReservationsPerYear>());
            c3.ClientNumber = 3;
            Client c4 = new Client(ClientType.Organisatie, Parser.GetDiscounts().Where(s => s.ClientType == ClientType.Organisatie).ToList(), new Address("Kerkstraat", "Antwerpen", "61"), "Jhon Doe", "BE0852665992", new List<ReservationsPerYear>());
            c4.ClientNumber = 4;

            List<Client> cs = new List<Client>() { c2, c3, c4 };
            sm.AddClients(cs);

        }
        [TestMethod]
        public void GetClientTests()
        {
            ManagerContextTest mct = new ManagerContextTest(true);
            ServiceManager sm = new ServiceManager(new UnitOfWork(mct));

            var cs = sm.GetClients();

            cs.Count.Should().Be(4);

            cs[0].Address.Should().Be(new Address("Leurshoek", "Beveren", "61") { Id = 1 });
            cs[0].BtwNumber.Should().BeNull();
            cs[0].ClientNumber.Should().Be(1);
            cs[0].Name.Should().BeEquivalentTo("Aaron Frans");
            cs[0].PastReservations.Should().BeEmpty();
            cs[0].StaffelDiscount.Count.Should().Be(4);
            cs[0].StaffelDiscount.First().ClientType.Should().Be(ClientType.Vip);
            cs[0].Type.Should().Be(ClientType.Vip);

            var c1 = sm.GetClientViaNumber(1);

            c1.Address.Should().Be(new Address("Leurshoek", "Beveren", "61") { Id = 1 });
            c1.BtwNumber.Should().BeNull();
            c1.ClientNumber.Should().Be(1);
            c1.Name.Should().BeEquivalentTo("Aaron Frans");
            c1.PastReservations.Should().BeEmpty();
            c1.StaffelDiscount.Count.Should().Be(4);
            c1.StaffelDiscount.First().ClientType.Should().Be(ClientType.Vip);
            c1.Type.Should().Be(ClientType.Vip);

            var c1_b = sm.GetClientViaInfo("Aaron Frans");
            c1_b.Address.Should().Be(new Address("Leurshoek", "Beveren", "61") { Id = 1 });
            c1_b.BtwNumber.Should().BeNull();
            c1_b.ClientNumber.Should().Be(1);
            c1_b.Name.Should().BeEquivalentTo("Aaron Frans");
            c1_b.PastReservations.Should().BeEmpty();
            c1_b.StaffelDiscount.Count.Should().Be(4);
            c1_b.StaffelDiscount.First().ClientType.Should().Be(ClientType.Vip);
            c1_b.Type.Should().Be(ClientType.Vip);

            var ds = sm.GetDiscountsForType(ClientType.Vip);

            ds.Count.Should().Be(4);
            ds[1].ClientType.Should().Be(ClientType.Vip);
        }

        [TestMethod]
        public void HireLimousineTest()
        {
            ManagerContextTest mct = new ManagerContextTest(true);
            ServiceManager sm = new ServiceManager(new UnitOfWork(mct));

            var StartLocation = new Address("Van Creanbroeck", "Gent", "9120");
            var EndLocation = new Address("Van Creanbroeck", "Gent", "9120");

            var ReservationDate = DateTime.Now;

            var result = sm.HireVehicle("test1", "Wellness", StartLocation, 1, ReservationDate,
                EndLocation, new DateTime(2020, 10, 5), startHour: new TimeSpan(8, 0, 0));

            result.Client.Name.Should().BeEquivalentTo("Aaron Frans");
            result.Client.BtwNumber.Should().BeNull();

            result.Location.Should().Be(StartLocation);

            result.ReservationDate.Should().Be(ReservationDate);

            result.Details.StartLocation.Should().Be(StartLocation);
            result.Details.EndLocation.Should().Be(EndLocation);

            result.Details.Limousine.Name.Should().BeEquivalentTo("test1");

            result.Details.DateLimousineNeeded.Should().Be(new DateTime(2020, 10, 5));

            result.Details.Arangement.Should().BeEquivalentTo("Wellness");

            result.Details.SubTotal.Should().Be(3000);
            result.Details.UsedDiscount.Should().Be(0.0f);
            result.Details.AmountWithoutBtw.Should().Be(3000);
            result.Details.BtwAmount.Should().Be(180);
            result.Details.ToPayAmount.Should().Be(3180);
        }


        [TestMethod]
        public void UpdateLimousinesTests()
        {
            ManagerContextTest mct = new ManagerContextTest(true);
            ServiceManager sm = new ServiceManager(new UnitOfWork(mct));

            var StartLocation = new Address("Van Creanbroeck", "Gent", "9120");
            var ReservationDate = DateTime.Now;

            sm.HireVehicle("test2", "Wellness", StartLocation, 1, ReservationDate,
            StartLocation, new DateTime(2020, 10, 5), startHour: new TimeSpan(8, 0, 0));

            var NewReservationDate = new DateTime(2020, 10, 5, 17, 0, 0);
            sm.UpdateVehicles(NewReservationDate);

            sm.GetVehicles().Single(c => c.Name.Equals("test2")).HireDates.Count.Should().Be(1);

            NewReservationDate = new DateTime(2020, 10, 5, 17, 0, 0);
            sm.UpdateVehicles(NewReservationDate);

            sm.GetVehicles().Single(c => c.Name.Equals("test2")).HireDates.Count.Should().Be(0);

        }
    }
}

