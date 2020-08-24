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
    /// <summary>
    /// Used to connect to the database.
    /// </summary>
    public class ManagerContext : DbContext
    {
        private string connectionString;

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public ManagerContext()
        {
        }
        
        /// <summary>
        /// Constructor used to connect to the database.
        /// </summary>
        /// <param name="db">Which database should be used, default is the production database.</param>
        public ManagerContext(string db = "Production") : base()
        {
            SetConnectionString(db);
        }

        /// <summary>
        /// Gets the connection string from the AppSettings.json file.
        /// </summary>
        /// <param name="db">Determines which database should be used. The defvault is the production database.</param>
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
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Arangement>().ToTable("Arangements")
                                                .HasDiscriminator<int>("ArangementType")
                                                .HasValue<Airport>(1)
                                                .HasValue<Business>(2)
                                                .HasValue<Wellness>(3)
                                                .HasValue<Nightlife>(4)
                                                .HasValue<Wedding>(5);
            modelBuilder.Entity<Client>().Property(c => c.ClientNumber)
                                                .ValueGeneratedNever();
        }

        /// <summary>
        /// Gives acces to the Limousines in the database.
        /// </summary>
        public DbSet<Limousine> Vehicles { get; set; }
        /// <summary>
        /// Gives acces to the reservations in the database.
        /// </summary>
        public DbSet<Reservering> Reservations { get; set; }
        /// <summary>
        /// Gives acces to the clients in the database.
        /// </summary>
        public DbSet<Client> Clients { get; set; }

        /// <summary>
        /// Used for implementation of the abstract arrangement class.
        /// </summary>
        private DbSet<Airport> Airport { get; set; }
        /// <summary>
        /// Used for implementation of the abstract arrangement class.
        /// </summary>
        private DbSet<Business> Business { get; set; }
        /// <summary>
        /// Used for implementation of the abstract arrangement class.
        /// </summary>
        private DbSet<Nightlife> NightLife { get; set; }
        /// <summary>
        /// Used for implementation of the abstract arrangement class.
        /// </summary>
        private DbSet<Wedding> Wedding { get; set; }
        /// <summary>
        /// Used for implementation of the abstract arrangement class.
        /// </summary>
        private DbSet<Wellness> Wellness { get; set; }
    }

    /// <summary>
    /// Used to easily test the functionality of the database.
    /// </summary>
    public class ManagerContextTest : ManagerContext
    {
        /// <summary>
        /// Empty contructor, it will connect  to the test databse.
        /// </summary>
        public ManagerContextTest() : base("Test")
        {

        }
        /// <summary>
        /// Constructor used to connect to the test database.
        /// </summary>
        /// <param name="keepExistingDB">Determines if the test database should be emptied. It will delete by default.</param>
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
