using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tourism.Model;

namespace makeyourtrip.Model
{
    public class Travellers
    {
        [Key]
        public int TravellerId { get; set; }
        public string? TravellerName { get;  set; }
        public string? TravellerEmail { get;  set; }
        public string? TravellerState { get;  set; }
        public string? TravellerCity { get;  set; }
        public string? TravellerPhone { get;  set; }
        public string? TravellerPassword { get;  set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        
    }
}
