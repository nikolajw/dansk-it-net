using System;
using System.Collections.Generic;
using System.Linq;

namespace Booking.Booking
{
    public class BookingRepository : IBookingRepository
    {
        private readonly List<BookingModel> bookings = new List<BookingModel>();

        public BookingRepository()
        {
            InitRepo();
        }

        public IEnumerable<BookingModel> GetAll()
        {
            return bookings.ToArray();
        }

        public int Save(BookingModel booking)
        {
            int id = bookings.Max(b => b.BookingId) + 1;
            booking.BookingId = id;
            bookings.Add(booking);

            return id;
        }

        public BookingModel Update(BookingModel booking)
        {
            Delete(booking.BookingId);
            Save(booking);

            return booking;
        }

        public void Delete(int bookingId)
        {
            var bookingModel = bookings.FirstOrDefault(b => b.BookingId == bookingId);
            bookings.Remove(bookingModel);
        }

        private void InitRepo()
        {
            bookings.Add(
                new BookingModel()
                {
                    BookingId = 1,
                    EndDate = DateTimeOffset.Now.Date.AddDays(2),
                    StartDate = DateTimeOffset.Now.Date.AddDays(1),
                    RoomType = RoomType.Double,
                    NumberOfGuests = 2,
                    Note = "Allergic to cats"
                });
            bookings.Add(
                new BookingModel()
                {
                    BookingId = 2,
                    EndDate = DateTimeOffset.Now.Date.AddDays(4),
                    StartDate = DateTimeOffset.Now.Date.AddDays(3),
                    RoomType = RoomType.Single,
                    NumberOfGuests = 1,
                    Note = "Bottle of whiskey and bucket of ice on arrival"
                });
            bookings.Add(
                new BookingModel()
                {
                    BookingId = 3,
                    EndDate = DateTimeOffset.Now.Date.AddDays(6),
                    StartDate = DateTimeOffset.Now.Date.AddDays(4),
                    RoomType = RoomType.Suite,
                    NumberOfGuests = 2,
                    Note = "We sleep in separate beds"
                });
            bookings.Add(
                new BookingModel()
                {
                    BookingId = 4,
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
        IEnumerable<BookingModel> GetAll();

        int Save(BookingModel booking);

        BookingModel Update(BookingModel booking);

        void Delete(int bookingId);
    }
}