using System;
using System.Collections.Generic;
using System.Linq;

namespace Booking.Controllers
{
    public class BookingRepository : IBookingRepository
    {
        List<BookingModel> _bookings = new List<BookingModel>();

        public BookingRepository()
        {
            InitRepo();
        }

        public IEnumerable<BookingModel> GetAll()
        {
            return _bookings.ToArray();
        }

        public int Save(BookingModel booking)
        {
            int id = _bookings.Max(b => b.BookingId) + 1;
            booking.BookingId = id;
            _bookings.Add(booking);

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
            var bookingModel = _bookings.FirstOrDefault(b => b.BookingId == bookingId);
            _bookings.Remove(bookingModel);
        }

        private void InitRepo()
        {
            _bookings.Add(
                new BookingModel()
                {
                    BookingId = 1,
                    EndDate = DateTimeOffset.Now.Date.AddDays(2),
                    StartDate = DateTimeOffset.Now.Date.AddDays(1),
                    RoomType = RoomType.Double
                });
            _bookings.Add(
                new BookingModel()
                {
                    BookingId = 2,
                    EndDate = DateTimeOffset.Now.Date.AddDays(4),
                    StartDate = DateTimeOffset.Now.Date.AddDays(3),
                    RoomType = RoomType.Single
                });
            _bookings.Add(
                new BookingModel()
                {
                    BookingId = 3,
                    EndDate = DateTimeOffset.Now.Date.AddDays(6),
                    StartDate = DateTimeOffset.Now.Date.AddDays(4),
                    RoomType = RoomType.Suite
                });
            _bookings.Add(
                new BookingModel()
                {
                    BookingId = 4,
                    EndDate = DateTimeOffset.Now.Date.AddDays(6),
                    StartDate = DateTimeOffset.Now.Date.AddDays(5),
                    RoomType = RoomType.Double
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