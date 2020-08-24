using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DomainLayer.Domain
{
    /// <summary>
    /// Represents errors that happen in the domain layer.
    /// </summary>
    public class DomainException : Exception
    {
        /// <summary>
        /// A constructor for a DomainException object.
        /// </summary>
        /// <param name="msg">The message of the error.</param>
        public DomainException(string msg) : base(msg)
        {

        }
    }
}
