using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Domain.Help
{
    public class HireDate
    {
        public int Id { get; set; }

       public DateTime Date { get; set; }

        public HireDate( DateTime date)
        {
            Date = date;
        }
    }
}
