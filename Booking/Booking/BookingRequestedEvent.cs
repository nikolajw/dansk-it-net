using System;
using MediatR;

namespace Booking.Booking
{
    public class BookingRequestedEvent : INotification
    {
        public BookingRequest Request { get; }
        public Guid BookingRequestId { get; }

        public BookingRequestedEvent(BookingRequest request, Guid bookingRequestId)
        {
            Request = request;
            BookingRequestId = bookingRequestId;
        }
    }
}