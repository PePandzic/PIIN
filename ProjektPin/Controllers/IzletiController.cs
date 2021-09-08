using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektPin.Data;
using ProjektPin.Models;

namespace ProjektPin.Controllers
{
    public class IzletiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IzletiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Izleti
        [Authorize]
        public async Task<IActionResult> Index(string search)
        {
          
            if (!String.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;
                var izletiSearch = from ljubimci in _context.Izleti select ljubimci;
                izletiSearch = izletiSearch.Where(Izleti => Izleti.Naziv.Contains(search));
                return View(izletiSearch.ToList());
            }
            return View(await _context.Izleti.ToListAsync());
        }

        // GET: Izleti/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var izleti = await _context.Izleti
                .FirstOrDefaultAsync(m => m.Id == id);
            if (izleti == null)
            {
                return NotFound();
            }

            return View(izleti);
        }

        // GET: Izleti/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: Izleti/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Mjesto,Date")] Izleti izleti)
        {
            if (ModelState.IsValid)
            {
                _context.Add(izleti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(izleti);
        }

        // GET: Izleti/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var izleti = await _context.Izleti.FindAsync(id);
            if (izleti == null)
            {
                return NotFound();
            }
            return View(izleti);
        }

        // POST: Izleti/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Mjesto,Date")] Izleti izleti)
        {
            if (id != izleti.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(izleti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IzletiExists(izleti.Id))
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
            return View(izleti);
        }

        // GET: Izleti/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var izleti = await _context.Izleti
                .FirstOrDefaultAsync(m => m.Id == id);
            if (izleti == null)
            {
                return NotFound();
            }

            return View(izleti);
        }

        // POST: Izleti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var izleti = await _context.Izleti.FindAsync(id);
            _context.Izleti.Remove(izleti);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IzletiExists(int id)
        {
            return _context.Izleti.Any(e => e.Id == id);
        }
    }
}
