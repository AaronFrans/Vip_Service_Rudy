using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainLayer.Domain.Help
{
    public class Address
    {
       
        public string Town { get; private set; }
        
        public string Street { get; private set; }
        
        public string StreetNumber { get; private set; }

        public Address(string street, string town, string streetNumber)
        {
            Street = street;
            Town = town;
            StreetNumber = streetNumber;
        }
        public override bool Equals(object obj)
        {
            return obj is Address address &&
                   Street == address.Street &&
                   Town == address.Town &&
                   StreetNumber == address.StreetNumber;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Street, Town, StreetNumber);
        }
    }
}
