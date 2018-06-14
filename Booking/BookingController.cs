using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Booking.ExternalServices;

namespace Booking
{
    [Route("api/[controller]")]
    public class BookingController : Controller
    {
        private readonly IBookingRepository repository;
        private readonly EventPublisherClient eventPublisher;
        private readonly AvailabilityClient availabilityService;

        public BookingController(IBookingRepository repository,
                                 EventPublisherClient eventPublisher,
                                 AvailabilityClient availabilityService)
        {
            this.repository = repository;
            this.eventPublisher = eventPublisher;
            this.availabilityService = availabilityService;
        }

        /// <summary>
        /// Books a room, subject to room availability
        /// </summary>
        /// <param name="booking">The requirements for the booking</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody]BookingModel booking)
        {
            if (!availabilityService.CheckAvailability(booking.StartDate, booking.EndDate, booking.RoomType.ToTypeRoom()))
                return BadRequest("Dates and/or roomType not available");

            var result = repository.Save(booking);

            eventPublisher.Publish(new BookingCreatedEvent(booking));

            return Created($"/api/booking/{result}", booking);
        }

        /// <summary>
        /// Updates a booking, subject to room availability
        /// </summary>
        /// <param name="id">The booking ID</param>
        /// <param name="booking"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]BookingModel booking)
        {
            if (!availabilityService.CheckAvailability(booking.StartDate, booking.EndDate, booking.RoomType.ToTypeRoom()))
                return BadRequest("Dates and/or roomType not available");

            var result = repository.Update(booking);

            eventPublisher.Publish(new BookingUpdatedEvent(booking));

            return Ok(result);
        }

        /// <summary>
        /// Cancels a booking
        /// </summary>
        /// <param name="id">The booking ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Cancel(int id)
        {
            repository.Delete(id);

            eventPublisher.Publish(new BookingCancelledEvent(id));

            return NoContent();
        }

        /// <summary>
        /// Fetches all bookings today or in the future
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult FetchAll()
        {
            var allBookings = repository.GetAll().Where(b => b.StartDate >= DateTimeOffset.Now.Date);

            return Ok(allBookings);
        }

        /// <summary>
        /// Fetches a booking
        /// </summary>
        /// <param name="id">ID of the booking to fetch</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult FetchOne(int id)
        {
            var result = repository.GetAll().FirstOrDefault(b => b.BookingId == id);

            return Ok(result);
        }
    }
}
