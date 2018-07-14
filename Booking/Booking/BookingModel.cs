using System;

namespace Booking.Booking
{
    public class BookingModel
    {
        public int BookingId { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public RoomType RoomType { get; set; }

        public int NumberOfGuests { get; set; }

        public string Note { get; set; }
    }

    public enum RoomType
    {
        Single,
        Double,
        Suite
    }
}
