using DomainLayer.Domain.Help;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace DataLayer.Func
{
    public class JsonParser
    {
        public static List<ClientDiscount> GetDiscounts()
        {
            List<ClientDiscount> discounts = new List<ClientDiscount>();
            using (StreamReader sr = new StreamReader(@"JsonFiles\Discounts"))
            {

                discounts = JsonConvert.DeserializeObject<List<ClientDiscount>>(sr.ReadToEnd());
            }

            return discounts;
        }
    }
}
