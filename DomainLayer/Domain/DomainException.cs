using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DomainLayer.Domain
{
    public class DomainException : Exception
    {
        public DomainException(string msg) : base(msg)
        {

        }
    }
}
