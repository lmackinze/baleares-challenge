using System;
using System.ComponentModel.DataAnnotations;

namespace BalearesChallengeApp.Models.DTOs
{
    public class ContactDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Company { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        [Required]
        [Range(1, 10)]
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string ProvinceName { get; set; }
    }
}
