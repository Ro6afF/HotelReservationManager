﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelReservationManager.Data;
using HotelReservationManager.Data.Models;
using HotelReservationManager.Models.Room;
using Microsoft.AspNetCore.Authorization;

namespace HotelReservationManager.Controllers
{
    // TODO: Role access modifiers
    public class RoomController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rooms
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rooms.ToListAsync());
        }

        // GET: Rooms/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Capacity,Type,Free,Price,PriceChildren,Number")] CreateRoomViewModel roomVM)
        {
            if (ModelState.IsValid)
            {
                var room = new Room
                {
                    Id = Guid.NewGuid().ToString(),
                    Capacity = roomVM.Capacity,
                    Type = roomVM.Type,
                    Price = roomVM.Price,
                    PriceChildren = roomVM.PriceChildren,
                    Number = roomVM.Number,
                    Free = true
                };
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roomVM);
        }

        // GET: Rooms/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            var roomVM = new EditRoomViewModel
            {
                Capacity = room.Capacity,
                Free = room.Free,
                Id = room.Id,
                Number = room.Number,
                Price = room.Price,
                PriceChildren = room.PriceChildren,
                Type = room.Type
            };
            return View(roomVM);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit([Bind("Capacity,Type,Free,Price,PriceChildren,Number,Id")] EditRoomViewModel roomVM)
        {
            if (ModelState.IsValid)
            {
                var room = await _context.Rooms.FindAsync(roomVM.Id);
                try
                {
                    room.Capacity = roomVM.Capacity;
                    room.Type = roomVM.Type;
                    room.Free = roomVM.Free;
                    room.Price = roomVM.Price;
                    room.PriceChildren = roomVM.PriceChildren;
                    room.Number = roomVM.Number;

                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.Id))
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
            return View(roomVM);
        }

        // GET: Rooms/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var room = await _context.Rooms.FindAsync(id);
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(string id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }
    }
}
