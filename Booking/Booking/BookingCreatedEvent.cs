using System;
using MediatR;

namespace Booking.Availability
{
    public class BookingCreatedEvent : INotification
    {
        public Guid BookingRequestBookingRequestId { get; }

        public BookingCreatedEvent(Guid bookingRequestBookingRequestId)
        {
            BookingRequestBookingRequestId = bookingRequestBookingRequestId;
        }
    }
}