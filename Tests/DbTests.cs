using DataLayer.Func;
using DataLayer.Repositories;
using DataLayer.UoW;
using DomainLayer.Domain;
using DomainLayer.Domain.Arangements;
using DomainLayer.Domain.Vloot;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestClass]
    public class DbTests
    {
        [TestMethod]
        public void AddLimousineTests()
        {
            ManagerContextTest mct = new ManagerContextTest();
            ServiceManager sm = new ServiceManager(new UnitOfWork(mct));

            List<Arangement> arangements = new List<Arangement>();
            arangements.Add(new Business());
            arangements.Add(new Wellness(3000));
            arangements.Add(new Airport());

            Limousine l = new Limousine(200, "test", 3, arangements);

            sm.AddVehicle(l);


        }
    }
}
