using System;
using System.Collections.Generic;
using System.IO;
using DataLayer.Func;
using DomainLayer.Domain.Arangements;
using DomainLayer.Domain.Vloot;
using DomainLayer.OtherInterfaces;
using DomainLayer.Repositories;
using Newtonsoft.Json;

namespace ConsoleLayer
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        static void MakeLimousineJson()
        {

            List<Limousine> cars = new List<Limousine>();
            List<IArangement> arangements = new List<IArangement>
            {
                new NightLife(1500),
                new Wedding(1200),
                new Wellness(1600),
                new Airport(),
                new Business()
            };
            cars.Add(new Limousine(310, "Porsche Cayenne Limousine White (met TV)", 1, arangements));

            arangements = new List<IArangement>();
            arangements.Add(new NightLife(1600));
            arangements.Add(new Wedding(1300));
            arangements.Add(new Wellness(1750));
            arangements.Add(new Airport());
            arangements.Add(new Business());
            cars.Add(new Limousine(350, "Porsche Cayenne Limousine Electric Blue", 1, arangements));

            arangements = new List<IArangement>();
            arangements.Add(new Wedding(700));
            arangements.Add(new Wellness(1000));
            arangements.Add(new Airport());
            arangements.Add(new Business());
            cars.Add(new Limousine(225, "Mercedes SL 55 AMG Silver", 1, arangements));

            arangements = new List<IArangement>();
            arangements.Add(new Wedding(2500));
            arangements.Add(new Wellness(2700));
            arangements.Add(new Airport());
            arangements.Add(new Business());
            cars.Add(new Limousine(600, "Tesla Model X - White", 1, arangements));

            arangements = new List<IArangement>();
            arangements.Add(new Wedding(2000));
            arangements.Add(new Wellness(2200));
            arangements.Add(new Airport());
            arangements.Add(new Business());
            cars.Add(new Limousine(500, "Tesla Model S - White", 1, arangements));

            arangements = new List<IArangement>();
            arangements.Add(new Wedding(1000));
            arangements.Add(new Airport());
            arangements.Add(new Business());
            arangements.Add(new NightLife(1500));
            cars.Add(new Limousine(300, "Porsche Cayenne Limousine White", 4, arangements));

            arangements = new List<IArangement>();
            arangements.Add(new Wedding(650));
            arangements.Add(new Wellness(950));
            arangements.Add(new Airport());
            arangements.Add(new Business());
            arangements.Add(new NightLife(790));
            cars.Add(new Limousine(225, "Lincoln White XXL Navigator Limousine", 1, arangements));

            arangements = new List<IArangement>();
            arangements.Add(new Wedding(500));
            arangements.Add(new Wellness(1000));
            arangements.Add(new Airport());
            arangements.Add(new Business());
            arangements.Add(new NightLife(900));
            cars.Add(new Limousine(180, "Lincoln Pink Limousine", 1, arangements));

            arangements = new List<IArangement>();
            arangements.Add(new Wedding(600));
            arangements.Add(new Wellness(1000));
            arangements.Add(new Airport());
            arangements.Add(new Business());
            arangements.Add(new NightLife(850));
            cars.Add(new Limousine(195, "Lincoln Black Limousine", 1, arangements));

            arangements = new List<IArangement>();
            arangements.Add(new Wedding(790));
            arangements.Add(new Wellness(1500));
            arangements.Add(new Airport());
            arangements.Add(new Business());
            arangements.Add(new NightLife(1290));
            cars.Add(new Limousine(225, "Hummer Limousine Yellow", 1, arangements));

            arangements = new List<IArangement>();
            arangements.Add(new Wellness(1100));
            arangements.Add(new Airport());
            arangements.Add(new Business());
            arangements.Add(new NightLife(990));
            cars.Add(new Limousine(195, "Hummer Limousine Black", 1, arangements));

            arangements = new List<IArangement>();
            arangements.Add(new Airport());
            arangements.Add(new Business());
            arangements.Add(new NightLife(990));
            cars.Add(new Limousine(195, "Hummer Limousine White", 1, arangements));

            arangements = new List<IArangement>();
            arangements.Add(new Wedding(450));
            arangements.Add(new Wellness(600));
            arangements.Add(new Airport());
            arangements.Add(new Business());
            cars.Add(new Limousine(175, "Chrysler 300C Sedan - White", 1, arangements));

            arangements = new List<IArangement>();
            arangements.Add(new Wedding(450));
            arangements.Add(new Wellness(600));
            arangements.Add(new Airport());
            arangements.Add(new Business());
            cars.Add(new Limousine(175, "Chrysler 300C Sedan - Black", 1, arangements));

            arangements = new List<IArangement>();
            arangements.Add(new Wedding(500));
            arangements.Add(new Wellness(1000));
            arangements.Add(new Airport());
            arangements.Add(new Business());
            arangements.Add(new NightLife(800));
            cars.Add(new Limousine(175, "Chrysler 300C Limousine – White", 1, arangements));

            arangements = new List<IArangement>();
            arangements.Add(new Wedding(600));
            arangements.Add(new Airport());
            arangements.Add(new Business());
            arangements.Add(new NightLife(800));
            cars.Add(new Limousine(175, "Chrysler 300C Limousine – Tuxedo Crème", 1, arangements));

            var settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.All
            };
            using StreamWriter sr = new StreamWriter("test.Json");
            sr.Write(JsonConvert.SerializeObject(cars, settings));
        }
    }
}
