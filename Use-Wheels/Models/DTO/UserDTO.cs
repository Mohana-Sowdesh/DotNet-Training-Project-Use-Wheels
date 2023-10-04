using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Use_Wheels.Models.DTO
{
	public class UserDTO 
    {
        [RegularExpression("^[A-Za-z0-9]+$", ErrorMessage = "Username can only contain letters and numbers.")]
        public string UserName { get; set; }

        public string? Role { get; set; }

        public string Email { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z]+")]
        public string First_Name { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z]+")]
        public string Last_Name { get; set; }

        [Required]
        public DateOnly Dob { get; set; }

        [Required]
        [RegularExpression("^\\d{10}$")]
        public string Phone_Number { get; set; }

        [Required]
        [RegularExpression("a-zA-Z")]
        public string Gender { get; set; }
    }
}

