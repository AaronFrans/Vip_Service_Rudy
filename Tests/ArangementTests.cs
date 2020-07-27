
using DomainLayer.Domain;
using DomainLayer.Domain.Arangements;
using DomainLayer.Domain.Clients;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class ArangementTests
    {
        [TestMethod]
        public void AirportTest_Constructor()
        {
            TimeSpan StartHour = new TimeSpan(40, 0, 0);
            TimeSpan EndHour = new TimeSpan(40, 0, 0);
            int MaxAmountOfHours = 11;
            long StartTicks = 1440000000000;
            long EndTicks = 1440000000000;

            Airport airport = new Airport();

            airport.StartHour.Should().Be(StartHour);
            airport.EndHour.Should().Be(EndHour);
            airport.MaxAmountOfHours.Should().Be(MaxAmountOfHours);
            airport.StartHourTicks.Should().Be(StartTicks);
            airport.EndHourTicks.Should().Be(EndTicks);
        }
        [TestMethod]
        public void AirportTest_Methods()
        {
            Airport airport = new Airport();
            int firstHourPrice = 100;
            int nightHoursPrice = 420;
            int dayHoursPrice = 0;
            int firstHour = 1;
            int nightHours = 3;
            int dayHours = 0;
            int totalPrice = 520;
            TimeSpan originalStartHour = new TimeSpan(40, 0, 0);
            TimeSpan originalEndHour = new TimeSpan(40, 0, 0);
            TimeSpan StartHour = new TimeSpan(22, 0, 0);
            TimeSpan EndHour = new TimeSpan(26, 0, 0);

            airport.SetTime(new TimeSpan(22, 0, 0), new TimeSpan(26, 0, 0));
            airport.StartHour.Should().Be(StartHour);
            airport.EndHour.Should().Be(EndHour);

            var result = airport.GetPrice(100);
            airport.GetEndTime();
            result.Key.Should().Be(totalPrice);
            result.Value.Single(h => h.Type == "Eerste uur").TotalPrice.Should().Be(firstHourPrice);
            result.Value.Single(h => h.Type == "Nacht uren").TotalPrice.Should().Be(nightHoursPrice);
            result.Value.Single(h => h.Type == "Dag uren").TotalPrice.Should().Be(dayHoursPrice);
            result.Value.Single(h => h.Type == "Eerste uur").NrOfHours.Should().Be(firstHour);
            result.Value.Single(h => h.Type == "Nacht uren").NrOfHours.Should().Be(nightHours);
            result.Value.Single(h => h.Type == "Dag uren").NrOfHours.Should().Be(dayHours);
            airport.StartHour.Should().Be(originalStartHour);
            airport.EndHour.Should().Be(originalEndHour);

            StartHour = new TimeSpan(22, 0, 0);
            EndHour = new TimeSpan(26, 0, 0);
            firstHourPrice = 102;
            nightHoursPrice = 430;
            dayHoursPrice = 0;
            firstHour = 1;
            nightHours = 3;
            dayHours = 0;
            totalPrice = 532;
            airport.SetTime(StartHour, EndHour);

            result = airport.GetPrice(102);
            airport.GetEndTime();
            result.Key.Should().Be(totalPrice);
            result.Value.Single(h => h.Type == "Eerste uur").TotalPrice.Should().Be(firstHourPrice);
            result.Value.Single(h => h.Type == "Nacht uren").TotalPrice.Should().Be(nightHoursPrice);
            result.Value.Single(h => h.Type == "Dag uren").TotalPrice.Should().Be(dayHoursPrice);
            result.Value.Single(h => h.Type == "Eerste uur").NrOfHours.Should().Be(firstHour);
            result.Value.Single(h => h.Type == "Nacht uren").NrOfHours.Should().Be(nightHours);
            result.Value.Single(h => h.Type == "Dag uren").NrOfHours.Should().Be(dayHours);
            airport.StartHour.Should().Be(originalStartHour);
            airport.EndHour.Should().Be(originalEndHour);

            StartHour = new TimeSpan(8, 0, 0);
            EndHour = new TimeSpan(15, 0, 0);
            firstHourPrice = 102;
            nightHoursPrice = 0;
            dayHoursPrice = 400;
            firstHour = 1;
            nightHours = 0;
            dayHours = 6;
            totalPrice = 502;
            airport.SetTime(StartHour, EndHour);

            result = airport.GetPrice(102);
            airport.GetEndTime();
            result.Key.Should().Be(totalPrice);
            result.Value.Single(h => h.Type == "Eerste uur").TotalPrice.Should().Be(firstHourPrice);
            result.Value.Single(h => h.Type == "Nacht uren").TotalPrice.Should().Be(nightHoursPrice);
            result.Value.Single(h => h.Type == "Dag uren").TotalPrice.Should().Be(dayHoursPrice);
            result.Value.Single(h => h.Type == "Eerste uur").NrOfHours.Should().Be(firstHour);
            result.Value.Single(h => h.Type == "Nacht uren").NrOfHours.Should().Be(nightHours);
            result.Value.Single(h => h.Type == "Dag uren").NrOfHours.Should().Be(dayHours);
            airport.StartHour.Should().Be(originalStartHour);
            airport.EndHour.Should().Be(originalEndHour);

            StartHour = new TimeSpan(23, 0, 0);
            EndHour = new TimeSpan(24 + 8, 0, 0);
            firstHourPrice = 102;
            nightHoursPrice = 1000;
            dayHoursPrice = 65;
            firstHour = 1;
            nightHours = 7;
            dayHours = 1;
            totalPrice = 1167;
            airport.SetTime(StartHour, EndHour);

            result = airport.GetPrice(102);
            airport.GetEndTime();
            result.Key.Should().Be(totalPrice);
            result.Value.Single(h => h.Type == "Eerste uur").TotalPrice.Should().Be(firstHourPrice);
            result.Value.Single(h => h.Type == "Nacht uren").TotalPrice.Should().Be(nightHoursPrice);
            result.Value.Single(h => h.Type == "Dag uren").TotalPrice.Should().Be(dayHoursPrice);
            result.Value.Single(h => h.Type == "Eerste uur").NrOfHours.Should().Be(firstHour);
            result.Value.Single(h => h.Type == "Nacht uren").NrOfHours.Should().Be(nightHours);
            result.Value.Single(h => h.Type == "Dag uren").NrOfHours.Should().Be(dayHours);
            airport.StartHour.Should().Be(originalStartHour);
            airport.EndHour.Should().Be(originalEndHour);

            StartHour = new TimeSpan(15, 0, 0);
            EndHour = new TimeSpan(24, 0, 0);
            firstHourPrice = 102;
            nightHoursPrice = 145;
            dayHoursPrice = 465;
            firstHour = 1;
            nightHours = 1;
            dayHours = 7;
            totalPrice = 712;
            airport.SetTime(StartHour, EndHour);

            result = airport.GetPrice(102);
            airport.GetEndTime();
            result.Key.Should().Be(totalPrice);
            result.Value.Single(h => h.Type == "Eerste uur").TotalPrice.Should().Be(firstHourPrice);
            result.Value.Single(h => h.Type == "Nacht uren").TotalPrice.Should().Be(nightHoursPrice);
            result.Value.Single(h => h.Type == "Dag uren").TotalPrice.Should().Be(dayHoursPrice);
            result.Value.Single(h => h.Type == "Eerste uur").NrOfHours.Should().Be(firstHour);
            result.Value.Single(h => h.Type == "Nacht uren").NrOfHours.Should().Be(nightHours);
            result.Value.Single(h => h.Type == "Dag uren").NrOfHours.Should().Be(dayHours);
            airport.StartHour.Should().Be(originalStartHour);
            airport.EndHour.Should().Be(originalEndHour);


            StartHour = new TimeSpan(21, 0, 0);
            EndHour = new TimeSpan(1,7, 0, 0);
            firstHourPrice = 102;
            nightHoursPrice = 1140;
            dayHoursPrice = 65;
            firstHour = 1;
            nightHours = 8;
            dayHours = 1;
            totalPrice = 1307;
            airport.SetTime(StartHour, EndHour);

            result = airport.GetPrice(102);
            airport.GetEndTime();
            result.Key.Should().Be(totalPrice);
            result.Value.Single(h => h.Type == "Eerste uur").TotalPrice.Should().Be(firstHourPrice);
            result.Value.Single(h => h.Type == "Nacht uren").TotalPrice.Should().Be(nightHoursPrice);
            result.Value.Single(h => h.Type == "Dag uren").TotalPrice.Should().Be(dayHoursPrice);
            result.Value.Single(h => h.Type == "Eerste uur").NrOfHours.Should().Be(firstHour);
            result.Value.Single(h => h.Type == "Nacht uren").NrOfHours.Should().Be(nightHours);
            result.Value.Single(h => h.Type == "Dag uren").NrOfHours.Should().Be(dayHours);
            airport.StartHour.Should().Be(originalStartHour);
            airport.EndHour.Should().Be(originalEndHour);

        }
        [TestMethod]
        public void AirportTest_Exceptions()
        {
            Airport airport = new Airport();
            Action act = () => airport.GetPrice(1);
            act.Should().Throw<DomainException>().WithMessage("Zorg er aub voor dat de start- en eindtijd ingevuld zijn");

            act = () => airport.SetTime(new TimeSpan(-1, 0, 0), new TimeSpan(15, 0, 0));
            act.Should().Throw<DomainException>().WithMessage("Zorg er a.u.b. voor dat het start uur positief.");

            act = () => airport.SetTime(new TimeSpan(20, 0, 0), new TimeSpan(15, 0, 0));
            act.Should().Throw<DomainException>().WithMessage("Zorg er a.u.b. voor dat het eind uur na het start uur is.");

            act = () => airport.SetTime(new TimeSpan(10, 7, 0), new TimeSpan(15, 0, 0));
            act.Should().Throw<DomainException>().WithMessage("Zorg er a.u.b. voor dat het start uur geen minuten of seconden heeft.");

            act = () => airport.SetTime(new TimeSpan(10, 0, 7), new TimeSpan(15, 0, 0));
            act.Should().Throw<DomainException>().WithMessage("Zorg er a.u.b. voor dat het start uur geen minuten of seconden heeft.");

            act = () => airport.SetTime(new TimeSpan(10, 0, 0), new TimeSpan(15, 7, 0));
            act.Should().Throw<DomainException>().WithMessage("Zorg er a.u.b. voor dat het eind uur geen minuten of seconden heeft.");

            act = () => airport.SetTime(new TimeSpan(10, 0, 0), new TimeSpan(15, 0, 7));
            act.Should().Throw<DomainException>().WithMessage("Zorg er a.u.b. voor dat het eind uur geen minuten of seconden heeft.");

            act = () => airport.SetTime(new TimeSpan(25, 0, 0), new TimeSpan(30, 0, 0));
            act.Should().Throw<DomainException>().WithMessage("Zorg er a.u.b. voor dat het start uur kleiner is dan 24 uur.");

            act = () => airport.SetTime(new TimeSpan(10, 0, 0), new TimeSpan(22, 0, 0));
            act.Should().Throw<DomainException>().WithMessage("Zorg er a.u.b. voor dat het eind uur niwet meer dan elf uur na het start uur is.");
        }

        [TestMethod]
        public void BusinessTest_Constructor()
        {
            TimeSpan StartHour = new TimeSpan(40, 0, 0);
            TimeSpan EndHour = new TimeSpan(40, 0, 0);
            long StartTicks = 1440000000000;
            long EndTicks = 1440000000000;



            Business business = new Business();

            business.StartHour.Should().Be(StartHour);
            business.EndHour.Should().Be(EndHour);
            business.StartHourTicks.Should().Be(StartTicks);
            business.EndHourTicks.Should().Be(EndTicks);

        }
        [TestMethod]
        public void BusinessTest_Methods()
        {
            Business business = new Business();
            int firstHourPrice = 100;
            int nightHoursPrice = 420;
            int dayHoursPrice = 0;
            int firstHour = 1;
            int nightHours = 3;
            int dayHours = 0;
            int totalPrice = 520;
            TimeSpan originalStartHour = new TimeSpan(40, 0, 0);
            TimeSpan originalEndHour = new TimeSpan(40, 0, 0);
            TimeSpan StartHour = new TimeSpan(22, 0, 0);
            TimeSpan EndHour = new TimeSpan(26, 0, 0);

            business.SetTime(new TimeSpan(22, 0, 0), new TimeSpan(26, 0, 0));
            business.StartHour.Should().Be(StartHour);
            business.EndHour.Should().Be(EndHour);

            var result = business.GetPrice(100);
            business.GetEndTime();
            result.Key.Should().Be(totalPrice);
            result.Value.Single(h => h.Type == "Eerste uur").TotalPrice.Should().Be(firstHourPrice);
            result.Value.Single(h => h.Type == "Nacht uren").TotalPrice.Should().Be(nightHoursPrice);
            result.Value.Single(h => h.Type == "Dag uren").TotalPrice.Should().Be(dayHoursPrice);
            result.Value.Single(h => h.Type == "Eerste uur").NrOfHours.Should().Be(firstHour);
            result.Value.Single(h => h.Type == "Nacht uren").NrOfHours.Should().Be(nightHours);
            result.Value.Single(h => h.Type == "Dag uren").NrOfHours.Should().Be(dayHours);
            business.StartHour.Should().Be(originalStartHour);
            business.EndHour.Should().Be(originalEndHour);

            StartHour = new TimeSpan(22, 0, 0);
            EndHour = new TimeSpan(26, 0, 0);
            firstHourPrice = 102;
            nightHoursPrice = 430;
            dayHoursPrice = 0;
            firstHour = 1;
            nightHours = 3;
            dayHours = 0;
            totalPrice = 532;
            business.SetTime(StartHour, EndHour);

            result = business.GetPrice(102);
            business.GetEndTime();
            result.Key.Should().Be(totalPrice);
            result.Value.Single(h => h.Type == "Eerste uur").TotalPrice.Should().Be(firstHourPrice);
            result.Value.Single(h => h.Type == "Nacht uren").TotalPrice.Should().Be(nightHoursPrice);
            result.Value.Single(h => h.Type == "Dag uren").TotalPrice.Should().Be(dayHoursPrice);
            result.Value.Single(h => h.Type == "Eerste uur").NrOfHours.Should().Be(firstHour);
            result.Value.Single(h => h.Type == "Nacht uren").NrOfHours.Should().Be(nightHours);
            result.Value.Single(h => h.Type == "Dag uren").NrOfHours.Should().Be(dayHours);
            business.StartHour.Should().Be(originalStartHour);
            business.EndHour.Should().Be(originalEndHour);

            StartHour = new TimeSpan(8, 0, 0);
            EndHour = new TimeSpan(15, 0, 0);
            firstHourPrice = 102;
            nightHoursPrice = 0;
            dayHoursPrice = 400;
            firstHour = 1;
            nightHours = 0;
            dayHours = 6;
            totalPrice = 502;
            business.SetTime(StartHour, EndHour);

            result = business.GetPrice(102);
            business.GetEndTime();
            result.Key.Should().Be(totalPrice);
            result.Value.Single(h => h.Type == "Eerste uur").TotalPrice.Should().Be(firstHourPrice);
            result.Value.Single(h => h.Type == "Nacht uren").TotalPrice.Should().Be(nightHoursPrice);
            result.Value.Single(h => h.Type == "Dag uren").TotalPrice.Should().Be(dayHoursPrice);
            result.Value.Single(h => h.Type == "Eerste uur").NrOfHours.Should().Be(firstHour);
            result.Value.Single(h => h.Type == "Nacht uren").NrOfHours.Should().Be(nightHours);
            result.Value.Single(h => h.Type == "Dag uren").NrOfHours.Should().Be(dayHours);
            business.StartHour.Should().Be(originalStartHour);
            business.EndHour.Should().Be(originalEndHour);

            StartHour = new TimeSpan(23, 0, 0);
            EndHour = new TimeSpan(24 + 8, 0, 0);
            firstHourPrice = 102;
            nightHoursPrice = 1000;
            dayHoursPrice = 65;
            firstHour = 1;
            nightHours = 7;
            dayHours = 1;
            totalPrice = 1167;
            business.SetTime(StartHour, EndHour);

            result = business.GetPrice(102);
            business.GetEndTime();
            result.Key.Should().Be(totalPrice);
            result.Value.Single(h => h.Type == "Eerste uur").TotalPrice.Should().Be(firstHourPrice);
            result.Value.Single(h => h.Type == "Nacht uren").TotalPrice.Should().Be(nightHoursPrice);
            result.Value.Single(h => h.Type == "Dag uren").TotalPrice.Should().Be(dayHoursPrice);
            result.Value.Single(h => h.Type == "Eerste uur").NrOfHours.Should().Be(firstHour);
            result.Value.Single(h => h.Type == "Nacht uren").NrOfHours.Should().Be(nightHours);
            result.Value.Single(h => h.Type == "Dag uren").NrOfHours.Should().Be(dayHours);
            business.StartHour.Should().Be(originalStartHour);
            business.EndHour.Should().Be(originalEndHour);

            StartHour = new TimeSpan(15, 0, 0);
            EndHour = new TimeSpan(24, 0, 0);
            firstHourPrice = 102;
            nightHoursPrice = 145;
            dayHoursPrice = 465;
            firstHour = 1;
            nightHours = 1;
            dayHours = 7;
            totalPrice = 712;
            business.SetTime(StartHour, EndHour);

            result = business.GetPrice(102);
            business.GetEndTime();
            result.Key.Should().Be(totalPrice);
            result.Value.Single(h => h.Type == "Eerste uur").TotalPrice.Should().Be(firstHourPrice);
            result.Value.Single(h => h.Type == "Nacht uren").TotalPrice.Should().Be(nightHoursPrice);
            result.Value.Single(h => h.Type == "Dag uren").TotalPrice.Should().Be(dayHoursPrice);
            result.Value.Single(h => h.Type == "Eerste uur").NrOfHours.Should().Be(firstHour);
            result.Value.Single(h => h.Type == "Nacht uren").NrOfHours.Should().Be(nightHours);
            result.Value.Single(h => h.Type == "Dag uren").NrOfHours.Should().Be(dayHours);
            business.StartHour.Should().Be(originalStartHour);
            business.EndHour.Should().Be(originalEndHour);

            StartHour = new TimeSpan(21, 0, 0);
            EndHour = new TimeSpan(1, 7, 0, 0);
            firstHourPrice = 102;
            nightHoursPrice = 1140;
            dayHoursPrice = 65;
            firstHour = 1;
            nightHours = 8;
            dayHours = 1;
            totalPrice = 1307;
            business.SetTime(StartHour, EndHour);

            result = business.GetPrice(102);
            business.GetEndTime();
            result.Key.Should().Be(totalPrice);
            result.Value.Single(h => h.Type == "Eerste uur").TotalPrice.Should().Be(firstHourPrice);
            result.Value.Single(h => h.Type == "Nacht uren").TotalPrice.Should().Be(nightHoursPrice);
            result.Value.Single(h => h.Type == "Dag uren").TotalPrice.Should().Be(dayHoursPrice);
            result.Value.Single(h => h.Type == "Eerste uur").NrOfHours.Should().Be(firstHour);
            result.Value.Single(h => h.Type == "Nacht uren").NrOfHours.Should().Be(nightHours);
            result.Value.Single(h => h.Type == "Dag uren").NrOfHours.Should().Be(dayHours);
            business.StartHour.Should().Be(originalStartHour);
            business.EndHour.Should().Be(originalEndHour);
        }
        [TestMethod]
        public void BusinessTest_Exceptions()
        {
            Business business = new Business();
            Action act = () => business.GetPrice(1);
            act.Should().Throw<DomainException>().WithMessage("Zorg er aub voor dat de start- en eindtijd ingevuld zijn");

            act = () => business.SetTime(new TimeSpan(-1, 0, 0), new TimeSpan(15, 0, 0));
            act.Should().Throw<DomainException>().WithMessage("Zorg er a.u.b. voor dat het start uur positief.");

            act = () => business.SetTime(new TimeSpan(20, 0, 0), new TimeSpan(15, 0, 0));
            act.Should().Throw<DomainException>().WithMessage("Zorg er a.u.b. voor dat het eind uur na het start uur is.");

            act = () => business.SetTime(new TimeSpan(10, 7, 0), new TimeSpan(15, 0, 0));
            act.Should().Throw<DomainException>().WithMessage("Zorg er a.u.b. voor dat het start uur geen minuten of seconden heeft.");

            act = () => business.SetTime(new TimeSpan(10, 0, 7), new TimeSpan(15, 0, 0));
            act.Should().Throw<DomainException>().WithMessage("Zorg er a.u.b. voor dat het start uur geen minuten of seconden heeft.");

            act = () => business.SetTime(new TimeSpan(10, 0, 0), new TimeSpan(15, 7, 0));
            act.Should().Throw<DomainException>().WithMessage("Zorg er a.u.b. voor dat het eind uur geen minuten of seconden heeft.");

            act = () => business.SetTime(new TimeSpan(10, 0, 0), new TimeSpan(15, 0, 7));
            act.Should().Throw<DomainException>().WithMessage("Zorg er a.u.b. voor dat het eind uur geen minuten of seconden heeft.");

            act = () => business.SetTime(new TimeSpan(25, 0, 0), new TimeSpan(30, 0, 0));
            act.Should().Throw<DomainException>().WithMessage("Zorg er a.u.b. voor dat het start uur kleiner is dan 24 uur.");

            act = () => business.SetTime(new TimeSpan(10, 0, 0), new TimeSpan(22, 0, 0));
            act.Should().Throw<DomainException>().WithMessage("Zorg er a.u.b. voor dat het eind uur niwet meer dan elf uur na het start uur is.");
        }

        [TestMethod]
        public void WellnessTest_Constructor()
        {
            int Price = 1000;
            TimeSpan StartHour = new TimeSpan(40, 0, 0);
            TimeSpan EndHour = new TimeSpan(40, 0, 0);
            int MaxAmountOfHours = 11;
            long StartTicks = 1440000000000;
            long EndTicks = 1440000000000;

            Wellness wellness = new Wellness(1000);

            wellness.Price.Should().Be(Price);
            wellness.StartHour.Should().Be(StartHour);
            wellness.EndHour.Should().Be(EndHour);
            wellness.MaxAmountOfHours.Should().Be(MaxAmountOfHours);
            wellness.StartHourTicks.Should().Be(StartTicks);
            wellness.EndHourTicks.Should().Be(EndTicks);
        }
        [TestMethod]
        public void WellnessTest_Methods()
        {
            Wellness wellness = new Wellness(1000);
            TimeSpan StartHour = new TimeSpan(10, 0, 0);
            TimeSpan EndHour = new TimeSpan(20, 0, 0);

            wellness.SetTime(new TimeSpan(10, 0, 0));
            wellness.StartHour.Should().Be(StartHour);
            wellness.EndHour.Should().Be(EndHour);
            wellness.GetEndTime();
        }
        [TestMethod]
        public void WellnessTest_Exceptions()
        {
            Wellness wellness = new Wellness(1000);
            Action act = () => wellness.SetTime(new TimeSpan(10, 1, 0));
            act.Should().Throw<DomainException>().WithMessage("Zorg er a.u.b. voor dat het start uur geen minuten of seconden heeft.");

            act = () => wellness.SetTime(new TimeSpan(10, 0, 1));
            act.Should().Throw<DomainException>().WithMessage("Zorg er a.u.b. voor dat het start uur geen minuten of seconden heeft.");

            act = () => wellness.SetTime(new TimeSpan(6, 0, 0));
            act.Should().Throw<DomainException>().WithMessage("Het arangement wellness kan aleen tussen 7 en 12 uur in de ochtend geboekt worden.");

            act = () => wellness.SetTime(new TimeSpan(13, 0, 0));
            act.Should().Throw<DomainException>().WithMessage("Het arangement wellness kan aleen tussen 7 en 12 uur in de ochtend geboekt worden.");
        }

        [TestMethod]
        public void WeddingTest_Constructor()
        {
            int Price = 2000;
            TimeSpan StartHour = new TimeSpan(40, 0, 0);
            TimeSpan EndHour = new TimeSpan(40, 0, 0);
            int? ExtraHours = null;
            long StartTicks = 1440000000000;
            long EndTicks = 1440000000000;

            Wedding wedding = new Wedding(2000);

            wedding.Price.Should().Be(Price);
            wedding.StartHour.Should().Be(StartHour);
            wedding.EndHour.Should().Be(EndHour);
            wedding.ExtraHours.Should().Be(ExtraHours);
            wedding.StartHourTicks.Should().Be(StartTicks);
            wedding.EndHourTicks.Should().Be(EndTicks);
        }
        [TestMethod]
        public void WeddingTest_Methods()
        {
            Wedding wedding = new Wedding(2000);
            TimeSpan StartHour = new TimeSpan(7, 0, 0);
            TimeSpan EndHour = new TimeSpan(14, 0, 0);
            int extraHours = 2;
            int extraHourPrice = 130;
            int CalculatedPrice = 2000;

            wedding.SetTime(new TimeSpan(7, 0, 0),null);
            wedding.EndHour.Should().Be(EndHour);
            var result = wedding.GetCalculatedPrice(100);
            result.Key.Should().Be(CalculatedPrice);
            result.Value.Any(s => s.Type == "Extra uren").Should().BeFalse();


            wedding.SetTime(new TimeSpan(7, 0, 0),2);
            CalculatedPrice = 2130;
            EndHour = new TimeSpan(16, 0, 0);
            wedding.EndHour.Should().Be(EndHour);
            result = wedding.GetCalculatedPrice(100);
            wedding.GetEndTime();
            result.Key.Should().Be(CalculatedPrice);
            result.Value.Single(s => s.Type == "Extra uren").TotalPrice.Should().Be(extraHourPrice);
            result.Value.Single(s => s.Type == "Extra uren").NrOfHours.Should().Be(extraHours);
        }
        [TestMethod]
        public void WeddingTest_Exceptions()
        {
            Wedding wedding = new Wedding(1000);
            Action act = () => wedding.GetCalculatedPrice(200);
            act.Should().Throw<DomainException>().WithMessage("Zorg er aub voor dat de start- en eindtijd ingevuld zijn");

            act = () => wedding.SetTime(new TimeSpan(7, 0, 0) ,- 10);
            act.Should().Throw<DomainException>().WithMessage("Je kan geen negatief aantal extra uren hebben");

            act = () => wedding.SetTime(new TimeSpan(7, 0, 0), 10);
            act.Should().Throw<DomainException>().WithMessage("Zorg er a.u.b. voor dat het eind uur niet meer dan elf uur na het start uur is. " +
                "(Het Wedding arangement heeft een standaartduuratie van 8 uur)");
        }

        [TestMethod]
        public void NightLifeTest_Constructor()
        {
            int Price = 2000;
            TimeSpan StartHour = new TimeSpan(40, 0, 0);
            TimeSpan EndHour = new TimeSpan(40, 0, 0);
            int? ExtraHours = null;
            long StartTicks = 1440000000000;
            long EndTicks = 1440000000000;

            NightLife nightLife = new NightLife(2000);

            nightLife.Price.Should().Be(Price);
            nightLife.StartHour.Should().Be(StartHour);
            nightLife.EndHour.Should().Be(EndHour);
            nightLife.ExtraHours.Should().Be(ExtraHours);
            nightLife.StartHourTicks.Should().Be(StartTicks);
            nightLife.EndHourTicks.Should().Be(EndTicks);
        }
        [TestMethod]
        public void NightLifeTest_Methods()
        {
            NightLife nightLife = new NightLife(2000);
            TimeSpan StartHour = new TimeSpan(20, 0, 0);
            TimeSpan EndHour = new TimeSpan(27, 0, 0);
            int extraHours = 2;
            int extraHourPrice = 280;
            int CalculatedPrice = 2000;

            nightLife.SetTime(new TimeSpan(20, 0, 0),null);
            nightLife.EndHour.Should().Be(EndHour);
            var result = nightLife.GetCalculatedPrice(100);
            nightLife.GetEndTime();
            result.Key.Should().Be(CalculatedPrice);
            result.Value.Any(s => s.Type == "Extra uren").Should().BeFalse();

            nightLife.SetTime(new TimeSpan(20, 0, 0),2);
            CalculatedPrice = 2280;
            EndHour = new TimeSpan(29, 0, 0);
            nightLife.EndHour.Should().Be(EndHour);
            result = nightLife.GetCalculatedPrice(100);
            nightLife.GetEndTime();
            result.Key.Should().Be(CalculatedPrice);
            result.Value.Single(s => s.Type == "Extra uren").TotalPrice.Should().Be(extraHourPrice);
            result.Value.Single(s => s.Type == "Extra uren").NrOfHours.Should().Be(extraHours);
        }
        [TestMethod]
        public void NightLifeTest_Exceptions()
        {
            NightLife nightLife = new NightLife(2000);
            Action act = () => nightLife.GetCalculatedPrice(200);
            act.Should().Throw<DomainException>().WithMessage("Zorg er aub voor dat de start- en eindtijd ingevuld zijn");

            act = () => nightLife.SetTime(new TimeSpan(20, 0, 0), - 10);
            act.Should().Throw<DomainException>().WithMessage("Je kan geen negatief aantal extra uren hebben");

            act = () => nightLife.SetTime(new TimeSpan(20, 0, 0),10);
            act.Should().Throw<DomainException>().WithMessage("Zorg er a.u.b. voor dat het eind uur niet meer dan elf uur na het start uur is." +
                " (Het NightLife arangement heeft een standaartduuratie van 4 uur)");
        }

    }
}
