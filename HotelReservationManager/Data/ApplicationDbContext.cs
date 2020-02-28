using System;
using System.Collections.Generic;
using System.Text;
using HotelReservationManager.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationManager.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public async Task<bool> IsRoomFree(Room room, DateTime when)
        {
            var reservs = await Reservations.Where(x => x.Room.Id == room.Id).ToListAsync();
            foreach (var reserv in reservs)
            {
                if (reserv.CheckInTime <= when && when < reserv.CheckOutTime)
                {
                    return false;
                }
            }
            return true;
        }
        public async Task<bool> IsRoomFreeInPeriod(Room room, DateTime begin, DateTime end)
        {
            return await IsRoomFree(room, begin) && await IsRoomFree(room, end);
        }
        public async Task UpdateRoom(Room room)
        {
            room.Free = true;
            var reservs = await Reservations.Where(x => x.Room.Id == room.Id).ToListAsync();
            foreach (var reserv in reservs)
            {
                if (reserv.CheckInTime.Date <= DateTime.Now.Date && DateTime.Now.Date < reserv.CheckOutTime.Date)
                {
                    room.Free = false;
                    Update(room);
                }
            }
        }
        public async Task UpdateRooms()
        {
            var rooms = await Rooms.ToListAsync();
            foreach (var room in rooms)
            {
                await UpdateRoom(room);
            }
            await SaveChangesAsync();
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
