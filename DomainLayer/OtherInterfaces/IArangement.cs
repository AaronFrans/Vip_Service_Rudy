using System;

namespace DomainLayer.OtherInterfaces
{
    public interface IArangement
    {
        public TimeSpan StartHour { get; }
        public TimeSpan EndHour { get; }
        public TimeSpan NightHourBegin { get; }
        public TimeSpan NightHourEnd { get; }
        public int MaxAmountOfHours { get; }
        public TimeSpan GetEndTime();
    }
}
