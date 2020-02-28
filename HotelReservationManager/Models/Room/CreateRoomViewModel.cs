﻿using HotelReservationManager.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationManager.Models.Room
{
    public class CreateRoomViewModel
    {
        public uint Capacity { get; set; }
        public RoomType Type { get; set; }
        public double Price { get; set; }

        [Display(Name = "Price for children")]
        public double PriceChildren { get; set; }
        public string Number { get; set; }
    }
}
