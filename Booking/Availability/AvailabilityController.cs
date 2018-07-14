using System;
using System.Threading.Tasks;
using Booking.Availability;
using Microsoft.AspNetCore.Mvc;

namespace Booking
{
    [ApiController]
    [Route("[controller]")]
    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityReader reader;

        public AvailabilityController(IAvailabilityReader reader)
        {
            this.reader = reader;
        }
        public async Task<IActionResult> GetAvailableRooms(DateTimeOffset from, DateTimeOffset to)
        {
            return null;
        }
    }
}
