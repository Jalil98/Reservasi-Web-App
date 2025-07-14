using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservasiRuangApp.Data;
using ReservasiRuangApp.Models;

namespace ReservasiRuangApp.Controllers
{
    public class ReservasiController : Controller
    {
        private readonly AppDbContext _context;

        public ReservasiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Reservasis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reservasi.ToListAsync());
        }

        // GET: Reservasis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservasi = await _context.Reservasi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservasi == null)
            {
                return NotFound();
            }

            return View(reservasi);
        }

        // GET: Reservasis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reservasis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NamaPemesan,NamaRuangan,WaktuMulai,WaktuSelesai,Keterangan")] Reservasi reservasi)
        {
            if (ModelState.IsValid)
            {
                // Cek bentrok jadwal
                var bentrok = _context.Reservasi.Any(r =>
                    r.NamaRuangan == reservasi.NamaRuangan &&
                    r.WaktuMulai < reservasi.WaktuSelesai &&
                    r.WaktuSelesai > reservasi.WaktuMulai
                );

                if (bentrok)
                {
                    ModelState.AddModelError("", "Ruangan sudah dibooking pada waktu tersebut.");
                    return View(reservasi);
                }

                _context.Add(reservasi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // PERLU ini agar tetap merender form bila gagal validasi
            return View(reservasi);
        }


        // GET: Reservasis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservasi = await _context.Reservasi.FindAsync(id);
            if (reservasi == null)
            {
                return NotFound();
            }
            return View(reservasi);
        }

        // POST: Reservasis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NamaPemesan,NamaRuangan,WaktuMulai,WaktuSelesai,Keterangan")] Reservasi reservasi)
        {
            if (id != reservasi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Cek bentrok jadwal, abaikan dirinya sendiri (r.Id != reservasi.Id)
                var bentrok = _context.Reservasi.Any(r =>
                    r.NamaRuangan == reservasi.NamaRuangan &&
                    r.Id != reservasi.Id &&
                    r.WaktuMulai < reservasi.WaktuSelesai &&
                    r.WaktuSelesai > reservasi.WaktuMulai
                );

                if (bentrok)
                {
                    ModelState.AddModelError("", "Ruangan sudah dibooking pada waktu tersebut.");
                    return View(reservasi);
                }

                try
                {
                    _context.Update(reservasi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservasiExists(reservasi.Id))
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
            return View(reservasi);
        }

        // GET: Reservasis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservasi = await _context.Reservasi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservasi == null)
            {
                return NotFound();
            }

            return View(reservasi);
        }

        // POST: Reservasis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservasi = await _context.Reservasi.FindAsync(id);
            if (reservasi != null)
            {
                _context.Reservasi.Remove(reservasi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservasiExists(int id)
        {
            return _context.Reservasi.Any(e => e.Id == id);
        }
    }
}
