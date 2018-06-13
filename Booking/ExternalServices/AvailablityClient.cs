using System;

namespace Booking.ExternalServices
{
    public class AvailabilityClient
    {
        public bool CheckAvailability(DateTimeOffset startDate, DateTimeOffset endDate, TypeRoom roomType)
        {
            return true;
        }
    }

    public enum TypeRoom
    {
        Unknown,
        Single,
        Double,
        Suite
    }
}
