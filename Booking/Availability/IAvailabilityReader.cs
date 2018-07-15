using Booking.Room;
using System;
using System.Threading.Tasks;

namespace Booking.Availability
{
    public interface IAvailabilityReader
    {
        Task<bool> IsAvailable(DateTimeOffset bookingStartDate, DateTimeOffset bookingEndDate, RoomType toTypeRoom);
        Task<AvailabilityReport> GetAvailabilities(DateTimeOffset @from, DateTimeOffset to);
    }
}