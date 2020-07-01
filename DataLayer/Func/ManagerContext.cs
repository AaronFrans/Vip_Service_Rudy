using DataLayer.Repositories;
using DomainLayer.Domain.Reservation;
using DomainLayer.Domain.Vloot;
using DomainLayer.OtherInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DataLayer.Func
{
    public class ManagerContext : DbContext
    {
        public ManagerContext()
        {
        }

        public DbSet<IVehicle> Vehicles { get; set; }
        public DbSet<Reservering> Reservations { get; set; }
        public DbSet<IKlant> Clients { get; set; }



    }
}
