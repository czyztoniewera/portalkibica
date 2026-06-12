using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalKibica.Data;
using PortalKibica.Models;

namespace PortalKibica.Controllers
{
    public class MatchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lista meczów (publiczna) - nadchodzące i rozegrane
        public async Task<IActionResult> Index()
        {
            var matches = await _context.Matches
                .OrderBy(m => m.MatchDate)
                .ToListAsync();
            return View(matches);
        }
    }
}