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

        public IEnumerable<Client> Guests { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Check-in")]
        public DateTime CheckInTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Check-out")]
        public DateTime CheckOutTime { get; set; }

        [Required]
        public bool Breakfast { get; set; }


        [Required]
        [Display(Name = "All inclusive")]
        public bool AllInclusive { get; set; }


        [Required]
        [Display(Name = "Total price")]
        public double TotalPrice { get; set; }

    }
}
