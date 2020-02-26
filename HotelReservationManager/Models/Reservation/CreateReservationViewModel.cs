using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationManager.Models.Reservation
{
    public class CreateReservationViewModel
    {
        public List<Data.Models.Room> AvaiableRooms { get; set; }
        public List<Data.Models.Client> AvaiableGuests { get; set; }
        public Data.Models.Room Room { get; set; }
        public string CreatorId { get; set; }
        public List<Data.Models.Client> Clients { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public bool Breakfast { get; set; }
        public bool AllInclusive { get; set; }
    }
}
