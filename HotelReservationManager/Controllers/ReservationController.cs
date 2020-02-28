using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelReservationManager.Data;
using HotelReservationManager.Data.Models;
using HotelReservationManager.Models.Reservation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace HotelReservationManager.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ReservationController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reservation
        public async Task<IActionResult> Index()
        {
            var reservations = await _context.Reservations.Include(x => x.Creator).Include(x => x.Room).Include(x => x.Guests).ToListAsync();
            return View(reservations);
        }

        // GET: Reservation/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.Include(x => x.Creator).Include(x => x.Room).Include(x => x.Guests).FirstOrDefaultAsync(m => m.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservation/Create
        public async Task<IActionResult> Create()
        {
            _context.UpdateRooms();
            var reservationVM = new CreateReservationViewModel
            {
                AvaiableRooms = await _context.Rooms.Where(x => x.Free).ToListAsync(),
                AvaiableGuests = await _context.Clients.ToListAsync(),
                CheckInTime = DateTime.Now,
                CheckOutTime = DateTime.Now
            };
            return View(reservationVM);
        }

        // POST: Reservation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CheckInTime,CheckOutTime,Breakfast,AllInclusive,Id,RoomId")] CreateReservationViewModel reservationVM)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _context.Users.FindAsync(_userManager.GetUserId(User));
                if (currentUser == null)
                {
                    return Unauthorized();
                }

                var selectedRoom = await _context.Rooms.FindAsync(reservationVM.RoomId);
                if (selectedRoom == null)
                {
                    return NotFound();
                }
                selectedRoom.Free = false;
                _context.Update(selectedRoom);


                var reservation = new Reservation
                {
                    Id = Guid.NewGuid().ToString(),
                    AllInclusive = reservationVM.AllInclusive,
                    Breakfast = reservationVM.Breakfast,
                    CheckInTime = reservationVM.CheckInTime,
                    CheckOutTime = reservationVM.CheckOutTime,
                    // Guests = reservationVM.Clients,
                    Guests = _context.Clients.ToList(),
                    Room = selectedRoom,
                    Creator = currentUser,
                    TotalPrice = 0
                };

                double price = 0;
                foreach (var client in reservation.Guests)
                {
                    price += (client.Mature) ? reservation.Room.Price : reservation.Room.PriceChildren;
                }
                reservation.TotalPrice = price;

                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservationVM);
        }

        // GET: Reservation/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.Include(x => x.Guests).Include(x => x.Room).Where(x => x.Id == id).FirstOrDefaultAsync();
            if (reservation == null)
            {
                return NotFound();
            }
            var reservationVM = new EditReservationViewModel
            {
                AllInclusive = reservation.AllInclusive,
                Breakfast = reservation.Breakfast,
                CheckInTime = reservation.CheckInTime,
                CheckOutTime = reservation.CheckOutTime,
                Clients = reservation.Guests,
                RoomId = reservation.Room.Id,
                Id = reservation.Id,
                AvaiableRooms = await _context.Rooms.Where(x => x.Free).ToListAsync(),
                AvaiableGuests = await _context.Clients.ToListAsync()
            };
            return View(reservationVM);
        }

        // POST: Reservation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("CheckInTime,CheckOutTime,Breakfast,AllInclusive,Id,RoomId,Clients")] EditReservationViewModel reservationVM)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _context.Users.FindAsync(_userManager.GetUserId(User));
                if (currentUser == null)
                {
                    return Unauthorized();
                }
                var reservation = await _context.Reservations.Include(x => x.Room).Where(x => x.Id == reservationVM.Id).FirstOrDefaultAsync();
                var prevRoom = await _context.Rooms.FindAsync(reservation.Room.Id);
                if (prevRoom != null)
                {
                    prevRoom.Free = true;
                    _context.Update(prevRoom);
                }

                var selectedRoom = await _context.Rooms.FindAsync(reservationVM.RoomId);
                if (selectedRoom == null)
                {
                    return NotFound();
                }
                selectedRoom.Free = false;
                _context.Update(selectedRoom);
                try
                {
                    reservation.AllInclusive = reservationVM.AllInclusive;
                    reservation.Breakfast = reservationVM.Breakfast;
                    reservation.CheckInTime = reservationVM.CheckInTime;
                    reservation.CheckOutTime = reservation.CheckOutTime;
                    reservation.Creator = currentUser;
                    reservation.Guests = _context.Clients.ToList();
                    reservation.Room = selectedRoom;
                    double price = 0;
                    foreach (var client in reservation.Guests)
                    {
                        price += (client.Mature) ? reservation.Room.Price : reservation.Room.PriceChildren;
                    }
                    reservation.TotalPrice = price;
                    if (selectedRoom == null)
                    {
                        return NotFound();
                    }
                    selectedRoom.Free = false;
                    _context.Update(selectedRoom);
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservationVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reservationVM);
        }

        // GET: Reservation/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.Include(x => x.Creator).Include(x => x.Room).Include(x => x.Guests)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // TODO: Free the room
        // POST: Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var reservation = await _context.Reservations.Include(x => x.Room).Where(x => x.Id == id).FirstOrDefaultAsync();
            var selectedRoom = await _context.Rooms.FindAsync(reservation.Room.Id);
            if (selectedRoom != null)
            {
                selectedRoom.Free = false;
                _context.Update(selectedRoom);
            }
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(string id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
