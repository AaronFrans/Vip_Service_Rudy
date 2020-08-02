using DomainLayer.Domain.Clients;
using DomainLayer.Domain.Help;
using DomainLayer.Domain.Vloot;
using Newtonsoft.Json;
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
            using (StreamReader sr = new StreamReader(@"Files\Discounts.json"))
            {

                discounts = JsonConvert.DeserializeObject<List<ClientDiscount>>(sr.ReadToEnd());
            }

            return discounts;
        }

        public static List<Limousine> GetLimousines()
        {
            List<Limousine> limousines = new List<Limousine>();
            var settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.All
            };
            using (StreamReader sr = new StreamReader(@"Files\Limousines.json"))
            {

                limousines = JsonConvert.DeserializeObject<List<Limousine>>(sr.ReadToEnd(),settings);
            }

            return limousines;
        }

        public static List<Client> GetClients()
        {
            using (StreamReader sr = new StreamReader(@"Files\klanten.txt"))
            {
                List<Client> toReturn = new List<Client>();

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

                    Client toAdd = null;
                    switch (clientType.ToLower())
                    {
                        case "particulier":
                            {
                                toAdd = new Client(ClientType.Particulier, discounts.Where(d => d.ClientType == ClientType.Particulier).ToList(),
                                    address, name, btwNumber, new List<ReservationsPerYear>());
                                
                            }
                            break;
                        case "vip":
                            {
                                toAdd = new Client(ClientType.Vip, discounts.Where(d => d.ClientType == ClientType.Vip).ToList(),
                                    address, name, btwNumber, new List<ReservationsPerYear>());
                            }
                            break;
                        case "organisatie":
                            {
                                toAdd = new Client(ClientType.Organisatie, discounts.Where(d => d.ClientType == ClientType.Organisatie).ToList(),
                                    address, name, btwNumber, new List<ReservationsPerYear>());
                            }
                                break;
                        case "concertpromotor":
                            {
                                toAdd = new Client(ClientType.ConcertPromotor, discounts.Where(d => d.ClientType == ClientType.ConcertPromotor).ToList(),
                                    address, name, btwNumber, new List<ReservationsPerYear>());
                            }
                                break;
                        case "evenementenbureau":
                            {
                                toAdd = new Client(ClientType.EvenementenBureau, discounts.Where(d => d.ClientType == ClientType.EvenementenBureau).ToList(),
                                    address, name, btwNumber, new List<ReservationsPerYear>());
                            }
                            break;
                        case "huwelijksplanner":
                            {
                                toAdd = new Client(ClientType.HuwelijksPlanner, discounts.Where(d => d.ClientType == ClientType.HuwelijksPlanner).ToList(),
                                    address, name, btwNumber, new List<ReservationsPerYear>());
                            }
                            break;
                    }
                    toAdd.ClientNumber = clientNumber;
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
