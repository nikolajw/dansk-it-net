using System;
using Booking.Room;

namespace Booking.Booking
{
    public class BookingRequest
    {
        public int RequestId { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public RoomType RoomType { get; set; }

        public int NumberOfGuests { get; set; }

        public string Note { get; set; }
    }
}