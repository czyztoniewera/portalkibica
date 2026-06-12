using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalKibica.Data;
using Microsoft.EntityFrameworkCore;

namespace PortalKibica.Controllers.Admin
{
    [Authorize]
    [Area("Admin")]
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("")]
        [Route("Dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            ViewBag.NewsCount = await _context.News.CountAsync();
            ViewBag.PlayersCount = await _context.Players.CountAsync();
            ViewBag.MatchesCount = await _context.Matches.CountAsync();
            return View();
        }
    }
}