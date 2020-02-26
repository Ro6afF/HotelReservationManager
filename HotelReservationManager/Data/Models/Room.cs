using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationManager.Data.Models
{
    public class Room : DbEntry
    {
        public uint Capacity { get; set; }
        public RoomType Type { get; set; }
        public bool Free { get; set; }
        public double Pricre { get; set; }
        public double PriceChildren { get; set; }
        public string Number { get; set; }
    }
}
