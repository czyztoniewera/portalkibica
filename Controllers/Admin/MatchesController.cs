using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalKibica.Data;
using PortalKibica.Models;

namespace PortalKibica.Controllers.Admin
{
    [Authorize]
    [Area("Admin")]
    [Route("Admin/Matches")]
    public class MatchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var matches = await _context.Matches
                .OrderByDescending(m => m.MatchDate)
                .ToListAsync();
            return View(matches);
        }

        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Match match)
        {
            if (ModelState.IsValid)
            {
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(match);
        }

        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null) return NotFound();
            return View(match);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Match match)
        {
            if (id != match.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(match);
        }

        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null) return NotFound();
            return View(match);
        }

        [HttpPost]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match != null)
            {
                _context.Matches.Remove(match);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}