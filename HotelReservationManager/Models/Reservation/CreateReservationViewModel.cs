using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationManager.Models.Reservation
{
    public class CreateReservationViewModel
    {
        public IEnumerable<Data.Models.Room> AvaiableRooms { get; set; }
        public IEnumerable<Data.Models.Client> AvaiableGuests { get; set; }
        public string RoomId { get; set; }
        public string CreatorId { get; set; }
        public IEnumerable<Data.Models.Client> Clients { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Check-in")]
        public DateTime CheckInTime { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Check-out")]
        public DateTime CheckOutTime { get; set; }
        public bool Breakfast { get; set; }
        public bool AllInclusive { get; set; }
    }
}
