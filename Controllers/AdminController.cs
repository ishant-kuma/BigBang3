
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Tourism.Model;

namespace Tourism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly ADbContext _dbContext;

        public AdminController(ADbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("GetStoredImage")]
        public async Task<ActionResult<Admin>> GetStoredImage()
        {
            var images=_dbContext.Adminis.ToList();
            return Ok(images);
        }
    

    [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(Admin file)
        {
            _dbContext.Adminis.Add(file);
            _dbContext.SaveChanges();
            return Ok(file);
            
        }

        [HttpDelete("DeleteImage/{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _dbContext.Adminis.FindAsync(id);

            if (image == null)
            {
                return NotFound("Image not found.");
            }

            _dbContext.Adminis.Remove(image);
            await _dbContext.SaveChangesAsync();

            return Ok("Image deleted successfully.");
        }




        [HttpPost("Users/Approval")]
      
        public async Task<IActionResult> SetApprovalStatus(int UserId)
        {

            var travelagency = await _dbContext.Users.FindAsync(UserId);
            if (travelagency == null)
            {
                return NotFound(" TravelAgency not Found");
            }

            travelagency.ApprovalStatus = true;
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}/ToggleApproval")]
        public async Task<IActionResult> ToggleApprovalStatus(int id)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

               
                user.ApprovalStatus = !user.ApprovalStatus;

               
                var travelAgency = await _dbContext.Travelagencies.FirstOrDefaultAsync(t => t.UserId == id);

                if (travelAgency != null)
                {
                   
                    travelAgency.ActiveStatus = !travelAgency.ActiveStatus;
                }

                await _dbContext.SaveChangesAsync();

                return Ok(new { UserApprovalStatus = user.ApprovalStatus, AgencyApprovalStatus = travelAgency?.ActiveStatus });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }




        [HttpPost("agentactive/{agentid}/setactive")]

        public async Task<IActionResult> SetAgentActive(int agentid)
        {



            var agent = await _dbContext.Travelagencies.FindAsync(agentid);
            if (agent == null)
            {
                return NotFound("Agent not found");
            }

            var userWithSamePhoneNumber = await _dbContext.Users
        .FirstOrDefaultAsync(u => u.UserPhone == agent.TravelAgencyPhone);

            if (userWithSamePhoneNumber == null || !userWithSamePhoneNumber.ApprovalStatus)
            {
                return BadRequest("User not found or approval status is false");
            }

            agent.ActiveStatus = true;
            await _dbContext.SaveChangesAsync();

            return Ok();
        }


        [HttpGet("GetTravelAgenciesPendingApproval")]
        public IActionResult GetTravelAgenciesPendingApproval()
        {
            var travelAgencies = _dbContext.Users
                .Where(user => user.Role == "TravelAgency" && user.ApprovalStatus == false)
                .ToList();

            return Ok(travelAgencies);
        }

    }

}

