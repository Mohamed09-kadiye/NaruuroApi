using Microsoft.AspNetCore.Mvc;
using NaruuroApi.Model.Repository;
using NaruuroApi.Model;
using NaruuroApi.Model.Interface;

namespace NaruuroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBooking _bookingRepository;

        public BookingsController(IBooking bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        // GET: api/Bookings
        [HttpGet]
        public IActionResult Get()
        {
            var bookings = _bookingRepository.GetBookings();
            return Ok(bookings);
        }

        // GET api/Bookings/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var booking = _bookingRepository.GetBookingById(id);
            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        // POST api/Bookings
        [HttpPost]
        public IActionResult Post([FromBody] Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _bookingRepository.AddBooking(booking);
            return CreatedAtAction(nameof(Get), new { id = booking.ID }, booking);
        }

        [HttpPost("refresh")]
        public IActionResult RefreshBookingData()
        {
            _bookingRepository.ExecuteRefreshProcedure();
            return Ok("Refresh completed.");
        }

        // PUT api/Bookings/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingBooking = _bookingRepository.GetBookingById(id);
            if (existingBooking == null)
            {
                return NotFound();
            }

            booking.ID = id;
            _bookingRepository.UpdateBooking(booking);
            return NoContent();
        }

        // DELETE api/Bookings/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingBooking = _bookingRepository.GetBookingById(id);
            if (existingBooking == null)
            {
                return NotFound();
            }

            _bookingRepository.DeleteBooking(id);
            return NoContent();
        }
    }
}
