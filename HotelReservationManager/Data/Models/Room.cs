using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationManager.Data.Models
{
    public class Room : DbEntry
    {
        [Required]
        public uint Capacity { get; set; }

        [Required]
        public RoomType Type { get; set; }

        [Required]
        public bool Free { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public double PriceChildren { get; set; }

        [Required]
        public string Number { get; set; }
    }
}
