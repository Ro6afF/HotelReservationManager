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
        public string SecondName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string EGN { get; set; }
        [Required]
        public DateTime HireTime { get; set; }
        [Required]
        public bool Active { get; set; }
        public DateTime? FireTime { get; set; }


    }
}