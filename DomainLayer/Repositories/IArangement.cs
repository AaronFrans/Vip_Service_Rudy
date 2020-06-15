using System;

namespace DomainLayer.Repositories
{
    public interface IArangement
    {
        public TimeSpan StartHour { get; }
        public TimeSpan EndHour { get; }
        public TimeSpan NightHourBegin { get; }
        public TimeSpan NightHourEnd { get; }
        public int MaxAmountOfHours { get; }
    }
}
