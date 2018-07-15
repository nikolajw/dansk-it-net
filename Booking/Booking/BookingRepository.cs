using System;
using System.Collections.Generic;
using System.Linq;
using Booking.Room;

namespace Booking.Booking
{
    public class BookingRepository : IBookingRepository
    {
        private readonly List<BookingRequest> bookings = new List<BookingRequest>();

        public BookingRepository()
        {
            InitRepo();
        }

        public IEnumerable<BookingRequest> GetAll()
        {
            return bookings.ToArray();
        }

        public int Save(BookingRequest booking)
        {
            int id = bookings.Max(b => b.RequestId) + 1;
            booking.RequestId = id;
            bookings.Add(booking);

            return id;
        }

        public BookingRequest Update(BookingRequest booking)
        {
            Delete(booking.RequestId);
            Save(booking);

            return booking;
        }

        public void Delete(int bookingId)
        {
            var bookingModel = bookings.FirstOrDefault(b => b.RequestId == bookingId);
            bookings.Remove(bookingModel);
        }

        private void InitRepo()
        {
            bookings.Add(
                new BookingRequest()
                {
                    RequestId = 1,
                    EndDate = DateTimeOffset.Now.Date.AddDays(2),
                    StartDate = DateTimeOffset.Now.Date.AddDays(1),
                    RoomType = RoomType.Double,
                    NumberOfGuests = 2,
                    Note = "Allergic to cats"
                });
            bookings.Add(
                new BookingRequest()
                {
                    RequestId = 2,
                    EndDate = DateTimeOffset.Now.Date.AddDays(4),
                    StartDate = DateTimeOffset.Now.Date.AddDays(3),
                    RoomType = RoomType.Single,
                    NumberOfGuests = 1,
                    Note = "Bottle of whiskey and bucket of ice on arrival"
                });
            bookings.Add(
                new BookingRequest()
                {
                    RequestId = 3,
                    EndDate = DateTimeOffset.Now.Date.AddDays(6),
                    StartDate = DateTimeOffset.Now.Date.AddDays(4),
                    RoomType = RoomType.Suite,
                    NumberOfGuests = 2,
                    Note = "We sleep in separate beds"
                });
            bookings.Add(
                new BookingRequest()
                {
                    RequestId = 4,
                    EndDate = DateTimeOffset.Now.Date.AddDays(6),
                    StartDate = DateTimeOffset.Now.Date.AddDays(5),
                    RoomType = RoomType.Double,
                    NumberOfGuests = 2,
                    Note = "Need parking space"
                });
        }
    }

    public interface IBookingRepository
    {
        IEnumerable<BookingRequest> GetAll();

        int Save(BookingRequest booking);

        BookingRequest Update(BookingRequest booking);

        void Delete(int bookingId);
    }
}