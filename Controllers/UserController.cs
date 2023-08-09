using makeyourtrip.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Numerics;
using Tourism.Model;

namespace Tourism.Model
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ADbContext _context;

        public UserController(ADbContext context)
        {
            _context = context;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] User model)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(a => a.UserEmail == model.UserEmail);
            if (existingUser != null)
            {
                return BadRequest("User with the same email already exists");
            }

            _context.Users.Add(model);
            await _context.SaveChangesAsync();

            if (model.Role == "TravelAgency")
            {
                var travelagency = new Travelagencies
                {
                    
                    TravelAgencyName = model.UserName,
                    TravelAgencyEmail = model.UserEmail,
                    TravelAgencyCountry = model.Country,
                    TravelAgencyState = model.State,
                    TravelAgencyCity = model.City,
                    TravelAgencyPhone = model.UserPhone,
                    TravelAgencyPassword = model.UserPassword,
                    ActiveStatus = model.ApprovalStatus,
                    UserId=model.UserId
                };

                _context.Travelagencies.Add(travelagency);
                await _context.SaveChangesAsync();
            }
            if (model.Role == "Traveller")
            {
                var Traveller = new Travellers
                {

                    TravellerName = model.UserName,
                    TravellerEmail = model.UserEmail,
                    TravellerState = model.State,
                    TravellerCity = model.City,
                    TravellerPhone = model.UserPhone,
                    TravellerPassword = model.UserPassword,
                    UserId=model.UserId
                };

                _context.Travellers.Add(Traveller);
                await _context.SaveChangesAsync();
            }

            return Ok("Registration successful");
        }




        [HttpGet("travelagents")]
        public IActionResult GetTravelAgents()
        {
            var travelAgents = _context.Users
                .Where(u => u.Role == "TravelAgency")
                .ToList();

            return Ok(travelAgents);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

 
    }
}
