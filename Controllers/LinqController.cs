using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Tourism.Model;

namespace Tourism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinqController : ControllerBase
    {
        private readonly ADbContext _dbContext;

        public LinqController(ADbContext context)
        {
            _dbContext = context;
        }

        [HttpGet("filter")]
        public IActionResult FilterToursByPackageType(string tourname)
        {

            IEnumerable<Tour> allTours = _dbContext.Tours;

            IEnumerable<Tour> filteredTours = allTours.Where(tour => tour.TourName == tourname);
            
            return Ok(filteredTours);
        }
        [HttpGet("Location")]
        public IActionResult Filterlocation(string locatioin)
        {

            IEnumerable<Tour> Tours = _dbContext.Tours;

            IEnumerable<Tour> filterlocation = Tours.Where(tour => tour.TourState == locatioin);

            return Ok(filterlocation);
        }



    }
}
