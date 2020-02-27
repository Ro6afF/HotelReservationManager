using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationManager.Data.Models
{
    public class Reservation : DbEntry
    {
        [Required]
        public Room Room { get; set; }
        [Required]
        public User Creator { get; set; }
        [Required]
        public IEnumerable<Client> Guests { get; set; }
        [Required]
        public DateTime CheckInTime { get; set; }
        [Required]
        public DateTime CheckOutTime { get; set; }
        [Required]
        public bool Breakfast { get; set; }
        [Required]
        public bool AllInclusive { get; set; }
    }
}
