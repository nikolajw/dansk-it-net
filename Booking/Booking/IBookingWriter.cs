using System;
using Booking.Booking;

namespace Booking.Availability
{
    public interface IBookingWriter
    {
        bool TryCreate(BookingRequest request, Guid bookingRequestId);
    }
}
