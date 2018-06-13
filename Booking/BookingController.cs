using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Booking.ExternalServices;

namespace Booking.Controllers
{
    [Route("api/[controller]")]
    public class BookingController : Controller
    {
        private readonly BookingRepository repository;
        private readonly EventPublisherClient eventPublisher;
        private readonly AvailabilityClient availabilityService;

        public BookingController(BookingRepository repository,
                                 EventPublisherClient eventPublisher,
                                 AvailabilityClient availabilityService)
        {
            this.repository = repository;
            this.eventPublisher = eventPublisher;
            this.availabilityService = availabilityService;
        }

        // POST api/booking
        [HttpPost]
        public IActionResult Create([FromBody]BookingModel booking)
        {
            if (!availabilityService.CheckAvailability(booking.StartDate, booking.EndDate, booking.RoomType.ToTypeRoom()))
                return BadRequest("Dates and/or roomType not available");

            var result = repository.Save(booking);

            eventPublisher.Publish(new BookingCreatedEvent(booking));

            return Created($"/api/booking/{result}", booking);
        }

        // PUT api/booking/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]BookingModel booking)
        {
            if (!availabilityService.CheckAvailability(booking.StartDate, booking.EndDate, booking.RoomType.ToTypeRoom()))
                return BadRequest("Dates and/or roomType not available");

            var result = repository.Update(booking);

            eventPublisher.Publish(new BookingUpdatedEvent(booking));

            return Ok(result);
        }

        // DELETE api/booking/5
        [HttpDelete("{id}")]
        public IActionResult Cancel(int id)
        {
            repository.Delete(id);

            eventPublisher.Publish(new BookingCancelledEvent(id));

            return NoContent();
        }

        // GET api/booking
        [HttpGet]
        public IActionResult FetchAll()
        {
            var allBookings = repository.GetAll();

            return Ok(allBookings);
        }

        // GET api/booking/5
        [HttpGet("{id}")]
        public IActionResult FetchOne(int id)
        {
            var result = repository.GetAll().FirstOrDefault(b => b.BookingId == id);

            return Ok(result);
        }
    }
}
