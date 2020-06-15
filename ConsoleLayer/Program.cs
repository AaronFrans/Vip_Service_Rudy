using System;
using System.Collections.Generic;
using System.IO;
using DataLayer.Func;
using DomainLayer.Domain.Arangements;
using DomainLayer.Repositories;
using Newtonsoft.Json;

namespace ConsoleLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IArangement> arangements = new List<IArangement>();

            var settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.All
            };
            using (StreamReader sr = new StreamReader("test.Json"))
            {
                List<IArangement> PLSWOrk = JsonConvert.DeserializeObject<List<IArangement>>(sr.ReadToEnd(), settings);
            }
        }
    }
}
