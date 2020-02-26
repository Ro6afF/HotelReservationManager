using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationManager.Data.Models
{
    public class User : IdentityUser<string>
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string EGN { get; set; }
        public DateTime HireTime { get; set; }
        public bool Active { get; set; }
        public DateTime? FireTime { get; set; }


    }
}