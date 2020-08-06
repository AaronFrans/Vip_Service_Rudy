using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainLayer.Domain.Arangements
{
    public abstract class Arangement
    {
        public int Id { get; set; }

        public long StartHourTicks { get; protected set; }
        public long EndHourTicks { get; protected set; }

        [NotMapped]
        public TimeSpan StartHour { get; protected set; }
        [NotMapped]
        public TimeSpan EndHour { get; protected set; }
        
        static public int MaxAmountOfHours { get; private set; } = 11;


    }
}
