using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using System.Security.Claims;
using System.Threading.Tasks;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using Tourism.Model;
namespace Tourism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingControllers : ControllerBase
    {
        private readonly ADbContext _dbContext;

        public BookingControllers(ADbContext context)
        {
            _dbContext = context;
        }

        [HttpPost]
        public IActionResult BookTour(Booking b )
        {
            try
            {
                _dbContext.Bookings.Add(b);
                _dbContext.SaveChanges();

                return Ok(b);
            }
            catch
            {
                return BadRequest("An error occurred booking the tour.");
            }
        }

        [HttpGet("Booked")]
        public async Task<ActionResult<IEnumerable<Tour>>> GetBookedTours(int userId)
        {
            var bookedTours = await _dbContext.Tours
                .Where(t => _dbContext.Bookings.Any(b => b.TourId == t.TourId && b.UserId == userId))
                .ToListAsync();

            return Ok(bookedTours);
        }

        [HttpGet("notbookedtours/{userId}")]
        public async Task<ActionResult<IEnumerable<Tour>>> GetNotBookedTours(int userId)
        {

            var unbookedTours = await _dbContext.Tours
    .Where(t => !_dbContext.Bookings.Any(b => b.TourId == t.TourId && b.UserId == userId)) // Filter unbooked tours
    .Where(t => _dbContext.Travelagencies.Any(a => a.TraveAgencyId == t.TravelAgencyId && a.ActiveStatus)) // Filter tours of active travel agents
    .ToListAsync();

            return Ok(unbookedTours);
        }

        
        [HttpGet("GeneratePDF")]
        public async Task<IActionResult> GeneratePDF(int UserId)
        {
            var booking = await _dbContext.Bookings.FirstOrDefaultAsync(b => b.UserId == UserId);

            if (booking != null)
            {
                var document = new PdfDocument();
                string HtmlContext = $@"
                     <h1>Thank you for choosing Make My Trip</h1>
                     <p>Booking ID: {booking.UserId}</p>
                     <p>Price: {booking.Price}</p>
                     <p>No. of Persons: {booking.No_Of_Persons}</p>";

                PdfGenerator.AddPdfPages(document, HtmlContext, PageSize.A4);
                byte[]? response = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    document.Save(ms);
                    response = ms.ToArray();
                }

                string Filename = "Booking" + booking.BookingId + ".pdf";

                return File(response, "application/pdf", Filename);
            }
            else
            {
                return BadRequest("Booking not found.");
            }
        }



    }
}



