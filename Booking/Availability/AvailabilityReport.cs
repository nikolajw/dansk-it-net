using System;
using System.Collections.Generic;
using Booking.Room;

namespace Booking.Availability
{
    public class AvailabilityReport
    {
        public Dictionary<RoomType, int> Availabilities { get; }

        public DateTimeOffset FromDate { get; }

        public DateTimeOffset ToDate { get; }

        public long TimeStamp { get; }

        public AvailabilityReport(Dictionary<RoomType, int> availablities, DateTimeOffset toDate, DateTimeOffset fromDate)
        {
            TimeStamp = DateTimeOffset.UtcNow.UtcTicks;
        
            Availabilities = availablities;
            ToDate = toDate;
            FromDate = fromDate;
        }
    }
}