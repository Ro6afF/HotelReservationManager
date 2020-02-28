using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationManager.Data.Models
{
    public class ClientReservation : DbEntry
    {
        public string ClientId { get; set; }
        public virtual Client Client { get; set; }

        public string ReservationId { get; set; }
        public virtual Reservation Reservation { get; set; }
    }
}
