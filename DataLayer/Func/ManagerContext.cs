using DataLayer.Repositories;
using DomainLayer.Domain.Arangements;
using DomainLayer.Domain.Clients;
using DomainLayer.Domain.Help;
using DomainLayer.Domain.Reservation;
using DomainLayer.Domain.Vloot;
using DomainLayer.OtherInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DataLayer.Func
{
    public class ManagerContext : DbContext
    {
        private string connectionString;

        public ManagerContext()
        {
        }

        public ManagerContext(string db = "Production") : base()
        {
            SetConnectionString(db);
        }

        private void SetConnectionString(string db = "Production")
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile(@"Files\appsettings.json", optional: false);

            var configuration = builder.Build();
            switch (db)
            {
                case "Production":
                    connectionString = configuration.GetConnectionString("ProdSQLconnection").ToString();
                    break;
                case "Test":
                    connectionString = configuration.GetConnectionString("TestSQLconnection").ToString();
                    break;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (connectionString == null)
            {
                SetConnectionString();
            }
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Arangement>().ToTable("Arangements")
                                                .HasDiscriminator<int>("ArangementType")
                                                .HasValue<Airport>(1)
                                                .HasValue<Business>(2)
                                                .HasValue<Wellness>(3)
                                                .HasValue<NightLife>(4)
                                                .HasValue<Wedding>(5);
            modelBuilder.Entity<Address>().HasKey(a => new { a.Town, a.Street, a.StreetNumber });
            modelBuilder.Entity<Client>().Property(et => et.ClientNumber)
                                                .ValueGeneratedNever();


        }

        public DbSet<Limousine> Vehicles { get; set; }
        public DbSet<Reservering> Reservations { get; set; }
        public DbSet<Client> Clients { get; set; }

        public DbSet<Airport> Airport { get; set; }
        public DbSet<Business> Business { get; set; }
        public DbSet<NightLife> NightLife { get; set; }
        public DbSet<Wedding> Wedding { get; set; }
        public DbSet<Wellness> Wellness { get; set; }
    }

    public class ManagerContextTest : ManagerContext
    {
        public ManagerContextTest() : base("Test")
        {

        }
        public ManagerContextTest(bool keepExistingDB = false) : base("Test")
        {
            if (keepExistingDB)
            {
                Database.EnsureCreated();
            }
            else
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }


        }
    }
}
