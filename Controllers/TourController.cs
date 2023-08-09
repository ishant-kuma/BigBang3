using System;
using System.Linq;
using System.Net;
using System.IO;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tourism.Model;
using System.Numerics;
using System.Security.Claims;
using System.Threading.Tasks; 
using System.Reflection;

namespace Tourism.Model
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToursController : ControllerBase
    {
        private readonly ADbContext _dbContext;

        public ToursController(ADbContext context)
        {
            _dbContext = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tour>>> GetTours()
        {
            return await _dbContext.Tours.ToListAsync();
        }


        [HttpGet("{id}")]
        public ActionResult<Tour> GetTour(int id)
        {
            try
            {
                var tour = _dbContext.Tours.FirstOrDefault(t => t.TourId == id);
                if (tour == null)
                {
                    return NotFound(); 
                }

                return Ok(tour);
            }
            catch (Exception)
            {
                return StatusCode(500); 
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostTour([FromForm] Tour createTourDto, IFormFile file)
        {
            var travelAgent = _dbContext.Travelagencies.FirstOrDefault(t => t.TraveAgencyId == createTourDto.TravelAgencyId);
            if (travelAgent == null)
            {
                return NotFound("TravelAgentId not found in the TravelAgent table.");
            }
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("Please upload a valid image file.");
                }


                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var imageData = memoryStream.ToArray();

                    var tour = new Tour
                    {
                        TravelAgencyId = createTourDto.TravelAgencyId,
                        TourName = createTourDto.TourName,
                        TourCountry = createTourDto.TourCountry,
                        TourState = createTourDto.TourState,
                        TourCity = createTourDto.TourCity,
                        Itenary = createTourDto.Itenary,
                        Description = createTourDto.Description,
                        Type_of_package= createTourDto.Type_of_package,
                        AgencyImages = imageData,
                        PackagePrice = createTourDto.PackagePrice,
                        Extra_Person_Price = createTourDto.Extra_Person_Price,
                       
                        
                       
                        
                    };

              
                    _dbContext.Tours.Add(tour);
                    await _dbContext.SaveChangesAsync();

                    return Ok("Tour created successfully.");
                }
            }
            catch (Exception ex)
            {
                
            
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while creating the tour.");
            }
        }

        [HttpGet("GetToursByTravelAgent/{travelAgentId}")]
        public IActionResult GetToursByTravelAgent(int travelAgentId)
        {
            var tours = _dbContext.Tours.Where(t => t.TravelAgencyId == travelAgentId).ToList();

            if (tours.Count == 0)
            {
                return NotFound("No tours found for the specified travel agent.");
            }
            return Ok(tours);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTour(int tourId, string fieldname, [FromForm] object updatedvalue, IFormFile file)
        {

            var tour = await _dbContext.Tours.FirstOrDefaultAsync(d => d.TourId == tourId);

            if (tour == null)
            {
                return BadRequest("Tour does not exist");
            }
            if (fieldname == "TourImage")
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("Invalid file or empty file");
                }

              
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var imageData = memoryStream.ToArray();
                }
            }
            else
            {

                PropertyInfo property = typeof(Tour).GetProperty(fieldname, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (property == null)
                {
                    return BadRequest("Invalid field name");
                }

                property.SetValue(tour, Convert.ChangeType(updatedvalue, property.PropertyType));




            }
            await _dbContext.SaveChangesAsync();

            return Ok("Tour details updated successfully");




        }

       
    }

}


