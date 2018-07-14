﻿using System;
using System.Linq;
using Booking.Availability;
using Booking.Booking;
using CorrelationId;
using Microsoft.AspNetCore.Mvc;

namespace Booking
{
    [Route("[controller]")]
    public class BookingController : Controller
    {
        private readonly IBookingRepository repository;
        private readonly IAvailabilityReader availabilityReader;
        private readonly string correlationId;

        public BookingController(IBookingRepository repository,
                                 IAvailabilityReader availabilityReader,
                                 ICorrelationContextAccessor correlationContext)
        {
            this.repository = repository;
            this.availabilityReader = availabilityReader;
            correlationId = correlationContext.CorrelationContext.CorrelationId;
        }

        /// <summary>
        /// Books a room, subject to room availability
        /// </summary>
        /// <param name="booking">The requirements for the booking</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody]BookingModel booking)
        {
            if (!availabilityReader.IsAvailable(booking.StartDate, booking.EndDate, booking.RoomType))
                return BadRequest("Dates and/or roomType not available");

            var bookingId = Guid.NewGuid();

            return CreatedAtRoute("FetchBooking", new {id = bookingId}, booking);
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
            if (!availabilityReader.IsAvailable(booking.StartDate, booking.EndDate, booking.RoomType))
                return BadRequest("Dates and/or roomType not available");

            var result = repository.Update(booking);

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
        [HttpGet("{id}", Name = "FetchBooking")]
        public IActionResult FetchOne(int id)
        {
            var result = repository.GetAll().FirstOrDefault(b => b.BookingId == id);

            return Ok(result);
        }
    }
}