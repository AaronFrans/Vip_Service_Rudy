using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainLayer.Domain.Help
{
    /// <summary>
    /// An object reperesenting an address.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// The id for the address.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The town for the address.
        /// </summary>
        public string Town { get; private set; }
        /// <summary>
        /// The street for the address.
        /// </summary>
        public string Street { get; private set; }
        /// <summary>
        /// The street number for the address.
        /// </summary>
        public string StreetNumber { get; private set; }

        /// <summary>
        /// A constructor used to make an address object.
        /// </summary>
        /// <param name="street">The street for the address.</param>
        /// <param name="town">The town for the address.</param>
        /// <param name="streetNumber">The street number for the address.</param>
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
