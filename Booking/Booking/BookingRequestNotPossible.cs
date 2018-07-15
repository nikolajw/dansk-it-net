using System;
using MediatR;

namespace Booking.Availability
{
    public class BookingRequestNotPossible : INotification
    {
        public Guid NotificationBookingRequestId { get; }

        public BookingRequestNotPossible(Guid notificationBookingRequestId)
        {
            NotificationBookingRequestId = notificationBookingRequestId;
        }
    }
}