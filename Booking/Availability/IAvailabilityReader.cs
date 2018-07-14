using System;
using Booking.Booking;

namespace Booking.Availability
{
    public interface IAvailabilityReader
    {
        bool IsAvailable(DateTimeOffset bookingStartDate, DateTimeOffset bookingEndDate, RoomType toTypeRoom);
    }
}