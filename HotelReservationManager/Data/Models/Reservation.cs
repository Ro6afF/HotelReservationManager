using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationManager.Data.Models
{
    public class Reservation : DbEntry
    {
        public Room Room { get; set; }
        public User Creator { get; set; }
        public IEnumerable<Client> Guests { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public bool Breakfast { get; set; }
        public bool AllInclusive { get; set; }
    }
}
