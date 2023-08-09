    using System.ComponentModel.DataAnnotations;

    namespace Tourism.Model
    {
        public class User
        {
            [Key]
           public int UserId { get; set; }
            [Required]
            public string? UserName { get; set; }
            [Required]
            public string? UserEmail { get; set;}

            public string? Country { get; set; }

            public string? State { get; set; }

            public string? City { get; set; }
            public string? UserPhone { get; set;}
            [Required]
            public string? UserPassword { get; set;}
            [Required]
            public string? Role { get; set;}

            public bool ApprovalStatus { get; set; }
        
        }
    }
