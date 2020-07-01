using DomainLayer.Domain.Help;
using DomainLayer.Domain.Reservation;
using DomainLayer.OtherInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Repositories
{
    public interface IVloot
    {

        public void HireVehicle(string name, string typeArangement, Address location, IKlant client, DateTime reservationDate, Address endLocation, DateTime dateLimousineNeeded,
             int? extraHours = null, TimeSpan? startHour = null, TimeSpan? endHour = null);
    }
}
