using Microsoft.AspNetCore.Mvc;

namespace Booking.Request
{
    public class RequestController : ControllerBase
    {
        public RequestController()
        {
            
        }

        /// <summary>
        /// Fetches a booking
        /// </summary>
        /// <param name="id">ID of the booking to fetch</param>
        /// <returns></returns>
        [HttpGet("{id}/status", Name = "FetchStatus")]
        public IActionResult FetchStatus(int id)
        {
            return Ok();
        }

    }
}
