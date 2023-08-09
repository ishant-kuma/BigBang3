using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tourism.Model
{
    public class Travelagencies
    {
        [Key]
        public int TraveAgencyId { get; set; }

        public string? TravelAgencyName { get; set; }

        public string? TravelAgencyEmail { get; set; }

        public string? TravelAgencyCountry { get; set; }

        public string? TravelAgencyState { get; set; }

        public string? TravelAgencyCity { get; set; }
        public string? TravelAgencyPhone { get; set; }

        public string? TravelAgencyPassword { get; set; }

        public bool ActiveStatus { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
