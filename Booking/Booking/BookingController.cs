using Booking.Availability;
using Booking.Booking;
using CorrelationId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Booking
{
    [Route("[controller]")]
    public class BookingController : Controller
    {
        private readonly IAvailabilityReader availabilityReader;
        private readonly IMediator mediator;
        private readonly string correlationId;

        public BookingController(IAvailabilityReader availabilityReader,
                                 ICorrelationContextAccessor correlationContext,
                                 IMediator mediator)
        {
            this.availabilityReader = availabilityReader;
            this.mediator = mediator;
            correlationId = correlationContext.CorrelationContext.CorrelationId;
        }

        /// <summary>
        /// Books a room, subject to room availability
        /// </summary>
        /// <param name="booking">The requirements for the booking</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> BookARoom([FromBody]BookingRequest booking)
        {
            if (!await availabilityReader.IsAvailable(booking.StartDate, booking.EndDate, booking.RoomType))
                return BadRequest("Dates and/or roomType not available");

            var bookingRequestId = Guid.NewGuid();

            var bookingRequestedEvent =
                new BookingRequestedEvent(booking, bookingRequestId);

            await mediator.Publish(bookingRequestedEvent);

            return CreatedAtRoute("FetchStatus", new {id = bookingRequestId}, booking);
        }

        /// <summary>
        /// Cancels a booking
        /// </summary>
        /// <param name="id">The booking ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Cancel(int id)
        {
            return NoContent();
        }
    }
}
