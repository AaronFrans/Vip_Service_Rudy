using DomainLayer.Domain.Clients;
using DomainLayer.Domain.Help;
using DomainLayer.OtherInterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataLayer.Func
{
    public class Parser
    {
        public static List<ClientDiscount> GetDiscounts()
        {
            List<ClientDiscount> discounts = new List<ClientDiscount>();
            using (StreamReader sr = new StreamReader(@"Files\Discounts"))
            {

                discounts = JsonConvert.DeserializeObject<List<ClientDiscount>>(sr.ReadToEnd());
            }

            return discounts;
        }

        public static List<IKlant> GetClients()
        {
            using (StreamReader sr = new StreamReader(@"Files\klanten.txt"))
            {
                List<IKlant> toReturn = new List<IKlant>();

                List<ClientDiscount> discounts = GetDiscounts();

                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    var lines = sr.ReadLine().Split(',');
                    int clientNumber = int.Parse(lines[0]);
                    string name = lines[1];
                    string clientType = lines[2];
                    string btwNumber = lines[3];
                    Address address = MakeAddress(lines[4]);

                    IKlant toAdd = null;
                    switch (clientType.ToLower())
                    {
                        case "particulier":
                            {
                                toAdd = new Particulier(address, name, btwNumber, new Dictionary<int, int>(), discounts
                                                                                                                          .Where(d => d.ClientType.Equals("Particulier"))
                                                                                                                          .ToList());
                            }
                            break;
                        case "vip":
                            {
                                toAdd = new Vip(address, name, btwNumber, new Dictionary<int, int>(), discounts
                                                                                                                         .Where(d => d.ClientType.Equals("Vip"))
                                                                                                                         .ToList());
                            }
                            break;
                        case "organisatie":
                            {
                                toAdd = new Organisatie(address, name, btwNumber, new Dictionary<int, int>(), discounts
                                                                                                                         .Where(d => d.ClientType.Equals("Organisatie"))
                                                                                                                         .ToList());
                            }
                            break;
                        case "concertpromotor":
                            {
                                toAdd = new ConcertPromotor(address, name, btwNumber, new Dictionary<int, int>(), discounts
                                                                                                                         .Where(d => d.ClientType.Equals("ConcertPromotor"))
                                                                                                                         .ToList());
                            }
                            break;
                        case "evenementenbureau":
                            {
                                toAdd = new EvenementenBureau(address, name, btwNumber, new Dictionary<int, int>(), discounts
                                                                                                                         .Where(d => d.ClientType.Equals("EvenementenBureau"))
                                                                                                                         .ToList());
                            }
                            break;
                        case "huwelijksplanner":
                            {
                                toAdd = new HuwelijksPlanner(address, name, btwNumber, new Dictionary<int, int>(), discounts
                                                                                                                         .Where(d => d.ClientType.Equals("HuwelijksPlanner"))
                                                                                                                         .ToList());


                            }
                            break;
                    }
                    toReturn.Add(toAdd);
                }

                return toReturn;
            }
        }

        private static Address MakeAddress(string input)
        {
            var splitAddress = input.Split('-');

            string town = splitAddress[1].Trim();

            var splitStreet = splitAddress[0].Trim().Split(' ');

            string streetNr = splitStreet[1];
            string streetName = splitStreet[0];

            return new Address(streetName, town, streetNr);
        }
    }
}
