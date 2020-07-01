using DomainLayer.Domain.Arangements;
using System;
using System.Collections.Generic;
using DomainLayer.Domain.Help;

namespace DomainLayer.OtherInterfaces
{
    public interface IVehicle
    {
        public List<IArangement> Arangements { get; }

        public KeyValuePair<int, List<HourType>> PriceForArangement(DateTime hireDate, string arangementType, int? extraHours = null, TimeSpan? startHour = null, TimeSpan? endHour = null);
    }
}
