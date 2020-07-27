using DomainLayer.Domain.Clients;
using DomainLayer.Domain.Help;
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
            using (StreamReader sr = new StreamReader(@"Files\Discounts"))
            {

                discounts = JsonConvert.DeserializeObject<List<ClientDiscount>>(sr.ReadToEnd());
            }

            return discounts;
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
                                

                            }
                            break;
                        case "vip":
                            {

                            }
                            break;
                        case "organisatie":
                            {
                            }
                                break;
                        case "concertpromotor":
                            {

                            }
                                break;
                        case "evenementenbureau":
                            {

                            }
                            break;
                        case "huwelijksplanner":
                            {
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
