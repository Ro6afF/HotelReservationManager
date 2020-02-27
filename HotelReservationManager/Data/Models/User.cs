using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationManager.Data.Models
{
    public class User : IdentityUser<string>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        public override string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Username")]
        public override string UserName { get; set; }

        [Required]
        public string SecondName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string EGN { get; set; }

        [Required]
        [Display(Name = "Enabled on")]
        public DateTime HireTime { get; set; }

        [Required]
        public bool Active { get; set; }

        [Display(Name = "Disabled on")]
        public DateTime? FireTime { get; set; }


    }
}