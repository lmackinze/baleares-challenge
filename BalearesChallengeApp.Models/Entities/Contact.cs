using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BalearesChallengeApp.Models.Entities
{
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Company { get; set; }

        [Required]
        public string Email { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        [Required]
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public bool IsDeleted { get; set; }
    }
}
