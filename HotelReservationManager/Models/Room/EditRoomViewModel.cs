﻿using HotelReservationManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationManager.Models.Room
{
    public class EditRoomViewModel
    {
        public string Id { get; set; }
        public uint Capacity { get; set; }
        public RoomType Type { get; set; }
        public bool Free { get; set; }
        public double Price { get; set; }
        public double PriceChildren { get; set; }
        public string Number { get; set; }
    }
}
