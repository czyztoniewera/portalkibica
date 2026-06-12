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

        // Lista zawodników (publiczna)
        public async Task<IActionResult> Index()
        {
            var players = await _context.Players
                .OrderBy(p => p.Number)
                .ToListAsync();
            return View(players);
        }

        // Szczegóły zawodnika
        public async Task<IActionResult> Details(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null) return NotFound();
            return View(player);
        }
    }
}