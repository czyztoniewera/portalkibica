using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalKibica.Data;
using PortalKibica.Models;

namespace PortalKibica.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lista zawodnikow (publiczna) z opcjonalnym filtrem pozycji
        public async Task<IActionResult> Index(string? position)
        {
            var playersQuery = _context.Players.OrderBy(p => p.Number).AsQueryable();

            if (!string.IsNullOrWhiteSpace(position))
            {
                playersQuery = playersQuery.Where(p => p.Position == position);
            }

            var players = await playersQuery.ToListAsync();

            var allPositions = await _context.Players
                .Select(p => p.Position)
                .Distinct()
                .OrderBy(p => p)
                .ToListAsync();

            ViewData["AllPositions"] = allPositions;
            ViewData["SelectedPosition"] = position;

            return View(players);
        }

        // Szczegoly zawodnika
        public async Task<IActionResult> Details(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null) return NotFound();
            return View(player);
        }
    }
}