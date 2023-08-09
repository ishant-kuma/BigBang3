using makeyourtrip.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tourism.Model;

namespace makeyourtrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravellersController : ControllerBase
    {
        private readonly ADbContext _context; 

        public TravellersController(ADbContext context) 
        {
            _context = context;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Travellers updatedTraveller)
        {
            if (id != updatedTraveller.TravellerId)
            {
                return BadRequest();
            }

            try
            {
                var existingTraveller = await _context.Travellers.FindAsync(id);

                if (existingTraveller == null)
                {
                    return NotFound();
                }

                existingTraveller.TravellerName = updatedTraveller.TravellerName;
                existingTraveller.TravellerEmail = updatedTraveller.TravellerEmail;
                existingTraveller.TravellerState = updatedTraveller.TravellerState;
                existingTraveller.TravellerCity = updatedTraveller.TravellerCity;
                existingTraveller.TravellerPhone = updatedTraveller.TravellerPhone;
                existingTraveller.TravellerPassword = updatedTraveller.TravellerPassword;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}

